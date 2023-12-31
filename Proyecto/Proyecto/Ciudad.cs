﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class Ciudad
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public List<Vuelo> vuelos { get; set; }
        public List<Hotel> hoteles { get; set; }

        public Ciudad(string nombre)
        {
            this.nombre = nombre;
            this.hoteles = new List<Hotel>();
            this.vuelos = new List<Vuelo>();
        }

    }
}
