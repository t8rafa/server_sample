using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkerAPI.Mail.Mandrill
{
    public class To
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Type
        {
            get
            {
                return "to";
            }
        }
    }
}