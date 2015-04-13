using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ticketing_System.Repositoy;

namespace Ticketing_System.API.Controllers
{
    public class GenericAPIController : ApiController
    {

        public dynamic Get(int type)
        {

            if (type == 1)
                return GenericRepository.GetPriorities();
            else if (type == 2)
                return GenericRepository.GetTaskType();
            else if (type == 3)
                return GenericRepository.GetTaskStatus();
            else
                return null;
        }
 
    }
}
