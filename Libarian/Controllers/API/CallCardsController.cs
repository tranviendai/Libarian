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

            list = list.OrderByDescending(x => x.callCardIndex);
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

        [Route("utils/returnBooks/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> RetCallCard(string id, [FromBody] string state)
        {
            var callCard = _context.CallCard.Where(x => x.callCardID == id).FirstOrDefault();
            if (callCard == null) return NotFound();

            callCard.endDate = DateTime.Now;
            callCard.bookStatus = state;
            _context.Entry(callCard).State = EntityState.Modified;
            _context.Entry(callCard).Property(x => x.callCardIndex).IsModified = false;

            if (state != "Nguyên vẹn") {
                var lc = _context.LibraryCard.Where(x => x.libraryCardID == callCard.libraryCardID).FirstOrDefault();
                if (lc == null) return NotFound();
                int p = 0;
                switch (state) {
                    case "Sách hỏng":
                        p = 30000;
                        break;
                    case "Mất sách":
                        p = 40000;
                        break;
                    default: p = 20000; break;
                }
                PenaltyTicket pt = new PenaltyTicket()
                {
                    callCardID = id,
                    reason = state,
                    price = p,
                    status = false
                };
                _context.PenaltyTicket.Add(pt);

                lc.cardStatus = "No";
                _context.Entry(lc).State = EntityState.Modified;
                _context.Entry(lc).Property(x => x.librayCardIndex).IsModified = false;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!CallCardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return NoContent();
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
            LibraryCard lc = _context.LibraryCard.Where(x => x.libraryCardID == callCard.libraryCardID).FirstOrDefault();
            if (lc == null) return NotFound();
            if(lc.cardStatus == "No") 
                return StatusCode(StatusCodes.Status400BadRequest, new { Errors = new { restricted = new string[] { "Thẻ bị khóa" } } });
            int count = _context.CallCard.Where(x => x.libraryCardID == callCard.libraryCardID && x.endDate == null).Count();
            if(count >= 3) return StatusCode(StatusCodes.Status400BadRequest, new { Errors = new { restricted = new string[] { "Thẻ đang mượn quá 3 sách" } } });

            LBook lb = _context.LBooks.Where(x => x.lBookID == callCard.lBookID).FirstOrDefault();
            if(lb == null) return NotFound();
            if(lb.status != "Còn sách")
                return StatusCode(StatusCodes.Status400BadRequest, new { Errors = new { notAvailable = new string[] { "Sách không có sẵn để mượn" } } });

            string lBookID = callCard.lBookID;
            string libraryCardID = callCard.libraryCardID;
            var c = _context.CallCard.OrderByDescending(x => x.callCardIndex).FirstOrDefault();
            var id = c == null ? 1 : c.callCardIndex;
            string newId = "C" + (1000 + id);

            callCard.callCardID = newId;
            callCard.startDate = DateTime.Now;
            callCard.deadline = DateTime.Now.AddDays(7);
            _context.CallCard.Add(callCard);

            //Update status
            var lBook = await _context.LBooks.FindAsync(lBookID);
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
                throw;
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
