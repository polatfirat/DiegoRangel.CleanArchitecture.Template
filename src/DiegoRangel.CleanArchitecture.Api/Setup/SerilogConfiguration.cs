using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DiegoRangel.CleanArchitecture.Api.Setup
{
    public static class SerilogConfiguration
    {
        public static Logger Setup() => new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                @"logs\app.log",
                fileSizeLimitBytes: 10000000,
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: 10,
                shared: true,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }
}