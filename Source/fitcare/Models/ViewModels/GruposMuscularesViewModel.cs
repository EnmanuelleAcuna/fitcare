// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using System;
// using System.ComponentModel.DataAnnotations;

// namespace fitcare.Models.ViewModels;

// public class InicioGrupoMuscularViewModel
// {
// 	public InicioGrupoMuscularViewModel(GrupoMuscular grupoMuscular)
// 	{
// 		Validators.ValidateGrupoMuscular(grupoMuscular);

// 		IdGrupoMuscular = grupoMuscular.Id.ToString();
// 		Nombre = grupoMuscular.Nombre;
// 		Descripcion = grupoMuscular.Descripcion;
// 		Estado = grupoMuscular.Estado ? "Activo" : "Inactivo";
// 	}

// 	public string IdGrupoMuscular { get; set; }

// 	[Display(Name = "Nombre")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Descripcion")]
// 	public string Descripcion { get; set; }

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }

// public class NuevoGrupoMuscularViewModel
// {
// 	[Display(Name = "Nombre del grupo muscular")]
// 	[Required(ErrorMessage = "El nombre es requerido")]
// 	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Descripción")]
// 	[Required(ErrorMessage = "La descripción es requerida.")]
// 	[StringLength(4000, ErrorMessage = "El nombre no puede exceder los 4000 caracteres")]
// 	public string Descripcion { get; set; }

// 	[Display(Name = "Activo")]
// 	public bool Estado { get; set; }

// 	public GrupoMuscular Entidad()
// 	{
// 		GrupoMuscular grupoMuscular = new(Guid.NewGuid(), Nombre, Descripcion, Estado);
// 		return grupoMuscular;
// 	}
// }

// public class EditarGrupoMuscularViewModel
// {
// 	public EditarGrupoMuscularViewModel() { }

// 	public EditarGrupoMuscularViewModel(GrupoMuscular grupoMuscular)
// 	{
// 		Validators.ValidateGrupoMuscular(grupoMuscular);

// 		Id = grupoMuscular.Id.ToString();
// 		Nombre = grupoMuscular.Nombre;
// 		Descripcion = grupoMuscular.Descripcion;
// 		Estado = grupoMuscular.Estado;
// 	}

// 	[Required(ErrorMessage = "El id es requerido")]
// 	public string Id { get; set; }

// 	[Display(Name = "Nombre del grupo muscular")]
// 	[Required(ErrorMessage = "El nombre es requerido")]
// 	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Descripción")]
// 	[Required(ErrorMessage = "ELa descripción es requerida.")]
// 	[StringLength(4000, ErrorMessage = "El nombre no puede exceder los 4000 caracteres")]
// 	public string Descripcion { get; set; }

// 	[Display(Name = "Activo")]
// 	public bool Estado { get; set; }

// 	public GrupoMuscular Entidad()
// 	{
// 		GrupoMuscular grupomuscular = new(Factory.SetGuid(Id), Nombre, Descripcion, Estado);
// 		return grupomuscular;
// 	}
// }

// public class DetalleGrupoMuscularViewModel : RegistroBitacoraViewModel
// {
// 	public DetalleGrupoMuscularViewModel(GrupoMuscular grupoMuscular, Bitacora registroBitacoraAgregar, Bitacora registroBitacoraModificar)
// 	:base(registroBitacoraAgregar, registroBitacoraModificar)

// 	{
// 		Validators.ValidateGrupoMuscular(grupoMuscular);

// 		IdGrupoMuscular = grupoMuscular.Id.ToString();
// 		Nombre = grupoMuscular.Nombre;
// 		Descripcion = grupoMuscular.Descripcion;
// 		Estado = grupoMuscular.Estado ? "Activo" : "Inactivo";
// 	}

// 	public string IdGrupoMuscular { get; set; }

// 	[Display(Name = "Nombre")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Descripción")]
// 	public string Descripcion { get; set; }

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }
