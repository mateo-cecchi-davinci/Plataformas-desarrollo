using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class UsuarioHotel
    {

        public int usuario_fk {  get; set; }
        public int hotel_fk { get; set; }
        public int cantidad { get; set; }
        public Usuario usuario { get; set; } 
        public Hotel hotel { get; set; }

        public UsuarioHotel() { }

        public UsuarioHotel(int usuario_fk, int hotel_fk, int cantidad) 
        { 
        
            this.usuario_fk = usuario_fk;
            this.hotel_fk = hotel_fk;
            this.cantidad = cantidad;

        }
    }
}
