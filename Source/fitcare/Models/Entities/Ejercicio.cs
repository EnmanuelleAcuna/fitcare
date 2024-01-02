using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace fitcare.Models.Entities;

[Table("EJERCICIOS", Schema = "fitcare")]
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

// public partial class Ejercicios
// {
// 	public Ejercicios()
// 	{
// 		DetalleRutina = new HashSet<DetalleRutina>();
// 		GruposMuscularesEjercicio = new HashSet<GruposMuscularesEjercicio>();
// 	}

// 	public Guid Id { get; set; }
// 	public Guid IdTipoEjercicio { get; set; }
// 	public string Codigo { get; set; }
// 	public string Nombre { get; set; }
// 	public bool Estado { get; set; }
// 	public DateTime DateCreated { get; set; }
// 	public string CreatedBy { get; set; }
// 	public DateTime? DateUpdated { get; set; }
// 	public string UpdatedBy { get; set; }

// 	public virtual TiposEjercicio IdTipoEjercicioNavigation { get; set; }
// 	public virtual ICollection<DetalleRutina> DetalleRutina { get; set; }
// 	public virtual ICollection<GruposMuscularesEjercicio> GruposMuscularesEjercicio { get; set; }
// }

[Table("TIPOS_EJERCICIO", Schema = "fitcare")]
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
