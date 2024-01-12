using EventBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.Consumer.Configurations;

namespace Order.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();   
            IConfiguration configuration = BuildConfig(builder);
            
            Console.WriteLine("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddAutoMapper(typeof(MapperConfig));
                    // MassTransit-RabbitMQ Configuration
                    services.AddMassTransit(config => {
                        config.AddConsumer<OrderConsumer>();

                        config.UsingRabbitMq((ctx, cfg) => {
                            cfg.Host(configuration.GetValue<string>("EventBusSettings:HostAddress"));

                            cfg.ReceiveEndpoint(EventBusConstants.EquityOrderQueue, c =>
                            {
                                c.ConfigureConsumer<OrderConsumer>(ctx);
                            });
                        });
                    });

                    services.AddScoped<OrderConsumer>();
                })                
                .Build();

            host.Run(); // Run the host instead of Console.ReadLine()
        }

        static IConfiguration BuildConfig(IConfigurationBuilder builder)
        {
            return builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables().Build();           
        }
    }
}