using Aris3._0.Infrastructure.Data.Context;
using Aris3._0Fe.Services;
using Microsoft.EntityFrameworkCore;

namespace Aris3._0Fe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication("MyCookieAuth")
            .AddCookie("MyCookieAuth", options =>
            {
                options.LoginPath = "/User/Login";
                options.Cookie.Expiration = null;
                options.ExpireTimeSpan = TimeSpan.FromHours(5);
                options.SlidingExpiration = true;
            });
            builder.Services.AddAuthorization();
            builder.Services.AddHostedService<AddNewFilmToDb>();
            builder.Services.AddHttpClient();
            builder.Services.AddDbContextPool<ArisDbContext>(options =>
            {
                var b = builder.Configuration.GetConnectionString("DefaultConnection");
                Console.WriteLine(b);
                options.UseSqlServer(b);
            });
            builder.Services.AddHttpClient<SendEmail>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
