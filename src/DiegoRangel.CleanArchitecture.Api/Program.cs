using System;
using System.Threading.Tasks;
using DiegoRangel.CleanArchitecture.Api.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DiegoRangel.CleanArchitecture.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = SerilogConfiguration.Setup();

            try
            {
                Log.Information("Starting Amplifier web host");
                var webHost = CreateHostBuilder(args).Build();

                /*
                using (var scope = webHost.Services.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<YourService>();
                }
                */

                await webHost.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseSerilog();
                });
    }
}
