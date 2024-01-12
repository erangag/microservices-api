using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderConsumer.API.Configurations;
using OrderConsumer.API.EventBusConsumer;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
    config.AddConsumer<EquityOrderConsumer>();
    config.AddConsumer<DeskOrderConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));

        cfg.ReceiveEndpoint(EventBusConstants.EquityOrderQueue, c =>
        {
            c.ConfigureConsumer<EquityOrderConsumer>(ctx);
        });

        cfg.ReceiveEndpoint(EventBusConstants.DeskOrderQueue, c =>
        {
            c.ConfigureConsumer<DeskOrderConsumer>(ctx);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
