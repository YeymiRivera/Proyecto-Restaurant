using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace proyecto.Models
{
    public class ResultadoOperacion
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = "";

    }
}