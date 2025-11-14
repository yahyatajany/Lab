using System;
using DotNetLabs.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetLabs.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

}
