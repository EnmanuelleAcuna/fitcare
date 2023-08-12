using System;
using System.ComponentModel.DataAnnotations;
using fitcare.Models.Entities;
using fitcare.Models.Extras;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace fitcare.Models.ViewModels;

public class ProvinciaViewModel : BaseViewModel
{
	public ProvinciaViewModel(Provincia provincia) : base(provincia)
	{
		Id = provincia.Id.ToString();
		Nombre = provincia.Nombre;
		Estado = provincia.Estado ? "Activo" : "Inactivo";
	}

	public string Id { get; set; }
	public string Nombre { get; set; }
	public string Estado { get; set; }
}

public class AgregarProvinciaViewModel
{
	[Display(Name = "Nombre de la provincia")]
	[Required(ErrorMessage = "El nombre es requerido.")]
	[StringLength(50, ErrorMessage = "El nombre no puede exceder los {0} caracteres.")]
	public string Nombre { get; set; }

	public bool Activo { get; set; }

	public Provincia Entidad(string createdBy)
	{
		var provincia = new Provincia(Factory.NewGUID((Guid.NewGuid())), Nombre, Activo);
		return provincia;
	}
}

public class EditarProvinciaViewModel : Provincia { }

public class EliminarProvinciaViewModel : Provincia { }

public class CantonViewModel : Canton { }

public class AgregarCantonViewModel
{
	[Required(ErrorMessage = "El id es requerido.")]
	public Guid Id { get; private set; }

	[Display(Name = "Nombre del cantón")]
	[Required(ErrorMessage = "El nombre es requerido.")]
	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
	public string Nombre { get; private set; }

	[Display(Name = "Activo")]
	public bool Estado { get; private set; }

	[Display(Name = "Código INEC")]
	[Required(ErrorMessage = "El código INEC es requerido.")]
	public int IdINEC { get; private set; }

	public Provincia Provincia { get; private set; }
}

public class EditarCantonViewModel : Canton { }

public class EliminarCantonViewModel : Canton { }

public class DistritoViewModel : Distrito { }

public class AgregarDistritoViewModel
{
	[Required(ErrorMessage = "El id es requerido")]
	public Guid Id { get; private set; }

	[Display(Name = "Nombre del distrito")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
	public string Nombre { get; private set; }

	[Display(Name = "Activo")]
	public bool Estado { get; private set; }

	[Display(Name = "Código INEC")]
	[Required(ErrorMessage = "El código INEC es requerido")]
	public int IdINEC { get; private set; }

	public Canton Canton { get; private set; }
}

public class EditarDistritoViewModel : Distrito { }

public class EliminarDistritoViewModel : Distrito { }

public class CantonesDistritosSelectListItem : SelectListItem
{
	public string IdPadre { get; set; }
}
