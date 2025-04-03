using System;
using System.Collections.Generic;

namespace Topic_3_ASP_DOT_NET_RAZOR_WORKEF_DBFRIST.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public string ShipAddress { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
