using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agencia.Migrations
{
    public partial class descripcion_hotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "hotel",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 1,
                column: "descripcion",
                value: "San Miguel de Tucumán, Centro histórico. A 287 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 2,
                column: "descripcion",
                value: "Córdoba, Barrio ItuzaingóA 445 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 3,
                column: "descripcion",
                value: "Rosario, Centro. A 966 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 4,
                column: "descripcion",
                value: "Mendoza, Centro. A 390 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 5,
                column: "descripcion",
                value: "Buenos Aires, La Plata. A 2,5 km del Museo de la Plata");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 6,
                column: "descripcion",
                value: "Mar del Plata. A 9,13 km del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 7,
                column: "descripcion",
                value: " San Miguel de Tucumán, Argentina. A 3,39 km del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 8,
                column: "descripcion",
                value: "Salta, Centro. A 226 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 9,
                column: "descripcion",
                value: "Santa Fe, Argentina. A 1,93 km del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 10,
                column: "descripcion",
                value: "San Juan. A 205 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 11,
                column: "descripcion",
                value: "Buenos Aires, Recoleta. A 1,10 km del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 12,
                column: "descripcion",
                value: "Córdoba, Argentina. A 31,70 km del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 13,
                column: "descripcion",
                value: "Rosario, Argentina. A 2,38 km del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 14,
                column: "descripcion",
                value: "Mendoza, Argentina. A 346 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 15,
                column: "descripcion",
                value: "Buenos Aires, La Plata. A 5,35 km del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 16,
                column: "descripcion",
                value: "Buenos Aires, Argentina. A 297 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 17,
                column: "descripcion",
                value: "San Miguel de Tucumán, Argentina. A 1,81 km del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 18,
                column: "descripcion",
                value: "Salta, Centro. A 704 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 19,
                column: "descripcion",
                value: "Santa Fe. A 2,37 km del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 20,
                column: "descripcion",
                value: "San Juan, Argentina. A 43 m del centro");

            migrationBuilder.UpdateData(
                table: "hotel",
                keyColumn: "id",
                keyValue: 2021,
                column: "descripcion",
                value: "San Miguel de Tucumán, Barrio Norte. A 1,34 km del centro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "hotel");
        }
    }
}
