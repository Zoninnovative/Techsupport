using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ticketing_System.API.Providers;
using Ticketing_System.Core;
using Ticketing_System.Repositoy;


namespace Ticketing_System.API.Controllers
{
    public class ProjectsAPIController : ApiController
    {
        [HttpGet]
        public dynamic Get(string userid, int projectid)
            {

            if (projectid > 0)
                {
                    return ProjectRepository.GetProjectDetails(projectid);
                }
            else if (userid.Trim().Length > 0)
                return ProjectRepository.GetProjectsInfo(userid);
                else
                    return null;
        
        }
        
        [HttpPost]
        public dynamic Post(CreateProjectDTO objcreateProject)
        {
            Mst_Project objprojectNew = new Mst_Project {       Name = objcreateProject.objProject.Name, 
                                                                Description = objcreateProject.objProject.Description, 
                                                                ShortName = objcreateProject.objProject.ShortName, 
                                                                Duration = objcreateProject.objProject.Duration, 
                                                                ClientID = objcreateProject.objProject.ClientID, 
                                                                PManagerID = objcreateProject.objProject.PManagerID,
                                                                ProposedEndDate = objcreateProject.objProject.ProposedEndDate, 
                                                                SignUpDate = objcreateProject.objProject.SignUpDate,
                                                                StartDate = objcreateProject.objProject.StartDate, 
                                                                Status = objcreateProject.objProject.Status,
                                                                CreatedBy = objcreateProject.objProject.CreatedBy
                                                        };


            List<Mst_ProjectUsers> objusers = new List<Mst_ProjectUsers>();
            
           
            foreach (string user in objcreateProject.Users)
            {
              objprojectNew.Mst_ProjectUsers.Add(new Mst_ProjectUsers { UserID = user });
            }

            List<string> ObjToAddresses = new List<string>();
            List<string> ObjFilteredToAddresses = new List<string>();
            ObjToAddresses.Add(objcreateProject.objProject.CreatedBy);
            ObjToAddresses.Add(objcreateProject.objProject.ClientID);
            ObjToAddresses.Add(objcreateProject.objProject.PManagerID);
           // ObjToAddresses.AddRange(ProjectRepository.GetProjectUsers(objtaskdto.ProjectID));
               ObjFilteredToAddresses = ObjToAddresses.Distinct().ToList();
               ObjFilteredToAddresses = UserRepository.GetEmailAddressesByUserIds(ObjFilteredToAddresses);
               foreach (var toMail in ObjFilteredToAddresses)
               {
                   MailSender.ProjectCreationMail(objprojectNew, toMail);
               }


                     return ProjectRepository.AddNewProject(objprojectNew);
                           
            }
          
    

        [HttpPut]
        public dynamic Put(CreateProjectDTO objupdateProject)
        {
            Mst_Project objprojectNew = new Mst_Project {ID=objupdateProject.objProject.ID ,Name = objupdateProject.objProject.Name, Description = objupdateProject.objProject.Description, ShortName = objupdateProject.objProject.ShortName, Duration = objupdateProject.objProject.Duration, ClientID = objupdateProject.objProject.ClientID, PManagerID = objupdateProject.objProject.PManagerID, ProposedEndDate = objupdateProject.objProject.ProposedEndDate, SignUpDate = objupdateProject.objProject.SignUpDate, StartDate = objupdateProject.objProject.StartDate, Status = objupdateProject.objProject.Status, CreatedBy = objupdateProject.objProject.CreatedBy ,UpdatedBy=objupdateProject.objProject.UpdatedBy};
            List<Mst_ProjectUsers> objusers = new List<Mst_ProjectUsers>();
            foreach (string user in objupdateProject.Users)
            {
                objprojectNew.Mst_ProjectUsers.Add(new Mst_ProjectUsers { UserID = user });
            }
            return ProjectRepository.UpdateProject(objprojectNew);
        }
         
    }
}
