using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models;
using fitcare.Models.Entities;

namespace fitcare.Models.ViewModels;

public class EjercicioViewModel : BaseViewModel
{
	public EjercicioViewModel(Ejercicio ejercicio) : base(ejercicio)
	{
		Id = ejercicio.Id.ToString();
		Codigo = ejercicio.Codigo;
		Nombre = ejercicio.Nombre;
		Estado = ejercicio.Estado ? "Activo" : "Inactivo";
		TipoEjercicio = ejercicio.TipoEjercicio.Nombre;
		GruposMusculares = ejercicio.GruposMusculares?.Select(g => g.Nombre).ToList() ?? new List<string>();
		Maquinas = ejercicio.Maquinas?.Select(m => m.Nombre).ToList() ?? new List<string>();
	}

	public string Id { get; set; }

	[Display(Name = "Código")]
	public string Codigo { get; set; }

	[Display(Name = "Ejercicio")]
	public string Nombre { get; set; }

	[Display(Name = "Tipo de ejercicio")]
	public string TipoEjercicio { get; set; }

	public string Estado { get; set; }

	[Display(Name = "Grupos musculares")]
	public List<string> GruposMusculares { get; set; }

	[Display(Name = "Máquinas")]
	public List<string> Maquinas { get; set; }
}

public class AgregarEjercicioViewModel
{
	[Display(Name = "Código")]
	[Required(ErrorMessage = "El código es requerido")]
	[StringLength(50, ErrorMessage = "El código no puede exceder los {0} caracteres")]
	public string Codigo { get; set; }

	[Display(Name = "Ejercicio")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(50, ErrorMessage = "El nombre no puede exceder los {0} caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Tipo de ejercicio")]
	[Required(ErrorMessage = "El tipo de ejercicio es requerido")]
	public string IdTipoEjercicio { get; set; }

	public bool Activo { get; set; }

	[Display(Name = "Grupos musculares")]
	public List<string> IdsGruposMusculares { get; set; } = new();

	[Display(Name = "Máquinas")]
	public List<string> IdsMaquinas { get; set; } = new();

	public Ejercicio Entidad() => new(Guid.NewGuid(), Codigo, Nombre, Activo, new Guid(IdTipoEjercicio));

	public static async Task<AgregarEjercicioViewModel> CrearAsync(IGeneradorCodigo<Ejercicio> generadorCodigo)
	{
		return new AgregarEjercicioViewModel
		{
			Codigo = await generadorCodigo.GenerarCodigoAsync(),
			Activo = true
		};
	}

	public async Task RegenerarCodigoAsync(IGeneradorCodigo<Ejercicio> generadorCodigo)
	{
		Codigo = await generadorCodigo.GenerarCodigoAsync();
	}
}

public class EditarEjercicioViewModel : BaseViewModel
{
	public EditarEjercicioViewModel() { }

	public EditarEjercicioViewModel(Ejercicio ejercicio) : base(ejercicio)
	{
		Id = ejercicio.Id.ToString();
		Codigo = ejercicio.Codigo;
		Nombre = ejercicio.Nombre;
		Activo = ejercicio.Estado;
		IdTipoEjercicio = ejercicio.TipoEjercicio.Id.ToString();
		IdsGruposMusculares = ejercicio.GruposMusculares?.Select(g => g.Id.ToString()).ToList() ?? new();
		IdsMaquinas = ejercicio.Maquinas?.Select(m => m.Id.ToString()).ToList() ?? new();
	}

	[Required(ErrorMessage = "El id es requerido")]
	public string Id { get; set; }

	[Display(Name = "Código")]
	[Required(ErrorMessage = "El código es requerido")]
	[StringLength(50, ErrorMessage = "El código no puede exceder los {0} caracteres")]
	public string Codigo { get; set; }

	[Display(Name = "Ejercicio")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(50, ErrorMessage = "El nombre no puede exceder los {0} caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Tipo de ejercicio")]
	[Required(ErrorMessage = "El tipo de ejercicio es requerido")]
	public string IdTipoEjercicio { get; set; }

	public bool Activo { get; set; }

	[Display(Name = "Grupos musculares")]
	public List<string> IdsGruposMusculares { get; set; } = new();

	[Display(Name = "Máquinas")]
	public List<string> IdsMaquinas { get; set; } = new();

	public Ejercicio Entidad() => new(new Guid(Id), Codigo, Nombre, Activo, new Guid(IdTipoEjercicio));
}

public class EliminarEjercicioViewModel
{
	public EliminarEjercicioViewModel() {}

	public EliminarEjercicioViewModel(Ejercicio ejercicio)
	{
		Id = ejercicio.Id.ToString();
		Nombre = ejercicio.Nombre;
	}
	[Required(ErrorMessage = "El id es requerido.")]
	public string Id { get; set; }

	public string Nombre { get; set; }
}

public class TipoEjercicioViewModel : BaseViewModel
{
	public TipoEjercicioViewModel(TipoEjercicio tipoEjercicio) : base(tipoEjercicio)
	{
		IdTipoEjercicio = tipoEjercicio.Id.ToString();
		Codigo = tipoEjercicio.Codigo;
		Nombre = tipoEjercicio.Nombre;
		Estado = tipoEjercicio.Estado ? "Activo" : "Inactivo";
	}

	public string IdTipoEjercicio { get; set; }

	[Display(Name = "Código")]
	public string Codigo { get; set; }

	[Display(Name = "Tipo de ejercicio")]
	public string Nombre { get; set; }

	public string Estado { get; set; }
}

public class AgregarTipoEjercicioViewModel
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

	public TipoEjercicio Entidad() => new(Guid.NewGuid(), Codigo, Nombre, Estado);

	public static async Task<AgregarTipoEjercicioViewModel> CrearAsync(IGeneradorCodigo<TipoEjercicio> generadorCodigo)
	{
		return new AgregarTipoEjercicioViewModel
		{
			Codigo = await generadorCodigo.GenerarCodigoAsync(),
			Estado = true
		};
	}

	public async Task RegenerarCodigoAsync(IGeneradorCodigo<TipoEjercicio> generadorCodigo)
	{
		Codigo = await generadorCodigo.GenerarCodigoAsync();
	}
}

public class EditarTipoEjercicioViewModel : BaseViewModel
{
	public EditarTipoEjercicioViewModel() { }

	public EditarTipoEjercicioViewModel(TipoEjercicio tipoEjercicio) : base(tipoEjercicio)
	{
		Id = tipoEjercicio.Id.ToString();
		Codigo = tipoEjercicio.Codigo;
		Nombre = tipoEjercicio.Nombre;
		Estado = tipoEjercicio.Estado;
	}

	public string Id { get; set; }

	[Display(Name = "Código")]
	[Required(ErrorMessage = "El código es requerido")]
	public string Codigo { get; set; }

	[Display(Name = "Tipo de ejercicio")]
	[Required(ErrorMessage = "El nombre es requerido")]
	[StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
	public string Nombre { get; set; }

	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public TipoEjercicio Entidad() => new(new Guid(Id), Codigo, Nombre, Estado);
}

public class EliminarTipoEjercicioViewModel
{
	public EliminarTipoEjercicioViewModel() { }

	public EliminarTipoEjercicioViewModel(TipoEjercicio tipoEjercicio)
	{
		IdTipoEjercicio = tipoEjercicio.Id.ToString();
		Nombre = tipoEjercicio.Nombre;
	}

	public string IdTipoEjercicio { get; set; }

	public string Nombre { get; set; }
}
