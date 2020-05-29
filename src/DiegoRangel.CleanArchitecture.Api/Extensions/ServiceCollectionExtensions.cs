using System;
using DiegoRangel.CleanArchitecture.Infra.CrossCutting.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.CleanArchitecture.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static AppSettings AddAppSettings(this IServiceCollection services, Func<AppSettings> setupAction)
        {
            var settings = setupAction();
            services.AddSingleton(settings);
            return settings;
        }

        public static void AddCorsWithDefaultPolicy(this IServiceCollection services, string frontEndUrl)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.WithOrigins(frontEndUrl);
                    policy.AllowCredentials();
                });
            });
        }
    }
}