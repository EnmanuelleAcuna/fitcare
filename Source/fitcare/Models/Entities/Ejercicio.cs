using System;
using System.Collections.Generic;
using System.Text.Json;

namespace fitcare.Models.Entities;

public class Ejercicio : Base
{
	public Guid Id { get; set; }
	public string Codigo { get; set; }
	public string Nombre { get; set; }
	public bool Estado { get; set; }

	public TipoEjercicio TipoEjercicio { get; set; }
	public IList<Maquina> Maquinas { get; set; }
	public IList<GrupoMuscular> GruposMusculares { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}

public class TipoEjercicio : Base
{
	public TipoEjercicio(Guid id, string codigo, string nombre, bool estado)
	{
		Id = id;
		Codigo = codigo;
		Nombre = nombre;
		Estado = estado;
	}

	public Guid Id { get; set; }
	public string Codigo { get; set; }
	public string Nombre { get; set; }
	public bool Estado { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
