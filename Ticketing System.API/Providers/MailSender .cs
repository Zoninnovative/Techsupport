using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using Ticketing_System.Core;
using Ticketing_System.Repositoy;

namespace Ticketing_System.API.Providers
{
    public class MailSender
    {

        internal static void ProjectCreationMail(Mst_Project objproject, string ToMail)
        {


            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
            Msg.Subject = "Zon Ticketing System - New Ticket Information";

            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Email Templates/EmailTemplate.html"));//EmailTemplate1.html
            string readFile = reader.ReadToEnd();
            string StrContent = "";
            StrContent = readFile;

            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=100%>");
            sb.Append("<tr><td>Project  Name<center></td><td>" + objproject.Name + "</td></tr>");
            sb.Append("<tr><td>Project Description<center></td><td><center>" + objproject.Description + "</td></tr>");
            sb.Append("<tr><td>Project Manager<center></td><td><center>" + UserRepository.GetFnamebyUid(objproject.PManagerID) + "</td></tr>");
            sb.Append("<tr><td>Client <center></td><td><center>" + UserRepository.GetFnamebyUid(objproject.ClientID) + "</td></tr>");
            sb.Append("<tr><td>Project Manager<center></td><td><center>" + UserRepository.GetFnamebyUid(objproject.CreatedBy) + "</td></tr>");
            sb.Append("<tr><td>Project Manager<center></td><td><center>" + objproject.StartDate + "</td></tr>");
            sb.Append("<tr><td>Project Manager<center></td><td><center>" + objproject.ProposedEndDate + "</td></tr>");
            sb.Append("</table>");
            StrContent = StrContent.Replace("$$TemplateBody$$", "<span>  A New Task is created in the Project  <b>" + objproject.Name + "</b> by <b>" + UserRepository.GetFnamebyUid(objproject.CreatedBy) + "</b>.\n Please find the Details below. </span></br><div>" + sb.ToString());
            Msg.To.Add(ToMail.ToString());
            StrContent = StrContent.Replace("$$User$$", ToMail.ToString());
            Msg.Body = StrContent.ToString();
            Msg.IsBodyHtml = true;
            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["MailServer"];
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            object mailstate = Msg;
            smtp.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["MailPort"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSLEnabled"].ToString());
            smtp.SendAsync(Msg, mailstate);
        }



        static void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MailMessage mail = e.UserState as MailMessage;

            if (!e.Cancelled && e.Error != null)
            {
                // message.Text = "Mail sent successfully";
            }
        }



        internal static void TaskStatusCreate_UpdationMail(string toname, string subject, string body, string ToMail)
        {
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
            Msg.Subject = subject;

            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Email Templates/EmailTemplate.html"));
            string readFile = reader.ReadToEnd();
            string StrContent = "";
            StrContent = readFile;

            StrContent = StrContent.Replace("$$TemplateBody$$", body);

            Msg.To.Add(ToMail);
            StrContent = StrContent.Replace("$$User$$", toname);
            Msg.Body = StrContent.ToString();
            Msg.IsBodyHtml = true;
            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["MailServer"];
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            object mailstate = Msg;
            smtp.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["MailPort"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSLEnabled"].ToString());
            smtp.SendAsync(Msg, mailstate);
        }

    }
}