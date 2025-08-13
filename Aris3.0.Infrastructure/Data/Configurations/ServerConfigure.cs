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
    public class ServerConfigure : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasMany(s=>s.Episodes)
                   .WithOne(e=>e.Server)
                   .HasForeignKey(e=>e.ServerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
