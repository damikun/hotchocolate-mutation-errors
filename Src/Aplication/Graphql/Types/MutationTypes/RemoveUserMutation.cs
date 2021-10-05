using ErrorHandling.Aplication.Commands;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace ErrorHandling.Aplication.GraphQL.Types {
    public class RemoveUserPayloadType : ObjectType<RemoveUserPayload> {
        protected override void Configure(IObjectTypeDescriptor<RemoveUserPayload> descriptor) {

            descriptor.Field(e => e.removedId).Type<IdType>().Resolve(ctx => {

                IIdSerializer serializer = ctx.Service<IIdSerializer>();

                return serializer.Serialize(default, "GQL_User", ctx.Parent<RemoveUserPayload>().removedId);
            });
        }
    }

    public class RemoveUserErrorUnion : UnionType<IRemoveUserError> {
        protected override void Configure(IUnionTypeDescriptor descriptor) {
            descriptor.Type<ValidationErrorType>();
            descriptor.Type<UnAuthorisedType>();
            descriptor.Type<InternalServerErrorType>();
        }
    }
}