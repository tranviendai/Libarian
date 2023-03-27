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
    public class LBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LBooks
        public async Task<IActionResult> Index(string bookID)
        {
            var lBooks = _context.LBooks.AsQueryable();

            if (!string.IsNullOrEmpty(bookID))
            {
                lBooks = lBooks.Where(b => b.bookID.Contains(bookID));
            }

            return View(await lBooks.ToListAsync());
        }

        // GET: LBooks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.LBooks == null)
            {
                return NotFound();
            }

            var lBook = await _context.LBooks
                .FirstOrDefaultAsync(m => m.lBookID == id);
            if (lBook == null)
            {
                return NotFound();
            }

            return View(lBook);
        }

        // GET: LBooks/Create
        public IActionResult Create()
        {
            ViewBag.BookID = new SelectList(_context.Book, "bookID", "title");
            return View();
        }

        // POST: LBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("lBookID,lBookIndex,status,note,bookID")] LBook lBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.BookID = new SelectList(_context.Book, "bookID", "title");
            return View(lBook);
        }

        // GET: LBooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.LBooks == null)
            {
                return NotFound();
            }

            var lBook = await _context.LBooks.FindAsync(id);
            if (lBook == null)
            {
                return NotFound();
            }
            return View(lBook);
        }

        // POST: LBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("lBookID,lBookIndex,status,note,bookID")] LBook lBook)
        {
            if (id != lBook.lBookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LBookExists(lBook.lBookID))
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
            return View(lBook);
        }

        // GET: LBooks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.LBooks == null)
            {
                return NotFound();
            }

            var lBook = await _context.LBooks
                .FirstOrDefaultAsync(m => m.lBookID == id);
            if (lBook == null)
            {
                return NotFound();
            }

            return View(lBook);
        }

        // POST: LBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.LBooks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LBooks'  is null.");
            }
            var lBook = await _context.LBooks.FindAsync(id);
            if (lBook != null)
            {
                _context.LBooks.Remove(lBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LBookExists(string id)
        {
            return (_context.LBooks?.Any(e => e.lBookID == id)).GetValueOrDefault();
        }
    }
}
