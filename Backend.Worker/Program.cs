
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System;
using Backend.Worker.Models;
using Backend.Application.Commands;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context, builder) =>
    {
        builder.AddApplicationInsights(
        context.Configuration["ApplicationInsights:InstrumentationKey"]);

    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<IReceiverClient>((p) =>
        {

            var configuration = p.GetService<IConfiguration>();
            var connectionSring = configuration.GetConnectionString("ServiceBusConnection");
            var serviceBuses = configuration.GetSection("ServiceBus").Get<List<ServiceBus>>();
            var serviceBus = serviceBuses.Single(e => e.Name.Equals("Customers", StringComparison.OrdinalIgnoreCase));
            return new SubscriptionClient(connectionSring, serviceBus.Topic, serviceBus.Subscription);
        });
        //services.AddSingleton<IDistributorMicroservice, DistributorMicroservice>();
        services.AddHostedService<Worker>();
        services.AddMediatR(typeof(ApplicationCommand));
        services.AddHttpClient();
    })
    .Build();

await host.RunAsync();


