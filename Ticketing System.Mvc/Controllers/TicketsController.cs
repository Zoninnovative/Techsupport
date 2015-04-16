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
    public class TicketsController : Controller
    {
        // GET: Tickets
        [HttpGet]
        public ActionResult ListAll(int ProjectID = 0, int task_Type = 0, int task_status = 0, int priority=0)
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();
            return View(FillTaskDropDowns(ProjectID, task_status, task_Type, priority));
        }


        public CreateTaskDTO FillTaskDropDowns(int ProjectID, int task_status, int task_Type, int priority)
        {
            CustomResponse res = APICalls.Get("TasksAPI/Get?ProjectID=" + ProjectID + "&IDType=1&userid=" + User.Identity.GetUserId() + "&task_status=" + task_status + "&Task_Type=" + task_Type+"&priority="+priority);
            CreateTaskDTO objTask = new CreateTaskDTO();
            if (res.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = res.Response.ToString();
                objTask.TasksDTO = serializer1.Deserialize<List<TaskDTO>>(uinfo);
                CustomResponse response = APICalls.Get("projectsapi/Get?projectid=0&userid=" + User.Identity.GetUserId());
                if (response.Response != null)
                {
                    var projects = response.Response.ToString();
                    List<ProjectDTO> projectsinfo = serializer1.Deserialize<List<ProjectDTO>>(projects);
                    List<SelectListItem> objprojects = new List<SelectListItem>();
                    foreach (ProjectDTO pdto in projectsinfo)
                        objprojects.Add(new SelectListItem { Text = pdto.Name, Value = pdto.ID.ToString() });
                    if (ProjectID == 0)
                        objprojects.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
                    else
                    {
                        objprojects.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
                        objprojects.Find(x => x.Value == ProjectID.ToString()).Selected = true;
                    }

                    List<SelectListItem> objtasktypedata = new List<SelectListItem>();
                    List<SelectListItem> objtaskstatusdata = new List<SelectListItem>();
                    List<SelectListItem> objtaskstatusdata2 = new List<SelectListItem>();


                    CustomResponse restype6 = APICalls.Get("GenericAPI/Get?type=1");
                    if (restype6.Response != null)
                    {
                        var tasktype1 = restype6.Response.ToString();
                        List<TypeAndPriorityDTO> objtasktype1 = serializer1.Deserialize<List<TypeAndPriorityDTO>>(tasktype1);
                        
                        foreach (TypeAndPriorityDTO objtp in objtasktype1)
                            objtaskstatusdata2.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                        if (priority == 0)
                            objtaskstatusdata2.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
                        else
                        {
                            objtaskstatusdata2.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
                            objtaskstatusdata2.Find(x => x.Value == priority.ToString()).Selected = true;
                        }
                        objTask.PriorityDDL = objtaskstatusdata2;

                    }
                    
                    CustomResponse restype2 = APICalls.Get("GenericAPI/Get?type=2");
                    if (restype2.Response != null)
                    {
                        var tasktype = restype2.Response.ToString();
                        List<TypeAndPriorityDTO> objtasktype = serializer1.Deserialize<List<TypeAndPriorityDTO>>(tasktype);
                       
                        foreach (TypeAndPriorityDTO objtp in objtasktype)
                            objtasktypedata.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                        if (task_Type == 0)
                            objtasktypedata.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
                        else
                        {
                            objtasktypedata.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
                            objtasktypedata.Find(x => x.Value == task_Type.ToString()).Selected = true;
                        }
                        objTask.TypeDDL = objtasktypedata;

                    }
                    CustomResponse restype7 = APICalls.Get("GenericAPI/Get?type=3");
                    if (restype7.Response != null)
                    {
                        var tasktype1 = restype7.Response.ToString();
                        List<TypeAndPriorityDTO> objtasktype1 = serializer1.Deserialize<List<TypeAndPriorityDTO>>(tasktype1);
                        
                        foreach (TypeAndPriorityDTO objtp in objtasktype1)
                            objtaskstatusdata.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                        if (task_status == 0)
                            objtaskstatusdata.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
                        else
                        {
                            objtaskstatusdata.Insert(0, new SelectListItem { Text = "All", Value = "0", Selected = true });
                            objtaskstatusdata.Find(x => x.Value == task_status.ToString()).Selected = true;
                        }
                        objTask.TaskStatusDDl = objtaskstatusdata;

                    }



                    objTask.ProjectsDDL = objprojects;

                    return objTask;
                }
                return null;
            }

            return null;
        }


        [HttpGet]
        public ActionResult Create()
        {

            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();
            
            return View(FillCreateTaskDropdowns());
        }

        [NonAction]
        private CreateTaskDTO FillCreateTaskDropdowns()
        
        {

            CreateTaskDTO objcreateTask = new CreateTaskDTO();
            List<SelectListItem> ObjDllData = new List<SelectListItem>();
            List<SelectListItem> ObjDllData1 = new List<SelectListItem>();
            List<SelectListItem> ObjDllData2 = new List<SelectListItem>();
            List<SelectListItem> ObjDllData3 = new List<SelectListItem>();
            List<SelectListItem> ObjDllData4 = new List<SelectListItem>();
            List<SelectListItem> ObjDllData5 = new List<SelectListItem>();
            CustomResponse restype1 = APICalls.Get("GenericAPI/Get?type=1");
            if (restype1.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = restype1.Response.ToString();
                List<TypeAndPriorityDTO> objprioroty = serializer1.Deserialize<List<TypeAndPriorityDTO>>(uinfo);
                ObjDllData.Add(new SelectListItem { Text = "Select Priority", Value = "" });
                foreach (TypeAndPriorityDTO objtp in objprioroty)
                    ObjDllData.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                objcreateTask.PriorityDDL = ObjDllData;


                CustomResponse restype2 = APICalls.Get("GenericAPI/Get?type=2");
                if (restype2.Response != null)
                {
                    var tasktype = restype2.Response.ToString();
                    List<TypeAndPriorityDTO> objtasktype = serializer1.Deserialize<List<TypeAndPriorityDTO>>(tasktype);
                    ObjDllData1.Add(new SelectListItem { Text = "Select User", Value = "" });
                    foreach (TypeAndPriorityDTO objtp in objtasktype)
                        ObjDllData1.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                    objcreateTask.TypeDDL = ObjDllData1;

                }
                CustomResponse restype3 = APICalls.Get("projectsapi/Get?projectid=0&userid=" + User.Identity.GetUserId());
                if (restype3.Response != null)
                {
                    var projects = restype3.Response.ToString();
                    List<ProjectDTO> projectsinfo = serializer1.Deserialize<List<ProjectDTO>>(projects);
                    ObjDllData2.Add(new SelectListItem { Text = "Select User", Value = "" });
                    foreach (ProjectDTO objtp in projectsinfo)
                        ObjDllData2.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                    objcreateTask.ProjectsDDL = ObjDllData2;

                }

                CustomResponse restype4 = APICalls.Get("UsersAPI/Get?type=1");
                if (restype4.Response != null)
                {
                    var assignees = restype4.Response.ToString();
                    List<UserDTO> usersdto = serializer1.Deserialize<List<UserDTO>>(assignees);
                    ObjDllData3.Add(new SelectListItem { Text = "Select User", Value = "" });
                    foreach (UserDTO objuser in usersdto)
                        ObjDllData3.Add(new SelectListItem { Text = objuser.FirstName, Value = objuser.Id.ToString() });
                    objcreateTask.AssignTODDL = ObjDllData3;

                }

                CustomResponse restype5 = APICalls.Get("UsersAPI/Get?type=0");
                if (restype5.Response != null)
                {
                    var referer = restype5.Response.ToString();
                    List<UserDTO> usersdto1 = serializer1.Deserialize<List<UserDTO>>(referer);
                    ObjDllData4.Add(new SelectListItem { Text = "Select User", Value = "" });
                    foreach (UserDTO objuser in usersdto1)
                        ObjDllData4.Add(new SelectListItem { Text = objuser.FirstName, Value = objuser.Id.ToString() });
                    objcreateTask.RefereerToDDL = ObjDllData4;

                }
                CustomResponse restype6 = APICalls.Get("GenericAPI/Get?type=3");
                if (restype6.Response != null)
                {
                    var tasktype1 = restype6.Response.ToString();
                    List<TypeAndPriorityDTO> objtasktype1 = serializer1.Deserialize<List<TypeAndPriorityDTO>>(tasktype1);
                    ObjDllData5.Add(new SelectListItem { Text = "Select TaskStatus", Value = "" });
                    foreach (TypeAndPriorityDTO objtp in objtasktype1)
                        ObjDllData5.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                    objcreateTask.TaskStatusDDl = ObjDllData5;

                }

            }

            return objcreateTask;
        }

        [HttpPost]
        public ActionResult Create(CreateTaskDTO objcreateTask)
        {
            if (ModelState.IsValid)
            {
                List<UploadDocumentDTO> objuploaddocumentlist = new List<UploadDocumentDTO>();
                if (Request.Files.Count > 0)
                {

                    for (int i = 0; i < Request.Files.Count; i++)
                    {

                        HttpPostedFileBase objfile = Request.Files[i];
                        if (objfile.FileName != "")
                        {
                            byte[] binaryData;
                            binaryData = new Byte[objfile.InputStream.Length];
                            long bytesRead = objfile.InputStream.Read(binaryData, 0, (int)objfile.InputStream.Length);
                            objfile.InputStream.Close();
                            string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);


                            UploadDocumentDTO objuploaddocument = new UploadDocumentDTO { FileName = objfile.FileName, Format = objfile.ContentType.Split('/')[1], Base64String = base64String };
                            // upload document
                            CustomResponse restype5 = APICalls.Post("UploadDocument/Post", objuploaddocument);
                            if (restype5.Response != null)
                            {
                                objuploaddocument.Base64String = restype5.Response.ToString();
                                objuploaddocumentlist.Add(objuploaddocument);
                            }
                        }
                    }
                }
                objcreateTask.CreatedBy = User.Identity.GetUserId();
                TaskDTO objtaskdto = new TaskDTO { Title = objcreateTask.Title, Task_Status = objcreateTask.Task_Status, Description = objcreateTask.Description, DueDate = objcreateTask.DueDate, PriorityID = objcreateTask.PriorityID, TypeID = objcreateTask.TypeID, RefereedTo = objcreateTask.RefereedTo, AssignedTo = objcreateTask.AssignedTo, CreatedBy = objcreateTask.CreatedBy, AssigndedDate = objcreateTask.AssigndedDate, ProjectID = objcreateTask.ProjectID, Attachment1_Path = objcreateTask.file };
                int j = 0;
                foreach(UploadDocumentDTO objfile in objuploaddocumentlist){
                    if (j == 0)
                    {
                        objtaskdto.Attachment1_Name = objfile.FileName;
                        objtaskdto.Attachment1_Path = objfile.Base64String;
                    }
                    if (j == 1)
                    {
                        objtaskdto.Attachment2_Name = objfile.FileName;
                        objtaskdto.Attachment2_Path = objfile.Base64String;
                    }
                    if (j == 2)
                    {
                        objtaskdto.Attachment3_Name = objfile.FileName;
                        objtaskdto.Attachment3_Path = objfile.Base64String;
                    }
                    j++;
                }
                
                CustomResponse res = APICalls.Post("TasksAPI/Post", objtaskdto);
                if (res.Status == CustomResponseStatus.Successful)
                {
                    TempData["Message"] = res.Message;
                    return RedirectToRoute("TicketsHomeRoute", new { ProjectID = 0 });
                }
                else
                 return View(FillCreateTaskDropdowns());
            }
            else
            {
                return View(FillCreateTaskDropdowns());
            }

           
        }

        [HttpGet]
        public ActionResult Edit(int ticketid)
        {

            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();
            CreateTaskDTO objcreateTask = new CreateTaskDTO();
            List<SelectListItem> ObjDllData = new List<SelectListItem>();
            List<SelectListItem> ObjDllData1 = new List<SelectListItem>();
            List<SelectListItem> ObjDllData2 = new List<SelectListItem>();
            List<SelectListItem> ObjDllData3 = new List<SelectListItem>();
            List<SelectListItem> ObjDllData4 = new List<SelectListItem>();
            List<SelectListItem> ObjDllData5 = new List<SelectListItem>();

            //get ticket details

            CustomResponse objticketreponse = APICalls.Get("TasksAPI/Get?projectID=" + ticketid + "&task_status=0&Task_Type=0&priority=0&IDType=2&userid=" + User.Identity.GetUserId());
            CreateTaskDTO objtaskdetails = new CreateTaskDTO();
            TaskDTO _taskDTO = new TaskDTO();
            if (objticketreponse.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = objticketreponse.Response.ToString();
                _taskDTO = serializer1.Deserialize<TaskDTO>(uinfo);
                objcreateTask.Title = _taskDTO.Title;
                objcreateTask.Description = _taskDTO.Description;
                objcreateTask.TaskDisplayName = _taskDTO.TaskDisplayName;
                objcreateTask.ProjectID = _taskDTO.ProjectID;
                objcreateTask.TypeID = _taskDTO.TypeID;
                objcreateTask.Task_Status = _taskDTO.Task_Status;
                objcreateTask.RefereedTo = _taskDTO.RefereedTo;
                objcreateTask.AssignedTo = _taskDTO.AssignedTo;
                objcreateTask.DueDate = _taskDTO.DueDate;
                objcreateTask.AssigndedDate = _taskDTO.AssigndedDate;
                objcreateTask.ID = _taskDTO.ID;
                objcreateTask.Comments = _taskDTO.Comments;
                objcreateTask.Attachment1_Name = _taskDTO.Attachment1_Name;
                objcreateTask.Attachment1_Path = _taskDTO.Attachment1_Path;
                objcreateTask.Attachment2_Name = _taskDTO.Attachment2_Name;
                objcreateTask.Attachment2_Path = _taskDTO.Attachment2_Path;
                objcreateTask.Attachment3_Name = _taskDTO.Attachment3_Name;
                objcreateTask.Attachment3_Path = _taskDTO.Attachment3_Path;
            }

            CustomResponse restype1 = APICalls.Get("GenericAPI/Get?type=1");

            if (restype1.Response != null)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = 1000000000;
                var uinfo = restype1.Response.ToString();
                List<TypeAndPriorityDTO> objprioroty = serializer1.Deserialize<List<TypeAndPriorityDTO>>(uinfo);

                foreach (TypeAndPriorityDTO objtp in objprioroty)
                    ObjDllData.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                ObjDllData.Find(x => x.Value == _taskDTO.PriorityID.ToString()).Selected = true;
                objcreateTask.PriorityDDL = ObjDllData;


                CustomResponse restype2 = APICalls.Get("GenericAPI/Get?type=2");
                if (restype2.Response != null)
                {
                    var tasktype = restype2.Response.ToString();
                    List<TypeAndPriorityDTO> objtasktype = serializer1.Deserialize<List<TypeAndPriorityDTO>>(tasktype);
                    foreach (TypeAndPriorityDTO objtp in objtasktype)
                        ObjDllData1.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                    ObjDllData1.Find(x => x.Value == _taskDTO.TypeID.ToString()).Selected = true;
                    objcreateTask.TypeDDL = ObjDllData1;

                }
                CustomResponse restype3 = APICalls.Get("projectsapi/Get?projectid=0&userid="+User.Identity.GetUserId());
                if (restype3.Response != null)
                {
                    var projects = restype3.Response.ToString();
                    List<ProjectDTO> projectsinfo = serializer1.Deserialize<List<ProjectDTO>>(projects);
                    foreach (ProjectDTO objtp in projectsinfo)
                        ObjDllData2.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                    ObjDllData2.Find(x => x.Value == _taskDTO.ProjectID.ToString()).Selected = true;
                    objcreateTask.ProjectsDDL = ObjDllData2;

                }

                CustomResponse restype4 = APICalls.Get("UsersAPI/Get?type=1");
                if (restype4.Response != null)
                {
                    var assignees = restype4.Response.ToString();
                    List<UserDTO> usersdto = serializer1.Deserialize<List<UserDTO>>(assignees);
                    foreach (UserDTO objuser in usersdto)
                        ObjDllData3.Add(new SelectListItem { Text = objuser.FirstName, Value = objuser.Id.ToString() });
                    ObjDllData3.Find(x => x.Value == _taskDTO.AssignedTo.ToString()).Selected = true;
                    objcreateTask.AssignTODDL = ObjDllData3;

                }

                CustomResponse restype5 = APICalls.Get("UsersAPI/Get?type=0");
                if (restype5.Response != null)
                {
                    var referer = restype5.Response.ToString();
                    List<UserDTO> usersdto1 = serializer1.Deserialize<List<UserDTO>>(referer);
                    foreach (UserDTO objuser in usersdto1)
                        ObjDllData4.Add(new SelectListItem { Text = objuser.FirstName, Value = objuser.Id.ToString() });
                    //ObjDllData4.Find(x => x.Value == _taskDTO.RefereedTo.ToString()).Selected = true;
                    objcreateTask.RefereerToDDL = ObjDllData4;

                }
                CustomResponse restype6 = APICalls.Get("GenericAPI/Get?type=3");
                if (restype6.Response != null)
                {
                    var tasktype1 = restype6.Response.ToString();
                    List<TypeAndPriorityDTO> objtasktype1 = serializer1.Deserialize<List<TypeAndPriorityDTO>>(tasktype1);
                    foreach (TypeAndPriorityDTO objtp in objtasktype1)
                        ObjDllData5.Add(new SelectListItem { Text = objtp.Name, Value = objtp.ID.ToString() });
                    //ObjDllData5.Find(x => x.Value == _taskDTO.Task_Status.ToString()).Selected = true;
                    objcreateTask.TaskStatusDDl = ObjDllData5;

                }

            }
            return View(objcreateTask);
        }


        [HttpPost]
        public ActionResult Edit(CreateTaskDTO objcreateTask)
        {
            objcreateTask.CreatedBy = User.Identity.GetUserId();
            string updatedby = User.Identity.GetUserId();
            TaskDTO objtaskdto = new TaskDTO { ID = objcreateTask.ID, UpdatedBy = updatedby, Title = objcreateTask.Title, Task_Status = objcreateTask.Task_Status, Description = objcreateTask.Description, DueDate = objcreateTask.DueDate, PriorityID = objcreateTask.PriorityID, TypeID = objcreateTask.TypeID, RefereedTo = objcreateTask.RefereedTo, AssignedTo = objcreateTask.AssignedTo, CreatedBy = objcreateTask.CreatedBy, AssigndedDate = objcreateTask.AssigndedDate, ProjectID = objcreateTask.ProjectID };
            CustomResponse res = APICalls.Put("TasksAPI/Put", objtaskdto);
            if (res.Status == CustomResponseStatus.Successful)
            {
             //   TempData["Message"] = res.Message;
                return RedirectToRoute("TicketsHomeRoute", new { ProjectID = 0 });
            }
            ViewBag.Message = "Failed to Update Task";
            return RedirectToRoute("TicketsHomeRoute", new { ProjectID = 0 });
        }

        public ActionResult Comment(string CommentText, string Tid)
        {
            UploadDocumentDTO objuploaddocument;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase objfile = Request.Files[0];


                byte[] binaryData;
                binaryData = new Byte[objfile.InputStream.Length];
                long bytesRead = objfile.InputStream.Read(binaryData, 0, (int)objfile.InputStream.Length);
                objfile.InputStream.Close();
                string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
                objuploaddocument = new UploadDocumentDTO { FileName = objfile.FileName, Format = objfile.ContentType.Split('/')[1], Base64String = base64String };
                // upload document
                CustomResponse restype5 = APICalls.Post("UploadDocument/Post", objuploaddocument);
                if (restype5.Response != null)
                {
                    var path = restype5.Response.ToString();
                    Trans_TicketDTO objcomment = new Trans_TicketDTO { TaskID = Convert.ToInt32(Tid), AttachmentName = objfile.FileName, AttachmentPath = path, CreatedBy = User.Identity.GetUserId(), CreatedDate = DateTime.Now, Comments = CommentText };
                    CustomResponse objcreatedocres = APICalls.Post("TaskTrasactionsAPI/Post", objcomment);

                    if (objcreatedocres.Status == CustomResponseStatus.Successful)
                    {
                        return RedirectToRoute("EditTicketRoute", new { ticketid = Tid });
                    }
                }
            }
            else
            {
                Trans_TicketDTO objcomment = new Trans_TicketDTO { TaskID = Convert.ToInt32(Tid), AttachmentName = null, AttachmentPath = null, CreatedBy = User.Identity.GetUserId(), CreatedDate = DateTime.Now, Comments = CommentText };
                CustomResponse objcreatedocres = APICalls.Post("TaskTrasactionsAPI/Post", objcomment);

                if (objcreatedocres.Status == CustomResponseStatus.Successful)
                {
                    return RedirectToRoute("EditTicketRoute", new { ticketid = Tid });
                }
            }
            return RedirectToRoute("EditTicketRoute", new { ticketid = Tid });

        }

        public FileResult DownloadAttachment(string filepath)
        {
            string filefullpath = System.Web.Hosting.HostingEnvironment.MapPath(System.Configuration.ConfigurationManager.AppSettings["DocumentsPath"]) + filepath;
            string contentType = string.Empty;

            if (filepath.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }

            else if (filepath.Contains(".docx"))
            {
                contentType = "application/docx";
            }
            else if (filepath.Contains(".png"))
            {
                contentType = "image/png";
            }
            else if (filepath.Contains(".jpeg") || filepath.Contains(".jpg"))
            {
                contentType = "image/jpeg";
            }
            else if (filepath.Contains(".txt"))
            {
                contentType = "text/plain";
            }

            return File(filefullpath, contentType, filepath);
        }

        public ActionResult UpdateTaskStatus(int taskid, int status)
        {

            
            CustomResponse res = APICalls.Put("TaskTrasactionsAPI/Put?ticketid=" + taskid + "&status=" + status + "&updatedby="+User.Identity.GetUserId(), null);
            if (res.Status == CustomResponseStatus.Successful)
            {
                return RedirectToRoute("EditTicketRoute", new { ticketid = taskid });
            }
            ViewBag.Message = "Failed to Create Task";
            return RedirectToRoute("EditTicketRoute", new { ticketid = taskid });
        }
    }
}
//}     