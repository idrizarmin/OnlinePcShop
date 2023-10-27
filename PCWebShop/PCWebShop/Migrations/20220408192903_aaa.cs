using Microsoft.EntityFrameworkCore.Migrations;

namespace PCWebShop.Migrations
{
    public partial class aaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "slika_korisnika",
                table: "KorisnickiNalog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "slika_korisnika",
                table: "KorisnickiNalog",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
