using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WorkerAPI.Mail.Mandrill
{
    public class MandrillApiRequest : IApiRequest
    {
        public string Domain { get; set; }

        public string Url
        { 
            get
            {
                return string.Format("https://mandrillapp.com/api/1.0/{0}", Domain);
            }
        }

        public HttpWebResponse ExecuteWithJson(string json)
        {
            HttpWebResponse result = null;

            var webRequest = WebRequest.Create(Url) as HttpWebRequest;

            try
            {
                webRequest.Method = "POST";
                webRequest.ServicePoint.Expect100Continue = false;
                webRequest.Timeout = 30000;
                webRequest.ContentType = "text/json";
                webRequest.AllowWriteStreamBuffering = true;

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                result = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException)
            {
                throw;
            }
            finally
            {
                webRequest = null;
            }

            return result;
        }
    }
}