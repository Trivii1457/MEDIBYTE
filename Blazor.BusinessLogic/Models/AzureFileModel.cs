using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blazor.BusinessLogic.Models
{
    public class AzureFileModel
    {
        public string Nombre { get; set; }
        public string ContentType { get; set; }
        public long Peso { get; set; }
        public string UsuarioCreador { get; set; }
        public byte[] Archivo { get; set; }
    }
}
