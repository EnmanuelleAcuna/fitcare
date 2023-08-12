// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.Linq;
// using System.Threading.Tasks;
// using fitcare.Models.Contracts;
// using fitcare.Models.DataAccess;
// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using Microsoft.Extensions.Configuration;

// namespace fitcare.Models.ViewModels;

// public class RutinaViewModel
// {
// 	public string Id { get; set; }

// 	[Display(Name = "Instructor")]
// 	[Required(ErrorMessage = "El instructor es requerido")]
// 	public string Instructor { get; set; }

// 	[Display(Name = "Cliente")]
// 	[Required(ErrorMessage = "El cliente es requerido")]
// 	public string Cliente { get; set; }

// 	[Display(Name = "Fecha")]
// 	public DateTime FechaRealizacion { get; set; }

// 	public IList<MedidaRutinaViewModel> Medidas { get; set; }
// 	public IList<EjercicioRutinaViewModel> Ejercicios { get; set; }

// 	public IDataCRUDBase<TipoMedida> _repoTiposMedida;
// 	public IDataCRUDBase<Ejercicio> _repoEjercicios;
// 	public IDataCRUDBase<Instructor> _repoInstructores;
// 	public IDataCRUDBase<Cliente> _repoClientes;

// 	public void SetDependencies(IDataCRUDBase<TipoMedida> repoTiposMedida, IDataCRUDBase<Ejercicio> repoEjercicios, IDataCRUDBase<Instructor> repoInstructores, IDataCRUDBase<Cliente> repoClientes)
// 	{
// 		_repoTiposMedida = repoTiposMedida;
// 		_repoEjercicios = repoEjercicios;
// 		_repoInstructores = repoInstructores;
// 		_repoClientes = repoClientes;
// 	}
// }

// public class InicioRutinasViewModel : RutinaViewModel
// {
// 	public InicioRutinasViewModel(Rutina rutina)
// 	{
// 		Validators.ValidateRutina(rutina);
// 		Validators.ValidateInstructor(rutina.Instructor);
// 		Validators.ValidateCliente(rutina.Cliente);

// 		Id = rutina.Id.ToString();
// 		//Instructor = string.Format("{0} {1} {2}", rutina.Instructor.Nombre, rutina.Instructor.PrimerApellido, rutina.Instructor.SegundoApellido);
// 		//Cliente = string.Format("{0} {1} {2}", rutina.Cliente.Nombre, rutina.Cliente.PrimerApellido, rutina.Cliente.SegundoApellido);
// 		FechaRealizacion = rutina.FechaRealizacion;
// 	}
// }

// public class NuevaRutinaViewModel : RutinaViewModel
// {
// 	[Display(Name = "Inicio")]
// 	[Required(ErrorMessage = "La fecha de inicio es requerida")]
// 	[DataType(DataType.Date)]
// 	public DateTime FechaInicio { get; set; } = DateTime.Now;

// 	[Display(Name = "Finalización")]
// 	[Required(ErrorMessage = "La fecha de finalización es requerida")]
// 	[DataType(DataType.Date)]
// 	public DateTime FechaFin { get; set; } = DateTime.Now;

// 	[Display(Name = "Objetivo")]
// 	public string Objetivo { get; set; }

// 	public async Task<Rutina> Entidad()
// 	{
// 		Instructor instructor = await _repoInstructores.ReadByIdAsync(Factory.SetGuid(Instructor));
// 		Cliente cliente = await _repoClientes.ReadByIdAsync(Factory.SetGuid(Cliente));
// 		IList<MedidaRutina> medidas = (IList<MedidaRutina>)Medidas.Select(async x => await x.Entidad()).ToList();
// 		IList<EjercicioRutina> ejercicios = (IList<EjercicioRutina>)Ejercicios.Select(async x => await x.Entidad()).ToList();
// 		Rutina rutina = new(Guid.NewGuid(), DateTime.Now, FechaInicio, FechaFin, Objetivo, instructor, cliente, medidas, ejercicios);
// 		return rutina;
// 	}
// }

// public class EditarRutinaViewModel : RutinaViewModel
// {
// 	public EditarRutinaViewModel(Rutina rutina)
// 	{
// 		Validators.ValidateRutina(rutina);
// 		Validators.ValidateInstructor(rutina.Instructor);
// 		Validators.ValidateCliente(rutina.Cliente);
// 		Validators.ValidateList(rutina.Medidas, nameof(MedidaRutina));
// 		Validators.ValidateList(rutina.Ejercicios, nameof(EjercicioRutina));

// 		Id = rutina.Id.ToString();
// 		Instructor = rutina.Instructor.Id.ToString();
// 		Cliente = rutina.Cliente.Id;
// 		FechaRealizacion = rutina.FechaRealizacion;
// 		FechaInicio = rutina.FechaInicio;
// 		FechaFin = rutina.FechaFin;
// 		Objetivo = rutina.Objetivo;

// 		Medidas = rutina.Medidas.Select(m => new MedidaRutinaViewModel(m)).ToList();
// 		Ejercicios = rutina.Ejercicios.Select(e => new EjercicioRutinaViewModel(e)).ToList();
// 	}

// 	[Display(Name = "Inicio")]
// 	[Required(ErrorMessage = "La fecha de inicio es requerida")]
// 	[DataType(DataType.Date)]
// 	public DateTime FechaInicio { get; set; } = DateTime.Now;

// 	[Display(Name = "Finalización")]
// 	[Required(ErrorMessage = "La fecha de finalización es requerida")]
// 	[DataType(DataType.Date)]
// 	public DateTime FechaFin { get; set; } = DateTime.Now;

// 	[Display(Name = "Objetivo")]
// 	public string Objetivo { get; set; }

// 	public async Task<Rutina> Entidad()
// 	{
// 		Instructor instructor = await _repoInstructores.ReadByIdAsync(Factory.SetGuid(Instructor));
// 		Cliente cliente = await _repoClientes.ReadByIdAsync(Factory.SetGuid(Cliente));
// 		IList<MedidaRutina> medidas = (IList<MedidaRutina>)Medidas.Select(async x => await x.Entidad()).ToList();
// 		IList<EjercicioRutina> ejercicios = (IList<EjercicioRutina>)Ejercicios.Select(async x => await x.Entidad()).ToList();
// 		Rutina rutina = new(Factory.SetGuid(Id), FechaRealizacion, FechaInicio, FechaFin, Objetivo, instructor, cliente, medidas, ejercicios);
// 		return rutina;
// 	}
// }

// public class EjercicioRutinaViewModel
// {
// 	public EjercicioRutinaViewModel(EjercicioRutina ejercicioRutina)
// 	{
// 		Validators.ValidateEjercicioRutina(ejercicioRutina);
// 		Validators.ValidateEjercicio(ejercicioRutina.Ejercicio);
// 		Validators.ValidateTipoEjercicio(ejercicioRutina.Ejercicio.TipoEjercicio);

// 		IdEjercicio = ejercicioRutina.Ejercicio.Id.ToString();
// 		Nombre = ejercicioRutina.Ejercicio.Nombre;
// 		Series = ejercicioRutina.Series;
// 		Repeticiones = ejercicioRutina.Repeticiones;
// 		MinutosDescanso = ejercicioRutina.MinutosDescanso;
// 	}

// 	public string IdEjercicio { get; set; }
// 	public string Nombre { get; set; }
// 	public int Series { get; set; }
// 	public int Repeticiones { get; set; }
// 	public int MinutosDescanso { get; set; }

// 	public IDataCRUDBase<Ejercicio> _repoEjercicios;

// 	public void SetDependencies(IDataCRUDBase<Ejercicio> repoEjercicios)
// 	{
// 		_repoEjercicios = repoEjercicios;
// 	}

// 	public async Task<EjercicioRutina> Entidad()
// 	{
// 		Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(IdEjercicio));
// 		EjercicioRutina ejercicioRutina = new(Series, Repeticiones, MinutosDescanso, ejercicio);
// 		return ejercicioRutina;
// 	}
// }

// public class MedidaRutinaViewModel
// {
// 	public MedidaRutinaViewModel(MedidaRutina medidaRutina)
// 	{
// 		Validators.ValidateMedidaRutina(medidaRutina);
// 		Validators.ValidateTipoMedida(medidaRutina.TipoMedida);

// 		IdTipoMedida = medidaRutina.TipoMedida.Id.ToString();
// 		Nombre = medidaRutina.TipoMedida.Nombre;
// 		Valor = medidaRutina.Valor;
// 		Comentario = medidaRutina.Comentario;
// 	}

// 	public string IdTipoMedida { get; set; }
// 	public string Nombre { get; set; }
// 	public string Valor { get; set; }
// 	public string Comentario { get; set; }

// 	public IDataCRUDBase<TipoMedida> _repoTiposMedida;

// 	public void SetDependencies(IDataCRUDBase<TipoMedida> repoTiposMedida)
// 	{
// 		_repoTiposMedida = repoTiposMedida;
// 	}

// 	public async Task<MedidaRutina> Entidad()
// 	{
// 		TipoMedida tipoMedida = await _repoTiposMedida.ReadByIdAsync(Factory.SetGuid(IdTipoMedida));
// 		MedidaRutina medidaRutina = new(Valor, Comentario, tipoMedida);
// 		return medidaRutina;
// 	}
// }

// public class ReporteRutinaDetalladoViewModel
// {
// 	public ReporteRutinaDetalladoViewModel(ReporteRutinaDetallado rutina, IConfiguration configuration)
// 	{
// 		if (rutina is null)
// 			throw new ArgumentNullException(paramName: nameof(rutina), message: configuration["AppSettings:ModeloNulo"]);

// 		NombreInstructor = rutina.NombreInstructor;
// 		NombreCliente = rutina.NombreCliente;
// 		FechaRegistro = rutina.FechaRegistro.ToString("dd/MM/yyyy");
// 		FechaInicio = rutina.FechaInicio.ToString("dd/MM/yyyy");
// 		FechaFin = rutina.FechaFin.ToString("dd/MM/yyyy");
// 		Objetivo = rutina.Objetivo;
// 		DiasRutina = rutina.DiasRutina;
// 		CantidadEjerciciosRegistrados = rutina.CantidadEjerciciosRegistrados;

// 		// Ejercicios = rutina.Ejercicios.Select(de => new DetalleEjercicioRutinaViewModel(de)).ToList();
// 		// Medidas = rutina.Medidas.Select(mr => new DetalleMedidaViewModel(mr)).ToList();
// 	}

// 	[Display(Name = "Instructor")]
// 	public string NombreInstructor { get; set; }

// 	[Display(Name = "Cliente")]
// 	public string NombreCliente { get; set; }

// 	[Display(Name = "Registro")]
// 	public string FechaRegistro { get; set; }

// 	[Display(Name = "Inicio")]
// 	public string FechaInicio { get; set; }

// 	[Display(Name = "Finalización")]
// 	public string FechaFin { get; set; }

// 	public string Objetivo { get; set; }

// 	[Display(Name = "Días")]
// 	public int DiasRutina { get; set; }

// 	[Display(Name = "Ejercicios")]
// 	public int CantidadEjerciciosRegistrados { get; set; }

// 	// public IEnumerable<DetalleEjercicioRutinaViewModel> Ejercicios { get; set; }

// 	// public IEnumerable<DetalleMedidaViewModel> Medidas { get; set; }
// }

// public class ReporteRutinaResumidoViewModel
// {
// 	public ReporteRutinaResumidoViewModel(ReporteRutinaResumido rutina, IConfiguration configuration)
// 	{
// 		if (rutina is null)
// 			throw new ArgumentNullException(paramName: nameof(rutina), message: configuration["AppSettings:ModeloNulo"]);

// 		NombreInstructor = rutina.NombreInstructor;
// 		NombreCliente = rutina.NombreCliente;
// 		FechaRegistro = rutina.FechaRegistro.ToString("dd/MM/yyyy");
// 		FechaInicio = rutina.FechaInicio.ToString("dd/MM/yyyy");
// 		FechaFin = rutina.FechaFin.ToString("dd/MM/yyyy");
// 		Objetivo = rutina.Objetivo;
// 		DiasRutina = rutina.DiasRutina;
// 		CantidadEjerciciosRegistrados = rutina.CantidadEjerciciosRegistrados;
// 	}

// 	[Display(Name = "Instructor")]
// 	public string NombreInstructor { get; set; }

// 	[Display(Name = "Cliente")]
// 	public string NombreCliente { get; set; }

// 	[Display(Name = "Registro de rutina")]
// 	public string FechaRegistro { get; set; }

// 	[Display(Name = "Inicio")]
// 	public string FechaInicio { get; set; }

// 	[Display(Name = "Finalización")]
// 	public string FechaFin { get; set; }

// 	public string Objetivo { get; set; }

// 	[Display(Name = "Cantidad de días")]
// 	public int DiasRutina { get; set; }

// 	[Display(Name = "Ejercicios registrados")]
// 	public int CantidadEjerciciosRegistrados { get; set; }
// }
