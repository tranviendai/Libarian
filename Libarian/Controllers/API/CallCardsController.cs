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
    public class CallCardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CallCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CallCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CallCard>>> GetCallCard(int page = 1, int limit = 20, string id = "", string bookID = "", string cardID = "", bool returned = false)
        {
            if (_context.CallCard == null)
            {
                return NotFound();
            }

            var list = from c in _context.CallCard
                       where c.callCardID.ToLower().Contains(id.ToLower())
                           && c.lBookID.ToLower().Contains(bookID.ToLower())
                           && c.libraryCardID.ToLower().Contains(cardID.ToLower())
                           && ((returned && c.endDate != null) || (!returned && c.endDate == null))
                       select c;

            int total = list.Count();
            int pageCount = (int)Math.Max(1, Math.Ceiling(total * 1.0f / limit));

            //paging
            limit = Math.Max(limit, 1);
            page = Math.Max(page, 1);
            list = list.Skip((page - 1) * limit).Take(limit);

            return new JsonResult(new { pageCount, list });
        }

        // GET: api/CallCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CallCard>> GetCallCard(string id)
        {
            if (_context.CallCard == null)
            {
                return NotFound();
            }
            var callCard = await _context.CallCard.FindAsync(id);

            if (callCard == null)
            {
                return NotFound();
            }

            return callCard;
        }

        // PUT: api/CallCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCallCard(string id, CallCard callCard)
        {
            if (id != callCard.callCardID)
            {
                return BadRequest();
            }

            _context.Entry(callCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallCardExists(id))
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

        [HttpPost]
        public async Task<IActionResult> PostCallCard(CallCard callCard)
        {
            if (callCard == null)
            {
                return BadRequest("CallCard object is null.");
            }
            _context.CallCard.Add(callCard);

            //Update status
            var lBook = await _context.LBooks.FindAsync(callCard.lBookID);
            if (lBook == null)
            {
                return NotFound("LBook not found.");
            }
            lBook.status = "Đang mượn";
            //_context.LBooks.Update(lBook);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CallCardExists(callCard.callCardID))
                {
                    return Conflict("CallCard already exists.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/CallCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCallCard(string id)
        {
            if (_context.CallCard == null)
            {
                return NotFound();
            }
            var callCard = await _context.CallCard.FindAsync(id);
            if (callCard == null)
            {
                return NotFound();
            }

            _context.CallCard.Remove(callCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CallCardExists(string id)
        {
            return (_context.CallCard?.Any(e => e.callCardID == id)).GetValueOrDefault();
        }
    }
}
