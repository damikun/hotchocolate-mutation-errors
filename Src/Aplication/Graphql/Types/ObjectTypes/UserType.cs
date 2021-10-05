using HotChocolate.Types;
using HotChocolate.Resolvers;
using ErrorHandling.Aplication.GraphQL.DataLoaders;
using ErrorHandling.Aplication.GraphQL.DTO;

namespace ErrorHandling.Aplication.GraphQL.Types {

    /// <summary>
    /// Graphql UserType
    /// </summary>
    public class UserType : ObjectType<GQL_User> {

        protected override void Configure(IObjectTypeDescriptor<GQL_User> descriptor) {

            descriptor.ImplementsNode().IdField(t => t.Guid)
            .ResolveNode((ctx, id) =>
                 ctx.DataLoader<UserByIdDataLoader>()
                 .LoadAsync(id, ctx.RequestAborted));

            descriptor.Field("systemid").Type<NonNullType<StringType>>()
            .Resolve((IResolverContext context) => {
                return context.Parent<GQL_User>().Guid.ToString();
            });

        }

        private class UserResolvers {

        }
    }
}
