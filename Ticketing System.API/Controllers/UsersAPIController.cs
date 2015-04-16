using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ticketing_System.Core;
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
                var data= UserRepository.GetAllClients();

                return data;
            }
            else if (type=="1")
                return UserRepository.GetAllDevelopers();
            else if (type == "3")
                return UserRepository.GetAllAdmins();
            else if (type == "4")
                return UserRepository.GetAllClientsandAdmins();

            else
                return UserRepository.GetAllUsers();
        }

        

        public dynamic Delete(string uid)
        {
            return UserRepository.Delete(uid);
        }

        public dynamic Put(UserDTO userDTO)
        {

            CustomResponse objres = new CustomResponse();
            try
            {


            }
            catch (Exception ex)
            {
                objres.Status = CustomResponseStatus.Exception;
                objres.Message = ex.Message;
                objres.Response = null;

            }
            return objres;
        }
       
    }
}
