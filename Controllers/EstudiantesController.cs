using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Http;
using WebApplication.Models;
using WebApplication.Utils;

namespace WebApplication.Controllers
{
    public class EstudiantesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetEstudiantes()
        {
            if (!BasicAuthentication.Authenticate(Request))
            {
                return Content(HttpStatusCode.Unauthorized, new { Message = "Token no válido" });
            }

            string connectionString = ConfigurationManager.ConnectionStrings["WebApplicationDB"].ConnectionString;

            List<Estudiante> estudiantes = new List<Estudiante>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Estudiantes";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Estudiante estudiante = new Estudiante
                            {
                                Id = reader.GetInt32("Id"),
                                NombreCompleto = reader.GetString("NombreCompleto"),
                                FechaNacimiento = reader.GetDateTime("FechaNacimiento"),
                                NombrePadre = reader.GetString("NombrePadre"),
                                NombreMadre = reader.GetString("NombreMadre"),
                                Grado = reader.GetString("Grado"),
                                Seccion = reader.GetString("Seccion"),
                                FechaIngreso = reader.GetDateTime("FechaIngreso")
                            };

                            estudiantes.Add(estudiante);
                        }
                    }
                    return Json(estudiantes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return Content(HttpStatusCode.InternalServerError, new { Message = ex.Message });
                }
            }
        }

        [HttpGet]
        [Route("api/Estudiantes/Grado/{Grado}")]
        public IHttpActionResult ObtenerEstudiantesPorSeccion(String Grado)
        {
            if (!BasicAuthentication.Authenticate(Request))
            {
                return Content(HttpStatusCode.Unauthorized, new { Message = "Token no válido" });
            }
            string connectionString = ConfigurationManager.ConnectionStrings["WebApplicationDB"].ConnectionString;

            List<Estudiante> estudiantes = new List<Estudiante>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    String query = String.Format("SELECT * FROM Estudiantes WHERE Grado = '{0}'", Grado);
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Estudiante estudiante = new Estudiante
                            {
                                Id = reader.GetInt32("Id"),
                                NombreCompleto = reader.GetString("NombreCompleto"),
                                FechaNacimiento = reader.GetDateTime("FechaNacimiento"),
                                NombrePadre = reader.GetString("NombrePadre"),
                                NombreMadre = reader.GetString("NombreMadre"),
                                Grado = reader.GetString("Grado"),
                                Seccion = reader.GetString("Seccion"),
                                FechaIngreso = reader.GetDateTime("FechaIngreso")
                            };

                            estudiantes.Add(estudiante);
                        }
                    }
                    return Json(estudiantes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return Content(HttpStatusCode.InternalServerError, new { Message = ex.Message });
                }
            }
        }

        [HttpPost]
        public IHttpActionResult PostEstudiantes(Estudiante estudiante)
        {
            if (!BasicAuthentication.Authenticate(Request))
            {
                return Content(HttpStatusCode.Unauthorized, new { Message = "Token no válido" });
            }
            string connectionString = ConfigurationManager.ConnectionStrings["WebApplicationDB"].ConnectionString;

            List<Estudiante> estudiantes = new List<Estudiante>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"INSERT INTO Estudiantes
                        (NombreCompleto, FechaNacimiento, NombrePadre, NombreMadre, Grado, Seccion, FechaIngreso) VALUES 
                        (@NombreCompleto, @FechaNacimiento, @NombrePadre, @NombreMadre, @Grado, @Seccion, @FechaIngreso)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreCompleto", estudiante.NombreCompleto);
                        command.Parameters.AddWithValue("@FechaNacimiento", estudiante.FechaNacimiento);
                        command.Parameters.AddWithValue("@NombrePadre", estudiante.NombrePadre);
                        command.Parameters.AddWithValue("@NombreMadre", estudiante.NombreMadre);
                        command.Parameters.AddWithValue("@Grado", estudiante.Grado);
                        command.Parameters.AddWithValue("@Seccion", estudiante.Seccion);
                        command.Parameters.AddWithValue("@FechaIngreso", estudiante.FechaIngreso);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Content(HttpStatusCode.OK, new { Message = "El estudiante se insertó correctamente." });

                        }
                        else
                        {
                            return Content(HttpStatusCode.BadRequest, new { Message = "No se pudo insertar el estudiante." });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return Content(HttpStatusCode.InternalServerError, new { Message = ex.Message });
                }
            }
        }
    }
}
