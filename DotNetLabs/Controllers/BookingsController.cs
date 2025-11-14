using Microsoft.AspNetCore.Mvc;

using DotNetLabs.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DotNetLabs.Data;

namespace DotNetLabs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBookings()
        {
            var username = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return Unauthorized();

            var bookings = await _context.Bookings
                .Where(b => b.UserId == user.Id)
                .Include(b => b.Room)
                .ToListAsync();

            return Ok(bookings);
        }

        [HttpPost("{roomId}")]
        public async Task<IActionResult> BookRoom(int roomId)
        {
            var username = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return Unauthorized();

            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null || !room.IsAvailable)
                return BadRequest("Room is not available.");

            var booking = new Booking
            {
                RoomId = roomId,
                UserId = user.Id
            };

            room.IsAvailable = false;
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Room booked successfully!", booking });
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var username = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return Unauthorized();

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == user.Id);

            if (booking == null) return NotFound();

            booking.Room.IsAvailable = true;
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking cancelled successfully." });
        }
    }
}
