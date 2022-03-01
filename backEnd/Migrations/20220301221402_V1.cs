using Microsoft.EntityFrameworkCore.Migrations;

namespace backEnd.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Promocije = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meni", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PivaHrana",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cena = table.Column<float>(type: "real", nullable: false),
                    PiceIliHrana = table.Column<bool>(type: "bit", nullable: false),
                    MeniID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PivaHrana", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PivaHrana_Meni_MeniID",
                        column: x => x.MeniID,
                        principalTable: "Meni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pivnice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrojStolova = table.Column<int>(type: "int", nullable: false),
                    MeniID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pivnice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pivnice_Meni_MeniID",
                        column: x => x.MeniID,
                        principalTable: "Meni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stolovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojStola = table.Column<int>(type: "int", nullable: false),
                    PivnicaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stolovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stolovi_Pivnice_PivnicaID",
                        column: x => x.PivnicaID,
                        principalTable: "Pivnice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Narudzbine",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KolicinaPiva = table.Column<int>(type: "int", nullable: false),
                    KolicinaHrane = table.Column<int>(type: "int", nullable: false),
                    StoID = table.Column<int>(type: "int", nullable: true),
                    PivoHranaID = table.Column<int>(type: "int", nullable: true),
                    Doneta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzbine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Narudzbine_PivaHrana_PivoHranaID",
                        column: x => x.PivoHranaID,
                        principalTable: "PivaHrana",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Narudzbine_Stolovi_StoID",
                        column: x => x.StoID,
                        principalTable: "Stolovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbine_PivoHranaID",
                table: "Narudzbine",
                column: "PivoHranaID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbine_StoID",
                table: "Narudzbine",
                column: "StoID");

            migrationBuilder.CreateIndex(
                name: "IX_PivaHrana_MeniID",
                table: "PivaHrana",
                column: "MeniID");

            migrationBuilder.CreateIndex(
                name: "IX_Pivnice_MeniID",
                table: "Pivnice",
                column: "MeniID");

            migrationBuilder.CreateIndex(
                name: "IX_Stolovi_PivnicaID",
                table: "Stolovi",
                column: "PivnicaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Narudzbine");

            migrationBuilder.DropTable(
                name: "PivaHrana");

            migrationBuilder.DropTable(
                name: "Stolovi");

            migrationBuilder.DropTable(
                name: "Pivnice");

            migrationBuilder.DropTable(
                name: "Meni");
        }
    }
}
