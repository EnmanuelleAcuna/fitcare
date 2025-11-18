using fitcare.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace fitcare.Models.ViewModels;

public class GrupoMuscularViewModel : BaseViewModel
{
	public GrupoMuscularViewModel(GrupoMuscular grupoMuscular) : base(grupoMuscular)
	{
		IdGrupoMuscular = grupoMuscular.Id.ToString();
		Nombre = grupoMuscular.Nombre;
		Descripcion = grupoMuscular.Descripcion;
		Estado = grupoMuscular.Estado ? "Activo" : "Inactivo";
	}

	public string IdGrupoMuscular { get; set; }

	public string Nombre { get; set; }

	[Display(Name = "Descripcion")]
	public string Descripcion { get; set; }
	
	public string Estado { get; set; }	
}

public class AgregarGrupoMuscularViewModel
{
	[Display(Name = "Nombre del grupo muscular")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Descripción")]
	[Required(ErrorMessage = "La descripción es requerida.")]
	[StringLength(4000, ErrorMessage = "El nombre no puede exceder los 4000 caracteres")]
	public string Descripcion { get; set; }
	
	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public GrupoMuscular Entidad() => new(Guid.NewGuid(), Nombre, Descripcion, Estado);
}

public class EditarGrupoMuscularViewModel : BaseViewModel
{
	public EditarGrupoMuscularViewModel() { }

	public EditarGrupoMuscularViewModel(GrupoMuscular grupoMuscular) : base(grupoMuscular)
	{
		Id = grupoMuscular.Id.ToString();
		Nombre = grupoMuscular.Nombre;
		Descripcion = grupoMuscular.Descripcion;
		Estado = grupoMuscular.Estado;
	}

	[Required(ErrorMessage = "El id es requerido")]
	public string Id { get; set; }

	[Display(Name = "Nombre del grupo muscular")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Descripción")]
	[Required(ErrorMessage = "ELa descripción es requerida.")]
	[StringLength(4000, ErrorMessage = "El nombre no puede exceder los 4000 caracteres")]
	public string Descripcion { get; set; }
	
	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public GrupoMuscular Entidad() => new(new Guid(Id), Nombre, Descripcion, Estado);
}

public class EliminarGrupoMuscularViewModel
{
	public EliminarGrupoMuscularViewModel() {}

	public EliminarGrupoMuscularViewModel(GrupoMuscular grupoMuscular)
	{
		IdGrupoMuscular = grupoMuscular.Id.ToString();
		Nombre = grupoMuscular.Nombre;
	}

	public string IdGrupoMuscular { get; set; }

	public string Nombre { get; set; }
}
