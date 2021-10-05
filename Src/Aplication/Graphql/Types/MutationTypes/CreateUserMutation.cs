
using ErrorHandling.Aplication.Commands;
using HotChocolate.Types;

namespace ErrorHandling.Aplication.GraphQL.Types {
    public class CreateUserPayloadType : ObjectType<CreateUserPayload> {
        protected override void Configure(IObjectTypeDescriptor<CreateUserPayload> descriptor) {

            // Add any graphql descriptor cfg for payload obj. in this palce
        }
    }

    public class CreateUserErrorUnion : UnionType<ICreateUserError> {
        protected override void Configure(IUnionTypeDescriptor descriptor) {
            descriptor.Type<ValidationErrorType>();
            descriptor.Type<UnAuthorisedType>();
            descriptor.Type<InternalServerErrorType>();
        }
    }
}