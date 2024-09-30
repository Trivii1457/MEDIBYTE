using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.DataBase;
using System;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class AdmisionesServiciosPrestadosBusinessLogic : GenericBusinessLogic<AdmisionesServiciosPrestados>
    {
        public AdmisionesServiciosPrestadosBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public AdmisionesServiciosPrestadosBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public override AdmisionesServiciosPrestados Add(AdmisionesServiciosPrestados data)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            try
            {
                unitOfWork.BeginTransaction();
                data = unitOfWork.Repository<AdmisionesServiciosPrestados>().Add(data);
                ActualizarValoresAdmision(unitOfWork, data, data.EsCorrecion);
                unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw (e);
            }

            return data;
        }

        //public override AdmisionesServiciosPrestados Modify(AdmisionesServiciosPrestados data)
        //{
        //    BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
        //    try
        //    {
        //        unitOfWork.BeginTransaction();
        //        data = unitOfWork.Repository<AdmisionesServiciosPrestados>().Modify(data);
        //        ActualizarValoresAdmision(unitOfWork, data);
        //        unitOfWork.CommitTransaction();
        //    }
        //    catch (Exception e)
        //    {
        //        unitOfWork.RollbackTransaction();
        //        throw (e);
        //    }

        //    return data;
        //}

        public override AdmisionesServiciosPrestados Remove(AdmisionesServiciosPrestados data)
        {
            var logicaData = new GenericBusinessLogic<AdmisionesServiciosPrestados>(this.UnitOfWork.Settings);
            try
            {
                logicaData.BeginTransaction();
                data = logicaData.Remove(data);
                ActualizarValoresAdmision(logicaData.UnitOfWork, data, data.EsCorrecion);
                logicaData.CommitTransaction();
            }
            catch (Exception e)
            {
                logicaData.RollbackTransaction();
                throw (e);
            }

            return data;
        }

        private void ActualizarValoresAdmision(IUnitOfWork unitOfWork, AdmisionesServiciosPrestados data, bool esCorrecion)
        {
            // Se calcula el total final en la admision
            var admision = unitOfWork.Repository<Admisiones>().FindById(x => x.Id == data.AdmisionesId, true);
            var totalvalorServicios = unitOfWork.Repository<AdmisionesServiciosPrestados>().Table.Where(x => x.AdmisionesId == data.AdmisionesId).Sum(x => x.Cantidad * x.ValorServicio);
            if (admision.PorcDescAutorizado > 0)
            {
                totalvalorServicios = totalvalorServicios - (totalvalorServicios * (admision.PorcDescAutorizado / 100));
            }
            admision.ValorPagarParticular = totalvalorServicios;

            if (!esCorrecion) // Si  es correcion no se actualiza el copago
            {

                if (admision.ValorPagoEstadosId == 10067) // Particular
                {
                    admision.ValorCopago = 0;
                }

                if (admision.ValorPagoEstadosId == 58) // Copago
                {
                    // Se calcula el copago por todos los servicios
                    var totalServicios = unitOfWork.Repository<AdmisionesServiciosPrestados>().Table.Count(x => x.AdmisionesId == admision.Id);
                    if (totalServicios <= 0)
                    {
                        admision.ValorCopago = 0;
                    }
                    else
                    {
                        var categoriasIngresosDetalles = unitOfWork.Repository<CategoriasIngresosDetalles>().FindById(x => x.CategoriasIngresosId == admision.Pacientes.CategoriasIngresosId && x.FechaInicial <= DateTime.Now && x.FechaFinal >= DateTime.Now, false);
                        if (categoriasIngresosDetalles == null)
                        {
                            throw new Exception("Usuario sin régimen y rango asociados.");
                        }

                        var valorServicio = admision.ValorPagarParticular * (categoriasIngresosDetalles.PorcentajeCopago / 100);
                        if (valorServicio >= categoriasIngresosDetalles.CopagoMaximoEvento)
                            valorServicio = categoriasIngresosDetalles.CopagoMaximoEvento;

                        valorServicio = Math.Round(valorServicio / 50, 0) * 50;

                        admision.ValorCopago = valorServicio;
                    }
                }
            }
            unitOfWork.Repository<Admisiones>().Modify(admision);
        }

    }

}

