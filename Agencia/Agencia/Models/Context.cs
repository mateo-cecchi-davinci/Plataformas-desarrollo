using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Agencia.Models
{
    public class Context : DbContext
    {
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Hotel> hoteles { get; set; }
        public DbSet<Vuelo> vuelos { get; set; }
        public DbSet<Ciudad> ciudades { get; set; }
        public DbSet<ReservaHotel> reservasHotel { get; set; }
        public DbSet<ReservaVuelo> reservasVuelo { get; set; }
        public DbSet<UsuarioHotel> usuarioHotel { get; set; }
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
            modelBuilder.Entity<Vuelo>()
                .ToTable("vuelo")
                .HasKey(v => v.id);
            modelBuilder.Entity<Ciudad>()
                .ToTable("ciudad")
                .HasKey(c => c.id);
            modelBuilder.Entity<ReservaHotel>()
                .ToTable("reservaHotel")
                .HasKey(rh => rh.id);
            modelBuilder.Entity<ReservaVuelo>()
                .ToTable("reservaVuelo")
                .HasKey(rv => rv.id);

            //RELACIONES

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.misReservasHoteles)
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
                .HasMany(h => h.misReservas)
                .WithOne(rh => rh.miHotel)
                .HasForeignKey(rh => rh.hotel_fk)
                .HasConstraintName("hotel_fk")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.misReservas)
                .WithMany(rh => rh.miHotel)
                .HasForeignKey(rh => rh.hotel_fk)
                .HasConstraintName("hotel_fk")
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Hotel>()
            //    .HasMany(h => h.huespedes)
            //    .WithMany(u => u.hotelesVisitados)
            //    .UsingEntity<UsuarioHotel>(
            //        euh => euh.HasOne(uh => uh.usuario).WithMany(u => u.usuario_hotel).HasForeignKey(uh => uh.usuario_fk),
            //        euh => euh.HasOne(uh => uh.hotel).WithMany(h => h.hotel_usuario).HasForeignKey(uh => uh.hotel_fk),
            //        euh => euh.HasKey(k => new { k.usuario_fk, k.hotel_fk })
            //    );



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
                .WithMany(c => c.vuelos)
                .HasForeignKey(v => v.origen_fk)
                .HasConstraintName("origen_fk")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vuelo>()
                .HasOne(v => v.destino)
                .WithMany()
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
                    h.Property(h => h.habitaciones_chicas).HasColumnType("int");
                    h.Property(h => h.habitaciones_medianas).HasColumnType("int");
                    h.Property(h => h.habitaciones_grandes).HasColumnType("int");
                    h.Property(h => h.costo_hab_chicas).HasColumnType("float");
                    h.Property(h => h.costo_hab_medianas).HasColumnType("float");
                    h.Property(h => h.costo_hab_grandes).HasColumnType("float");
                    h.Property(h => h.nombre).HasColumnType("varchar(50)");
                    h.Property(h => h.nombre).IsRequired(true);
                    h.Property(h => h.imagen).HasColumnType("varchar(255)");
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

            modelBuilder.Entity<ReservaHotel>(
                rh =>
                {
                    rh.Property(rh => rh.fechaDesde).HasColumnType("datetime");
                    rh.Property(rh => rh.fechaDesde).IsRequired(true);
                    rh.Property(rh => rh.fechaHasta).HasColumnType("datetime");
                    rh.Property(rh => rh.fechaHasta).IsRequired(true);
                    rh.Property(rh => rh.pagado).HasColumnType("float");
                    rh.Property(rh => rh.cant_hab_chicas).HasColumnType("int");
                    rh.Property(rh => rh.cant_hab_medianas).HasColumnType("int");
                    rh.Property(rh => rh.cant_hab_grandes).HasColumnType("int");
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
                new { id = 1, nombre = "Hotel Buenos Aires", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 7 },
                new { id = 2, nombre = "Hotel Rosario", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 2 },
                new { id = 3, nombre = "Hotel Córdoba", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 3 },
                new { id = 4, nombre = "Hotel Mendoza", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 4 },
                new { id = 5, nombre = "Hotel San Juan", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 5 },
                new { id = 6, nombre = "Hotel Mar del Plata", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 6 },
                new { id = 7, nombre = "Hotel Tucumán", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 7 },
                new { id = 8, nombre = "Hotel Salta", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 8 },
                new { id = 9, nombre = "Hotel Jujuy", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 9 },
                new { id = 10, nombre = "Hotel Neuquén", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 10 },
                new { id = 11, nombre = "Hotel La Plata", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 1 },
                new { id = 12, nombre = "Hotel Santa Fe", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 2 },
                new { id = 13, nombre = "Hotel San Luis", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 3 },
                new { id = 14, nombre = "Hotel Formosa", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 4 },
                new { id = 15, nombre = "Hotel Entre Ríos", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 5 },
                new { id = 16, nombre = "Hotel Catamarca", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 6 },
                new { id = 17, nombre = "Hotel La Rioja", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 7 },
                new { id = 18, nombre = "Hotel Chaco", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 8 },
                new { id = 19, nombre = "Hotel Tierra del Fuego", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 9 },
                new { id = 20, nombre = "Hotel Santa Cruz", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 10 },
                new { id = 2021, nombre = "PRUEBA", habitaciones_chicas = 50, habitaciones_medianas = 20, habitaciones_grandes = 10, costo_hab_chicas = 20000.00, costo_hab_medianas = 35000.00, costo_hab_grandes = 70000.00, ciudad_fk = 7 });

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

            modelBuilder.Entity<ReservaHotel>().HasData(
                new { id = 1, fechaDesde = DateTime.Parse("2023-10-01 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-05 09:00:00.000"), pagado = 150000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 1, usuarioRH_fk = 1 },
                new { id = 2, fechaDesde = DateTime.Parse("2023-11-15 09:00:00.000"), fechaHasta = DateTime.Parse("2023-11-20 09:00:00.000"), pagado = 120000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 2, usuarioRH_fk = 2 },
                new { id = 3, fechaDesde = DateTime.Parse("2023-10-03 09:00:00.000"), fechaHasta = DateTime.Parse("2023-11-03 09:00:00.000"), pagado = 170000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 3, usuarioRH_fk = 3 },
                new { id = 4, fechaDesde = DateTime.Parse("2023-09-20 09:00:00.000"), fechaHasta = DateTime.Parse("2023-09-25 09:00:00.000"), pagado = 140000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 4, usuarioRH_fk = 4 },
                new { id = 5, fechaDesde = DateTime.Parse("2023-11-01 09:00:00.000"), fechaHasta = DateTime.Parse("2023-11-10 09:00:00.000"), pagado = 110000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 5, usuarioRH_fk = 5 },
                new { id = 6, fechaDesde = DateTime.Parse("2023-10-08 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-12 09:00:00.000"), pagado = 150000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 1, usuarioRH_fk = 6 },
                new { id = 7, fechaDesde = DateTime.Parse("2023-12-05 09:00:00.000"), fechaHasta = DateTime.Parse("2023-12-10 09:00:00.000"), pagado = 120000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 2, usuarioRH_fk = 7 },
                new { id = 8, fechaDesde = DateTime.Parse("2023-09-25 09:00:00.000"), fechaHasta = DateTime.Parse("2023-09-30 09:00:00.000"), pagado = 170000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 3, usuarioRH_fk = 8 },
                new { id = 9, fechaDesde = DateTime.Parse("2023-11-12 09:00:00.000"), fechaHasta = DateTime.Parse("2023-11-17 09:00:00.000"), pagado = 140000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 4, usuarioRH_fk = 9 },
                new { id = 10, fechaDesde = DateTime.Parse("2023-12-20 09:00:00.000"), fechaHasta = DateTime.Parse("2023-12-25 09:00:00.000"), pagado = 110000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 5, usuarioRH_fk = 10 },
                new { id = 1002, fechaDesde = DateTime.Parse("2023-10-20 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-24 09:00:00.000"), pagado = 150000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 1, usuarioRH_fk = 3 },
                new { id = 2007, fechaDesde = DateTime.Parse("2023-10-25 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-31 09:00:00.000"), pagado = 120000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 2, usuarioRH_fk = 1024 },
                new { id = 2008, fechaDesde = DateTime.Parse("2023-10-11 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-18 09:00:00.000"), pagado = 110000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 5, usuarioRH_fk = 1024 },
                new { id = 2009, fechaDesde = DateTime.Parse("2023-10-25 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-25 09:00:00.000"), pagado = 160000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 6, usuarioRH_fk = 1024 },
                new { id = 2010, fechaDesde = DateTime.Parse("2023-10-23 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-23 09:00:00.000"), pagado = 135000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 7, usuarioRH_fk = 1024 },
                new { id = 2011, fechaDesde = DateTime.Parse("2023-10-02 09:00:00.000"), fechaHasta = DateTime.Parse("2023-10-05 09:00:00.000"), pagado = 110000.00, cant_hab_chicas = 1, cant_hab_medianas = 0, cant_hab_grandes = 0, hotel_fk = 5, usuarioRH_fk = 1024 });

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

            modelBuilder.Entity<UsuarioHotel>().HasData(
                new { usuario_fk = 1, hotel_fk = 1, cantidad = 2 },
                new { usuario_fk = 2, hotel_fk = 2, cantidad = 0 },
                new { usuario_fk = 3, hotel_fk = 1, cantidad = 1 },
                new { usuario_fk = 3, hotel_fk = 3, cantidad = 1 },
                new { usuario_fk = 4, hotel_fk = 4, cantidad = 1 },
                new { usuario_fk = 5, hotel_fk = 5, cantidad = 0 },
                new { usuario_fk = 6, hotel_fk = 1, cantidad = 1 },
                new { usuario_fk = 7, hotel_fk = 2, cantidad = 0 },
                new { usuario_fk = 8, hotel_fk = 3, cantidad = 1 },
                new { usuario_fk = 9, hotel_fk = 4, cantidad = 0 },
                new { usuario_fk = 10, hotel_fk = 5, cantidad = 0 },
                new { usuario_fk = 18, hotel_fk = 1, cantidad = 2 },
                new { usuario_fk = 1024, hotel_fk = 1, cantidad = 1 },
                new { usuario_fk = 1024, hotel_fk = 2, cantidad = 0 },
                new { usuario_fk = 1024, hotel_fk = 5, cantidad = 2 },
                new { usuario_fk = 1024, hotel_fk = 6, cantidad = 0 },
                new { usuario_fk = 1024, hotel_fk = 7, cantidad = 1 },
                new { usuario_fk = 1024, hotel_fk = 2021, cantidad = 9 });

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
