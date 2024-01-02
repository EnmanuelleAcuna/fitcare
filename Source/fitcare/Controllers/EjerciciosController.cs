using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using fitcare.Models.Extras;
using fitcare.Models.Identity;
using fitcare.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace fitcare.Controllers;

[Authorize]
public class EjerciciosController : BaseController
{
	private readonly IManager<TipoEjercicio> _tiposEjercicioManager;
	private readonly ILogger<EjerciciosController> _logger;

	public EjerciciosController(IManager<TipoEjercicio> tiposEjercicioManager,
								IDivisionTerritorialManager divisionTerritorialManager,
								ApplicationUserManager<ApplicationUser> userManager,
								RoleManager<ApplicationRole> roleManager,
								IConfiguration configuration,
								IHttpContextAccessor contextAccesor,
								ILogger<EjerciciosController> logger,
								IWebHostEnvironment environment)
	: base(divisionTerritorialManager, userManager, roleManager, configuration, contextAccesor, environment)
	{
		_tiposEjercicioManager = tiposEjercicioManager;
		_logger = logger;
	}

	// [HttpGet]
	// public async Task<ActionResult> Inicio()
	// {
	// 	IEnumerable<Ejercicio> listaEjercicios = await _repoEjercicios.ReadAllAsync();
	// 	IEnumerable<InicioEjercicioViewModel> modelo = listaEjercicios.Select(x => new InicioEjercicioViewModel(x)).ToList();
	// 	return View(modelo);
	// }

	// [HttpGet]
	// public async Task<ActionResult> Nuevo()
	// {
	// 	await CargarViewBags();
	// 	return View();
	// }

	// [HttpPost]
	// [ValidateAntiForgeryToken]
	// public async Task<ActionResult> Nuevo(NuevoEjercicioViewModel modelo, IFormCollection collection)
	// {
	// 	if (ModelState.IsValid)
	// 	{
	// 		modelo.SetDependencies(_repoAccesorios, _repoMaquinas, _repoGruposMusculares, _repoTiposEjercicio);
	// 		Ejercicio ejercicio = await modelo.Entidad(collection);
	// 		await _repoEjercicios.CreateAsync(ejercicio);
	// 		return RedirectToAction("Inicio");
	// 	}

	// 	// Si se llega a este punto, hubo un error
	// 	await CargarViewBags();
	// 	ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Ejercicio)));
	// 	return View(modelo);
	// }

	// [HttpGet]
	// public async Task<ActionResult> Editar(string id)
	// {
	// 	Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(id));
	// 	EditarEjercicioViewModel modelo = new(ejercicio);

	// 	await CargarViewBags();
	// 	return View(modelo);
	// }

	// [HttpPost]
	// [ValidateAntiForgeryToken]
	// public async Task<ActionResult> Editar(EditarEjercicioViewModel modelo, IFormCollection collection)
	// {
	// 	if (ModelState.IsValid)
	// 	{
	// 		modelo.SetDependencies(_repoAccesorios, _repoMaquinas, _repoGruposMusculares, _repoTiposEjercicio);
	// 		Ejercicio ejercicio = await modelo.Entidad(collection);
	// 		await _repoEjercicios.UpdateAsync(ejercicio);
	// 		return RedirectToAction("Inicio");
	// 	}

	// 	// Si se llega a este punto, hubo un error
	// 	await CargarViewBags();
	// 	ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(Ejercicio)));
	// 	return View(modelo);
	// }

	// [HttpGet]
	// public async Task<ActionResult> Eliminar(string id)
	// {
	// 	Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(id));
	// 	EditarEjercicioViewModel modelo = new(ejercicio);

	// 	return View(modelo);
	// }

	// [HttpPost]
	// public async Task<ActionResult> Eliminar(EditarEjercicioViewModel modelo)
	// {
	// 	Guid idObjeto = Factory.SetGuid(modelo.Id);
	// 	await _repoEjercicios.DeleteAsync(idObjeto);

	// 	return RedirectToAction("Inicio");
	// }

	// [HttpGet]
	// public async Task<JsonResult> Detalle(string id)
	// {
	// 	Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(id));
	// 	Bitacora registroBitacoraAgregar = await DAOBitacora.ReadNewestByIdObjetoAsync(ejercicio.Id, AccionesBitacora.Agregar);
	// 	Bitacora registroBitacoraModificar = await DAOBitacora.ReadNewestByIdObjetoAsync(ejercicio.Id, AccionesBitacora.Modificar);
	// 	EjerciciosViewModel modelo = new(ejercicio, registroBitacoraAgregar, registroBitacoraModificar);


	// 	//Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(id));
	// 	DetalleEjercicioViewModel modeloVista = new(ejercicio);

	// 	return Json(modeloVista);
	// }

	// private async Task CargarViewBags()
	// {
	// 	ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _repoTiposEjercicio.ReadAllAsync());
	// 	ViewBag.Maquinas = CargarListaSeleccionMaquinas(await _repoMaquinas.ReadAllAsync());
	// 	ViewBag.GruposMusculares = CargarListaSeleccionGruposMusculares(await _repoGruposMusculares.ReadAllAsync());
	// }

	[HttpGet]
	public async Task<ActionResult> ListarTiposEjercicio()
	{
		IEnumerable<TipoEjercicio> listaTiposEjercicio = await _tiposEjercicioManager.ReadAllAsync();
		IEnumerable<TipoEjercicioViewModel> modelo = listaTiposEjercicio.Select(x => new TipoEjercicioViewModel(x)).ToList();
		return View(modelo);
	}

	[HttpGet]
	public ActionResult AgregarTipoEjercicio()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> AgregarTipoEjercicio(AgregarTipoEjercicioViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			await _tiposEjercicioManager.CreateAsync(modelo.Entidad(), GetCurrentUser());
			return RedirectToAction(nameof(ListarTiposEjercicio));
		}

		ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(TipoEjercicio)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> EditarTipoEjercicio(string id)
	{
		TipoEjercicio tipoEjercicio = await _tiposEjercicioManager.ReadByIdAsync(new Guid(id));
		if (tipoEjercicio == null) return NotFound();
		EditarTipoEjercicioViewModel modelo = new(tipoEjercicio);
		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> EditarTipoEjercicio(EditarTipoEjercicioViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			TipoEjercicio tipoEjercicio = modelo.Entidad();
			await _tiposEjercicioManager.UpdateAsync(tipoEjercicio, GetCurrentUser());
			return RedirectToAction(nameof(ListarTiposEjercicio));
		}

		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoEjercicio)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> EliminarTipoEjercicio(string id)
	{
		TipoEjercicio tipoEjercicio = await _tiposEjercicioManager.ReadByIdAsync(new Guid(id));
		if (tipoEjercicio == null) return NotFound();
		EliminarTipoEjercicioViewModel modelo = new(tipoEjercicio);
		return View(modelo);
	}

	[HttpPost]
	public async Task<ActionResult> EliminarTipoEjercicio(EliminarTipoEjercicioViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			await _tiposEjercicioManager.DeleteAsync(new Guid(modelo.IdTipoEjercicio));
			return RedirectToAction(nameof(ListarTiposEjercicio));
		}

		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoEjercicio)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<JsonResult> DetalleTipoEjercicio(string id)
	{
		TipoEjercicio tipoEjercicio = await _tiposEjercicioManager.ReadByIdAsync(new Guid(id));
		var modelo = new TipoEjercicioViewModel(tipoEjercicio);
		return Json(modelo);
	}
}
