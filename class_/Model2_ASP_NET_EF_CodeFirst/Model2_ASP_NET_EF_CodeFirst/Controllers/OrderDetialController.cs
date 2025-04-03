using Model2_ASP_NET_EF_CodeFirst.Models;

namespace Model2_ASP_NET_EF_CodeFirst.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

public class OrderDetailController : Controller
{
    private readonly PRN222DbContext _context;

    public OrderDetailController(PRN222DbContext context)
    {
        _context = context;
    }

    // // Hiển thị danh sách chi tiết đơn hàng
    // public IActionResult Index(int orderId)
    // {
    //     var orderDetails = _context.OrderDetails
    //         .Where(od => od.OrderId == orderId)
    //         .ToList();
    //
    //     return View(orderDetails);
    // }

    // Thêm chi tiết đơn hàng
    [HttpPost]
    public async Task<IActionResult> Add(OrderDetails orderDetail)
    {
        if (ModelState.IsValid)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { orderId = orderDetail.OrderId });
        }
        return View(orderDetail);
    }
}



