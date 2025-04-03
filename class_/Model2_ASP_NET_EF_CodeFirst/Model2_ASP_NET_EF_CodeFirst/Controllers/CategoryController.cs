// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Model2_ASP_NET_EF_CodeFirst.Models;
//
// namespace Model2_ASP_NET_EF_CodeFirst.Controllers
// {
//     public class CategoryController : Controller
//     {
//         private readonly PRN222DbContext _context;
//         public CategoryController(PRN222DbContext context)
//         {
//             _context = context;
//         }
//
//         // Hiển thị danh sách danh mục
//         public async Task<IActionResult> Index()
//         {
//             var categories = await _context.Categories.Include(c => c.Products).ToListAsync();
//             return View(categories);
//         }
//
//         // Xem chi tiết danh mục
//         public async Task<IActionResult> Details(Guid id)
//         {
//             var category = await _context.Categories.Include(c => c.Products)
//                 .FirstOrDefaultAsync(c => c.CategoryId == id);
//             if (category == null) return NotFound();
//             return View(category);
//         }
//
//         // Hiển thị form tạo mới danh mục
//         public IActionResult Create()
//         {
//             return View();
//         }
//
//         [HttpPost]
//         public async Task<IActionResult> Create(Category category)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return View(category);
//             }
//
//             category.CategoryId = Guid.NewGuid(); // Tạo ID mới
//             _context.Categories.Add(category);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//
//         // Hiển thị form chỉnh sửa danh mục
//         public async Task<IActionResult> Edit(Guid id)
//         {
//             var category = await _context.Categories.FindAsync(id);
//             if (category == null) return NotFound();
//             return View(category);
//         }
//
//         [HttpPost]
//         public async Task<IActionResult> Edit(Guid id, Category category)
//         {
//             if (!ModelState.IsValid) return View(category);
//
//             _context.Update(category);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//
//         // Xóa danh mục
//         public async Task<IActionResult> Delete(Guid id)
//         {
//             var category = await _context.Categories.FindAsync(id);
//             if (category == null) return NotFound();
//             _context.Categories.Remove(category);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model2_ASP_NET_EF_CodeFirst.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Model2_ASP_NET_EF_CodeFirst.Controllers
{
    public class CategoryController : Controller
    {
        private readonly PRN222DbContext _context;
        public CategoryController(PRN222DbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var categories = await (from c in _context.Categories.Include(c => c.Products)
                                    select c).ToListAsync();
            return View(categories);
        }

        // Xem chi tiết danh mục
        public async Task<IActionResult> Details(Guid id)
        {
            var category = await (from c in _context.Categories.Include(c => c.Products)
                                  where c.CategoryId == id
                                  select c).FirstOrDefaultAsync();
            if (category == null) return NotFound();
            return View(category);
        }

        // Hiển thị form tạo mới danh mục
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            category.CategoryId = Guid.NewGuid();
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await (from c in _context.Categories
                                  where c.CategoryId == id
                                  select c).FirstOrDefaultAsync();

            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Category category)
        {
            if (!ModelState.IsValid) return View(category);

            var existingCategory = await (from c in _context.Categories
                                          where c.CategoryId == id
                                          select c).FirstOrDefaultAsync();

            if (existingCategory == null) return NotFound();

            existingCategory.CategoryName = category.CategoryName;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Xóa danh mục
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await (from c in _context.Categories
                                  where c.CategoryId == id
                                  select c).FirstOrDefaultAsync();

            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
