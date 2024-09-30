using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Serialize.Linq.Extensions;
using Dominus.Backend.Data;
using Dominus.Backend.DataBase;

namespace Blazor.Infrastructure.Entities
{
    public partial class Medicamentos
    {
        [NotMapped]
        public string DisplayExpre
        {
            private set { }
            get
            {
                return $"Nombre: {Nombre} - Concentración: {Concentraciones?.Nombre} - Forma farmacéutica: {FormasFarmaceuticas?.Nombre} - Vía de administración: {ViaAdministracion?.Nombre}";
            }
        }
    }
}
