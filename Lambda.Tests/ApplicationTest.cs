using Backend.Abstract.Models;
using Backend.Abstract.ServiceBus;
using Backend.Application.Commands;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Lambda.Tests
{
    public class ApplicationTest
    {
        [Fact]
        public async Task SendMessage()
        {
            //Arrange
            var _ILogger = new Mock<ILogger<SendMessageCommandHandler>>();
            var _IServiceBus = new Mock<IServiceBus>();

            _ILogger.Setup(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>())).Verifiable();
            _IServiceBus.Setup(x => x.Publish(It.IsAny<IntegrationEvent>()));

            var command = new SendMessageCommand() { IntegrationEvent = new IntegrationEvent("Service Bus Message") };
            var commandHandler = new SendMessageCommandHandler(_ILogger.Object, _IServiceBus.Object);
            bool result = false;
            try
            {
                result = await commandHandler.Handle(command, new());
                Assert.True(result);
            }
            catch (Exception ex)
            {
                Assert.True(result);
            }
        }
    }
}