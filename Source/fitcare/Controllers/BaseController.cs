using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using fitcare.Models.Identity;
using fitcare.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IO = System.IO;

namespace fitcare.Controllers;

public class BaseController : Controller
{
	private readonly IDivisionTerritorialManager _divisionTerritorialManager;
	private readonly ApplicationUserManager<ApplicationUser> _userManager;
	private readonly RoleManager<ApplicationRole> _roleManager;
	private readonly IConfiguration _configuration;
	private readonly IHttpContextAccessor _contextAccessor;
	private readonly IWebHostEnvironment _environment;

	public BaseController(IDivisionTerritorialManager divisionTerritorialManager,
						  ApplicationUserManager<ApplicationUser> userManager,
						  RoleManager<ApplicationRole> roleManager,
						  IConfiguration configuration,
						  IHttpContextAccessor contextAccessor,
						  IWebHostEnvironment environment)
	{
		_divisionTerritorialManager = divisionTerritorialManager;
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration = configuration;
		_contextAccessor = contextAccessor;
		_environment = environment;
	}

	public async Task CreateDefaultUser()
	{
		if (await _userManager.Users.AnyAsync()) return;

		var rolAdministrador = new ApplicationRole(Guid.NewGuid().ToString(), "Administrador", "Administrador del sistema.");
		var rolInstructor = new ApplicationRole(Guid.NewGuid().ToString(), "Instructor", "Instructor del gimnasio.");
		var rolCliente = new ApplicationRole(Guid.NewGuid().ToString(), "Cliente", "Cliente del gimnasio.");

		await _roleManager.CreateAsync(rolAdministrador);
		await _roleManager.CreateAsync(rolInstructor);
		await _roleManager.CreateAsync(rolCliente);

		var usuario = new ApplicationUser(Guid.NewGuid().ToString(), "emanuelacu@gmail.com",
										  "emanuelacu@gmail.com", "Enmanuelle", "Acuña", "Arguedas",
										  "206830685", DateTime.Now, true);

		var rolesSeleccionados = new List<string> { "Administrador", "Instructor" };

		await _userManager.CreateAsync(usuario, "ContraseñaGenerica");
		await _userManager.AddToRolesAsync(usuario, rolesSeleccionados);

		await Task.CompletedTask;
	}

	public string GetCurrentUser() => User.Identity.Name;

	public IActionResult RedirectToLocal(string returnUrl)
	{
		if (Url.IsLocalUrl(returnUrl))
		{
			return Redirect(returnUrl);
		}

		return RedirectToAction("Index", "Home");
	}

	public void AddErrors(IdentityResult Result)
	{
		foreach (IdentityError error in Result.Errors)
		{
			ModelState.AddModelError("", error.Description);
		}
	}

	public async Task<IList<SelectListItem>> CargarListaSeleccionProvincias()
	{
		IList<Provincia> provincias = await _divisionTerritorialManager.Provincias.ReadAllAsync();
		IList<SelectListItem> listaSeleccionProvincias = provincias.Select(p => new SelectListItem { Value = Convert.ToString(p.Id.ToString(), new CultureInfo("es-CR")), Text = p.Nombre }).ToList();
		return listaSeleccionProvincias;
	}

	public async Task<IList<CantonesDistritosSelectListItem>> CargarListaSeleccionCantones()
	{
		IList<Canton> cantones = await _divisionTerritorialManager.Cantones.ReadAllAsync();
		IList<CantonesDistritosSelectListItem> listaSeleccionCantones = cantones.Select(c => new CantonesDistritosSelectListItem { Value = c.Id.ToString(), Text = c.Nombre, IdPadre = c.Provincia.Id.ToString() }).ToList();
		return listaSeleccionCantones;
	}

	public async Task<IList<CantonesDistritosSelectListItem>> CargarListaSeleccionDistritos()
	{
		IList<Distrito> distritos = await _divisionTerritorialManager.Distritos.ReadAllAsync();
		IList<CantonesDistritosSelectListItem> listaSeleccionDistritos = distritos.Select(d => new CantonesDistritosSelectListItem { Value = d.Id.ToString(), Text = d.Nombre, IdPadre = d.Canton.Id.ToString() }).ToList();
		return listaSeleccionDistritos;
	}

	public async Task<IList<SelectListItem>> CargarListaSeleccionDistritosConCantonesProvincias()
	{
		IList<Distrito> distritos = await _divisionTerritorialManager.Distritos.ReadAllAsync();
		IList<SelectListItem> listaSeleccionDistritos = distritos.Select(d => new SelectListItem { Value = Convert.ToString(d.Id.ToString(), new CultureInfo("es-CR")), Text = String.Format("{0}, {1}, {2}", d.Canton.Provincia.Nombre, d.Canton.Nombre, d.Nombre) }).ToList();
		return listaSeleccionDistritos;
	}

	public IList<SelectListItem> CargarListaSeleccionRoles()
	{
		IList<ApplicationRole> listaRoles = _roleManager.Roles.ToList();
		IList<SelectListItem> listaSeleccionRoles = listaRoles.Select(p => new SelectListItem { Value = p.Id, Text = p.Name }).ToList();
		return listaSeleccionRoles;
	}

	public static IList<string> ObtenerRolesSeleccionados(IFormCollection collection)
	{
		// En la colección vienen los rols seleccionados y la llave es el id del rol chequeado
		// Recorrer los roles chequeados y tomar el id que es el id del rol y crear un
		// objeto rol asignando la propiedad id obtenida del collection
		IList<string> rolesSeleccionados = new List<string>();
		foreach (string key in collection.Keys)
		{
			if (key[..1].Equals("R", StringComparison.OrdinalIgnoreCase))
			{
				string rolSeleccionado = key[2..];
				rolesSeleccionados.Add(rolSeleccionado);
			}
		}

		return rolesSeleccionados;
	}

	public async Task<IList<ApplicationRole>> ObtenerRolesUsuario(ApplicationUser usuario)
	{
		IList<string> nombresRolesUsuario = await _userManager.GetRolesAsync(usuario);

		IList<ApplicationRole> rolesUsuario = new List<ApplicationRole>();
		foreach (string nombreRol in nombresRolesUsuario)
		{
			ApplicationRole rol = await _roleManager.FindByNameAsync(nombreRol);
			rolesUsuario.Add(rol);
		}

		return rolesUsuario;
	}

	public async Task<IdentityResult> ActualizarRolesUsuario(ApplicationUser usuario, IEnumerable<string> roles)
	{
		IList<string> rolesActuales = await _userManager.GetRolesAsync(usuario);

		IdentityResult rolesEliminados = await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);

		if (!rolesEliminados.Succeeded) return rolesEliminados;

		IdentityResult rolesAsignados = await _userManager.AddToRolesAsync(usuario, roles);

		return rolesAsignados;
	}

	public static IEnumerable<SelectListItem> CargarListaSeleccionTiposMaquina(IEnumerable<TipoMaquina> listaTiposMaquina)
	{
		IEnumerable<SelectListItem> listaSeleccionTiposMaquina = listaTiposMaquina.Select(p => new SelectListItem { Value = Convert.ToString(p.Id.ToString(), new CultureInfo("es-CR")), Text = p.Nombre }).ToList();
		return listaSeleccionTiposMaquina;
	}

	public static IEnumerable<SelectListItem> CargarListaSeleccionMaquinas(IEnumerable<Maquina> listaMaquinas)
	{
		IEnumerable<SelectListItem> listaSeleccionMaquinas = listaMaquinas.Select(p => new SelectListItem { Value = Convert.ToString(p.Id.ToString(), new CultureInfo("es-CR")), Text = p.Nombre }).ToList();
		return listaSeleccionMaquinas;
	}

	public static IEnumerable<SelectListItem> CargarListaSeleccionTiposEjercicio(IEnumerable<TipoEjercicio> listaTiposEjercicio)
	{
		IEnumerable<SelectListItem> listaSeleccionTiposEjercicio = listaTiposEjercicio.Select(p => new SelectListItem { Value = Convert.ToString(p.Id.ToString(), new CultureInfo("es-CR")), Text = p.Nombre }).ToList();
		return listaSeleccionTiposEjercicio;
	}

	public static IEnumerable<SelectListItem> CargarListaSeleccionEjercicios(IEnumerable<Ejercicio> listaEjercicios)
	{
		IEnumerable<SelectListItem> listaSeleccionEjercicios = listaEjercicios.Select(p => new SelectListItem { Value = Convert.ToString(p.Id.ToString(), new CultureInfo("es-CR")), Text = p.Nombre }).ToList();
		return listaSeleccionEjercicios;
	}

	public static IEnumerable<SelectListItem> CargarListaSeleccionGruposMusculares(IEnumerable<GrupoMuscular> listaGruposMusculares)
	{
		IEnumerable<SelectListItem> listaSeleccionGruposMusculares = listaGruposMusculares.Select(p => new SelectListItem { Value = Convert.ToString(p.Id.ToString(), new CultureInfo("es-CR")), Text = p.Nombre }).ToList();
		return listaSeleccionGruposMusculares;
	}

	public static IEnumerable<SelectListItem> CargarListaSeleccionTiposMedida(IEnumerable<TipoMedida> listaTiposMedida)
	{
		IEnumerable<SelectListItem> listaSeleccionTiposMedida = listaTiposMedida.Select(p => new SelectListItem { Value = Convert.ToString(p.Id.ToString(), new CultureInfo("es-CR")), Text = p.Nombre }).ToList();
		return listaSeleccionTiposMedida;
	}

	//public IEnumerable<SelectListItem> CargarListaSeleccionInstructores(IEnumerable<Instructor> listaInstructores)
	//{
	//	IEnumerable<SelectListItem> listaSeleccionInstructores = listaInstructores.Select(p => new SelectListItem { Value = Convert.ToString(p.Id.ToString(), new CultureInfo("es-CR")), Text = string.Format("{0} {1} {2}", p.Name, p.PrimerApellido, p.SegundoApellido) }).ToList();
	//	return listaSeleccionInstructores;
	//}

	//public IEnumerable<SelectListItem> CargarListaSeleccionClientes(IEnumerable<Cliente> listaClientes)
	//{
	//	IEnumerable<SelectListItem> listaSeleccionClientes = listaClientes.Select(p => new SelectListItem { Value = Convert.ToString(p.Id.ToString(), new CultureInfo("es-CR")), Text = string.Format("{0} {1} {2}", p.Nombre, p.PrimerApellido, p.SegundoApellido) }).ToList();
	//	return listaSeleccionClientes;
	//}

	// protected async Task CargarImagenUsuario(IEnumerable<Maquina> maquinas)
	// {
	// 	foreach (Maquina maquina in maquinas)
	// 	{
	// 		maquina.SetImagen(await ObtenerImagen(maquina.Id));
	// 	}
	// }

	// public async Task<Imagen> ObtenerImagen(Guid idObjeto)
	// {
	// 	Imagen imagen = await _repoImagenes.ReadByIdAsync(idObjeto);
	// 	return imagen;
	// }

	// public async Task<Imagen> GuardarImagenAsync(Guid idObjeto, IFormFile archivoSubido, string carpeta)
	// {
	// 	Uri uriImagen = GuardarImagenDisco(archivoSubido, carpeta);
	// 	Imagen imagen = new(idObjeto, uriImagen);
	// 	await _repoImagenes.CreateAsync(imagen);
	// 	return imagen;
	// }

	// public async Task<Imagen> ActualizarImagenAsync(Guid idObjeto, IFormFile archivoSubido, string carpeta)
	// {
	// 	Uri uriNuevaImagen = GuardarImagenDisco(archivoSubido, carpeta);
	// 	Imagen imagen = await _repoImagenes.ReadByIdAsync(idObjeto);
	// 	EliminarImagenDisco(imagen.URL);
	// 	imagen.EstablecerNuevaUri(uriNuevaImagen);
	// 	await _repoImagenes.UpdateAsync(imagen);
	// 	return imagen;
	// }

	// public async Task EliminarImagenAsync(Guid idObjeto)
	// {
	// 	Imagen imagen = await _repoImagenes.ReadByIdAsync(idObjeto);
	// 	EliminarImagenDisco(imagen.URL);
	// 	await _repoImagenes.DeleteAsync(idObjeto);
	// }

	private Uri GuardarImagenDisco(IFormFile archivoSubido, string carpeta)
	{
		ValidarArchivoSubido(archivoSubido);

		string relativePath = $"images\\{carpeta}";
		string absolutePath = Path.Combine(_environment.WebRootPath, relativePath);
		string relativePathWithFileName = $"{relativePath}/{Guid.NewGuid()}{Path.GetExtension(archivoSubido.FileName)}";
		string absolutePathWithFileName = Path.Combine(_environment.WebRootPath, relativePathWithFileName);

		if (!Directory.Exists(absolutePath)) Directory.CreateDirectory(absolutePath);

		using (Stream fileStream = new FileStream(absolutePathWithFileName, FileMode.Create))
		{
			archivoSubido.CopyTo(fileStream);
		}

		return new UriBuilder("https", "localhost", 44321, relativePathWithFileName).Uri;
	}

	private void EliminarImagenDisco(Uri uri)
	{
		string rutaImagen = Path.Combine(_environment.WebRootPath, uri.LocalPath.TrimStart('/').Replace("/", "\\"));
		if (IO.File.Exists(rutaImagen)) IO.File.Delete(rutaImagen);
	}

	private static void ValidarArchivoSubido(IFormFile archivoSubido)
	{
		// if (archivoSubido is null) throw new ArgumentNullException(paramName: nameof(Imagen), message: Messages.MensajeArchivoNulo);

		// TODO: Validar extension de archivo de imagen suministrado
	}
}