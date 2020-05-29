using System;
using DiegoRangel.CleanArchitecture.Api.Extensions;
using DiegoRangel.CleanArchitecture.Api.Views.Shared;
using DiegoRangel.CleanArchitecture.Infra.CrossCutting.Settings;
using DiegoRangel.CleanArchitecture.Infra.IoC;
using DiegoRangel.DotNet.Framework.CQRS.API.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.API.Filters;
using DiegoRangel.DotNet.Framework.CQRS.API.Temp;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Mailing;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.SMS;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.UserAgent;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DiegoRangel.CleanArchitecture.Api
{
    public class Startup
    {
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
            _appSettings = _configuration.GetSection("AppSettings").Get<AppSettings>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly,
                typeof(Domain.Common.ProjectIdentifier).Assembly,
                typeof(Infra.DataAccess.Common.ProjectIdentifier).Assembly,
                typeof(Infra.CrossCutting.Common.ProjectIdentifier).Assembly,
                typeof(Infra.IoC.Common.ProjectIdentifier).Assembly,
            };

            services.AddAppSettings(() => _appSettings);
            services.AddCorsWithDefaultPolicy(_appSettings.FrontEndUrl);

            //The ResponseValidationFilter is useful to automatically prevent the pipeline to send http ok responses which has validation errors.
            services.AddControllers(options => options.Filters.Add<ResponseValidationFilter>())
                .AddNewtonsoftJson();

            services.AddCulture("en-US");
            services.AddHealthChecks();
            services.AddCacheServices();
            services.AddCompression();
            services.AddRepositoryRejectionOnControllers(assemblies);
            services.AddUserAgentService();

            services.AddTwilio(_env.IsDevelopment, _configuration.GetSection("TwilioSettings").Get<TwilioSettings>);

            services.AddMailServices(_env.IsDevelopment, _configuration.GetSection("MailSettings").Get<MailSettings>)
                .WithTemplatingRenderers(services, settings =>
                {
                    settings.EmailTemplatesDiscoveryType = typeof(EmailTemplatesDiscovery);
                    settings.EmailTemplatesNamespace = "<Add here your EmailTemplates Namespace>";
                });

            services.AddSwaggerDocumentation(() => new SwaggerSettings
            {
                ApiTitle = "<Add here your API Title>",
                ApiDescription = "<Add here your API description>",
                ApiContactInfo = "<Add here your contact info>",
                SecureWithUseJwtAuth = true
            });

            services.AddCommonMessages(() => new CommonMessages
            {
                NotFound = "<Add here a common not found message>",
                InvalidOperation = "<Add here a common invalid operation message>",
                UnhandledOperation = "<Add here a common unhandled exception message>"
            });

            services.AddAutoMapperWithSettings(settings =>
            {
                settings.UseStringTrimmingTransformers = true;
            }, assemblies);

            services.AddMediatr(typeof(Domain.Common.ProjectIdentifier).Assembly)
                .AddRequestLoggerBehavior()
                .AddRequestPerformanceBehavior();

            services.AddEfCoreServices();

            //Replace by your providers
            services.AddUserSignedInServices<TempUser, Guid,
                TempLoggedInUserProvider,
                TempLoggedInUserIdProvider,
                TempLoggedInUserIdentifierProvider>();

            Bootstrapper.RegisterServicesBasedOn<Guid>(services, assemblies);
            AppInitializer.RegisterMyApplicationModules(services, _configuration.GetConnectionString("DefaultConnection"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseRouting();
            app.UseExceptionHandlers();
            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSwaggerDocumentation(() => new SwaggerUiSettings
            {
                ApiTitle = "<Add here your API Title>",
                ApiDocExpansion = DocExpansion.Full
            });
        }
    }
}
