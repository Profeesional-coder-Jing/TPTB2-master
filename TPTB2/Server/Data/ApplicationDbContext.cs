using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPTB2.Server.Configurations.Entities;
using TPTB2.Server.Models;
using TPTB2.Shared.Domain;

namespace TPTB2.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Country> Country { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UsersSeedConfiguration());
            builder.ApplyConfiguration(new ReviewSeedConfiguration());
            builder.ApplyConfiguration(new PaymentSeedConfiguration());
            builder.ApplyConfiguration(new BookingSeedConfiguration());
            builder.ApplyConfiguration(new CountrySeedConfiguration());

        }
    }
}
