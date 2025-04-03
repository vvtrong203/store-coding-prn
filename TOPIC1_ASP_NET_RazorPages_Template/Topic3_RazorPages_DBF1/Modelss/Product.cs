using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Topic3_RazorPages_DBF1.Modelss;

public partial class Product
{
    public Guid ProductId { get; set; }
    [Required(ErrorMessage ="Product name required")]
    public string ProductName { get; set; } = null!;
    [Range(1,3000 , ErrorMessage ="Price must from 1$ to 3000$")]
    public int Price { get; set; }

    public Guid CategoryId { get; set; }
    [Range(1, 3000, ErrorMessage = "Price must from 1 to 3000")]
    public int? Stock { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
