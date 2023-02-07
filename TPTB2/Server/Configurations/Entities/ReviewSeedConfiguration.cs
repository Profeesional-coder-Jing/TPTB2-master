using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPTB2.Shared.Domain;

namespace TPTB2.Server.Configurations.Entities
{
    public class ReviewSeedConfiguration : IEntityTypeConfiguration<Reviews>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Reviews> builder)
        {
            builder.HasData(
                     new Reviews
                     {
                         Id = 1,
                         Name = "DickRooster",
                         Text = "The trip was fun and interactive. I thoroughly enjoyed myself at many point of interests, would recommend to anyone who is interested in visiting the country.",
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     },
                     new Reviews
                     {
                         Id = 2,
                         Name = "ArnoldSchwarzenegger",
                         Text = "The trip was not as great as I had expected. The points of interest did not look as good as they were on paper, so don't get your hopes up too much if you are planning to buy it. 5/10 overall.",
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     }
         );
        }
    }
}
