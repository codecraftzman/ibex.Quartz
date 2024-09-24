using Quartz.Services.ImageService.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder
       .ConfigureServices()
       .ConfigurePipeline();

//await app.ResetDatabaseAsync();

app.Run();

