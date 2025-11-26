using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace fitcare.Models.Entities;

[Table("Ejercicios", Schema = "fitcare")]
public class Ejercicio : Base
{
	public Guid Id { get; set; }
	public string Codigo { get; set; }
	public string Nombre { get; set; }
	public bool Estado { get; set; }

	[ForeignKey(nameof(TipoEjercicio))]
	[Column("IdTipoEjercicio")]
	public Guid IdTipoEjercicio { get; set; }
	public TipoEjercicio TipoEjercicio { get; set; }

	public ICollection<GrupoMuscular> GruposMusculares { get; set; } // ⭐ RELACIÓN MUCHOS-A-MUCHOS
	public ICollection<Maquina> Maquinas { get; set; } // ⭐ RELACIÓN MUCHOS-A-MUCHOS

	public Ejercicio()
	{
		GruposMusculares = new HashSet<GrupoMuscular>();
		Maquinas = new HashSet<Maquina>();
	}
	
	public Ejercicio(Guid id, string codigo, string nombre, bool estado, Guid idTipoEjercicio)
	{
		Id = id;
		Codigo = codigo;
		Nombre = nombre;
		Estado = estado;
		IdTipoEjercicio = idTipoEjercicio;

		GruposMusculares = new HashSet<GrupoMuscular>();
		Maquinas = new HashSet<Maquina>();
	}
	
	public Ejercicio(Guid id, string codigo, string nombre, bool estado, TipoEjercicio tipoEjercicio)
	{
		Id = id;
		Codigo = codigo;
		Nombre = nombre;
		Estado = estado;
		TipoEjercicio = tipoEjercicio;
		IdTipoEjercicio = tipoEjercicio.Id;

		GruposMusculares = new HashSet<GrupoMuscular>();
		Maquinas = new HashSet<Maquina>();
	}
	
	public override string ToString() => JsonSerializer.Serialize(this);
}

[Table("TIPOSEJERCICIO", Schema = "fitcare")]
public class TipoEjercicio : Base
{
	public TipoEjercicio() : base()
	{
		Ejercicios = new HashSet<Ejercicio>();
	}

	public TipoEjercicio(Guid id, string codigo, string nombre, bool estado)
	{
		Id = id;
		Codigo = codigo;
		Nombre = nombre;
		Estado = estado;

		Ejercicios = new HashSet<Ejercicio>();
	}

	public TipoEjercicio(Guid id)
	{
		Id = id;

		Ejercicios = new HashSet<Ejercicio>();
	}

	public Guid Id { get; set; }
	public string Codigo { get; set; }
	public string Nombre { get; set; }
	public bool Estado { get; set; }

	public virtual ICollection<Ejercicio> Ejercicios { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
