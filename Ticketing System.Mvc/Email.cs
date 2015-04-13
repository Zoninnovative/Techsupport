using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Ticketing_System.Mvc
{
    public static class Email
    {

        public static void SendEmail(string body,string filepath,string toemail,string subject)
        {
            try {
                MailMessage Msg = new MailMessage();
                Msg.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
                Msg.Subject = subject;
                Msg.To.Add(toemail);
                StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(filepath));
                string readFile = reader.ReadToEnd();
                string StrContent = "";
                StrContent = readFile;
                StrContent = StrContent.Replace("$$TemplateBody$$",body);
                StrContent = StrContent.Replace("$$User$$",toemail);
                Msg.Body = body;
                Msg.IsBodyHtml = true;

                // your remote SMTP server IP.
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["MailServer"];
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["MailPort"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSLEnabled"].ToString());
                smtp.Send(Msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}