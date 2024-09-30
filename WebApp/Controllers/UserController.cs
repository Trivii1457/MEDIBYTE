using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExtreme.AspNet.Data;
using WidgetGallery;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Dominus.Backend.Security;
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
    public partial class UserController : BaseAppController
    {

        //private const string Prefix = "User"; 

        public UserController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return  DataSourceLoader.Load( Manager().GetBusinessLogic<User>().Tabla(true).Where(x=>x.Id != 1), loadOptions);
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

        private UserModel NewModel()
        {
            UserModel model = new UserModel();
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private UserModel EditModel(long Id)
        {
            UserModel model = new UserModel();
            model.Entity = Manager().GetBusinessLogic<User>().FindById(x => x.Id == Id, false);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(UserModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private UserModel EditModel(UserModel model)
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
                        model.Entity.Password = Cryptography.Encrypt(model.Entity.Password);
                        model.Entity = Manager().GetBusinessLogic<User>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        if (model.ModifyPassword)
                            model.Entity.Password = Cryptography.Encrypt(model.Entity.Password);
                        model.Entity = Manager().GetBusinessLogic<User>().Modify(model.Entity);
                    }
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
            model.ModifyPassword = false;
            return model;
        }

        [HttpPost]
        public IActionResult Delete(UserModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private UserModel DeleteModel(UserModel model)
        {
            ViewBag.Accion = "Delete";
            UserModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<User>().FindById(x=>x.Id== model.Entity.Id, false);
                    Manager().GetBusinessLogic<User>().Remove(model.Entity);
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

        private UserModel NewModelDetail(long IdFather) 
        { 
            UserModel model = new UserModel(); 
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
        public IActionResult EditDetail(UserModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(UserModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private UserModel DeleteModelDetail(UserModel model)
        { 
            ViewBag.Accion = "Delete"; 
            UserModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<User>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<User>().Remove(model.Entity); 
                    return newModel;
                } 
                catch (Exception e) 
                { 
                    ModelState.AddModelError("Entity.Id", e.GetFullErrorMessage());
                } 
            } 
            return model; 
        } 
        */

        #endregion

        #region Datasource Combobox Foraneos 

        [HttpGet]
        public LoadResult GetGenders(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetGenders(), loadOptions);
        }

        [HttpGet]
        public LoadResult GetIdentificationTypes(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetIdentificationTypes(), loadOptions);
        }

        #endregion

    }
}
