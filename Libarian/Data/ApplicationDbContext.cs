using Libarian.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Libarian.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Book { get; set; }
        public DbSet<LibaryCard> LibaryCard { get; set; }
        public DbSet<LBook> LBooks { get; set; }

        public DbSet<CallCard> CallCard { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<Profile> Profile { get; set; }
        public DbSet<PenaltyTicket> PenaltyTicket { get; set; }
    }
}