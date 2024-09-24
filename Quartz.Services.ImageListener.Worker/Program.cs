using EasyNetQ;
using Serilog;
using Quartz.Services.ImageListener.Worker;
using Quartz.Shared.Contracts;
using Quartz.Shared.Integration.Events;
using Serilog.Exceptions;

var builder = Host.CreateApplicationBuilder(args);


//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .WriteTo.Seq("http://localhost:5341")
//    .CreateLogger();


Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddSerilog();
    })
    .UseSerilog((context, loggerConfig) =>
    {
        loggerConfig.WriteTo.Seq("http://localhost:5341")
        .Enrich.WithExceptionDetails();

    });
    

//builder.Logging.AddSerilog(new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    //.Enrich.FromLogContext()
//    .Enrich.WithExceptionDetails()
//    .WriteTo.Seq("http://localhost:5341")
//    .CreateLogger());

// Configure your message bus connection
var rabbitMQ = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();
//builder.Services.AddSingleton<IMessageBusService>(_ => new MessageBusService(rabbitMQ!.ConnectionString));
builder.Services.AddSingleton<IBus>(_ => RabbitHutch.CreateBus(rabbitMQ!.ConnectionString));
builder.Services.AddHostedService<ImageConsumerService>();

//builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();


