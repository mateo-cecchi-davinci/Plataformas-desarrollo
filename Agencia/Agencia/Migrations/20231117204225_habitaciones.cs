using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agencia.Migrations
{
    public partial class habitaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "hotel_fk",
                table: "reservaHotel");

            migrationBuilder.DropTable(
                name: "usuarioHotel");

            migrationBuilder.DropColumn(
                name: "cant_hab_chicas",
                table: "reservaHotel");

            migrationBuilder.DropColumn(
                name: "cant_hab_grandes",
                table: "reservaHotel");

            migrationBuilder.DropColumn(
                name: "costo_hab_chicas",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "costo_hab_grandes",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "costo_hab_medianas",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "habitaciones_chicas",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "habitaciones_grandes",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "habitaciones_medianas",
                table: "hotel");

            migrationBuilder.RenameColumn(
                name: "hotel_fk",
                table: "reservaHotel",
                newName: "habitacion_fk");

            migrationBuilder.RenameColumn(
                name: "cant_hab_medianas",
                table: "reservaHotel",
                newName: "cantPersonas");

            migrationBuilder.RenameIndex(
                name: "IX_reservaHotel_hotel_fk",
                table: "reservaHotel",
                newName: "IX_reservaHotel_habitacion_fk");

            migrationBuilder.CreateTable(
                name: "Habitacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    capacidad = table.Column<int>(type: "int", nullable: false),
                    costo = table.Column<double>(type: "float", nullable: false),
                    hotel_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacion", x => x.id);
                    table.ForeignKey(
                        name: "hotel_fk",
                        column: x => x.hotel_fk,
                        principalTable: "hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarioHabitacion",
                columns: table => new
                {
                    usuarios_fk = table.Column<int>(type: "int", nullable: false),
                    habitaciones_fk = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarioHabitacion", x => new { x.usuarios_fk, x.habitaciones_fk });
                    table.ForeignKey(
                        name: "FK_usuarioHabitacion_Habitacion_habitaciones_fk",
                        column: x => x.habitaciones_fk,
                        principalTable: "Habitacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarioHabitacion_usuario_usuarios_fk",
                        column: x => x.usuarios_fk,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Habitacion",
                columns: new[] { "id", "capacidad", "costo", "hotel_fk" },
                values: new object[,]
                {
                    { 1, 2, 20000.0, 1 },
                    { 2, 2, 20000.0, 1 },
                    { 3, 4, 35000.0, 1 },
                    { 4, 4, 35000.0, 1 },
                    { 5, 8, 70000.0, 1 },
                    { 6, 8, 70000.0, 1 },
                    { 7, 2, 20000.0, 2 },
                    { 8, 2, 20000.0, 2 },
                    { 9, 4, 35000.0, 2 },
                    { 10, 4, 35000.0, 2 },
                    { 11, 8, 70000.0, 2 },
                    { 12, 8, 70000.0, 2 },
                    { 13, 2, 20000.0, 3 },
                    { 14, 2, 20000.0, 3 },
                    { 15, 4, 35000.0, 3 },
                    { 16, 4, 35000.0, 3 },
                    { 17, 8, 70000.0, 3 },
                    { 18, 8, 70000.0, 3 },
                    { 19, 2, 20000.0, 4 },
                    { 20, 2, 20000.0, 4 },
                    { 21, 4, 35000.0, 4 },
                    { 22, 4, 35000.0, 4 },
                    { 23, 8, 70000.0, 4 },
                    { 24, 8, 70000.0, 4 },
                    { 25, 2, 20000.0, 5 },
                    { 26, 2, 20000.0, 5 },
                    { 27, 4, 35000.0, 5 },
                    { 28, 4, 35000.0, 5 },
                    { 29, 8, 70000.0, 5 },
                    { 30, 8, 70000.0, 5 },
                    { 31, 2, 20000.0, 6 },
                    { 32, 2, 20000.0, 6 },
                    { 33, 4, 35000.0, 6 },
                    { 34, 4, 35000.0, 6 },
                    { 35, 8, 70000.0, 6 },
                    { 36, 8, 70000.0, 6 },
                    { 37, 2, 20000.0, 7 },
                    { 38, 2, 20000.0, 7 },
                    { 39, 4, 35000.0, 7 },
                    { 40, 4, 35000.0, 7 },
                    { 41, 8, 70000.0, 7 },
                    { 42, 8, 70000.0, 7 }
                });

            migrationBuilder.InsertData(
                table: "Habitacion",
                columns: new[] { "id", "capacidad", "costo", "hotel_fk" },
                values: new object[,]
                {
                    { 43, 2, 20000.0, 8 },
                    { 44, 2, 20000.0, 8 },
                    { 45, 4, 35000.0, 8 },
                    { 46, 4, 35000.0, 8 },
                    { 47, 8, 70000.0, 8 },
                    { 48, 8, 70000.0, 8 },
                    { 49, 2, 20000.0, 9 },
                    { 50, 2, 20000.0, 9 },
                    { 51, 4, 35000.0, 9 },
                    { 52, 4, 35000.0, 9 },
                    { 53, 8, 70000.0, 9 },
                    { 54, 8, 70000.0, 9 },
                    { 55, 2, 20000.0, 10 },
                    { 56, 2, 20000.0, 10 },
                    { 57, 4, 35000.0, 10 },
                    { 58, 4, 35000.0, 10 },
                    { 59, 8, 70000.0, 10 },
                    { 60, 8, 70000.0, 10 },
                    { 61, 2, 20000.0, 11 },
                    { 62, 2, 20000.0, 11 },
                    { 63, 4, 35000.0, 11 },
                    { 64, 4, 35000.0, 11 },
                    { 65, 8, 70000.0, 11 },
                    { 66, 8, 70000.0, 11 },
                    { 67, 2, 20000.0, 12 },
                    { 68, 2, 20000.0, 12 },
                    { 69, 4, 35000.0, 12 },
                    { 70, 4, 35000.0, 12 },
                    { 71, 8, 70000.0, 12 },
                    { 72, 8, 70000.0, 12 },
                    { 73, 2, 20000.0, 13 },
                    { 74, 2, 20000.0, 13 },
                    { 75, 4, 35000.0, 13 },
                    { 76, 4, 35000.0, 13 },
                    { 77, 8, 70000.0, 13 },
                    { 78, 8, 70000.0, 13 },
                    { 79, 2, 20000.0, 14 },
                    { 80, 2, 20000.0, 14 },
                    { 81, 4, 35000.0, 14 },
                    { 82, 4, 35000.0, 14 },
                    { 83, 8, 70000.0, 14 },
                    { 84, 8, 70000.0, 14 }
                });

            migrationBuilder.InsertData(
                table: "Habitacion",
                columns: new[] { "id", "capacidad", "costo", "hotel_fk" },
                values: new object[,]
                {
                    { 85, 2, 20000.0, 15 },
                    { 86, 2, 20000.0, 15 },
                    { 87, 4, 35000.0, 15 },
                    { 88, 4, 35000.0, 15 },
                    { 89, 8, 70000.0, 15 },
                    { 90, 8, 70000.0, 15 },
                    { 91, 2, 20000.0, 16 },
                    { 92, 2, 20000.0, 16 },
                    { 93, 4, 35000.0, 16 },
                    { 94, 4, 35000.0, 16 },
                    { 95, 8, 70000.0, 16 },
                    { 96, 8, 70000.0, 16 },
                    { 97, 2, 20000.0, 17 },
                    { 98, 2, 20000.0, 17 },
                    { 99, 4, 35000.0, 17 },
                    { 100, 4, 35000.0, 17 },
                    { 101, 8, 70000.0, 17 },
                    { 102, 8, 70000.0, 17 },
                    { 103, 2, 20000.0, 18 },
                    { 104, 2, 20000.0, 18 },
                    { 105, 4, 35000.0, 18 },
                    { 106, 4, 35000.0, 18 },
                    { 107, 8, 70000.0, 18 },
                    { 108, 8, 70000.0, 18 },
                    { 109, 2, 20000.0, 19 },
                    { 110, 2, 20000.0, 19 },
                    { 111, 4, 35000.0, 19 },
                    { 112, 4, 35000.0, 19 },
                    { 113, 8, 70000.0, 19 },
                    { 114, 8, 70000.0, 19 },
                    { 115, 2, 20000.0, 20 },
                    { 116, 2, 20000.0, 20 },
                    { 117, 4, 35000.0, 20 },
                    { 118, 4, 35000.0, 20 },
                    { 119, 8, 70000.0, 20 },
                    { 120, 8, 70000.0, 20 },
                    { 121, 2, 20000.0, 2021 },
                    { 122, 2, 20000.0, 2021 },
                    { 123, 4, 35000.0, 2021 },
                    { 124, 4, 35000.0, 2021 },
                    { 125, 8, 70000.0, 2021 },
                    { 126, 8, 70000.0, 2021 }
                });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 1,
                column: "cantPersonas",
                value: 2);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 6,
                column: "cantPersonas",
                value: 2);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 1002,
                column: "cantPersonas",
                value: 2);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 7 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 13 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 19 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 25 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 7 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 13 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 19 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 25 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2007,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 7 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2008,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 25 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2009,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 31 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2010,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 37 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2011,
                columns: new[] { "cantPersonas", "habitacion_fk" },
                values: new object[] { 2, 25 });

            migrationBuilder.InsertData(
                table: "usuarioHabitacion",
                columns: new[] { "habitaciones_fk", "usuarios_fk", "cantidad" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 7, 2, 0 },
                    { 1, 3, 1 },
                    { 13, 3, 1 },
                    { 19, 4, 1 },
                    { 25, 5, 0 },
                    { 1, 6, 1 },
                    { 7, 7, 0 },
                    { 13, 8, 1 },
                    { 19, 9, 0 },
                    { 25, 10, 0 },
                    { 1, 18, 2 },
                    { 1, 1024, 1 },
                    { 7, 1024, 0 },
                    { 25, 1024, 2 },
                    { 31, 1024, 0 },
                    { 37, 1024, 1 },
                    { 121, 1024, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habitacion_hotel_fk",
                table: "Habitacion",
                column: "hotel_fk");

            migrationBuilder.CreateIndex(
                name: "IX_usuarioHabitacion_habitaciones_fk",
                table: "usuarioHabitacion",
                column: "habitaciones_fk");

            migrationBuilder.AddForeignKey(
                name: "habitacion_fk",
                table: "reservaHotel",
                column: "habitacion_fk",
                principalTable: "Habitacion",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "habitacion_fk",
                table: "reservaHotel");

            migrationBuilder.DropTable(
                name: "usuarioHabitacion");

            migrationBuilder.DropTable(
                name: "Habitacion");

            migrationBuilder.RenameColumn(
                name: "habitacion_fk",
                table: "reservaHotel",
                newName: "hotel_fk");

            migrationBuilder.RenameColumn(
                name: "cantPersonas",
                table: "reservaHotel",
                newName: "cant_hab_medianas");

            migrationBuilder.RenameIndex(
                name: "IX_reservaHotel_habitacion_fk",
                table: "reservaHotel",
                newName: "IX_reservaHotel_hotel_fk");

            migrationBuilder.AddColumn<int>(
                name: "cant_hab_chicas",
                table: "reservaHotel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cant_hab_grandes",
                table: "reservaHotel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "costo_hab_chicas",
                table: "hotel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "costo_hab_grandes",
                table: "hotel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "costo_hab_medianas",
                table: "hotel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "habitaciones_chicas",
                table: "hotel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "habitaciones_grandes",
                table: "hotel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "habitaciones_medianas",
                table: "hotel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "usuarioHotel",
                columns: table => new
                {
                    usuario_fk = table.Column<int>(type: "int", nullable: false),
                    hotel_fk = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarioHotel", x => new { x.usuario_fk, x.hotel_fk });
                    table.ForeignKey(
                        name: "FK_usuarioHotel_hotel_hotel_fk",
                        column: x => x.hotel_fk,
                        principalTable: "hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarioHotel_usuario_usuario_fk",
                        column: x => x.usuario_fk,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 2021,
                columns: new[] { "costo_hab_chicas", "costo_hab_grandes", "costo_hab_medianas", "habitaciones_chicas", "habitaciones_grandes", "habitaciones_medianas" },
                values: new object[] { 20000.0, 70000.0, 35000.0, 50, 10, 20 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 2 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 3 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 4 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 5 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 2 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 3 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 4 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 5 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 1002,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2007,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 2 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2008,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 5 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2009,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 6 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2010,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 7 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2011,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas", "hotel_fk" },
                values: new object[] { 1, 0, 5 });

            migrationBuilder.InsertData(
                table: "usuarioHotel",
                columns: new[] { "hotel_fk", "usuario_fk", "cantidad" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 0 },
                    { 1, 3, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "usuarioHotel",
                columns: new[] { "hotel_fk", "usuario_fk", "cantidad" },
                values: new object[,]
                {
                    { 5, 5, 0 },
                    { 1, 6, 1 },
                    { 2, 7, 0 },
                    { 3, 8, 1 },
                    { 4, 9, 0 },
                    { 5, 10, 0 },
                    { 1, 18, 2 },
                    { 1, 1024, 1 },
                    { 2, 1024, 0 },
                    { 5, 1024, 2 },
                    { 6, 1024, 0 },
                    { 7, 1024, 1 },
                    { 2021, 1024, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_usuarioHotel_hotel_fk",
                table: "usuarioHotel",
                column: "hotel_fk");

            migrationBuilder.AddForeignKey(
                name: "hotel_fk",
                table: "reservaHotel",
                column: "hotel_fk",
                principalTable: "hotel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
