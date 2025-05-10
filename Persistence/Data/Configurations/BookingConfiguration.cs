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
                   .WithMany(d=>d.Bookings)
                   .HasForeignKey(b => b.DecorationId)
                   .OnDelete(DeleteBehavior.ClientCascade);  // If Decoration is deleted, booking will still exist but with null decoration

            // Relationship with Venue (Optional - Can be null)
            builder.HasOne(b => b.Venue)
                   .WithMany(v=>v.Bookings)
                   .HasForeignKey(b => b.VenueId)
                   .OnDelete(DeleteBehavior.ClientCascade);  // If Venue is deleted, booking will still exist but with null venue

            // Relationship with MusicEnvironment (Optional - Can be null)
            builder.HasOne(b => b.MusicEnvironment)
                   .WithMany(m=>m.Bookings)
                   .HasForeignKey(b => b.MusicEnvironmentId)
                   .OnDelete(DeleteBehavior.ClientCascade);  // If MusicEnvironment is deleted, booking will still exist but with null music environment

            // Relationship with PhotographyAndVideography (Optional - Can be null)
            builder.HasOne(b => b.PhotographyAndVideography)
                   .WithMany(p=>p.Bookings)
                   .HasForeignKey(b => b.PhotographyAndVideographyId)
                   .OnDelete(DeleteBehavior.ClientCascade);

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
