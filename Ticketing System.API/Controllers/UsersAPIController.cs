using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ticketing_System.Repositoy;

namespace Ticketing_System.API.Controllers
{
    public class UsersAPIController : ApiController
    {

        [HttpGet]
        public dynamic Get(string type)
        {
           // developer-1,clients-2,all-0

            if (type == "")
            {
                var data= UserRepository.GetAllUsers();
                return data;
            }
            else if (type == "2")
            {
                var data= UserRepository.GetAllClientsandAdmins();

                return data;
            }
            else if (type=="1")
                return UserRepository.GetAllDevelopers();

            else
                return UserRepository.GetAllUsers();
        }

        

        public dynamic Delete(string uid)
        {
            return UserRepository.Delete(uid);
        }

       
    }
}
