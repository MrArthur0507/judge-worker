using JudgeContracts.ExecuteCodeConsumer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace HostExtensions.ServiceConfig
{
    public static class ServiceConfig {
    public static void LoadServices(this IHostBuilder builder) {
        builder.ConfigureServices((hostContext, services) =>
    {
        services.AddScoped<ILogger, Logger>();
        services.AddScoped<Sandbox>();
        services.AddScoped<ICodeExecutor, CodeExecutor>();
        services.AddScoped<ILanguageDetector, LanguageDetector>();
        services.AddMassTransit(x =>
        {
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq", "/", h =>
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

    });
    }
}
}
