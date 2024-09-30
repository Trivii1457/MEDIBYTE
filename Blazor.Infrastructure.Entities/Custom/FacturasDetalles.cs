using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class FacturasDetalles
    {
        [NotMapped]
        public virtual Decimal SubTotalValue
        {
            get
            {
                return Math.Round(ValorServicio * Cantidad, 0);
            }
        }

        [NotMapped]
        public virtual Decimal DiscountValue
        {
            get
            {
                return Math.Round((ValorServicio * Cantidad) * PorcDescuento / 100, 0);
            }
        }

        [NotMapped]
        public virtual Decimal TaxValue
        {
            get
            {
                return Math.Round((SubTotalValue - DiscountValue) * PorcImpuesto / 100, 0);
            }
        }

        [NotMapped]
        public virtual Decimal TotalValue
        {
            get
            {
                return Math.Round(SubTotalValue - DiscountValue + TaxValue, 0);
            }
        }

    }
 }
