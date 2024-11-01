// Create and configure the Host for the worker
using JudgeContracts.ExecuteCodeConsumer;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Hosting;
using HostExtensions.ServiceConfig;

IHostBuilder builder = Host.CreateDefaultBuilder();
builder.LoadServices();

var host = builder.Build();

await host.RunAsync();