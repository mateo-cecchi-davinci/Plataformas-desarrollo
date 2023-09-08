﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    internal class Hotel
    {

        private List<Usuario> huesped;

        public int id { get; set; }
        public Ciudad ubicacion { get; set; }
        public int capacidad { get; set; }
        public double costo { get; set; }
        public List<Usuario> huespedes { get => huesped.ToList(); }
        public string nombre { get; set; }

        public Hotel (int id, Ciudad ubicacion, int capacidad, double costo, List<Usuario> huespedes, string nombre)
        {
            this.id = id;
            this.ubicacion = ubicacion;
            this.capacidad = capacidad;
            this.costo = costo;
            this.huesped = new List<Usuario>();
            this.nombre = nombre;
        }

    }
}
