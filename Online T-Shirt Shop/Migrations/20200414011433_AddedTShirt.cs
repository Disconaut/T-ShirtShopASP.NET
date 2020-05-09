using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_T_Shirt_Shop.Migrations
{
    public partial class AddedTShirt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Age", "Description", "ImagePath", "KeywordsJson", "Name", "Sex" },
                values: new object[] { 1, (byte)1, "Normal T-Shirt", "E:\\Coding\\Online T-Shirt Shop\\Online T-Shirt Shop\\wwwroot\\assets\\img\\product_img\\KingCardWhite.png", "[]", "King Card", (byte)0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
