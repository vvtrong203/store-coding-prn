using System;
using System.Collections.Generic;

namespace Topic_3_ASP_DOT_NET_RAZOR_WORKEF_DBFRIST.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
