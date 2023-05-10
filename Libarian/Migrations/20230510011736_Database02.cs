using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librarian.Migrations
{
    /// <inheritdoc />
    public partial class Database02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a79e98b4-d8a6-4640-98eb-5b417ffb2661",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "birthday", "startProfile" },
                values: new object[] { "723cede2-9a61-455d-be56-3cd91af972ce", "AQAAAAIAAYagAAAAEKYadwUpG7D5Y7aRJVCTW9fo91vZ8TSw8lvG4CSP9lqPO2lxLZ4LBGcPgy6ytZO1Pg==", "1d600667-dfff-421f-9aa2-48a7e09c9332", new DateTime(2023, 5, 10, 8, 17, 35, 861, DateTimeKind.Local).AddTicks(2239), new DateTime(2023, 5, 10, 8, 17, 35, 861, DateTimeKind.Local).AddTicks(2252) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a79e98b4-d8a6-4640-98eb-5b417ffb2661",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "birthday", "startProfile" },
                values: new object[] { "03acab66-0329-4322-bc49-8ab17b8a76f3", "AQAAAAIAAYagAAAAEBvohYFiRol+dVV3Y7AE2UIiNvZFvivojCk+84deCFLry3zpz0RiUWBH+zFNhvl5og==", "ec2175cd-6e08-493f-b661-cdf755edcc21", new DateTime(2023, 5, 10, 8, 15, 47, 127, DateTimeKind.Local).AddTicks(9163), new DateTime(2023, 5, 10, 8, 15, 47, 127, DateTimeKind.Local).AddTicks(9174) });
        }
    }
}
