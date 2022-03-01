using Microsoft.EntityFrameworkCore.Migrations;

namespace backEnd.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promocije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinKolicinaPica = table.Column<int>(type: "int", nullable: false),
                    MinKolicinaHrane = table.Column<int>(type: "int", nullable: false),
                    Popust = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NarudzbinaPromocija",
                columns: table => new
                {
                    NarudzbineID = table.Column<int>(type: "int", nullable: false),
                    PromocijeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NarudzbinaPromocija", x => new { x.NarudzbineID, x.PromocijeID });
                    table.ForeignKey(
                        name: "FK_NarudzbinaPromocija_Narudzbine_NarudzbineID",
                        column: x => x.NarudzbineID,
                        principalTable: "Narudzbine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NarudzbinaPromocija_Promocije_PromocijeID",
                        column: x => x.PromocijeID,
                        principalTable: "Promocije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbinaPromocija_PromocijeID",
                table: "NarudzbinaPromocija",
                column: "PromocijeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NarudzbinaPromocija");

            migrationBuilder.DropTable(
                name: "Promocije");
        }
    }
}
