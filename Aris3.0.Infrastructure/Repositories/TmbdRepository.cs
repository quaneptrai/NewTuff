using Aris3._0.Application.Interface.Repositories;
using Aris3._0.Domain.Entities;
using Aris3._0.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aris3._0.Infrastructure.Repositories
{
    public class TmbdRepository : Repository<Tmbd>, ITmbdRepository
    {
        private readonly ArisDbContext dbContext;

        public TmbdRepository(ArisDbContext dbContext):base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<Tmbd> GetByID(string id)
        {
            return await dbContext.Tmbd.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
