using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Core;

namespace Ticketing_System.Repositoy
{
  public static  class AccountRepository
    {


      // Method to get user profile
      public static dynamic GetUserProfile(string userid)
      {
          CustomResponse objres = new CustomResponse();
          try
          {


              UserDTO objuser = new UserDTO();

              using (var objcontext = new Db_Zon_Test_techsupportEntities())
              {

                  objuser = (from user in objcontext.AspNetUsers
                             where user.Id == userid
                             select new UserDTO {
                             Id=user.Id,
                             FirstName=user.FirstName,
                             LastName=user.LastName,
                             Email=user.Email,
                             MobileNumber=user.MobileNumber
                             }).FirstOrDefault();
                              


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


      // Method to update user profile
      public static dynamic EditProfile(UserDTO objuserinfo)
      {

          CustomResponse objres = new CustomResponse();
          try
          {
              using (var objcontext = new Db_Zon_Test_techsupportEntities())
              {
                  string uid = objuserinfo.Id;
                  AspNetUser objuser = objcontext.AspNetUsers.Where(x => x.Id == uid).FirstOrDefault();

                  objuser.FirstName = objuserinfo.FirstName;
                  objuser.LastName = objuserinfo.LastName;
                  objuser.MobileNumber = objuserinfo.MobileNumber;
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

       public static void AddPasswordResetToken(string userid,string token)
       {
           using (var context = new Db_Zon_Test_techsupportEntities())
           {
               AspNetUser objuser = context.AspNetUsers.Where(x => x.Id == userid).FirstOrDefault();
               objuser.PasswordHash = token;
               context.SaveChanges();
           }
       
       }

       public static bool CompareResetToken(string userid, string key)
       {
           using (var context = new Db_Zon_Test_techsupportEntities())
           {
               return context.AspNetUsers.Where(x => x.Id == userid && x.PasswordHash == key).Any();
           }
       }

      
    }
}
