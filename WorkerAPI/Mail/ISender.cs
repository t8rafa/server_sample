using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerAPI.Mail
{
    public interface ISender
    {
        Response Send(string toName, string toEmail, string subject, string text);
    }
}