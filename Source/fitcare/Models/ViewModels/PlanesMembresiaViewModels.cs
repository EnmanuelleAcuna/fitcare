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
		Meses = modelo.Meses;
		Costo = modelo.Costo;
		Estado = modelo.Estado ? "Activo" : "Inactivo";
		EstadoBool = modelo.Estado;
	}

	public string Id { get; set; }

	[Display(Name = "Nombre del plan")]
	public string Nombre { get; set; }

	[Display(Name = "Meses")]
	public int Meses { get; set; }

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

	[Display(Name = "Meses de duración")]
	[Required(ErrorMessage = "Los meses son requeridos")]
	[Range(1, 120, ErrorMessage = "Los meses deben estar entre 1 y 120")]
	public int Meses { get; set; }

	[Display(Name = "Costo")]
	[Required(ErrorMessage = "El costo es requerido")]
	[Range(0.01, 999999.99, ErrorMessage = "El costo debe ser mayor a 0")]
	public decimal Costo { get; set; }

	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public PlanMembresia Entidad() => new(Guid.NewGuid(), Nombre, Meses, Costo, Estado);
}

public class EditarPlanMembresiaViewModel : BaseViewModel
{
	public EditarPlanMembresiaViewModel() { }

	public EditarPlanMembresiaViewModel(PlanMembresia modelo) : base(modelo)
	{
		Id = modelo.Id.ToString();
		Nombre = modelo.Nombre;
		Meses = modelo.Meses;
		Costo = modelo.Costo;
		Estado = modelo.Estado;
	}

	[Required(ErrorMessage = "El id es requerido")]
	public string Id { get; set; }

	[Display(Name = "Nombre del plan")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Meses de duración")]
	[Required(ErrorMessage = "Los meses son requeridos")]
	[Range(1, 120, ErrorMessage = "Los meses deben estar entre 1 y 120")]
	public int Meses { get; set; }

	[Display(Name = "Costo")]
	[Required(ErrorMessage = "El costo es requerido")]
	[Range(0.01, 999999.99, ErrorMessage = "El costo debe ser mayor a 0")]
	public decimal Costo { get; set; }

	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public PlanMembresia Entidad() => new(new Guid(Id), Nombre, Meses, Costo, Estado);
}
