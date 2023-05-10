﻿// <auto-generated />
using System;
using Librarian.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Libarian.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Librarian.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasMaxLength(42)
                        .HasColumnType("nvarchar(42)");

                    b.Property<string>("sex")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<DateTime>("startProfile")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a79e98b4-d8a6-4640-98eb-5b417ffb2661",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "723cede2-9a61-455d-be56-3cd91af972ce",
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEKYadwUpG7D5Y7aRJVCTW9fo91vZ8TSw8lvG4CSP9lqPO2lxLZ4LBGcPgy6ytZO1Pg==",
                            PhoneNumber = "0582072743",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1d600667-dfff-421f-9aa2-48a7e09c9332",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com",
                            address = "Tắc Vân - Cà Mau",
                            birthday = new DateTime(2023, 5, 10, 8, 17, 35, 861, DateTimeKind.Local).AddTicks(2239),
                            fullName = "Trần Viễn Đại",
                            sex = "Nam",
                            startProfile = new DateTime(2023, 5, 10, 8, 17, 35, 861, DateTimeKind.Local).AddTicks(2252)
                        });
                });

            modelBuilder.Entity("Librarian.Models.Book", b =>
                {
                    b.Property<string>("bookID")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("addDate")
                        .HasColumnType("Date");

                    b.Property<string>("author")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<int>("bookIndex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bookIndex"));

                    b.Property<int>("categoryID")
                        .HasColumnType("int");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("publisher")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("publishingYear")
                        .HasColumnType("Date");

                    b.Property<string>("summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("bookID");

                    b.HasIndex("categoryID");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("Librarian.Models.CallCard", b =>
                {
                    b.Property<string>("callCardID")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("bookStatus")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<int>("callCardIndex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("callCardIndex"));

                    b.Property<DateTime>("deadline")
                        .HasColumnType("Date");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("Date");

                    b.Property<string>("lBookID")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("libraryCardID")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("Date");

                    b.HasKey("callCardID");

                    b.HasIndex("lBookID");

                    b.HasIndex("libraryCardID");

                    b.ToTable("CallCard");
                });

            modelBuilder.Entity("Librarian.Models.Category", b =>
                {
                    b.Property<int>("categoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("categoryID"));

                    b.Property<string>("nameCategory")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("categoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Librarian.Models.LBook", b =>
                {
                    b.Property<string>("lBookID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("bookID")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("lBookIndex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("lBookIndex"));

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("lBookID");

                    b.HasIndex("bookID");

                    b.ToTable("LBooks");
                });

            modelBuilder.Entity("Librarian.Models.LibraryCard", b =>
                {
                    b.Property<string>("libraryCardID")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("cardStatus")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime>("expirationDate")
                        .HasColumnType("Date");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<int>("librayCardIndex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("librayCardIndex"));

                    b.Property<DateTime>("startDate")
                        .HasColumnType("Date");

                    b.HasKey("libraryCardID");

                    b.ToTable("LibraryCard");
                });

            modelBuilder.Entity("Librarian.Models.PenaltyTicket", b =>
                {
                    b.Property<string>("callCardID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("price")
                        .HasColumnType("Money");

                    b.Property<string>("reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.HasKey("callCardID");

                    b.ToTable("PenaltyTicket");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "07bf1560-b5ff-4702-a9f1-a64026e570cf",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2ccdcef3-db18-46c7-b5ff-910be6ae4906",
                            Name = "Thủ Thư",
                            NormalizedName = "THỦ THƯ"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "a79e98b4-d8a6-4640-98eb-5b417ffb2661",
                            RoleId = "07bf1560-b5ff-4702-a9f1-a64026e570cf"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Librarian.Models.Book", b =>
                {
                    b.HasOne("Librarian.Models.Category", "category")
                        .WithMany("books")
                        .HasForeignKey("categoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("Librarian.Models.CallCard", b =>
                {
                    b.HasOne("Librarian.Models.LBook", "lBook")
                        .WithMany()
                        .HasForeignKey("lBookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Librarian.Models.LibraryCard", "libaryCard")
                        .WithMany("callCards")
                        .HasForeignKey("libraryCardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lBook");

                    b.Navigation("libaryCard");
                });

            modelBuilder.Entity("Librarian.Models.LBook", b =>
                {
                    b.HasOne("Librarian.Models.Book", "Books")
                        .WithMany("lBooks")
                        .HasForeignKey("bookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Books");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Librarian.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Librarian.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Librarian.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Librarian.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Librarian.Models.Book", b =>
                {
                    b.Navigation("lBooks");
                });

            modelBuilder.Entity("Librarian.Models.Category", b =>
                {
                    b.Navigation("books");
                });

            modelBuilder.Entity("Librarian.Models.LibraryCard", b =>
                {
                    b.Navigation("callCards");
                });
#pragma warning restore 612, 618
        }
    }
}
