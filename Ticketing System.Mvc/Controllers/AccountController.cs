using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ticketing_System.Mvc.Models;
using Ticketing_System.Mvc.CustomIdentityClasses;
using Microsoft.AspNet.Identity.EntityFramework;
using Ticketing_System.Core;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;

namespace Ticketing_System.Mvc.Controllers
{
     
    public class AccountController : Controller
    {
        private UserManager<MyIdentityUser> userManager;
        private RoleManager<MyIdentityRole> roleManager;

        public AccountController()
        {

            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);
            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous]

        
        [HttpPost]
        public  ActionResult ForgotPassword(string txtemail)
        {

            UserDTO objuserinfo = new UserDTO {Email=txtemail };
            if (APICalls.Post("AuthenticationAPI/Post?type=" + 2, objuserinfo).Status == CustomResponseStatus.Successful)
                ViewBag.Message = "Email sent , Please check your email address !!!";
            else ViewBag.Message = "Failed to send email";
            return View();

        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string token,string userid)
        {
                ViewBag.Token = token;
                ViewBag.Uid = userid;
             return View();
        }

        [HttpPost]
        public  ActionResult ResetPassword(string userid,string token,string newpassword)
        {
           ChangePasswordDTO objchangepassword=new ChangePasswordDTO{userid=userid,oldpassword=token,newpassword=newpassword,ChageType=1};
           CustomResponse objres = APICalls.Put("AuthenticationAPI/Put", objchangepassword);
              
            if(objres.Status==CustomResponseStatus.Successful)
            return RedirectToRoute("LoginRoute");
             else
               ViewBag.Message = "Failed to update password";
               return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                MyIdentityUser user = new MyIdentityUser();
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MobileNumber = model.MobileNumber;
                user.CreatedBy = User.Identity.GetUserId();
                IdentityResult result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    
                    userManager.AddToRole(user.Id, "Administrator");
                   
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("UserName", "Error while creating the user!");
                }
            }
            return View(model);
        }



    [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                MyIdentityUser user = new MyIdentityUser();
                
                try
                {
                    CustomResponse res = APICalls.Get("AuthenticationAPI/Get?username=" + model.UserName+"&password="+model.Password+"&type=1");
                    if (res.Status == CustomResponseStatus.Successful && res.Response != null)
                    {
                        JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                        serializer1.MaxJsonLength = 1000000000;
                        var uinfo = res.Response.ToString();
                        user = serializer1.Deserialize<MyIdentityUser>(uinfo);
                    }
                   
                }
                catch (System.FormatException ex)
                {
                    return View();
                }
                if (user.Email !=null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        

                        List<string> roless = userManager.GetRoles(user.Id).ToList() ;

                        if (roless[0] == "Administrator")
                            return RedirectToRoute("DashboardRoute");
                        else if (roless[0] == "Client")
                            return RedirectToRoute("ClientDashboardRoute");
                        else

                            return RedirectToRoute("UserDashboardRoute");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View();
                }
            }
            return View(model);
        }


        
         
        [Authorize]
        public JsonResult ChangePassword(string oldpassword,string newpassword)
        {
            MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);
            ChangePasswordDTO objchangepassword = new ChangePasswordDTO {userid=user.Id,oldpassword=oldpassword,newpassword=newpassword,ChageType=2 };
            CustomResponse restype1 = APICalls.Put("AuthenticationAPI/Put", objchangepassword);
                  return Json(restype1, JsonRequestBehavior.AllowGet);
                   
                     
        }

        [Authorize]
        public JsonResult ChangeProfile()
        {
            MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);
            UserDTO objuserinfo = new UserDTO();
            CustomResponse res = APICalls.Get("AuthenticationAPI/Get?username=" + user.Id+"&password=0&type=2");
            if (res.Status == CustomResponseStatus.Successful)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = res.Response.ToString();
                objuserinfo = serializer1.Deserialize<UserDTO>(uinfo);
                return Json(new CustomResponse { Status = CustomResponseStatus.Successful, Response = objuserinfo, Message = "Profile Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new CustomResponse { Status = CustomResponseStatus.UnSuccessful, Response = objuserinfo, Message = "Profile Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }

 
        [Authorize]
       
        public JsonResult PostChangeProfile(string FirstName, string LastName, string MobileNumber)
        {
            if (ModelState.IsValid)
            {
                UserDTO objuser = new UserDTO { Id = HttpContext.User.Identity.GetUserId(), FirstName = FirstName, LastName = LastName, MobileNumber = MobileNumber };
                CustomResponse objres = APICalls.Post("AuthenticationAPI/Post?type=1", objuser);
                if (objres.Status == CustomResponseStatus.Successful)
                {
                    return Json(new CustomResponse { Status = CustomResponseStatus.Successful, Response = null, Message = "Profile Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError("", "Error while saving profile.");
                }
            }
            return null;
        }
        

       

        [Authorize]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToRoute("LoginRoute");
        }

    }
}