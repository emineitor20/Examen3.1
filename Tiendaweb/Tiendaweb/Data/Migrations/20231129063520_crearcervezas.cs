using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiendaweb.Data.Migrations
{
    public partial class crearcervezas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cervezas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alcohol = table.Column<double>(type: "float", nullable: false),
                    idEstilo = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cervezas", x => x.id);
                    table.ForeignKey(
                        name: "FK_cervezas_estilo_idEstilo",
                        column: x => x.idEstilo,
                        principalTable: "estilo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cervezas_idEstilo",
                table: "cervezas",
                column: "idEstilo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cervezas");
        }
    }
}
