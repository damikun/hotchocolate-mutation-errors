using ErrorHandling.Aplication.GraphQL.Errors;
using HotChocolate.Types;

namespace ErrorHandling.Aplication.GraphQL.Types {

    public class ValidationErrorType : ObjectType<ValidationError> {
        protected override void Configure(IObjectTypeDescriptor<ValidationError> descriptor) {
               descriptor.Implements<BaseErrorInterfaceType>();
        }
    }

}