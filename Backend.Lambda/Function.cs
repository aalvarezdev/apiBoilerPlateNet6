using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Backend.Abstract.Models;
using Backend.Abstract.ServiceBus;
using Backend.Application.Commands;
using Backend.Application.Interfaces;
using Backend.Infraestructure.ServiceBus;
using BackendLambda;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static Amazon.Lambda.SNSEvents.SNSEvent;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BackendLambda;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>ç
    /// 


    private IMediator _mediator;
    private ServiceCollection _services;

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
        _services = new ServiceCollection();
        ConfigureServices(_services);
        var serviceProvider = _services.BuildServiceProvider();
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
        foreach (SNSRecord record in evnt.Records)
        {
            Console.WriteLine($"Incoming value :  {record.Sns?.Message}");
            IntegrationEvent integrationEvent = new IntegrationEvent(record.Sns?.Message);
            Console.WriteLine("Attempting to send message");          
            SendMessageCommand command = new SendMessageCommand() { IntegrationEvent = integrationEvent };
            await _mediator.Send(command, new CancellationToken());
        }
    }

    private void ConfigureServices(IServiceCollection services)
    {

        IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();

        services.AddScoped<IConfiguration>(_ => configuration);
        //services.AddSingleton<ILambdaConfiguration, FunctionConfiguration>();
        services.AddLogging(config => config.AddConsole());
        services.AddTransient<IServiceBus, AzureServiceBus>();
        services.AddMediatR(typeof(SendMessageCommandHandler).Assembly);
    }
}