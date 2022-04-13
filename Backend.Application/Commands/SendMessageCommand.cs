using Backend.Abstract.Models;
using Backend.Abstract.ServiceBus;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Application.Commands
{
    public class SendMessageCommand : IRequest<bool>
    {
        public IntegrationEvent IntegrationEvent { get; set; }
    }

    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, bool>
    {
        private readonly  ILogger<SendMessageCommandHandler> _logger;
        private readonly IServiceBus _serviceBus;

        public SendMessageCommandHandler(ILogger<SendMessageCommandHandler> logger, IServiceBus serviceBus)
        {
            _logger = logger;
            _serviceBus = serviceBus;
        }

        public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceBus.Publish(request.IntegrationEvent);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
