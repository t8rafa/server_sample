using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkerAPI.Mail.Mandrill
{
    public class MessageRequest
    {
        public string Key { get; set; }
        public Message Message { get; set; }
    }
}