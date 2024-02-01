using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using fitcare.Models.Identity;

namespace fitcare.Models.Entities;

[Table("RUTINAS", Schema = "fitcare")]
public class Rutina : Base
{
	public Rutina() : base()
	{
		Medidas = new HashSet<MedidaRutina>();
		Ejercicios = new HashSet<EjercicioRutina>();
		GruposMusculares = new HashSet<GrupoMuscularRutina>();
	}

	public Rutina(Guid id, DateTime fechaRealizacion, DateTime fechaInicio, DateTime fechaFin, string objetivo, ApplicationUser instructor, ApplicationUser cliente, IList<EjercicioRutina> ejercicios, IList<MedidaRutina> medidas, IList<GrupoMuscularRutina> gruposMusculares)
	{
		Id = id;

		FechaRealizacion = fechaRealizacion;
		FechaInicio = fechaInicio;
		FechaFin = fechaFin;
		Objetivo = objetivo;

		IdInstructor = instructor.Id;
		Instructor = instructor;

		IdCliente = cliente.Id;
		Cliente = cliente;

		Medidas = medidas;
		Ejercicios = ejercicios;
		GruposMusculares = gruposMusculares;
	}

	public Rutina(Guid id, DateTime fechaRealizacion, DateTime fechaInicio, DateTime fechaFin, string objetivo, string idInstructor, string idCliente)
	{
		Id = id;
		FechaRealizacion = fechaRealizacion;
		FechaInicio = fechaInicio;
		FechaFin = fechaFin;
		Objetivo = objetivo;

		IdInstructor = idInstructor;
		IdCliente = idCliente;

		Medidas = new HashSet<MedidaRutina>();
		Ejercicios = new HashSet<EjercicioRutina>();
		GruposMusculares = new HashSet<GrupoMuscularRutina>();
	}

	public Rutina(Guid id)
	{
		Id = id;

		Medidas = new HashSet<MedidaRutina>();
		Ejercicios = new HashSet<EjercicioRutina>();
		GruposMusculares = new HashSet<GrupoMuscularRutina>();
	}

	[Key]
	public Guid Id { get; set; }

	public DateTime FechaRealizacion { get; set; }
	public DateTime FechaInicio { get; set; }
	public DateTime FechaFin { get; set; }
	public string Objetivo { get; set; }

	[ForeignKey(nameof(ApplicationUser))]
	public string IdInstructor { get; set; }
	public ApplicationUser Instructor { get; set; }

	[ForeignKey(nameof(ApplicationUser))]
	public string IdCliente { get; set; }
	public ApplicationUser Cliente { get; set; }

	public ICollection<EjercicioRutina> Ejercicios { get; set; }
	public ICollection<MedidaRutina> Medidas { get; set; }
	public ICollection<GrupoMuscularRutina> GruposMusculares { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}

[Table("DETALLE_RUTINA", Schema = "fitcare")]
public class EjercicioRutina : Base
{
	public EjercicioRutina() : base() { }

	[Key]
	public Guid Id { get; set; }

	public Guid IdRutina { get; set; }

	[Column("Num_Series")]
	public int Series { get; set; }

	[Column("Num_Repeticiones")]
	public int Repeticiones { get; set; }

	[Column("Minutos_Descanso")]
	public int MinutosDescanso { get; set; }

	public Guid IdEjercicio { get; set; }
	public Ejercicio Ejercicio { get; set; }

	public Guid? IdMaquina { get; set; }
	public Maquina Maquina { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}

[Table("DETALLE_MEDIDAS", Schema = "fitcare")]
public class MedidaRutina : Base
{
	public MedidaRutina() : base() { }

	[Key]
	public Guid Id { get; set; }

	public Guid IdRutina { get; set; }

	[Column("ValorMedida")]
	public string Valor { get; set; }

	public string Comentario { get; set; }

	public Guid IdTipoMedida { get; set; }
	public TipoMedida TipoMedida { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}

[Table("DETALLE_GRUPOS_MUSCULARES", Schema = "fitcare")]
public class GrupoMuscularRutina : Base
{
	public GrupoMuscularRutina() : base() { }

	[Key]
	public Guid Id { get; set; }

	public Guid IdRutina { get; set; }

	public Guid IdGrupoMuscular { get; set; }
	public GrupoMuscular GrupoMuscular { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
