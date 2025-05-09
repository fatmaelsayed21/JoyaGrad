using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class DecorationConfigurations : IEntityTypeConfiguration<Decoration>
    {
        public void Configure(EntityTypeBuilder<Decoration> builder)
        {
            builder.HasOne(D => D.Seller)
                .WithMany(U => U.Decorations)
                .HasForeignKey(D => D.SellerId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
