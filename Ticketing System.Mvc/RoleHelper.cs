using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Ticketing_System.Mvc.CustomIdentityClasses;

namespace Ticketing_System.Mvc
{
    public static class RoleHelper
    {
        static UserManager<MyIdentityUser> userManager;
        static RoleManager<MyIdentityRole> roleManager;

       

        public static dynamic GetUserRole()
        { 
            var userIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            if (roles.Count > 0)
                return roles[0].Value;
            else 
                return null;
        }

        public static dynamic GetUserRoleByID(string userid)
        {


            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);
            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);
            List<string> roles=  userManager.GetRoles(userid).ToList();
            
            if (roles.Count > 0)
                return roles[0];
            else
                return null;
        }


        public static string GetTaskStatus(int data)
        {

switch(data){

    case 1:

        return "To Do";
        break;
    case 2:
        return "Done";
        break;
    case 3:
        return "In Review";
        break;
    case 4:
        return "Re Open";

    case 5:

        return "In Progress";

    default :

        return "None";
}
return "None";

        }
    }
}