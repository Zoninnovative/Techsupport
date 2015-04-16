using System;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.api;


using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticketing_System.Core;
using Ticketing_System.Mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Script.Serialization;
namespace Ticketing_System.Mvc.Controllers
{

    [Authorize()]
    public class ProjectReportController : Controller
    {
        // GET: ProjectReport
        public ActionResult Home()
        {
            ReportsModel objreportmodel = BindReportData();
            //objreportmodel.ProjectsDDl.Find(x => x.Value == "0").Selected = true;
            //objreportmodel.TaskStatusDDl.Find(x => x.Value == "0").Selected = true;
            //objreportmodel.TaskTypeDDl.Find(x => x.Value == "0").Selected = true;
                 return View(BindReportData());
        }

        public ReportsModel BindReportData()
        { 
         ReportsModel objreport = new ReportsModel();
         objreport.ReportData = new List<ReportData>();
         List<SelectListItem> objtasktypedata = new List<SelectListItem>();
         List<SelectListItem> objtaskstatusdata = new List<SelectListItem>();
             CustomResponse response = APICalls.Get("projectsapi/Get?projectid=0&userid=" + User.Identity.GetUserId());
             JavaScriptSerializer serializer1 = new JavaScriptSerializer();
             if (response.Response != null)
             {
                
                 var projects = response.Response.ToString();
                 List<ProjectDTO> projectsinfo = serializer1.Deserialize<List<ProjectDTO>>(projects);
                 List<SelectListItem> objprojects = new List<SelectListItem>();
                 foreach (ProjectDTO pdto in projectsinfo)
                 objprojects.Add(new SelectListItem { Text = pdto.Name, Value = pdto.ID.ToString() });
                 objtasktypedata.Add(new SelectListItem { Text = "All", Value = "0"  });
                 objreport.ProjectsDDl = objprojects;
                
             }
             CustomResponse restype2 = APICalls.Get("GenericAPI/Get?type=2");
             if (restype2.Response != null)
             {
                 var tasktype = restype2.Response.ToString();
                 List<TypeAndPriorityDTO> objtasktype = serializer1.Deserialize<List<TypeAndPriorityDTO>>(tasktype);
                 foreach (TypeAndPriorityDTO objtp in objtasktype)
                     objtasktypedata.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                 objtasktypedata.Add(new SelectListItem { Text = "All", Value = "0"});
                 objreport.TaskTypeDDl = objtasktypedata;

             }
             CustomResponse restype6 = APICalls.Get("GenericAPI/Get?type=3");
             if (restype6.Response != null)
             {
                 var tasktype1 = restype6.Response.ToString();
                 List<TypeAndPriorityDTO> objtasktype1 = serializer1.Deserialize<List<TypeAndPriorityDTO>>(tasktype1);
                 foreach (TypeAndPriorityDTO objtp in objtasktype1)
                     objtaskstatusdata.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                 objtaskstatusdata.Add(new SelectListItem { Text = "All", Value = "0"});
                 objreport.TaskStatusDDl = objtaskstatusdata;

             }
             return objreport;
             }
        
        public ActionResult GenerateReport(string ProjectID,string TaskStatus,string TaskType,string FromDate,string ToDate)
        {
            //get report data from the api and add it to model object
            ReportsModel objreportdata = BindReportData();
            ReportDTO objreportdto = new ReportDTO { ProjectID =Convert.ToInt32(ProjectID), TaskStatusID = Convert.ToInt32(TaskStatus), TaskTypeID = Convert.ToInt32(TaskType), FromDate = Convert.ToDateTime(FromDate), ToDate = Convert.ToDateTime(ToDate) };
            CustomResponse response = APICalls.Post("projectreportapi/Post", objreportdto);
            JavaScriptSerializer serializer1 = new JavaScriptSerializer();
            if (response.Response != null)
            {
                var reportdata = response.Response.ToString();
                List<ReportData> projectsinfo = serializer1.Deserialize<List<ReportData>>(reportdata);
                objreportdata.ReportData = projectsinfo;
            }
            //pre populate selected project
                objreportdata.TaskStatusDDl.Find(x => x.Value == TaskStatus).Selected = true;
                objreportdata.TaskTypeDDl.Find(x => x.Value == TaskType).Selected = true;
                objreportdata.ProjectsDDl.Find(x => x.Value == ProjectID).Selected = true;
            return View("Home",objreportdata);
        }

        public JsonResult GenerateReportEmail(string ProjectID, string TaskStatus, string TaskType, string FromDate, string ToDate)
        {
            ReportsModel objreportdata = BindReportData();
            ReportDTO objreportdto = new ReportDTO { ProjectID = Convert.ToInt32(ProjectID), TaskStatusID = Convert.ToInt32(TaskStatus), TaskTypeID = Convert.ToInt32(TaskType), FromDate = Convert.ToDateTime(FromDate), ToDate = Convert.ToDateTime(ToDate) };
            CustomResponse response = APICalls.Put("projectreportapi/Put", objreportdto);
            return Json(new { response.Message }, JsonRequestBehavior.AllowGet);

        }
    }
}