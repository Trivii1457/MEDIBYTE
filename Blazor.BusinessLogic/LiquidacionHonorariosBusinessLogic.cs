using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class LiquidacionHonorariosLogic : GenericBusinessLogic<LiquidacionHonorarios>
    {
        public LiquidacionHonorariosLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public LiquidacionHonorariosLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public override LiquidacionHonorarios Add(LiquidacionHonorarios data)
        {
            data.Consecutivo = LiquidacionConsecutivo();
            return base.Add(data);
        }

        public List<LiquidacionHonorariosDetalle> GetDetalleALiquidar(long liquidacionId)
        {
            List<LiquidacionHonorariosDetalle> resultadoTotal = new List<LiquidacionHonorariosDetalle>();
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            if (liquidacionId <= 0)
                return new List<LiquidacionHonorariosDetalle>();
            var liquidacion = unitOfWork.Repository<LiquidacionHonorarios>().FindById(x => x.Id == liquidacionId, true);
            if (liquidacion.EstadosId != 10064)
                return new List<LiquidacionHonorariosDetalle>();

            #region Atenciones

            var servicios = unitOfWork.Repository<AdmisionesServiciosPrestados>().Table
                .Include(x => x.Servicios)
                .Include(x => x.Atenciones)
                .Include(x => x.Atenciones.Empleados)
                .Include(x => x.Admisiones)
                .Include(x => x.Admisiones.Pacientes)
                .Include(x => x.Admisiones.ProgramacionCitas)
                .Where(x => x.Atenciones.FechaAtencion.Date >= liquidacion.FechaInicio.Date &&
                x.Atenciones.FechaAtencion.Date <= liquidacion.FechaFinal.Date &&
                x.Atenciones.Empleados.TipoEmpleados == 2 &&
                x.Atenciones.EstadosId == 10076 &&
                !unitOfWork.Repository<LiquidacionHonorariosDetalle>().Table.Include(j => j.LiquidacionHonorarios)
                    .Where(j => j.LiquidacionHonorarios.EstadosId != 10065)
                    .Select(j => j.AdmisionesServiciosPrestadosId).Contains(x.Id)
                );

            if (liquidacion.EmpleadosId != null)
                servicios = servicios.Where(x => x.Atenciones.EmpleadosId == liquidacion.EmpleadosId);

            List<AdmisionesServiciosPrestados> resultServicio = servicios.ToList();

            int idData = 1;
            foreach (var servicioPrestado in resultServicio)
            {
                LiquidacionHonorariosDetalle item = new LiquidacionHonorariosDetalle();
                item.LiquidacionHonorariosId = liquidacionId;
                item.EmpleadosId = servicioPrestado.Atenciones.EmpleadosId;
                item.Empleados = servicioPrestado.Atenciones.Empleados;
                item.AdmisionesServiciosPrestadosId = servicioPrestado.Id;
                item.AdmisionesServiciosPrestados = servicioPrestado;
                item.AdmisionesServiciosPrestados.Cantidad = servicioPrestado.Cantidad;
                item.EsLectura = false;
                item.AtencionesResultadoId = null;

                //Se calcula el valor a liquidar para el empleado del servicio
                AdministracionHonorariosServicios config = unitOfWork.Repository<AdministracionHonorariosServicios>().Table.Include(x => x.AdministracionHonorarios)
                    .FirstOrDefault(x => x.AdministracionHonorarios.EmpleadosId == item.EmpleadosId &&
                    x.ServiciosId == item.AdmisionesServiciosPrestados.ServiciosId);

                decimal valorLiquidar = 0;
                if (config != null)
                {
                    if (item.AdmisionesServiciosPrestados.Admisiones.ConveniosId != null)
                    {
                        if (config.TipoPagoConvenioEstadosId == 76)
                            valorLiquidar = item.AdmisionesServiciosPrestados.ValorTotal * (config.ValorHonorarioConvenio / 100);
                        else
                            valorLiquidar = config.ValorHonorarioConvenio * item.AdmisionesServiciosPrestados.Cantidad;
                    }
                    else
                    {
                        if (config.TipoPagoParticularEstadosId == 76)
                            valorLiquidar = item.AdmisionesServiciosPrestados.ValorTotal * (config.ValorHonorarioParticular / 100);
                        else
                            valorLiquidar = config.ValorHonorarioParticular * item.AdmisionesServiciosPrestados.Cantidad;
                    }
                }
                item.Valor = valorLiquidar;
                item.Id = idData;
                idData++;

                resultadoTotal.Add(item);
            }

            #endregion

            #region Lecturas

            var lecturas = unitOfWork.Repository<AtencionesResultado>().Table
                .Include(x => x.Empleado)
                .Include(x => x.AdmisionesServiciosPrestados)
                .Include(x => x.AdmisionesServiciosPrestados.Servicios)
                .Include(x => x.AdmisionesServiciosPrestados.Atenciones)
                .Include(x => x.AdmisionesServiciosPrestados.Atenciones.Empleados)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones.Pacientes)
                .Include(x => x.AdmisionesServiciosPrestados.Admisiones.ProgramacionCitas)
                .Where(x => x.FechaLectura.Date >= liquidacion.FechaInicio.Date &&
                x.FechaLectura.Date <= liquidacion.FechaFinal.Date &&
                x.Empleado.TipoEmpleados == 2 &&
                !unitOfWork.Repository<LiquidacionHonorariosDetalle>().Table.Include(j => j.LiquidacionHonorarios)
                    .Where(j => j.LiquidacionHonorarios.EstadosId != 10065)
                    .Select(j => j.AdmisionesServiciosPrestadosId).Contains(x.AdmisionesServiciosPrestadosId)
                );
            if (liquidacion.EmpleadosId != null)
                lecturas = lecturas.Where(x => x.EmpleadoId == liquidacion.EmpleadosId);
            List<AtencionesResultado> resultLecturas = lecturas.ToList();
            foreach (var lectura in resultLecturas)
            {
                LiquidacionHonorariosDetalle item = new LiquidacionHonorariosDetalle();
                item.LiquidacionHonorariosId = liquidacionId;
                item.EmpleadosId = lectura.EmpleadoId.GetValueOrDefault(0);
                item.Empleados = lectura.Empleado;
                item.AdmisionesServiciosPrestadosId = lectura.AdmisionesServiciosPrestadosId;
                item.AdmisionesServiciosPrestados = lectura.AdmisionesServiciosPrestados;
                item.EsLectura = true;
                item.AtencionesResultadoId = lectura.Id;
                item.AtencionesResultado = lectura;

                //Se calcula el valor a liquidar para el empleado de la lectura
                AdministracionHonorariosLecturas config = unitOfWork.Repository<AdministracionHonorariosLecturas>().Table.Include(x => x.AdministracionHonorarios)
                    .FirstOrDefault(x => x.AdministracionHonorarios.EmpleadosId == item.EmpleadosId &&
                    x.ServiciosId == item.AdmisionesServiciosPrestados.ServiciosId);

                decimal valorLiquidar = 0;
                if (config != null)
                {
                    if (config.TipoPagoLecturaEstadosId == 76)
                        valorLiquidar = item.AdmisionesServiciosPrestados.ValorServicio * (config.ValorHonorarioLectura / 100);
                    else
                        valorLiquidar = config.ValorHonorarioLectura;
                }
                item.Valor = valorLiquidar;
                item.Id = idData;
                idData++;

                resultadoTotal.Add(item);
            }

            #endregion

            return resultadoTotal;
        }

        public long Liquidar(List<LiquidacionHonorariosDetalle> models, string user)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            try
            {
                unitOfWork.BeginTransaction();
                models.ForEach(x =>
                {
                    x.Id = 0;
                    x.UpdatedBy = user;
                    x.CreatedBy = user;
                    x = unitOfWork.Repository<LiquidacionHonorariosDetalle>().Add(x);
                });
                var liquidacion = unitOfWork.Repository<LiquidacionHonorarios>().Table.FirstOrDefault(x => x.Id == models[0].LiquidacionHonorariosId);
                liquidacion.EstadosId = 10062;
                liquidacion.ValorTotal = models.Sum(x => x.Valor);
                unitOfWork.Repository<LiquidacionHonorarios>().Modify(liquidacion);
                unitOfWork.CommitTransaction();
                return liquidacion.Id;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public int LiquidacionConsecutivo()
        {
            try
            {
                BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
                return unitOfWork.Repository<LiquidacionHonorarios>().Table.Max(x => x.Consecutivo) + 1;
            }
            catch
            {
                return 1;
            }
        }

    }
}
