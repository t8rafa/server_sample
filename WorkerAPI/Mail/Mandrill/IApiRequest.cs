using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorkerAPI.Mail.Mandrill
{
    public interface IApiRequest
    {
        string Domain { get; set; }
        HttpWebResponse ExecuteWithJson(string json);
    }
}
