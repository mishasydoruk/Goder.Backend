using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Wrapper.Models
{
    public class RabbitMQOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
    }
}
