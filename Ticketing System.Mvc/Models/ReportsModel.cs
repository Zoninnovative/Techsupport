using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticketing_System.Core;

namespace Ticketing_System.Mvc.Models
{
    public class ReportsModel : ReportDTO
    {
        public List<SelectListItem> ProjectsDDl { get; set; }
        public List<SelectListItem> TaskTypeDDl { get; set; }
        public List<SelectListItem> TaskStatusDDl { get; set; }
    }
}