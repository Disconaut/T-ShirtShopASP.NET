using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_T_Shirt_Shop.Migrations
{
    public partial class AddedOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "968f7b77-8675-4f4d-b225-91e202941f96", "relaxer@rlx.net", true, null, "Man", null, false, null, null, null, null, null, false, "96639828-c965-4dec-b3cf-ebe82709c6ce", false, "Relaxer" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ConsumerId", "Date", "Submission" },
                values: new object[] { 1, "1", new DateTime(2020, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 29.99m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
