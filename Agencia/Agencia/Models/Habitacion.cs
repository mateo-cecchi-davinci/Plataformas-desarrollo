namespace Agencia.Models
{
    public class Habitacion
    {
        public int id {  get; set; }
        public int capacidad { get; set; }
        public List<Usuario> usuarios { get; set; } = new List<Usuario>();
        public Hotel hotel { get; set; }
        public int hotel_fk { get; set; }

        public Habitacion () { }

        public Habitacion (int capacidad, Hotel hotel)
        {
            this.capacidad = capacidad;
            this.hotel = hotel;
        }
    }
}
