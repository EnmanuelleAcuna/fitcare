using System;
using System.Text.Json;

namespace fitcare.Models.DataAccess;

public class ReporteRutinaResumido
{
	public string Id { get; set; }
	public string NombreInstructor { get; set; }
	public string NombreCliente { get; set; }
	public DateTime FechaRegistro { get; set; }
	public DateTime FechaInicio { get; set; }
	public DateTime FechaFin { get; set; }
	public string Objetivo { get; set; }
	public int DiasRutina { get; set; }
	public int CantidadEjerciciosRegistrados { get; set; }

	// Sobreescribir metodo ToString() de la clase para devolver el objeto en una cadena string en formato JSON
	public override string ToString() => JsonSerializer.Serialize(this);
}
