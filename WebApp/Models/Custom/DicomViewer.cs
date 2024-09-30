using Blazor.Infrastructure.Entities;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraPrinting.Drawing;
using Dominus.Backend.DataBase;
using System;
using System.Drawing;
using System.IO;

namespace Blazor.WebApp.Models
{
    public class DicomViewerModel
    {
        public bool EsDesdeDisco { get; set; } = false;
        public bool TieneImagen { get; set; } = false;
        public string NombreArchivoAzureBlob { get; set; }
        public string Contenedor { get; set; }
        private string Host { get; set; } = "https://imagenesdiagnosticas.blob.core.windows.net";
        public string EnlaceAzure
        {
            private set { }
            get
            {
                return $"{Host}/{Contenedor}/{NombreArchivoAzureBlob}";
            }
        }

        public string EnlaceDisco
        {
            private set { }
            get
            {
                var path = $"/ArchivosImagenesDiagnosticas/{Contenedor}/{NombreArchivoAzureBlob}";
                return path;
            }
        }
    }
}
