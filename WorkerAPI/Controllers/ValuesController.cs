using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WorkerAPI.Models;
using WorkerAPI.Mail;
using WorkerAPI.Mail.Mandrill;

namespace WorkerAPI.Controllers
{
    public class ValuesController : ApiController
    {
        public ISender sender { get; set; }

        public ValuesController()
        {
            sender = MandrillSender.Create();
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public CandidateResponse Post([FromBody]Candidate value)
        {
            var result = new CandidateResponse();

            if (value == null || string.IsNullOrWhiteSpace(value.Name) || string.IsNullOrWhiteSpace(value.Email))
            {
                result.Status = "error";
                result.Code = "1";
                result.Message = "Incorrect arguments";
            }
            else
            {
                var messages = CandidateClassificator.GetMessages(value);

                result.Status = "success";
                result.Code = "0";
                result.Message = "Success";

                foreach (var item in messages)
                {
                    var response = sender.Send(value.Name, value.Email, "Obrigado por se candidatar", item.Value);

                    if (response.Status != "sent")
                    {
                        result.Status = response.Status;
                        result.Code = "2";
                        result.Message = "Can't send email";
                    }
                }
            }

            return result;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
