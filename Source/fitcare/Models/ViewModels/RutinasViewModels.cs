using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models.DataAccess;
using fitcare.Models.Entities;
using fitcare.Models.Identity;
using Microsoft.Extensions.Configuration;

namespace fitcare.Models.ViewModels;

public class RutinaViewModel
{
	public RutinaViewModel(Rutina rutina)
	{
		Id = rutina.Id.ToString();

		Instructor = rutina.Instructor.FullName;
		Cliente = rutina.Cliente.FullName;

		FechaRealizacion = rutina.FechaRealizacion;
		FechaInicio = rutina.FechaInicio;
		FechaFin = rutina.FechaFin;
		Objetivos = rutina.Objetivo;

		CantidadEjercicios = rutina.Ejercicios?.Count;
	}

	public string Id { get; set; }

	[Display(Name = "Instructor")]
	public string Instructor { get; set; }

	[Display(Name = "Cliente")]
	public string Cliente { get; set; }

	[Display(Name = "Fecha de realización")]
	public DateTime FechaRealizacion { get; set; }

	[Display(Name = "Fecha de inicio")]
	public DateTime FechaInicio { get; set; }

	[Display(Name = "Fecha de finalización")]
	public DateTime FechaFin { get; set; }

	public string Objetivos { get; set; }

	public int? CantidadEjercicios { get; set; } = 0;
}

public class AgregarRutinaViewModel
{
	[Required(ErrorMessage = "El instructor es requerido.")]
	public string IdInstructor { get; set; }

	[Required(ErrorMessage = "El cliente es requerido.")]
	public string IdCliente { get; set; }

	[Display(Name = "Realización")]
	[Required(ErrorMessage = "La fecha de realización es requerida.")]
	[DataType(DataType.Date)]
	public DateTime FechaRealizacion { get; set; } = DateTime.Now;

	[Display(Name = "Inicio")]
	[Required(ErrorMessage = "La fecha de inicio es requerida")]
	[DataType(DataType.Date)]
	public DateTime FechaInicio { get; set; } = DateTime.Now;

	[Display(Name = "Finalización")]
	[Required(ErrorMessage = "La fecha de finalización es requerida")]
	[DataType(DataType.Date)]
	public DateTime FechaFin { get; set; } = DateTime.Now.AddMonths(1);

	[Display(Name = "Objetivo")]
	public string Objetivo { get; set; }

	public Rutina Entidad(ApplicationUser instructor, ApplicationUser cliente)
	{
		// IList<EjercicioRutina> ejercicios = (IList<EjercicioRutina>)Ejercicios.Select(async x => await x.Entidad()).ToList();

		// IList<MedidaRutina> medidas = (IList<MedidaRutina>)Medidas.Select(async x => await x.Entidad()).ToList();

		Rutina rutina = new(Guid.NewGuid(), FechaRealizacion, FechaInicio, FechaFin, Objetivo, instructor, cliente, null, null, null);
		return rutina;
	}
}

public class EjercicioRutinaViewModel
{
	public EjercicioRutinaViewModel(EjercicioRutina ejercicioRutina)
	{
		IdEjercicio = ejercicioRutina.Ejercicio.Id.ToString();
		Nombre = ejercicioRutina.Ejercicio.Nombre;
		Series = ejercicioRutina.Series;
		Repeticiones = ejercicioRutina.Repeticiones;
		MinutosDescanso = ejercicioRutina.MinutosDescanso;
	}

	public string IdEjercicio { get; set; }
	public string Nombre { get; set; }
	public int Series { get; set; }
	public int Repeticiones { get; set; }
	public int MinutosDescanso { get; set; }

	// public async Task<EjercicioRutina> Entidad()
	// {
	// 	Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(IdEjercicio));
	// 	EjercicioRutina ejercicioRutina = new(Series, Repeticiones, MinutosDescanso, ejercicio);
	// 	return ejercicioRutina;
	// }
}

public class MedidaRutinaViewModel
{
	public MedidaRutinaViewModel(MedidaRutina medidaRutina)
	{
		IdTipoMedida = medidaRutina.TipoMedida.Id.ToString();
		Nombre = medidaRutina.TipoMedida.Nombre;
		Valor = medidaRutina.Valor;
		Comentario = medidaRutina.Comentario;
	}

	public string IdTipoMedida { get; set; }
	public string Nombre { get; set; }
	public string Valor { get; set; }
	public string Comentario { get; set; }

	// public async Task<MedidaRutina> Entidad()
	// {
	// 	TipoMedida tipoMedida = await _repoTiposMedida.ReadByIdAsync(Factory.SetGuid(IdTipoMedida));
	// 	MedidaRutina medidaRutina = new(Valor, Comentario, tipoMedida);
	// 	return medidaRutina;
	// }
}

public class ReporteRutinaDetalladoViewModel
{
	public ReporteRutinaDetalladoViewModel(ReporteRutinaDetallado rutina, IConfiguration configuration)
	{
		if (rutina is null)
			throw new ArgumentNullException(paramName: nameof(rutina), message: configuration["AppSettings:ModeloNulo"]);

		NombreInstructor = rutina.NombreInstructor;
		NombreCliente = rutina.NombreCliente;
		FechaRegistro = rutina.FechaRegistro.ToString("dd/MM/yyyy");
		FechaInicio = rutina.FechaInicio.ToString("dd/MM/yyyy");
		FechaFin = rutina.FechaFin.ToString("dd/MM/yyyy");
		Objetivo = rutina.Objetivo;
		DiasRutina = rutina.DiasRutina;
		CantidadEjerciciosRegistrados = rutina.CantidadEjerciciosRegistrados;

		// Ejercicios = rutina.Ejercicios.Select(de => new DetalleEjercicioRutinaViewModel(de)).ToList();
		// Medidas = rutina.Medidas.Select(mr => new DetalleMedidaViewModel(mr)).ToList();
	}

	[Display(Name = "Instructor")]
	public string NombreInstructor { get; set; }

	[Display(Name = "Cliente")]
	public string NombreCliente { get; set; }

	[Display(Name = "Registro")]
	public string FechaRegistro { get; set; }

	[Display(Name = "Inicio")]
	public string FechaInicio { get; set; }

	[Display(Name = "Finalización")]
	public string FechaFin { get; set; }

	public string Objetivo { get; set; }

	[Display(Name = "Días")]
	public int DiasRutina { get; set; }

	[Display(Name = "Ejercicios")]
	public int CantidadEjerciciosRegistrados { get; set; }

	// public IEnumerable<DetalleEjercicioRutinaViewModel> Ejercicios { get; set; }

	// public IEnumerable<DetalleMedidaViewModel> Medidas { get; set; }
}

public class ReporteRutinaResumidoViewModel
{
	public ReporteRutinaResumidoViewModel(ReporteRutinaResumido rutina, IConfiguration configuration)
	{
		if (rutina is null)
			throw new ArgumentNullException(paramName: nameof(rutina), message: configuration["AppSettings:ModeloNulo"]);

		NombreInstructor = rutina.NombreInstructor;
		NombreCliente = rutina.NombreCliente;
		FechaRegistro = rutina.FechaRegistro.ToString("dd/MM/yyyy");
		FechaInicio = rutina.FechaInicio.ToString("dd/MM/yyyy");
		FechaFin = rutina.FechaFin.ToString("dd/MM/yyyy");
		Objetivo = rutina.Objetivo;
		DiasRutina = rutina.DiasRutina;
		CantidadEjerciciosRegistrados = rutina.CantidadEjerciciosRegistrados;
	}

	[Display(Name = "Instructor")]
	public string NombreInstructor { get; set; }

	[Display(Name = "Cliente")]
	public string NombreCliente { get; set; }

	[Display(Name = "Registro de rutina")]
	public string FechaRegistro { get; set; }

	[Display(Name = "Inicio")]
	public string FechaInicio { get; set; }

	[Display(Name = "Finalización")]
	public string FechaFin { get; set; }

	public string Objetivo { get; set; }

	[Display(Name = "Cantidad de días")]
	public int DiasRutina { get; set; }

	[Display(Name = "Ejercicios registrados")]
	public int CantidadEjerciciosRegistrados { get; set; }
}
