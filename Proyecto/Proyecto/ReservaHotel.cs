﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class ReservaHotel
    {

        public int id { get; set; }
        public Hotel miHotel { get; set; }
        public Usuario miUsuario { get; set; }
        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public double pagado { get; set; }
        public int cantPersonas { get; set; }

        public ReservaHotel (int id, Hotel miHotel, Usuario miUsuario, DateTime fechaDesde, DateTime fechaHasta, double pagado, int cantPersonas)
        {
            this.id = id;
            this.miHotel = miHotel;
            this.miUsuario = miUsuario;
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
            this.pagado = pagado;
            this.cantPersonas = cantPersonas;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), miHotel.nombre, miUsuario.id.ToString(), miUsuario.nombre, fechaDesde.ToString(), fechaHasta.ToString(), pagado.ToString(), cantPersonas.ToString() };
        }
    }
}
