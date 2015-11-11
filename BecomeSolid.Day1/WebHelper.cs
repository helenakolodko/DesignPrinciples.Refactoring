using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Day1
{
    static class WebHelper
    {
        public static string GetResponseString(string url)
        {
            string responseString;
            WebRequest request = WebRequest.Create(url);        
            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                 responseString = streamReader.ReadToEnd();
            }
            return responseString;
        }
    }
}
