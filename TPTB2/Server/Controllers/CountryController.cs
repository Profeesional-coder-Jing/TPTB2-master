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
    public class CountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _unitOfWork.Country.GetAll();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountries(int id)
        {
            var countries = await _unitOfWork.Country.GetAll(q => q.Id == id);

            if (countries == null)
            {
                return NotFound();
            }

            return Ok(countries);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountries(int id, Country countries)
        {
            if (id != countries.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Country.Update(countries);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExist(id))
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
        public async Task<ActionResult<Country>> PostCountries(Country countries)
        {
            await _unitOfWork.Country.Insert(countries);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetCountries", new { id = countries.Id }, countries);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountries(int id)
        {
            var countries = await _unitOfWork.Country.Get(q => q.Id == id);
            if (countries == null)
            {
                return NotFound();
            }

            await _unitOfWork.Country.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> CountryExist(int id)
        {
            var countries = await _unitOfWork.Country.Get(q => q.Id == id);
            return countries != null;
        }
    }
}
