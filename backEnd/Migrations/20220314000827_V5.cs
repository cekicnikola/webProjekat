using Microsoft.EntityFrameworkCore.Migrations;

namespace backEnd.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NarudzbinaPromocija");

            migrationBuilder.DropTable(
                name: "Promocije");

            migrationBuilder.RenameColumn(
                name: "Promocije",
                table: "Meni",
                newName: "OpisPromocije");

            migrationBuilder.AddColumn<float>(
                name: "Racun",
                table: "Narudzbine",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "MinKolicinaHrane",
                table: "Meni",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinKolicinaPica",
                table: "Meni",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Popust",
                table: "Meni",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Racun",
                table: "Narudzbine");

            migrationBuilder.DropColumn(
                name: "MinKolicinaHrane",
                table: "Meni");

            migrationBuilder.DropColumn(
                name: "MinKolicinaPica",
                table: "Meni");

            migrationBuilder.DropColumn(
                name: "Popust",
                table: "Meni");

            migrationBuilder.RenameColumn(
                name: "OpisPromocije",
                table: "Meni",
                newName: "Promocije");

            migrationBuilder.CreateTable(
                name: "Promocije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinKolicinaHrane = table.Column<int>(type: "int", nullable: false),
                    MinKolicinaPica = table.Column<int>(type: "int", nullable: false),
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
    }
}
