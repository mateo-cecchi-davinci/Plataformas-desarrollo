namespace Agencia.Models
{
    public class Habitacion
    {
        public int id {  get; set; }
        public int capacidad { get; set; }
        public double costo { get; set; }
        public ICollection<Usuario> usuarios { get; set; } = new List<Usuario>();
        public List<UsuarioHabitacion> habitacion_usuario { get; set; } = new List<UsuarioHabitacion>();
        public Hotel hotel { get; set; }
        public int hotel_fk { get; set; }
        public List<ReservaHabitacion> misReservas { get; set; } = new List<ReservaHabitacion>();

        public Habitacion () { }

        public Habitacion (int capacidad, double costo, Hotel hotel, int hotel_fk)
        {
            this.capacidad = capacidad;
            this.costo = costo;
            this.hotel = hotel;
            this.hotel_fk = hotel_fk;
        }
    }
}
