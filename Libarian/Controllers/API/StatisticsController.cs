using Librarian.Data;
using Librarian.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

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


        [Route("Data/Books")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetBooksData(DateTime start, DateTime end)
        {
            var countBook = _context.Book.Count();

            var countLbook = _context.LBooks.Count();

            var countDamaged = _context.LBooks.Where(x => x.status == "Sách hỏng").Count();
            var countLost = _context.LBooks.Where(x => x.status == "Sách mất").Count();
            var countBorrowed = _context.LBooks.Where(x => x.status == "Đang mượn").Count();
            var countAvailable = _context.LBooks.Where(x => x.status == "Còn sách").Count();

            var tbooks = from b in _context.Book
                        from l in _context.LBooks
                        where b.bookID == l.bookID
                        from c in _context.CallCard
                        where c.lBookID == l.lBookID && c.startDate >= start && c.endDate <= end
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

            var lbookDamaged = from l in _context.LBooks
                               where l.status == "Sách hỏng"
                               from b in _context.Book
                               where b.bookID == l.bookID
                               select new { l.lBookID, b.title, l.note };

            return Ok(new { countBook, countLbook, countDamaged, countLost, countBorrowed, countAvailable, booksHigh, booksLow, lbookDamaged });
        }

        [Route("Data/Borrow")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetBorrowData(DateTime start, DateTime end)
        {
            var borrow = _context.CallCard.Where(x => x.startDate >= start && x.endDate <= end);

            var countBorrow = borrow.Count();
            var countReturn = borrow.Where(x => x.bookStatus == "Nguyên vẹn").Count();

            var months = new List<object>();

            var startDate = new DateTime(start.Year, start.Month, 1);
            //max 12 months
            for (DateTime i = startDate; i <= new DateTime(end.Year, end.Month, 1) && i < startDate.AddMonths(12); i = i.AddMonths(1)) {
                var t_borrow = borrow.Where(x => x.startDate >= i && x.startDate < i.AddMonths(1));
                var c_borrow = t_borrow.Count();
                var c_ret = t_borrow.Where(x => x.bookStatus == "Nguyên vẹn").Count();
                var sum = borrow.Where(x => x.startDate < i.AddMonths(1)).Count();

                months.Add(new { month = i.Month, borrow = c_borrow, ret = c_ret, sum });
            }

            return Ok(new { countBorrow, countReturn, months});
        }

        [Route("Data/Fine")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetFineData()
        {

            var countFine = _context.PenaltyTicket.Count();
            var totalFine = _context.PenaltyTicket.ToList().Sum(x => x.price);
            var unpaid = _context.PenaltyTicket.Where(x => !x.status);
            var countUnpaid = unpaid.Count();
            var totalUnpaid = unpaid.ToList().Sum(x => x.price);

            var countCard = _context.LibraryCard.Count();
            var countBan = _context.LibraryCard.Where(x => x.cardStatus == "No").Count();

            return Ok(new { countFine, totalFine, countUnpaid, totalUnpaid, countCard, countBan });
        }
    }
}
