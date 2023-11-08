using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agencia.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ciudad",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ciudad", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dni = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    apellido = table.Column<string>(type: "varchar(50)", nullable: false),
                    mail = table.Column<string>(type: "varchar(255)", nullable: false),
                    clave = table.Column<string>(type: "varchar(50)", nullable: false),
                    intentosFallidos = table.Column<int>(type: "int", nullable: false),
                    bloqueado = table.Column<bool>(type: "bit", nullable: false),
                    credito = table.Column<double>(type: "float", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hotel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    capacidad = table.Column<int>(type: "int", nullable: false),
                    costo = table.Column<double>(type: "float", nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    ciudad_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel", x => x.id);
                    table.ForeignKey(
                        name: "ciudad_fk",
                        column: x => x.ciudad_fk,
                        principalTable: "ciudad",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vuelo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    capacidad = table.Column<int>(type: "int", nullable: false),
                    vendido = table.Column<int>(type: "int", nullable: false),
                    costo = table.Column<double>(type: "float", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    aerolinea = table.Column<string>(type: "varchar(50)", nullable: false),
                    avion = table.Column<string>(type: "varchar(50)", nullable: false),
                    origen_fk = table.Column<int>(type: "int", nullable: false),
                    destino_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vuelo", x => x.id);
                    table.ForeignKey(
                        name: "destino_fk",
                        column: x => x.destino_fk,
                        principalTable: "ciudad",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "origen_fk",
                        column: x => x.origen_fk,
                        principalTable: "ciudad",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reservaHotel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaDesde = table.Column<DateTime>(type: "datetime", nullable: false),
                    fechaHasta = table.Column<DateTime>(type: "datetime", nullable: false),
                    pagado = table.Column<double>(type: "float", nullable: false),
                    cantPersonas = table.Column<int>(type: "int", nullable: false),
                    hotel_fk = table.Column<int>(type: "int", nullable: false),
                    usuarioRH_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservaHotel", x => x.id);
                    table.ForeignKey(
                        name: "hotel_fk",
                        column: x => x.hotel_fk,
                        principalTable: "hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "usuarioRH_fk",
                        column: x => x.usuarioRH_fk,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "reservaVuelo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pagado = table.Column<double>(type: "float", nullable: false),
                    cantPersonas = table.Column<int>(type: "int", nullable: false),
                    vuelo_fk = table.Column<int>(type: "int", nullable: false),
                    usuarioRV_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservaVuelo", x => x.id);
                    table.ForeignKey(
                        name: "usuarioRV_fk",
                        column: x => x.usuarioRV_fk,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "vuelo_fk",
                        column: x => x.vuelo_fk,
                        principalTable: "vuelo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarioVuelo",
                columns: table => new
                {
                    usuario_fk = table.Column<int>(type: "int", nullable: false),
                    vuelo_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarioVuelo", x => new { x.usuario_fk, x.vuelo_fk });
                    table.ForeignKey(
                        name: "FK_usuarioVuelo_usuario_usuario_fk",
                        column: x => x.usuario_fk,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarioVuelo_vuelo_vuelo_fk",
                        column: x => x.vuelo_fk,
                        principalTable: "vuelo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ciudad",
                columns: new[] { "id", "nombre" },
                values: new object[,]
                {
                    { 1, "Buenos Aires" },
                    { 2, "Córdoba" },
                    { 3, "Rosario" },
                    { 4, "Mendoza" },
                    { 5, "La Plata" },
                    { 6, "Mar del Plata" },
                    { 7, "San Miguel de Tucumán" },
                    { 8, "Salta" },
                    { 9, "Santa Fe" },
                    { 10, "San Juan" },
                    { 11, "Resistencia" },
                    { 12, "Corrientes" },
                    { 13, "Posadas" },
                    { 14, "Neuquén" },
                    { 15, "Formosa" },
                    { 16, "Bahía Blanca" },
                    { 17, "San Salvador de Jujuy" },
                    { 18, "Río Cuarto" },
                    { 19, "Comodoro Rivadavia" },
                    { 20, "Concordia" }
                });

            migrationBuilder.InsertData(
                table: "usuario",
                columns: new[] { "id", "apellido", "bloqueado", "clave", "credito", "dni", "intentosFallidos", "isAdmin", "mail", "nombre" },
                values: new object[,]
                {
                    { 1, "Pérez", false, "123", 1000000.0, 12345678, 0, true, "juan@gmail.com", "Juan" },
                    { 2, "Gómez", false, "123", 800000.0, 98765432, 0, true, "maria@gmail.com", "María" },
                    { 3, "López", false, "123", 500000.0, 45678901, 0, false, "carlos@gmail.com", "Carlos" },
                    { 4, "Martínez", false, "123", 1200000.0, 78436532, 0, false, "ana@gmail.com", "Ana" },
                    { 5, "Rodríguez", false, "123", 1200000.0, 12344356, 0, false, "laura@gmail.com", "Laura" },
                    { 6, "Sánchez", false, "123", 1200000.0, 41234324, 0, false, "eduardo@gmail.com", "Eduardo" },
                    { 7, "Fernández", false, "123", 1200000.0, 76474567, 0, true, "lucia@gmail.com", "Lucía" },
                    { 8, "García", false, "123", 1200000.0, 98667667, 0, false, "javier@gmail.com", "Javier" },
                    { 9, "Hernández", false, "123", 1200000.0, 23455433, 0, false, "sofia@gmail.com", "Sofía" },
                    { 10, "Pérez", false, "123", 1200000.0, 74545345, 0, false, "carlos2@gmail.com", "Carlos" },
                    { 11, "López", false, "123", 1200000.0, 12313455, 0, false, "mariana@gmail.com", "Mariana" },
                    { 12, "Martínez", false, "123", 1200000.0, 53467454, 0, true, "andres@gmail.com", "Andrés" },
                    { 13, "Gómez", false, "123", 1200000.0, 23467636, 0, false, "valentina@gmail.com", "Valentina" },
                    { 14, "Santos", false, "123", 1200000.0, 43765457, 0, false, "diego@gmail.com", "Diego" },
                    { 15, "Torres", false, "123", 1200000.0, 43254756, 0, false, "marcela@gmail.com", "Marcela" },
                    { 16, "Díaz", false, "123", 1200000.0, 78787878, 0, true, "alejandro@gmail.com", "Alejandro" },
                    { 17, "Ramírez", false, "123", 1200000.0, 12344321, 0, false, "luis@gmail.com", "Luis" },
                    { 18, "Soto", false, "123", 1200000.0, 98776655, 0, false, "ana2@gmail.com", "Ana" },
                    { 19, "Silva", false, "123", 1200000.0, 11223344, 0, false, "juan2@gmail.com", "Juan" },
                    { 1023, "prueba", false, "123", 1000000.0, 12348765, 0, false, "prueba@gmail.com", "prueba" },
                    { 1024, "PRUEBA", false, "123", 10000000.0, 123345, 0, false, "PRUEBA", "HOLA" }
                });

            migrationBuilder.InsertData(
                table: "hotel",
                columns: new[] { "id", "capacidad", "ciudad_fk", "costo", "nombre" },
                values: new object[,]
                {
                    { 1, 101, 7, 21000.0, "Hotel Buenos Aires" },
                    { 2, 80, 2, 22000.0, "Hotel Rosario" },
                    { 3, 120, 3, 27000.0, "Hotel Córdoba" },
                    { 4, 90, 4, 24000.0, "Hotel Mendoza" },
                    { 5, 70, 5, 21000.0, "Hotel San Juan" },
                    { 6, 110, 6, 26000.0, "Hotel Mar del Plata" },
                    { 7, 95, 7, 23500.0, "Hotel Tucumán" },
                    { 8, 75, 8, 22500.0, "Hotel Salta" },
                    { 9, 85, 9, 24500.0, "Hotel Jujuy" },
                    { 10, 105, 10, 25500.0, "Hotel Neuquén" },
                    { 11, 80, 1, 22000.0, "Hotel La Plata" },
                    { 12, 110, 2, 26500.0, "Hotel Santa Fe" },
                    { 13, 90, 3, 23000.0, "Hotel San Luis" },
                    { 14, 70, 4, 21000.0, "Hotel Formosa" },
                    { 15, 100, 5, 25000.0, "Hotel Entre Ríos" },
                    { 16, 120, 6, 22000.0, "Hotel Catamarca" },
                    { 17, 120, 7, 27500.0, "Hotel La Rioja" },
                    { 18, 95, 8, 24000.0, "Hotel Chaco" },
                    { 19, 75, 9, 22500.0, "Hotel Tierra del Fuego" },
                    { 20, 105, 10, 25500.0, "Hotel Santa Cruz" },
                    { 2021, 101, 7, 21000.0, "PRUEBA" }
                });

            migrationBuilder.InsertData(
                table: "vuelo",
                columns: new[] { "id", "aerolinea", "avion", "capacidad", "costo", "destino_fk", "fecha", "origen_fk", "vendido" },
                values: new object[,]
                {
                    { 1, "Aerolínea Buenos Aires", "Boeing 747", 150, 22000.0, 3, new DateTime(2023, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 },
                    { 2, "Aerolínea Córdoba", "Airbus A320", 120, 21000.0, 4, new DateTime(2023, 10, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 0 },
                    { 3, "Aerolínea Mendoza", "Boeing 787 Dreamliner", 180, 24000.0, 5, new DateTime(2023, 10, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 4, "Aerolínea Buenos Aires", "Airbus A320", 160, 22500.0, 1, new DateTime(2023, 10, 4, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, 0 },
                    { 5, "Aerolínea Córdoba", "Boeing 747", 130, 23000.0, 2, new DateTime(2023, 10, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, 0 },
                    { 6, "Aerolínea Córdoba", "Boeing 787 Dreamliner", 170, 23500.0, 3, new DateTime(2023, 10, 6, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 },
                    { 7, "Aerolínea Buenos Aires", "Airbus A320", 140, 22800.0, 4, new DateTime(2023, 10, 7, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 0 },
                    { 8, "Aerolínea Mendoza", "Boeing 747", 110, 21500.0, 5, new DateTime(2023, 10, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 9, "Aerolínea Córdoba", "Boeing 787 Dreamliner", 190, 24500.0, 1, new DateTime(2023, 10, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, 0 },
                    { 10, "Aerolínea Buenos Aires", "Airbus A320", 155, 22700.0, 2, new DateTime(2023, 10, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, 0 },
                    { 11, "Aerolínea Mendoza", "Boeing 747", 170, 23500.0, 3, new DateTime(2023, 10, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 },
                    { 12, "Aerolínea Córdoba", "Boeing 787 Dreamliner", 120, 21000.0, 4, new DateTime(2023, 10, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 0 },
                    { 13, "Aerolínea Buenos Aires", "Airbus A320", 140, 22800.0, 5, new DateTime(2023, 10, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 14, "Aerolínea Mendoza", "Boeing 747", 180, 24000.0, 1, new DateTime(2023, 10, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, 0 },
                    { 15, "Aerolínea Córdoba", "Boeing 787 Dreamliner", 130, 23000.0, 2, new DateTime(2023, 10, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, 0 },
                    { 16, "Aerolínea Buenos Aires", "Airbus A320", 150, 22000.0, 3, new DateTime(2023, 10, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 },
                    { 17, "Aerolínea Mendoza", "Boeing 747", 160, 22500.0, 4, new DateTime(2023, 10, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 0 },
                    { 18, "Aerolínea Córdoba", "Boeing 787 Dreamliner", 190, 24500.0, 5, new DateTime(2023, 10, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 19, "Aerolínea Buenos Aires", "Airbus A320", 155, 22700.0, 1, new DateTime(2023, 10, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, 0 },
                    { 20, "Aerolínea Mendoza", "Boeing 747", 110, 21500.0, 2, new DateTime(2023, 10, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, 0 },
                    { 22, "PRUEBA", "PRUEBA", 110, 21000.0, 19, new DateTime(2023, 10, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "reservaHotel",
                columns: new[] { "id", "cantPersonas", "fechaDesde", "fechaHasta", "hotel_fk", "pagado", "usuarioRH_fk" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 150000.0, 1 },
                    { 2, 1, new DateTime(2023, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 120000.0, 2 },
                    { 3, 1, new DateTime(2023, 10, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, 170000.0, 3 },
                    { 4, 2, new DateTime(2023, 9, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, 140000.0, 4 },
                    { 5, 2, new DateTime(2023, 11, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, 110000.0, 5 },
                    { 6, 2, new DateTime(2023, 10, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 150000.0, 6 },
                    { 7, 1, new DateTime(2023, 12, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 120000.0, 7 },
                    { 8, 1, new DateTime(2023, 9, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 30, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, 170000.0, 8 },
                    { 9, 2, new DateTime(2023, 11, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, 140000.0, 9 },
                    { 10, 2, new DateTime(2023, 12, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, 110000.0, 10 },
                    { 1002, 1, new DateTime(2023, 10, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 24, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 150000.0, 3 },
                    { 2007, 1, new DateTime(2023, 10, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 31, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 120000.0, 1024 },
                    { 2008, 1, new DateTime(2023, 10, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, 110000.0, 1024 },
                    { 2009, 1, new DateTime(2023, 10, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 6, 160000.0, 1024 },
                    { 2010, 1, new DateTime(2023, 10, 23, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 23, 9, 0, 0, 0, DateTimeKind.Unspecified), 7, 135000.0, 1024 },
                    { 2011, 1, new DateTime(2023, 10, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, 110000.0, 1024 }
                });

            migrationBuilder.InsertData(
                table: "reservaVuelo",
                columns: new[] { "id", "cantPersonas", "pagado", "usuarioRV_fk", "vuelo_fk" },
                values: new object[,]
                {
                    { 1, 1, 120000.0, 1, 1 },
                    { 2, 1, 110000.0, 2, 2 },
                    { 3, 1, 140000.0, 3, 3 },
                    { 4, 1, 125000.0, 4, 4 },
                    { 5, 2, 130000.0, 5, 5 },
                    { 6, 2, 120000.0, 6, 1 },
                    { 7, 2, 110000.0, 7, 2 },
                    { 8, 1, 140000.0, 8, 3 },
                    { 9, 2, 125000.0, 9, 4 },
                    { 10, 2, 130000.0, 10, 5 },
                    { 1002, 1, 120000.0, 3, 1 },
                    { 2010, 1, 125000.0, 1024, 4 },
                    { 2011, 1, 130000.0, 1024, 5 }
                });

            migrationBuilder.InsertData(
                table: "usuarioHotel",
                columns: new[] { "hotel_fk", "usuario_fk", "cantidad" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 0 },
                    { 1, 3, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 0 },
                    { 1, 6, 1 },
                    { 2, 7, 0 },
                    { 3, 8, 1 },
                    { 4, 9, 0 },
                    { 5, 10, 0 },
                    { 1, 18, 2 },
                    { 1, 1024, 1 }
                });

            migrationBuilder.InsertData(
                table: "usuarioHotel",
                columns: new[] { "hotel_fk", "usuario_fk", "cantidad" },
                values: new object[,]
                {
                    { 2, 1024, 0 },
                    { 5, 1024, 2 },
                    { 6, 1024, 0 },
                    { 7, 1024, 1 },
                    { 2021, 1024, 9 }
                });

            migrationBuilder.InsertData(
                table: "usuarioVuelo",
                columns: new[] { "usuario_fk", "vuelo_fk" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 1 },
                    { 7, 2 },
                    { 8, 3 },
                    { 9, 4 },
                    { 10, 5 },
                    { 18, 1 },
                    { 1024, 1 },
                    { 1024, 4 },
                    { 1024, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_hotel_ciudad_fk",
                table: "hotel",
                column: "ciudad_fk");

            migrationBuilder.CreateIndex(
                name: "IX_reservaHotel_hotel_fk",
                table: "reservaHotel",
                column: "hotel_fk");

            migrationBuilder.CreateIndex(
                name: "IX_reservaHotel_usuarioRH_fk",
                table: "reservaHotel",
                column: "usuarioRH_fk");

            migrationBuilder.CreateIndex(
                name: "IX_reservaVuelo_usuarioRV_fk",
                table: "reservaVuelo",
                column: "usuarioRV_fk");

            migrationBuilder.CreateIndex(
                name: "IX_reservaVuelo_vuelo_fk",
                table: "reservaVuelo",
                column: "vuelo_fk");

            migrationBuilder.CreateIndex(
                name: "IX_usuarioHotel_hotel_fk",
                table: "usuarioHotel",
                column: "hotel_fk");

            migrationBuilder.CreateIndex(
                name: "IX_usuarioVuelo_vuelo_fk",
                table: "usuarioVuelo",
                column: "vuelo_fk");

            migrationBuilder.CreateIndex(
                name: "IX_vuelo_destino_fk",
                table: "vuelo",
                column: "destino_fk");

            migrationBuilder.CreateIndex(
                name: "IX_vuelo_origen_fk",
                table: "vuelo",
                column: "origen_fk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservaHotel");

            migrationBuilder.DropTable(
                name: "reservaVuelo");

            migrationBuilder.DropTable(
                name: "usuarioHotel");

            migrationBuilder.DropTable(
                name: "usuarioVuelo");

            migrationBuilder.DropTable(
                name: "hotel");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "vuelo");

            migrationBuilder.DropTable(
                name: "ciudad");
        }
    }
}
