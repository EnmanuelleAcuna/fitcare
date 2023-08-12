using fitcare.Models.Entities;
using System.Collections.Generic;
using System.Text.Json;

namespace fitcare.Models.DataAccess;

public class ReporteRutinaDetallado : ReporteRutinaResumido
{
	public IEnumerable<EjercicioRutina> Ejercicios { get; set; }
	public IEnumerable<MedidaRutina> Medidas { get; set; }

	// Sobreescribir metodo ToString() de la clase para devolver el objeto en una cadena string en formato JSON
	public override string ToString() => JsonSerializer.Serialize(this);
}
