using Microsoft.EntityFrameworkCore.Migrations;

namespace PCWebShop.Migrations
{
    public partial class AddedAdressFieldToKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresa1",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adresa2",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa1",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Adresa2",
                table: "Korisnik");
        }
    }
}
