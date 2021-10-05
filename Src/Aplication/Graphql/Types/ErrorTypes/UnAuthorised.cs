using ErrorHandling.Aplication.GraphQL.Errors;
using HotChocolate.Types;

namespace ErrorHandling.Aplication.GraphQL.Types {

    public class UnAuthorisedType : ObjectType<UnAuthorised> {
        protected override void Configure(IObjectTypeDescriptor<UnAuthorised> descriptor) {
            descriptor.Implements<BaseErrorInterfaceType>();
        }
    }

}