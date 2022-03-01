using Microsoft.EntityFrameworkCore.Migrations;

namespace backEnd.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pivnice_Meni_MeniID",
                table: "Pivnice");

            migrationBuilder.DropIndex(
                name: "IX_Pivnice_MeniID",
                table: "Pivnice");

            migrationBuilder.DropColumn(
                name: "MeniID",
                table: "Pivnice");

            migrationBuilder.AddColumn<int>(
                name: "PivnicaID",
                table: "Meni",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Meni_PivnicaID",
                table: "Meni",
                column: "PivnicaID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Meni_Pivnice_PivnicaID",
                table: "Meni",
                column: "PivnicaID",
                principalTable: "Pivnice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meni_Pivnice_PivnicaID",
                table: "Meni");

            migrationBuilder.DropIndex(
                name: "IX_Meni_PivnicaID",
                table: "Meni");

            migrationBuilder.DropColumn(
                name: "PivnicaID",
                table: "Meni");

            migrationBuilder.AddColumn<int>(
                name: "MeniID",
                table: "Pivnice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pivnice_MeniID",
                table: "Pivnice",
                column: "MeniID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pivnice_Meni_MeniID",
                table: "Pivnice",
                column: "MeniID",
                principalTable: "Meni",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
