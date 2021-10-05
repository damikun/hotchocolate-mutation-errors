using MediatR;
using System.Linq;
using System.Threading;
using FluentValidation;
using System.Threading.Tasks;
using ErrorHandling.Aplication.Payload;
using Microsoft.EntityFrameworkCore;
using ErrorHandling.Persistence;
using ErrorHandling.Domain.Models;
using ErrorHandling.Aplication.GraphQL.Errors;
using ErrorHandling.Aplication.Shared.Behaviours;

namespace ErrorHandling.Aplication.Commands {

    // [Authorize]
    // [Authorize(FieldPolicy = true)]
    public class RemoveUser : IRequest<RemoveUserPayload> {

        public string guid {get;set;}
    }

    /// <summary>
    /// RemoveUser Validator
    /// </summary>
    public class RemoveUserValidator : AbstractValidator<RemoveUser> {

        public RemoveUserValidator(){

            RuleFor(e => e.guid)
            .NotEmpty()
            .NotNull();
        }
    }

    /// <summary>
    /// IRemoveUserError
    /// </summary>
    public interface IRemoveUserError { }

    /// <summary>
    /// RemoveUserPayload
    /// </summary>
    public class RemoveUserPayload : BasePayload<RemoveUserPayload, IRemoveUserError> {
        public string removedId {get;set;}

    }

    /// <summary>
    /// CreateUser field Authorization
    /// </summary>
    public class RemoveUserAuthorizationValidator : AuthorizationValidator<RemoveUserValidator> {

        private readonly IDbContextFactory<AppDbContext> _factory;
        public RemoveUserAuthorizationValidator(IDbContextFactory<AppDbContext> factory) {

            _factory = factory;

            // Add Field authorization cehcks..
        }
    }


    /// <summary>Handler for <c>RemoveUser</c> command </summary>
    public class RemoveUserHandler : IRequestHandler<RemoveUser, RemoveUserPayload> {

        /// <summary>
        /// Injected <c>IDbContextFactory</c>
        /// </summary>
        private readonly IDbContextFactory<AppDbContext> _factory;

        /// <summary>
        /// Main constructor
        /// </summary>
        public RemoveUserHandler(
             IDbContextFactory<AppDbContext> factory) {

            _factory = factory;
        }

        /// <summary>
        /// Command handler for <c>RemoveUser</c>
        /// </summary>
        public async Task<RemoveUserPayload> Handle(RemoveUser request, CancellationToken cancellationToken) {

            await using AppDbContext dbContext = 
                _factory.CreateDbContext();

            User user = await dbContext.Users.Where(
                e=>e.Guid == request.guid)
                    .FirstOrDefaultAsync(cancellationToken);

            if(user == null){
                return  RemoveUserPayload.Error(
                    new UserNotFoundError(
                        string.Format("User with id: {0} was not found",request.guid)));
            }

            string removed_user_ig = user.Guid;

            dbContext.Users.Remove(user);

            await dbContext.SaveChangesAsync(cancellationToken);

            var payload = RemoveUserPayload.Success();

            payload.removedId = removed_user_ig;

            return payload;
        }
    }
}