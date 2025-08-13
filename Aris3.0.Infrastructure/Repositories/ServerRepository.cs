using Aris3._0.Application.Interface.Repositories;
using Aris3._0.Domain.Entities;
using Aris3._0.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aris3._0.Infrastructure.Repositories
{
    public class ServerRepository : Repository<Server>, IServerRepository
    {
        private readonly ArisDbContext _dbContext;

        public ServerRepository(ArisDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Server>> GetByNameAsync(string serverName)
        {
            return await _dbContext.Servers
                .Where(s => s.ServerName == serverName)
                .ToListAsync();
        }

        public Task<int> GetTotalFilmForEachServer(Server server)
        {
            throw new NotImplementedException();
        }
    }
}
