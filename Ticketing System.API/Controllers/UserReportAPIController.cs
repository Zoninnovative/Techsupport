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
    public class UserReportAPIController : ApiController
    {

        //get User level report
        public dynamic Post(ReportDTO objreport)
        {
            return UserReportRepository.GetReportData(objreport.UserID, objreport.TaskTypeID, objreport.TaskStatusID, objreport.FromDate, objreport.ToDate);
        }
        public dynamic Put(ReportDTO objreport)
        {
            CustomResponse objresult = new CustomResponse();
            objresult.Response = null;
            try
            {
                // Step1 : get relavent data from the database and prepare mail body and get clietn id. 
                // Step2 : send mail to the client of the project

                // return ProjectReportRepository.GetReportData(objreport.ProjectID, objreport.TaskTypeID, objreport.TaskStatusID, objreport.FromDate, objreport.ToDate);

                List<ReportData> objmaildata = UserReportRepository.GetReportDataAsync(objreport.UserID  , objreport.TaskTypeID, objreport.TaskStatusID, objreport.FromDate, objreport.ToDate);
                  
                    
                //prepare mail body 

                StringBuilder sbmailbody = new StringBuilder();

                sbmailbody.Append("<table width=100%><tr><th>ProjectName</th><th>TicketID</th><th>AssignedDate</th><th>Assign To</th><th>Task Status</th><th>Task Type</th></tr>");

                foreach (ReportData objreport1 in objmaildata)

                    sbmailbody.Append("<tr><td>" + objreport1.ProjectName  + "</td><td>" + objreport1.TicketDisplayName + "</td><td>" + objreport1.AssignedDate + "</td><td>" + objreport1.AssignedTo + "</td><td>" + objreport1.TaskStatus + "</td><td>" + objreport1.TicketType + "</td></tr>");

                sbmailbody.Append("</table>");

               
                List<String> uids = new List<string>();
                uids.Add(objreport.UserID);
                string subject = "User Activity Report from " + objreport.FromDate + "-" + objreport.ToDate;
                List<string> mails = UserRepository.GetEmailAddressesByUserIds(uids);

                foreach (string toMail in mails)
                {

                    MailSender.TaskStatusCreate_UpdationMail(UserReportRepository.GetFnameByEmail(toMail), subject, sbmailbody.ToString(), toMail);

                }
                objresult.Status = CustomResponseStatus.Successful;
                objresult.Message = "Email Sent Successfully";


            }
            catch (Exception ex)
            {
                objresult.Status = CustomResponseStatus.Successful;
                objresult.Message = "Failed to send email";

            }
            return objresult;

        }
    }
}
