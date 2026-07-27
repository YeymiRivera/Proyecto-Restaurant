using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace proyecto.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categoria_pkey", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "menu_diario",
                columns: table => new
                {
                    id_menu_diario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    menu_dia = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("menu_diario_pkey", x => x.id_menu_diario);
                });

            migrationBuilder.CreateTable(
                name: "mesa",
                columns: table => new
                {
                    id_mesa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numero_mesa = table.Column<int>(type: "integer", nullable: false),
                    capacidad = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'Disponible'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("mesa_pkey", x => x.id_mesa);
                });

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rol_pkey", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    precio = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    id_categoria = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("producto_pkey", x => x.id_producto);
                    table.ForeignKey(
                        name: "fk_producto_categoria",
                        column: x => x.id_categoria,
                        principalTable: "categoria",
                        principalColumn: "id_categoria");
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    id_rol = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuario_pkey", x => x.id_usuario);
                    table.ForeignKey(
                        name: "fk_rol_usuario",
                        column: x => x.id_rol,
                        principalTable: "rol",
                        principalColumn: "id_rol");
                });

            migrationBuilder.CreateTable(
                name: "detalle_menu_diario",
                columns: table => new
                {
                    id_detalle_menu = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_menu_diario = table.Column<int>(type: "integer", nullable: false),
                    id_producto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalle_menu_diario_pkey", x => x.id_detalle_menu);
                    table.ForeignKey(
                        name: "fk_detalle_menu",
                        column: x => x.id_menu_diario,
                        principalTable: "menu_diario",
                        principalColumn: "id_menu_diario");
                    table.ForeignKey(
                        name: "fk_detalle_menu_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto");
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    id_pedido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    tipo_pedido = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'Pendiente'::character varying"),
                    total = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    id_mesa = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pedido_pkey", x => x.id_pedido);
                    table.ForeignKey(
                        name: "fk_pedido_mesa",
                        column: x => x.id_mesa,
                        principalTable: "mesa",
                        principalColumn: "id_mesa");
                    table.ForeignKey(
                        name: "fk_pedido_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "reserva",
                columns: table => new
                {
                    id_reserva = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    hora = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    cantidad_personas = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'Pendiente'::character varying"),
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reserva_pkey", x => x.id_reserva);
                    table.ForeignKey(
                        name: "fk_reserva_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "delivery",
                columns: table => new
                {
                    id_delivery = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    direccion_entrega = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    referencia = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    costo_envio = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    estado_entrega = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'Pendiente'::character varying"),
                    hora_salida = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    hora_entrega = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_pedido = table.Column<int>(type: "integer", nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("delivery_pkey", x => x.id_delivery);
                    table.ForeignKey(
                        name: "fk_delivery_pedido",
                        column: x => x.id_pedido,
                        principalTable: "pedido",
                        principalColumn: "id_pedido");
                    table.ForeignKey(
                        name: "fk_delivery_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "detalle_pedido",
                columns: table => new
                {
                    id_detalle_pedido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    id_pedido = table.Column<int>(type: "integer", nullable: false),
                    id_producto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalle_pedido_pkey", x => x.id_detalle_pedido);
                    table.ForeignKey(
                        name: "fk_detalle_pedido",
                        column: x => x.id_pedido,
                        principalTable: "pedido",
                        principalColumn: "id_pedido");
                    table.ForeignKey(
                        name: "fk_detalle_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto");
                });

            migrationBuilder.CreateTable(
                name: "detalle_reserva",
                columns: table => new
                {
                    id_detalle_reserva = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_reserva = table.Column<int>(type: "integer", nullable: false),
                    id_mesa = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalle_reserva_pkey", x => x.id_detalle_reserva);
                    table.ForeignKey(
                        name: "fk_detalle_reserva",
                        column: x => x.id_reserva,
                        principalTable: "reserva",
                        principalColumn: "id_reserva");
                    table.ForeignKey(
                        name: "fk_detalle_reserva_mesa",
                        column: x => x.id_mesa,
                        principalTable: "mesa",
                        principalColumn: "id_mesa");
                });

            migrationBuilder.CreateIndex(
                name: "categoria_nombre_key",
                table: "categoria",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "delivery_id_pedido_key",
                table: "delivery",
                column: "id_pedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_delivery_id_usuario",
                table: "delivery",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_menu_diario_id_producto",
                table: "detalle_menu_diario",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "uq_menu_producto",
                table: "detalle_menu_diario",
                columns: new[] { "id_menu_diario", "id_producto" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_detalle_pedido_id_producto",
                table: "detalle_pedido",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "uq_pedido_producto",
                table: "detalle_pedido",
                columns: new[] { "id_pedido", "id_producto" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_detalle_reserva_id_mesa",
                table: "detalle_reserva",
                column: "id_mesa");

            migrationBuilder.CreateIndex(
                name: "uq_reserva_mesa",
                table: "detalle_reserva",
                columns: new[] { "id_reserva", "id_mesa" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "menu_diario_menu_dia_key",
                table: "menu_diario",
                column: "menu_dia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "mesa_numero_mesa_key",
                table: "mesa",
                column: "numero_mesa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pedido_id_mesa",
                table: "pedido",
                column: "id_mesa");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_id_usuario",
                table: "pedido",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "idx_producto_categoria",
                table: "producto",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_reserva_id_usuario",
                table: "reserva",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "rol_nombre_key",
                table: "rol",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_id_rol",
                table: "usuario",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "usuario_email_key",
                table: "usuario",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "delivery");

            migrationBuilder.DropTable(
                name: "detalle_menu_diario");

            migrationBuilder.DropTable(
                name: "detalle_pedido");

            migrationBuilder.DropTable(
                name: "detalle_reserva");

            migrationBuilder.DropTable(
                name: "menu_diario");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "producto");

            migrationBuilder.DropTable(
                name: "reserva");

            migrationBuilder.DropTable(
                name: "mesa");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "rol");
        }
    }
}
