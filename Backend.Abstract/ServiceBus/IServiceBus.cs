using Backend.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Abstract.ServiceBus
{
    public interface IServiceBus
    {
        Task Publish(IntegrationEvent @event);
    }
}
