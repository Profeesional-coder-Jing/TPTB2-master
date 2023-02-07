using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPTB2.Shared.Domain;

namespace TPTB2.Server.Configurations.Entities
{
    public class MakesSeedConfiguration : IEntityTypeConfiguration<Makes>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Makes> builder)
        {
            builder.HasData(
                     new Makes
                     {
                         Id = 1,
                         Name = "Jason",
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     },
                     new Makes
                     {
                         Id = 2,
                         Name = "Jacob saggy thorous",
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     }
         );
        }
    }
}
