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
    [Authorize(Roles="Developer")]
    public class UserDashboardController : Controller
    {
 
        // GET: Dashboard
        public ActionResult Dashboard()
        {
             
            DashboardModel objdashboardModel = new DashboardModel();
            CustomResponse response = APICalls.Get("UserDashboardAPI/Get?userid=" + User.Identity.GetUserId() + "&pageno=0");
            if (response.Status == CustomResponseStatus.Successful)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var projects = response.Response.ToString();
                UserDashboardDTO dbinfo = serializer1.Deserialize<UserDashboardDTO>(projects);
                return View(dbinfo);
            }
            return View();
        }

        public JsonResult GetRecentTickets()
        {
            CustomResponse res = APICalls.Get("TasksAPI/Get?projectID=0&IDType=1&task_status=0&Task_Type=0&priority=0&userid=" + User.Identity.GetUserId());
            CreateTaskDTO objTask = new CreateTaskDTO();
            if (res.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = res.Response.ToString();
                return Json(serializer1.Deserialize<List<TaskDTO>>(uinfo).Take(10), JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }


        public JsonResult GetActivitiesByPageNo(int pagenumber)
        {
            CustomResponse response = APICalls.Get("UserDashboardAPI/Get?userid=" + User.Identity.GetUserId() + "&pageno="+pagenumber);
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