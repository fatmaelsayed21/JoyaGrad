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
    public class CustomerReviewConfiguration : IEntityTypeConfiguration<CustomerReview>
    {
        public void Configure(EntityTypeBuilder<CustomerReview> builder)
        {
            builder.HasOne(cr => cr.Venue)
          .WithMany(v => v.CustomerReviews)
          .HasForeignKey(cr => cr.VenueId)
          .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.Decoration)
                   .WithMany(d => d.CustomerReviews)
                   .HasForeignKey(r => r.DecorationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.MusicEnvironment)
                   .WithMany(m => m.CustomerReviews)
                   .HasForeignKey(cr => cr.MusicEnvironmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.PhotographyAndVideography)
                   .WithMany(p => p.CustomerReviews)
                   .HasForeignKey(cr => cr.PhotographyAndVideographyId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(cr => cr.Seller)
                  .WithMany(U => U.CustomerReviews)
                  .HasForeignKey(cr => cr.SellerId)
                  .IsRequired(true).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr=>cr.Buyer)
                .WithMany(U=>U.WrittenReviews)
                .HasForeignKey(cr=>cr.BuyerId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
