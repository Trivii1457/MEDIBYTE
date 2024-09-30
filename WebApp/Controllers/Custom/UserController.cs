using Blazor.BusinessLogic;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Blazor.WebApp.Controllers
{
    public partial class UserController
    {
        public JsonResult ChangePassword(string CurrentPass, string NewPass, int UserId)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            User user = Manager().GetBusinessLogic<User>().FindById(x=>x.Id == UserId, false);
            if (user == null)
            {
                result.Add("ErrorLogic", "No existe usuario en este conexto, por favor vuelva a loguearse.");
                return Json(result);
            }

            string CurrentPassEncrypt = Cryptography.Encrypt(CurrentPass);
            if (user.Password != CurrentPassEncrypt)
            {
                result.Add("ErrorCurrentPass", "La contraseña actual no es la correcta.");
                return Json(result);
            }

            try
            {
                user.Password = CurrentPassEncrypt = Cryptography.Encrypt(NewPass);
                Manager().GetBusinessLogic<User>().Modify(user);
            }
            catch (Exception e)
            {
                result.Add("ErrorLogic", e.Message);
                return Json(result);
            }

            result.Add("success", "La contraseña se actualizo correctamente.");
            return Json(result);
        }
    }
}
