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
using Newtonsoft.Json;
using System;

namespace Blazor.WebApp.Controllers
{

    [Authorize]
    public partial class ProfileUserController : BaseAppController
    {

        //private const string Prefix = "ProfileUser"; 

        public ProfileUserController(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        #region Functions Master

        [HttpPost]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<ProfileUser>().Tabla(true), loadOptions);
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

        private ProfileUserModel NewModel()
        {
            ProfileUserModel model = new ProfileUserModel();
            model.Entity.IsNew = true;
            return model;
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            return PartialView("Edit", EditModel(Id));
        }

        private ProfileUserModel EditModel(long Id)
        {
            ProfileUserModel model = new ProfileUserModel();
            model.Entity = Manager().GetBusinessLogic<ProfileUser>().FindById(x=>x.Id== Id,false);
            model.Entity.IsNew = false;
            return model;
        }

        [HttpPost]
        public IActionResult Edit(ProfileUserModel model)
        {
            return PartialView("Edit", EditModel(model));
        }

        private ProfileUserModel EditModel(ProfileUserModel model)
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
                        model.Entity = Manager().GetBusinessLogic<ProfileUser>().Add(model.Entity);
                        model.Entity.IsNew = false;
                    }
                    else
                    {
                        model.Entity = Manager().GetBusinessLogic<ProfileUser>().Modify(model.Entity);
                    }
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

        [HttpPost]
        public IActionResult Delete(ProfileUserModel model)
        {
            return PartialView("Edit", DeleteModel(model));
        }

        private ProfileUserModel DeleteModel(ProfileUserModel model)
        {
            ViewBag.Accion = "Delete";
            ProfileUserModel newModel = NewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Entity = Manager().GetBusinessLogic<ProfileUser>().FindById(x=>x.Id== model.Entity.Id,false);
                    Manager().GetBusinessLogic<ProfileUser>().Remove(model.Entity);
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

        private ProfileUserModel NewModelDetail(long IdFather) 
        { 
            ProfileUserModel model = new ProfileUserModel(); 
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
        public IActionResult EditDetail(ProfileUserModel model)
        {
            return PartialView("EditDetail",EditModel(model));
        }

        [HttpPost]
        public IActionResult DeleteDetail(ProfileUserModel model)
        {
            return PartialView("EditDetail", DeleteModelDetail(model));
        }

        private ProfileUserModel DeleteModelDetail(ProfileUserModel model)
        { 
            ViewBag.Accion = "Delete"; 
            ProfileUserModel newModel = NewModelDetail(model.Entity.IdFather); 
            if (ModelState.IsValid) 
            { 
                try 
                { 
                    model.Entity = Manager().GetBusinessLogic<ProfileUser>().FindById(x => x.Id == model.Entity.Id, false); 
                    Manager().GetBusinessLogic<ProfileUser>().Remove(model.Entity); 
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

        #region Funcions Detail Edit in Grid

        [HttpPost]
        public IActionResult AddInGrid(string values)
        {
            ProfileUser entity = new ProfileUser();
            JsonConvert.PopulateObject(values, entity);
            //Manager().GetBusinessLogic<ProfileUser>().Add(model);
            ProfileUserModel model = new ProfileUserModel();
            model.Entity = entity;
            model.Entity.IsNew = true;
            this.EditModel(model);
            return Ok(ModelState);
        }

        [HttpPost]
        public IActionResult ModifyInGrid(int key, string values)
        {
            ProfileUser entity = Manager().GetBusinessLogic<ProfileUser>().FindById(x=>x.Id== key,false);
            JsonConvert.PopulateObject(values, entity);
            //Manager().GetBusinessLogic<ProfileUser>().Modify(model);
            ProfileUserModel model = new ProfileUserModel();
            model.Entity = entity;
            model.Entity.IsNew = false;
            this.EditModel(model);
            return Ok(ModelState);
        }

        [HttpPost]
        public void DeleteInGrid(int key)
        {
            ProfileUser entity = Manager().GetBusinessLogic<ProfileUser>().FindById(x=>x.Id==key,false);
            Manager().GetBusinessLogic<ProfileUser>().Remove(entity);
        }

        #endregion

        #region Datasource Combobox Foraneos 

        [HttpPost]
        public LoadResult GetProfileId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<Profile>().Tabla(true), loadOptions);
        }
        [HttpPost]
        public LoadResult GetUserId(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(Manager().GetBusinessLogic<User>().Tabla(true), loadOptions);
        }

        #endregion

    }
}
