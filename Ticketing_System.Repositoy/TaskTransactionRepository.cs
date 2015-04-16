using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
   public static class TaskTransactionRepository
    {

        public static dynamic AddComment(Trans_Ticket objtasktransaction)
        {

            CustomResponse objres = new CustomResponse();
            try
            {

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
                    objcontext.Trans_Ticket.Add(objtasktransaction);
                    objcontext.SaveChanges();
                    objres.Message = "Success";
                    objres.Response = objtasktransaction;
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


        public static dynamic UpdateTaskStatus(int taskid, int status, string updatedby)
        {

            CustomResponse objres = new CustomResponse();
            try
            {
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
                    Mst_Task objtask = objcontext.Mst_Task.Where(x => x.ID == taskid).FirstOrDefault();
                    Trans_Ticket objtasktrans = new Trans_Ticket();
                    objtasktrans.TaskID = taskid;
                    objtasktrans.CreatedBy = updatedby;
                    objtasktrans.CreatedDate = DateTime.Now;
                    objtasktrans.O_Task_Status = objtask.Task_Status;
                    objtasktrans.N_Task_Statuus = status;
                    objcontext.Trans_Ticket.Add(objtasktrans);
                    objtask.Task_Status = status;
                    objtask.UpdatedBy = updatedby;
                    objcontext.SaveChanges();


                    objres.Message = "Success";
                    objres.Response = objtask;
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




        public static int GetProjectidByTaskid(int Tid)
        {
            using (var objts = new Db_Zon_Test_techsupportEntities())
            {

                return objts.Mst_Task.Where(a => a.ID == Tid).FirstOrDefault().ProjectID;
            }



        }
    }
}
