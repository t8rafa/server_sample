using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkerAPI.Mail.Mandrill
{
    public class Message
    {
        [JsonProperty(PropertyName = "from_email")]
        public string FromEmail { get; set; }
        [JsonProperty(PropertyName = "from_name")]
        public string FromName { get; set; }
        public IList<To> To { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}