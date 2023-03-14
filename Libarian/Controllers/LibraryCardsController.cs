using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Librarian.Data;
using Librarian.Models;

namespace Librarian.Controllers
{
    public class LibraryCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibraryCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LibraryCards
        public async Task<IActionResult> Index()
        {
              return _context.LibraryCard != null ? 
                          View(await _context.LibraryCard.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LibraryCard'  is null.");
        }

        // GET: LibraryCards/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.LibraryCard == null)
            {
                return NotFound();
            }

            var libraryCard = await _context.LibraryCard
                .FirstOrDefaultAsync(m => m.libraryCardID == id);
            if (libraryCard == null)
            {
                return NotFound();
            }

            return View(libraryCard);
        }

        // GET: LibraryCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LibraryCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("libraryCardID,librayCardIndex,fullName,cardStatus,startDate,expirationDate")] LibraryCard libraryCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libraryCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libraryCard);
        }

        // GET: LibraryCards/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.LibraryCard == null)
            {
                return NotFound();
            }

            var libraryCard = await _context.LibraryCard.FindAsync(id);
            if (libraryCard == null)
            {
                return NotFound();
            }
            return View(libraryCard);
        }

        // POST: LibraryCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("libraryCardID,librayCardIndex,fullName,cardStatus,startDate,expirationDate")] LibraryCard libraryCard)
        {
            if (id != libraryCard.libraryCardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libraryCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryCardExists(libraryCard.libraryCardID))
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
            return View(libraryCard);
        }

        // GET: LibraryCards/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.LibraryCard == null)
            {
                return NotFound();
            }

            var libraryCard = await _context.LibraryCard
                .FirstOrDefaultAsync(m => m.libraryCardID == id);
            if (libraryCard == null)
            {
                return NotFound();
            }

            return View(libraryCard);
        }

        // POST: LibraryCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.LibraryCard == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LibraryCard'  is null.");
            }
            var libraryCard = await _context.LibraryCard.FindAsync(id);
            if (libraryCard != null)
            {
                _context.LibraryCard.Remove(libraryCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryCardExists(string id)
        {
          return (_context.LibraryCard?.Any(e => e.libraryCardID == id)).GetValueOrDefault();
        }
    }
}
