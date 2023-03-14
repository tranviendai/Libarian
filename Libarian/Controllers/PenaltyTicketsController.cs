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
    public class PenaltyTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PenaltyTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PenaltyTickets
        public async Task<IActionResult> Index()
        {
              return _context.PenaltyTicket != null ? 
                          View(await _context.PenaltyTicket.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PenaltyTicket'  is null.");
        }

        // GET: PenaltyTickets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PenaltyTicket == null)
            {
                return NotFound();
            }

            var penaltyTicket = await _context.PenaltyTicket
                .FirstOrDefaultAsync(m => m.callCardID == id);
            if (penaltyTicket == null)
            {
                return NotFound();
            }

            return View(penaltyTicket);
        }

        // GET: PenaltyTickets/Create
        public IActionResult Create()
        {
            ViewBag.callCardID = new SelectList(_context.CallCard, "callCardID", "callCardID");
            return View();
        }

        // POST: PenaltyTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("callCardID,price,reason,status")] PenaltyTicket penaltyTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(penaltyTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.callCardID = new SelectList(_context.CallCard, "callCardID", "callCardID");
            return View(penaltyTicket);
        }

        // GET: PenaltyTickets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PenaltyTicket == null)
            {
                return NotFound();
            }

            var penaltyTicket = await _context.PenaltyTicket.FindAsync(id);
            if (penaltyTicket == null)
            {
                return NotFound();
            }
            return View(penaltyTicket);
        }

        // POST: PenaltyTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("callCardID,price,reason,status")] PenaltyTicket penaltyTicket)
        {
            if (id != penaltyTicket.callCardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penaltyTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenaltyTicketExists(penaltyTicket.callCardID))
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
            return View(penaltyTicket);
        }

        // GET: PenaltyTickets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PenaltyTicket == null)
            {
                return NotFound();
            }

            var penaltyTicket = await _context.PenaltyTicket
                .FirstOrDefaultAsync(m => m.callCardID == id);
            if (penaltyTicket == null)
            {
                return NotFound();
            }

            return View(penaltyTicket);
        }

        // POST: PenaltyTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PenaltyTicket == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PenaltyTicket'  is null.");
            }
            var penaltyTicket = await _context.PenaltyTicket.FindAsync(id);
            if (penaltyTicket != null)
            {
                _context.PenaltyTicket.Remove(penaltyTicket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenaltyTicketExists(string id)
        {
          return (_context.PenaltyTicket?.Any(e => e.callCardID == id)).GetValueOrDefault();
        }
    }
}
