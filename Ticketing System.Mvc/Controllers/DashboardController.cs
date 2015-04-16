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
    public class DashboardController : Controller
    {

        [Authorize(Roles = "Administrator")]
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            string Role = RoleHelper.GetUserRole();
            DashboardModel objdashboardModel = new DashboardModel();
            JavaScriptSerializer serializer1 = new JavaScriptSerializer();

            CustomResponse ObjActivityData = APICalls.Get("DashboardAPI/Get?Type=3&pageno=0");
            if (ObjActivityData.Status == CustomResponseStatus.Successful && ObjActivityData.Response != null)
            {
                var jsondata = ObjActivityData.Response.ToString();
               objdashboardModel.ActivityDTO=serializer1.Deserialize<List<Trans_TicketDTO>>(jsondata);
            }

            CustomResponse response = APICalls.Get("DashboardAPI/Get?Type=1&pageno=0");
            if (response.Status == CustomResponseStatus.Successful)
            {
                
                serializer1.MaxJsonLength = 1000000000;
                var projects = response.Response.ToString();
                List<DashBoardStatisticsDTO> dbinfo = serializer1.Deserialize<List<DashBoardStatisticsDTO>>(projects);
                foreach (DashBoardStatisticsDTO ds in dbinfo)
                {
                    if (ds.Type == "1")
                        objdashboardModel.ProjectsCount = ds.Count;
                    else if (ds.Type == "2")
                        objdashboardModel.ClientsCount = ds.Count;
                    else if (ds.Type == "3")
                        objdashboardModel.AdminsCount = ds.Count;
                    else if (ds.Type == "4")
                        objdashboardModel.UsersCount = ds.Count;
                }
                CustomResponse objtabledata = APICalls.Get("DashboardAPI/Get?Type=2&pageno=0");
                if (objtabledata.Status == CustomResponseStatus.Successful)
                {
                    var tabledata = objtabledata.Response.ToString();
                    objdashboardModel.TableData = serializer1.Deserialize<List<ProjectTicketUsersDTO>>(tabledata);
                }
                return View(objdashboardModel);
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
            CustomResponse response = APICalls.Get("DashboardAPI/Get?type=3&pageno=" + pagenumber);
            if (response.Status == CustomResponseStatus.Successful)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var projects = response.Response.ToString();
                return Json(serializer1.Deserialize <List<Trans_TicketDTO>>(projects), JsonRequestBehavior.AllowGet);
            }
            else return null;

        }
    }
}