﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ticketing_System.Repositoy;

namespace Ticketing_System.API.Controllers
{
    public class UserDashboardAPIController : ApiController
    {

        public dynamic Get(string userid,int pageno)
        {
                return UserDashboardRepository.Get(userid, pageno);
        }
    }
}
