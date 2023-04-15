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
    public class LBooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LBooks
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<LBook>>> GetLBooks(string? bookID, string? status)
        {
          if (_context.LBooks == null)
          {
              return NotFound();
          }
            return await _context.LBooks.Where(x => (bookID == null || x.bookID == bookID) && (status == null || x.status == status)).ToListAsync();
        }

        // GET: api/LBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LBook>> GetLBook(string id)
        {
          if (_context.LBooks == null)
          {
              return NotFound();
          }
            var lBook = await _context.LBooks.FindAsync(id);

            if (lBook == null)
            {
                return NotFound();
            }

            return lBook;
        }

        // PUT: api/LBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutLBook(string id, LBook lBook)
        {
            if (id != lBook.lBookID)
            {
                return BadRequest();
            }

            _context.Entry(lBook).State = EntityState.Modified;
            _context.Entry(lBook).Property(x => x.lBookIndex).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LBookExists(id))
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

        // POST: api/LBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LBook>> PostLBook(LBook lBook)
        {
          if (_context.LBooks == null)
          {
              return Problem("Entity set 'ApplicationDbContext.LBooks'  is null.");
          }
            /*string[]StatusArray= new string[4] {"Còn sách","Đang mượn","Sách hỏng","Sách mất"};
            if(!StatusArray.Contains(lBook.status))
            { return Problem("Không có trạng thái đã nhập!");}    */
            lBook.status = "Còn sách";
            var lbook = _context.LBooks.OrderByDescending(c => c.lBookIndex).FirstOrDefault();

            var autoID = lbook != null ? lbook.lBookIndex + 1 : 1;
            
            autoID += 10;
            lBook.lBookID = lBook.bookID + "L"+ autoID;
            _context.LBooks.Add(lBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LBookExists(lBook.lBookID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLBook", new { id = lBook.lBookID }, lBook);
        }

        // DELETE: api/LBooks/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLBook(string id)
        {
            if (_context.LBooks == null)
            {
                return NotFound();
            }
            var lBook = await _context.LBooks.FindAsync(id);
            if (lBook == null)
            {
                return NotFound();
            }

            _context.LBooks.Remove(lBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LBookExists(string id)
        {
            return (_context.LBooks?.Any(e => e.lBookID == id)).GetValueOrDefault();
        }
    }
}
