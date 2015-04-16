using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing_System.Core
{


    public class DashBoardStatisticsDTO
    {

        public string Type { get; set; }
        public int Count { get; set; }
    

    }

    public class ProjectTicketUsersDTO
    {

        public int ProjectID { get; set; }

        public string  ProjectName { get; set; }
        public int TicketsCount { get; set; }
        public int UsersCount { get; set; }
    }
    public class DashboardModel
    {
        public int AdminsCount { get; set; }
        public int ClientsCount { get; set; }
        public int UsersCount { get; set; }

        public int ProjectsCount { get; set; }

        public List<ProjectTicketUsersDTO> TableData { get; set; }


        public List<Trans_TicketDTO> ActivityDTO { get; set; }
    
    }
    public partial class TaskDTO
    {
        public string Task_Type_Name { get; set; }

        public string Task_Status_Name { get; set; }
        public int? Task_Status { get; set; }
        public string TaskDisplayName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectID { get; set; }

        public string AssignedTo { get; set; }
        public string RefereedTo { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> AssigndedDate { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public Nullable<int> TypeID { get; set; }
        public Nullable<bool> Status { get; set; }

        public string AssignedToName { get; set; }

        public string RefereedToName { get; set; }

        public string TaskTypeName{ get; set; }

        public string PriorityName { get; set; }

        public List<CommentsDTO> Comments { get; set; }

        public string Attachment1_Name { get; set; }
        public string Attachment1_Path { get; set; }
        public string Attachment2_Name { get; set; }
        public string Attachment2_Path { get; set; }
        public string Attachment3_Name { get; set; }
        public string Attachment3_Path { get; set; }
    }

    public partial class UserDTO
    {
        public UserDTO()
        {
            
        }

        public string RoleName { get; set; }

        public string Status { get; set; }
        public string UpdatedBy { get; set; }

        [Required]
        public string Password { get; set; }
        public string Id { get; set; }
         [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }

        public string CreatedBy { get; set; }

        
    }
    public class CreateProjectDTO
    {
        public ProjectDTO objProject { get; set; }

        public List<string> Users { get; set; }

    }

    public partial class ProjectDTO
    {
        public ProjectDTO()
        {
            
        }
        
        public string UpdatedBy { get; set; }
        public List<string> ProjectUsers { get; set; }
        public string CreatedBy { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClientID { get; set; }
        public string PManagerID { get; set; }
        public Nullable<System.DateTime> SignUpDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<System.DateTime> ProposedEndDate { get; set; }
        public Nullable<System.DateTime> ActualEndDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public string ShortName { get; set; }

        public string ProjectManager { get; set; }

        public string Client { get; set; }

         
    }
    public class ProjectUsersDTO
    {
         

        public virtual AspNetUser AspNetUser { get; set; }
        public int ID { get; set; }
      
        public int? ProjectID { get; set; }
        public bool? Status { get; set; }
        public string UserID { get; set; }
    }

    public partial class ProjectDrocumentsDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DocumentPath { get; set; }
        public string Description { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string UploadedBy { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }
    public partial class TaskPriorityDTO
    {
        public TaskPriorityDTO()
        {
             
        }

        public int ID { get; set; }
        public string PriorityName { get; set; }
        public Nullable<bool> Status { get; set; }
    }
    public partial class TaskTypeDTO
    {
        public TaskTypeDTO()
        {
            
        }

        public int ID { get; set; }
        public string Type { get; set; }
        public Nullable<bool> Status { get; set; }

    }

    public partial class CreateTaskDTO
    {

        public List<CommentsDTO> Comments { get; set; }
        public string TaskDisplayName { get; set; }
        
        public dynamic TaskStatusDDl { get; set; }
         [Required]
        public int? Task_Status { get; set; }
        public string CreatedBy { get; set; }
        public dynamic PriorityDDL { get; set; }
        public dynamic TypeDDL { get; set; }

        public dynamic ProjectsDDL { get; set; }
        public dynamic AssignTODDL { get; set; }

        public dynamic RefereerToDDL { get; set; }

        public List<TaskDTO> TasksDTO { get; set; }

        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
         [Required]
        
        public string Description { get; set; }
         [Required]
        public int ProjectID { get; set; }
         [Required]
        public string AssignedTo { get; set; }
         [Required]
        public string RefereedTo { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> AssigndedDate { get; set; }
         [Required]
        public Nullable<int> PriorityID { get; set; }
         [Required]
        public Nullable<int> TypeID { get; set; }
        public Nullable<bool> Status { get; set; }
         
        public string AssignedToName { get; set; }

        public string RefereedToName { get; set; }

        public string TaskTypeName { get; set; }

        public string PriorityName { get; set; }

        public string file { get; set; }
        public string Attachment1_Name { get; set; }
        public string Attachment1_Path { get; set; }
        public string Attachment2_Name { get; set; }
        public string Attachment2_Path { get; set; }
        public string Attachment3_Name { get; set; }
        public string Attachment3_Path { get; set; }


    }

    public partial class TypeAndPriorityDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

   


    public partial class UserDashboardDTO
    {

      public  List<TaskDTO> AssignedToMe { get; set; }

     public   List<Trans_TicketDTO> ActivityDTO { get; set; }
    
    }
    public partial class ClientDashboardDTO
    {

        public List<Trans_TicketDTO> ActivityDTO { get; set; }

    }


    public partial class UploadDocumentDTO
    {
        public string FileName { get; set; }
        public string Format { get; set; }
        public string Base64String { get; set; }
    
    }

    public partial class Trans_TicketDTO
    {
        public int ID { get; set; }
        public Nullable<int> TaskID { get; set; }

        public string DisplayName { get; set; }
        public string Comments { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string O_Title { get; set; }
        public string N_Title { get; set; }
        public string O_Description { get; set; }
        public string N_Description { get; set; }
        public string O_AssignedTo { get; set; }
        public string N_AssignedTo { get; set; }
        public string O_RefereedTo { get; set; }
        public string N_RefereedTo { get; set; }
        public Nullable<int> O_PriorityID { get; set; }
        public Nullable<int> N_PriorityID { get; set; }
        public Nullable<int> O_TypeID { get; set; }
        public Nullable<int> N_TypeID { get; set; }
        public Nullable<int> O_Task_Status { get; set; }
        public Nullable<int> N_Task_Statuus { get; set; }
        public Nullable<bool> Status { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Mst_Task Mst_Task { get; set; }
    }

    public partial class CommentsDTO
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public string Comment { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }
    
    }


    public partial class ChangePasswordDTO
    {

        public int ChageType { get; set; }
        public string userid { get; set; }
        public string oldpassword { get; set; }

        public string newpassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public int Role { get; set; }

    
    }

    public partial class ReportDTO
    {
        public int ProjectID { get; set; }
        public string UserID { get; set; }
        public dynamic ReportData { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int   TaskStatusID { get; set; }
        public int TaskTypeID { get; set; }

    }


    public partial class ReportData
    {
        public string ProjectName { get; set; }
        public int TicketID { get; set; }
        public string TicketDisplayName { get; set; }
        public string TicketType { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string TaskStatus { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string AssignedTo { get; set; }
        public string RefereerTo { get; set; }
    }

    public partial class projectusers
    {
        public string mailid { get; set; }

    }

    }


