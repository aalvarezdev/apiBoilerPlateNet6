using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Worker.Models
{
    public class ServiceBus
    {
        public string Name { get; set; }

        public string Topic { get; set; }

        public string Subscription { get; set; }
    }
}
