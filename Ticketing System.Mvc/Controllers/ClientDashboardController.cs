using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ticketing_System.Core;
using Ticketing_System.Mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Ticketing_System.Mvc.Controllers
{
    public class ClientDashboardController : Controller
    {
        [Authorize(Roles="Client")]
        // GET: ClientDashboard
        public ActionResult Dashboard()
        {
            DashboardModel objdashboardModel = new DashboardModel();
            ClientDashboardDTO objClientDashboradDTO = new ClientDashboardDTO();
            CustomResponse response = APICalls.Get("ClientDashboardAPI/Get?userid=" + User.Identity.GetUserId() + "&pageno=0");
            if (response.Status == CustomResponseStatus.Successful)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var projects = response.Response.ToString();
                objClientDashboradDTO = serializer1.Deserialize<ClientDashboardDTO>(projects);
                return View(objClientDashboradDTO);
            }
            return Dashboard();
        }
        public JsonResult GetActivitiesByPageNo(int pagenumber)
        {
            CustomResponse response = APICalls.Get("ClientDashboardAPI/Get?userid=" + User.Identity.GetUserId() + "&pageno=" + pagenumber);
            if (response.Status == CustomResponseStatus.Successful)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var projects = response.Response.ToString();
                UserDashboardDTO dbinfo = serializer1.Deserialize<UserDashboardDTO>(projects);
                return Json(dbinfo.ActivityDTO, JsonRequestBehavior.AllowGet);
            }
            else return null;

        }
    }
}