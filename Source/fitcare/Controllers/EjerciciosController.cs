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
	private readonly IManager<Ejercicio> _ejerciciosManager;
	private readonly IManager<TipoEjercicio> _tiposEjercicioManager;
	private readonly ILogger<EjerciciosController> _logger;

	public EjerciciosController(IManager<Ejercicio> ejerciciosManager,
								IManager<TipoEjercicio> tiposEjercicioManager,
								IDivisionTerritorialManager divisionTerritorialManager,
								ApplicationUserManager<ApplicationUser> userManager,
								RoleManager<ApplicationRole> roleManager,
								IConfiguration configuration,
								IHttpContextAccessor contextAccesor,
								ILogger<EjerciciosController> logger,
								IWebHostEnvironment environment)
	: base(divisionTerritorialManager, userManager, roleManager, configuration, contextAccesor, environment)
	{
		_ejerciciosManager = ejerciciosManager;
		_tiposEjercicioManager = tiposEjercicioManager;
		_logger = logger;
	}

	[HttpGet]
	public async Task<ActionResult> ListarEjercicios()
	{
		var ejercicios = await _ejerciciosManager.ReadAllAsync();
		var modelo = ejercicios.Select(x => new EjercicioViewModel(x)).ToList();
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> AgregarEjercicio()
	{
		ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicioManager.ReadAllAsync());
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> AgregarEjercicio(AgregarEjercicioViewModel modelo)
	{
		if (!ModelState.IsValid)
		{
			ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicioManager.ReadAllAsync());
			ModelState.AddModelError("", Messages.MensajeModeloInvalido);
			return View(modelo);
		}

		await _ejerciciosManager.CreateAsync(modelo.Entidad(), GetCurrentUser());
		return RedirectToAction(nameof(ListarEjercicios));
	}

	[HttpGet]
	public async Task<ActionResult> EditarEjercicio(string id)
	{
		var ejercicio = await _ejerciciosManager.ReadByIdAsync(new Guid(id));
		ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicioManager.ReadAllAsync());
		var modelo = new EditarEjercicioViewModel(ejercicio);
		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> EditarEjercicio(EditarEjercicioViewModel modelo)
	{
		if (!ModelState.IsValid)
		{
			ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicioManager.ReadAllAsync());
			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(Ejercicio)));
			return View(modelo);
		}

		await _ejerciciosManager.UpdateAsync(modelo.Entidad(), GetCurrentUser());
		return RedirectToAction(nameof(ListarEjercicios));
	}

	[HttpGet]
	public async Task<ActionResult> EliminarEjercicio(string id)
	{
		var ejercicio = await _ejerciciosManager.ReadByIdAsync(new Guid(id));
		var modelo = new EliminarEjercicioViewModel(ejercicio);
		return View(modelo);
	}

	[HttpPost]
	public async Task<ActionResult> EliminarEjercicio(EliminarEjercicioViewModel modelo)
	{
		if (!ModelState.IsValid)
		{
			ModelState.AddModelError("", Messages.MensajeErrorEliminar(nameof(Ejercicio)));
			return View(modelo);
		}

		await _ejerciciosManager.DeleteAsync(new Guid(modelo.Id));
		return RedirectToAction(nameof(ListarEjercicios));
	}

	[HttpGet]
	public async Task<JsonResult> DetalleEjercicio(string id)
	{
		var ejercicio = await _ejerciciosManager.ReadByIdAsync(new Guid(id));
		var modelo = new EjercicioViewModel(ejercicio);
		return Json(modelo);
	}

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
