using System;
using DotNetLabs.Data;
using DotNetLabs.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetLabs.Services;

public class RoomService : IRoomService
{
    private readonly AppDbContext _context;
    public RoomService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Room> CreateAsync(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null) return false;

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _context.Rooms.AsNoTracking().ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public async Task<Room?> UpdateAsync(int id, Room updatedRoom)
    {
        var existing = await _context.Rooms.FindAsync(id);
        if (existing == null) return null;

        existing.Reference = updatedRoom.Reference;
        existing.IsAvailable = updatedRoom.IsAvailable;

        await _context.SaveChangesAsync();
        return existing;
    }
}
