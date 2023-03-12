using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libarian.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataBase01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "LibaryCard",
                columns: table => new
                {
                    libaryCardID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    librayCardIndex = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullName = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    cardStatus = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    startDate = table.Column<DateTime>(type: "Date", nullable: false),
                    expirationDate = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibaryCard", x => x.libaryCardID);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(42)", maxLength: 42, nullable: false),
                    sex = table.Column<bool>(type: "bit", nullable: false),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startProfile = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    bookID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    bookIndex = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    author = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publisher = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    publishingYear = table.Column<DateTime>(type: "Date", nullable: false),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    addDate = table.Column<DateTime>(type: "Date", nullable: false),
                    categoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.bookID);
                    table.ForeignKey(
                        name: "FK_Book_Category_categoryID",
                        column: x => x.categoryID,
                        principalTable: "Category",
                        principalColumn: "categoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LBooks",
                columns: table => new
                {
                    lBookID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    lBookIndex = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bookID = table.Column<string>(type: "nvarchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LBooks", x => x.lBookID);
                    table.ForeignKey(
                        name: "FK_LBooks_Book_bookID",
                        column: x => x.bookID,
                        principalTable: "Book",
                        principalColumn: "bookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CallCard",
                columns: table => new
                {
                    callCardID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    callCardIndex = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startDate = table.Column<DateTime>(type: "Date", nullable: false),
                    deadlineDate = table.Column<DateTime>(type: "Date", nullable: false),
                    endDate = table.Column<DateTime>(type: "Date", nullable: false),
                    bookStatus = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    libaryCardID = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    lBookID = table.Column<string>(type: "nvarchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallCard", x => x.callCardID);
                    table.ForeignKey(
                        name: "FK_CallCard_LBooks_lBookID",
                        column: x => x.lBookID,
                        principalTable: "LBooks",
                        principalColumn: "lBookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CallCard_LibaryCard_libaryCardID",
                        column: x => x.libaryCardID,
                        principalTable: "LibaryCard",
                        principalColumn: "libaryCardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PenaltyTicket",
                columns: table => new
                {
                    callCardID = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    price = table.Column<decimal>(type: "Money", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PenaltyTicket", x => x.callCardID);
                    table.ForeignKey(
                        name: "FK_PenaltyTicket_CallCard_callCardID",
                        column: x => x.callCardID,
                        principalTable: "CallCard",
                        principalColumn: "callCardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_categoryID",
                table: "Book",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CallCard_lBookID",
                table: "CallCard",
                column: "lBookID");

            migrationBuilder.CreateIndex(
                name: "IX_CallCard_libaryCardID",
                table: "CallCard",
                column: "libaryCardID");

            migrationBuilder.CreateIndex(
                name: "IX_LBooks_bookID",
                table: "LBooks",
                column: "bookID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PenaltyTicket");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "CallCard");

            migrationBuilder.DropTable(
                name: "LBooks");

            migrationBuilder.DropTable(
                name: "LibaryCard");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
