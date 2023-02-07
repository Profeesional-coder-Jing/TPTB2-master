using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPTB2.Server.Data;
using TPTB2.Server.IRepository;
using TPTB2.Shared.Domain;

namespace TPTB2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public BookingsController(ApplicationDbContext context)
        public BookingsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Bookings
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Bookings>>> GetBookings()
        public async Task<IActionResult> GetBookings()
        {
            //Refactored
            //return await _context.Bookings.ToListAsync();
            var bookings = await _unitOfWork.Bookings.GetAll();
            return Ok(bookings);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Bookings>> GetBookings(int id)
        public async Task<IActionResult> GetBookings(int id)
        {
            //Refactored
            //var bookings = await _context.Bookings.FindAsync(id);
            var bookings = await _unitOfWork.Bookings.GetAll(q => q.Id == id);

            if (bookings == null)
            {
                return NotFound();
            }

            return Ok(bookings);
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookings(int id, Bookings bookings)
        {
            if (id != bookings.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(bookings).State = EntityState.Modified;
            _unitOfWork.Bookings.Update(bookings);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!UsersExists(id)) 
                if (!await BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookings>> PostBookings(Bookings bookings)
        {
            //Refactored
            //_context.Bookings.Add(bookings);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Bookings.Insert(bookings);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetBookings", new { id = bookings.Id }, bookings);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookings(int id)
        {
            //Refactored
            //var bookings = await _context.Bookings.FindAsync(id);
            var bookings = await _unitOfWork.Bookings.Get(q => q.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Bookings.Remove(bookings);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Bookings.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool UsersExists(int id)
        private async Task<bool> BookingExists(int id)
        {
            //Refactored
            //return _context.Bookings.Any(e => e.Id == id);
            var bookings = await _unitOfWork.Bookings.Get(q => q.Id == id);
            return bookings != null;
        }
    }
}
