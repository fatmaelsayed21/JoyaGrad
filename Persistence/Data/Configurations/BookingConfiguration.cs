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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
           builder.HasKey(b=>b.BookingId);
            // Relationship with Decoration (Optional - Can be null)
            builder.HasOne(b => b.Decoration)
                   .WithMany()
                   .HasForeignKey(b => b.DecorationId)
                   .OnDelete(DeleteBehavior.SetNull);  // If Decoration is deleted, booking will still exist but with null decoration

            // Relationship with Venue (Optional - Can be null)
            builder.HasOne(b => b.Venue)
                   .WithMany()
                   .HasForeignKey(b => b.VenueId)
                   .OnDelete(DeleteBehavior.SetNull);  // If Venue is deleted, booking will still exist but with null venue

            // Relationship with MusicEnvironment (Optional - Can be null)
            builder.HasOne(b => b.MusicEnvironment)
                   .WithMany()
                   .HasForeignKey(b => b.MusicEnvironmentId)
                   .OnDelete(DeleteBehavior.SetNull);  // If MusicEnvironment is deleted, booking will still exist but with null music environment

            // Relationship with PhotographyAndVideography (Optional - Can be null)
            builder.HasOne(b => b.PhotographyAndVideography)
                   .WithMany()
                   .HasForeignKey(b => b.PhotographyAndVideographyId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b=>b.Buyer)
                .WithMany(U=>U.Bookings)
                .HasForeignKey(b=>b.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);
           builder.HasOne(b=>b.Payment)
                .WithOne(p=>p.Booking)
                .HasForeignKey<Payment>(P=>P.BookingId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
