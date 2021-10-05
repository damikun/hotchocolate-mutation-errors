using System;
using GreenDonut;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErrorHandling.Persistence;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ErrorHandling.Aplication.GraphQL.DTO;

namespace ErrorHandling.Aplication.GraphQL.DataLoaders {

    public class UserByIdDataLoader : BatchDataLoader<string, GQL_User> {

        /// <summary>
        /// Injected <c>AppDbContext</c>
        /// </summary>
        private readonly IDbContextFactory<AppDbContext> _factory;

        public UserByIdDataLoader(
            IBatchScheduler scheduler,
            IDbContextFactory<AppDbContext> factory) : base(scheduler) {
            _factory = factory;
        }

        protected override async Task<IReadOnlyDictionary<string, GQL_User>> LoadBatchAsync(
            IReadOnlyList<string> keys,
            CancellationToken cancellationToken) {

            await using AppDbContext dbContext = 
                _factory.CreateDbContext();

            return await dbContext.Users
            .AsNoTracking()
            .Where(s => keys.Contains(s.Guid))
            .Select(e => new GQL_User {
                Guid = e.Guid,
                NickName = e.NickName,
                Age = e.Age
            }).ToDictionaryAsync(t => t.Guid, cancellationToken);

        }
    }
}