using Librarian.Data;
using Librarian.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Librarian.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetStatistics()
        {
            var countBook = _context.Book.Count();

            var countLbook = _context.LBooks.Count();

            var countCard = _context.LibraryCard.Count();

            var countBorrow = _context.CallCard.Count();

            var tbooks = from b in _context.Book
                        from l in _context.LBooks
                        where b.bookID == l.bookID
                        from c in _context.CallCard
                        where c.lBookID == l.lBookID
                        group b by b.bookID into g
                        select new { g.Key, count = g.Count() };
            
            var booksHigh = from b in _context.Book
                           from t in tbooks.OrderByDescending(x => x.count).Take(10)
                           where b.bookID == t.Key
                           select new { count = t.count, book = b };
            var booksLow = from b in _context.Book
                           from t in tbooks.OrderBy(x => x.count).Take(10)
                           where b.bookID == t.Key
                           select new { count = t.count, book = b };

            var countFine = _context.PenaltyTicket.Count();

            var totalFine = _context.PenaltyTicket.ToList().Sum(x => x.price);



            return Ok(new { countBook, countLbook , countCard, countBorrow, booksHigh, booksLow, countFine, totalFine });
        }
    }
}
