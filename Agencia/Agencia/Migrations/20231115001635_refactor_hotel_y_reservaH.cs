using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agencia.Migrations
{
    public partial class refactor_hotel_y_reservaH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cantPersonas",
                table: "reservaHotel",
                newName: "cant_hab_medianas");

            migrationBuilder.RenameColumn(
                name: "costo",
                table: "hotel",
                newName: "costo_hab_medianas");

            migrationBuilder.RenameColumn(
                name: "capacidad",
                table: "hotel",
                newName: "habitaciones_medianas");

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
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

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
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

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
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2008,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2009,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2010,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2011,
                columns: new[] { "cant_hab_chicas", "cant_hab_medianas" },
                values: new object[] { 1, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "habitaciones_chicas",
                table: "hotel");

            migrationBuilder.DropColumn(
                name: "habitaciones_grandes",
                table: "hotel");

            migrationBuilder.RenameColumn(
                name: "cant_hab_medianas",
                table: "reservaHotel",
                newName: "cantPersonas");

            migrationBuilder.RenameColumn(
                name: "habitaciones_medianas",
                table: "hotel",
                newName: "capacidad");

            migrationBuilder.RenameColumn(
                name: "costo_hab_medianas",
                table: "hotel",
                newName: "costo");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 101, 21000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 80, 22000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 120, 27000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 90, 24000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 70, 21000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 110, 26000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 95, 23500.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 75, 22500.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 85, 24500.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 105, 25500.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 80, 22000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 110, 26500.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 90, 23000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 70, 21000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 100, 25000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 120, 22000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 120, 27500.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 95, 24000.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 75, 22500.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 105, 25500.0 });

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 2021,
                columns: new[] { "capacidad", "costo" },
                values: new object[] { 101, 21000.0 });

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 1,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 3,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 4,
                column: "cantPersonas",
                value: 2);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 5,
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
                keyValue: 7,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 8,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 9,
                column: "cantPersonas",
                value: 2);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 10,
                column: "cantPersonas",
                value: 2);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 1002,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2007,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2008,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2009,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2010,
                column: "cantPersonas",
                value: 1);

            migrationBuilder.UpdateData(
                table: "reservaHotel",
                keyColumn: "id",
                keyValue: 2011,
                column: "cantPersonas",
                value: 1);
        }
    }
}
