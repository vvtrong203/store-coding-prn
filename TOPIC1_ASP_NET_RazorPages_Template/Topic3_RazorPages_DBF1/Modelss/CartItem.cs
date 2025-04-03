namespace Topic3_RazorPages_DBF1.Modelss;

public class CartItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Quantity { get; set; }
}