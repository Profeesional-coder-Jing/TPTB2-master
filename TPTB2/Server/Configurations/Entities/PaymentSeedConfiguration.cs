using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPTB2.Shared.Domain;

namespace TPTB2.Server.Configurations.Entities
{
    public class PaymentSeedConfiguration : IEntityTypeConfiguration<Payments>
    {
        public void Configure(EntityTypeBuilder<Payments> builder)
        {
            builder.HasData(
                     new Payments
                     {
                         Id = 1,
                         Name = "Tan Ping Jing",
                         CardNumber = "2563 6143 3434 8172",
                         SecurityCode = "465",
                         DateOfExpiry = new DateTime(2025, 5, 24),
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     },
                     new Payments
                     {
                         Id = 2,
                         Name = "Arnold Schwarzenegger",
                         CardNumber = "6363 5261 9765 0162",
                         SecurityCode = "816",
                         DateOfExpiry = new DateTime(2025, 7, 16),
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     }
         );
        }
    }
}
