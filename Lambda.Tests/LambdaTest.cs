using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Amazon.Lambda.SNSEvents.SNSEvent;

namespace Lambda.Tests
{
    public class LambdaTest
    {
        [Fact]
        public void CallLambda()
        {
            //Arrange
            var _iMediator = new Mock<IMediator>();


            _iMediator.Setup(x => x.Send(It.IsAny<object>(), It.IsAny<CancellationToken>())).Verifiable();

            var function = new BackendLambda.Function(_iMediator.Object);
            bool result = false;
            try
            {
                var handlerResult = function.FunctionHandler(new Amazon.Lambda.SNSEvents.SNSEvent()
                {
                    Records =
                    new List<SNSRecord>() { new SNSRecord() { Sns = new SNSMessage() { Message = "TestMessage" } } }
                }, null);

                result = handlerResult.IsCompletedSuccessfully;
                Assert.True(result);
            }
            catch (Exception ex)
            {
                Assert.True(result);
            }
        }
    }
}
