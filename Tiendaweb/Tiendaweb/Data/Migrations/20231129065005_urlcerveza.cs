using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiendaweb.Data.Migrations
{
    public partial class urlcerveza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Urlimagen",
                table: "cervezas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Urlimagen",
                table: "cervezas");
        }
    }
}
