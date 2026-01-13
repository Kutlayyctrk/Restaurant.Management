using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.OuterInfrastructure.Tools
{
    public class SmtpOptions
    {
        public string Host { get; set; } = "";
        public int Port { get; set; } = 587;
        public bool EnableSSl { get; set; } = true;

        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string FromName { get; set; } = "";
    }
}
