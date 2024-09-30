using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Dominus.Backend.Application.Language;
using LanguageResource = Dominus.Backend.Application.Language.LanguageResource;

namespace Blazor.BusinessLogic
{
    public class GenericBusinessLogic<T> : IDomainLogic<T> where T : BaseEntity
    {
        public GenericBusinessLogic(IUnitOfWork unitWork)
        {
            UnitOfWork = unitWork;
            CommitTheTransaction = false;
        }

        public GenericBusinessLogic(DataBaseSetting configuracionBD)
        {
            UnitOfWork = new BlazorUnitWork(configuracionBD);
            CommitTheTransaction = true;
        }

        public int GetSecuence(string id)
        {
            int secuence = 0;
            List<DataBaseParameter> parameters = new List<DataBaseParameter>{
                new DataBaseParameter("@Id", id, Direcction.In),
                new DataBaseParameter("@Secuence", secuence, Direcction.Out)
            };
            _ = int.TryParse(ExecuteStoreProcedure("GetSecuence", parameters)["@Secuence"].ToString(), out secuence);
            return secuence;
        }

        #region Manejo de Imagenes a Tabla Archivos

        public long? ManageArchivo(Archivos archivo, long? idArchivoMaestro, GenericBusinessLogic<Archivos> archivoLogica)
        {
            if (archivo == null)
                return null;
            else if (string.IsNullOrWhiteSpace(archivo.Nombre) || string.IsNullOrWhiteSpace(archivo.TipoContenido) || string.IsNullOrWhiteSpace(archivo.Maestro))
                return null;

            var archivoBD = archivoLogica.FindById(x => x.Id == idArchivoMaestro, false);

            if (archivo.EliminarArchivo && idArchivoMaestro != null && idArchivoMaestro > 0)
            {
                archivoBD.Nombre = "delete";
                archivoBD.TipoContenido = "delete";
                archivoBD.Archivo = null;
                archivoBD.LastUpdate = DateTime.Now;
                archivoBD.UpdatedBy = archivo.UpdatedBy;
                archivoLogica.Modify(archivoBD);
                return null;
            }

            if (archivo.IsNew)
            {
                archivo.Archivo = DApp.Util.StringToArrayBytes(archivo.StringToBase64);
                archivo.LastUpdate = DateTime.Now;
                if (idArchivoMaestro == null || idArchivoMaestro == 0)
                {
                    archivo.CreationDate = DateTime.Now;
                    archivo = archivoLogica.Add(archivo);
                }
                else
                {
                    archivo.CreationDate = archivoBD.CreationDate;
                    archivo.CreatedBy = archivoBD.CreatedBy;
                    archivo.Id = idArchivoMaestro.GetValueOrDefault();
                    archivo = archivoLogica.Modify(archivo);
                }

                return archivo.Id;
            }
            else
            {
                return idArchivoMaestro;
            }
        }

        public void EliminarArchivoDeMaestro(long? idArchivoMaestro, BlazorUnitWork unitOfWork)
        {
            if (idArchivoMaestro != null && idArchivoMaestro > 0)
            {
                var archivoBD = unitOfWork.Repository<Archivos>().FindById(x => x.Id == idArchivoMaestro, false);
                unitOfWork.Repository<Archivos>().Remove(archivoBD);
            }
        }

        #endregion

    }
}
