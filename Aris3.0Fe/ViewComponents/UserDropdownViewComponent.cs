//using Aris3._0.Infrastructure.Data.Context;
//using Microsoft.AspNetCore.Mvc;

//namespace Aris3._0Fe.ViewComponents
//{
//    public class UserDropdownViewComponent
//    {
//        private readonly ArisDbContext dbContext;

//        public UserDropdownViewComponent(ArisDbContext dbContext)
//        {
//            this.dbContext = dbContext;
//        }
//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            var user = await dbContext.Accounts.ToListAsync();
//            return View(countries);
//        }
//    }
//}
