using System;

namespace WebApplication.Models
{
    public class Estudiante
    {
        public Int32 Id { get; set; }
        public String NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public String NombrePadre { get; set; }
        public String NombreMadre { get; set; }
        public String Grado { get; set; }
        public String Seccion { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}