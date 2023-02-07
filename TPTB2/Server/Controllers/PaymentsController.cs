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
    public class PaymentsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public PaymentsController(ApplicationDbContext context)
        public PaymentsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Payments
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Payments>>> GetPayments()
        public async Task<IActionResult> GetPayments()
        {
            //Refactored
            //return await _context.Payments.ToListAsync();
            var payments = await _unitOfWork.Payments.GetAll();
            return Ok(payments);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Payments>> GetPayments(int id)
        public async Task<IActionResult> GetPayments(int id)
        {
            //Refactored
            //var payments = await _context.Payments.FindAsync(id);
            var payments = await _unitOfWork.Payments.GetAll(q => q.Id == id);

            if (payments == null)
            {
                return NotFound();
            }

            return Ok(payments);
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayments(int id, Payments payments)
        {
            if (id != payments.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(payments).State = EntityState.Modified;
            _unitOfWork.Payments.Update(payments);

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
                if (!await PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payments>> PostPayments(Payments payments)
        {
            //Refactored
            //_context.Payments.Add(payments);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Payments.Insert(payments);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPayments", new { id = payments.Id }, payments);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayments(int id)
        {
            //Refactored
            //var payments = await _context.Payments.FindAsync(id);
            var payments = await _unitOfWork.Payments.Get(q => q.Id == id);
            if (payments == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Payments.Remove(payments);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Payments.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool UsersExists(int id)
        private async Task<bool> PaymentExists(int id)
        {
            //Refactored
            //return _context.Payments.Any(e => e.Id == id);
            var payments = await _unitOfWork.Payments.Get(q => q.Id == id);
            return payments != null;
        }
    }
}
