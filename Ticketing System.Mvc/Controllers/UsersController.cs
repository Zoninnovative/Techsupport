using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ticketing_System.Core;
using Ticketing_System.Mvc.CustomIdentityClasses;
using Ticketing_System.Mvc.Models;

namespace Ticketing_System.Mvc.Controllers
{

    [Authorize(Roles="Administrator")]
    public class UsersController : Controller
    {

        private UserManager<MyIdentityUser> userManager;
        private RoleManager<MyIdentityRole> roleManager;
        // GET: Users

        public UsersController()
        {

            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);
            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);
        }
        public ActionResult ListAll(string Role)
        {
            CustomResponse res = APICalls.Get("usersapi/Get?type="+Role);
            List<SelectListItem> objroles = new List<SelectListItem> {  new SelectListItem { Text = "Clients", Value = "2" }, new SelectListItem { Text = "Developers", Value = "1" }, new SelectListItem { Text = "All", Value = "0" } };
            if (Role == "" || Role ==null)
                objroles.Find(x => x.Text == "All").Selected = true;
            else
                objroles.Find(x => x.Value == Role.ToString()).Selected = true;
            ViewBag.Roles = objroles;

            if (res.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = res.Response.ToString();
                List<UserDTO> userinfo = serializer1.Deserialize<List<UserDTO>>(uinfo);


                
                return View(userinfo);
            }

            return View();
        }
        

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(UserDTO objuser)
        {
            if (ModelState.IsValid)
            {
                objuser.CreatedBy = User.Identity.GetUserId();
                CustomResponse objres = APICalls.Post("AuthenticationAPI/Post?Type=3",objuser);
                if (objres.Status == CustomResponseStatus.Successful)
                {
                    return RedirectToRoute("UsersHomeRoute", new { Role = "" });
                }
                else
                {
                    ViewBag.Message = "Error While Creating User";
                }
            }
            return View();
        }
        public ActionResult Delete(string id)
        {
            CustomResponse res = APICalls.Delete("AuthenticationAPI/Delete?uid=" + id);
            if (res.Status == CustomResponseStatus.Successful)
            {
                TempData["Message"] = res.Message;
                return RedirectToRoute("UsersHomeRoute", new { Role = "" });
            }
            else
            {
                ViewBag.Message = "Failed to Delete User";
                return View();
            }
        }

    }
}