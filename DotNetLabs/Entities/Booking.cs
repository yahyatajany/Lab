using System;

namespace DotNetLabs.Entities;

public class Booking
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public Room Room { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
