using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
    public static class DashboardRepository
    {


        public static CustomResponse GetDashboardStatistics()
        {
            CustomResponse objres = new CustomResponse();
            List<DashBoardStatisticsDTO> StatisticsDTO = new List<DashBoardStatisticsDTO>();
            try
            {
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {

                    // projects
                    StatisticsDTO.Add(new DashBoardStatisticsDTO { Type = "1", Count = objcontext.Mst_Project.Count() });
                    // clients
                    SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Db_Zon_Test_techsupportEntities"].ToString());
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select count (distinct ur.UserId) from AspNetUserRoles ur,AspNetUsers u where  ur.RoleId=@roleid  and ur.Userid =u.Id and u.Status=1", con);
                    cmd.Parameters.AddWithValue("@roleid", "294e2276-384e-4844-ad8a-7a4c4dd9fcdc");
                    StatisticsDTO.Add(new DashBoardStatisticsDTO { Type = "2", Count = Convert.ToInt32(cmd.ExecuteScalar()) });
                    // admins
                    SqlCommand cmd1 = new SqlCommand("select count (distinct ur.UserId) from AspNetUserRoles ur,AspNetUsers u where  ur.RoleId=@roleid  and ur.Userid =u.Id and u.Status=1", con);
                    cmd1.Parameters.AddWithValue("@roleid", "594875d4-5d30-4d84-bdb3-6a9309799ae2");
                    StatisticsDTO.Add(new DashBoardStatisticsDTO { Type = "3", Count = Convert.ToInt32(cmd1.ExecuteScalar()) });
                    // users-developers
                    SqlCommand cmd2 = new SqlCommand("select count (distinct ur.UserId) from AspNetUserRoles ur,AspNetUsers u where  ur.RoleId=@roleid  and ur.Userid =u.Id and u.Status=1", con);
                    cmd2.Parameters.AddWithValue("@roleid", "15d2fcc9-23c3-4ad8-b93d-d134d3f9349e");
                    StatisticsDTO.Add(new DashBoardStatisticsDTO { Type = "4", Count = Convert.ToInt32(cmd2.ExecuteScalar()) });
                    con.Close();
                    objres.Message = "Success";
                    objres.Response = StatisticsDTO;
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

        public static CustomResponse GetProjectTicketUsersCount()
        {
            CustomResponse objres = new CustomResponse();
            List<ProjectTicketUsersDTO> StatisticsDTO = new List<ProjectTicketUsersDTO>();
            try
            {
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
                    List<Mst_Project> ProjectsList = objcontext.Mst_Project.ToList();
                    foreach (Mst_Project proj in ProjectsList)
                    {
                        int ticketscount = objcontext.Mst_Task.Where(x => x.ProjectID == proj.ID && x.Task_Status != 4).Count();
                        int userscount = objcontext.Mst_ProjectUsers.Where(x => x.ProjectID == proj.ID).Count() + 2;
                        StatisticsDTO.Add(new ProjectTicketUsersDTO { ProjectName = proj.Name, ProjectID = proj.ID, TicketsCount = ticketscount, UsersCount = userscount });
                    }

                    objres.Message = "Success";
                    objres.Response = StatisticsDTO;
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

        public static dynamic GetActivityByPageNo(int pageno)
        {


            int skipcount = 10 * pageno;
            CustomResponse objres = new CustomResponse();
            try
            {
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {

                    List<Trans_TicketDTO> ActivityDTO = (from objtrans in objcontext.Trans_Ticket
                                                         join ticket in objcontext.Mst_Task on objtrans.TaskID equals ticket.ID
                                                         join user2 in objcontext.AspNetUsers on ticket.CreatedBy equals user2.Id
                                                         select new Trans_TicketDTO { CreatedBy = user2.Email, CreatedDate = objtrans.CreatedDate, TaskID = objtrans.TaskID, DisplayName = ticket.TaskDisplayName, O_Title = objtrans.O_Title, N_Title = objtrans.N_Title, AttachmentName = objtrans.AttachmentName, AttachmentPath = objtrans.AttachmentPath, Comments = objtrans.Comments, O_Description = objtrans.O_Description, O_Task_Status = objtrans.O_Task_Status, N_Description = objtrans.N_Description, N_Task_Statuus = objtrans.N_Task_Statuus }).ToList().OrderByDescending(x => x.TaskID).Distinct().Skip(skipcount).Take(10).ToList();                // Get Actual Data in place of ids 


                    //List<Trans_TicketDTO> NewActivityDTO = new List<Trans_TicketDTO>();

                    //foreach (Trans_TicketDTO objtransaction in ActivityDTO)
                    //{ 
                    //    Trans_TicketDTO objtrdto=new Trans_TicketDTO{TaskID=objtransaction.TaskID,DisplayName=objtransaction.DisplayName,AttachmentName=objtransaction.AttachmentName,AttachmentPath=objtransaction.AttachmentPath,Comments=objtransaction.Comments,N_Title=objtransaction.N_Title,O_Title=objtransaction.O_Title,N_Description=objtransaction.N_Description,O_Description=objtransaction.O_Description,CreatedDate=objtransaction.CreatedDate,O_Task_Status=objtransaction.O_Task_Status,N_Task_Statuus=objtransaction.N_Task_Statuus};

                    //    if (objtransaction.CreatedBy !=null)
                    //    {
                    //        string id = objtransaction.CreatedBy.ToString();
                    //        objtrdto.CreatedBy = objcontext.AspNetUsers.Where(x => x.Id == id).FirstOrDefault().UserName;
                    //    }
                    //    NewActivityDTO.Add(objtrdto);
                    //}

                    objres.Message = "Success";
                    //objres.Response = NewDTO;
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
