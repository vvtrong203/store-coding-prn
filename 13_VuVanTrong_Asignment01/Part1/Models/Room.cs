using System;
using System.Collections.Generic;

namespace Part1.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public string RoomType { get; set; } = null!;

    public int? Capacity { get; set; }

    public bool? IsAvailable { get; set; }
}
