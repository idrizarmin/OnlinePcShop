using Microsoft.EntityFrameworkCore.Migrations;

namespace PCWebShop.Migrations
{
    public partial class UserTokenAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            

            migrationBuilder.AddColumn<string>(
                name: "UserToken",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "UserToken",
                table: "Korisnik");

            
        }
    }
}
