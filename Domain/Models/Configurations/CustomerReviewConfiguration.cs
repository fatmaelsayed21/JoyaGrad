using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Domain.Models.Configurations
{
    public class CustomerReviewConfiguration : IEntityTypeConfiguration<CustomerReview>
    {
        public void Configure(EntityTypeBuilder<CustomerReview> builder)
        {
            // Primary Key
            builder.HasKey(cr => cr.Id);

            // Properties
            builder.Property(cr => cr.Rating)
                .IsRequired()
                .HasPrecision(3, 2);

            builder.Property(cr => cr.CreatedAt)
                .IsRequired();

            // Relationships
            // Optional service relationships
            builder.HasOne(cr => cr.Venue)
                .WithMany(v => v.CustomerReviews)
                .HasForeignKey(cr => cr.VenueId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(cr => cr.Decoration)
                .WithMany()
                .HasForeignKey(cr => cr.DecorationId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(cr => cr.PhotographyAndVideography)
                .WithMany()
                .HasForeignKey(cr => cr.PhotographyAndVideographyId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(cr => cr.MusicEnvironment)
                .WithMany()
                .HasForeignKey(cr => cr.MusicEnvironmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Buyer relationship
            builder.HasOne(cr => cr.Buyer)
                .WithMany(u => u.WrittenReviews)
                .HasForeignKey(cr => cr.BuyerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Seller relationship
            builder.HasOne(cr => cr.Seller)
                .WithMany(u => u.ReceivedReviews)
                .HasForeignKey(cr => cr.SellerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Indexes
            builder.HasIndex(cr => cr.BuyerId);
            builder.HasIndex(cr => cr.SellerId);
            builder.HasIndex(cr => cr.CreatedAt);
        }
    }
} 