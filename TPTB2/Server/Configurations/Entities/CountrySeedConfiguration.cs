using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPTB2.Shared.Domain;

namespace TPTB2.Server.Configurations.Entities
{
    public class CountrySeedConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                     new Country
                     {
                         Id = 1,
                         Name = "Singapore",
                         Text = "No",
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     },
                     new Country
                     {
                         Id = 2,
                         Name = "Thailand",
                         Text = "I am Thai.",
                         DateCreated = DateTime.Now,
                         DateUpdated = DateTime.Now,
                         CreatedBy = "System",
                         UpdatedBy = "System"
                     }
         );
        }
    }
}
