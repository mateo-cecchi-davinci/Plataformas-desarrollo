using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Agencia.Models;

namespace Agencia.Models
{
    public class Context : DbContext
    {
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Hotel> hoteles { get; set; }
        public DbSet<Habitacion> habitaciones { get; set; }
        public DbSet<Vuelo> vuelos { get; set; }
        public DbSet<Ciudad> ciudades { get; set; }
        public DbSet<ReservaHabitacion> reservasHabitacion { get; set; }
        public DbSet<ReservaVuelo> reservasVuelo { get; set; }
        public DbSet<UsuarioHabitacion> usuarioHabitacion { get; set; }
        public DbSet<UsuarioVuelo> usuarioVuelo { get; set; }

        public Context() { }

        public Context(DbContextOptions<Context> optionsBuilder) : base(optionsBuilder) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Context");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //TABLAS

            modelBuilder.Entity<Usuario>()
                .ToTable("usuario")
                .HasKey(u => u.id);
            modelBuilder.Entity<Hotel>()
                .ToTable("hotel")
                .HasKey(h => h.id);
            modelBuilder.Entity<Habitacion>()
                .ToTable("habitacion")
                .HasKey(h => h.id);
            modelBuilder.Entity<Vuelo>()
                .ToTable("vuelo")
                .HasKey(v => v.id);
            modelBuilder.Entity<Ciudad>()
                .ToTable("ciudad")
                .HasKey(c => c.id);
            modelBuilder.Entity<ReservaHabitacion>()
                .ToTable("reservaHotel")
                .HasKey(rh => rh.id);
            modelBuilder.Entity<ReservaVuelo>()
                .ToTable("reservaVuelo")
                .HasKey(rv => rv.id);

            //RELACIONES

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.misReservasHabitaciones)
                .WithOne(rh => rh.miUsuario)
                .HasForeignKey(rh => rh.usuarioRH_fk)
                .HasConstraintName("usuarioRH_fk")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.misReservasVuelos)
                .WithOne(rv => rv.miUsuario)
                .HasForeignKey(rv => rv.usuarioRV_fk)
                .HasConstraintName("usuarioRV_fk")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
                .HasMany(hab => hab.habitaciones)
                .WithOne(hotel => hotel.hotel)
                .HasForeignKey(hab => hab.hotel_fk)
                .HasConstraintName("hotel_fk")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Habitacion>()
                .HasMany(h => h.misReservas)
                .WithOne(rh => rh.miHabitacion)
                .HasForeignKey(rh => rh.habitacion_fk)
                .HasConstraintName("habitacion_fk")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Habitacion>()
                .HasMany(h => h.usuarios)
                .WithMany(u => u.habitacionesUsadas)
                .UsingEntity<UsuarioHabitacion>(
                    euh => euh.HasOne(uh => uh.usuario).WithMany(u => u.usuario_habitacion).HasForeignKey(uh => uh.usuarios_fk),
                    euh => euh.HasOne(uh => uh.habitacion).WithMany(h => h.habitacion_usuario).HasForeignKey(uh => uh.habitaciones_fk),
                    euh => euh.HasKey(k => new { k.usuarios_fk, k.habitaciones_fk })
                );

            modelBuilder.Entity<Vuelo>()
                .HasMany(v => v.misReservas)
                .WithOne(rv => rv.miVuelo)
                .HasForeignKey(rv => rv.vuelo_fk)
                .HasConstraintName("vuelo_fk")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vuelo>()
                .HasMany(v => v.pasajeros)
                .WithMany(u => u.vuelosTomados)
                .UsingEntity<UsuarioVuelo>(
                    euv => euv.HasOne(uv => uv.usuario).WithMany(u => u.usuario_vuelo).HasForeignKey(uv => uv.usuario_fk),
                    euv => euv.HasOne(uv => uv.vuelo).WithMany(v => v.vuelo_usuario).HasForeignKey(uv => uv.vuelo_fk),
                    euv => euv.HasKey(k => new { k.usuario_fk, k.vuelo_fk })
                );

            modelBuilder.Entity<Ciudad>()
                .HasMany(c => c.hoteles)
                .WithOne(h => h.ubicacion)
                .HasForeignKey(h => h.ciudad_fk)
                .HasConstraintName("ciudad_fk")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vuelo>()
                .HasOne(v => v.origen)
                .WithMany(c => c.vuelos_origen)
                .HasForeignKey(v => v.origen_fk)
                .HasConstraintName("origen_fk")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vuelo>()
                .HasOne(v => v.destino)
                .WithMany(c => c.vuelos_destino)
                .HasForeignKey(v => v.destino_fk)
                .HasConstraintName("destino_fk")
                .OnDelete(DeleteBehavior.NoAction);

            //COLUMNAS

            modelBuilder.Entity<Usuario>(
                u =>
                {
                    u.Property(u => u.dni).HasColumnType("int");
                    u.Property(u => u.nombre).HasColumnType("varchar(50)");
                    u.Property(u => u.nombre).IsRequired(true);
                    u.Property(u => u.apellido).HasColumnType("varchar(50)");
                    u.Property(u => u.apellido).IsRequired(true);
                    u.Property(u => u.mail).HasColumnType("varchar(255)");
                    u.Property(u => u.mail).IsRequired(true);
                    u.Property(u => u.clave).HasColumnType("varchar(50)");
                    u.Property(u => u.clave).IsRequired(true);
                    u.Property(u => u.intentosFallidos).HasColumnType("int");
                    u.Property(u => u.bloqueado).HasColumnType("bit");
                    u.Property(u => u.credito).HasColumnType("float");
                    u.Property(u => u.isAdmin).HasColumnType("bit");
                });

            modelBuilder.Entity<Hotel>(
                h =>
                {
                    h.Property(h => h.nombre).HasColumnType("varchar(50)");
                    h.Property(h => h.nombre).IsRequired(true);
                    h.Property(h => h.imagen).HasColumnType("varchar(255)");
                    h.Property(h => h.descripcion).HasColumnType("varchar(255)");
                });

            modelBuilder.Entity<Habitacion>(
                hab =>
                {
                    hab.Property(hab => hab.capacidad).HasColumnType("int");
                    hab.Property(hab => hab.costo).HasColumnType("float");
                });

            modelBuilder.Entity<Vuelo>(
                v =>
                {
                    v.Property(v => v.capacidad).HasColumnType("int");
                    v.Property(v => v.vendido).HasColumnType("int");
                    v.Property(v => v.costo).HasColumnType("float");
                    v.Property(v => v.fecha).HasColumnType("datetime");
                    v.Property(v => v.fecha).IsRequired(true);
                    v.Property(v => v.aerolinea).HasColumnType("varchar(50)");
                    v.Property(v => v.aerolinea).IsRequired(true);
                    v.Property(v => v.avion).HasColumnType("varchar(50)");
                    v.Property(v => v.avion).IsRequired(true);
                });

            modelBuilder.Entity<Ciudad>(
                c =>
                {
                    c.Property(c => c.nombre).HasColumnType("varchar(50)");
                    c.Property(c => c.nombre).IsRequired(true);
                });

            modelBuilder.Entity<ReservaHabitacion>(
                rh =>
                {
                    rh.Property(rh => rh.fechaDesde).HasColumnType("datetime");
                    rh.Property(rh => rh.fechaDesde).IsRequired(true);
                    rh.Property(rh => rh.fechaHasta).HasColumnType("datetime");
                    rh.Property(rh => rh.fechaHasta).IsRequired(true);
                    rh.Property(rh => rh.pagado).HasColumnType("float");
                    rh.Property(rh => rh.cantPersonas).HasColumnType("int");
                });

            modelBuilder.Entity<ReservaVuelo>(
                rv =>
                {
                    rv.Property(rv => rv.pagado).HasColumnType("float");
                    rv.Property(rv => rv.cantPersonas).HasColumnType("int");
                });

            //REGISTROS

            modelBuilder.Entity<Usuario>().HasData(
                new { id = 1, dni = 12345678, nombre = "Juan", apellido = "Pérez", mail = "juan@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1000000.00, isAdmin = true },
                new { id = 2, dni = 98765432, nombre = "María", apellido = "Gómez", mail = "maria@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 800000.00, isAdmin = true },
                new { id = 3, dni = 45678901, nombre = "Carlos", apellido = "López", mail = "carlos@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 500000.00, isAdmin = false },
                new { id = 4, dni = 78436532, nombre = "Ana", apellido = "Martínez", mail = "ana@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 5, dni = 12344356, nombre = "Laura", apellido = "Rodríguez", mail = "laura@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 6, dni = 41234324, nombre = "Eduardo", apellido = "Sánchez", mail = "eduardo@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 7, dni = 76474567, nombre = "Lucía", apellido = "Fernández", mail = "lucia@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = true },
                new { id = 8, dni = 98667667, nombre = "Javier", apellido = "García", mail = "javier@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 9, dni = 23455433, nombre = "Sofía", apellido = "Hernández", mail = "sofia@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 10, dni = 74545345, nombre = "Carlos", apellido = "Pérez", mail = "carlos2@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 11, dni = 12313455, nombre = "Mariana", apellido = "López", mail = "mariana@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 12, dni = 53467454, nombre = "Andrés", apellido = "Martínez", mail = "andres@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = true },
                new { id = 13, dni = 23467636, nombre = "Valentina", apellido = "Gómez", mail = "valentina@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 14, dni = 43765457, nombre = "Diego", apellido = "Santos", mail = "diego@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 15, dni = 43254756, nombre = "Marcela", apellido = "Torres", mail = "marcela@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 16, dni = 78787878, nombre = "Alejandro", apellido = "Díaz", mail = "alejandro@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = true },
                new { id = 17, dni = 12344321, nombre = "Luis", apellido = "Ramírez", mail = "luis@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 18, dni = 98776655, nombre = "Ana", apellido = "Soto", mail = "ana2@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 19, dni = 11223344, nombre = "Juan", apellido = "Silva", mail = "juan2@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1200000.00, isAdmin = false },
                new { id = 1023, dni = 12348765, nombre = "prueba", apellido = "prueba", mail = "prueba@gmail.com", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 1000000.00, isAdmin = false },
                new { id = 1024, dni = 123345, nombre = "HOLA", apellido = "PRUEBA", mail = "PRUEBA", clave = "123", intentosFallidos = 0, bloqueado = false, credito = 10000000.00, isAdmin = false });

            modelBuilder.Entity<Ciudad>().HasData(
                new { id = 1, nombre = "Buenos Aires" },
                new { id = 2, nombre = "Córdoba" },
                new { id = 3, nombre = "Rosario" },
                new { id = 4, nombre = "Mendoza" },
                new { id = 5, nombre = "La Plata" },
                new { id = 6, nombre = "Mar del Plata" },
                new { id = 7, nombre = "San Miguel de Tucumán" },
                new { id = 8, nombre = "Salta" },
                new { id = 9, nombre = "Santa Fe" },
                new { id = 10, nombre = "San Juan" },
                new { id = 11, nombre = "Resistencia" },
                new { id = 12, nombre = "Corrientes" },
                new { id = 13, nombre = "Posadas" },
                new { id = 14, nombre = "Neuquén" },
                new { id = 15, nombre = "Formosa" },
                new { id = 16, nombre = "Bahía Blanca" },
                new { id = 17, nombre = "San Salvador de Jujuy" },
                new { id = 18, nombre = "Río Cuarto" },
                new { id = 19, nombre = "Comodoro Rivadavia" },
                new { id = 20, nombre = "Concordia" });

            modelBuilder.Entity<Hotel>().HasData(
                new { id = 1, nombre = "Garden Plaza", ciudad_fk = 7, imagen = "images/hotel/04a270ea-1772-4970-84ea-8511a77d344b_hotelGardenPlaza-Tucuman.png", descripcion = "San Miguel de Tucumán, Centro histórico. A 287 m del centro" },
                new { id = 2, nombre = "Selina Nueva Cordoba", ciudad_fk = 2, imagen = "images/hotel/04314e9f-5c9a-4f8a-8672-94781301b98f_selinaNuevaCordoba-Cordoba.png", descripcion = "Córdoba, Barrio ItuzaingóA 445 m del centro" },
                new { id = 3, nombre = "American Puerto Rosario Hotel", ciudad_fk = 3, imagen = "images/hotel/79fdae4f-dfee-4262-9803-b1094618f216_americanPuertoRosarioHotel-Rosario.png", descripcion = "Rosario, Centro. A 966 m del centro" },
                new { id = 4, nombre = "Raices Aconcagua Hotel", ciudad_fk = 4, imagen = "images/hotel/c9252ae1-4112-4f7e-9ca2-7b87cb681dda_raicesAconcaguaHotel-Mendoza.png", descripcion = "Mendoza, Centro. A 390 m del centro" },
                new { id = 5, nombre = "Dazzler by Wyndham La Plata", ciudad_fk = 5, imagen = "images/hotel/0bf11e5f-3554-42a0-b0e0-6087fd485599_dazzlerByWyndhamLaPlata-La Plata.png", descripcion = "Buenos Aires, La Plata. A 2,5 km del Museo de la Plata" },
                new { id = 6, nombre = "Hotel Aatrac Mar del Plata", ciudad_fk = 6, imagen = "images/hotel/5b90c67e-11a4-45f7-aa5f-36101400d3ac_hotelAatracMarDelPlata-Mar del Plata.png", descripcion = "Mar del Plata. A 9,13 km del centro" },
                new { id = 7, nombre = "Hotel Portal del Norte", ciudad_fk = 7, imagen = "images/hotel/bdbb374f-432c-4147-a5ce-499bbc0a292f_hotelPortalDelNorte-Tucuman.png", descripcion = " San Miguel de Tucumán, Argentina. A 3,39 km del centro" },
                new { id = 8, nombre = "Hotel Posada del Sol Salta", ciudad_fk = 8, imagen = "images/hotel/a917a5f2-964c-4bb7-94d1-ec4ba0c2baae_hotelPosadaDelSolSalta-Salta.png", descripcion = "Salta, Centro. A 226 m del centro" },
                new { id = 9, nombre = "Hotel Castelar", ciudad_fk = 9, imagen = "images/hotel/3bd2a198-62ac-47cd-8158-fd910db6fb15_hotelCastelar-Santa Fe.png", descripcion = "Santa Fe, Argentina. A 1,93 km del centro" },
                new { id = 10, nombre = "Alhambra Inn Hotel", ciudad_fk = 10, imagen = "images/hotel/779f2715-5640-4c7e-af86-dbfc2f2b05a4_alhambraInnHotel-San Juan.png", descripcion = "San Juan. A 205 m del centro" },
                new { id = 11, nombre = "Gran Hotel Buenos Aires", ciudad_fk = 1, imagen = "images/hotel/97148535-98f8-41a2-8584-678d3874d480_granHotelBuenosAires-Buenos Aires.png", descripcion = "Buenos Aires, Recoleta. A 1,10 km del centro" },
                new { id = 12, nombre = "Howard Johnson by Wyndham Cordoba", ciudad_fk = 2, imagen = "images/hotel/00475231-6fa4-4b8b-9ee6-5837912b6f28_howardJohnsonByWyndhamCordoba-Cordoba.png", descripcion = "Córdoba, Argentina. A 31,70 km del centro" },
                new { id = 13, nombre = "Ariston Hotel", ciudad_fk = 3, imagen = "images/hotel/27806441-6c89-466a-9766-3264bb46cdaf_aristonHotel-Rosario.png", descripcion = "Rosario, Argentina. A 2,38 km del centro" },
                new { id = 14, nombre = "Hotel Princess Gold", ciudad_fk = 4, imagen = "images/hotel/8477278d-e8cc-49fe-8afb-1182f09f77e0_hotelPrincessGold-Mendoza.png", descripcion = "Mendoza, Argentina. A 346 m del centro" },
                new { id = 15, nombre = "Hotel del Sol", ciudad_fk = 5, imagen = "images/hotel/6c8fbd02-09ed-49c2-b4cc-3e2bbb0c17f9_hotelDelSol-La Plata.png", descripcion = "Buenos Aires, La Plata. A 5,35 km del centro" },
                new { id = 16, nombre = "Hotel Argentino", ciudad_fk = 6, imagen = "images/hotel/d76a8e6a-4453-4e94-9cf5-2f09fe59a298_hotelArgentino-Mar del Plata.png", descripcion = "Buenos Aires, Argentina. A 297 m del centro" },
                new { id = 17, nombre = "Hotel Le Park", ciudad_fk = 7, imagen = "images/hotel/dd74e84b-2fa4-455f-9064-a20e5cbb8230_hotelLePark-tucuman.png", descripcion = "San Miguel de Tucumán, Argentina. A 1,81 km del centro" },
                new { id = 18, nombre = "Hotel Samka", ciudad_fk = 8, imagen = "images/hotel/6e456053-c5b0-462e-a510-2722dbe875b6_hotelSamka-Salta.png", descripcion = "Salta, Centro. A 704 m del centro" },
                new { id = 19, nombre = "Puerto Amarras Hotel & Suites", ciudad_fk = 9, imagen = "images/hotel/15c46820-eb41-468f-833e-8a1aae28eade_puertoAmarrasHotel&Suites-Santa Fe.png", descripcion = "Santa Fe. A 2,37 km del centro" },
                new { id = 20, nombre = "Hotel Albertina", ciudad_fk = 10, imagen = "images/hotel/7ccd44f5-2b8d-4217-b152-10508cd991e0_hotelAlbertina-San Juan.png", descripcion = "San Juan, Argentina. A 43 m del centro" },
                new { id = 2021, nombre = "Sheraton Tucuman Hotel", ciudad_fk = 7, imagen = "images/hotel/9d04ad05-788c-4f24-b594-f7944b62eb62_sheratonTucumanHotel-Tucuman.png", descripcion = "San Miguel de Tucumán, Barrio Norte. A 1,34 km del centro" });

            modelBuilder.Entity<Habitacion>().HasData(
                new { id = 1, capacidad = 2, costo = 20000.00, hotel_fk = 1 },
                new { id = 2, capacidad = 2, costo = 20000.00, hotel_fk = 1 },
                new { id = 3, capacidad = 4, costo = 35000.00, hotel_fk = 1 },
                new { id = 4, capacidad = 4, costo = 35000.00, hotel_fk = 1 },
                new { id = 5, capacidad = 8, costo = 70000.00, hotel_fk = 1 },
                new { id = 6, capacidad = 8, costo = 70000.00, hotel_fk = 1 },
                new { id = 7, capacidad = 2, costo = 20000.00, hotel_fk = 2 },
                new { id = 8, capacidad = 2, costo = 20000.00, hotel_fk = 2 },
                new { id = 9, capacidad = 4, costo = 35000.00, hotel_fk = 2 },
                new { id = 10, capacidad = 4, costo = 35000.00, hotel_fk = 2 },
                new { id = 11, capacidad = 8, costo = 70000.00, hotel_fk = 2 },
                new { id = 12, capacidad = 8, costo = 70000.00, hotel_fk = 2 }, 
                new { id = 13, capacidad = 2, costo = 20000.00, hotel_fk = 3 },
                new { id = 14, capacidad = 2, costo = 20000.00, hotel_fk = 3 },
                new { id = 15, capacidad = 4, costo = 35000.00, hotel_fk = 3 },
                new { id = 16, capacidad = 4, costo = 35000.00, hotel_fk = 3 },
                new { id = 17, capacidad = 8, costo = 70000.00, hotel_fk = 3 },
                new { id = 18, capacidad = 8, costo = 70000.00, hotel_fk = 3 }, 
                new { id = 19, capacidad = 2, costo = 20000.00, hotel_fk = 4 },
                new { id = 20, capacidad = 2, costo = 20000.00, hotel_fk = 4 },
                new { id = 21, capacidad = 4, costo = 35000.00, hotel_fk = 4 },
                new { id = 22, capacidad = 4, costo = 35000.00, hotel_fk = 4 },
                new { id = 23, capacidad = 8, costo = 70000.00, hotel_fk = 4 },
                new { id = 24, capacidad = 8, costo = 70000.00, hotel_fk = 4 }, 
                new { id = 25, capacidad = 2, costo = 20000.00, hotel_fk = 5 },
                new { id = 26, capacidad = 2, costo = 20000.00, hotel_fk = 5 },
                new { id = 27, capacidad = 4, costo = 35000.00, hotel_fk = 5 },
                new { id = 28, capacidad = 4, costo = 35000.00, hotel_fk = 5 },
                new { id = 29, capacidad = 8, costo = 70000.00, hotel_fk = 5 },
                new { id = 30, capacidad = 8, costo = 70000.00, hotel_fk = 5 }, 
                new { id = 31, capacidad = 2, costo = 20000.00, hotel_fk = 6 },
                new { id = 32, capacidad = 2, costo = 20000.00, hotel_fk = 6 },
                new { id = 33, capacidad = 4, costo = 35000.00, hotel_fk = 6 },
                new { id = 34, capacidad = 4, costo = 35000.00, hotel_fk = 6 },
                new { id = 35, capacidad = 8, costo = 70000.00, hotel_fk = 6 },
                new { id = 36, capacidad = 8, costo = 70000.00, hotel_fk = 6 }, 
                new { id = 37, capacidad = 2, costo = 20000.00, hotel_fk = 7 },
                new { id = 38, capacidad = 2, costo = 20000.00, hotel_fk = 7 },
                new { id = 39, capacidad = 4, costo = 35000.00, hotel_fk = 7 },
                new { id = 40, capacidad = 4, costo = 35000.00, hotel_fk = 7 },
                new { id = 41, capacidad = 8, costo = 70000.00, hotel_fk = 7 },
                new { id = 42, capacidad = 8, costo = 70000.00, hotel_fk = 7 }, 
                new { id = 43, capacidad = 2, costo = 20000.00, hotel_fk = 8 },
                new { id = 44, capacidad = 2, costo = 20000.00, hotel_fk = 8 },
                new { id = 45, capacidad = 4, costo = 35000.00, hotel_fk = 8 },
                new { id = 46, capacidad = 4, costo = 35000.00, hotel_fk = 8 },
                new { id = 47, capacidad = 8, costo = 70000.00, hotel_fk = 8 },
                new { id = 48, capacidad = 8, costo = 70000.00, hotel_fk = 8 }, 
                new { id = 49, capacidad = 2, costo = 20000.00, hotel_fk = 9 },
                new { id = 50, capacidad = 2, costo = 20000.00, hotel_fk = 9 },
                new { id = 51, capacidad = 4, costo = 35000.00, hotel_fk = 9 },
                new { id = 52, capacidad = 4, costo = 35000.00, hotel_fk = 9 },
                new { id = 53, capacidad = 8, costo = 70000.00, hotel_fk = 9 },
                new { id = 54, capacidad = 8, costo = 70000.00, hotel_fk = 9 }, 
                new { id = 55, capacidad = 2, costo = 20000.00, hotel_fk = 10 },
                new { id = 56, capacidad = 2, costo = 20000.00, hotel_fk = 10 },
                new { id = 57, capacidad = 4, costo = 35000.00, hotel_fk = 10 },
                new { id = 58, capacidad = 4, costo = 35000.00, hotel_fk = 10 },
                new { id = 59, capacidad = 8, costo = 70000.00, hotel_fk = 10 },
                new { id = 60, capacidad = 8, costo = 70000.00, hotel_fk = 10 }, 
                new { id = 61, capacidad = 2, costo = 20000.00, hotel_fk = 11 },
                new { id = 62, capacidad = 2, costo = 20000.00, hotel_fk = 11 },
                new { id = 63, capacidad = 4, costo = 35000.00, hotel_fk = 11 },
                new { id = 64, capacidad = 4, costo = 35000.00, hotel_fk = 11 },
                new { id = 65, capacidad = 8, costo = 70000.00, hotel_fk = 11 },
                new { id = 66, capacidad = 8, costo = 70000.00, hotel_fk = 11 }, 
                new { id = 67, capacidad = 2, costo = 20000.00, hotel_fk = 12 },
                new { id = 68, capacidad = 2, costo = 20000.00, hotel_fk = 12 },
                new { id = 69, capacidad = 4, costo = 35000.00, hotel_fk = 12 },
                new { id = 70, capacidad = 4, costo = 35000.00, hotel_fk = 12 },
                new { id = 71, capacidad = 8, costo = 70000.00, hotel_fk = 12 },
                new { id = 72, capacidad = 8, costo = 70000.00, hotel_fk = 12 }, 
                new { id = 73, capacidad = 2, costo = 20000.00, hotel_fk = 13 },
                new { id = 74, capacidad = 2, costo = 20000.00, hotel_fk = 13 },
                new { id = 75, capacidad = 4, costo = 35000.00, hotel_fk = 13 },
                new { id = 76, capacidad = 4, costo = 35000.00, hotel_fk = 13 },
                new { id = 77, capacidad = 8, costo = 70000.00, hotel_fk = 13 },
                new { id = 78, capacidad = 8, costo = 70000.00, hotel_fk = 13 }, 
                new { id = 79, capacidad = 2, costo = 20000.00, hotel_fk = 14 },
                new { id = 80, capacidad = 2, costo = 20000.00, hotel_fk = 14 },
                new { id = 81, capacidad = 4, costo = 35000.00, hotel_fk = 14 },
                new { id = 82, capacidad = 4, costo = 35000.00, hotel_fk = 14 },
                new { id = 83, capacidad = 8, costo = 70000.00, hotel_fk = 14 },
                new { id = 84, capacidad = 8, costo = 70000.00, hotel_fk = 14 }, 
                new { id = 85, capacidad = 2, costo = 20000.00, hotel_fk = 15 },
                new { id = 86, capacidad = 2, costo = 20000.00, hotel_fk = 15 },
                new { id = 87, capacidad = 4, costo = 35000.00, hotel_fk = 15 },
                new { id = 88, capacidad = 4, costo = 35000.00, hotel_fk = 15 },
                new { id = 89, capacidad = 8, costo = 70000.00, hotel_fk = 15 },
                new { id = 90, capacidad = 8, costo = 70000.00, hotel_fk = 15 }, 
                new { id = 91, capacidad = 2, costo = 20000.00, hotel_fk = 16 },
                new { id = 92, capacidad = 2, costo = 20000.00, hotel_fk = 16 },
                new { id = 93, capacidad = 4, costo = 35000.00, hotel_fk = 16 },
                new { id = 94, capacidad = 4, costo = 35000.00, hotel_fk = 16 },
                new { id = 95, capacidad = 8, costo = 70000.00, hotel_fk = 16 },
                new { id = 96, capacidad = 8, costo = 70000.00, hotel_fk = 16 }, 
                new { id = 97, capacidad = 2, costo = 20000.00, hotel_fk = 17 },
                new { id = 98, capacidad = 2, costo = 20000.00, hotel_fk = 17 },
                new { id = 99, capacidad = 4, costo = 35000.00, hotel_fk = 17 },
                new { id = 100, capacidad = 4, costo = 35000.00, hotel_fk = 17 },
                new { id = 101, capacidad = 8, costo = 70000.00, hotel_fk = 17 },
                new { id = 102, capacidad = 8, costo = 70000.00, hotel_fk = 17 }, 
                new { id = 103, capacidad = 2, costo = 20000.00, hotel_fk = 18 },
                new { id = 104, capacidad = 2, costo = 20000.00, hotel_fk = 18 },
                new { id = 105, capacidad = 4, costo = 35000.00, hotel_fk = 18 },
                new { id = 106, capacidad = 4, costo = 35000.00, hotel_fk = 18 },
                new { id = 107, capacidad = 8, costo = 70000.00, hotel_fk = 18 },
                new { id = 108, capacidad = 8, costo = 70000.00, hotel_fk = 18 }, 
                new { id = 109, capacidad = 2, costo = 20000.00, hotel_fk = 19 },
                new { id = 110, capacidad = 2, costo = 20000.00, hotel_fk = 19 },
                new { id = 111, capacidad = 4, costo = 35000.00, hotel_fk = 19 },
                new { id = 112, capacidad = 4, costo = 35000.00, hotel_fk = 19 },
                new { id = 113, capacidad = 8, costo = 70000.00, hotel_fk = 19 },
                new { id = 114, capacidad = 8, costo = 70000.00, hotel_fk = 19 }, 
                new { id = 115, capacidad = 2, costo = 20000.00, hotel_fk = 20 },
                new { id = 116, capacidad = 2, costo = 20000.00, hotel_fk = 20 },
                new { id = 117, capacidad = 4, costo = 35000.00, hotel_fk = 20 },
                new { id = 118, capacidad = 4, costo = 35000.00, hotel_fk = 20 },
                new { id = 119, capacidad = 8, costo = 70000.00, hotel_fk = 20 },
                new { id = 120, capacidad = 8, costo = 70000.00, hotel_fk = 20 }, 
                new { id = 121, capacidad = 2, costo = 20000.00, hotel_fk = 2021 },
                new { id = 122, capacidad = 2, costo = 20000.00, hotel_fk = 2021 },
                new { id = 123, capacidad = 4, costo = 35000.00, hotel_fk = 2021 },
                new { id = 124, capacidad = 4, costo = 35000.00, hotel_fk = 2021 },
                new { id = 125, capacidad = 8, costo = 70000.00, hotel_fk = 2021 },
                new { id = 126, capacidad = 8, costo = 70000.00, hotel_fk = 2021 });

            modelBuilder.Entity<Vuelo>().HasData(
                new { id = 1, capacidad = 150, vendido = 0, costo = 22000.00, fecha = DateTime.Parse("2023-10-01 09:00:00"), aerolinea = "Aerolínea Buenos Aires", avion = "Boeing 747", origen_fk = 1, destino_fk = 3 },
                new { id = 2, capacidad = 120, vendido = 0, costo = 21000.00, fecha = DateTime.Parse("2023-10-02 09:00:00"), aerolinea = "Aerolínea Córdoba", avion = "Airbus A320", origen_fk = 2, destino_fk = 4 },
                new { id = 3, capacidad = 180, vendido = 0, costo = 24000.00, fecha = DateTime.Parse("2023-10-03 09:00:00"), aerolinea = "Aerolínea Mendoza", avion = "Boeing 787 Dreamliner", origen_fk = 3, destino_fk = 5 },
                new { id = 4, capacidad = 160, vendido = 0, costo = 22500.00, fecha = DateTime.Parse("2023-10-04 09:00:00"), aerolinea = "Aerolínea Buenos Aires", avion = "Airbus A320", origen_fk = 4, destino_fk = 1 },
                new { id = 5, capacidad = 130, vendido = 0, costo = 23000.00, fecha = DateTime.Parse("2023-10-05 09:00:00"), aerolinea = "Aerolínea Córdoba", avion = "Boeing 747", origen_fk = 5, destino_fk = 2 },
                new { id = 6, capacidad = 170, vendido = 0, costo = 23500.00, fecha = DateTime.Parse("2023-10-06 09:00:00"), aerolinea = "Aerolínea Córdoba", avion = "Boeing 787 Dreamliner", origen_fk = 1, destino_fk = 3 },
                new { id = 7, capacidad = 140, vendido = 0, costo = 22800.00, fecha = DateTime.Parse("2023-10-07 09:00:00"), aerolinea = "Aerolínea Buenos Aires", avion = "Airbus A320", origen_fk = 2, destino_fk = 4 },
                new { id = 8, capacidad = 110, vendido = 0, costo = 21500.00, fecha = DateTime.Parse("2023-10-08 09:00:00"), aerolinea = "Aerolínea Mendoza", avion = "Boeing 747", origen_fk = 3, destino_fk = 5 },
                new { id = 9, capacidad = 190, vendido = 0, costo = 24500.00, fecha = DateTime.Parse("2023-10-09 09:00:00"), aerolinea = "Aerolínea Córdoba", avion = "Boeing 787 Dreamliner", origen_fk = 4, destino_fk = 1 },
                new { id = 10, capacidad = 155, vendido = 0, costo = 22700.00, fecha = DateTime.Parse("2023-10-10 09:00:00"), aerolinea = "Aerolínea Buenos Aires", avion = "Airbus A320", origen_fk = 5, destino_fk = 2 },
                new { id = 11, capacidad = 170, vendido = 0, costo = 23500.00, fecha = DateTime.Parse("2023-10-11 09:00:00"), aerolinea = "Aerolínea Mendoza", avion = "Boeing 747", origen_fk = 1, destino_fk = 3 },
                new { id = 12, capacidad = 120, vendido = 0, costo = 21000.00, fecha = DateTime.Parse("2023-10-12 09:00:00"), aerolinea = "Aerolínea Córdoba", avion = "Boeing 787 Dreamliner", origen_fk = 2, destino_fk = 4 },
                new { id = 13, capacidad = 140, vendido = 0, costo = 22800.00, fecha = DateTime.Parse("2023-10-13 09:00:00"), aerolinea = "Aerolínea Buenos Aires", avion = "Airbus A320", origen_fk = 3, destino_fk = 5 },
                new { id = 14, capacidad = 180, vendido = 0, costo = 24000.00, fecha = DateTime.Parse("2023-10-14 09:00:00"), aerolinea = "Aerolínea Mendoza", avion = "Boeing 747", origen_fk = 4, destino_fk = 1 },
                new { id = 15, capacidad = 130, vendido = 0, costo = 23000.00, fecha = DateTime.Parse("2023-10-15 09:00:00"), aerolinea = "Aerolínea Córdoba", avion = "Boeing 787 Dreamliner", origen_fk = 5, destino_fk = 2 },
                new { id = 16, capacidad = 150, vendido = 0, costo = 22000.00, fecha = DateTime.Parse("2023-10-16 09:00:00"), aerolinea = "Aerolínea Buenos Aires", avion = "Airbus A320", origen_fk = 1, destino_fk = 3 },
                new { id = 17, capacidad = 160, vendido = 0, costo = 22500.00, fecha = DateTime.Parse("2023-10-17 09:00:00"), aerolinea = "Aerolínea Mendoza", avion = "Boeing 747", origen_fk = 2, destino_fk = 4 },
                new { id = 18, capacidad = 190, vendido = 0, costo = 24500.00, fecha = DateTime.Parse("2023-10-18 09:00:00"), aerolinea = "Aerolínea Córdoba", avion = "Boeing 787 Dreamliner", origen_fk = 3, destino_fk = 5 },
                new { id = 19, capacidad = 155, vendido = 0, costo = 22700.00, fecha = DateTime.Parse("2023-10-19 09:00:00"), aerolinea = "Aerolínea Buenos Aires", avion = "Airbus A320", origen_fk = 4, destino_fk = 1 },
                new { id = 20, capacidad = 110, vendido = 0, costo = 21500.00, fecha = DateTime.Parse("2023-10-20 09:00:00"), aerolinea = "Aerolínea Mendoza", avion = "Boeing 747", origen_fk = 5, destino_fk = 2 },
                new { id = 22, capacidad = 110, vendido = 0, costo = 21000.00, fecha = DateTime.Parse("2023-10-25 09:00:00"), aerolinea = "PRUEBA", avion = "PRUEBA", origen_fk = 1, destino_fk = 19 });

            modelBuilder.Entity<ReservaHabitacion>().HasData(
                new { id = 1, fechaDesde = DateTime.Parse("2023-10-01 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-05 09:00:00.000"), pagado = 150000.00, cantPersonas = 2, habitacion_fk = 1, usuarioRH_fk = 1 },
                new { id = 2, fechaDesde = DateTime.Parse("2023-11-15 09:00:00.000"), fechaHasta = DateTime.Parse("2023-11-20 09:00:00.000"), pagado = 120000.00, cantPersonas = 2, habitacion_fk = 7, usuarioRH_fk = 2 },
                new { id = 3, fechaDesde = DateTime.Parse("2023-10-03 09:00:00.000"), fechaHasta = DateTime.Parse("2023-11-03 09:00:00.000"), pagado = 170000.00, cantPersonas = 2, habitacion_fk = 13, usuarioRH_fk = 3 },
                new { id = 4, fechaDesde = DateTime.Parse("2023-09-20 09:00:00.000"), fechaHasta = DateTime.Parse("2023-09-25 09:00:00.000"), pagado = 140000.00, cantPersonas = 2, habitacion_fk = 19, usuarioRH_fk = 4 },
                new { id = 5, fechaDesde = DateTime.Parse("2023-11-01 09:00:00.000"), fechaHasta = DateTime.Parse("2023-11-10 09:00:00.000"), pagado = 110000.00, cantPersonas = 2, habitacion_fk = 25, usuarioRH_fk = 5 },
                new { id = 6, fechaDesde = DateTime.Parse("2023-10-08 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-12 09:00:00.000"), pagado = 150000.00, cantPersonas = 2, habitacion_fk = 1, usuarioRH_fk = 6 },
                new { id = 7, fechaDesde = DateTime.Parse("2023-12-05 09:00:00.000"), fechaHasta = DateTime.Parse("2023-12-10 09:00:00.000"), pagado = 120000.00, cantPersonas = 2, habitacion_fk = 7, usuarioRH_fk = 7 },
                new { id = 8, fechaDesde = DateTime.Parse("2023-09-25 09:00:00.000"), fechaHasta = DateTime.Parse("2023-09-30 09:00:00.000"), pagado = 170000.00, cantPersonas = 2, habitacion_fk = 13, usuarioRH_fk = 8 },
                new { id = 9, fechaDesde = DateTime.Parse("2023-11-12 09:00:00.000"), fechaHasta = DateTime.Parse("2023-11-17 09:00:00.000"), pagado = 140000.00, cantPersonas = 2, habitacion_fk = 19, usuarioRH_fk = 9 },
                new { id = 10, fechaDesde = DateTime.Parse("2023-12-20 09:00:00.000"), fechaHasta = DateTime.Parse("2023-12-25 09:00:00.000"), pagado = 110000.00, cantPersonas = 2, habitacion_fk = 25, usuarioRH_fk = 10 },
                new { id = 1002, fechaDesde = DateTime.Parse("2023-10-20 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-24 09:00:00.000"), pagado = 150000.00, cantPersonas = 2, habitacion_fk = 1, usuarioRH_fk = 3 },
                new { id = 2007, fechaDesde = DateTime.Parse("2023-10-25 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-31 09:00:00.000"), pagado = 120000.00, cantPersonas = 2, habitacion_fk = 7, usuarioRH_fk = 1024 },
                new { id = 2008, fechaDesde = DateTime.Parse("2023-10-11 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-18 09:00:00.000"), pagado = 110000.00, cantPersonas = 2, habitacion_fk = 25, usuarioRH_fk = 1024 },
                new { id = 2009, fechaDesde = DateTime.Parse("2023-10-25 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-25 09:00:00.000"), pagado = 160000.00, cantPersonas = 2, habitacion_fk = 31, usuarioRH_fk = 1024 },
                new { id = 2010, fechaDesde = DateTime.Parse("2023-10-23 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-23 09:00:00.000"), pagado = 135000.00, cantPersonas = 2, habitacion_fk = 37, usuarioRH_fk = 1024 },
                new { id = 2011, fechaDesde = DateTime.Parse("2023-10-02 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-05 09:00:00.000"), pagado = 110000.00, cantPersonas = 2, habitacion_fk = 25, usuarioRH_fk = 1024 });

            modelBuilder.Entity<ReservaVuelo>().HasData(
                new { id = 1, pagado = 120000.00, cantPersonas = 1, vuelo_fk = 1, usuarioRV_fk = 1 },
                new { id = 2, pagado = 110000.00, cantPersonas = 1, vuelo_fk = 2, usuarioRV_fk = 2 },
                new { id = 3, pagado = 140000.00, cantPersonas = 1, vuelo_fk = 3, usuarioRV_fk = 3 },
                new { id = 4, pagado = 125000.00, cantPersonas = 1, vuelo_fk = 4, usuarioRV_fk = 4 },
                new { id = 5, pagado = 130000.00, cantPersonas = 2, vuelo_fk = 5, usuarioRV_fk = 5 },
                new { id = 6, pagado = 120000.00, cantPersonas = 2, vuelo_fk = 1, usuarioRV_fk = 6 },
                new { id = 7, pagado = 110000.00, cantPersonas = 2, vuelo_fk = 2, usuarioRV_fk = 7 },
                new { id = 8, pagado = 140000.00, cantPersonas = 1, vuelo_fk = 3, usuarioRV_fk = 8 },
                new { id = 9, pagado = 125000.00, cantPersonas = 2, vuelo_fk = 4, usuarioRV_fk = 9 },
                new { id = 10, pagado = 130000.00, cantPersonas = 2, vuelo_fk = 5, usuarioRV_fk = 10 },
                new { id = 1002, pagado = 120000.00, cantPersonas = 1, vuelo_fk = 1, usuarioRV_fk = 3 },
                new { id = 2010, pagado = 125000.00, cantPersonas = 1, vuelo_fk = 4, usuarioRV_fk = 1024 },
                new { id = 2011, pagado = 130000.00, cantPersonas = 1, vuelo_fk = 5, usuarioRV_fk = 1024 });

            modelBuilder.Entity<UsuarioHabitacion>().HasData(
                new { usuarios_fk = 1, habitaciones_fk = 1, cantidad = 2 },
                new { usuarios_fk = 2, habitaciones_fk = 7, cantidad = 0 },
                new { usuarios_fk = 3, habitaciones_fk = 1, cantidad = 1 },
                new { usuarios_fk = 3, habitaciones_fk = 13, cantidad = 1 },
                new { usuarios_fk = 4, habitaciones_fk = 19, cantidad = 1 },
                new { usuarios_fk = 5, habitaciones_fk = 25, cantidad = 0 },
                new { usuarios_fk = 6, habitaciones_fk = 1, cantidad = 1 },
                new { usuarios_fk = 7, habitaciones_fk = 7, cantidad = 0 },
                new { usuarios_fk = 8, habitaciones_fk = 13, cantidad = 1 },
                new { usuarios_fk = 9, habitaciones_fk = 19, cantidad = 0 },
                new { usuarios_fk = 10, habitaciones_fk = 25, cantidad = 0 },
                new { usuarios_fk = 18, habitaciones_fk = 1, cantidad = 2 },
                new { usuarios_fk = 1024, habitaciones_fk = 1, cantidad = 1 },
                new { usuarios_fk = 1024, habitaciones_fk = 7, cantidad = 0 },
                new { usuarios_fk = 1024, habitaciones_fk = 25, cantidad = 2 },
                new { usuarios_fk = 1024, habitaciones_fk = 31, cantidad = 0 },
                new { usuarios_fk = 1024, habitaciones_fk = 37, cantidad = 1 },
                new { usuarios_fk = 1024, habitaciones_fk = 121, cantidad = 9 });

            modelBuilder.Entity<UsuarioVuelo>().HasData(
                new { usuario_fk = 1, vuelo_fk = 1 },
                new { usuario_fk = 2, vuelo_fk = 2 },
                new { usuario_fk = 3, vuelo_fk = 1 },
                new { usuario_fk = 3, vuelo_fk = 3 },
                new { usuario_fk = 4, vuelo_fk = 4 },
                new { usuario_fk = 5, vuelo_fk = 5 },
                new { usuario_fk = 6, vuelo_fk = 1 },
                new { usuario_fk = 7, vuelo_fk = 2 },
                new { usuario_fk = 8, vuelo_fk = 3 },
                new { usuario_fk = 9, vuelo_fk = 4 },
                new { usuario_fk = 10, vuelo_fk = 5 },
                new { usuario_fk = 18, vuelo_fk = 1 },
                new { usuario_fk = 1024, vuelo_fk = 1 },
                new { usuario_fk = 1024, vuelo_fk = 4 },
                new { usuario_fk = 1024, vuelo_fk = 5 });

        }
    }
}
