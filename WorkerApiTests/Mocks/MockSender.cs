using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerAPI.Mail;

namespace WorkerApiTests.Mocks
{
    public class MockSender : ISender
    {
        public bool IsValidReturn { get; set; }
        public Response Send(string toName, string toEmail, string subject, string text)
        {
            var response = new Response();
            response.Status = IsValidReturn ? "sent" : "error";

            return response;
        }
    }
}
