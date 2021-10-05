
using ErrorHandling.Aplication.GraphQL.Errors;
using HotChocolate.Types;

namespace ErrorHandling.Aplication.GraphQL.Types {

    public class InternalServerErrorType : ObjectType<InternalServerError> {
        protected override void Configure(IObjectTypeDescriptor<InternalServerError> descriptor) {
            descriptor.Implements<BaseErrorInterfaceType>();
        }
    }

}