using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace fitcare.Models.Entities;

[Table("PlanesMembresia", Schema = "fitcare")]
public class PlanMembresia : Base
{
	public PlanMembresia() : base() { }

	public PlanMembresia(Guid id, string nombre, int meses, decimal costo, bool estado)
	{
		Id = id;
		Nombre = nombre;
		Meses = meses;
		Costo = costo;
		Estado = estado;
	}

	public Guid Id { get; set; }
	public string Nombre { get; set; }
	public int Meses { get; set; }
	public decimal Costo { get; set; }
	public bool Estado { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
