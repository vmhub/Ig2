using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Ig2.Models.GetRates
{
    public static class Rates
    {
        public static string getJson(string val)
        {
            WebClient client = new WebClient();
            string url = "http://api.fixer.io/latest?base=" + val;
            return client.DownloadString(url);
        }
    }
}