using MediatR;
using HotChocolate;
using HotChocolate.Types;
using System.Threading.Tasks;
using HotChocolate.Types.Relay;
using ErrorHandling.Aplication.Commands;
using ErrorHandling.Aplication.GraphQL.DTO;

namespace ErrorHandling.Aplication.GraphQL.Mutation {

    /// <summary>
    /// User Mutation extension
    /// </summary>
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class UserMutations {

        /// <summary>
        // Create user input object
        /// </summary>
        public class CreateUserInput {

            public string NickName {get; set;}

            public int Age {get; set;}
        }

        /// <summary>
        /// Create user mutation
        /// </summary>
        /// <returns></returns>
        public async Task<CreateUserPayload> CreateUser(
            CreateUserInput request,
            [Service] IMediator _mediator) {

            return await _mediator.Send(new CreateUser(){
                NickName = request.NickName,
                Age = request.Age
            });
        }


        /// <summary>
        /// Remove user mutation
        /// </summary>
        /// <returns></returns>
        public async Task<RemoveUserPayload> RemoveUser(
            [ID(nameof(GQL_User))] string userId,
            [Service] IMediator _mediator) {

            return await _mediator.Send(new RemoveUser(){
                guid = userId
            });
        }
    }
}
