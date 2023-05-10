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
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("utils/sortBookBy/{sortBy}")]
        public async Task<ActionResult<IEnumerable<Book>>> SortBookBy(string sortBy)
        {
            IQueryable<Book> query = _context.Book;

            query = sortBy.ToLower() switch
            {
                "title" => query.OrderBy(b => b.title),
                "author" => query.OrderBy(b => b.author),
                "publisher" => query.OrderBy(b => b.publisher),
                "publishingyear" => query.OrderBy(b => b.publishingYear),
                "summary" => query.OrderBy(b => b.summary),
                "count" => query.OrderBy(b => b.count),
                "adddate" => query.OrderBy(b => b.addDate),
                _ => query.OrderBy(b => b.title),
            };
            return await query.ToListAsync();
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook(int? cateId, string keyword = "", int searchOpt = 1 ,string orderBy = "addDate", bool asc = false, int limit = 20, int page = 1)
        {
            /*var book = from b in _context.Book
                       where
                            (cateId == null || b.categoryID == cateId)
                            && (b.title.ToLower().StartsWith(keyword.ToLower()))
                       select b;*/

            var book = _context.Book.Where(x =>
                (x.categoryID == cateId || cateId == null) &&
                (searchOpt == 2 && x.bookID.ToLower().Contains(keyword.ToLower())) ||
                (searchOpt == 3 && x.author.ToLower().Contains(keyword.ToLower())) ||
                (x.title.ToLower().Contains(keyword.ToLower()))
            );
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

		[HttpGet("utils/countBook")]
		public async Task<ActionResult<int>> CountBook(int? cateId, string keyword = "", int searchOpt = 1)
		{
            var book = _context.Book.Where(x =>
               (x.categoryID == cateId || cateId == null) &&
               (searchOpt == 2 && x.bookID.ToLower().Contains(keyword.ToLower())) ||
               (searchOpt == 3 && x.author.ToLower().Contains(keyword.ToLower())) ||
               (x.title.ToLower().Contains(keyword.ToLower()))
           );


            if (_context.Book == null)
			{
				return NotFound();
			}
            return Ok(book.Count());
		}

        [HttpGet("utils/popularBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> getPoluparBooks(int? limit = 6)
        {
            var books = from b in _context.Book
                        from l in _context.LBooks
                        where b.bookID == l.bookID
                        from c in _context.CallCard
                        where c.lBookID == l.lBookID
                        group b by b.bookID into g
                        select new { g.Key, count = g.Count() };

            books = books.OrderByDescending(x => x.count).Take(limit.Value);

            var res = from b in _context.Book
                      from t in books
                      where b.bookID == t.Key
                      select b;

            return new JsonResult(res);
        }

        // GET: api/Books
        [HttpGet("utils/getSearchHint")]
		public async Task<ActionResult<IEnumerable<string>>> SearchHint(int? cateId, string keyword = "", int searchOpt = 1)
		{
            var book = _context.Book.Where(x =>
                (x.categoryID == cateId || cateId == null) &&
                (searchOpt == 2 && x.bookID.ToLower().Contains(keyword.ToLower())) ||
                (searchOpt == 3 && x.author.ToLower().Contains(keyword.ToLower())) ||
                (x.title.ToLower().Contains(keyword.ToLower()))
            ).Select(x => (searchOpt == 2 ? x.bookID: (
                    searchOpt == 3? x.author: x.title
                ))
            );


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
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(string id, Book book)
        {
            if (id != book.bookID)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;
            _context.Entry(book).Property(x => x.bookIndex).IsModified = false;
            _context.Entry(book).Property(x => x.addDate).IsModified = false;

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
        [Authorize]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Book == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
          }
        
            book.addDate = DateTime.Now;
            book.count = 0;
            var tbook = _context.Book.OrderByDescending(c => c.bookID).FirstOrDefault();
            var autoID = tbook != null ? int.Parse(tbook.bookID.Substring(1, tbook.bookID.Length - 1)) + 1 + "" : "0001";
            book.bookID = "B" + autoID;
            //Bxxxx
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
        [Authorize]
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
