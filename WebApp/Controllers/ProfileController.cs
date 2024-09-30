using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using WidgetGallery;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Frontend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class ProfileController : BaseAppController
    {

        //private const string Prefix = "Profile"; 

        public ProfileController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Profile>().Tabla(true).Where(x=>x.Id != 1), loadOptions);
        }

        public IActionResult List()
        {
            return View("List");
        }

        public IActionResult ListPartial()
        {
            return PartialView("List");
        }

        [HttpGet]
        public IActionResult New()
        {
            return PartialView("Edit", NewModel());
        }

        private ProfileModel NewModel()
        {
            ProfileModel model = new ProfileModel();
            model.Entity.IsNew = true;
            model.Entity.EmpresasId = this.ActualEmpresaId();
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ProfileModel EditModel(long Id)
        {
            ProfileModel model = new ProfileModel();
            model.Entity = Manager().GetBusinessLogic<Profile>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(ProfileModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private ProfileModel EditModel(ProfileModel model)
        {
            ViewBag.Accion = "Save";
            var OnState = model.Entity.IsNew;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity.LastUpdate = DateTime.Now;
                    model.Entity.UpdatedBy = User.Identity.Name;
                    if (model.Entity.IsNew)
                    {
                        model.Entity.CreationDate = DateTime.Now;
                        model.Entity.CreatedBy = User.Identity.Name;
                        model.Entity = Manager().GetBusinessLogic<Profile>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().GetBusinessLogic<Profile>().Modify(model.Entity);
                    }
                    GetCurrentSecurityNavigation(model.Entity.Id);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
                }
            }
            else
            {
                ModelState.AddModelError("Entity.Id", $"Error en vista, diferencia con base de datos. | " + ModelState.GetFullErrorMessage());
            }
            return model;
        }

        [HttpPost]
        public IActionResult Delete(ProfileModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ProfileModel DeleteModel(ProfileModel model)
        {
            ViewBag.Accion = "Delete";
            ProfileModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<Profile>().FindById(x => x.Id == model.Entity.Id, false);
                    Manager().GetBusinessLogic<Profile>().Remove(model.Entity);
                    return newModel;
                }
                catch (Exception e)
                {
                    while (e != null)
                    {
                        ModelState.AddModelError("Error: ", e.Message);
                        e = e.InnerException;
                    }
                }
            }
            return model;
        }

        #endregion

        #region Functions Detail 

        /*
        [HttpGet]
        public IActionResult NewDetail(long IdFather)
        {
            return PartialView("EditDetail", NewModelDetail(IdFather));
        }

        private ProfileModel NewModelDetail(long IdFather) 
        { 
            ProfileModel model = new ProfileModel(); 
            model.Entity.IdFather = IdFather; 
            model.Entity.IsNew = true; 
            return model; 
        } 

        [HttpGet]
        public IActionResult EditDetail(long Id)
        {
            return PartialView("EditDetail", EditModel(Id));
        }

        [HttpPost]
        public IActionResult EditDetail(ProfileModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ProfileModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ProfileModel DeleteModelDetail(ProfileModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ProfileModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<Profile>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<Profile>().Remove(model.Entity); 
                    return newModel;
                } 
                catch (Exception e) 
                { 
                    while (e!=null) 
                    { 
                        ModelState.AddModelError("Error: ", e.Message); 
                        e = e.InnerException; 
                    } 
                } 
            } 
            return model; 
        } 
        */

        #endregion


    }
}
