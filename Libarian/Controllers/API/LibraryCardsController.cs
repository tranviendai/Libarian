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

        // GET: api/LibraryCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetLibraryCard()
        {
          if (_context.LibraryCard == null)
          {
              return NotFound();
          }
            return await _context.LibraryCard.ToListAsync();
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

            if (libraryCard == null)
            {
                return NotFound();
            }

            return libraryCard;
        }

        // PUT: api/LibraryCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutLibraryCard(string id, LibraryCard libraryCard)
        {
            if (id != libraryCard.libraryCardID)
            {
                return BadRequest();
            }

            _context.Entry(libraryCard).State = EntityState.Modified;

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

            _context.LibraryCard.Remove(libraryCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibraryCardExists(string id)
        {
            return (_context.LibraryCard?.Any(e => e.libraryCardID == id)).GetValueOrDefault();
        }
    }
}
