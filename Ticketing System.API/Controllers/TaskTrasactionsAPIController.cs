using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Ticketing_System.API.Providers;
using Ticketing_System.Core;
using Ticketing_System.Repositoy;

namespace Ticketing_System.API.Controllers
{
    public class TaskTrasactionsAPIController : ApiController
    {

        public dynamic Post(Trans_TicketDTO objtasktransactionDTO)
        {
            Trans_Ticket objtasktransaction=new Trans_Ticket{TaskID=objtasktransactionDTO.TaskID,Comments=objtasktransactionDTO.Comments,AttachmentName=objtasktransactionDTO.AttachmentName,AttachmentPath=objtasktransactionDTO.AttachmentPath,CreatedBy=objtasktransactionDTO.CreatedBy,CreatedDate=objtasktransactionDTO.CreatedDate};
            return TaskTransactionRepository.AddComment(objtasktransaction);
        }

        public dynamic Put(int ticketid, int status,string updatedby)
        {

            // First get existing tiket detals from the db 

            CustomResponse objres = TaskRepository.GetTicketDetails(ticketid);
            CustomResponse ApiResponse = TaskTransactionRepository.UpdateTaskStatus(ticketid, status, updatedby);
            if (objres.Status == CustomResponseStatus.Successful)
            {
                //compare status fields
                string oldstatus = TaskRepository.GetTaskNameByID(((TaskDTO)objres.Response).Task_Status);
                string newstatus = TaskRepository.GetTaskNameByID(status);

                
                List<string> ObjToAddresses = new List<string>();
                List<string> ObjFilteredToAddresses = new List<string>();

                ObjToAddresses.AddRange(ProjectRepository.GetProjectUsers(TaskTransactionRepository.GetProjectidByTaskid(ticketid)));
                ObjFilteredToAddresses = ObjToAddresses.Distinct().ToList();
                ObjFilteredToAddresses = UserRepository.GetEmailAddressesByUserIds(ObjFilteredToAddresses);

                string fname = UserRepository.GetFnamebyUid(updatedby);
                string ticket = UserRepository.GetTaskNameByID(ticketid);
                StringBuilder sb = new StringBuilder("<b> {0} </b> has changed the status of  Ticket <b> {1} </b>  from <b> {2} </b> to <b> {3} </b>");
                sb.Replace("{0}", fname);
                sb.Replace("{1}", ticket);
                sb.Replace("{2}", oldstatus);
                sb.Replace("{3}", newstatus);
                string subject = "Task Update Information";


                foreach (var toMail in ObjFilteredToAddresses)
                {
                    MailSender.TaskStatusCreate_UpdationMail(fname, subject,sb.ToString(), toMail);
                }

               
            }
            return ApiResponse;
        }
    }
}
