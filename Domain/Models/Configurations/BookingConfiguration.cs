using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Domain.Models.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Primary Key
            builder.HasKey(b => b.BookingId);

            // Properties
            builder.Property(b => b.EventDate)
                .IsRequired();

            builder.Property(b => b.Status)
                .IsRequired();

            builder.Property(b => b.TotalPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(b => b.BuyerId)
                .IsRequired();

            // Relationships
            // One-to-One with Payment
            builder.HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-One with User (Buyer)
            builder.HasOne(b => b.Buyer)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Optional relationships with services
            builder.HasOne(b => b.Venue)
                .WithMany(v => v.Bookings)
                .HasForeignKey(b => b.VenueId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(b => b.Decoration)
                .WithMany()
                .HasForeignKey(b => b.DecorationId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(b => b.PhotographyAndVideography)
                .WithMany()
                .HasForeignKey(b => b.PhotographyAndVideographyId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(b => b.MusicEnvironment)
                .WithMany()
                .HasForeignKey(b => b.MusicEnvironmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(b => b.BuyerId);
            builder.HasIndex(b => b.EventDate);
            builder.HasIndex(b => b.Status);
        }
    }
} 