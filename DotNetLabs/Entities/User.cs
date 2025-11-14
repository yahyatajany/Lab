using System;

namespace DotNetLabs.Entities;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "Admin";

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

}
