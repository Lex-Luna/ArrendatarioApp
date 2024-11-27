using System;
using System.Collections.Generic;
using System.Text;

namespace ArrendatarioPilasApp.Models
{
    public class Respuesta
    {
        public Respuesta()
        {
            //this.Mensaje = "No se puede conectar";
            this.Exito = 0;
        }
        public int Exito { get; set; }
        public string Mensaje { get; set; }
    }
}
