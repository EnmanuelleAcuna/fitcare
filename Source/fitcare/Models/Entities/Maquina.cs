using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace fitcare.Models.Entities;

public class Maquina : Base
{
	public Guid Id { get; private set; }
	public string Codigo { get; private set; }
	public string Nombre { get; private set; }
	public string CodigoActivo { get; private set; }
	public bool Estado { get; private set; }
	public DateTime FechaAdquisicion { get; private set; }

	public TipoMaquina TipoMaquina { get; set; }
	public IList<GrupoMuscular> GruposMusculares { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}

[Table("TIPOS_MAQUINA", Schema = "fitcare")]
public class TipoMaquina : Base
{
	public TipoMaquina()
	{
		Maquinas = new HashSet<Maquina>();
	}

	public TipoMaquina(Guid id, string nombre, bool estado, string codigo)
	{
		Id = id;
		Nombre = nombre;
		Estado = estado;
		Codigo = codigo;
	}

	public TipoMaquina(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; set; }
	public string Nombre { get; set; }
	public string Codigo { get; set; }
	public bool Estado { get; set; }

	public ICollection<Maquina> Maquinas { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
