using DotNetLabs.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetLabs.Data
{
    public static class SeedData
    {
        public static void SeedRooms(AppDbContext context)
        {
            // Ensure database is created
            context.Database.Migrate();

            // Only seed if no rooms exist
            if (!context.Rooms.Any())
            {
                var rooms = new List<Room>
                {
                    new Room { Reference = "Room-101", IsAvailable = true },
                    new Room { Reference = "Room-102", IsAvailable = true },
                    new Room { Reference = "Room-103", IsAvailable = true },
                    new Room { Reference = "Room-104", IsAvailable = true },
                    new Room { Reference = "Room-105", IsAvailable = true }
                };

                context.Rooms.AddRange(rooms);
                context.SaveChanges();
            }
        }

        public static void SeedAdminUser(AppDbContext context)
        {
            if (!context.Users.Any(u => u.Role == "Admin"))
            {
                var adminPassword = "Admin@123"; // default password
                var hashedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(adminPassword));
                context.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = hashedPassword,
                    Role = "Admin"
                });
                context.SaveChanges();
            }
        }
    }
}
