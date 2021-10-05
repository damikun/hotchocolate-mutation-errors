using ErrorHandling.Aplication.GraphQL.Errors;
using ErrorHandling.Aplication.GraphQL.Interfaces;
using HotChocolate.Types;

namespace ErrorHandling.Aplication.GraphQL.Types {

    public class BaseErrorType : ObjectType<BaseError> {
        protected override void Configure(IObjectTypeDescriptor<BaseError> descriptor) {

            descriptor.Implements<BaseErrorInterfaceType>();
        }
    }

    public class BaseErrorInterfaceType : InterfaceType<IBaseError> {
        protected override void Configure(IInterfaceTypeDescriptor<IBaseError> descriptor) {
            descriptor.Field(e => e.message).Type<StringType>();
        }
    }

}