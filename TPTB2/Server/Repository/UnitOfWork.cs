using TPTB2.Server.Data;
using TPTB2.Server.IRepository;
using TPTB2.Server.Models;
using TPTB2.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TPTB2.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Bookings> _bookings;
        private IGenericRepository<Payments> _payments;
        private IGenericRepository<Reviews> _reviews;
        private IGenericRepository<Users> _users;
        private IGenericRepository<Country> _country;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Bookings> Bookings
            => _bookings ??= new GenericRepository<Bookings>(_context);
        public IGenericRepository<Payments> Payments
            => _payments ??= new GenericRepository<Payments>(_context);
        public IGenericRepository<Reviews> Reviews
            => _reviews ??= new GenericRepository<Reviews>(_context);
        public IGenericRepository<Users> Users
            => _users ??= new GenericRepository<Users>(_context);

        public IGenericRepository<Country> Country => throw new NotImplementedException();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
