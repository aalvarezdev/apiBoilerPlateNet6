using Azure.Messaging.ServiceBus;
using Backend.Abstract.Models;
using Backend.Abstract.ServiceBus;
using Backend.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infraestructure.ServiceBus
{
    public class AzureServiceBus : IServiceBus
    {
        private readonly ILogger<AzureServiceBus> _log;
        private readonly string _connectionString;
        private readonly string _topicName;
        public AzureServiceBus(IConfiguration config, ILogger<AzureServiceBus> log)
        {
            _connectionString = config["ServiceBus:ConnectionString"];
            _topicName = config["ServiceBus:TopicName"];
            _log = log;
        }
        public async Task Publish(IntegrationEvent @event)
        {
            var serviceBusClient = new ServiceBusClient(_connectionString);
            var serviceBusChannel = serviceBusClient.CreateSender(_topicName);

            var message = new ServiceBusMessage(@event.Message);

            try
            {                      
                await serviceBusChannel.SendMessageAsync(message);        
            }
            finally
            {               
                await serviceBusChannel.DisposeAsync();
                await serviceBusClient.DisposeAsync();
            }
           
        }
    }
}
