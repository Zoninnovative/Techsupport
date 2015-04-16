using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
   public static class ProjectReportRepository
    {


       // Based on userrole we need to generate report data , 


       public static dynamic GetReportData(int projectid, int tasktype, int taskstatus, DateTime? fromdate, DateTime? todate)
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
                                    where (0 == projectid || t.ProjectID == projectid) && (0 == tasktype || t.TypeID == tasktype) && (0 == taskstatus || t.Task_Status == taskstatus)
                                    && EntityFunctions.TruncateTime(t.AssigndedDate) >= EntityFunctions.TruncateTime(fromdate)
                                     && EntityFunctions.TruncateTime(t.AssigndedDate) <= EntityFunctions.TruncateTime(todate)
                                    select new ReportData {
                                        ProjectName = p.Name,
                                        TicketDisplayName = t.TaskDisplayName,
                                        AssignedDate = t.AssigndedDate,
                                        AssignedTo = u.FirstName,
                                        RefereerTo = u1.FirstName,
                                    TaskStatus=ts.Task_Status,TicketType=tt.Type,TicketID=t.ID

                                    }).OrderByDescending(x => x.TicketID).Take(5000).ToList();
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



       public static List<ReportData> GetReportDataAsync(int projectid, int tasktype, int taskstatus, DateTime? fromdate, DateTime? todate)
       {

           List<ReportData> objreportdata = new List<ReportData>();

            
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
                                    where (0 == projectid || t.ProjectID == projectid) && (0 == tasktype || t.TypeID == tasktype) && (0 == taskstatus || t.Task_Status == taskstatus)
                                    && EntityFunctions.TruncateTime(t.AssigndedDate) >= EntityFunctions.TruncateTime(fromdate)
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

                                    }).OrderByDescending(x => x.TicketID).Take(5000).ToList();

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
