using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
    public static class TaskRepository
    {


        public static dynamic GetAllTicketsByProjectID(string userid, int task_status, int Task_Type, int? ProjectID = 0, int priority=0)
        {
            CustomResponse objres = new CustomResponse();
            try
            {


                List<TaskDTO> objtasks = new List<TaskDTO>();

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {

                    if (ProjectID == 0)
                    {
                        if (UserRepository.GetUserRole(userid) == "Administrator")
                        {
                            objtasks = (from task in objcontext.Mst_Task
                                        join user in objcontext.AspNetUsers on task.AssignedTo equals user.Id
                                        join user1 in objcontext.AspNetUsers on task.RefereedTo equals user1.Id
                                        join tasktype in objcontext.Mst_TaskType on task.TypeID equals tasktype.ID
                                        join taskstatus in objcontext.Mst_TaskStatus on task.Task_Status equals taskstatus.ID
                                        where (0==task_status || taskstatus.ID==task_status ) &&
                                        (0 == Task_Type || tasktype.ID == Task_Type) &&

                                        (0==priority || task.PriorityID==priority)

                                        select new TaskDTO { Title = task.Title, ID = task.ID,Task_Status = task.Task_Status, Description = task.Description, TaskDisplayName = task.TaskDisplayName, AssignedToName = user.FirstName, RefereedToName = user1.FirstName, PriorityName = task.Mst_TaskPriority.PriorityName, TaskTypeName = task.Mst_TaskType.Type,Task_Type_Name=task.Mst_TaskType.Type,Task_Status_Name=task.Mst_TaskStatus.Task_Status, AssigndedDate = task.AssigndedDate }).OrderByDescending(x=>x.ID).Take(5000).ToList();
                        }
                        else if (UserRepository.GetUserRole(userid) == "Client")
                        {
                            objtasks = (from task in objcontext.Mst_Task
                                        join user in objcontext.AspNetUsers on task.AssignedTo equals user.Id
                                        join user1 in objcontext.AspNetUsers on task.RefereedTo equals user1.Id 
                                        join proj in objcontext.Mst_Project on task.ProjectID equals proj.ID

                                        join tasktype in objcontext.Mst_TaskType on task.TypeID equals tasktype.ID
                                        join taskstatus in objcontext.Mst_TaskStatus on task.Task_Status equals taskstatus.ID

                                        where proj.ClientID == userid && (0 == task_status || taskstatus.ID == task_status) &&
                                        (0 == Task_Type || tasktype.ID == Task_Type)
                                        &&

                                        (0 == priority || task.PriorityID == priority)
                                        select new TaskDTO { Title = task.Title, ID = task.ID, Task_Status = task.Task_Status, Description = task.Description, TaskDisplayName = task.TaskDisplayName, AssignedToName = user.FirstName, RefereedToName = user1.FirstName, PriorityName = task.Mst_TaskPriority.PriorityName, TaskTypeName = task.Mst_TaskType.Type, Task_Type_Name = task.Mst_TaskType.Type, Task_Status_Name = task.Mst_TaskStatus.Task_Status, AssigndedDate = task.AssigndedDate }).OrderByDescending(x => x.ID).Take(5000).ToList();
                        
                        }
                        else if (UserRepository.GetUserRole(userid) == "Developer")
                        {
                            objtasks = (from task in objcontext.Mst_Task
                                        join project in objcontext.Mst_ProjectUsers on task.ProjectID equals project.ProjectID 
                                         join user in objcontext.AspNetUsers on task.AssignedTo equals user.Id 
                                         join user1 in objcontext.AspNetUsers on task.RefereedTo equals user1.Id

                                        join tasktype in objcontext.Mst_TaskType on task.TypeID equals tasktype.ID
                                        join taskstatus in objcontext.Mst_TaskStatus on task.Task_Status equals taskstatus.ID
                                        where project.UserID == userid

                                        && (0 == task_status || taskstatus.ID == task_status) &&
                                        (0 == Task_Type || tasktype.ID == Task_Type)
                                        &&

                                        (0 == priority || task.PriorityID == priority)

                                        select new TaskDTO { Title = task.Title, ID = task.ID, Task_Status = task.Task_Status, Description = task.Description, TaskDisplayName = task.TaskDisplayName, AssignedToName = user.FirstName, RefereedToName = user1.FirstName, PriorityName = task.Mst_TaskPriority.PriorityName, TaskTypeName = task.Mst_TaskType.Type, Task_Type_Name = task.Mst_TaskType.Type, Task_Status_Name = task.Mst_TaskStatus.Task_Status, AssigndedDate = task.AssigndedDate }).OrderByDescending(x => x.ID).Take(5000).ToList();
                        }

                         }

                    else
                    {
                        objtasks = (from task in objcontext.Mst_Task
                                    join tasktype in objcontext.Mst_TaskType on task.TypeID equals tasktype.ID
                                    join taskstatus in objcontext.Mst_TaskStatus on task.Task_Status equals taskstatus.ID


                                    where task.ProjectID == ProjectID && (0 == task_status || taskstatus.ID == task_status) &&
                                        (0 == Task_Type || tasktype.ID == Task_Type)
                                        &&

                                        (0 == priority || task.PriorityID == priority)

                                    select new TaskDTO { Title = task.Title, ID = task.ID, Task_Status = task.Task_Status, Description = task.Description, TaskDisplayName = task.TaskDisplayName, AssignedToName = task.AspNetUser.UserName, RefereedToName = task.AspNetUser.UserName, PriorityName = task.Mst_TaskPriority.PriorityName, TaskTypeName = task.Mst_TaskType.Type, Task_Type_Name = task.Mst_TaskType.Type, Task_Status_Name = task.Mst_TaskStatus.Task_Status, AssigndedDate = task.AssigndedDate }).OrderByDescending(x => x.ID).Take(5000).ToList();
                    }

                    objres.Message = "Success";
                    objres.Response = objtasks;
                    objres.Status = CustomResponseStatus.Successful;
                    return objres;

                }
            }
            catch (Exception ex)
            {
                objres.Message = ex.Message;
                objres.Response = null;
                objres.Status = CustomResponseStatus.UnSuccessful;
                return objres;
            }

        }

        public static dynamic AddNewTicket(Mst_Task objnewTask)
        {

            CustomResponse objres = new CustomResponse();
            try
            {

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
                    // get short name of the project and get the max count of the tickets and use it as display name 
                    string shortname = objcontext.Mst_Project.Where(x => x.ID == objnewTask.ProjectID).FirstOrDefault().ShortName;
                    int maxcount = objcontext.Mst_Task.Where(x => x.ProjectID == objnewTask.ProjectID).Count();
                    objnewTask.TaskDisplayName = shortname + '-' + (maxcount + 1);
                    objcontext.Mst_Task.Add(objnewTask);
                    objcontext.SaveChanges();
                    Trans_Ticket objtrans = new Trans_Ticket { TaskID = objnewTask.ID, N_Title = objnewTask.Title, N_Description = objnewTask.Description, DisplayName = objnewTask.TaskDisplayName, CreatedDate = DateTime.Now, CreatedBy = objnewTask.CreatedBy };
                    objcontext.Trans_Ticket.Add(objtrans);
                    objcontext.SaveChanges();
                    objres.Message = "Success";
                    objres.Response = objnewTask;
                    objres.Status = CustomResponseStatus.Successful;
                    return objres;
                }
            }
            catch (Exception ex)
            {
                objres.Message = ex.Message;
                objres.Response = null;
                objres.Status = CustomResponseStatus.UnSuccessful;
                return objres;
            }
        }


        public static dynamic GetTicketDetails(int ticketid)
        {
            CustomResponse objres = new CustomResponse();
            try
            {


                TaskDTO  objtasks = new  TaskDTO();

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
  
                        objtasks = (from task in objcontext.Mst_Task 
                                    join task_type in objcontext.Mst_TaskType on task.TypeID equals task_type.ID
                                    join task_status in objcontext.Mst_TaskStatus on task.Task_Status equals task_status.ID
                                    where task.ID == ticketid
                                    select new TaskDTO { Title = task.Title, ID = task.ID, Task_Status = task.Task_Status, Description = task.Description, TaskDisplayName = task.TaskDisplayName, AssignedTo = task.AssignedTo, RefereedTo = task.RefereedTo, PriorityID = task.PriorityID,ProjectID=task.ProjectID, TaskTypeName = task.Mst_TaskType.Type,TypeID=task.TypeID,DueDate=task.DueDate, AssigndedDate = task.AssigndedDate,Task_Status_Name=task_status.Task_Status,Task_Type_Name=task_type.Type, Attachment1_Name=task.Attachment1_Name, Attachment1_Path=task.Attachment1_Path, Attachment2_Name=task.Attachment2_Name, Attachment2_Path=task.Attachment2_Path, Attachment3_Name=task.Attachment3_Name, Attachment3_Path=task.Attachment3_Path }).First();
                        objtasks.Comments = (from comment in objcontext.Trans_Ticket
                                             where comment.TaskID == ticketid && comment.Comments !=null
                                             select
                                                             new CommentsDTO {
                                                                 CreatedBy=comment.AspNetUser.UserName,
                                                                 CreatedTime=comment.CreatedDate,
                                                                 FileName=comment.AttachmentName,
                                                                 FilePath=comment.AttachmentPath,
                                                                 Comment=comment.Comments
                                                                 
                                                             }).ToList();
                          
                         
                    objres.Message = "Success";
                    objres.Response = objtasks;
                    objres.Status = CustomResponseStatus.Successful;
                    return objres;

                }
            }
            catch (Exception ex)
            {
                objres.Message = ex.Message;
                objres.Response = null;
                objres.Status = CustomResponseStatus.UnSuccessful;
                return objres;
            }
        }

        public static dynamic GetUserRecentTickets(string userid)
        {

            CustomResponse objres = new CustomResponse();
            try
            {


                List<TaskDTO> objtasks = new List<TaskDTO>();

                if (UserRepository.GetUserRole(userid) == "Developer")
                {
                    using (var objcontext = new Db_Zon_Test_techsupportEntities())
                    {

                        objtasks = (from task in objcontext.Mst_Task
                                    join project in objcontext.Mst_ProjectUsers on task.ProjectID equals project.ProjectID
                                    join user in objcontext.AspNetUsers on task.AssignedTo equals user.Id
                                    join user1 in objcontext.AspNetUsers on task.RefereedTo equals user1.Id
                                    where project.UserID == userid && task.Task_Status!= 2 

                                    select new TaskDTO { Title = task.Title, ID = task.ID, Task_Status = task.Task_Status, Description = task.Description, TaskDisplayName = task.TaskDisplayName, AssignedToName = user.FirstName, RefereedToName = user1.FirstName, PriorityName = task.Mst_TaskPriority.PriorityName, TaskTypeName = task.Mst_TaskType.Type, AssigndedDate = task.AssigndedDate }).ToList().OrderBy(x => x.AssigndedDate).Take(15).ToList();


                        objres.Message = "Success";
                        objres.Response = objtasks;
                        objres.Status = CustomResponseStatus.Successful;
                        return objres;

                    }
                }
                else if (UserRepository.GetUserRole(userid) == "Client")
                {
                    using (var objcontext = new Db_Zon_Test_techsupportEntities())
                    {

                        objtasks = (from task in objcontext.Mst_Task
                                    join project in objcontext.Mst_Project on task.ProjectID equals project.ID
                                    join user in objcontext.AspNetUsers on task.AssignedTo equals user.Id
                                    join user1 in objcontext.AspNetUsers on task.RefereedTo equals user1.Id
                                    where project.ClientID == userid

                                    select new TaskDTO { Title = task.Title, ID = task.ID, Task_Status = task.Task_Status, Description = task.Description, TaskDisplayName = task.TaskDisplayName, AssignedToName = user.FirstName, RefereedToName = user1.FirstName, PriorityName = task.Mst_TaskPriority.PriorityName, TaskTypeName = task.Mst_TaskType.Type, AssigndedDate = task.AssigndedDate }).ToList().OrderBy(x => x.AssigndedDate).Take(15).ToList();


                        objres.Message = "Success";
                        objres.Response = objtasks;
                        objres.Status = CustomResponseStatus.Successful;
                        return objres;

                    }
                }
                else
                {
                    objres.Message = "Invalid Role";
                    objres.Response = null;
                    objres.Status = CustomResponseStatus.UnSuccessful;
                    return objres;
                }
            }
            catch (Exception ex)
            {
                objres.Message = ex.Message;
                objres.Response = null;
                objres.Status = CustomResponseStatus.UnSuccessful;
                return objres;
            }

        }
        public static dynamic UpdateTicket(Mst_Task objupdateTask)
        {

            CustomResponse objres = new CustomResponse();
            try
            {

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
                    // get short name of the project and get the max count of the tickets and use it as display name 
                   int tid=objupdateTask.ID;;
                   Mst_Task objtask = objcontext.Mst_Task.Where(x => x.ID == tid).FirstOrDefault();

                   objtask.Title = objupdateTask.Title;
                   objtask.Description = objupdateTask.Description;
                   objtask.AssigndedDate = objupdateTask.AssigndedDate;
                   objtask.DueDate = objupdateTask.DueDate;
                   objtask.AssignedTo = objupdateTask.AssignedTo;
                   objtask.RefereedTo = objupdateTask.RefereedTo;
                   objtask.Task_Status = objupdateTask.Task_Status;
                   objtask.TypeID = objupdateTask.TypeID;
                   objtask.ProjectID = objupdateTask.ProjectID;
                   objtask.PriorityID = objupdateTask.PriorityID;
                    objcontext.SaveChanges();
                    objres.Message = "Success";
                    objres.Response = objupdateTask;
                    objres.Status = CustomResponseStatus.Successful;
                    return objres;
                }
            }
            catch (Exception ex)
            {
                objres.Message = ex.Message;
                objres.Response = null;
                objres.Status = CustomResponseStatus.UnSuccessful;
                return objres;
            }
        }


        public static string GetTaskNameByID(int? task_Id)
        {
            using (var objconext = new Db_Zon_Test_techsupportEntities())
            {
                try
                {
                  return  objconext.Mst_TaskStatus.Where(x => x.ID == task_Id).FirstOrDefault().Task_Status;
                }
                catch (Exception ex)

                {
                   return ex.Message.ToString();
                }
            
            
            }
        }


    }
}
