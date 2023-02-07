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
    public class MakesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MakesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetMakes()
        {
            var makes = await _unitOfWork.Makes.GetAll();
            return Ok(makes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMakes(int id)
        {
            var makes = await _unitOfWork.Makes.GetAll(q => q.Id == id);

            if (makes == null)
            {
                return NotFound();
            }

            return Ok(makes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMakes(int id, Makes makes)
        {
            if (id != makes.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Makes.Update(makes);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MakeExist(id))
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

        [HttpPost]
        public async Task<ActionResult<Makes>> PostMakes(Makes makes)
        {
            await _unitOfWork.Makes.Insert(makes);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMakes", new { id = makes.Id }, makes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMakes(int id)
        {
            var makes = await _unitOfWork.Makes.Get(q => q.Id == id);
            if (makes == null)
            {
                return NotFound();
            }

            await _unitOfWork.Makes.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> MakeExist(int id)
        {
            var makes = await _unitOfWork.Makes.Get(q => q.Id == id);
            return makes != null;
        }
    }

}
