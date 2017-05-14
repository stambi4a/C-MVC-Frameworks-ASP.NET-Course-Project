namespace ESportsEventsApp.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Web.Security;

    using Data;

    using global::Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public static class RoleExtensions
    {
        public static bool IsInAnyRole(this IPrincipal user, List<string> roles)
        {
            var userRoles = Roles.GetRolesForUser(user.Identity.Name);

            return userRoles.Any(u => roles.Contains(u));
        }

        public static bool IsInAnyRole(this RegisteredUser user, List<string> roles)
        {
            var userRoles = Roles.GetRolesForUser(user.UserName);

            return userRoles.Any(u => roles.Contains(u));
        }

        public static bool IsInGivenRole(this RegisteredUser user, string roleName)
        {
            var context = new ESportsEventsContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userRoles = user.Roles.Select(r => roleManager.FindById(r.RoleId).Name);

            return userRoles.Contains(roleName);
        }

        //public static bool IsInGivenRole(this RegisteredUser user, string roleName)
        //{
        //    var context = new ESportsEventsContext();
        //    //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        //    var userRoles = Roles.GetRolesForUser(user.UserName);

        //    return userRoles.Contains(roleName);
        //}

        public static string GetRoles(this RegisteredUser user)
        {
            var context = new ESportsEventsContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userRoles = user.Roles.Select(r => roleManager.FindById(r.RoleId).Name);
            return string.Join(", ", userRoles);
        }
    }
}