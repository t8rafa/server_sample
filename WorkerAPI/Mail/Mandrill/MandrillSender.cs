using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WorkerAPI.Mail.Mandrill
{
    public class MandrillSender : ISender
    {
        private static MandrillSender instance;

        public IApiRequest WebRequest { get; set; }

        private MandrillSender()
        {
            WebRequest = new MandrillApiRequest();
            WebRequest.Domain = "messages/send.json";
        }

        public static MandrillSender Create()
        {
            if (instance == null)
            {
                instance = new MandrillSender();
            }

            return instance;
        }

        private MessageRequest PrepareRequest(string toName, string toEmail, string subject, string text)
        {
            var to = new To()
            {
                Name = toName,
                Email = toEmail
            };

            var message = new Message()
            {
                FromEmail = "rafael@t8studio.com.br",
                FromName = "Rafael Alencar Trisotto",
                To = new List<To>() { to },
                Subject = subject,
                Text = text
            };

            return new MessageRequest()
            {
                Key = "ib5AeJW6lO2pGgdGN0OMyw",
                Message = message
            };
        }

        private string SerializeToJson(Object value)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(value, settings);
        }

        public Response Send(string toName, string toEmail, string subject, string text)
        {
            var result = new Response();

            if (string.IsNullOrWhiteSpace(toName) ||
                string.IsNullOrWhiteSpace(toEmail) ||
                string.IsNullOrWhiteSpace(subject) ||
                string.IsNullOrWhiteSpace(text))
            {
                result.Status = "error";
                result.Message = "Incorrect arguments";
            }
            else
            {
                try
                {
                    var jsonObject = PrepareRequest(toName, toEmail, subject, text);
                    var json = SerializeToJson(jsonObject);

                    var response = WebRequest.ExecuteWithJson(json);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            var responseText = streamReader.ReadToEnd();
                            MessageResponse responseObject = null;
                            if (responseText.IndexOf('[') != -1)
                            {
                                var mandrill = JsonConvert.DeserializeObject<List<MessageResponse>>(responseText);
                                responseObject = mandrill.FirstOrDefault();
                            }
                            else
                            {
                                responseObject = JsonConvert.DeserializeObject<MessageResponse>(responseText);
                            }

                            result.Status = responseObject.Status;
                            result.Message = responseObject.Message;
                        }
                    }
                    else
                    {
                        result.Status = "error";
                        result.Message = "Malformed request";
                    }

                    return result;
                }
                catch (WebException)
                {
                    result.Status = "connection_error";
                    result.Message = "Can't connect to server";
                }
                catch (Exception)
                {
                    result.Status = "error";
                    result.Message = "Server is buggy";
                }
            }

            return result;
        }
    }
}