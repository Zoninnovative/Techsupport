using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
    public static class ProjectRepository
    {
       
      

        // Get all projects information based on username
        public static dynamic GetProjectsInfo(string username)
        {
            CustomResponse objres = new CustomResponse();
            try
            {
                
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {

                    if (UserRepository.GetUserRole(username) == "Administrator")
                    {
                        //return all projects information
                        var objprojectdetails = (from proj in objcontext.Mst_Project

                                                 join user in objcontext.AspNetUsers on proj.PManagerID equals user.Id

                                                 join user1 in objcontext.AspNetUsers on proj.ClientID equals user1.Id
                                                 select new ProjectDTO
                                                 {
                                                     ID = proj.ID,
                                                     Name = proj.Name,
                                                     Description = proj.Description,
                                                     ShortName = proj.ShortName,
                                                     ProjectManager = user.FirstName,
                                                     Client = user1.FirstName,
                                                     ClientID = proj.ClientID,
                                                     PManagerID = proj.PManagerID,
                                                     SignUpDate = proj.SignUpDate,
                                                     StartDate = proj.StartDate,
                                                     Duration = proj.Duration,
                                                     ProposedEndDate = proj.ProposedEndDate,
                                                     ActualEndDate = proj.ActualEndDate,
                                                     Status = proj.Status,
                                                     
                                                 }).ToList();
                        objres.Message = "Success";
                        objres.Response = objprojectdetails;
                        objres.Status = CustomResponseStatus.Successful;
                        return objres;
                    }
                    else if (UserRepository.GetUserRole(username) == "Developer")
                    {
                        //return all projects information
                        var objprojectdetails = (from proj in objcontext.Mst_Project

                                                 join user in objcontext.AspNetUsers on proj.PManagerID equals user.Id

                                                 join user1 in objcontext.AspNetUsers on proj.ClientID equals user1.Id

                                                 join pusers in objcontext.Mst_ProjectUsers on proj.ID equals pusers.ProjectID

                                                 where  pusers.UserID== username
                                                 select new ProjectDTO
                                                 {
                                                     ID = proj.ID,
                                                     Name = proj.Name,
                                                     Description = proj.Description,
                                                     ShortName = proj.ShortName,
                                                     ProjectManager = user.UserName,
                                                     Client = user1.UserName,
                                                     ClientID = proj.ClientID,
                                                     PManagerID = proj.PManagerID,
                                                     SignUpDate = proj.SignUpDate,
                                                     StartDate = proj.StartDate,
                                                     Duration = proj.Duration,
                                                     ProposedEndDate = proj.ProposedEndDate,
                                                     ActualEndDate = proj.ActualEndDate,
                                                     Status = proj.Status,

                                                 }).ToList();
                        objres.Message = "Success";
                        objres.Response = objprojectdetails;
                        objres.Status = CustomResponseStatus.Successful;
                        return objres;
                    }
                    else if (UserRepository.GetUserRole(username) == "Client")
                    {
                        //return all projects information
                        var objprojectdetails = (from proj in objcontext.Mst_Project

                                                 join user in objcontext.AspNetUsers on proj.PManagerID equals user.Id

                                                 join user1 in objcontext.AspNetUsers on proj.ClientID equals user1.Id

                                                where proj.ClientID==username
                                                 select new ProjectDTO
                                                 {
                                                     ID = proj.ID,
                                                     Name = proj.Name,
                                                     Description = proj.Description,
                                                     ShortName = proj.ShortName,
                                                     ProjectManager = user.UserName,
                                                     Client = user1.UserName,
                                                     ClientID = proj.ClientID,
                                                     PManagerID = proj.PManagerID,
                                                     SignUpDate = proj.SignUpDate,
                                                     StartDate = proj.StartDate,
                                                     Duration = proj.Duration,
                                                     ProposedEndDate = proj.ProposedEndDate,
                                                     ActualEndDate = proj.ActualEndDate,
                                                     Status = proj.Status,

                                                 }).ToList();
                        objres.Message = "Success";
                        objres.Response = objprojectdetails;
                        objres.Status = CustomResponseStatus.Successful;
                        return objres;
                    }
                }
                objres.Message = "Success";
                objres.Response = null;
                objres.Status = CustomResponseStatus.Successful;
                return objres;
               
            }
            catch (Exception ex)
            {
                objres.Message = ex.Message;
                objres.Response = null;
                objres.Status = CustomResponseStatus.UnSuccessful;
                return objres;
                
            }
        }

        public static dynamic AddNewProject(Mst_Project objnewproject)
        { 
        
         CustomResponse objres = new CustomResponse();
         try
         {

             using (var objcontext = new Db_Zon_Test_techsupportEntities())
             {
                 objcontext.Mst_Project.Add(objnewproject);
                 objcontext.SaveChanges();
                 objres.Message = "Success";
                 objres.Response = objnewproject;
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


        // get particular project detail
        public static dynamic GetProjectDetails(int projectid)
        {
            CustomResponse objres = new CustomResponse();
            try
            {


                ProjectDTO objproject = new ProjectDTO();

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {

                    objproject = (from proj in objcontext.Mst_Project
                                where proj.ID == projectid
                                select new ProjectDTO {
                                    ID = proj.ID,
                                    Name = proj.Name,
                                    Description = proj.Description,
                                    ShortName = proj.ShortName,
                                    ProjectManager = proj.AspNetUser.FirstName,
                                    Client = proj.AspNetUser.FirstName,
                                    ClientID = proj.ClientID,
                                    PManagerID = proj.PManagerID,
                                    SignUpDate = proj.SignUpDate,
                                    StartDate = proj.StartDate,
                                    Duration = proj.Duration,
                                    ProposedEndDate = proj.ProposedEndDate,
                                    ActualEndDate = proj.ActualEndDate,
                                    Status = proj.Status
                                }).First();

                    objproject.ProjectUsers = objcontext.Mst_ProjectUsers.Where(x => x.ProjectID == projectid).Select(x => x.UserID).ToList();
                    objres.Message = "Success";
                    objres.Response = objproject;
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

        public static dynamic UpdateProject(Mst_Project objupdateproject)
        {
            CustomResponse objres = new CustomResponse();
            try
            {
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
                    int pid=objupdateproject.ID;
                    Mst_Project objdbproject = objcontext.Mst_Project.Where(x => x.ID == pid).FirstOrDefault();

                    objdbproject.Name = objupdateproject.Name;
                    objdbproject.Description = objupdateproject.Description;
                    objdbproject.PManagerID = objupdateproject.PManagerID;
                    objdbproject.ClientID = objupdateproject.ClientID;
                    objdbproject.SignUpDate = objupdateproject.SignUpDate;
                    objdbproject.StartDate = objupdateproject.StartDate;
                    objdbproject.Duration = objupdateproject.Duration;
                    objdbproject.ShortName = objupdateproject.ShortName;
                    objdbproject.ProposedEndDate = objupdateproject.ProposedEndDate;
                    for (int i = 0; i < objdbproject.Mst_ProjectUsers.Count(); i++)
                    {
                        objdbproject.Mst_ProjectUsers.ElementAt(i).Status = false;
                    }
                    objdbproject.Mst_ProjectUsers = objupdateproject.Mst_ProjectUsers;
                    objcontext.SaveChanges();
                    objres.Message = "Success";
                    objres.Response = objdbproject;
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


        // get particular project detail
        public static List<string> GetProjectUsers(int projectid)
        {
            CustomResponse objres = new CustomResponse();
            try
            {
                List<string> objUserIds = new List<string>();
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
                    // Add ProjectManager and Client Info
                    Mst_Project objproject = objcontext.Mst_Project.Where(x => x.ID == projectid).FirstOrDefault();
                    objUserIds.Add(objproject.PManagerID);
                    objUserIds.Add(objproject.ClientID);
                    // Add ProjectUsers info
                    objUserIds.AddRange(objcontext.Mst_ProjectUsers.Where(x => x.ProjectID == projectid).Select(x => x.UserID).ToList());
                    return objUserIds;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        

    }


}
