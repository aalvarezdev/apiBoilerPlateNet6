using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Backend.Abstract.Models;
using Backend.Abstract.ServiceBus;
using Backend.Application.Commands;
using Backend.Application.Interfaces;
using Backend.Infraestructure.ServiceBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using static Amazon.Lambda.SNSEvents.SNSEvent;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Backend.Lambda;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>ç
    /// 
 
   
    private IMediator _mediator;


    /// <summary>
    /// This constructor is for unit testing only. Amazon will call the parameterless constructor by default;
    /// </summary>
    /// <param name="mediator"></param>
    public Function(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Function()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();        
        this._mediator = serviceProvider.GetService<IMediator>();
    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SNS event object and can be used 
    /// to respond to SNS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(SNSEvent evnt, ILambdaContext context)
    {
        foreach(SNSRecord record in evnt.Records)
        {
            IntegrationEvent integrationEvent = new IntegrationEvent(record.Sns?.Message);
            SendMessageCommand command = new SendMessageCommand() { IntegrationEvent = integrationEvent };
            await _mediator.Send(command, new CancellationToken());
        }
    }

   


    private void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ILambdaConfiguration, FunctionConfiguration>();
        services.AddMediatR(typeof(SendMessageCommand));
        services.AddTransient<IServiceBus, AzureServiceBus>();
    }
}