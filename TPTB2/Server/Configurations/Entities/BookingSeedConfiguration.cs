using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPTB2.Shared.Domain;

namespace TPTB2.Server.Configurations.Entities
{
    public class BookingSeedConfiguration : IEntityTypeConfiguration<Bookings>
    {
        public void Configure(EntityTypeBuilder<Bookings> builder)
        {
            builder.HasData(
                     new Bookings
                     {
                         Id = 1,
                         DateIn = new DateTime(2023, 4, 26),
                         DateOut = new DateTime(2023, 4, 30),
                         TotalCost = "Japan $70.00",
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     },
                     new Bookings
                     {
                         Id = 2,
                         DateIn = new DateTime(2023, 7,18),
                         DateOut = new DateTime(2023, 7, 22),
                         TotalCost = "Italy $76.00",
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     }
         );
        }
    }
}
