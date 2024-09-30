using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Blazor.WebApp.Models;
using DevExpress.DataProcessing;
using Dominus.Backend.Application;
using WidgetGallery;
using Dominus.Backend.Security;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.WebApp.Controllers
{

    public partial class ProfileNavigationController
    {

        [HttpGet]
        [Route("GetNavigationProfile")]
        public List<ProfileMethodModel> GetNavigationProfile(int ProfileId)
        {
            var menuJson = new AppState(config,httpContextAccessor).GetMenu();
            List<ProfileMethodModel> data = (
                from ProfileNavigation in Manager().GetBusinessLogic<ProfileNavigation>().FindAll(x => x.ProfileId == ProfileId)
                join Masters in Manager().GetBusinessLogic<Infrastructure.Entities.Menu>().FindAll(null)
                    on ProfileNavigation.MenuId equals Masters.Id
                join Methods in Manager().GetBusinessLogic<MenuAction>().FindAll(null)
                    on ProfileNavigation.MenuActionId equals Methods.Id
                select new ProfileMethodModel
                {
                    ProfileNavigationId = ProfileNavigation.Id,
                    ProfileId = ProfileNavigation.ProfileId,
                    //Module = DApp.DefaultLanguage.GetResource(Masters.Module),
                    Module = DApp.DefaultLanguage.GetResource(menuJson.FirstOrDefault(x => x.Options.FirstOrDefault(j => j.Name == Masters.Name)?.Name == Masters.Name)?.Module),
                    ResourceKeyMaster = DApp.DefaultLanguage.GetResource(Masters.ResourceKey),
                    ResourceKeyMethod = DApp.DefaultLanguage.GetResource(Methods.ResourceKey),
                    MenuActionId = ProfileNavigation.MenuActionId
                }
            ).ToList();

            return data;
            //return Manager().GetBusinessLogic<ProfileNavigation>().FindAll(loadOptions).ToLoadResult();
        }

        [HttpGet]
        [Route("NewProfileNavigation")]
        public IActionResult NewProfileNavigation(long IdFather)
        {
            ProfileNavigationModel model = new ProfileNavigationModel();
            model.Entity.IsNew = true;
            model.Entity.ProfileId = IdFather;
            model.Entity.Profile = Manager().GetBusinessLogic<Profile>().FindById(x=>x.Id== IdFather,false);
            return PartialView("EditProfileNavigation", model);
        }

        [HttpGet]
        [Route("GetNavigationMaster")]
        public List<ProfileMethodModel> GetNavigationMaster(int ProfileId)
        {
            List<MenuModel> menuJson = new AppState(config, httpContextAccessor).GetMenu();
            List<Option> opcionesMenuJson = new List<Option>();
            foreach (var item in menuJson)
            {
                opcionesMenuJson.AddRange(item.Options);
            }

            List<ProfileNavigation> ProfileNavigation = Manager().GetBusinessLogic<ProfileNavigation>().FindAll(x => x.ProfileId == ProfileId);
            var data = (
                from Methods in Manager().GetBusinessLogic<MenuAction>().FindAll(null)
                from Masters in Manager().GetBusinessLogic<Infrastructure.Entities.Menu>().FindAll(null)
                    join opcionesJson in opcionesMenuJson on Masters.Name equals opcionesJson.Name
                where Methods.MenuId == Masters.Id || Methods.MenuId == 0
                select new ProfileMethodModel
                {
                    ProfileId = ProfileId,
                    Module = DApp.DefaultLanguage.GetResource(menuJson.FirstOrDefault(x => x.Options.FirstOrDefault(j => j.Name == Masters.Name)?.Name == Masters.Name)?.Module),
                    ResourceKeyMaster = DApp.DefaultLanguage.GetResource(Masters.ResourceKey),
                    ResourceKeyMethod = DApp.DefaultLanguage.GetResource(Methods.ResourceKey),
                    MenuId = Masters.Id,
                    MenuName = Masters.Name,
                    MenuActionId = Methods.Id,
                    MenuActionName = Methods.ActionName,
                    HavePermision = (ProfileNavigation.Exists(x => x.MenuId == Masters.Id && x.MenuActionId == Methods.Id))
                }

            ).ToList();

            return data;
            //return Manager().GetBusinessLogic<ProfileNavigation>().FindAll(loadOptions).ToLoadResult();
        }

        [HttpPost]
        [Route("SetNavigationProfile")]
        public IActionResult SetNavigationProfile(string key, string values)
        {
            try
            {
                ProfileMethodModel ProfileMethodModel = new ProfileMethodModel();
                JsonConvert.PopulateObject(values, ProfileMethodModel);
                JsonConvert.PopulateObject(key, ProfileMethodModel);

                List<ProfileNavigation> ProfileNavigationList = Manager().GetBusinessLogic<ProfileNavigation>().FindAll(
                    x => x.ProfileId == ProfileMethodModel.ProfileId && x.MenuId == ProfileMethodModel.MenuId && x.MenuActionId == ProfileMethodModel.MenuActionId
                );

                if (ProfileNavigationList.Count == 0 && ProfileMethodModel.HavePermision)
                {
                    ProfileNavigation profileNavigation = new ProfileNavigation();
                    profileNavigation.ProfileId = ProfileMethodModel.ProfileId;
                    profileNavigation.Path = "/" + ProfileMethodModel.MenuName + "/" + ProfileMethodModel.MenuActionName;
                    profileNavigation.MenuId = ProfileMethodModel.MenuId;
                    profileNavigation.MenuActionId = ProfileMethodModel.MenuActionId;
                    profileNavigation.UpdatedBy = User.Identity.Name;
                    profileNavigation.LastUpdate = DateTime.Now;
                    profileNavigation.CreatedBy = User.Identity.Name;
                    profileNavigation.CreationDate = DateTime.Now;
                    profileNavigation = Manager().GetBusinessLogic<ProfileNavigation>().Add(profileNavigation);
                    //if (ProfileMethodModel.MenuActionName.Equals("List")) {
                    //    profileNavigation.Path = "/" + ProfileMethodModel.MenuName + "/Get";
                    //    profileNavigation = Manager().GetBusinessLogic<ProfileNavigation>().Add(profileNavigation);
                    //}
                }
                else if (ProfileNavigationList.Count == 1 && !ProfileMethodModel.HavePermision)
                {
                    ProfileNavigation profileNavigation = ProfileNavigationList[0];
                    profileNavigation = Manager().GetBusinessLogic<ProfileNavigation>().Remove(profileNavigation);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);
                return BadRequest();
            }
            return Ok();
        }
    }
}
