using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using fitcare.Models.Entities;
using fitcare.Models.Identity;

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

public class DetalleRutinaViewModel
{
	public DetalleRutinaViewModel(Rutina rutina)
	{
		Id = rutina.Id.ToString();

		Instructor = rutina.Instructor.FullName;
		Cliente = rutina.Cliente.FullName;

		FechaRealizacion = rutina.FechaRealizacion;
		FechaInicio = rutina.FechaInicio;
		FechaFin = rutina.FechaFin;
		Objetivos = rutina.Objetivo;

		CantidadEjercicios = rutina.Ejercicios?.Count;

		Ejercicios = rutina.Ejercicios.Select(e => new EjercicioRutinaViewModel(e)).ToList();

		Medidas = rutina.Medidas.Select(m => new MedidaRutinaViewModel(m)).ToList();
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

	public IEnumerable<EjercicioRutinaViewModel> Ejercicios { get; set; }

	public IEnumerable<MedidaRutinaViewModel> Medidas { get; set; }
}

public class AgregarRutinaViewModel
{
	[Required(ErrorMessage = "El instructor es requerido.")]
	[Display(Name = "Instructor")]
	public string IdInstructor { get; set; }

	[Required(ErrorMessage = "El cliente es requerido.")]
	[Display(Name = "Cliente")]
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

	public List<EjercicioRutinaViewModel> DetalleEjercicios { get; set; } = new();
	public List<MedidaRutinaViewModel> DetalleMedidas { get; set; } = new();

	public Rutina Entidad(ApplicationUser instructor, ApplicationUser cliente)
	{
		IList<EjercicioRutina> ejercicios = DetalleEjercicios?.Select(x => x.Entidad()).ToList() ?? new List<EjercicioRutina>();
		IList<MedidaRutina> medidas = DetalleMedidas?.Select(x => x.Entidad()).ToList() ?? new List<MedidaRutina>();

		Rutina rutina = new(Guid.NewGuid(), FechaRealizacion, FechaInicio, FechaFin, Objetivo, instructor, cliente, ejercicios, medidas);
		return rutina;
	}
}

public class EjercicioRutinaViewModel
{
	public EjercicioRutinaViewModel() { }

	public EjercicioRutinaViewModel(EjercicioRutina ejercicioRutina)
	{
		Id = ejercicioRutina.Id.ToString();
		IdRutina = ejercicioRutina.IdRutina.ToString();
		IdEjercicio = ejercicioRutina.Ejercicio.Id.ToString();
		NombreEjercicio = ejercicioRutina.Ejercicio?.Nombre;
		Series = ejercicioRutina.Series;
		Repeticiones = ejercicioRutina.Repeticiones;
		MinutosDescanso = ejercicioRutina.MinutosDescanso;
		GruposMusculares = ejercicioRutina.Ejercicio?.GruposMusculares != null
			? string.Join(", ", ejercicioRutina.Ejercicio.GruposMusculares.Select(g => g.Nombre))
			: string.Empty;
		Maquinas = ejercicioRutina.Ejercicio?.Maquinas != null
			? string.Join(", ", ejercicioRutina.Ejercicio.Maquinas.Select(m => $"{m.Nombre} ({m.TipoMaquina?.Nombre})"))
			: string.Empty;
	}

	public string Id { get; set; }
	public string IdRutina { get; set; }
	public string IdEjercicio { get; set; }
	public string NombreEjercicio { get; set; }
	public int Series { get; set; }
	public int Repeticiones { get; set; }
	public int MinutosDescanso { get; set; }

	[Display(Name = "Grupos musculares")]
	public string GruposMusculares { get; set; }

	[Display(Name = "Maquinas")]
	public string Maquinas { get; set; }

	public EjercicioRutina Entidad()
	{
		Ejercicio ejercicio = new Ejercicio(new Guid(IdEjercicio), string.Empty, string.Empty, false, Guid.Empty);
		EjercicioRutina ejercicioRutina = new(Guid.NewGuid(), Series, Repeticiones, MinutosDescanso, ejercicio);
		return ejercicioRutina;
	}
}

public class MedidaRutinaViewModel
{
	public MedidaRutinaViewModel() { }

	public MedidaRutinaViewModel(MedidaRutina medidaRutina)
	{
		Id = medidaRutina.Id.ToString();
		IdRutina = medidaRutina.IdRutina.ToString();
		IdTipoMedida = medidaRutina.TipoMedida.Id.ToString();
		NombreTipoMedida = medidaRutina.TipoMedida.Nombre;
		Valor = medidaRutina.Valor;
		Comentario = medidaRutina.Comentario;
	}

	public string Id { get; set; }
	public string IdRutina { get; set; }
	public string IdTipoMedida { get; set; }
	public string NombreTipoMedida { get; set; }
	public string Valor { get; set; }
	public string Comentario { get; set; }

	public MedidaRutina Entidad()
	{
		TipoMedida tipoMedida = new TipoMedida(new Guid(IdTipoMedida));
		MedidaRutina medidaRutina = new(Guid.NewGuid(), Valor, Comentario, tipoMedida);
		return medidaRutina;
	}
}

public class RutinaListaViewModel
{
	public RutinaListaViewModel() { }

	public RutinaListaViewModel(Rutina rutina)
	{
		if (rutina is null)
			throw new ArgumentNullException(paramName: nameof(rutina), message: Extras.Messages.MensajeModeloNulo);

		Id = rutina.Id.ToString();
		NombreInstructor = rutina.Instructor?.FullName;
		NombreCliente = rutina.Cliente?.FullName;
		FechaRegistro = rutina.FechaRealizacion.ToString("dd/MM/yyyy");
		FechaInicio = rutina.FechaInicio.ToString("dd/MM/yyyy");
		FechaFin = rutina.FechaFin.ToString("dd/MM/yyyy");
		Objetivo = rutina.Objetivo;
		DiasRutina = (rutina.FechaFin - rutina.FechaInicio).Days;
		CantidadEjerciciosRegistrados = rutina.Ejercicios?.Count ?? 0;
	}

	public string Id { get; init; }

	[Display(Name = "Instructor")]
	public string NombreInstructor { get; init; }

	[Display(Name = "Cliente")]
	public string NombreCliente { get; init; }

	[Display(Name = "Registro de rutina")]
	public string FechaRegistro { get; init; }

	[Display(Name = "Inicio")]
	public string FechaInicio { get; init; }

	[Display(Name = "Finalización")]
	public string FechaFin { get; init; }

	public string Objetivo { get; init; }

	[Display(Name = "Cantidad de días")]
	public int DiasRutina { get; init; }

	[Display(Name = "Ejercicios registrados")]
	public int CantidadEjerciciosRegistrados { get; init; }
}

public class AgregarEjercicioRutinaViewModel
{
	[Required(ErrorMessage = "La rutina es requerida.")]
	public string IdRutina { get; set; }

	[Required(ErrorMessage = "El ejercicio es requerido.")]
	public string IdEjercicio { get; set; }

	[Required(ErrorMessage = "Las series son requeridas.")]
	[Range(1, 100, ErrorMessage = "Las series deben estar entre 1 y 100.")]
	public int Series { get; set; }

	[Required(ErrorMessage = "Las repeticiones son requeridas.")]
	[Range(1, 500, ErrorMessage = "Las repeticiones deben estar entre 1 y 500.")]
	public int Repeticiones { get; set; }

	[Required(ErrorMessage = "Los minutos de descanso son requeridos.")]
	[Range(0, 60, ErrorMessage = "Los minutos de descanso deben estar entre 0 y 60.")]
	public int MinutosDescanso { get; set; }

	public EjercicioRutina Entidad()
	{
		Ejercicio ejercicio = new Ejercicio(new Guid(IdEjercicio), string.Empty, string.Empty, false, Guid.Empty);
		EjercicioRutina ejercicioRutina = new(Guid.NewGuid(), Series, Repeticiones, MinutosDescanso, ejercicio);
		return ejercicioRutina;
	}
}

public class EditarEjercicioRutinaViewModel
{
	[Required(ErrorMessage = "El ID del ejercicio en rutina es requerido.")]
	public string IdEjercicioRutina { get; set; }

	[Required(ErrorMessage = "El ejercicio es requerido.")]
	public string IdEjercicio { get; set; }

	[Required(ErrorMessage = "Las series son requeridas.")]
	[Range(1, 100, ErrorMessage = "Las series deben estar entre 1 y 100.")]
	public int Series { get; set; }

	[Required(ErrorMessage = "Las repeticiones son requeridas.")]
	[Range(1, 500, ErrorMessage = "Las repeticiones deben estar entre 1 y 500.")]
	public int Repeticiones { get; set; }

	[Required(ErrorMessage = "Los minutos de descanso son requeridos.")]
	[Range(0, 60, ErrorMessage = "Los minutos de descanso deben estar entre 0 y 60.")]
	public int MinutosDescanso { get; set; }
}

public class EliminarEjercicioRutinaViewModel
{
	[Required(ErrorMessage = "El ID del ejercicio es requerido.")]
	public string IdEjercicioRutina { get; set; }
}

public class AgregarMedidaRutinaViewModel
{
	[Required(ErrorMessage = "La rutina es requerida.")]
	public string IdRutina { get; set; }

	[Required(ErrorMessage = "El tipo de medida es requerido.")]
	public string IdTipoMedida { get; set; }

	[Required(ErrorMessage = "El valor es requerido.")]
	public string Valor { get; set; }

	public string Comentario { get; set; }

	public MedidaRutina Entidad()
	{
		TipoMedida tipoMedida = new TipoMedida(new Guid(IdTipoMedida));
		MedidaRutina medidaRutina = new(Guid.NewGuid(), Valor, Comentario, tipoMedida);
		return medidaRutina;
	}
}

public class EditarMedidaRutinaViewModel
{
	[Required(ErrorMessage = "El ID de la medida en rutina es requerido.")]
	public string IdMedidaRutina { get; set; }

	[Required(ErrorMessage = "El tipo de medida es requerido.")]
	public string IdTipoMedida { get; set; }

	[Required(ErrorMessage = "El valor es requerido.")]
	public string Valor { get; set; }

	public string Comentario { get; set; }
}

public class EliminarMedidaRutinaViewModel
{
	[Required(ErrorMessage = "El ID de la medida es requerido.")]
	public string IdMedidaRutina { get; set; }
}
