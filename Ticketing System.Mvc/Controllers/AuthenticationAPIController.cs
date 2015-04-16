using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Ticketing_System.Core;
using Ticketing_System.Mvc.CustomIdentityClasses;
using Ticketing_System.Repositoy;

namespace Ticketing_System.Mvc.Controllers
{
    public class AuthenticationAPIController : ApiController
    {
        
        private UserManager<MyIdentityUser> userManager;
        private RoleManager<MyIdentityRole> roleManager;


        public AuthenticationAPIController()
        {
            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);
            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);
        
        }

        
        //reset password api
        [HttpPut]
        public dynamic Put(ChangePasswordDTO objresetpassword)
        {

            CustomResponse objres = new CustomResponse();
            if (objresetpassword.ChageType == 1)
            {

                try
                {
                    //compare key with database
                    if (AccountRepository.CompareResetToken(objresetpassword.userid, objresetpassword.oldpassword))
                    {
                        var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Sample");
                        userManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<MyIdentityUser>(provider.Create("EmailConfirmation"));

                        string resettoken = userManager.GeneratePasswordResetToken(objresetpassword.userid);
                        IdentityResult objresult = userManager.ResetPassword(objresetpassword.userid, resettoken, objresetpassword.newpassword);
                        if (objresult.Succeeded)
                        {
                            objres.Status = CustomResponseStatus.Successful;
                            objres.Message = "Password Updated Successfully";
                            objres.Response = null;
                        }
                        else
                        {
                            objres.Status = CustomResponseStatus.UnSuccessful;
                            objres.Message = "Failed";
                            objres.Response = null;
                        }
                    }
                    else
                    {
                        objres.Status = CustomResponseStatus.UnSuccessful;
                        objres.Message = "Invalid Access token";
                        objres.Response = null;
                    }



                }
                catch (Exception ex)
                {
                    objres.Status = CustomResponseStatus.Exception;
                    objres.Message = ex.Message;
                    objres.Response = null;

                }
                return objres;
            }
            else if (objresetpassword.ChageType==2)
            {

                try
                {
                    IdentityResult result = userManager.ChangePassword(objresetpassword.userid, objresetpassword.oldpassword, objresetpassword.newpassword);
                    objres.Response = null;

                    if (result.Succeeded)
                    {
                        objres.Status = CustomResponseStatus.Successful;
                        objres.Message = "Password Changed Successfully";
                    }
                    else
                    {
                        objres.Status = CustomResponseStatus.UnSuccessful;
                        objres.Message = "Failed to update Password";
                    }
                }
                catch (Exception ex)
                {

                    objres.Status = CustomResponseStatus.Successful;
                    objres.Message = ex.Message;
                    objres.Response = null;
                }

                return objres;
            }
            else if (objresetpassword.ChageType == 3)
            {

                try
                {
                    MyIdentityUser objuser = userManager.FindByEmail(objresetpassword.Email);
                    objuser.FirstName = objresetpassword.FirstName;
                    objuser.LastName = objresetpassword.LastName;
                    objuser.MobileNumber = objresetpassword.MobileNumber;
                    IdentityResult objidentityresult=   userManager.Update(objuser);
                    objres.Response = null;

                    if (objidentityresult.Succeeded)
                    {
                        objres.Status = CustomResponseStatus.Successful;
                        objres.Message = "User Updated Successfully";
                    }
                    else
                    {
                        objres.Status = CustomResponseStatus.UnSuccessful;
                        objres.Message = "Failed to update User Details";
                    }
                }
                catch (Exception ex)
                {

                    objres.Status = CustomResponseStatus.Successful;
                    objres.Message = ex.Message;
                    objres.Response = null;
                }

                return objres;
            }
            else
                return null;
        
        
        }


        // Get user profile
        public dynamic Get(string username,string password,int type)
            {
            if (type == 1)
            {

                CustomResponse objres = new CustomResponse();
                objres.Response = userManager.Find(username, password);
                objres.Status = CustomResponseStatus.Successful;
                objres.Message = "Successful";
                return objres;
            }
            else if (type == 2)
                return AccountRepository.GetUserProfile(username);
            else
                return null;
        }

        // Type-1: update proifle   Type-2 : forgot password link sending, Type-3 : Add User
        public dynamic Post(UserDTO objuserinfo,int type)
        {
            CustomResponse objres = new CustomResponse();

            if (type == 1)

            {
                return UserRepository.UpdateProfile(objuserinfo);
            }
            else if (type == 2)
            {
                try
                {
                    MyIdentityUser objuser = userManager.FindByEmail(objuserinfo.Email);
                    string uid = objuser.Id;
                    Random r = new Random();
                    string resettoken = DateTime.Now.Millisecond.ToString() + r.Next(9999) + DateTime.Now.Millisecond + r.Next(9999).ToString() + DateTime.Now.Millisecond;
                    AccountRepository.AddPasswordResetToken(uid, resettoken);
                    Email.SendEmail("<span><h3>Please Click in the below link to reset your password</h3>  </span><div> <a href='" + ConfigurationManager.AppSettings["BaseWebUrl"].ToString() + "/Account/ResetPassword?token=" + resettoken + "&userid=" + objuser.Id + "'>Reset Password</a></div>", @"~/Email Templates/EmailTemplate.html", objuser.UserName, "Zon ticketing System Reset Password Link");
                    objres.Status = CustomResponseStatus.Successful;
                    objres.Message = "success";
                    objres.Response = null;
                }
                catch (Exception ex)
                {
                    objres.Status = CustomResponseStatus.Exception;
                    objres.Message = ex.Message;
                    objres.Response = null;

                }
              
            }
            else if (type == 3)
            {
                try
                {
                    MyIdentityUser user = new MyIdentityUser();
                    user.UserName = objuserinfo.Email;
                    user.Email = objuserinfo.Email;
                    user.FirstName = objuserinfo.FirstName;
                    user.LastName = objuserinfo.LastName;
                    user.MobileNumber = objuserinfo.MobileNumber;
                    user.CreatedBy = objuserinfo.CreatedBy;
                    user.Status = "1";
                    IdentityResult result = userManager.Create(user, "Zoninn@123");
                    if (result.Succeeded)
                    {
                        if (Convert.ToInt32(objuserinfo.Password) == 1)
                            userManager.AddToRole(user.Id, "Administrator");
                        else if (Convert.ToInt32(objuserinfo.Password) == 2)
                            userManager.AddToRole(user.Id, "Client");
                        else if (Convert.ToInt32(objuserinfo.Password) == 3)
                            userManager.AddToRole(user.Id, "Developer");
                        Email.SendEmail("<span><h3>You are now registered user of  Zon Innovative Ticketing System.</h3>  </span><div>Your registered Username is <b>" + objuserinfo.Email + "</b></div><div>Your Password is <b>Zoninn@123</b></div><div>You will need to use this the next time you login to our system.</div>", @"~/Email Templates/EmailTemplate.html", objuserinfo.Email, "User Registered In Zon Innovative Ticketing System");

                        objres.Status = CustomResponseStatus.Successful;
                        objres.Message = "Success";
                        objres.Response = null;
                    }
                    else
                    {
                        objres.Status = CustomResponseStatus.UnSuccessful;
                        objres.Message = "Failed";
                        objres.Response = null;
                    }

                }
                catch (Exception ex)
                {
                    objres.Status = CustomResponseStatus.Exception;
                    objres.Message = ex.Message;
                    objres.Response = null;

                }
            }
            return objres;
        }

        public dynamic Delete(string userid)
        {
            return UserRepository.Delete(userid);
        
        }
        

    }
}
