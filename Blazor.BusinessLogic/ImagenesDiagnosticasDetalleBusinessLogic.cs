using Blazor.BusinessLogic.Models;
using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using System;
using System.Collections.Generic;
using System.IO;

namespace Blazor.BusinessLogic
{
    public class ImagenesDiagnosticasDetalleBusinessLogic : GenericBusinessLogic<ImagenesDiagnosticasDetalle>
    {

        //private CloudStorageAccount CloudStorageAccount { get; set; }
        //private CloudBlobClient CloudBlobClient { get; set; }
        //private CloudBlobContainer CloudBlobContainer { get; set; }
        private string Contenedor { get; set; }
        private string PathImagenesDiagnosticasDisco { get; set; }

        public ImagenesDiagnosticasDetalleBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
            SetConfiguracionStorage();
        }

        public ImagenesDiagnosticasDetalleBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
            SetConfiguracionStorage();
        }

        private void SetConfiguracionStorage()
        {
            this.Contenedor = this.UnitOfWork.Settings.InitialCatalog;
            //this.CloudStorageAccount = CloudStorageAccount.Parse(DApp.CloudStorageAccount);
            //this.CloudBlobClient = this.CloudStorageAccount.CreateCloudBlobClient();
            //this.CloudBlobContainer = this.CloudBlobClient.GetContainerReference(Contenedor);
            this.PathImagenesDiagnosticasDisco = Path.Combine(Environment.CurrentDirectory, "ArchivosImagenesDiagnosticas");
        }

        public List<string> CargarImagenDiagnosticaDesdeAdmisionServicioPrestado(AzureFileModel azureFileModel, long admisionesServiciosPrestadosId)
        {
            return SubirImagenADiscoDesdeAdmisionServicioPrestado(azureFileModel, admisionesServiciosPrestadosId);
            //return SubirImagenStorageAzure(azureFileModel, imagenesDiagnosticasId);
        }

        public List<string> CargarImagenDiagnostica(AzureFileModel azureFileModel, long imagenesDiagnosticasId)
        {
            return SubirImagenADiscoDesdeImagenDiagnostica(azureFileModel, imagenesDiagnosticasId);
            //return SubirImagenStorageAzure(azureFileModel, imagenesDiagnosticasId);
        }

        private List<string> SubirImagenADiscoDesdeAdmisionServicioPrestado(AzureFileModel azureFileModel, long admisionesServiciosPrestadosId)
        {
            List<string> Errores = new List<string>();
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            try
            {
                var admisionesServiciosPrestado = unitOfWork.Repository<AdmisionesServiciosPrestados>().FindById(x => x.Id == admisionesServiciosPrestadosId, true);
                var paciente = unitOfWork.Repository<Pacientes>().FindById(x => x.Id == admisionesServiciosPrestado.Admisiones.PacientesId,false);
                var servicio = admisionesServiciosPrestado.Servicios;

                Errores = SubirImagenADisco(azureFileModel, paciente, servicio, admisionesServiciosPrestadosId, true);
            }
            catch (Exception e)
            {
                Errores.Add($"Error subiendo imagen a disco. | {e.Message}");
            }

            return Errores;
        }

        private List<string> SubirImagenADiscoDesdeImagenDiagnostica(AzureFileModel azureFileModel, long imagenesDiagnosticasId)
        {

            List<string> Errores = new List<string>();
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            try
            {
                ImagenesDiagnosticas imagenesDiagnosticas = unitOfWork.Repository<ImagenesDiagnosticas>().FindById(x => x.Id == imagenesDiagnosticasId, true);
                var paciente = imagenesDiagnosticas.Pacientes;
                var servicio = imagenesDiagnosticas.Servicios;

                Errores = SubirImagenADisco(azureFileModel, paciente, servicio, imagenesDiagnosticasId, false);
            }
            catch (Exception e)
            {
                Errores.Add($"Error subiendo imagen a disco. | {e.Message}");
            }

            return Errores;
        }


        private List<string> SubirImagenADisco(AzureFileModel azureFileModel, Pacientes paciente, Servicios servicio, long id, bool esDesdeAdmisionServicioPrestado)
        {
            List<string> Errores = new List<string>();
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();

            string contenedor = unitOfWork.Settings.InitialCatalog.ToLower();
            string nombreArchivoAzureBlobgen = "";
            ImagenesDiagnosticasDetalle imagenesDiagnosticasDetalle = new ImagenesDiagnosticasDetalle();

            try
            {
                if (paciente != null && servicio != null && id > 0 && azureFileModel.Archivo != null && azureFileModel.Archivo.Length > 0)
                {
                    nombreArchivoAzureBlobgen = $"{Guid.NewGuid()}.dcm";
                    imagenesDiagnosticasDetalle.CreatedBy = azureFileModel.UsuarioCreador;
                    imagenesDiagnosticasDetalle.UpdatedBy = azureFileModel.UsuarioCreador;
                    imagenesDiagnosticasDetalle.CreationDate = DateTime.Now;
                    imagenesDiagnosticasDetalle.LastUpdate = DateTime.Now;
                    imagenesDiagnosticasDetalle.Id = 0;
                    if(esDesdeAdmisionServicioPrestado)
                        imagenesDiagnosticasDetalle.AdmisionesServiciosPrestadosId = id;
                    else
                        imagenesDiagnosticasDetalle.ImagenesDiagnosticasId = id;
                    imagenesDiagnosticasDetalle.Nombre = $"{paciente.NumeroIdentificacion}_{paciente.NombreCompleto.Replace(" ", "_").ToUpper()}_{servicio.Nombre.Replace(" ", "_").ToUpper()}.dcm";
                    imagenesDiagnosticasDetalle.NombreArchivoAzureBlob = nombreArchivoAzureBlobgen;
                    imagenesDiagnosticasDetalle.Peso = azureFileModel.Peso;
                    imagenesDiagnosticasDetalle = unitOfWork.Repository<ImagenesDiagnosticasDetalle>().Add(imagenesDiagnosticasDetalle);

                    var path = Path.Combine(this.PathImagenesDiagnosticasDisco, contenedor);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    File.WriteAllBytes(Path.Combine(path, imagenesDiagnosticasDetalle.NombreArchivoAzureBlob), azureFileModel.Archivo);

                    imagenesDiagnosticasDetalle.SubioAStorage = true;
                    imagenesDiagnosticasDetalle = unitOfWork.Repository<ImagenesDiagnosticasDetalle>().Modify(imagenesDiagnosticasDetalle);
                }
                else
                {
                    Errores.Add("Los datos del archivo no son correctos.");
                }
            }
            catch (Exception e)
            {
                Errores.Add($"Error subiendo imagen a disco. | {e.Message}");
            }

            if (Errores.Count > 0)
                unitOfWork.RollbackTransaction();
            else
            {
                unitOfWork.CommitTransaction();
            }
            return Errores;
        }

        //private List<string> SubirImagenStorageAzureDesdeImagenesDiagnosticas(AzureFileModel azureFileModel, long imagenesDiagnosticasId)
        //{
        //    List<string> Errores = new List<string>();
        //    BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
        //    unitOfWork.BeginTransaction();

        //    string contenedor = unitOfWork.Settings.InitialCatalog.ToLower();
        //    ImagenesDiagnosticas imagenesDiagnosticas = unitOfWork.Repository<ImagenesDiagnosticas>().FindById(x => x.Id == imagenesDiagnosticasId, true);
        //    string nombreArchivoAzureBlobgen = "";
        //    ImagenesDiagnosticasDetalle imagenesDiagnosticasDetalle = new ImagenesDiagnosticasDetalle();

        //    try
        //    {
        //        if (imagenesDiagnosticasId > 0 && azureFileModel.Archivo != null && azureFileModel.Archivo.Length > 0)
        //        {
        //            if (CloudBlobContainer.Exists())
        //            {
        //                if (imagenesDiagnosticas != null)
        //                {
        //                    nombreArchivoAzureBlobgen = $"{Guid.NewGuid()}.dcm";
        //                    imagenesDiagnosticasDetalle.CreatedBy = azureFileModel.UsuarioCreador;
        //                    imagenesDiagnosticasDetalle.UpdatedBy = azureFileModel.UsuarioCreador;
        //                    imagenesDiagnosticasDetalle.CreationDate = DateTime.Now;
        //                    imagenesDiagnosticasDetalle.LastUpdate = DateTime.Now;
        //                    imagenesDiagnosticasDetalle.Id = 0;
        //                    imagenesDiagnosticasDetalle.ImagenesDiagnosticasId = imagenesDiagnosticasId;
        //                    imagenesDiagnosticasDetalle.Nombre = $"{imagenesDiagnosticas.Pacientes.NumeroIdentificacion}_{imagenesDiagnosticas.Pacientes.NombreCompleto.Replace(" ", "_").ToUpper()}_{imagenesDiagnosticas.Servicios.Nombre.Replace(" ", "_").ToUpper()}.dcm";
        //                    imagenesDiagnosticasDetalle.NombreArchivoAzureBlob = nombreArchivoAzureBlobgen;
        //                    imagenesDiagnosticasDetalle.Peso = azureFileModel.Peso;
        //                    imagenesDiagnosticasDetalle = unitOfWork.Repository<ImagenesDiagnosticasDetalle>().Add(imagenesDiagnosticasDetalle);
        //                }
        //                else
        //                {
        //                    Errores.Add($"No se envio el encabezado en dicha esta imagen a subir. Por favor vuelva a ingresar a la opcion desde el menu.");
        //                }
        //            }
        //            else
        //            {
        //                Errores.Add($"No se encontro un contenedor con el nombre \"{contenedor}\" en Azure. Comunicarse con cloudonesoft.");
        //            }
        //        }
        //        else
        //        {
        //            Errores.Add("Los datos del archivo no son correctos.");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Errores.Add($"Error conectando a la cuenta de alamacenamiento. Comunicarse con cloudonesoft. | {e.Message}");
        //    }

        //    if (Errores.Count > 0)
        //        unitOfWork.RollbackTransaction();
        //    else
        //    {
        //        unitOfWork.CommitTransaction();

        //        CloudBlockBlob cloudBlockBlob = CloudBlobContainer.GetBlockBlobReference(nombreArchivoAzureBlobgen);
        //        cloudBlockBlob.Properties.ContentType = azureFileModel.ContentType;
        //        cloudBlockBlob.Metadata["Paciente"] = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(imagenesDiagnosticas.Pacientes.NombreCompleto));
        //        cloudBlockBlob.Metadata["Servicio"] = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(imagenesDiagnosticas.Servicios.Nombre));
        //        cloudBlockBlob.Metadata["UsuarioCreo"] = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(azureFileModel.UsuarioCreador));

        //        this.SubirImagenDiagnostica(azureFileModel.Archivo, cloudBlockBlob, imagenesDiagnosticasDetalle);
        //    }
        //    return Errores;
        //}

        //private async void SubirImagenDiagnostica(byte[] archivo, CloudBlockBlob cloudBlockBlob, ImagenesDiagnosticasDetalle imagenesDiagnosticasDetalle)
        //{
        //    BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
        //    unitOfWork.BeginTransaction();
        //    try
        //    {
        //        await cloudBlockBlob.UploadFromByteArrayAsync(archivo, 0, archivo.Length);
        //        imagenesDiagnosticasDetalle.SubioAStorage = true;
        //        unitOfWork.Repository<ImagenesDiagnosticasDetalle>().Modify(imagenesDiagnosticasDetalle);
        //    }
        //    catch (Exception e)
        //    {
        //        imagenesDiagnosticasDetalle.SubioAStorage = false;
        //        imagenesDiagnosticasDetalle.ErrorNoSubio = e.Message;
        //        if (e.InnerException != null)
        //            imagenesDiagnosticasDetalle.ErrorNoSubio += " | InnerException: " + e.InnerException.Message;
        //        unitOfWork.Repository<ImagenesDiagnosticasDetalle>().Modify(imagenesDiagnosticasDetalle);
        //    }
        //    unitOfWork.CommitTransaction();

        //}


        public AzureFileModel DescargarImagenDiagnostica(long id)
        {
            return DescargarImagenDiagnosticaDisco(id);
            //return DescargarImagenDiagnosticaAzure(id);
        }

        private AzureFileModel DescargarImagenDiagnosticaDisco(long id)
        {
            try
            {
                BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
                string contenedor = unitOfWork.Settings.InitialCatalog.ToLower();
                AzureFileModel azureFileModel = new AzureFileModel();
                ImagenesDiagnosticasDetalle imagenesDiagnosticasDetalle = unitOfWork.Repository<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == id, false);
                if (imagenesDiagnosticasDetalle != null)
                {
                    var path = Path.Combine(this.PathImagenesDiagnosticasDisco, contenedor, imagenesDiagnosticasDetalle.NombreArchivoAzureBlob);
                    if (File.Exists(path))
                    {
                        azureFileModel.Nombre = imagenesDiagnosticasDetalle.Nombre;
                        azureFileModel.ContentType = "application/octet-stream";
                        azureFileModel.Archivo = File.ReadAllBytes(path);
                        azureFileModel.Peso = azureFileModel.Archivo.Length;
                    }
                }
                return azureFileModel;
            }
            catch (Exception)
            {
                return new AzureFileModel();
            }
        }

        //private AzureFileModel DescargarImagenDiagnosticaAzure(long id)
        //{
        //    BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
        //    AzureFileModel azureFileModel = new AzureFileModel();
        //    ImagenesDiagnosticasDetalle imagenesDiagnosticasDetalle = unitOfWork.Repository<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == id, false);
        //    CloudBlockBlob cloudBlockBlob = CloudBlobContainer.GetBlockBlobReference(imagenesDiagnosticasDetalle.NombreArchivoAzureBlob);
        //    if (cloudBlockBlob.Exists())
        //    {
        //        azureFileModel.Archivo = new byte[cloudBlockBlob.Properties.Length];
        //        cloudBlockBlob.DownloadToByteArray(azureFileModel.Archivo, 0);
        //        azureFileModel.Nombre = imagenesDiagnosticasDetalle.Nombre;
        //        azureFileModel.ContentType = cloudBlockBlob.Properties.ContentType;
        //        azureFileModel.Peso = cloudBlockBlob.Properties.Length;
        //    }
        //    return azureFileModel;
        //}

        public void EliminarImagenDiagnostica(long id)
        {
            EliminarImagenDiagnosticaDisco(id);
            //return DescargarImagenDiagnosticaAzure(id);
        }

        private void EliminarImagenDiagnosticaDisco(long id)
        {
            try
            {
                BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
                string contenedor = unitOfWork.Settings.InitialCatalog.ToLower();
                ImagenesDiagnosticasDetalle imagenesDiagnosticasDetalle = unitOfWork.Repository<ImagenesDiagnosticasDetalle>().FindById(x => x.Id == id, false);
                if (imagenesDiagnosticasDetalle != null)
                {
                    var path = Path.Combine(this.PathImagenesDiagnosticasDisco, contenedor, imagenesDiagnosticasDetalle.NombreArchivoAzureBlob);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        unitOfWork.Repository<ImagenesDiagnosticasDetalle>().Remove(imagenesDiagnosticasDetalle);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw (e);
            }
        }

    }
}
