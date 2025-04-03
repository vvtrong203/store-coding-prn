using System;
using System.Collections.Generic;

namespace Question2.Models;

public partial class Car
{
    public string? CarName { get; set; }

    public string? Manufacturer { get; set; }

    public decimal? Price { get; set; }

    public int? ReleasedYear { get; set; }

    public int CarId { get; set; }
}
