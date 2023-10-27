using Microsoft.EntityFrameworkCore.Migrations;

namespace PCWebShop.Migrations
{
    public partial class AddedField_NaStanju_IntoProizvod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NaStanju",
                table: "Proizvod",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NaStanju",
                table: "Proizvod");
        }
    }
}
