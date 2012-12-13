using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace WhereShouldWeEatLunch.APIs
{
    public class RESTfulAPI
    {
        protected static object GetRESTfulResponseString(string endpoint)
        {
            var myRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            myRequest.Method = "GET";

            // Invoke the request
            WebResponse response = myRequest.GetResponse();

            // Assemble result and return it
            StreamReader responseStream = new StreamReader(response.GetResponseStream());

            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            var data = serializer.Deserialize(responseStream.ReadToEnd(), typeof(object));
            responseStream.Close();
            response.Close();

            return data;
        }

    }
}