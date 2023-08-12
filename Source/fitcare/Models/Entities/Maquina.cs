using System;
using System.Collections.Generic;
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

public class TipoMaquina : Base
{
	public Guid Id { get; private set; }
	public string Nombre { get; private set; }
	public bool Estado { get; private set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
