using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkerAPI.Models
{
    public class Candidate
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Html { get; set; }
        public int Css { get; set; }
        public int JavaScript { get; set; }
        public int Python { get; set; }
        public int Django { get; set; }
        public int iOS { get; set; }
        public int Android { get; set; }
    }
}