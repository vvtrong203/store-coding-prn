namespace Topic3_RazorPages_DBF1.Modelss;

public class OrderHistoryViewModel
{
    public DateTime OrderDate { get; set; }
    public string ProductName { get; set; } = String.Empty;
    public int Quantity { get; set; }
}