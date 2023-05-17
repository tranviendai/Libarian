using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Librarian.Data;
using Librarian.Models;

namespace Librarian.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenaltyTicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PenaltyTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PenaltyTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PenaltyTicket>>> GetPenaltyTicket(int page=1, bool paid = false, string cardId = "", string borrowId="")
        {
          if (_context.PenaltyTicket == null)
          {
              return NotFound();
          }
            

            var list = from p in _context.PenaltyTicket
                       where p.status == paid && p.callCardID.ToLower().Contains(borrowId.ToLower())
                       from c in _context.CallCard
                       where p.callCardID == c.callCardID && c.libraryCardID.ToLower().Contains(cardId.ToLower())
                       select new { p.callCardID, c.libraryCardID, p.reason, p.price, p.status };

            const int limit = 20;
            int total = list.Count();
            int pageCount = (int)Math.Max(1, Math.Ceiling(total * 1.0f / limit));

            page = Math.Max(page, 1);
            list = list.Skip((page - 1) * limit).Take(limit);

            return new JsonResult(new { pageCount, list });
        }

        // GET: api/PenaltyTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PenaltyTicket>> GetPenaltyTicket(string id)
        {
          if (_context.PenaltyTicket == null)
          {
              return NotFound();
          }
            var penaltyTicket = await _context.PenaltyTicket.FindAsync(id);

            if (penaltyTicket == null)
            {
                return NotFound();
            }

            return penaltyTicket;
        }

        // PUT: api/PenaltyTickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPenaltyTicket(string id, PenaltyTicket penaltyTicket)
        {
            if (id != penaltyTicket.callCardID)
            {
                return BadRequest();
            }

            _context.Entry(penaltyTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaltyTicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PenaltyTickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PenaltyTicket>> PostPenaltyTicket(PenaltyTicket penaltyTicket)
        {
          if (_context.PenaltyTicket == null)
          {
              return Problem("Entity set 'ApplicationDbContext.PenaltyTicket'  is null.");
          }
            _context.PenaltyTicket.Add(penaltyTicket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PenaltyTicketExists(penaltyTicket.callCardID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPenaltyTicket", new { id = penaltyTicket.callCardID }, penaltyTicket);
        }

        // DELETE: api/PenaltyTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePenaltyTicket(string id)
        {
            if (_context.PenaltyTicket == null)
            {
                return NotFound();
            }
            var penaltyTicket = await _context.PenaltyTicket.FindAsync(id);
            if (penaltyTicket == null)
            {
                return NotFound();
            }

            penaltyTicket.status = true;
            _context.Entry(penaltyTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaltyTicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PenaltyTicketExists(string id)
        {
            return (_context.PenaltyTicket?.Any(e => e.callCardID == id)).GetValueOrDefault();
        }
    }
}
