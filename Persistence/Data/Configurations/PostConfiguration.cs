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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(P=>P.Id);
            builder.HasOne(P=>P.Seller)
                .WithMany(U=>U.Posts)
                .HasForeignKey(P=>P.SellerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
