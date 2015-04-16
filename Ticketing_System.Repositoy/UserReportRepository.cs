using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
  public static  class UserReportRepository
    {


        // Based on userrole we need to generate report data , 


        public static dynamic GetReportData(string userid , int tasktype, int taskstatus, DateTime? fromdate, DateTime? todate)
        {

            List<ReportData> objreportdata = new List<ReportData>();

            CustomResponse objres = new CustomResponse();
            try
            {

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {


                    objreportdata = (from t in objcontext.Mst_Task

                                     join tt in objcontext.Mst_TaskType on t.TypeID equals tt.ID
                                     join ts in objcontext.Mst_TaskStatus on t.Task_Status equals ts.ID
                                     join p in objcontext.Mst_Project on t.ProjectID equals p.ID
                                     join u in objcontext.AspNetUsers on t.AssignedTo equals u.Id
                                     join u1 in objcontext.AspNetUsers on t.RefereedTo equals u1.Id
                                     where ( userid.Length==1 || t.AssignedTo == userid) &&
                                     (0 == tasktype || t.TypeID == tasktype) &&
                                     (0 == taskstatus || t.Task_Status == taskstatus)
                                     && System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(t.AssigndedDate) >= EntityFunctions.TruncateTime(fromdate)
                                      && EntityFunctions.TruncateTime(t.AssigndedDate) <= EntityFunctions.TruncateTime(todate)
                                     select new ReportData
                                     {
                                         ProjectName = p.Name,
                                         TicketDisplayName = t.TaskDisplayName,
                                         AssignedDate = t.AssigndedDate,
                                         AssignedTo = u.FirstName,
                                         RefereerTo = u1.FirstName,
                                         TaskStatus = ts.Task_Status,
                                         TicketType = tt.Type,
                                         TicketID = t.ID

                                     }).OrderByDescending(x=>x.TicketID).Take(5000).ToList();
                    objres.Message = "Success";
                    objres.Response = objreportdata;
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

        public static List<UserDTO> GetUserProjectDetailsByProejectid(int ProjectID)
        {
            var objcontext = new Db_Zon_Test_techsupportEntities();
            List<UserDTO> users = new List<UserDTO>();
            {
                string projectmanagerid = objcontext.Mst_Project.Where(x => x.ID == ProjectID).FirstOrDefault().PManagerID;
                string clientid = objcontext.Mst_Project.Where(x => x.ID == ProjectID).FirstOrDefault().PManagerID;

                users.Add(new UserDTO { Email = objcontext.AspNetUsers.Where(x => x.Id == projectmanagerid).FirstOrDefault().Email, RoleName = "2" });
                users.Add(new UserDTO { Email = objcontext.AspNetUsers.Where(x => x.Id == clientid).FirstOrDefault().Email, RoleName = "1" });
            }

            List<Mst_ProjectUsers> objuserproejct = objcontext.Mst_ProjectUsers.Where(x => x.ProjectID == ProjectID).ToList();
            foreach (Mst_ProjectUsers uproj in objuserproejct)
            {
                string uid = uproj.UserID;
                users.Add(new UserDTO { Email = objcontext.AspNetUsers.Where(x => x.Id == uid).FirstOrDefault().Email, RoleName = "5" });
            }

            return users;
        }
        public static string GetEmailByUid(string CreatedBy)
        {
            var objcontext = new Db_Zon_Test_techsupportEntities();
            return objcontext.AspNetUsers.Where(x => x.Id == CreatedBy).FirstOrDefault().Email;
        }

      public static string GetFnameByEmail(string email)
        {

          using(var objts=new Db_Zon_Test_techsupportEntities())
          {
              return objts.AspNetUsers.Where(a => a.Email==email ).FirstOrDefault().FirstName;
          }

        }



      public static List<ReportData> GetReportDataAsync(string userid, int tasktype, int taskstatus, DateTime? fromdate, DateTime? todate)
      {

          List<ReportData> objreportdata = new List<ReportData>();
         

          try
          {

              using (var objcontext = new Db_Zon_Test_techsupportEntities())
              {

                  objreportdata=(from user in objcontext.Mst_ProjectUsers
                                     join t in objcontext.Mst_Task on user.ProjectID equals t.ProjectID
                                     where (null == userid || user.UserID  == userid.ToString()) && (0 == tasktype || t.TypeID == tasktype) && (0 == taskstatus || t.Task_Status == taskstatus)
                                      && EntityFunctions.TruncateTime(t.AssigndedDate) >= EntityFunctions.TruncateTime(fromdate)
                                    && EntityFunctions.TruncateTime(t.AssigndedDate) <= EntityFunctions.TruncateTime(todate)

                                 select new ReportData
                                 {
                                     ProjectName = user.ProjectID.ToString(),
                                     TicketDisplayName = t.TaskDisplayName,
                                     AssignedDate = t.AssigndedDate,
                                     AssignedTo = t.AspNetUser.FirstName,
                                     RefereerTo = t.AspNetUser.FirstName,
                                     TaskStatus = t.Mst_TaskStatus.Task_Status,
                                     TicketType = t.Mst_TaskType.Type,
                                     TicketID = t.ID 

                                 }).OrderByDescending(x => x.TicketID).Take(20).ToList();


                 
                  return objreportdata;
              }
          }
          catch (Exception ex)
          {

              return null;
          }

      }
    }
}
