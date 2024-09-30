using Blazor.Infrastructure.Entities;
using Dominus.Backend.Data;
using Dominus.Backend.HttpClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;

namespace Blazor.Infrastructure.Models
{
    public partial class SingInModel
    {
        [Required(ErrorMessage = "La conexion es obligatoria.")]
        [DisplayName("Conexion")]
        public string ConnectionId { get; set; }

        ////[Required(ErrorMessage = "La conexion es obligatoria.")]
        ////[DisplayName("Conexion")]
        //public KeyValue Connection { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [DisplayName("Usuario")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DisplayName("Contraseña")]
        [StringLength(255)]
        public string Password { get; set; }

        [DisplayName("Empresa")]
        public string RazonSocialEmpresa { get; set; }

        public string Logo { get; set; }

        public List<string> errores = new List<string>();

        //[Required(ErrorMessage = "La Entidad es obligatoria.")]
        //[Range(1,long.MaxValue,ErrorMessage = "La Entidad es obligatoria.")]
        //[DisplayName("Empresa")]
        //public long EmpresaId { get; set; }

    }
}
