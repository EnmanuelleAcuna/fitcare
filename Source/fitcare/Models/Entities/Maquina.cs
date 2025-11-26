using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace fitcare.Models.Entities;

[Table("MAQUINAS", Schema = "fitcare")]
public class Maquina : Base
{
	public Maquina() : base()
	{
		Ejercicios = new HashSet<Ejercicio>();
	}

	public Maquina(Guid id, string codigo, string nombre, string codigoActivo, bool activo, DateTime fechaAdquisicion, TipoMaquina tipoMaquina)
	{
		Id = id;
		Nombre = nombre;
		Codigo = codigo;
		CodigoActivo = codigoActivo;
		Estado = activo;
		FechaAdquisicion = fechaAdquisicion;

		IdTipoMaquina = tipoMaquina.Id;
		TipoMaquina = tipoMaquina;

		Ejercicios = new HashSet<Ejercicio>();
	}

	public Maquina(Guid id, string codigo, string nombre, string codigoActivo, bool activo, DateTime fechaAdquisicion, Guid idTipoMaquina)
	{
		Id = id;
		Nombre = nombre;
		Codigo = codigo;
		CodigoActivo = codigoActivo;
		Estado = activo;
		FechaAdquisicion = fechaAdquisicion;

		IdTipoMaquina = idTipoMaquina;

		Ejercicios = new HashSet<Ejercicio>();
	}

	public Maquina(Guid id)
	{
		Id = id;

		Ejercicios = new HashSet<Ejercicio>();
	}

	public Guid Id { get; private set; }
	public string Codigo { get; set; }
	public string Nombre { get; set; }

	[Column("NumeroActivo")]
	public string CodigoActivo { get; set; }

	public bool Estado { get; set; }
	public DateTime FechaAdquisicion { get; set; }

	[ForeignKey(nameof(TipoMaquina))]
	[Column("IdTipoMaquina")]
	public Guid IdTipoMaquina { get; set; }
	public TipoMaquina TipoMaquina { get; set; }

	public ICollection<Ejercicio> Ejercicios { get; set; } // ⭐ RELACIÓN MUCHOS-A-MUCHOS

	public override string ToString() => JsonSerializer.Serialize(this);
}

[Table("TIPOSMAQUINA", Schema = "fitcare")]
public class TipoMaquina : Base
{
	public TipoMaquina() : base()
	{
		Maquinas = new HashSet<Maquina>();
	}

	public TipoMaquina(Guid id, string nombre, bool estado, string codigo)
	{
		Id = id;
		Nombre = nombre;
		Estado = estado;
		Codigo = codigo;

		Maquinas = new HashSet<Maquina>();
	}

	public TipoMaquina(Guid id)
	{
		Id = id;

		Maquinas = new HashSet<Maquina>();
	}

	public Guid Id { get; set; }
	public string Nombre { get; set; }
	public string Codigo { get; set; }
	public bool Estado { get; set; }

	public ICollection<Maquina> Maquinas { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
