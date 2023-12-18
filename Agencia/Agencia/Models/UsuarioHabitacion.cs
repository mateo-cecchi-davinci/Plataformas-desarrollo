namespace Agencia.Models
{
    public class UsuarioHabitacion
    {
        public int usuarios_fk { get; set; }
        public int habitaciones_fk { get; set; }
        public int cantidad { get; set; }
        public Usuario usuario { get; set; }
        public Habitacion habitacion { get; set; }

        public UsuarioHabitacion() { }

        public UsuarioHabitacion(int usuario_fk, int habitaciones_fk, int cantidad)
        {

            this.usuarios_fk = usuario_fk;
            this.habitaciones_fk = habitaciones_fk;
            this.cantidad = cantidad;

        }
    }
}
