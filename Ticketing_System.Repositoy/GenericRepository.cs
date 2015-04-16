using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
   public static class GenericRepository
    {


       public static dynamic GetPriorities()
       {
           CustomResponse objresponse = new CustomResponse();
           try
           {
               using (var objcontext = new Db_Zon_Test_techsupportEntities())
               {
                   objresponse.Status = CustomResponseStatus.Successful;
                   objresponse.Response = (from tasktype in objcontext.Mst_TaskPriority select new TypeAndPriorityDTO { Name = tasktype.PriorityName, ID = tasktype.ID }).ToList();
                   objresponse.Message = "Success";
               }
           }
           catch (Exception ex)
           {
               objresponse.Status = CustomResponseStatus.Exception;
               objresponse.Response =null;
               objresponse.Message =ex.Message;
           }
           return objresponse;
           
       }
       public static dynamic GetTaskType()
       {

           CustomResponse objresponse = new CustomResponse();
           try
           {
               using (var objcontext = new Db_Zon_Test_techsupportEntities())
               {
                   objresponse.Status = CustomResponseStatus.Successful;
                   objresponse.Response = (from tasktype in objcontext.Mst_TaskType select new TypeAndPriorityDTO {Name=tasktype.Type,ID=tasktype.ID }).ToList();
                   objresponse.Message = "Success";
               }
           }
           catch (Exception ex)
           {
               objresponse.Status = CustomResponseStatus.Exception;
               objresponse.Response = null;
               objresponse.Message = ex.Message;
           }
           return objresponse;
       }

       public static dynamic GetTaskStatus()
       {

           CustomResponse objresponse = new CustomResponse();
           try
           {
               using (var objcontext = new Db_Zon_Test_techsupportEntities())
               {
                   objresponse.Status = CustomResponseStatus.Successful;
                   objresponse.Response = (from status in objcontext.Mst_TaskStatus select new TypeAndPriorityDTO { Name = status.Task_Status, ID = status.ID }).ToList();
                   objresponse.Message = "Success";
               }
           }
           catch (Exception ex)
           {
               objresponse.Status = CustomResponseStatus.Exception;
               objresponse.Response = null;
               objresponse.Message = ex.Message;
           }
           return objresponse;
       }


       public static dynamic GetUserIdByEmail(string email)
       {
           CustomResponse objresponse = new CustomResponse();
           try
           {
               using (var objcontext = new Db_Zon_Test_techsupportEntities())
               {
                   objresponse.Status = CustomResponseStatus.Successful;
                   objresponse.Response= objcontext.AspNetUsers.Where(x => x.Email == email).FirstOrDefault().Id;
                   objresponse.Message = "Success";
               }
           
            }
           catch (Exception ex)
           {
               objresponse.Status = CustomResponseStatus.Exception;
               objresponse.Response = null;
               objresponse.Message = ex.Message;
           }
           return objresponse;
       
       }






       public static string GetPrioritynameByPid(int? pid)
       {
           
           using (var objts = new Db_Zon_Test_techsupportEntities())
               return objts.Mst_TaskPriority.Where(a => a.ID == pid).FirstOrDefault().PriorityName;

       }

       public static string GetTasknameByPid(int? pid)
       {
          
           using (var objts = new Db_Zon_Test_techsupportEntities())
               return objts.Mst_TaskType.Where(a => a.ID == pid).FirstOrDefault().Type;

       }



       public static string GetTaskStausByPid(int? pid)
       {
          
           using (var objts = new Db_Zon_Test_techsupportEntities())
               return objts.Mst_TaskStatus.Where(a => a.ID == pid).FirstOrDefault().Task_Status;

       }
    }
}
