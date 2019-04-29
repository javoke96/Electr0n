using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCities.Models;
namespace ASPCities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase

    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            return await _context.CityItems.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(long id)
        {
            var cityItem = await _context.CityItems.FindAsync(id);

            if (cityItem == null)
            {
                return NotFound();
            }

            return cityItem;
        }

        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            city.Prev = _context.CityItems.Last().Name;
            _context.CityItems.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(long id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }
            //city.Prev = _context.CityItems.AsNoTracking().Last().Name;
            _context.Entry(city).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(long id)
        {
            var city = await _context.CityItems.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            _context.CityItems.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //testing purposes only
        private readonly CityContext _context;
        public CityController(CityContext context)
        {
            _context = context;
            if(_context.CityItems.Count() == 0)
            {
                _context.CityItems.Add(new City { Name = "Istanbul", Prev = "", Status = true });
                _context.CityItems.Add(new City { Name = "Baku", Prev = "Istanbul", Status = true });
                _context.CityItems.Add(new City { Name = "Izmir", Prev = "Baku", Status = false });
                _context.CityItems.Add(new City { Name = "Melbourne", Prev = "Izmir", Status = false });
                _context.CityItems.Add(new City { Name = "Kyoto", Prev = "Melbourne", Status = true });
                _context.SaveChanges();
            }
        }
    }
}
