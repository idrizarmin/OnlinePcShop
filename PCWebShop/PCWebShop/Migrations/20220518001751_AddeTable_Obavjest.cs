using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCWebShop.Migrations
{
    public partial class AddeTable_Obavjest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Obavjest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DateRead = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TipObavjesti = table.Column<int>(type: "int", nullable: false),
                    SendOnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Seen = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavjest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Obavjest_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Obavjest_KorisnikId",
                table: "Obavjest",
                column: "KorisnikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obavjest");
        }
    }
}
