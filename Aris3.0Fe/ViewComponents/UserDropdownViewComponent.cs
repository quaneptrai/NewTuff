using Aris3._0.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aris3._0Fe.ViewComponents
{
    public class UserDropdownViewComponent:ViewComponent
    {
        private readonly ArisDbContext dbContext;

        public UserDropdownViewComponent(ArisDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await dbContext.Accounts.ToListAsync();
            return View(user);
        }
    }
}
