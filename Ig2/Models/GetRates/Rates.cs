using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Ig2.Models.GetRates
{
    public static class Rates
    {
        /// <summary>
        /// Returning currency JSON
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string getJson(string val)
        {   
            WebClient client = new WebClient();
            string url = "http://api.fixer.io/latest?base=" + val;
            try 
            {
              return client.DownloadString(url);
            }
            catch(WebException ex)
            {
              return "{\"error\":\"error\", \"msg\":\"" + ex.ToString() + "\"}";
                                                   
            }
         
        }
    }
}