using System;
using System.ComponentModel.DataAnnotations;
using fitcare.Models.Entities;

namespace fitcare.Models.ViewModels;

public class PlanMembresiaViewModel : BaseViewModel
{
	public PlanMembresiaViewModel(PlanMembresia modelo) : base(modelo)
	{
		Id = modelo.Id.ToString();
		Nombre = modelo.Nombre;
		Dias = modelo.Dias;
		Costo = modelo.Costo;
		Estado = modelo.Estado ? "Activo" : "Inactivo";
		EstadoBool = modelo.Estado;
	}

	public string Id { get; set; }

	[Display(Name = "Nombre del plan")]
	public string Nombre { get; set; }

	[Display(Name = "Días")]
	public int Dias { get; set; }

	[Display(Name = "Costo")]
	public decimal Costo { get; set; }

	public string Estado { get; set; }
	public bool EstadoBool { get; set; }
}

public class AgregarPlanMembresiaViewModel
{
	[Display(Name = "Nombre del plan")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Días de duración")]
	[Required(ErrorMessage = "Los días son requeridos")]
	[Range(1, 3650, ErrorMessage = "Los días deben estar entre 1 y 3650")]
	public int Dias { get; set; }

	[Display(Name = "Costo")]
	[Required(ErrorMessage = "El costo es requerido")]
	[Range(0.01, 999999.99, ErrorMessage = "El costo debe ser mayor a 0")]
	public decimal Costo { get; set; }

	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public PlanMembresia Entidad() => new(Guid.NewGuid(), Nombre, Dias, Costo, Estado);
}

public class EditarPlanMembresiaViewModel : BaseViewModel
{
	public EditarPlanMembresiaViewModel() { }

	public EditarPlanMembresiaViewModel(PlanMembresia modelo) : base(modelo)
	{
		Id = modelo.Id.ToString();
		Nombre = modelo.Nombre;
		Dias = modelo.Dias;
		Costo = modelo.Costo;
		Estado = modelo.Estado;
	}

	[Required(ErrorMessage = "El id es requerido")]
	public string Id { get; set; }

	[Display(Name = "Nombre del plan")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Días de duración")]
	[Required(ErrorMessage = "Los días son requeridos")]
	[Range(1, 3650, ErrorMessage = "Los días deben estar entre 1 y 3650")]
	public int Dias { get; set; }

	[Display(Name = "Costo")]
	[Required(ErrorMessage = "El costo es requerido")]
	[Range(0.01, 999999.99, ErrorMessage = "El costo debe ser mayor a 0")]
	public decimal Costo { get; set; }

	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public PlanMembresia Entidad() => new(new Guid(Id), Nombre, Dias, Costo, Estado);
}
