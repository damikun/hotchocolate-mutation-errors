
using ErrorHandling.Aplication.GraphQL.Interfaces;

namespace ErrorHandling.Aplication.GraphQL.Errors {
    public class BaseError : IBaseError {
            public string message { get; set; }
    }
}