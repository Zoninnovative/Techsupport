using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ticketing_System.API.Providers;
using Ticketing_System.Core;
using Ticketing_System.Repositoy;

namespace Ticketing_System.API.Controllers
{
    public class TasksAPIController : ApiController
    {
        public dynamic Get(int IDType, string userid, int projectID, int task_status, int Task_Type,int priority)
        {
            if (IDType == 1)
                return TaskRepository.GetAllTicketsByProjectID(userid, task_status, Task_Type, projectID, priority);
            else if (IDType == 2)
                return TaskRepository.GetTicketDetails(projectID);

            else if (IDType == 3)

                return TaskRepository.GetUserRecentTickets(userid);
            else
                return TaskRepository.GetAllTicketsByProjectID(userid, task_status, Task_Type, projectID);

        }
        
        [HttpPost]
        public dynamic Post(TaskDTO objtaskdto)
        {
            Mst_Task objtask = new Mst_Task { Title = objtaskdto.Title, Task_Status = objtaskdto.Task_Status, Description = objtaskdto.Description, DueDate = objtaskdto.DueDate, PriorityID = objtaskdto.PriorityID, TypeID = objtaskdto.TypeID, RefereedTo = objtaskdto.RefereedTo, AssignedTo = objtaskdto.AssignedTo, CreatedBy = objtaskdto.CreatedBy, AssigndedDate = objtaskdto.AssigndedDate, ProjectID = objtaskdto.ProjectID, Attachment1_Name = objtaskdto.Attachment1_Name, Attachment1_Path = objtaskdto.Attachment1_Path, Attachment2_Name = objtaskdto.Attachment2_Name, Attachment2_Path = objtaskdto.Attachment2_Path, Attachment3_Name = objtaskdto.Attachment3_Name, Attachment3_Path = objtaskdto.Attachment3_Path };
            CustomResponse res = TaskRepository.AddNewTicket(objtask);
           if (res.Status == CustomResponseStatus.Successful)
           {
               List<UserDTO> users = UserReportRepository.GetUserProjectDetailsByProejectid(objtaskdto.ProjectID);
               users.Add(new UserDTO { Email = objtaskdto.AssignedTo, RoleName = "3" });
               string createdbyemail = UserReportRepository.GetEmailByUid(objtaskdto.CreatedBy);

               List<string> ObjToAddresses = new List<string>();
               List<string> ObjFilteredToAddresses = new List<string>();
               ObjToAddresses.Add(objtaskdto.AssignedTo);
               ObjToAddresses.Add(objtaskdto.RefereedTo);
               ObjToAddresses.Add(objtaskdto.CreatedBy);
               // Get All Stackholders related to project 
               ObjToAddresses.AddRange(ProjectRepository.GetProjectUsers(objtaskdto.ProjectID));
               ObjFilteredToAddresses = ObjToAddresses.Distinct().ToList();
               ObjFilteredToAddresses = UserRepository.GetEmailAddressesByUserIds(ObjFilteredToAddresses);
               string subject="Zon Ticketing System - New Ticket Information";

               StringBuilder sb = new StringBuilder();
               sb.Append("<table width=100% align='center'>");
               sb.Append("<tr><td>Task ID</td><td>" + objtask.ID + "</td></tr>");
               sb.Append("<tr><td>Task Name<center></td><td>" + objtask.Title  + "</td></tr>");
               sb.Append("<tr><td>Task Description<center></td><td><center>" + objtask.Description  + "</td></tr>");

               sb.Append("<tr><td>Assigned To<center></td><td><center>" + UserRepository.GetFnamebyUid(objtask.AssignedTo) + "</td></tr>");
               sb.Append("<tr><td>Reffer To<center></td><td><center>" + UserRepository.GetFnamebyUid(objtask.RefereedTo) + "</td></tr>");
               sb.Append("</table>");

               foreach (var toMail in ObjFilteredToAddresses)
               {

                   MailSender.TaskStatusCreate_UpdationMail(UserReportRepository.GetFnameByEmail(toMail), subject, sb.ToString(), toMail);
                  
               }



           }
            return res;
        }

        [HttpPut]
        public dynamic Put(TaskDTO objtaskdto)
        {
            TaskDTO Oldtask = (TaskDTO)TaskRepository.GetTicketDetails(objtaskdto.ID).Response;
            Mst_Task objtask = new Mst_Task { ID = objtaskdto.ID, CreatedBy = objtaskdto.CreatedBy, UpdatedBy = objtaskdto.UpdatedBy, Title = objtaskdto.Title, Task_Status = objtaskdto.Task_Status, Description = objtaskdto.Description, DueDate = objtaskdto.DueDate, PriorityID = objtaskdto.PriorityID, TypeID = objtaskdto.TypeID, RefereedTo = objtaskdto.RefereedTo, AssignedTo = objtaskdto.AssignedTo, AssigndedDate = objtaskdto.AssigndedDate, ProjectID = objtaskdto.ProjectID };
            CustomResponse res = TaskRepository.UpdateTicket(objtask);
           
           
            List<string> ObjToAddresses = new List<string>();
            List<string> ObjFilteredToAddresses = new List<string>();

            if (res.Status  == CustomResponseStatus.Successful)
            {

                ObjToAddresses.Add(objtaskdto.CreatedBy);
                ObjToAddresses.Add(objtaskdto.RefereedTo);
                ObjToAddresses.Add(objtaskdto.AssignedTo);
                // Get All Stackholders related to project 
                ObjToAddresses.AddRange(ProjectRepository.GetProjectUsers(objtaskdto.ProjectID));
                ObjFilteredToAddresses = ObjToAddresses.Distinct().ToList();
                ObjFilteredToAddresses = UserRepository.GetEmailAddressesByUserIds(ObjFilteredToAddresses);
                StringBuilder sb = new StringBuilder();
                sb.Append("<table>");
                sb.Append("<tr><td><center><b>  Updated Ticket Infromation</b></center> <td></tr>");
                sb.Append("<tr><td><center> Project Name </center> <td> <td><center>" + objtaskdto.ProjectID + "  </center> <td></tr>");
                sb.Append("<tr><td><center> Ticket ID </center> <td> <td><center>" + objtaskdto.ID + "  </center> <td></tr>");
                sb.Append("<tr><td><center> Ticket Titile  </center> <td> <td><center>" + objtaskdto.Title  + "  </center> <td></tr>");
                sb.Append("<tr><td><center> Ticket Description </center> <td> <td><center>" + objtaskdto.Description  + "  </center> <td></tr>");
                sb.Append("<tr><td><center> Ticket Start Date </center> <td> <td><center>" + objtaskdto.AssigndedDate  + "  </center> <td></tr>");
                sb.Append("<tr><td><center> Ticket Due Date </center> <td> <td><center>" + objtaskdto.DueDate  + "  </center> <td></tr>");
                if(objtaskdto.PriorityID!= Oldtask.PriorityID )
                    sb.Append("<tr><td><center> New Priority </center> <td> <td><center>" + GenericRepository.GetPrioritynameByPid(objtaskdto.PriorityID)  + "  </center> <td></tr>");
                if(objtaskdto.Task_Status!=Oldtask.Task_Status )
                sb.Append("<tr><td><center> New Task Status </center> <td> <td><center>" + GenericRepository.GetTaskStausByPid(objtaskdto.Task_Status) + "  </center> <td></tr>");
                if (objtaskdto.TaskTypeName!=Oldtask.TaskTypeName )
                sb.Append("<tr><td><center> New Task Type </center> <td> <td><center>" + GenericRepository.GetTasknameByPid(objtaskdto.TypeID)  + "  </center> <td></tr>");
                sb.Append("</table>");

                string subject = "Tech Support - Ticket Updtaes Information";

                 foreach (var toMail in ObjFilteredToAddresses)
                {
                    MailSender.TaskStatusCreate_UpdationMail(UserReportRepository.GetFnameByEmail(toMail), subject, sb.ToString(), toMail);
                }

                
            
            }
            
            return res;
        }

        

    }
}
