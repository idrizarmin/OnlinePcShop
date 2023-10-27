using Microsoft.EntityFrameworkCore.Migrations;

namespace PCWebShop.Migrations
{
    public partial class PopravkaProizvoda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "slikaProizvoda",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "slika_korisnika",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "Dostavljac");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "Dostavljac");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "slikaProizvoda",
                table: "Proizvod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "slika_korisnika",
                table: "KorisnickiNalog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "Dostavljac",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "Dostavljac",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
