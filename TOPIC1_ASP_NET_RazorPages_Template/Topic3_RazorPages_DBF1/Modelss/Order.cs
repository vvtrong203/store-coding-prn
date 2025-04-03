using System;
using System.Collections.Generic;

namespace Topic3_RazorPages_DBF1.Modelss;

public partial class Order
{
    public Guid OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public string ShipAddress { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
