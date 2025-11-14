using System;
using DotNetLabs.Data;
using DotNetLabs.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetLabs.Services;

public class BookingService : IBookingService
{
    private readonly AppDbContext _context;

    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync() =>
        await _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Booking?> GetByIdAsync(int id) =>
        await _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<Booking> CreateAsync(Booking booking)
    {
        // Example rule: prevent booking if room is unavailable
        var room = await _context.Rooms.FindAsync(booking.RoomId);
        if (room == null || !room.IsAvailable)
            throw new Exception("Room not available.");

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return false;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return true;
    }

}
