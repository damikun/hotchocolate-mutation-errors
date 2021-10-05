using MediatR;
using System.Threading;
using FluentValidation;
using System.Threading.Tasks;
using ErrorHandling.Aplication.Payload;
using ErrorHandling.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ErrorHandling.Persistence;
using ErrorHandling.Aplication.GraphQL.Errors;
using ErrorHandling.Aplication.GraphQL.DTO;
using ErrorHandling.Aplication.Shared.Behaviours;

namespace ErrorHandling.Aplication.Commands {
    
    // [Authorize]
    // [Authorize(FieldPolicy = true)]
    public class CreateUser : IRequest<CreateUserPayload> {

        public string NickName {get; set;}

        public int Age {get; set;}
    }

    /// <summary>
    /// CreateUser field Authorization
    /// </summary>
    public class CreateUserAuthorizationValidator : AuthorizationValidator<CreateUser> {

        private readonly IDbContextFactory<AppDbContext> _factory;
        public CreateUserAuthorizationValidator(IDbContextFactory<AppDbContext> factory) {

            _factory = factory;

            // Add Field authorization cehcks..
        }
    }

    /// <summary>
    /// CreateUser Validator
    /// </summary>
    public class CreateUserValidator : AbstractValidator<CreateUser> {

        private readonly IDbContextFactory<AppDbContext> _factory;

        public CreateUserValidator(
            IDbContextFactory<AppDbContext> factory){

            _factory = factory;
            
            RuleFor(e => e.NickName)
            .NotEmpty()
            .NotNull();
            
            RuleFor(e => e.Age)
            .GreaterThan(18)
            .LessThan(100)
            .WithMessage("The agemust be between 18-100"); // Oh sorry Grandma :)

            RuleFor(e=>e.NickName)
            .MustAsync(HasUniqueName)
            .WithMessage("Nickname must be unique");
        }

        public async Task<bool> HasUniqueName(string name, CancellationToken cancellationToken) {
            
            await using AppDbContext dbContext = 
                _factory.CreateDbContext();

            return await dbContext.Users.AllAsync(e => e.NickName != name);
        }
    }

    /// <summary>
    /// ICreateUserError
    /// </summary>
    public interface ICreateUserError { }

    /// <summary>
    /// CreateUserPayload
    /// </summary>
    public class CreateUserPayload : BasePayload<CreateUserPayload, ICreateUserError> {
        public GQL_User user {get;set;}

    }

    /// <summary>Handler for <c>CreateUser</c> command </summary>
    public class CreateUserHandler : IRequestHandler<CreateUser, CreateUserPayload> {

        /// <summary>
        /// Injected <c>IDbContextFactory</c>
        /// </summary>
        private readonly IDbContextFactory<AppDbContext> _factory;

        /// <summary>
        /// Main constructor
        /// </summary>
        public CreateUserHandler(
             IDbContextFactory<AppDbContext> factory) {

            _factory = factory;
        }

        /// <summary>
        /// Command handler for <c>CreateUser</c>
        /// </summary>
        public async Task<CreateUserPayload> Handle(CreateUser request, CancellationToken cancellationToken) {

            await using AppDbContext dbContext = 
                _factory.CreateDbContext();

            User new_user = new User(){
                    NickName = request.NickName,
                    Age = request.Age
            };

            dbContext.Users.Add(new_user);

            await dbContext.SaveChangesAsync(cancellationToken);

            if(!string.IsNullOrWhiteSpace(new_user.Guid)){
                var payload = CreateUserPayload.Success();

                payload.user = new GQL_User(){
                    Guid = new_user.Guid,
                    NickName = new_user.NickName,
                    Age = new_user.Age
                };

                return payload;
            }else{
                return CreateUserPayload.Error(
                    new InternalServerError("Failed to create new user"));
            }

        }
    }
}