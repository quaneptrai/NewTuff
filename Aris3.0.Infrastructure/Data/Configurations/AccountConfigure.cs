using Aris3._0.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aris3._0.Infrastructure.Data.Configurations
{
    public class AccountConfigure : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Subscription)          
                   .WithMany(s => s.accounts)            
                   .HasForeignKey(a => a.SubcriptionId) 
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.Otps)
                    .WithOne(o => o.Account)
                    .HasForeignKey(o => o.AccounId);
        }
    }
}
