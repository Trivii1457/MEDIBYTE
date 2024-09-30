using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Blazor.WebApp.Controllers
{

    public partial class EventosController : BaseAppController
    {
        public EventosController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

       

    }
}
