using System;
using ErrorHandling.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorHandling.Configuration {
    public static partial class ServiceExtension {

        public static IServiceCollection AddDbContext(
            this IServiceCollection serviceCollection,
            IWebHostEnvironment Environment) {
                
                // AppDbContext
                serviceCollection.AddPooledDbContextFactory<AppDbContext>(
                (s, o) => o
                    .UseSqlite("Data Source=../Persistence/appDB.db", option => {

                    if (Environment.IsDevelopment()) {
                        o.EnableDetailedErrors();
                        o.EnableSensitiveDataLogging();
                    }

                    }).UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()));

            return serviceCollection;
        }


        public static IApplicationBuilder UseEnsureApiContextCreated(
            this IApplicationBuilder app_builder,
            IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory) {
                
            var serviceScopeFactory = app_builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using (var serviceScope = serviceScopeFactory.CreateScope()) {
                var _factory = serviceScope.ServiceProvider.GetService<IDbContextFactory<AppDbContext>>();

                 using AppDbContext dbContext =  _factory.CreateDbContext();

                dbContext.Database.EnsureCreated();
            }

            return app_builder;
        }
    }
}