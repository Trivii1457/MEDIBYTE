using Blazor.BusinessLogic;
using Dominus.Backend.Application;
using WidgetGallery;
using Dominus.Backend.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class ProfileController
    {
        [HttpGet]
        [Route("GetCurrentSecurityNavigation")]
        public List<SecurityNavigation> GetCurrentSecurityNavigation(long ProfileId)
        {
            Manager().UserBusinessLogic().UpdateSecurityNavigation(null, ProfileId, Request.Host.Value);
            return Manager().UserBusinessLogic().GetCurrentSecurityNavigation();
        }
    }
}
