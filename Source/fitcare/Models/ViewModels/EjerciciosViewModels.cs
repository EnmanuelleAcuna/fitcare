using System;
using System.ComponentModel.DataAnnotations;
using fitcare.Models.Entities;

namespace fitcare.Models.ViewModels;

// public class EjercicioViewModel
// {
// 	public string Id { get; set; }

// 	[Display(Name = "Código")]
// 	[Required(ErrorMessage = "El código es requerido")]
// 	[StringLength(50, ErrorMessage = "El código no puede exceder los {0} caracteres")]
// 	public string Codigo { get; set; }

// 	[Display(Name = "Ejercicio")]
// 	[Required(ErrorMessage = "El nombre es requerido")]
// 	[StringLength(50, ErrorMessage = "El nombre no puede exceder los {0} caracteres")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Tipo de ejercicio")]
// 	[Required(ErrorMessage = "El tipo de ejercicio es requerido")]
// 	public string TipoEjercicio { get; set; }

// 	public IList<Accesorio> Accesorios { get; set; }
// 	public IList<Maquina> Maquinas { get; set; }
// 	public IList<GrupoMuscular> GruposMusculares { get; set; }
// 	public ICollection<string> AllKeys;

// 	public IDataCRUDBase<Accesorio> _repoAccesorios;
// 	public IDataCRUDBase<Maquina> _repoMaquinas;
// 	public IDataCRUDBase<GrupoMuscular> _repoGruposMusculares;
// 	public IDataCRUDBase<TipoEjercicio> _repoTiposEjercicio;

// 	public void SetDependencies(IDataCRUDBase<Accesorio> repoAccesorios, IDataCRUDBase<Maquina> repoMaquinas, IDataCRUDBase<GrupoMuscular> repoGruposMusculares, IDataCRUDBase<TipoEjercicio> repoTiposEjercicio)
// 	{
// 		_repoAccesorios = repoAccesorios;
// 		_repoMaquinas = repoMaquinas;
// 		_repoGruposMusculares = repoGruposMusculares;
// 		_repoTiposEjercicio = repoTiposEjercicio;
// 	}

// 	public async Task AsignarAccesorios(int keysCountToSkip)
// 	{
// 		Accesorios = new List<Accesorio>();

// 		// Recorrer los accesorios chequeados y tomar el id que es el id del accesorio y crear un
// 		// objeto accesorio asignando la propiedad id obtenida del collection,
// 		// los mismo para maquinas y grupos musculares
// 		foreach (string key in AllKeys.Skip(keysCountToSkip))
// 		{
// 			if (key[..1].Equals("A", StringComparison.OrdinalIgnoreCase))
// 			{
// 				string idAccesorio = key[2..];
// 				Accesorio accesorio = await _repoAccesorios.ReadByIdAsync(Factory.SetGuid(idAccesorio));
// 				Accesorios.Add(accesorio);
// 			}
// 		}
// 	}

// 	public async Task AsignarMaquinas(int keysCountToSkip)
// 	{
// 		Maquinas = new List<Maquina>();

// 		// Recorrer los accesorios chequeados y tomar el id que es el id del accesorio y crear un
// 		// objeto accesorio asignando la propiedad id obtenida del collection,
// 		// los mismo para maquinas y grupos musculares
// 		foreach (string key in AllKeys.Skip(keysCountToSkip))
// 		{
// 			if (key[..1].Equals("M", StringComparison.OrdinalIgnoreCase))
// 			{
// 				string idMaquina = key[2..];
// 				Maquina maquina = await _repoMaquinas.ReadByIdAsync(Factory.SetGuid(idMaquina));
// 				Maquinas.Add(maquina);
// 			}
// 		}
// 	}

// 	public async Task AsignarGruposMusculares(int keysCountToSkip)
// 	{
// 		GruposMusculares = new List<GrupoMuscular>();

// 		// Recorrer los accesorios chequeados y tomar el id que es el id del accesorio y crear un
// 		// objeto accesorio asignando la propiedad id obtenida del collection,
// 		// los mismo para maquinas y grupos musculares
// 		foreach (string key in AllKeys.Skip(keysCountToSkip))
// 		{
// 			if (key[..1].Equals("G", StringComparison.OrdinalIgnoreCase))
// 			{
// 				string idGrupoMuscular = key[2..];
// 				GrupoMuscular grupoMuscular = await _repoGruposMusculares.ReadByIdAsync(Factory.SetGuid(idGrupoMuscular));
// 				GruposMusculares.Add(grupoMuscular);
// 			}
// 		}
// 	}
// }

// public class InicioEjercicioViewModel : EjercicioViewModel
// {
// 	public InicioEjercicioViewModel(Ejercicio ejercicio)
// 	{
// 		Validators.ValidateEjercicio(ejercicio);
// 		Validators.ValidateTipoEjercicio(ejercicio.TipoEjercicio);

// 		Id = ejercicio.Id.ToString();
// 		Codigo = ejercicio.Codigo;
// 		Nombre = ejercicio.Nombre;
// 		Estado = ejercicio.Estado ? "Activo" : "Inactivo";
// 		TipoEjercicio = ejercicio.TipoEjercicio.Nombre;
// 	}

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }

// public class NuevoEjercicioViewModel : EjercicioViewModel
// {
// 	[Display(Name = "Activo")]
// 	public bool Activo { get; set; }

// 	public async Task<Ejercicio> Entidad(IFormCollection collection)
// 	{
// 		// En la colección vienen los accesorios y maquinas seleccionados y la llave es el id del accesorio y maquina chequeado
// 		AllKeys = collection.Keys;
// 		await AsignarAccesorios(4);
// 		await AsignarMaquinas(4);
// 		await AsignarGruposMusculares(4);
// 		TipoEjercicio tipoEjercicio = await _repoTiposEjercicio.ReadByIdAsync(Factory.SetGuid(TipoEjercicio));
// 		Ejercicio ejercicio = new(Guid.NewGuid(), Codigo, Nombre, Activo, tipoEjercicio, Accesorios, Maquinas, GruposMusculares);
// 		return ejercicio;
// 	}
// }

// public class EditarEjercicioViewModel : EjercicioViewModel
// {
// 	public EditarEjercicioViewModel() { }

// 	public EditarEjercicioViewModel(Ejercicio ejercicio)
// 	{
// 		Validators.ValidateEjercicio(ejercicio);
// 		Validators.ValidateTipoEjercicio(ejercicio.TipoEjercicio);
// 		Validators.ValidateList<Accesorio>(ejercicio.Accesorios, nameof(Accesorio));
// 		Validators.ValidateList<Maquina>(ejercicio.Maquinas, nameof(Maquina));
// 		Validators.ValidateList<GrupoMuscular>(ejercicio.GruposMusculares, nameof(GrupoMuscular));

// 		Id = ejercicio.Id.ToString();
// 		Codigo = ejercicio.Codigo;
// 		Nombre = ejercicio.Nombre;
// 		Activo = ejercicio.Estado;
// 		TipoEjercicio = ejercicio.TipoEjercicio.Id.ToString();

// 		Accesorios = ejercicio.Accesorios;
// 		Maquinas = ejercicio.Maquinas;
// 		GruposMusculares = ejercicio.GruposMusculares;
// 	}

// 	[Display(Name = "Activo")]
// 	public bool Activo { get; set; }

// 	public async Task<Ejercicio> Entidad(IFormCollection collection)
// 	{
// 		// En la colección vienen los accesorios y maquinas seleccionados y la llave es el id del accesorio y maquina chequeado
// 		AllKeys = collection.Keys;
// 		await AsignarAccesorios(5);
// 		await AsignarMaquinas(5);
// 		await AsignarGruposMusculares(5);
// 		TipoEjercicio tipoEjercicio = await _repoTiposEjercicio.ReadByIdAsync(Factory.SetGuid(TipoEjercicio));
// 		Ejercicio ejercicio = new(Factory.SetGuid(Id), Codigo, Nombre, Activo, tipoEjercicio, Accesorios, Maquinas, GruposMusculares);
// 		return ejercicio;
// 	}
// }

// public class EjerciciosViewModel : RegistroBitacoraViewModel
// {
// 	public EjerciciosViewModel(Ejercicio ejercicio, Bitacora registroBitacoraAgregar, Bitacora registroBitacoraModificar)
// 	: base(registroBitacoraAgregar, registroBitacoraModificar)

// 	{
// 		Validators.ValidateEjercicio(ejercicio);
// 	}

// }

// public class DetalleEjercicioViewModel : EjercicioViewModel
// {
// 	public DetalleEjercicioViewModel(Ejercicio ejercicio)
// 	{
// 		Validators.ValidateEjercicio(ejercicio);
// 		Validators.ValidateTipoEjercicio(ejercicio.TipoEjercicio);

// 		Id = ejercicio.Id.ToString();
// 		Codigo = ejercicio.Codigo;
// 		Nombre = ejercicio.Nombre;
// 		Estado = ejercicio.Estado ? "Activo" : "Inactivo";
// 		TipoEjercicio = ejercicio.TipoEjercicio.Nombre;

// 		Accesorios = ejercicio.Accesorios;
// 		Maquinas = ejercicio.Maquinas;
// 		GruposMusculares = ejercicio.GruposMusculares;
// 	}

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }

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
