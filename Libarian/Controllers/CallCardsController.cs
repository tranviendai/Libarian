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
    public class CallCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CallCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CallCards
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CallCard.Include(c => c.lBook).Include(c => c.libaryCard);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CallCards/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.CallCard == null)
            {
                return NotFound();
            }

            var callCard = await _context.CallCard
                .Include(c => c.lBook)
                .Include(c => c.libaryCard)
                .FirstOrDefaultAsync(m => m.callCardID == id);
            if (callCard == null)
            {
                return NotFound();
            }

            return View(callCard);
        }

        // GET: CallCards/Create
        public IActionResult Create()
        {
            ViewData["lBookID"] = new SelectList(_context.LBooks, "lBookID", "lBookID");
            ViewData["libaryCardID"] = new SelectList(_context.LibaryCard, "libaryCardID", "libaryCardID");
            return View();
        }

        // POST: CallCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("callCardID,callCardIndex,startDate,deadline,endDate,bookStatus,libaryCardID,lBookID")] CallCard callCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(callCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["lBookID"] = new SelectList(_context.LBooks, "lBookID", "lBookID", callCard.lBookID);
            ViewData["libaryCardID"] = new SelectList(_context.LibaryCard, "libaryCardID", "libaryCardID", callCard.libaryCardID);
            return View(callCard);
        }

        // GET: CallCards/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.CallCard == null)
            {
                return NotFound();
            }

            var callCard = await _context.CallCard.FindAsync(id);
            if (callCard == null)
            {
                return NotFound();
            }
            ViewData["lBookID"] = new SelectList(_context.LBooks, "lBookID", "lBookID", callCard.lBookID);
            ViewData["libaryCardID"] = new SelectList(_context.LibaryCard, "libaryCardID", "libaryCardID", callCard.libaryCardID);
            return View(callCard);
        }

        // POST: CallCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("callCardID,callCardIndex,startDate,deadline,endDate,bookStatus,libaryCardID,lBookID")] CallCard callCard)
        {
            if (id != callCard.callCardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(callCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallCardExists(callCard.callCardID))
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
            ViewData["lBookID"] = new SelectList(_context.LBooks, "lBookID", "lBookID", callCard.lBookID);
            ViewData["libaryCardID"] = new SelectList(_context.LibaryCard, "libaryCardID", "libaryCardID", callCard.libaryCardID);
            return View(callCard);
        }

        // GET: CallCards/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.CallCard == null)
            {
                return NotFound();
            }

            var callCard = await _context.CallCard
                .Include(c => c.lBook)
                .Include(c => c.libaryCard)
                .FirstOrDefaultAsync(m => m.callCardID == id);
            if (callCard == null)
            {
                return NotFound();
            }

            return View(callCard);
        }

        // POST: CallCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.CallCard == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CallCard'  is null.");
            }
            var callCard = await _context.CallCard.FindAsync(id);
            if (callCard != null)
            {
                _context.CallCard.Remove(callCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallCardExists(string id)
        {
          return (_context.CallCard?.Any(e => e.callCardID == id)).GetValueOrDefault();
        }
    }
}
