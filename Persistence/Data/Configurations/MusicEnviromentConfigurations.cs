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
    public class MusicEnviromentConfigurations : IEntityTypeConfiguration<MusicEnvironment>
    {
        public void Configure(EntityTypeBuilder<MusicEnvironment> builder)
        {

            builder.HasOne(M => M.Seller)
                .WithMany(U => U.MusicEnviroments)
                .HasForeignKey(D => D.SellerId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
