using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Librarian.Data;
using Librarian.Models;
using Microsoft.AspNetCore.Identity;

namespace Librarian.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook(int? cateId, string keyword = "", string orderBy = "addDate", bool asc = false, int limit = 20, int page = 1)
        {
            var book = from b in _context.Book
                       where
                            (cateId == null || b.categoryID == cateId)
                            && (b.title.ToLower().StartsWith(keyword.ToLower()))
                       select b;

            //order by
            string[] orderCols = new string[] { "addDate", "title" };
            if (orderCols.Contains(orderBy))
                book = book.OrderBy(x => EF.Property<Book>(x, orderBy)).ThenBy(x => x.bookIndex);
            if (!asc) book = book.Reverse();

            //paging
            limit = Math.Max(limit, 1);
            page = Math.Max(page, 1);
            book = book.Skip((page - 1) * limit).Take(limit);

          if (_context.Book == null)
          {
              return NotFound();
          }
            return Ok(await book.ToListAsync());
        }

		// GET: api/Books
		[HttpGet("utils/countBook")]
		public async Task<ActionResult<int>> CountBook(int? cateId, string keyword = "")
		{
            var book = from b in _context.Book
                       where
                            (cateId == null || b.categoryID == cateId)
                            && (b.title.ToLower().StartsWith(keyword.ToLower()))
                       select b;


			if (_context.Book == null)
			{
				return NotFound();
			}
            return Ok(book.Count());
		}

		// GET: api/Books
		[HttpGet("utils/getSearchHint")]
		public async Task<ActionResult<IEnumerable<string>>> SearchHint(int? cateId, string keyword = "")
		{
            var book = from b in _context.Book
                       where
                            (cateId == null || b.categoryID == cateId)
                            && (b.title.ToLower().StartsWith(keyword.ToLower()))
                       select b.title;


			if (_context.Book == null)
			{
				return NotFound();
			}
            return Ok(await book.ToListAsync());
		}



		// GET: api/Books/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
          if (_context.Book == null)
          {
              return NotFound();
          }

            var book = _context.Book.FirstOrDefault(x => x.bookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(string id, Book book)
        {
            if (id != book.bookID)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Book == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
          }
            _context.Book.Add(book);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.bookID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBook", new { id = book.bookID }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            if (_context.Book == null)
            {
                return NotFound();
            }
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(string id)
        {
            return (_context.Book?.Any(e => e.bookID == id)).GetValueOrDefault();
        }
    }
}
