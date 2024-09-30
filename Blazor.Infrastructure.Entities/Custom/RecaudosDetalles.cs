using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Dominus.Backend.Data;
using Dominus.Backend.DataBase;

namespace Blazor.Infrastructure.Entities
{
    public partial class RecaudosDetalles
    {
        [NotMapped]
        public decimal Total
        {
            get
            {
                return ValorAplicado + ValorRetencion+ValorReteIca;
            }
        }
    }
 }
