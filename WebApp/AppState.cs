using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using WidgetGallery;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Blazor.BusinessLogic;

namespace Blazor.WebApp
{

    [Authorize]
    public class AppState : BaseAppController
    {
        public AppState(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
            SwitchApp = Convert.ToInt16(config["SwitchApp"]);
            SetUser(this.ActualUsuarioId());
            SetEmpresa(this.ActualEmpresaId());
        }

        public short SwitchApp { get; set; }

        #region Empresa

        public Empresas Empresa { get; set; }

        private void SetEmpresa(long id)
        {
            if (id > 0)
            {
                Empresa = Manager().GetBusinessLogic<Empresas>().FindById(x => x.Id == id, true);
            }
            if (Empresa == null)
            {
                Empresa = new Empresas();
                Empresa.RazonSocial = (Empresa.RazonSocial ?? "Empresa sin Razon Social");
            }

        }
        #endregion

        #region Usuario

        public User Usuario { get; set; }

        private void SetUser(long id)
        {
            if (id > 0)
                Usuario = Manager().GetBusinessLogic<User>().FindById(x=> x.Id ==  id, true);

            if (Usuario == null)
            {
                Usuario = new User();
                Usuario.Names = (Usuario.Names ?? "Usuario sin nombre");
                Usuario.LastNames = (Usuario.LastNames ?? "Usuario sin apellidos");
                Usuario.Email = (Usuario.Email ?? "Usuario sin email");
            }
        }

        #endregion

        public List<Dominus.Backend.Application.MenuModel> GetMenu()
        {
            Manager().UserBusinessLogic().UpdateSecurityNavigation(null, 0, httpContextAccessor.HttpContext.Request.Host.Value);

            var pathMenu = System.IO.Path.Combine("Utils","menu.json");
            List<Dominus.Backend.Application.MenuModel> menu = Dominus.Backend.Application.Menu.GetMenu(pathMenu);
            menu.ForEach(x => {
                x.Options.ForEach(j =>
                {
                    j.Havepermission = !DApp.ActionViewSecurity(httpContextAccessor.HttpContext, "/" + j.Name + "/List");
                    j.Resource = @DApp.DefaultLanguage.GetResource(j.Resource);
                });
                x.Module = @DApp.DefaultLanguage.GetResource(x.Module);
            });


            return menu;
        }

    }


}
