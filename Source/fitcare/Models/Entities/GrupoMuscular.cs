using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace fitcare.Models.Entities;

[Table("GruposMusculares", Schema = "fitcare")]
public class GrupoMuscular : Base
{
	public GrupoMuscular() : base() { }

	public GrupoMuscular(Guid id, string nombre, string descripcion, bool estado)
	{
		Id = id;
		Nombre = nombre;
		Descripcion = descripcion;
		Estado = estado;
	}

	public GrupoMuscular(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; set; }
	public string Nombre { get; set; }
	public string Descripcion { get; set; }
	public bool Estado { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
