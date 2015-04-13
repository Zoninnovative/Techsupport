using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Ticketing_System.Core;
using Ticketing_System.Mvc.CustomIdentityClasses;


namespace Ticketing_System.Mvc.Controllers
{
    public class UploadDocumentController : ApiController
    {

    

        public UploadDocumentController()
        { 
           
        }
        public dynamic Post(UploadDocumentDTO objuploaddocument)
         {
            Random r = new Random();
            CustomResponse objres = new CustomResponse();
            try
            {
                 //now we need to upload content into uploaded documents folder and then return path
                byte[] bytes = System.Convert.FromBase64String(objuploaddocument.Base64String);
                string savedfilename = r.Next() + DateTime.Now.Millisecond.ToString() + r.Next() + DateTime.Now.Millisecond.ToString() + '_' + objuploaddocument.FileName;
                File.WriteAllBytes(System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["DocumentsPath"].ToString() + savedfilename), bytes);
                objres.Message = "Success";
                objres.Response = savedfilename;
                objres.Status = CustomResponseStatus.Successful;
                return objres;

            }
            catch (Exception ex)
            {
                objres.Message = ex.Message;
                objres.Response = null;
                objres.Status = CustomResponseStatus.UnSuccessful;
                return objres;

            }
        }


        // change password api
    //    [HttpPut]
    //    public dynamic Put(ChangePasswordDTO objchnagepassworddto)
    //{

    //    CustomResponse response = new CustomResponse();
    //    try {
    //        IdentityResult result = userManager.ChangePassword(objchnagepassworddto.userid,objchnagepassworddto.oldpassword,objchnagepassworddto.newpassword);
    //        response.Response = null;

    //        if (result.Succeeded)
    //        {
    //            response.Status = CustomResponseStatus.Successful;
    //            response.Message = "Password Changed Successfully";
    //        }
    //        else
    //        {
    //            response.Status = CustomResponseStatus.UnSuccessful;
    //            response.Message = "Failed to update Password";
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        response.Status = CustomResponseStatus.Successful;
    //        response.Message = ex.Message;
    //        response.Response = null;
    //    }

    //    return response;
    //}
    }
}
