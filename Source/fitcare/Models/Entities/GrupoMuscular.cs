using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace fitcare.Models.Entities;

[Table("GruposMusculares", Schema = "fitcare")]
public class GrupoMuscular : Base
{
	public GrupoMuscular()
	{
		Ejercicios = new HashSet<Ejercicio>();	
	}
	
	public GrupoMuscular(Guid id, string nombre, string descripcion, bool estado)
	{
		Id = id;
		Nombre = nombre;
		Descripcion = descripcion;
		Estado = estado;
		
		Ejercicios = new HashSet<Ejercicio>();
	}

	public Guid Id { get; set; }
	public string Nombre { get; set; }
	public string Descripcion { get; set; }
	public bool Estado { get; set; }
	
	public ICollection<Ejercicio> Ejercicios { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
