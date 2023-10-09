using System;
using System.Text.Json;

namespace fitcare.Models.Entities;

public class GrupoMuscular
{
	public GrupoMuscular(Guid id, string nombre, string descripcion, bool estado)
	{
		Id = id;
		Nombre = nombre;
		Descripcion = descripcion;
		Estado = estado;
	}

	public Guid Id { get; private set; }
	public string Nombre { get; set; }
	public string Descripcion { get; set; }
	public bool Estado { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
