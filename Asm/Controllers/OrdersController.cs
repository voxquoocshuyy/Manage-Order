using Asm.Data;
using Asm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asm.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public OrdersController(DataContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            webHostEnvironment = _webHostEnvironment;
        }

        // GET: OrdersController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: OrdersController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.orderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // goi den khi bam nut submit tren view create
        public async Task<IActionResult> Create([Bind("orderID,productName,photo,quantity,total,dateOrder,UserID")] Order order, IFormFile hinhanh)
        {
            if (hinhanh == null || hinhanh.Length == 0)
            {
                return Content("please select file");
            }
            if (ModelState.IsValid)
            {
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", hinhanh.FileName);
                var stream = new FileStream(path, FileMode.Create);
                _ = hinhanh.CopyToAsync(stream);
                order.photo = hinhanh.FileName;
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: OrdersController/Edit/5
        //View Edit se load lai data cu~
        // goi den khi bam nut submit tren view edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var order = await _context.Orders.FindAsync(id);*/
            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.orderID == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("orderID,productName,photo,quantity,total,dateOrder,UserID")] Order order, IFormFile hinhanh, string hinhanhcu)
        {
            if (id != order.orderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (hinhanh == null || hinhanh.Length == 0)
                    {
                        order.photo = hinhanhcu;
                    }
                    else
                    {
                        var path = Path.Combine(webHostEnvironment.WebRootPath, "images", hinhanh.FileName);
                        var stream = new FileStream(path, FileMode.Create);
                        _ = hinhanh.CopyToAsync(stream);
                        order.photo = hinhanh.FileName;
                    }
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.orderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: OrdersController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.orderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: OrdersController/Delete/5
        [HttpPost, ActionName("Delete")]
/*        [ValidateAntiForgeryToken]*/
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.orderID == id);
        }
    }
}
