﻿using Librarian.Data;
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


        [Route("Data/Books")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetBooksData()
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

            return Ok(new { countBook, countLbook, countDamaged, countLost, countBorrowed, countAvailable, booksHigh, booksLow });
        }

        [Route("Data/LibCard")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryCard>>> GetCardsData()
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



            return Ok(new { countBook, countLbook, countCard, countBorrow, booksHigh, booksLow, countFine, totalFine });
        }
    }
}
