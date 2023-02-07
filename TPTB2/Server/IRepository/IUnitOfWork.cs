using TPTB2.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPTB2.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Bookings> Bookings { get; }
        IGenericRepository<Payments> Payments { get; }
        IGenericRepository<Reviews> Reviews { get; }
        IGenericRepository<Users> Users { get; }
        IGenericRepository<Country> Country { get; }
    }
}
