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
    public class PhotographyAndVideographyConfigurations : IEntityTypeConfiguration<PhotographyAndVideography>
    {
        public void Configure(EntityTypeBuilder<PhotographyAndVideography> builder)
        {

            builder.HasOne(p => p.Seller)
                .WithMany(U => U.PhotographyAndVideographies)
                .HasForeignKey(p => p.SellerId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
