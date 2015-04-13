using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticketing_System.Core;

namespace Ticketing_System.Mvc.Models
{
    public class CreateProjectModel
    {

       
        public string UserIds { get; set; }
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
          [Required]
        public string Description { get; set; }
          [Required]
        public string ClientID { get; set; }
          [Required]
        public string PManagerID { get; set; }
          [Required]
        public Nullable<System.DateTime> SignUpDate { get; set; }
          [Required]
        public Nullable<System.DateTime> StartDate { get; set; }
          [Required]
        public Nullable<int> Duration { get; set; }
          [Required]
        public Nullable<System.DateTime> ProposedEndDate { get; set; }

        public Nullable<System.DateTime> ActualEndDate { get; set; }
        public Nullable<bool> Status { get; set; }
           
        public string ShortName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
          [Required]
        public string ProjectManager { get; set; }
          [Required]
        public string Client { get; set; }

        public List<SelectListItem> ClientsDDl { get; set; }

        public List<SelectListItem> UsersList { get; set; }
        public List<SelectListItem> ProjectManagerDDl { get; set; }
    }

    
}