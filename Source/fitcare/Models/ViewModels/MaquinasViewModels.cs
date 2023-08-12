// using fitcare.Models.Contracts;
// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using Microsoft.AspNetCore.Http;
// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.Linq;
// using System.Threading.Tasks;

// namespace fitcare.Models.ViewModels;

// public class MaquinaViewModel
// {
// 	public MaquinaViewModel() { }

// 	public MaquinaViewModel(Maquina maquina)
// 	{
// 		Id = maquina.Id.ToString();
// 		Codigo = maquina.Codigo;
// 		NombreTipoMaquina = maquina.TipoMaquina.Nombre;
// 		Nombre = maquina.Nombre;
// 		NumeroActivo = maquina.CodigoActivo;
// 		Estado = maquina.Estado ? "Activo" : "Inactivo";
// 		Activo = maquina.Estado;
// 		FechaAdquisicion = maquina.FechaAdquisicion;
// 		URLImagen = maquina.Imagen.URL;

// 		GruposMusculares = maquina.GruposMusculares;
// 	}

// 	public string Id { get; set; }

// 	[Display(Name = "Tipo de máquina")]
// 	public string NombreTipoMaquina { get; set; }

// 	[Display(Name = "Código")]
// 	[Required(ErrorMessage = "El código es requerido")]
// 	[StringLength(100, ErrorMessage = "El código no puede exceder los 100 caracteres")]
// 	public string Codigo { get; set; }

// 	[Required(ErrorMessage = "El nombre es requerido")]
// 	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Activo #")]
// 	[Required(ErrorMessage = "El número de activo es requerido")]
// 	[StringLength(100, ErrorMessage = "El número de activo no puede exceder los 100 caracteres")]
// 	public string NumeroActivo { get; set; }

// 	[Display(Name = "Fecha de adquisición")]
// 	[Required(ErrorMessage = "La fecha de adquisición es requerida")]
// 	[DataType(DataType.Date, ErrorMessage = "La fecha no tiene formato correcto.")]
// 	public DateTime FechaAdquisicion { get; set; }

// 	public string Estado { get; set; }

// 	public bool Activo { get; set; }

// 	[Display(Name = "Imagen")]
// 	public Uri URLImagen { get; set; }

// 	public IList<GrupoMuscular> GruposMusculares { get; set; }
// 	public ICollection<string> AllKeys;

// 	public IDataCRUDBase<TipoMaquina> _repoTiposMaquina;
// 	public IDataCRUDBase<GrupoMuscular> _repoGruposMusculares;

// 	public void SetDependencies(IDataCRUDBase<TipoMaquina> repoTiposMaquina, IDataCRUDBase<GrupoMuscular> repoGruposMusculares)
// 	{
// 		_repoGruposMusculares = repoGruposMusculares;
// 		_repoTiposMaquina = repoTiposMaquina;
// 	}

// 	public async Task AsignarGruposMusculares()
// 	{
// 		GruposMusculares = new List<GrupoMuscular>();

// 		// Recorrer los accesorios chequeados y tomar el id que es el id del accesorio y crear un
// 		// objeto accesorio asignando la propiedad id obtenida del collection,
// 		// los mismo para maquinas y grupos musculares
// 		foreach (string key in AllKeys.Skip(6))
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

// public class NuevaMaquinaViewModel : MaquinaViewModel
// {
// 	[Display(Name = "Tipo de máquina")]
// 	[Required(ErrorMessage = "Debe indicar el tipo de máquina")]
// 	public string IdTipoMaquina { get; set; }

// 	[Required(ErrorMessage = "La imagen es requerida")]
// 	public IFormFile Imagen { get; set; }

// 	public async Task<Maquina> Entidad(IFormCollection collection)
// 	{
// 		AllKeys = collection.Keys;
// 		await AsignarGruposMusculares();
// 		TipoMaquina tipoMaquina = await _repoTiposMaquina.ReadByIdAsync(Factory.SetGuid(IdTipoMaquina));
// 		Maquina maquina = new(Guid.NewGuid(), Codigo, Nombre, NumeroActivo, Activo, Convert.ToDateTime(FechaAdquisicion), tipoMaquina, GruposMusculares);
// 		return maquina;
// 	}
// }

// public class EditarMaquinaViewModel : MaquinaViewModel
// {
// 	public EditarMaquinaViewModel() { }

// 	public EditarMaquinaViewModel(Maquina maquina) : base(maquina)
// 	{
// 		Validators.ValidateList<GrupoMuscular>(maquina.GruposMusculares, nameof(GrupoMuscular));

// 		IdTipoMaquina = maquina.TipoMaquina.Id.ToString();
// 		GruposMusculares = maquina.GruposMusculares;
// 	}

// 	[Display(Name = "Tipo de máquina")]
// 	[Required(ErrorMessage = "Debe indicar el tipo de máquina")]
// 	public string IdTipoMaquina { get; set; }

// 	[Display(Name = "Nueva imagen")]
// 	public IFormFile Imagen { get; set; }

// 	public async Task<Maquina> Entidad(IFormCollection collection)
// 	{
// 		AllKeys = collection.Keys;
// 		await AsignarGruposMusculares();
// 		TipoMaquina tipoMaquina = await _repoTiposMaquina.ReadByIdAsync(Factory.SetGuid(IdTipoMaquina));
// 		Maquina maquina = new(Factory.SetGuid(Id), Codigo, Nombre, NumeroActivo, Activo, Convert.ToDateTime(FechaAdquisicion), tipoMaquina, GruposMusculares);
// 		return maquina;
// 	}
// }

// public class DetalleMaquinaViewModel : MaquinaViewModel
// {
// 	public string NombresGruposMusculares { get; set; }
// }

// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using System;
// using System.ComponentModel.DataAnnotations;

// namespace fitcare.Models.ViewModels;

// public class InicioTipoMaquinaViewModel
// {
// 	public InicioTipoMaquinaViewModel(TipoMaquina tipoMaquina)
// 	{
// 		Validators.ValidateTipoMaquina(tipoMaquina);

// 		IdTipoMaquina = tipoMaquina.Id.ToString();
// 		Nombre = tipoMaquina.Nombre;
// 		Estado = tipoMaquina.Estado ? "Activo" : "Inactivo";
// 	}

// 	public string IdTipoMaquina { get; set; }

// 	[Display(Name = "Tipo de máquina")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }

// public class NuevoTipoMaquinaViewModel
// {
// 	[Display(Name = "Nombre del tipo de máquina")]
// 	[Required(ErrorMessage = "El nombre es requerido")]
// 	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Activo")]
// 	public bool Estado { get; set; }

// 	public TipoMaquina Entidad()
// 	{
// 		TipoMaquina tipoMaquina = new(Guid.NewGuid(), Nombre, Estado);
// 		return tipoMaquina;
// 	}
// }

// public class EditarTipoMaquinaViewModel
// {
// 	public EditarTipoMaquinaViewModel() { }

// 	public EditarTipoMaquinaViewModel(TipoMaquina tipoMaquina)
// 	{
// 		Validators.ValidateTipoMaquina(tipoMaquina);

// 		Id = tipoMaquina.Id.ToString();
// 		Nombre = tipoMaquina.Nombre;
// 		Estado = tipoMaquina.Estado;
// 	}

// 	public string Id { get; set; }

// 	[Display(Name = "Nombre del tipo de máquina")]
// 	[Required(ErrorMessage = "El nombre es requerido")]
// 	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Activo")]
// 	public bool Estado { get; set; }

// 	public TipoMaquina Entidad()
// 	{
// 		TipoMaquina tipoMaquina = new(Factory.SetGuid(Id), Nombre, Estado);
// 		return tipoMaquina;
// 	}
// }

// public class DetalleTipoMaquinaViewModel
// {
// 	public DetalleTipoMaquinaViewModel(TipoMaquina tipoMaquina)
// 	{
// 		Validators.ValidateTipoMaquina(tipoMaquina);

// 		IdTipoMaquina = tipoMaquina.Id.ToString();
// 		Nombre = tipoMaquina.Nombre;
// 		Estado = tipoMaquina.Estado ? "Activo" : "Inactivo";
// 	}

// 	public string IdTipoMaquina { get; set; }

// 	[Display(Name = "TipoMaquina")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }
