using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class UserBusinessLogic : GenericBusinessLogic<User>
    {
        public UserBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public UserBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }


        public User SingIn(string userName, string password, string host)
        {
            var passCrypt = Cryptography.Encrypt(password);
            User data = FindById(x => x.UserName == userName && x.Password == Cryptography.Encrypt(password), false);
            if (data != null)
            {
                UpdateSecurityNavigation(data, 0 , host);
            }
            return data;
        }

        public List<Empresas> GetEmpresasByUser(string UserName)
        {
            User user = this.FindById(x => x.UserName == UserName, false);
            List<Empresas> resultEmpresas = new List<Empresas>();
            if (user != null)
            {
                //Busco el usuario que empleados tiene asociados y consulto sus empresas
                var dataEmpleados = new GenericBusinessLogic<Empleados>(this.UnitOfWork.Settings).FindAll(x => x.UserId == user.Id);
                resultEmpresas = new GenericBusinessLogic<Empresas>(this.UnitOfWork.Settings).FindAll(null).Where(x => dataEmpleados.Any(j => j.EmpresasId == x.Id)).ToList();
            }
            return resultEmpresas;
        }

        public void UpdateSecurityNavigation(User data, long ProfileId, string host)
        {
            DataBaseSetting BD = this.UnitOfWork.Settings;

            List<Profile> ListProfiles = new List<Profile>();
            if (ProfileId != 0) {
                ListProfiles = new GenericBusinessLogic<Profile>(BD).FindAll(x => x.Id == ProfileId, false);
            }
            else if (data == null)
            {
                ListProfiles = new GenericBusinessLogic<Profile>(BD).FindAll(null);
            }
            else
            {
                ListProfiles = (from ProfileUsers in new GenericBusinessLogic<ProfileUser>(BD).FindAll(x => x.UserId == data.Id)
                                join Profiles in new GenericBusinessLogic<Profile>(BD).FindAll(null)
                                on ProfileUsers.ProfileId equals Profiles.Id
                                select Profiles).ToList();
            }

            foreach (var Profile in ListProfiles)
            {
                SecurityNavigation securityNavigation = new SecurityNavigation();
                securityNavigation.ProfileId = Profile.Id;
                securityNavigation.SecurityPolicy = Profile.SecurityPolicy;

                List<Navegation> ListaNavegation = new List<Navegation>();
                List<ProfileNavigation> ListProfileNavigation = new GenericBusinessLogic<ProfileNavigation>(BD).FindAll(x => x.ProfileId == Profile.Id);
                foreach (var ProfileNavigation in ListProfileNavigation)
                {
                    Navegation navegation = new Navegation();
                    navegation.Path = ProfileNavigation.Path;
                    ListaNavegation.Add(navegation);
                }
                securityNavigation.ListNavegation = ListaNavegation;
                DApp.UpdateSecurityNavigation(securityNavigation, host);
            }

        }


        /// <summary>
        /// Se obtiene la seguridad de esta conexion de base de datos.
        /// </summary>
        /// <returns></returns>
        public List<SecurityNavigation> GetCurrentSecurityNavigation()
        {
            return this.UnitOfWork.Settings.ListSecurityNavigation;
        }


        /// <summary>
        /// Se obtienen todos los metodos registrados para evaluar su seguridad
        /// </summary>
        /// <returns></returns>
        public List<string> GetMenuActionsNames()
        {
            return new GenericBusinessLogic<MenuAction>(UnitOfWork.Settings).FindAll(null, false).Select(x => x.ActionName).Distinct().ToList();
        }

        //public Session GetCurrentSession(string userName)
        //{
        //    try
        //    {
        //        var oldSession = new GenericBusinessLogic<Session>(UnitOfWork.Settings).Tabla().Where(x => x.User.UserName == userName).Max(x => x.Id);

        //        var currentSession = new GenericBusinessLogic<Session>(UnitOfWork.Settings).FindById(x => x.Id == oldSession, false);
        //        if (!currentSession.IsExpired)
        //            currentSession.IsExpired = (DateTime.Now - currentSession.LastUpdate).TotalMinutes >= 15;
        //        return currentSession;
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}

    }
}
