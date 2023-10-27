using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCWebShop.Migrations
{
    public partial class AdeedTable_AdministratorObavjest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "trajanjeUgovora",
                table: "Administrator",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "aAdministratorObavjest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdministratorId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_aAdministratorObavjest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_aAdministratorObavjest_Administrator_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Administrator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aAdministratorObavjest_AdministratorId",
                table: "aAdministratorObavjest",
                column: "AdministratorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aAdministratorObavjest");

            migrationBuilder.DropColumn(
                name: "trajanjeUgovora",
                table: "Administrator");
        }
    }
}
