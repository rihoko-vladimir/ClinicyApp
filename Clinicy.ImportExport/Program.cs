using Clinicy.ImportExport.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console().CreateLogger();
Log.Logger = logger;
Log.Information("Application is starting up...");

builder.Services.AddApplication(builder.Configuration);

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.Enrich.WithProperty("ServiceName", "ImportExport");
    lc.WriteTo.Console(outputTemplate:
            "[{ServiceName} {Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
        .ReadFrom.Configuration(ctx.Configuration);
});

var app = builder.Build();

app.Run();

Log.Information("Application is shutting down...");
Log.CloseAndFlush();