using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class AtencionesResultadoBusinessLogic : GenericBusinessLogic<Atenciones>
    {
        public AtencionesResultadoBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public AtencionesResultadoBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public AtencionesResultado SubirAudioAResultado(byte[] mp3Bytes, long admisionesServiciosPrestadosId, long id, string user, long userId)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                AtencionesResultado atencionesResultado = new AtencionesResultado();
                atencionesResultado.IsNew = true;
                atencionesResultado.CreatedBy = user;
                atencionesResultado.CreationDate = DateTime.Now;
                if (id != 0)
                {
                    atencionesResultado = unitOfWork.Repository<AtencionesResultado>().FindById(x => x.Id == id, false);
                    atencionesResultado.IsNew = false;
                }
                atencionesResultado.UpdatedBy = user;
                atencionesResultado.LastUpdate = DateTime.Now;
                atencionesResultado.EstadosId = 66;
                atencionesResultado.ResultadoAudio = mp3Bytes;
                atencionesResultado.AdmisionesServiciosPrestadosId = admisionesServiciosPrestadosId;
                atencionesResultado.FechaLectura = DateTime.Now;
                var empleado = unitOfWork.Repository<Empleados>().FindById(x => x.UserId == userId, false);
                if (empleado != null)
                {
                    atencionesResultado.EmpleadoId = empleado.Id;
                }
                else
                {
                     throw new Exception("El usuario actual no tiene un empleado asociado.");
                }


                if (atencionesResultado.IsNew)
                {
                    atencionesResultado = unitOfWork.Repository<AtencionesResultado>().Add(atencionesResultado);
                }
                else
                {
                    atencionesResultado = unitOfWork.Repository<AtencionesResultado>().Modify(atencionesResultado);
                }
                atencionesResultado.IsNew = false;

                AdmisionesServiciosPrestados admisionesServiciosPrestados = unitOfWork.Repository<AdmisionesServiciosPrestados>().FindById(x => x.Id == admisionesServiciosPrestadosId, false);
                admisionesServiciosPrestados.LecturaRealizada = true;
                unitOfWork.Repository<AdmisionesServiciosPrestados>().Modify(admisionesServiciosPrestados);
                unitOfWork.CommitTransaction();

                return atencionesResultado;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw(e);
            }

        }

    }
}
