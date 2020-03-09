using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_T_Shirt_Shop.Migrations.Product
{
    public partial class CreatedT_Shirt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Shirt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    InStock = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Shirt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Shirt");
        }
    }
}
