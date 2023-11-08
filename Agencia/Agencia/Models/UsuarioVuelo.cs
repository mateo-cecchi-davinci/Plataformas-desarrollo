using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencia.Models
{
    public class UsuarioVuelo
    {

        public int usuario_fk { get; set; }
        public int vuelo_fk { get; set; }
        public Usuario usuario { get; set; }
        public Vuelo vuelo { get; set;}

        public UsuarioVuelo() { }

        public UsuarioVuelo (int usuario_fk, int vuelo_fk)
        {
            this.usuario_fk = usuario_fk;
            this.vuelo_fk = vuelo_fk;
        }

    }
}
