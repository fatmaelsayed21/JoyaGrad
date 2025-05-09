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
    public class VenueConfigurations : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.HasKey(V=>V.VenueId);

            builder.HasOne(V => V.Seller)
                .WithMany(U=>U.Venues).HasForeignKey(V=>V.SellerId)
                .OnDelete(DeleteBehavior.Cascade);
          
            
        }
    }
}
