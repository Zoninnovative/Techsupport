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
    [Authorize]
    public class ProjectsController : Controller
    {
        // GET: Projects
        public ActionResult ListAll()
        {
            CustomResponse res = APICalls.Get("projectsapi/Get?projectid=0&userid=" + User.Identity.GetUserId());
            if (res.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = res.Response.ToString();
                List<ProjectDTO> userinfo = serializer1.Deserialize<List<ProjectDTO>>(uinfo);
                return View(userinfo);
            }
            return View();
        }

        [NonAction]
        private CreateProjectModel FillCreateProjectModel()
        {

            CreateProjectModel objCreateNewModel = new CreateProjectModel();
            List<SelectListItem> objclients = new List<SelectListItem>();
            List<SelectListItem> objusers = new List<SelectListItem>();

            CustomResponse res = APICalls.Get("usersapi/Get?type=4");
            if (res.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = res.Response.ToString();
                List<UserDTO> userinfo = serializer1.Deserialize<List<UserDTO>>(uinfo);
                objclients.Add(new SelectListItem { Text = "Select Client", Value = "" });
                foreach (UserDTO udto in userinfo)
                    objclients.Add(new SelectListItem { Text = udto.FirstName, Value = udto.Id });
                objCreateNewModel.ClientsDDl = objclients;

                CustomResponse res1 = APICalls.Get("usersapi/Get?type=0");
                uinfo = res1.Response.ToString();
                userinfo = serializer1.Deserialize<List<UserDTO>>(uinfo);
                //objusers.Add(new SelectListItem { Text = "Select Project Manager", Value = "" });
                foreach (UserDTO udto in userinfo)
                    objusers.Add(new SelectListItem { Text = udto.FirstName, Value = udto.Id });
                objCreateNewModel.UsersList = objusers;
                return objCreateNewModel;
            }
            else
                return null;
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {

            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();

            return View(FillCreateProjectModel());
        }

        [Authorize(Roles = "Administrator,Developer")]
        [HttpPost]
        public ActionResult Create(CreateProjectModel objcreateproject)
        {

            //if (ModelState.IsValid)
            //{
            CreateProjectDTO objcreate = new CreateProjectDTO();
            List<string> Developesassigned = new List<string>();
            if (objcreateproject.UserIds.Trim().Length > 1)
                Developesassigned = objcreateproject.UserIds.Split('#').ToList();
            ProjectDTO obj = new ProjectDTO { Name = objcreateproject.Name, Description = objcreateproject.Description, Duration = objcreateproject.Duration, ClientID = objcreateproject.ClientID, PManagerID = objcreateproject.PManagerID, ProposedEndDate = objcreateproject.ProposedEndDate, ShortName = objcreateproject.ShortName, SignUpDate = objcreateproject.SignUpDate, StartDate = objcreateproject.StartDate };
            obj.CreatedBy = User.Identity.GetUserId();
            objcreate.objProject = obj;
            objcreate.Users = Developesassigned;
            CustomResponse res = APICalls.Post("projectsapi/post", objcreate);
            if (res.Response != null)
            {
                return RedirectToRoute("ProjectHomeRoute");
            }
            else
            {
                TempData["Message"] = "Failed to Add Project.";
                return RedirectToAction("Create");
            }
            //}
            //else
            //{
            //    return View(FillCreateProjectModel());
            //}

        }




        [HttpGet]
        public ActionResult Edit(int projectid)
        {


            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();
            CreateProjectModel objEditProjectModel = new CreateProjectModel();
            List<SelectListItem> objclients = new List<SelectListItem>();
            List<SelectListItem> objPms = new List<SelectListItem>();
            List<SelectListItem> objusers = new List<SelectListItem>();


            //get project details 
            CustomResponse objticketreponse = APICalls.Get("projectsapi/Get?userid=" + User.Identity.GetUserId() + "&projectid=" + projectid);
            CreateProjectDTO objprojectdetails = new CreateProjectDTO();
            ProjectDTO _projectDTO = new ProjectDTO();
            if (objticketreponse.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = objticketreponse.Response.ToString();
                _projectDTO = serializer1.Deserialize<ProjectDTO>(uinfo);
                objEditProjectModel.Name = _projectDTO.Name;
                objEditProjectModel.Description = _projectDTO.Description;
                objEditProjectModel.ID = _projectDTO.ID;
                objEditProjectModel.PManagerID = _projectDTO.PManagerID;
                objEditProjectModel.ClientID = _projectDTO.ClientID;
                objEditProjectModel.Duration = _projectDTO.Duration;
                objEditProjectModel.ProposedEndDate = _projectDTO.ProposedEndDate;
                objEditProjectModel.SignUpDate = _projectDTO.SignUpDate;
                objEditProjectModel.StartDate = _projectDTO.StartDate;
                objEditProjectModel.Status = _projectDTO.Status;
                objEditProjectModel.ShortName = _projectDTO.ShortName;

            }


            CustomResponse res = APICalls.Get("usersapi/Get?type=4");
            if (res.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = res.Response.ToString();
                List<UserDTO> userinfo = serializer1.Deserialize<List<UserDTO>>(uinfo);

                foreach (UserDTO udto in userinfo)
                {
                    objclients.Add(new SelectListItem { Text = udto.Email, Value = udto.Id });
                    objPms.Add(new SelectListItem { Text = udto.Email, Value = udto.Id });

                }
                objclients.Find(x => x.Value == _projectDTO.ClientID).Selected = true;
                objEditProjectModel.ClientsDDl = objclients;

                objPms.Find(x => x.Value == _projectDTO.PManagerID).Selected = true;
                objEditProjectModel.ProjectManagerDDl = objPms;
                CustomResponse res1 = APICalls.Get("usersapi/Get?type=0");
                uinfo = res1.Response.ToString();
                userinfo = serializer1.Deserialize<List<UserDTO>>(uinfo);
                foreach (UserDTO udto in userinfo)
                {
                    objusers.Add(new SelectListItem { Text = udto.Email, Value = udto.Id });
                }
                foreach (string user in _projectDTO.ProjectUsers)
                {
                    //objusers.Add(new SelectListItem { Text = udto.Email, Value = udto.Id });
                    foreach (SelectListItem y in objusers)
                    {
                        if (y.Value == user)
                        {
                            objusers.Find(x => x.Value == user).Selected = true;
                        }
                    }
                    objEditProjectModel.UserIds = objEditProjectModel.UserIds + '#' + user;
                }

                objEditProjectModel.UserIds.Trim('#');
                objEditProjectModel.UsersList = objusers;
                return View(objEditProjectModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(CreateProjectModel objcreateproject)
        {
            CreateProjectDTO objcreate = new CreateProjectDTO();
            List<string> Developesassigned = new List<string>();

            if (objcreateproject.UserIds.Trim().Length > 1)
                Developesassigned = objcreateproject.UserIds.Split('#').ToList();
            string updatedby = User.Identity.GetUserId();
            ProjectDTO obj = new ProjectDTO { ID = objcreateproject.ID, UpdatedBy = updatedby, Name = objcreateproject.Name, Description = objcreateproject.Description, Duration = objcreateproject.Duration, ClientID = objcreateproject.ClientID, PManagerID = objcreateproject.PManagerID, ProposedEndDate = objcreateproject.ProposedEndDate, ShortName = objcreateproject.ShortName, SignUpDate = objcreateproject.SignUpDate, StartDate = objcreateproject.StartDate };
            obj.CreatedBy = User.Identity.GetUserId();
            objcreate.objProject = obj;
            objcreate.Users = Developesassigned.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            CustomResponse res = APICalls.Put("projectsapi/Put", objcreate);
            if (res.Status == CustomResponseStatus.Successful)
            {
                return RedirectToRoute("ProjectHomeRoute");
            }
            else
            {
                TempData["Message"] = "Failed to Update Project.";
                return RedirectToAction("Create");
            }

        }

    }
}