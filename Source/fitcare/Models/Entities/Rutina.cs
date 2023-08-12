using System;
using System.Collections.Generic;
using System.Text.Json;
using fitcare.Models.Extras;
using fitcare.Models.Identity;

namespace fitcare.Models.Entities;

public class Rutina
{
	public Rutina(Guid id, DateTime fechaRealizacion, DateTime fechaInicio, DateTime fechaFin, string objetivo, ApplicationUser instructor, ApplicationUser cliente, IList<MedidaRutina> medidas, IList<EjercicioRutina> ejercicios)
	{
		Id = Factory.NewGUID(id);
		FechaRealizacion = fechaRealizacion;
		FechaInicio = fechaInicio;
		FechaFin = fechaFin;
		Objetivo = objetivo;
		Instructor = instructor;
		Cliente = cliente;
		Medidas = medidas;
		Ejercicios = ejercicios;
	}

	public Guid Id { get; set; }
	public DateTime FechaRealizacion { get; set; }
	public DateTime FechaInicio { get; set; }
	public DateTime FechaFin { get; set; }
	public string Objetivo { get; set; }

	public ApplicationUser Instructor { get; set; }
	public ApplicationUser Cliente { get; set; }

	public IList<MedidaRutina> Medidas { get; set; }
	public IList<EjercicioRutina> Ejercicios { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}

public class MedidaRutina
{
	public MedidaRutina(string valor, string comentario, TipoMedida tipoMedida)
	{
		Validators.ValidateTipoMedida(tipoMedida);

		Valor = valor;
		Comentario = comentario;
		TipoMedida = tipoMedida;
	}

	public string Valor { get; set; }
	public string Comentario { get; set; }
	public TipoMedida TipoMedida { get; set; }

	public override string ToString() => JsonSerializer.Serialize(this);
}

public class EjercicioRutina
{
	public EjercicioRutina(int series, int repeticiones, int minutosDescanso, Ejercicio ejercicio)
	{
		Validators.ValidateEjercicio(ejercicio);

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
