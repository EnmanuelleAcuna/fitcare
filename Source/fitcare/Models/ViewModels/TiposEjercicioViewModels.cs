using System;
using System.ComponentModel.DataAnnotations;
using fitcare.Models.Entities;
using fitcare.Models.Extras;

namespace fitcare.Models.ViewModels;

public class InicioTipoEjercicioViewModel
{
	public InicioTipoEjercicioViewModel(TipoEjercicio tipoEjercicio)
	{
		Validators.ValidateTipoEjercicio(tipoEjercicio);

		IdTipoEjercicio = tipoEjercicio.Id.ToString();
		Codigo = tipoEjercicio.Codigo;
		Nombre = tipoEjercicio.Nombre;
		Estado = tipoEjercicio.Estado ? "Activo" : "Inactivo";
	}

	public string IdTipoEjercicio { get; set; }

	[Display(Name = "Código")]
	public string Codigo { get; set; }

	[Display(Name = "Nombre")]
	public string Nombre { get; set; }

	[Display(Name = "Estado")]
	public string Estado { get; set; }
}

public class NuevoTipoEjercicioViewModel
{
	[Display(Name = "Código")]
	[Required(ErrorMessage = "El código es requerido")]
	public string Codigo { get; set; }

	[Display(Name = "Tipo de ejercicio")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public TipoEjercicio Entidad()
	{
		TipoEjercicio tipoEjercicio = new(Guid.NewGuid(), Codigo, Nombre, Estado);
		return tipoEjercicio;
	}
}

public class EditarTipoEjercicioViewModel
{
	public EditarTipoEjercicioViewModel() { }

	public EditarTipoEjercicioViewModel(TipoEjercicio tipoEjercicio)
	{
		Validators.ValidateTipoEjercicio(tipoEjercicio);

		Id = tipoEjercicio.Id.ToString();
		Codigo = tipoEjercicio.Codigo;
		Nombre = tipoEjercicio.Nombre;
		Estado = tipoEjercicio.Estado;
	}

	public string Id { get; set; }

	[Display(Name = "Código")]
	[Required(ErrorMessage = "El código es requerido")]
	public string Codigo { get; set; }

	[Display(Name = "Nombre del tipo de ejercicio")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public TipoEjercicio Entidad()
	{
		TipoEjercicio tipoEjercicio = new(Factory.NewGUID(Id), Codigo, Nombre, Estado);
		return tipoEjercicio;
	}
}

public class DetalleTipoEjercicioViewModel
{
	public DetalleTipoEjercicioViewModel(TipoEjercicio tipoEjercicio)
	{
		Validators.ValidateTipoEjercicio(tipoEjercicio);

		IdTipoEjercicio = tipoEjercicio.Id.ToString();
		Codigo = tipoEjercicio.Codigo;
		Nombre = tipoEjercicio.Nombre;
		Estado = tipoEjercicio.Estado ? "Activo" : "Inactivo";
	}

	public string IdTipoEjercicio { get; set; }

	[Display(Name = "Código")]
	public string Codigo { get; set; }

	[Display(Name = "Tipo de Ejercicio")]
	public string Nombre { get; set; }

	[Display(Name = "Estado")]
	public string Estado { get; set; }
}
