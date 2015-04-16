using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
  public static  class ClientDashboardRepository
    {

        // Get Assignedto me and my tasks 
        public static dynamic Get(string userid, int pageno)
        {


            int skipcount = 10 * pageno;
            ClientDashboardDTO ObjUserDashboardDTO = new ClientDashboardDTO();

            CustomResponse objres = new CustomResponse();
            try
            {
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {


                    List<Trans_TicketDTO> ActivityDTO = (from objtrans in objcontext.Trans_Ticket
                                                         join ticket in objcontext.Mst_Task on objtrans.TaskID equals ticket.ID
                                                         join project in objcontext.Mst_Project on ticket.ProjectID equals project.ID
                                                         join user2 in objcontext.AspNetUsers on ticket.CreatedBy equals user2.Id
                                                         where project.ClientID == userid
                                                         select new Trans_TicketDTO { CreatedBy = user2.FirstName, CreatedDate = objtrans.CreatedDate, TaskID = objtrans.TaskID, DisplayName = ticket.TaskDisplayName, O_Title = objtrans.O_Title, N_Title = objtrans.N_Title, AttachmentName = objtrans.AttachmentName, AttachmentPath = objtrans.AttachmentPath, Comments = objtrans.Comments, O_Description = objtrans.O_Description, O_Task_Status = objtrans.O_Task_Status, N_Description = objtrans.N_Description, N_Task_Statuus = objtrans.N_Task_Statuus }).OrderByDescending(x => x.TaskID).Skip(skipcount).Take(10).Distinct().ToList();
                    // Get Actual Data in place of ids 


                    //List<Trans_TicketDTO> NewActivityDTO = new List<Trans_TicketDTO>();

                    //foreach (Trans_TicketDTO objtransaction in ActivityDTO)
                    //{
                    //    Trans_TicketDTO objtrdto = new Trans_TicketDTO { TaskID = objtransaction.TaskID, DisplayName = objtransaction.DisplayName, AttachmentName = objtransaction.AttachmentName, AttachmentPath = objtransaction.AttachmentPath, Comments = objtransaction.Comments, N_Title = objtransaction.N_Title, O_Title = objtransaction.O_Title, N_Description = objtransaction.N_Description, O_Description = objtransaction.O_Description, CreatedDate = objtransaction.CreatedDate, O_Task_Status = objtransaction.O_Task_Status, N_Task_Statuus = objtransaction.N_Task_Statuus };

                    //    if (objtransaction.CreatedBy != null)
                    //    {
                    //        string id = objtransaction.CreatedBy.ToString();
                    //        objtrdto.CreatedBy = objcontext.AspNetUsers.Where(x => x.Id == id).FirstOrDefault().UserName;
                    //    }


                    //    //int? tid = objtransaction.TaskID;
                    //    //objtrdto.DisplayName = objcontext.Mst_Task.Where(x => x.ID == tid).FirstOrDefault().TaskDisplayName;


                    //    NewActivityDTO.Add(objtrdto);
                    //}
                    ObjUserDashboardDTO.ActivityDTO = ActivityDTO;
                    objres.Message = "Success";
                    objres.Response = ObjUserDashboardDTO;
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
    }
}
