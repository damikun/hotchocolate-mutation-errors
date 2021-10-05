using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using ErrorHandling.Persistence;
using Microsoft.EntityFrameworkCore;
using ErrorHandling.Aplication.GraphQL.DTO;
using ErrorHandling.Aplication.GraphQL.Types;
using ErrorHandling.Aplication.GraphQL.Extensions;

namespace ErrorHandling.Aplication.GraphQL.Queries {

    /// <summary>
    /// UserQueries
    /// </summary>
    [ExtendObjectType(OperationTypeNames.Query)]
    public class UserQueries {

        [UseAppDbContextAttribute]
        [UsePaging(typeof(UserType))]
        [UseFiltering]
        public IQueryable<GQL_User> GetUsers(
        [ScopedService] AppDbContext context) {

            return context.Users
                .AsNoTracking()
                .Select(e=>  new GQL_User {
                    Guid = e.Guid,
                    NickName = e.NickName,
                    Age = e.Age,
                });
        }
    }
}
