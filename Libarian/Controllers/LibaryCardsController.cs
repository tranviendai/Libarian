using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Libarian.Data;
using Libarian.Models;

namespace Libarian.Controllers
{
    public class LibaryCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibaryCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LibaryCards
        public async Task<IActionResult> Index()
        {
              return _context.LibaryCard != null ? 
                          View(await _context.LibaryCard.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LibaryCard'  is null.");
        }

        // GET: LibaryCards/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.LibaryCard == null)
            {
                return NotFound();
            }

            var libaryCard = await _context.LibaryCard
                .FirstOrDefaultAsync(m => m.libaryCardID == id);
            if (libaryCard == null)
            {
                return NotFound();
            }

            return View(libaryCard);
        }

        // GET: LibaryCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LibaryCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("libaryCardID,librayCardIndex,fullName,cardStatus,startDate,expirationDate")] LibaryCard libaryCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libaryCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libaryCard);
        }

        // GET: LibaryCards/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.LibaryCard == null)
            {
                return NotFound();
            }

            var libaryCard = await _context.LibaryCard.FindAsync(id);
            if (libaryCard == null)
            {
                return NotFound();
            }
            return View(libaryCard);
        }

        // POST: LibaryCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("libaryCardID,librayCardIndex,fullName,cardStatus,startDate,expirationDate")] LibaryCard libaryCard)
        {
            if (id != libaryCard.libaryCardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libaryCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibaryCardExists(libaryCard.libaryCardID))
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
            return View(libaryCard);
        }

        // GET: LibaryCards/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.LibaryCard == null)
            {
                return NotFound();
            }

            var libaryCard = await _context.LibaryCard
                .FirstOrDefaultAsync(m => m.libaryCardID == id);
            if (libaryCard == null)
            {
                return NotFound();
            }

            return View(libaryCard);
        }

        // POST: LibaryCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.LibaryCard == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LibaryCard'  is null.");
            }
            var libaryCard = await _context.LibaryCard.FindAsync(id);
            if (libaryCard != null)
            {
                _context.LibaryCard.Remove(libaryCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibaryCardExists(string id)
        {
          return (_context.LibaryCard?.Any(e => e.libaryCardID == id)).GetValueOrDefault();
        }
    }
}
