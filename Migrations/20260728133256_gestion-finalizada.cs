using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace proyecto.Migrations
{
    /// <inheritdoc />
    public partial class gestionfinalizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_reserva_id_usuario",
                table: "reserva",
                newName: "idx_reserva_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_detalle_reserva_id_mesa",
                table: "detalle_reserva",
                newName: "idx_detalle_reserva_mesa");

            migrationBuilder.AlterColumn<int>(
                name: "id_rol",
                table: "usuario",
                type: "integer",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "producto",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "menu_item",
                columns: table => new
                {
                    id_menu = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ruta = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_item", x => x.id_menu);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemRol",
                columns: table => new
                {
                    IdMenu = table.Column<int>(type: "integer", nullable: false),
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemRol", x => new { x.IdMenu, x.IdRol });
                    table.ForeignKey(
                        name: "FK_MenuItemRol_menu_item_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "menu_item",
                        principalColumn: "id_menu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemRol_rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "rol",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_detalle_reserva_reserva",
                table: "detalle_reserva",
                column: "id_reserva");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemRol_IdRol",
                table: "MenuItemRol",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemRol");

            migrationBuilder.DropTable(
                name: "menu_item");

            migrationBuilder.DropIndex(
                name: "idx_detalle_reserva_reserva",
                table: "detalle_reserva");

            migrationBuilder.DropColumn(
                name: "image",
                table: "producto");

            migrationBuilder.RenameIndex(
                name: "idx_reserva_usuario",
                table: "reserva",
                newName: "IX_reserva_id_usuario");

            migrationBuilder.RenameIndex(
                name: "idx_detalle_reserva_mesa",
                table: "detalle_reserva",
                newName: "IX_detalle_reserva_id_mesa");

            migrationBuilder.AlterColumn<int>(
                name: "id_rol",
                table: "usuario",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldDefaultValue: 1);
        }
    }
}
