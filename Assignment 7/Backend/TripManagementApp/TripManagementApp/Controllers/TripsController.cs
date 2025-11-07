////using Microsoft.AspNetCore.Mvc;
////using Microsoft.EntityFrameworkCore;
////using TripManagementApp.Data;
////using TripManagementApp.Models;

////namespace TripManagementApp.Controllers
////{
////    [ApiController]
////    [Route("api/[controller]")]
////    public class TripsController : ControllerBase
////    {
////        private readonly AppDbContext _context;
////        public TripsController(AppDbContext context) => _context = context;

////        [HttpGet]
////        public async Task<ActionResult<IEnumerable<Trip>>> GetAll()
////        {
////            return await _context.Trips
////                .Include(t => t.Driver)
////                .Include(t => t.Vehicle)
////                .ToListAsync();
////        }

////        [HttpPost]
////        public async Task<ActionResult<Trip>> Create(Trip trip)
////        {
////            var vehicle = await _context.Vehicles.FindAsync(trip.VehicleId);
////            if (vehicle == null || !vehicle.IsAvailable)
////                return BadRequest("Vehicle not available");

////            vehicle.IsAvailable = false;
////            trip.StartTime = DateTime.Now;
////            trip.Status = "Active";

////            _context.Trips.Add(trip);
////            await _context.SaveChangesAsync();

////            return CreatedAtAction(nameof(GetAll), new { id = trip.Id }, trip);
////        }

////        [HttpPost("{id}/complete")]
////        public async Task<IActionResult> CompleteTrip(int id)
////        {
////            var trip = await _context.Trips
////                .Include(t => t.Vehicle)
////                .FirstOrDefaultAsync(t => t.Id == id);

////            if (trip == null) return NotFound();

////            trip.Status = "Completed";
////            trip.EndTime = DateTime.Now;
////            trip.Vehicle.IsAvailable = true;

////            await _context.SaveChangesAsync();
////            return NoContent();
////        }

////        [HttpGet("long-trips")]
////        public async Task<ActionResult<IEnumerable<Trip>>> GetLongTrips()
////        {
////            return await _context.Trips
////                .Where(t => t.EndTime != null && EF.Functions.DateDiffHour(t.StartTime, t.EndTime.Value) > 8)
////                .Include(t => t.Driver)
////                .Include(t => t.Vehicle)
////                .ToListAsync();
////        }
////    }
////}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using TripManagementApp.Data;
//using TripManagementApp.Models;

//namespace TripManagementApp.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TripsController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public TripsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/trips
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Trip>>> GetAll()
//        {
//            var trips = await _context.Trips
//                .Include(t => t.Driver)
//                .Include(t => t.Vehicle)
//                .ToListAsync();

//            return Ok(trips);
//        }

//        // GET: api/trips/{id}
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Trip>> GetById(int id)
//        {
//            var trip = await _context.Trips
//                .Include(t => t.Driver)
//                .Include(t => t.Vehicle)
//                .FirstOrDefaultAsync(t => t.Id == id);

//            if (trip == null)
//                return NotFound();

//            return Ok(trip);
//        }

//        // POST: api/trips
//        [HttpPost]
//        public async Task<ActionResult<Trip>> Create(Trip trip)
//        {
//            if (trip.VehicleId == 0)
//                return BadRequest("VehicleId is required");

//            var vehicle = await _context.Vehicles.FindAsync(trip.VehicleId);

//            if (vehicle == null || !vehicle.IsAvailable)
//                return BadRequest("Vehicle not available");

//            // Mark vehicle as unavailable
//            vehicle.IsAvailable = false;

//            // Set trip details
//            trip.StartTime = DateTime.Now;
//            trip.Status = "Active";

//            _context.Trips.Add(trip);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetById), new { id = trip.Id }, trip);
//        }

//        // POST: api/trips/{id}/complete
//        [HttpPost("{id}/complete")]
//        public async Task<IActionResult> CompleteTrip(int id)
//        {
//            var trip = await _context.Trips
//                .Include(t => t.Vehicle)
//                .FirstOrDefaultAsync(t => t.Id == id);

//            if (trip == null)
//                return NotFound();

//            trip.Status = "Completed";
//            trip.EndTime = DateTime.Now;

//            if (trip.Vehicle != null)
//                trip.Vehicle.IsAvailable = true;

//            await _context.SaveChangesAsync();
//            return NoContent();
//        }

//        // GET: api/trips/long-trips
//        [HttpGet("long-trips")]
//        public async Task<ActionResult<IEnumerable<Trip>>> GetLongTrips()
//        {
//            var trips = await _context.Trips
//                .Include(t => t.Driver)
//                .Include(t => t.Vehicle)
//                .Where(t => t.EndTime != null)
//                .ToListAsync();

//            // Calculate long trips in memory
//            var longTrips = trips
//                .Where(t => (t.EndTime.Value - t.StartTime).TotalHours > 8)
//                .ToList();

//            return Ok(longTrips);
//        }
//    }
//}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using TripManagementApp.Data;
//using TripManagementApp.Models;

//namespace TripManagementApp.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TripsController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public TripsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/trips
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Trip>>> GetAll()
//        {
//            var trips = await _context.Trips
//                .Include(t => t.Driver)
//                .Include(t => t.Vehicle)
//                .ToListAsync();

//            return Ok(trips);
//        }

//        // GET: api/trips/{id}
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Trip>> GetById(int id)
//        {
//            var trip = await _context.Trips
//                .Include(t => t.Driver)
//                .Include(t => t.Vehicle)
//                .FirstOrDefaultAsync(t => t.Id == id);

//            if (trip == null)
//                return NotFound();

//            return Ok(trip);
//        }

//        // POST: api/trips
//        [HttpPost]
//        public async Task<ActionResult<Trip>> Create(Trip trip)
//        {
//            if (trip.VehicleId == 0 || trip.DriverId == 0)
//                return BadRequest("Driver and Vehicle are required.");

//            var vehicle = await _context.Vehicles.FindAsync(trip.VehicleId);
//            if (vehicle == null || !vehicle.IsAvailable)
//                return BadRequest("Vehicle not available.");

//            vehicle.IsAvailable = false;
//            trip.Status = "Active";
//            trip.StartTime = trip.StartTime == DateTime.MinValue ? DateTime.Now : trip.StartTime;

//            _context.Trips.Add(trip);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetById), new { id = trip.Id }, trip);
//        }

//        // PUT: api/trips/{id}
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, Trip updatedTrip)
//        {
//            if (id != updatedTrip.Id)
//                return BadRequest();

//            var trip = await _context.Trips.Include(t => t.Vehicle).FirstOrDefaultAsync(t => t.Id == id);
//            if (trip == null)
//                return NotFound();

//            trip.Status = updatedTrip.Status ?? trip.Status;
//            trip.EndTime = updatedTrip.EndTime ?? trip.EndTime;

//            if (trip.Status == "Completed" && trip.Vehicle != null)
//                trip.Vehicle.IsAvailable = true;

//            _context.Entry(trip).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // DELETE: api/trips/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTrip(int id)
//        {
//            var trip = await _context.Trips.FindAsync(id);
//            if (trip == null)
//                return NotFound();

//            _context.Trips.Remove(trip);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripManagementApp.Data;
using TripManagementApp.Models;

namespace TripManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TripsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAll()
        {
            var trips = await _context.Trips
                .Include(t => t.Driver)
                .Include(t => t.Vehicle)
                .ToListAsync();

            return Ok(trips);
        }

        // GET: api/trips/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetById(int id)
        {
            var trip = await _context.Trips
                .Include(t => t.Driver)
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null)
                return NotFound();

            return Ok(trip);
        }

        // POST: api/trips
        [HttpPost]
        public async Task<ActionResult<Trip>> Create(Trip trip)
        {
            if (trip.VehicleId == 0 || trip.DriverId == 0)
                return BadRequest("Driver and Vehicle are required.");

            var vehicle = await _context.Vehicles.FindAsync(trip.VehicleId);
            if (vehicle == null || !vehicle.IsAvailable)
                return BadRequest("Vehicle not available.");

            vehicle.IsAvailable = false;
            trip.Status = "Active";
            trip.StartTime = trip.StartTime == DateTime.MinValue ? DateTime.Now : trip.StartTime;

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = trip.Id }, trip);
        }

        // PUT: api/trips/{id} — ✅ Update only status
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] Trip updatedTrip)
        {
            if (id != updatedTrip.Id)
                return BadRequest("Trip ID mismatch.");

            var trip = await _context.Trips
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null)
                return NotFound();

            if (!string.IsNullOrEmpty(updatedTrip.Status))
                trip.Status = updatedTrip.Status;

            if (trip.Status == "Completed")
            {
                trip.EndTime = DateTime.Now;
                if (trip.Vehicle != null)
                    trip.Vehicle.IsAvailable = true;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/trips/{id}/complete — ✅ mark trip completed
        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteTrip(int id)
        {
            var trip = await _context.Trips
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null)
                return NotFound();

            if (trip.Status == "Completed")
                return BadRequest("Trip already completed.");

            trip.Status = "Completed";
            trip.EndTime = DateTime.Now;

            if (trip.Vehicle != null)
                trip.Vehicle.IsAvailable = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/trips/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
                return NotFound();

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
