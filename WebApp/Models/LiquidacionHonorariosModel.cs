using Blazor.Infrastructure.Entities;
using System.Collections.Generic;

namespace Blazor.WebApp.Models
{

    public partial class LiquidacionHonorariosModel
    {
        public LiquidacionHonorarios Entity { get; set; }
        public List<LiquidacionHonorariosDetalle> LiquidacionHonorariosDetalle { get; set; }

        public LiquidacionHonorariosModel()
        {
            Entity = new LiquidacionHonorarios();
        }

    }

}
