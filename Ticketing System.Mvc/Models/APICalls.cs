 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Ticketing_System.Core;

namespace Ticketing_System.Mvc.Models
{
    public static class APICalls
    {

        static APICalls()
        {
             //initialize variables here
        }


        #region global variables
       public static string baseurl = ConfigurationManager.AppSettings["BaseURl"].ToString();
       public static CustomResponse res = new CustomResponse();
       public static System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        #endregion

        //<summary> Get Method </summary><description>api url contains api method along with parameters</description>
        public static CustomResponse Get(string apiurl)
        {
            HttpClient client = new HttpClient(); 
            client.BaseAddress = new Uri(baseurl);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(apiurl).Result;
            if (response.IsSuccessStatusCode)
            {
               res = response.Content.ReadAsAsync<CustomResponse>().Result;
            }
            else
            {
            }
            return res;
        }

        //<summary>Post Method </summary><description>method takes api and data variable</description>
        public static CustomResponse Post(string apiurl,object data)
        {
            HttpClient client = new HttpClient(); 
            client.BaseAddress = new Uri(baseurl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(apiurl, data).Result;
            if (response.IsSuccessStatusCode)
            {
                res = response.Content.ReadAsAsync<CustomResponse>().Result;
            }
                return res;
        }


        //<summary>Put Method </summary><description>method takes api and data variable</description>
        public static CustomResponse Put(string apiurl, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string jsonstring = javaScriptSerializer.Serialize(data);
            StringContent content = new System.Net.Http.StringContent(jsonstring, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(apiurl,content).Result;
            if (response.IsSuccessStatusCode)
            {
                res = response.Content.ReadAsAsync<CustomResponse>().Result;
            }
            else
            {
            }
            return res;

        }

        //<summary>Delete Method </summary><description>method takes apiaddress</description>
        public static CustomResponse Delete(string apiurl)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            HttpResponseMessage response = client.DeleteAsync(apiurl).Result;
            if (response.IsSuccessStatusCode)
            {
                res = response.Content.ReadAsAsync<CustomResponse>().Result;
            }
            return res;
        }

          

        
 
    }

}