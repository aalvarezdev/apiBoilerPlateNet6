
using Backend.Worker.Models;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Workers.CustomerWorker
{
    public class Worker : BackgroundService
    {


        public Worker(ILogger<Worker> logger, IReceiverClient receiverClient, IMediator mediator) 
        {

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        
    }
}