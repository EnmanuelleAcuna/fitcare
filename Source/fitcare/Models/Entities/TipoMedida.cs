using System;
using System.Text.Json;

namespace fitcare.Models.Entities;

public class TipoMedida : Base
{
	public Guid Id { get; set; }
	public string Codigo { get; set; }
	public string Nombre { get; set; }
	public bool Estado { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
