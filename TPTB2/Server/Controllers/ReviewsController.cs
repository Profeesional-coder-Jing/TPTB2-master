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
    public class ReviewsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public ReviewsController(ApplicationDbContext context)
        public ReviewsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Reviews
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Reviews>>> GetReviews()
        public async Task<IActionResult> GetReviews()
        {
            //to be deleted or comment after testing global error handling
            //return NotFound();
            //Refactored
            //return await _context.Reviews.ToListAsync();
            var reviews = await _unitOfWork.Reviews.GetAll();
            return Ok(reviews);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Reviews>> GetReviews(int id)
        public async Task<IActionResult> GetReviews(int id)
        {
            //Refactored
            //var reviews = await _context.Reviews.FindAsync(id);
            var reviews = await _unitOfWork.Reviews.GetAll(q => q.Id == id);

            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(reviews);
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviews(int id, Reviews reviews)
        {
            if (id != reviews.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(reviews).State = EntityState.Modified;
            _unitOfWork.Reviews.Update(reviews);

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
                if (!await ReviewExist(id))
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reviews>> PostReviews(Reviews reviews)
        {
            //Refactored
            //_context.Reviews.Add(reviews);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reviews.Insert(reviews);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetReviews", new { id = reviews.Id }, reviews);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviews(int id)
        {
            //Refactored
            //var reviews = await _context.Reviews.FindAsync(id);
            var reviews = await _unitOfWork.Reviews.Get(q => q.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Reviews.Remove(reviews);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reviews.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool UsersExists(int id)
        private async Task<bool> ReviewExist(int id)
        {
            //Refactored
            //return _context.Reviews.Any(e => e.Id == id);
            var reviews = await _unitOfWork.Reviews.Get(q => q.Id == id);
            return reviews != null;
        }
    }
}
