using Model2_ASP_NET_EF_CodeFirst.Models;

namespace Model2_ASP_NET_EF_CodeFirst.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;


public class OrderController : Controller
{
    private readonly PRN222DbContext _context;

    public OrderController(PRN222DbContext context)
    {
        _context = context;
    }

    // Danh sách đơn hàng
    public IActionResult Index()
    {
        var orders = _context.Orders.ToList();
        return View(orders);
    }

    // Tạo đơn hàng
    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        if (ModelState.IsValid)
        {
            order.OrderDate = DateTime.Now;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(order);
    }

    // public IActionResult Details(int id)
    // {
    //     var order = _context.Orders
    //         .Where(o => o.OrderId == id)
    //         .FirstOrDefault();
    //
    //     if (order == null)
    //         return NotFound();
    //
    //     return View(order);
    // }
}


