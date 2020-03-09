using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_T_Shirt_Shop.Models;

namespace Online_T_Shirt_Shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductContext _context;

        public ShopController(ProductContext context)
        {
            _context = context;
        }

        // GET: Shop
        public async Task<IActionResult> Index()
        {
            return View(await _context.T_Shirt.ToListAsync());
        }

        // GET: Shop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var t_Shirt = await _context.T_Shirt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (t_Shirt == null)
            {
                return NotFound();
            }

            return View(t_Shirt);
        }

        // GET: Shop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,Price,InStock")] T_Shirt t_Shirt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(t_Shirt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(t_Shirt);
        }

        // GET: Shop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var t_Shirt = await _context.T_Shirt.FindAsync(id);
            if (t_Shirt == null)
            {
                return NotFound();
            }
            return View(t_Shirt);
        }

        // POST: Shop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImagePath,Price,InStock")] T_Shirt t_Shirt)
        {
            if (id != t_Shirt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(t_Shirt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!T_ShirtExists(t_Shirt.Id))
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
            return View(t_Shirt);
        }

        // GET: Shop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var t_Shirt = await _context.T_Shirt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (t_Shirt == null)
            {
                return NotFound();
            }

            return View(t_Shirt);
        }

        // POST: Shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var t_Shirt = await _context.T_Shirt.FindAsync(id);
            _context.T_Shirt.Remove(t_Shirt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool T_ShirtExists(int id)
        {
            return _context.T_Shirt.Any(e => e.Id == id);
        }
    }
}
