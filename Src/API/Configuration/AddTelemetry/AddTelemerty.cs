using System;
using OpenTelemetry.Trace;
using ErrorHandling.Domain;
using OpenTelemetry.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorHandling.Configuration {
    public static partial class ServiceExtension {

        public static IServiceCollection AddTelemerty(
            this IServiceCollection serviceCollection,
            IConfiguration Configuration, IWebHostEnvironment Environment) {

            serviceCollection.AddOpenTelemetryTracing((builder) => {
    
                builder.AddSource(Sources.DemoSource.Name);

                builder.SetResourceBuilder(ResourceBuilder
                  .CreateDefault()
                  .AddService(Environment.ApplicationName));

                builder.AddAspNetCoreInstrumentation(opts => {
                    opts.RecordException = true;
                });

                builder.AddElasticsearchClientInstrumentation();

                builder.AddSqlClientInstrumentation();

                builder.AddHttpClientInstrumentation(
                    opts => opts.RecordException = true);

                builder.AddEntityFrameworkCoreInstrumentation(
                    e => e.SetDbStatementForText = true);

                builder.AddOtlpExporter(options => {
                    options.Endpoint = new Uri("http://localhost:55680");             
                });
            });

            return serviceCollection;
        }
    }
}