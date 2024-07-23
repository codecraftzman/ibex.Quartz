using EasyNetQ;
using Quartz.Services.ImageListener.Worker;
using Quartz.Shared.Contracts;
using Quartz.Shared.Integration;

var builder = Host.CreateApplicationBuilder(args);

// Configure your message bus connection
var rabbitMQ = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();
//builder.Services.AddSingleton<IMessageBusService>(_ => new MessageBusService(rabbitMQ!.ConnectionString));
builder.Services.AddSingleton<IBus>(_ => RabbitHutch.CreateBus(rabbitMQ!.ConnectionString));
builder.Services.AddHostedService<ImageConsumerService>();

//builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();


