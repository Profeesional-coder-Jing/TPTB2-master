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
    public class UsersController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public UsersController(ApplicationDbContext context)
        public UsersController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Users
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        public async Task<IActionResult> GetUsers()
        {
            //Refactored
            //return await _context.Users.ToListAsync();
            var users = await _unitOfWork.Users.GetAll(includes: q => q.Include(x => x.Reviews));
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Users>> GetUsers(int id)
        public async Task<IActionResult> GetUsers(int id)
        {
            //Refactored
            //var users = await _context.Users.FindAsync(id);
            var users = await _unitOfWork.Users.GetAll(q => q.Id == id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(users).State = EntityState.Modified;
            _unitOfWork.Users.Update(users);

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
                if (!await UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            //Refactored
            //_context.Users.Add(users);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Users.Insert(users);
            await _unitOfWork.Save(HttpContext);
                
            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            //Refactored
            //var users = await _context.Users.FindAsync(id);
            var users = await _unitOfWork.Users.Get(q => q.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Users.Remove(users);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.Save(HttpContext);    

            return NoContent();
        }

        //Refactored
        //private bool UsersExists(int id)
        private async Task<bool> UserExists(int id) 
        {
            //Refactored
            //return _context.Users.Any(e => e.Id == id);
            var users = await _unitOfWork.Users.Get(q => q.Id == id);
            return users != null;    
        }
    }
}
