// Create and configure the Host for the worker
using JudgeWorker;
using MassTransit;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        
        services.AddMassTransit(x =>
        {
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                
                cfg.ReceiveEndpoint("execute-code-queue", e =>
                {
                    e.ConfigureConsumer<ExecuteCodeConsumer>(context);
                });
            });

            
            x.AddConsumer<ExecuteCodeConsumer>();
        });

        
        services.AddMassTransitHostedService();
    })
    .Build();

await host.RunAsync();