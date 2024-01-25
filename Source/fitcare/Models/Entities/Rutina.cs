using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using fitcare.Models.Extras;
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

	public Rutina(Guid id, DateTime fechaRealizacion, DateTime fechaInicio, DateTime fechaFin, string objetivo, ApplicationUser instructor, ApplicationUser cliente, IList<MedidaRutina> medidas, IList<EjercicioRutina> ejercicios, IList<GrupoMuscularRutina> gruposMusculares)
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

	public ICollection<MedidaRutina> Medidas { get; set; }
	public ICollection<EjercicioRutina> Ejercicios { get; set; }
	public ICollection<GrupoMuscularRutina> GruposMusculares { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}

public class MedidaRutina
{
	public MedidaRutina(string valor, string comentario, TipoMedida tipoMedida)
	{
		Valor = valor;
		Comentario = comentario;
		TipoMedida = tipoMedida;
	}

	public string Valor { get; set; }
	public string Comentario { get; set; }
	public TipoMedida TipoMedida { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
public partial class DetalleMedidas
{
	public Guid Id { get; set; }
	public Guid IdRutina { get; set; }
	public Guid IdTipoMedida { get; set; }
	public string ValorMedida { get; set; }
	public string Comentario { get; set; }
	public DateTime DateCreated { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? DateUpdated { get; set; }
	public string UpdatedBy { get; set; }

	// public virtual Rutinas IdRutinaNavigation { get; set; }
	// public virtual TiposMedida IdTipoMedidaNavigation { get; set; }
}

public class EjercicioRutina
{
	public EjercicioRutina(int series, int repeticiones, int minutosDescanso, Ejercicio ejercicio)
	{
		Series = series;
		Repeticiones = repeticiones;
		MinutosDescanso = minutosDescanso;
		Ejercicio = ejercicio;
	}

	public int Series { get; set; }
	public int Repeticiones { get; set; }
	public int MinutosDescanso { get; set; }
	public Ejercicio Ejercicio { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
public partial class DetalleRutina
{
	public Guid Id { get; set; }
	public Guid IdRutina { get; set; }
	public Guid IdEjercicio { get; set; }
	public Guid? IdMaquina { get; set; }
	public int NumSeries { get; set; }
	public int NumRepeticiones { get; set; }
	public int MinutosDescanso { get; set; }
	public DateTime DateCreated { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? DateUpdated { get; set; }
	public string UpdatedBy { get; set; }

	// public virtual Ejercicios IdEjercicioNavigation { get; set; }
	// public virtual Maquina IdMaquinaNavigation { get; set; }
	// public virtual Rutinas IdRutinaNavigation { get; set; }
}

public class GrupoMuscularRutina
{
	public GrupoMuscularRutina(int series, int repeticiones, int minutosDescanso, GrupoMuscular grupoMuscular)
	{
		Series = series;
		Repeticiones = repeticiones;
		MinutosDescanso = minutosDescanso;
		GrupoMuscular = grupoMuscular;
	}

	public int Series { get; set; }
	public int Repeticiones { get; set; }
	public int MinutosDescanso { get; set; }
	public GrupoMuscular GrupoMuscular { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}
