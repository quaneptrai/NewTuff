using Aris3._0.Domain.Entities;
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

            var personAdminId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var personUser1Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var personUser2Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var now = DateTime.UtcNow;

            // Seed Subscriptions
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription
                {
                    Id = 1,
                    Name = "Basic Plan",
                    Description = "Access to standard features and content.",
                    CreatedDate = now,
                    UpdatedDate = now,
                    type = "Basic"
                },
                new Subscription
                {
                    Id = 2,
                    Name = "Premium Plan",
                    Description = "Includes HD streaming and exclusive content.",
                    CreatedDate = now,
                    UpdatedDate = now,
                    type = "Premium"
                },
                new Subscription
                {
                    Id = 3,
                    Name = "Enterprise Plan",
                    Description = "For organizations with extended access and multiple accounts.",
                    CreatedDate = now,
                    UpdatedDate = now,
                    type = "Enterprise"
                }
            );

            // Seed Persons
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    id = personAdminId,
                    Name = "Admin Person",
                    Email = "admin@gmail.com",
                    Role = "Admin",
                    PhoneNumber = "0123456789",
                    City = "Hanoi",
                    State = "HN",
                    Zipcode = "100000",
                    Country = "Vn",
                    Region = "Sea",
                    AccountStat = true,
                    Created = now,
                    LastUpdated = now,
                },
                new Person
                {
                    id = personUser1Id,
                    Name = "User One",
                    Email = "user1@gmail.com",
                    Role = "User",
                    PhoneNumber = "0987654321",
                    City = "HCMC",
                    State = "HCM",
                    Zipcode = "700000",
                    Country = "Vn",
                    Region = "Sea",
                    AccountStat = true,
                    Created = now,
                    LastUpdated = now
                },
                new Person
                {
                    id = personUser2Id,
                    Name = "User Two",
                    Email = "user2@gmail.com",
                    Role = "User",
                    PhoneNumber = "0911222333",
                    City = "Danang",
                    State = "DN",
                    Zipcode = "550000",
                    Country = "Vn",
                    Region = "Sea",
                    AccountStat = true,
                    Created = now,
                    LastUpdated = now
                }
            );

            // Seed Accounts
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Password = "admin123", // hash in production
                    Role = "Admin",
                    status = true,
                    AccountStat = true,
                    Created = now,
                    LastUpdated = now,
                    PersonId = personAdminId,
                    SubcriptionId = 1
                },
                new Account
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    UserName = "user1",
                    Email = "user1@gmail.com",
                    Password = "user123",
                    Role = "User",
                    status = true,
                    AccountStat = true,
                    Created = now,
                    LastUpdated = now,
                    PersonId = personUser1Id,
                    SubcriptionId = 2
                },
                new Account
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    UserName = "user2",
                    Email = "user2@gmail.com",
                    Password = "user123",
                    Role = "User",
                    status = true,
                    AccountStat = true,
                    Created = now,
                    LastUpdated = now,
                    PersonId = personUser2Id,
                    SubcriptionId = 3
                }
            );
        }
    }
}