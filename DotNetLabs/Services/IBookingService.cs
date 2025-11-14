using System;
using DotNetLabs.Entities;

namespace DotNetLabs.Services;

public interface IBookingService
{
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);
    Task<Booking> CreateAsync(Booking booking);
    Task<bool> DeleteAsync(int id);

}
