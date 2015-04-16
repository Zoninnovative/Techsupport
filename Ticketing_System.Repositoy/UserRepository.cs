using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
   public static class UserRepository
    {

        public static dynamic GetAllUsers()
        {
            CustomResponse objres = new CustomResponse();
            List<UserDTO> ObjFilteredUsers = new List<UserDTO>();
            try
            {

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {

                    
                        //return all projects information
                    string comp = "1";
                    List<UserDTO> objuserdetails = (from user in objcontext.AspNetUsers where user.Status =="1"
                                                    select new UserDTO
                                                    {
                                                        Id = user.Id,
                                                        FirstName = user.FirstName,
                                                        LastName = user.LastName,
                                                        Email = user.Email,
                                                        MobileNumber = user.MobileNumber,
                                                        CreatedBy = user.CreatedBy,
                                                        Status = user.Status
                                                    }).ToList();


                        for (int i = 0; i < objuserdetails.Count; i++)
                        {
                        string uid=objuserdetails[i].CreatedBy;
                            if (uid !=null)
                        objuserdetails[i].CreatedBy = objcontext.AspNetUsers.Where(x => x.Id == uid).FirstOrDefault().UserName;
                        }

                            objres.Message = "Success";
                            objres.Response = objuserdetails;
                        objres.Status = CustomResponseStatus.Successful;
                        return objres;
                   

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

        public static dynamic GetAllAdmins()
        { 
        
           CustomResponse objres = new CustomResponse();
            try
            {
          using (var objcontext = new  Db_Zon_Test_techsupportEntities())
                {


                    List<AspNetUser> objuser = objcontext.AspNetUsers.ToList();
                    List<AspNetUser> objoutput = new List<AspNetUser>();
                    List<UserDTO> objusersdto = new List<UserDTO>();
                    foreach (AspNetUser user in objuser)
                    {
                        if (user.Status=="1")
                        {

                            SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Db_Zon_Test_techsupportEntities"].ToString());
                            // 1. Instantiate a new command with a query and connection
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select RoleId from AspNetUserRoles where UserId=@uid", con);
                            cmd.Parameters.AddWithValue("@uid", user.Id);
                            // 2. Call Execute reader to get query results
                            SqlDataReader rdr = cmd.ExecuteReader();
                            // print the CategoryName of each record
                            while (rdr.Read())
                            {
                                if (rdr[0].ToString() == "594875d4-5d30-4d84-bdb3-6a9309799ae2")
                                {
                                    string rolename ="Administrator";
                                    objusersdto.Add(new UserDTO { Id = user.Id, Email = user.Email,RoleName=rolename, MobileNumber = user.MobileNumber, FirstName = user.FirstName, LastName = user.LastName });
                                }
                                    continue;
                            }
                            con.Close();
                        }
                    
                    }

                            objres.Message = "Success";
                            objres.Response = objusersdto;
                        objres.Status = CustomResponseStatus.Successful;
                        return objres;
                   

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
        public static dynamic GetAllClientsandAdmins()
        { 
        
           CustomResponse objres = new CustomResponse();
            try
            {
          using (var objcontext = new  Db_Zon_Test_techsupportEntities())
                {


                    List<AspNetUser> objuser = objcontext.AspNetUsers.ToList();
                    List<AspNetUser> objoutput = new List<AspNetUser>();
                    List<UserDTO> objusersdto = new List<UserDTO>();
                    foreach (AspNetUser user in objuser)
                    {
                        if (user.Status=="1")
                        {

                            SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Db_Zon_Test_techsupportEntities"].ToString());
                            // 1. Instantiate a new command with a query and connection
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select RoleId from AspNetUserRoles where UserId=@uid", con);
                            cmd.Parameters.AddWithValue("@uid", user.Id);
                            // 2. Call Execute reader to get query results
                            SqlDataReader rdr = cmd.ExecuteReader();
                            // print the CategoryName of each record
                            while (rdr.Read())
                            {
                                if (rdr[0].ToString() == "294e2276-384e-4844-ad8a-7a4c4dd9fcdc" || rdr[0].ToString() == "594875d4-5d30-4d84-bdb3-6a9309799ae2")
                                {
                                    string rolename = rdr[0].ToString() == "294e2276-384e-4844-ad8a-7a4c4dd9fcdc" ? "Client" : "Administrator";
                                    objusersdto.Add(new UserDTO { Id = user.Id, Email = user.Email,RoleName=rolename, MobileNumber = user.MobileNumber, FirstName = user.FirstName, LastName = user.LastName });
                                }
                                    continue;
                            }
                            con.Close();
                        }
                    
                    }

                            objres.Message = "Success";
                            objres.Response = objusersdto;
                        objres.Status = CustomResponseStatus.Successful;
                        return objres;
                   

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


        public static dynamic GetAllClients()
        {

            CustomResponse objres = new CustomResponse();
            try
            {
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {


                    List<AspNetUser> objuser = objcontext.AspNetUsers.ToList();
                    List<AspNetUser> objoutput = new List<AspNetUser>();
                    List<UserDTO> objusersdto = new List<UserDTO>();
                    foreach (AspNetUser user in objuser)
                    {
                        if (user.Status == "1")
                        {

                            SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Db_Zon_Test_techsupportEntities"].ToString());
                            // 1. Instantiate a new command with a query and connection
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select RoleId from AspNetUserRoles where UserId=@uid", con);
                            cmd.Parameters.AddWithValue("@uid", user.Id);
                            // 2. Call Execute reader to get query results
                            SqlDataReader rdr = cmd.ExecuteReader();
                            // print the CategoryName of each record
                            while (rdr.Read())
                            {
                                if (rdr[0].ToString() == "294e2276-384e-4844-ad8a-7a4c4dd9fcdc")
                                {
                                    string rolename = "Client";
                                    objusersdto.Add(new UserDTO { Id = user.Id, Email = user.Email, RoleName = rolename, MobileNumber = user.MobileNumber, FirstName = user.FirstName, LastName = user.LastName });
                                }
                                continue;
                            }
                            con.Close();
                        }

                    }

                    objres.Message = "Success";
                    objres.Response = objusersdto;
                    objres.Status = CustomResponseStatus.Successful;
                    return objres;


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

        public static dynamic GetAllDevelopers()
        {

            CustomResponse objres = new CustomResponse();
            try
            {
                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {


                    List<AspNetUser> objuser = objcontext.AspNetUsers.ToList();
                    List<AspNetUser> objoutput = new List<AspNetUser>();
                    List<UserDTO> objusersdto = new List<UserDTO>();
                    foreach (AspNetUser user in objuser)
                    {
                        if (user.Status == "1")
                        {
                            SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Db_Zon_Test_techsupportEntities"].ToString());
                            // 1. Instantiate a new command with a query and connection
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select RoleId from AspNetUserRoles where UserId=@uid", con);
                            cmd.Parameters.AddWithValue("@uid", user.Id);
                            // 2. Call Execute reader to get query results
                            SqlDataReader rdr = cmd.ExecuteReader();
                            // print the CategoryName of each record
                            while (rdr.Read())
                            {
                                if (rdr[0].ToString() == "15d2fcc9-23c3-4ad8-b93d-d134d3f9349e")
                                    objusersdto.Add(new UserDTO { Id = user.Id,RoleName="Developers" ,Email = user.Email,MobileNumber=user.MobileNumber ,FirstName = user.FirstName, LastName = user.LastName });
                                continue;
                            }
                            con.Close();
                        }

                    }

                    objres.Message = "Success";
                    objres.Response = objusersdto;
                    objres.Status = CustomResponseStatus.Successful;
                    return objres;


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


        public static dynamic Delete(string Userid)
        {

            CustomResponse objres = new CustomResponse();
            try
            {

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
                    AspNetUser objuser = objcontext.AspNetUsers.Where(x => x.Id == Userid).FirstOrDefault();
                    objuser.Status = "0";
                    objcontext.SaveChanges();
                    objres.Message = "Success";
                    objres.Response = objuser;
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


        public static dynamic EDIT(string Userid)
        {

            CustomResponse objres = new CustomResponse();
            try
            {

                using (var objcontext = new Db_Zon_Test_techsupportEntities())
                {
                    AspNetUser objuser = objcontext.AspNetUsers.Where(x => x.Id == Userid).FirstOrDefault();
                    objuser.Status = "0";
                    objcontext.SaveChanges();
                    objres.Message = "Success";
                    objres.Response = objuser;
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

        public static string GetUserRole(string uid)//
        {
            List<string> roles = new List<string>();
            SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Db_Zon_Test_techsupportEntities"].ToString());
            // 1. Instantiate a new command with a query and connection
            con.Open();
            SqlCommand cmd = new SqlCommand("select RoleId from AspNetUserRoles where UserId=@uid", con);
            cmd.Parameters.AddWithValue("@uid", uid);
            // 2. Call Execute reader to get query results
            SqlDataReader rdr = cmd.ExecuteReader();
            // print the CategoryName of each record
            while (rdr.Read())
            {
                roles.Add(rdr[0].ToString());
            }
            con.Close();

            if (roles[0].ToString() == "15d2fcc9-23c3-4ad8-b93d-d134d3f9349e")
            {
                return "Developer";
            }
            else if (roles[0].ToString() == "294e2276-384e-4844-ad8a-7a4c4dd9fcdc")
            {
                return "Client";
            }
            else if (roles[0].ToString() == "594875d4-5d30-4d84-bdb3-6a9309799ae2")
            {
                return "Administrator";
            }
            else
                return null;
        
        }


        public static string GetFnamebyUid(String uid)
        {
            var objcontext = new Db_Zon_Test_techsupportEntities();
            return objcontext.AspNetUsers.Where(a => a.Id == uid).FirstOrDefault().FirstName;


        }
        public static string GetProjectNmaebyid(int pid)
        {
            var objcontext = new Db_Zon_Test_techsupportEntities();
            return objcontext.Mst_Project.Where(a => a.ID == pid).FirstOrDefault().Name;

        }//

       public static string GetTaskNameByID(int TaskID)
       {
           var objcontext = new Db_Zon_Test_techsupportEntities();
           return objcontext.Mst_Task.Where(a => a.ID == TaskID).FirstOrDefault().TaskDisplayName;
       }

       public static dynamic GetEmailAddressesByUserIds(List<string> objuserids)
       {
           
           List<string> EmailAddresses = new List<string>();
           try
           {
               using (var objcontext = new Db_Zon_Test_techsupportEntities())
               {
                   EmailAddresses = (from p in objcontext.AspNetUsers where objuserids.Contains(p.Id) select p).Select(x=>x.Email).ToList();
                   return EmailAddresses;
               }
           }
           catch (Exception ex)
           {
               return null;
           }

       }
       public static int UpdateStatus(string EmailID,string Status)
       {
           var objcontext = new Db_Zon_Test_techsupportEntities();
           return objcontext.AspNetUsers.Where(x => x.Email == EmailID && x.Status == Status).Count();
       }

       public static void ChangeStatus(string Email)
       {
           var objcontext = new Db_Zon_Test_techsupportEntities();
           AspNetUser objUser = objcontext.AspNetUsers.Where(x => x.Email == Email).First();
           objUser.Status = "1";
           objcontext.SaveChanges();
       }
       public static dynamic UpdateProfile(UserDTO objuser)
       {
           CustomResponse objres = new CustomResponse();
           try
           {

               using (var objcontext = new Db_Zon_Test_techsupportEntities())
               {
                   // get short name of the project and get the max count of the tickets and use it as display name 
                   string uid = objuser.Id; 
                   AspNetUser objuinfo = objcontext.AspNetUsers.Where(x => x.Id == uid).FirstOrDefault();

                   objuinfo.FirstName = objuser.FirstName;
                   objuinfo.LastName = objuser.LastName;
                   objuinfo.MobileNumber = objuser.MobileNumber;
                   objuinfo.CreatedBy = objuser.CreatedBy;
                   objcontext.SaveChanges();
                   objres.Message = "Success";
                   objres.Response = objuinfo;
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

       
    }
}
