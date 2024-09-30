using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blazor.BusinessLogic.Models
{
    public class ArchivoDescargaModel
    {
        public string Nombre { get; set; }
        public string ContentType { get; set; }
        public byte[] Archivo { get; set; }
    }
}
