using Blazor.Infrastructure.Entities;
using System.IO;

namespace Blazor.WebApp.Models
{

    public partial class EmpleadosModel
    {
        public Empleados Entity { get; set; }
        public string FirmaPath { get; set; }
        public string SelloPath { get; set; }

        public EmpleadosModel()
        {
            Entity = new Empleados();
            //if(Entity.Personas == null)
            //    Entity.Personas = new Personas();
            Entity.FirmaDigitalArchivo = new Archivos();
            Entity.SelloDigitalArchivo = new Archivos();
        }

        public void SetFilesTemp()
        {
            if (!string.IsNullOrWhiteSpace(FirmaPath))
            {
                Entity.FirmaDigitalArchivo.Archivo = File.ReadAllBytes(FirmaPath);
            }
            if (!string.IsNullOrWhiteSpace(SelloPath))
            {
                Entity.SelloDigitalArchivo.Archivo = File.ReadAllBytes(SelloPath);
            }
        }

        public void DeleteTempFiles()
        {
            if (!string.IsNullOrWhiteSpace(FirmaPath))
            {
                File.Delete(FirmaPath);
                FirmaPath = null;
            }
            if (!string.IsNullOrWhiteSpace(SelloPath))
            {
                File.Delete(SelloPath);
                SelloPath = null;
            }
        }
    }

}
