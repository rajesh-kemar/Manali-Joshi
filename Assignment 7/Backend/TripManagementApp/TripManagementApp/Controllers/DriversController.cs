using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripManagementApp.Data;
using TripManagementApp.Models;

namespace TripManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DriversController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetAll() =>
            await _context.Drivers.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> Get(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return NotFound();
            return driver;
        }

        [HttpPost]
        public async Task<ActionResult<Driver>> Create(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = driver.Id }, driver);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Driver driver)
        {
            if (id != driver.Id) return BadRequest();
            _context.Entry(driver).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return NotFound();
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
