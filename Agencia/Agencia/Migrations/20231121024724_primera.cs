using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agencia.Migrations
{
    public partial class primera : Migration
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
                    nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    ciudad_fk = table.Column<int>(type: "int", nullable: false),
                    imagen = table.Column<string>(type: "varchar(255)", nullable: true)
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
                name: "habitacion",
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
                    table.PrimaryKey("PK_habitacion", x => x.id);
                    table.ForeignKey(
                        name: "hotel_fk",
                        column: x => x.hotel_fk,
                        principalTable: "hotel",
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
                    habitacion_fk = table.Column<int>(type: "int", nullable: false),
                    usuarioRH_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservaHotel", x => x.id);
                    table.ForeignKey(
                        name: "habitacion_fk",
                        column: x => x.habitacion_fk,
                        principalTable: "habitacion",
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
                        name: "FK_usuarioHabitacion_habitacion_habitaciones_fk",
                        column: x => x.habitaciones_fk,
                        principalTable: "habitacion",
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
                columns: new[] { "id", "ciudad_fk", "imagen", "nombre" },
                values: new object[,]
                {
                    { 1, 7, "images/hotel/04a270ea-1772-4970-84ea-8511a77d344b_hotelGardenPlaza-Tucuman.png", "Garden Plaza" },
                    { 2, 2, "images/hotel/04314e9f-5c9a-4f8a-8672-94781301b98f_selinaNuevaCordoba-Cordoba.png", "Selina Nueva Cordoba" },
                    { 3, 3, "images/hotel/79fdae4f-dfee-4262-9803-b1094618f216_americanPuertoRosarioHotel-Rosario.png", "American Puerto Rosario Hotel" },
                    { 4, 4, "images/hotel/c9252ae1-4112-4f7e-9ca2-7b87cb681dda_raicesAconcaguaHotel-Mendoza.png", "Raices Aconcagua Hotel" },
                    { 5, 5, "images/hotel/0bf11e5f-3554-42a0-b0e0-6087fd485599_dazzlerByWyndhamLaPlata-La Plata.png", "Dazzler by Wyndham La Plata" },
                    { 6, 6, "images/hotel/5b90c67e-11a4-45f7-aa5f-36101400d3ac_hotelAatracMarDelPlata-Mar del Plata.png", "Hotel Aatrac Mar del Plata" },
                    { 7, 7, "images/hotel/bdbb374f-432c-4147-a5ce-499bbc0a292f_hotelPortalDelNorte-Tucuman.png", "Hotel Portal del Norte" },
                    { 8, 8, "images/hotel/a917a5f2-964c-4bb7-94d1-ec4ba0c2baae_hotelPosadaDelSolSalta-Salta.png", "Hotel Posada del Sol Salta" },
                    { 9, 9, "images/hotel/3bd2a198-62ac-47cd-8158-fd910db6fb15_hotelCastelar-Santa Fe.png", "Hotel Castelar" },
                    { 10, 10, "images/hotel/779f2715-5640-4c7e-af86-dbfc2f2b05a4_alhambraInnHotel-San Juan.png", "Alhambra Inn Hotel" },
                    { 11, 1, "images/hotel/97148535-98f8-41a2-8584-678d3874d480_granHotelBuenosAires-Buenos Aires.png", "Gran Hotel Buenos Aires" },
                    { 12, 2, "images/hotel/00475231-6fa4-4b8b-9ee6-5837912b6f28_howardJohnsonByWyndhamCordoba-Cordoba.png", "Howard Johnson by Wyndham Cordoba" },
                    { 13, 3, "images/hotel/27806441-6c89-466a-9766-3264bb46cdaf_aristonHotel-Rosario.png", "Ariston Hotel" },
                    { 14, 4, "images/hotel/8477278d-e8cc-49fe-8afb-1182f09f77e0_hotelPrincessGold-Mendoza.png", "Hotel Princess Gold" },
                    { 15, 5, "images/hotel/6c8fbd02-09ed-49c2-b4cc-3e2bbb0c17f9_hotelDelSol-La Plata.png", "Hotel del Sol" },
                    { 16, 6, "images/hotel/d76a8e6a-4453-4e94-9cf5-2f09fe59a298_hotelArgentino-Mar del Plata.png", "Hotel Argentino" },
                    { 17, 7, "images/hotel/dd74e84b-2fa4-455f-9064-a20e5cbb8230_hotelLePark-tucuman.png", "Hotel Le Park" },
                    { 18, 8, "images/hotel/6e456053-c5b0-462e-a510-2722dbe875b6_hotelSamka-Salta.png", "Hotel Samka" },
                    { 19, 9, "images/hotel/15c46820-eb41-468f-833e-8a1aae28eade_puertoAmarrasHotel&Suites-Santa Fe.png", "Puerto Amarras Hotel & Suites" },
                    { 20, 10, "images/hotel/7ccd44f5-2b8d-4217-b152-10508cd991e0_hotelAlbertina-San Juan.png", "Hotel Albertina" },
                    { 2021, 7, "images/hotel/9d04ad05-788c-4f24-b594-f7944b62eb62_sheratonTucumanHotel-Tucuman.png", "Sheraton Tucuman Hotel" }
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
                table: "habitacion",
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
                table: "habitacion",
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
                table: "habitacion",
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

            migrationBuilder.InsertData(
                table: "reservaHotel",
                columns: new[] { "id", "cantPersonas", "fechaDesde", "fechaHasta", "habitacion_fk", "pagado", "usuarioRH_fk" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2023, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 150000.0, 1 },
                    { 2, 2, new DateTime(2023, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), 7, 120000.0, 2 },
                    { 3, 2, new DateTime(2023, 10, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), 13, 170000.0, 3 },
                    { 4, 2, new DateTime(2023, 9, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 19, 140000.0, 4 },
                    { 5, 2, new DateTime(2023, 11, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), 25, 110000.0, 5 },
                    { 6, 2, new DateTime(2023, 10, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 150000.0, 6 },
                    { 7, 2, new DateTime(2023, 12, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), 7, 120000.0, 7 },
                    { 8, 2, new DateTime(2023, 9, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 30, 9, 0, 0, 0, DateTimeKind.Unspecified), 13, 170000.0, 8 },
                    { 9, 2, new DateTime(2023, 11, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 19, 140000.0, 9 },
                    { 10, 2, new DateTime(2023, 12, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 25, 110000.0, 10 },
                    { 1002, 2, new DateTime(2023, 10, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 24, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 150000.0, 3 },
                    { 2007, 2, new DateTime(2023, 10, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 31, 9, 0, 0, 0, DateTimeKind.Unspecified), 7, 120000.0, 1024 },
                    { 2008, 2, new DateTime(2023, 10, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 25, 110000.0, 1024 },
                    { 2009, 2, new DateTime(2023, 10, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 31, 160000.0, 1024 },
                    { 2010, 2, new DateTime(2023, 10, 23, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 23, 9, 0, 0, 0, DateTimeKind.Unspecified), 37, 135000.0, 1024 },
                    { 2011, 2, new DateTime(2023, 10, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 25, 110000.0, 1024 }
                });

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
                name: "IX_habitacion_hotel_fk",
                table: "habitacion",
                column: "hotel_fk");

            migrationBuilder.CreateIndex(
                name: "IX_hotel_ciudad_fk",
                table: "hotel",
                column: "ciudad_fk");

            migrationBuilder.CreateIndex(
                name: "IX_reservaHotel_habitacion_fk",
                table: "reservaHotel",
                column: "habitacion_fk");

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
                name: "IX_usuarioHabitacion_habitaciones_fk",
                table: "usuarioHabitacion",
                column: "habitaciones_fk");

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
                name: "usuarioHabitacion");

            migrationBuilder.DropTable(
                name: "usuarioVuelo");

            migrationBuilder.DropTable(
                name: "habitacion");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "vuelo");

            migrationBuilder.DropTable(
                name: "hotel");

            migrationBuilder.DropTable(
                name: "ciudad");
        }
    }
}
