using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ticketing_System.Repositoy;

namespace Ticketing_System.API.Controllers
{
    public class DashboardAPIController : ApiController
    {
        public dynamic Get(int type,int pageno)
        {
            if (type == 1)
                return DashboardRepository.GetDashboardStatistics();
            else if (type == 2)
                return DashboardRepository.GetProjectTicketUsersCount();
            else if(type==3)
                return DashboardRepository.GetActivityByPageNo(pageno);
                else 
            return DashboardRepository.GetDashboardStatistics();
        }
       
    }
}
