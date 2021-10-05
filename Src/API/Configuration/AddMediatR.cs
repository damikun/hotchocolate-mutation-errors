using MediatR;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ErrorHandling.Aplication.Commands;
using ErrorHandling.Aplication.Shared.Behaviours;

namespace ErrorHandling.Configuration {
    public static partial class ServiceExtension {
        public static IServiceCollection AddMediatR(this IServiceCollection services) {

            services.AddMediatR(typeof(CreateUser).GetTypeInfo().Assembly);

            services.AddValidatorsFromAssembly(typeof(CreateUserValidator).GetTypeInfo().Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExBehaviour<,>));

            return services;
        }
    }
}