using EasyNetQ;
using Serilog;
using Quartz.Services.ImageListener.Worker;
using Serilog.Exceptions;
using Quartz.Shared.Messaging;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    { 
        services.AddMessagingServices(context.Configuration);
        services.AddHostedService<ImageConsumerService>();
    })
    //var builder = Host.CreateDefaultBuilder(args)
    //.ConfigureServices((context, services) =>
    //{
    //    services.AddSingleton<IBus>(_ => RabbitHutch.CreateBus(context.Configuration.GetSection("RabbitMQ")
    //        .Get<RabbitMQConfig>()!.ConnectionString));
    //    services.AddHostedService<ImageConsumerService>();
    //})
    .UseSerilog((context, loggerConfig) =>
    {
        loggerConfig.WriteTo.Console()
    .Enrich.WithExceptionDetails()
    .WriteTo.Seq("http://localhost:5341");

    });

//var builder = Host.CreateApplicationBuilder(args);


//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .WriteTo.Seq("http://localhost:5341")
//    .CreateLogger();


//Host.CreateDefaultBuilder(args)
//    .ConfigureLogging(logging =>
//    {
//        logging.ClearProviders();
//        logging.AddSerilog();
//    })
//    .UseSerilog((context, loggerConfig) =>
//    {
//        loggerConfig.WriteTo.Seq("http://localhost:5341")
//        .Enrich.WithExceptionDetails();

//    });


//builder.Logging.AddSerilog(new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    //.Enrich.FromLogContext()
//    .Enrich.WithExceptionDetails()
//    .WriteTo.Seq("http://localhost:5341")
//    .CreateLogger());

// Configure your message bus connection

//var rabbitMQ = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();
//builder.Services.AddSingleton<IBus>(_ => RabbitHutch.CreateBus(rabbitMQ!.ConnectionString));
//builder.Services.AddHostedService<ImageConsumerService>();


var host = builder.Build();
host.Run();


