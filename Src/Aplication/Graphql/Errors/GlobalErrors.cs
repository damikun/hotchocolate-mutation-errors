
using ErrorHandling.Aplication.Commands;

namespace ErrorHandling.Aplication.GraphQL.Errors {

    public class UnAuthorised : BaseError, ICreateUserError, IRemoveUserError {
        public UnAuthorised() {
            this.message = "Unauthorised to process or access resource";
        }

        public UnAuthorised(string s) {

            this.message = s;
        }

        public UnAuthorised(object content, string message) {

            this.message = message;
        }
    }

    public class InternalServerError : BaseError, ICreateUserError, IRemoveUserError {

        public InternalServerError() {
            this.message = "Internal server error";
        }

        public InternalServerError(string s) {
            this.message = s;
        }
    }

    public class ValidationError : BaseError, ICreateUserError, IRemoveUserError {
        public ValidationError() {
            this.message = "Some parameter/s (fields) are invalid";
        }

        public ValidationError(string s) {
            this.message = s;
        }

        public ValidationError(string propName, string message) {
            this.message = message;
            this.FieldName = propName;
        }
        #nullable enable
        public string? FieldName { get; set; }
        #nullable disable
    }

    
    public class UserNotFoundError : BaseError, IRemoveUserError {

        public UserNotFoundError() {
            this.message = "User was not found";
        }

        public UserNotFoundError(string s) {
            this.message = s;
        }
    }


}