using Aris3._0Fe.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Aris3._0.Infrastructure.Data.Context
{
    public class ArisDbContext : DbContext
    {
        public ArisDbContext(DbContextOptions<ArisDbContext> options) : base(options)
        {

        }
        public DbSet<Tmbd> Tmbd { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Created> Created { get; set; }
        public DbSet<Modified> Modified { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArisDbContext).Assembly);
        }
    }
}
