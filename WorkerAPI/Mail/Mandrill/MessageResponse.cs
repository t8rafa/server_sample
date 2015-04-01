using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkerAPI.Mail.Mandrill
{
    public class MessageResponse
    {
        public string Status { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string RejectReason { get; set; }
        public string _Id { get; set; }
    }
}