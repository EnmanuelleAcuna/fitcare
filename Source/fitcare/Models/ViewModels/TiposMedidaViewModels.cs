// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using System;
// using System.ComponentModel.DataAnnotations;

// namespace fitcare.Models.ViewModels;

// public class InicioTipoMedidaViewModel
// {
// 	public InicioTipoMedidaViewModel(TipoMedida Modelo)
// 	{
// 		Validators.ValidateTipoMedida(Modelo);

// 		Id = Modelo.Id.ToString();
// 		Codigo = Modelo.Codigo;
// 		Nombre = Modelo.Nombre;
// 		Estado = Modelo.Estado ? "Activo" : "Inactivo";
// 	}

// 	public string Id { get; set; }

// 	[Display(Name = "Código")]
// 	public string Codigo { get; set; }

// 	[Display(Name = "Tipo de medida")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }

// public class NuevoTipoMedidaViewModel
// {
// 	[Display(Name = "Código")]
// 	[Required(ErrorMessage = "El código es requerido")]
// 	[StringLength(50, ErrorMessage = "El código no puede exceder los 50 caracteres")]
// 	public string Codigo { get; set; }

// 	[Display(Name = "Nombre")]
// 	[Required(ErrorMessage = "El nombre es requerido")]
// 	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Activo")]
// 	public bool Estado { get; set; }

// 	public TipoMedida Entidad()
// 	{
// 		TipoMedida tipoMedida = new(Guid.NewGuid(), Codigo, Nombre, Estado);
// 		return tipoMedida;
// 	}
// }

// public class EditarTipoMedidaViewModel
// {
// 	// Constructores
// 	public EditarTipoMedidaViewModel() { }

// 	public EditarTipoMedidaViewModel(TipoMedida tipoMedida)
// 	{
// 		Validators.ValidateTipoMedida(tipoMedida);

// 		Id = tipoMedida.Id.ToString();
// 		Codigo = tipoMedida.Codigo;
// 		Nombre = tipoMedida.Nombre;
// 		Estado = tipoMedida.Estado;
// 	}

// 	public string Id { get; set; }

// 	[Display(Name = "Código")]
// 	[Required(ErrorMessage = "El código es requerido")]
// 	[StringLength(50, ErrorMessage = "El código no puede exceder los 50 caracteres")]
// 	public string Codigo { get; set; }

// 	[Display(Name = "Nombre")]
// 	[Required(ErrorMessage = "El nombre es requerido")]
// 	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Activo")]
// 	public bool Estado { get; set; }

// 	public TipoMedida Entidad()
// 	{
// 		TipoMedida tipoMedida = new(Factory.SetGuid(Id), Codigo, Nombre, Estado);
// 		return tipoMedida;
// 	}
// }

// public class DetalleTipoMedidaViewModel
// {
// 	public DetalleTipoMedidaViewModel(TipoMedida tipoMedida)
// 	{
// 		Validators.ValidateTipoMedida(tipoMedida);

// 		IdTipoMedida = tipoMedida.Id.ToString();
// 		Codigo = tipoMedida.Codigo;
// 		Nombre = tipoMedida.Nombre;
// 		Estado = tipoMedida.Estado ? "Activo" : "Inactivo";
// 	}

// 	public string IdTipoMedida { get; set; }

// 	[Display(Name = "Código")]
// 	public string Codigo { get; set; }

// 	[Display(Name = "Tipo de medida")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }
