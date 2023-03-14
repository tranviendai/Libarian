using Librarian.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Librarian.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        const string ADMIN_USER_GUID = "a79e98b4-d8a6-4640-98eb-5b417ffb2661";
        const string ADMIN_ROLE_GUID = "07bf1560-b5ff-4702-a9f1-a64026e570cf";
        const string THUTHU_ROLE_GUID = "2ccdcef3-db18-46c7-b5ff-910be6ae4906";
        private ApplicationUser user;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
                entity.HasData(
                    new IdentityRole
                    {
                        Id = ADMIN_ROLE_GUID,
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Id = THUTHU_ROLE_GUID,
                        Name = "Thủ Thư",
                        NormalizedName = "THỦ THƯ"
                    });
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = ADMIN_ROLE_GUID,
                        UserId = ADMIN_USER_GUID,
                    });
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = ADMIN_USER_GUID,
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    PasswordHash = hasher.HashPassword(user, "Admin@123"),
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    fullName = "Trần Viễn Đại",
                    PhoneNumber = "0582072743",
                    address = "Tắc Vân - Cà Mau",
                    birthday = DateTime.Now,
                    startProfile = DateTime.Now,
                    sex = "Nam",
                });

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Book { get; set; }
        public DbSet<LibraryCard> LibraryCard { get; set; }
        public DbSet<LBook> LBooks { get; set; }

        public DbSet<CallCard> CallCard { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<PenaltyTicket> PenaltyTicket { get; set; }
    }

}