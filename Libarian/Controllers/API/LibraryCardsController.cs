using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Librarian.Data;
using Librarian.Models;
using Microsoft.AspNetCore.Authorization;

namespace Librarian.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryCardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibraryCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Khóa thẻ
        // Put: api/LibraryCards/utils/lockLibraryCard/{id}
        [HttpPut("utils/lockLibraryCard/{id}")]
        public async Task<IActionResult> LockLibraryCard(int id)
        {
            var libraryCard = await _context.LibraryCard.FindAsync(id);
            if (libraryCard == null) return NotFound();
            if (libraryCard.cardStatus == "No")
                return BadRequest("Card is already locked!");
            libraryCard.cardStatus = "No";
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: api/LibraryCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetLibraryCard(string searchID = "", string searchName = "", int page = 1, int pageLength = 20, bool active = true)
        {
            if (_context.LibraryCard == null)
            {
                return NotFound();
            }
            string state = active ? "Yes" : "No";
            var list = await _context.LibraryCard
                .Where(x =>
                    x.fullName.ToLower().Contains(searchName.ToLower())
                    && x.libraryCardID.ToLower().Contains(searchID.ToLower())
                    && x.cardStatus == state
                ).ToListAsync();


                
            var count = Math.Ceiling(1.0f * list.Count() / pageLength);
            if (count < 1) count = 1;
            page = Math.Max(page, 1);
            pageLength = Math.Max(pageLength, 1);

            list = list.Skip((page - 1) * pageLength).Take(pageLength).ToList();

            return new JsonResult(new { PageCount = count, list = list });
        }

        [HttpGet("utils/rawList")]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetLibraryCardList(string searchID = "", string searchName = "", int page = 1, int pageLength = 20, bool active = true)
        {
            if (_context.LibraryCard == null)
            {
                return NotFound();
            }
            string state = active ? "Yes" : "No";
            var list = await _context.LibraryCard.Include(x => x.callCards).ThenInclude(x => x.lBook).ThenInclude(x => x.Books)
                .Where(x =>
                    x.fullName.ToLower().Contains(searchName.ToLower())
                    && x.libraryCardID.ToLower().Contains(searchID.ToLower())
                    && x.cardStatus == state
                ).ToListAsync();

            var count = Math.Ceiling(1.0f * list.Count() / pageLength);
            if (count < 1) count = 1;
            page = Math.Max(page, 1);
            pageLength = Math.Max(pageLength, 1);

            list = list.Skip((page - 1) * pageLength).Take(pageLength).ToList();

            return new JsonResult(list);
        }

        // GET: api/LibraryCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryCard>> GetLibraryCard(string id)
        {
            if (_context.LibraryCard == null)
            {
                return NotFound();
            }
            var libraryCard = await _context.LibraryCard.FindAsync(id);
            var history = from cc in _context.CallCard
                          where cc.libraryCardID == id
                          from lb in _context.LBooks
                          where cc.lBookID == lb.lBookID
                          from b in _context.Book
                          where b.bookID == lb.bookID
                          select new { cc.callCardID, b.title, cc.lBookID, cc.startDate, cc.deadline, cc.endDate, cc.bookStatus };


            if (libraryCard == null)
            {
                return NotFound();
            }

            return new JsonResult(new { detail = libraryCard, history = history.ToList() });
        }

        // PUT: api/LibraryCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutLibraryCard(string id, LibraryCard libraryCard)
        {

            //var libraryCard = await _context.LibraryCard.FindAsync(id);
            //libraryCard.expirationDate= expirationDate;
            if (id != libraryCard.libraryCardID)
            {
                return BadRequest();
            }

            _context.Entry(libraryCard).State = EntityState.Modified;
            _context.Entry(libraryCard).Property(x => x.fullName).IsModified = false;
            _context.Entry(libraryCard).Property(x => x.startDate).IsModified = false;
            _context.Entry(libraryCard).Property(x => x.cardStatus).IsModified = false;

            _context.Entry(libraryCard).Property(x => x.librayCardIndex).IsModified = false;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryCardExists(id))
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

        // POST: api/LibraryCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LibraryCard>> PostLibraryCard(LibraryCard libraryCard)
        {
            if (_context.LibraryCard == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LibraryCard'  is null.");
            }
            libraryCard.startDate = DateTime.Now;
            libraryCard.cardStatus = "Yes";
            libraryCard.expirationDate= DateTime.Now.AddYears(4);
            var tlib = _context.LibraryCard.OrderByDescending(c => c.libraryCardID).FirstOrDefault();
            var autoID = tlib != null ? int.Parse(tlib.libraryCardID.Substring(1, tlib.libraryCardID.Length - 1)) + 1 + "" : "0001";
            libraryCard.libraryCardID = "H" + autoID;
            _context.LibraryCard.Add(libraryCard);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LibraryCardExists(libraryCard.libraryCardID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLibraryCard", new { id = libraryCard.libraryCardID }, libraryCard);
        }

        // DELETE: api/LibraryCards/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLibraryCard(string id)
        {
            if (_context.LibraryCard == null)
            {
                return NotFound();
            }
            var libraryCard = await _context.LibraryCard.FindAsync(id);
            if (libraryCard == null)
            {
                return NotFound();
            }
            if (libraryCard.cardStatus == "Yes")
                libraryCard.cardStatus = "No";
            else libraryCard.cardStatus = "Yes";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryCardExists(id))
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

        private bool LibraryCardExists(string id)
        {
            return (_context.LibraryCard?.Any(e => e.libraryCardID == id)).GetValueOrDefault();
        }
    }
}
