using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPSet.Models
{
    [Serializable]
    public class NetworkInterface
    {

        public string Ip { get; set; }

        public string Subnet { get; set; }

        public string  Gateway { get; set; }

        public string Proxy { get; set; }

        public string Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        
    }
}
