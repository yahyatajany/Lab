using System;

namespace DotNetLabs.Entities;

public class Room
{
    public int Id { get; set; }

    public string Reference { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

}
