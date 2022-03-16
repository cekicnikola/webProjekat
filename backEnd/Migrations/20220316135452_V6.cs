using Microsoft.EntityFrameworkCore.Migrations;

namespace backEnd.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Racun",
                table: "Narudzbine",
                newName: "ZaNaplatu");

            migrationBuilder.AddColumn<float>(
                name: "Racun",
                table: "Stolovi",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Racun",
                table: "Stolovi");

            migrationBuilder.RenameColumn(
                name: "ZaNaplatu",
                table: "Narudzbine",
                newName: "Racun");
        }
    }
}
