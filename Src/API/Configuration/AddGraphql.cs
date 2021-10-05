using System;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ErrorHandling.Aplication.GraphQL.Queries;
using ErrorHandling.Aplication.GraphQL.Mutation;
using ErrorHandling.Aplication.GraphQL.Types;
using ErrorHandling.Aplication.GraphQL.DataLoaders;

namespace ErrorHandling.Configuration {
    public static partial class ServiceExtension {
        public static IServiceCollection AddGraphql(
            this IServiceCollection serviceCollection, IWebHostEnvironment env) {

            serviceCollection.AddGraphQLServer()
                    .SetPagingOptions(
                        new PagingOptions { IncludeTotalCount = true, MaxPageSize = 100 })
                    .ModifyRequestOptions(requestExecutorOptions => {
                        if (env.IsDevelopment() ||
                            System.Diagnostics.Debugger.IsAttached) {
                            requestExecutorOptions.ExecutionTimeout = TimeSpan.FromMinutes(1);
                        }
                        
                         requestExecutorOptions.IncludeExceptionDetails = !env.IsProduction();
                    })

                    .AddGlobalObjectIdentification()
                    .AddQueryFieldToMutationPayloads()

                    .AddFiltering()
                    .AddSorting()

                    .AddQueryType<Query>()
                        .AddTypeExtension<SystemQueries>()
                        .AddTypeExtension<UserQueries>()
                    .AddMutationType<Mutation>()
                        .AddTypeExtension<UserMutations>()

                    .BindRuntimeType<DateTime, DateTimeType>()
                    .BindRuntimeType<int, IntType>()

                    .AddType<InternalServerErrorType>()
                    .AddType<UnAuthorisedType>()
                    .AddType<ValidationErrorType>()
                    .AddType<BaseErrorType>()
                    .AddType<UserType>()
                    .AddType<BaseErrorInterfaceType>()

                    .AddDataLoader<UserByIdDataLoader>()

                    .AddType<CreateUserPayloadType>()
                    .AddType<CreateUserErrorUnion>()
                    .AddType<RemoveUserPayloadType>()
                    .AddType<RemoveUserErrorUnion>();
                
            return serviceCollection;
        }
    }
}