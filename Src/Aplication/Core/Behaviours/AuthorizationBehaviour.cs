using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using FluentValidation;
using Serilog;
using FluentValidation.Results;
using System.Diagnostics;
using ErrorHandling.Aplication.Shared.Attributes;
using ErrorHandling.Aplication.Interfaces;
using ErrorHandling.Aplication.Payload;
using ErrorHandling.Aplication.GraphQL.Errors;
using ErrorHandling.Aplication.Shared.Exceptions;
using ErrorHandling.Domain;

namespace ErrorHandling.Aplication.Shared.Behaviours {

    /// <summary>
    /// Authorization marker interface for Fluent validation
    /// </summary>
    public interface IAuthorizationValidator { }

    /// <summary>
    /// Authorization validator wrapper
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public class AuthorizationValidator<TRequest> : AbstractValidator<TRequest>, IAuthorizationValidator {

    }

    /// <summary>
    /// Authorization behaviour for MediatR pipeline
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> {
        private readonly ICurrentUser _currentUserService;
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger _logger;

        public AuthorizationBehaviour(
            ICurrentUser currentUserService,
            IEnumerable<IValidator<TRequest>> validators,
             ILogger logger) {
            _currentUserService = currentUserService;
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {

            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any()) {

                var activity = Sources.DemoSource.StartActivity(
                String.Format("AuthorizationBehaviour: Request<{0}>", request.GetType().FullName), ActivityKind.Server);

                try {
                    activity.Start();

                    // Must be authenticated user
                    if (!_currentUserService.Exist) {
                        return HandleUnAuthorised(null);
                    }

                    // Role-based authorization
                    var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));
                    if (authorizeAttributesWithRoles.Any()) {
                        foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(','))) {
                            var authorized = false;

                            foreach (var role in roles) {

                                if (_currentUserService.HasRole(role.Trim())) {
                                    authorized = true;
                                    break;
                                }
                            }

                            // Must be a member of at least one role in roles
                            if (!authorized) {
                                return HandleUnAuthorised("Role authorization failure");
                            }
                        }
                    }

                    // Policy-based authorization
                    var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                    if (authorizeAttributesWithPolicies.Any()) {
                        foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy)) {
                            if (!_currentUserService.HasRole(policy.Trim())) {
                                return HandleUnAuthorised($"Policy: {policy} authorization failure");
                            }
                        }
                    }

                    // Inner command validator autorization checks
                    var authorizeAttributesWithInnerPolicies = authorizeAttributes.Where(
                        a => a.FieldPolicy == true);
                    if (authorizeAttributesWithInnerPolicies.Any()) {
                        IValidator<TRequest>[] authValidators = _validators.Where(
                            v => v is AuthorizationValidator<TRequest> ).ToArray();

                        ValidationFailure[] authorization_validator_failures = await CommandAuthValidationFailuresAsync(request, authValidators);

                        if (authorization_validator_failures.Any()) {
                            return HandleUnAuthorised(authorization_validator_failures);
                        }
                    }
                } catch (Exception ex) {
                     Common.CheckAndSetOtelExceptionError(ex,_logger);

                    // In case it is Mutation Response Payload = handled as payload error union
                    if (Common.IsSubclassOfRawGeneric(typeof(BasePayload<,>), typeof(TResponse))) {
                        return Shared.Common.HandleBaseCommandException<TResponse>(ex);
                    } else {
                        throw;
                    }
                } finally {
                    activity.Stop();
                    activity.Dispose();
                }
            }

            // Continue in pipe
            return await next();
        }

        private static TResponse HandleUnAuthorised(object error_obj) {

            // In case it is Mutation Response Payload = handled as payload error union
            if (Common.IsSubclassOfRawGeneric(typeof(BasePayload<,>), typeof(TResponse))) {
                IBasePayload payload = ((IBasePayload)Activator.CreateInstance<TResponse>());

                if (error_obj is ValidationFailure[]) {
                    foreach (var item in error_obj as ValidationFailure[]) {
                        payload.AddError(new UnAuthorised(item.CustomState, item.ErrorMessage));
                    }
                } else if (error_obj is string) {
                    payload.AddError(new UnAuthorised(error_obj as string));
                } else {
                    payload.AddError(new UnAuthorised());
                }

                return (TResponse)payload;
            } else {
                // In case it is query response = handled by global filter
                if (error_obj is ValidationFailure[]) {
                    throw new AuthorizationException(error_obj as ValidationFailure[]);
                } else if (error_obj is string) {
                    throw new AuthorizationException(error_obj as string);
                } else {
                    throw new AuthorizationException();
                }
            }
        }

        private static async Task<ValidationFailure[]> CommandAuthValidationFailuresAsync(TRequest request, IEnumerable<IValidator<TRequest>> authValidators) {
            var validateTasks = authValidators
                .Select(v => v.ValidateAsync(request));
            var validateResults = await Task.WhenAll(validateTasks);
            var validationFailures = validateResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToArray();

            if (validationFailures == null) {
                return new ValidationFailure[0];
            } else {
                return validationFailures;
            }

        }
    }
}