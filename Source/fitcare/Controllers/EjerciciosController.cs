using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models;
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
	private readonly IBaseCore<Ejercicio> _ejercicios;
	private readonly IBaseCore<TipoEjercicio> _tiposEjercicio;
	private readonly ILogger<EjerciciosController> _logger;

	public EjerciciosController(IBaseCore<Ejercicio> ejercicios,
								IBaseCore<TipoEjercicio> tiposEjercicio,
								IDivisionTerritorial divisionTerritorial,
								ApplicationUserManager<ApplicationUser> userManager,
								RoleManager<ApplicationRole> roleManager,
								IConfiguration configuration,
								IHttpContextAccessor contextAccesor,
								ILogger<EjerciciosController> logger,
								IWebHostEnvironment environment)
	: base(divisionTerritorial, userManager, roleManager, configuration, contextAccesor, environment)
	{
		_ejercicios = ejercicios;
		_tiposEjercicio = tiposEjercicio;
		_logger = logger;
	}

	[HttpGet]
	public async Task<ActionResult> ListarEjercicios()
	{
		var ejercicios = await _ejercicios.ReadAllAsync();
		var modelo = ejercicios.Select(x => new EjercicioViewModel(x)).ToList();
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> AgregarEjercicio()
	{
		ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicio.ReadAllAsync());
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> AgregarEjercicio(AgregarEjercicioViewModel modelo)
	{
		if (!ModelState.IsValid)
		{
			ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicio.ReadAllAsync());
			ModelState.AddModelError("", Messages.MensajeModeloInvalido);
			return View(modelo);
		}

		await _ejercicios.CreateAsync(modelo.Entidad(), GetCurrentUser());
		TempData["ToastMessage"] = "Ejercicio agregado exitosamente";
		TempData["ToastType"] = "success";
		return RedirectToAction(nameof(ListarEjercicios));
	}

	[HttpGet]
	public async Task<ActionResult> EditarEjercicio(string id)
	{
		var ejercicio = await _ejercicios.ReadByIdAsync(new Guid(id));
		ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicio.ReadAllAsync());
		var modelo = new EditarEjercicioViewModel(ejercicio);
		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> EditarEjercicio(EditarEjercicioViewModel modelo)
	{
		if (!ModelState.IsValid)
		{
			ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicio.ReadAllAsync());
			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(Ejercicio)));
			return View(modelo);
		}

		await _ejercicios.UpdateAsync(modelo.Entidad(), GetCurrentUser());
		TempData["ToastMessage"] = "Ejercicio actualizado exitosamente";
		TempData["ToastType"] = "success";
		return RedirectToAction(nameof(ListarEjercicios));
	}

	[HttpGet]
	public async Task<ActionResult> EliminarEjercicio(string id)
	{
		var ejercicio = await _ejercicios.ReadByIdAsync(new Guid(id));
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

		try
		{
			await _ejercicios.DeleteAsync(new Guid(modelo.Id));
			TempData["ToastMessage"] = "Ejercicio eliminado exitosamente";
			TempData["ToastType"] = "success";
			return RedirectToAction(nameof(ListarEjercicios));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al eliminar ejercicio");
			ModelState.AddModelError("", "Error al eliminar el ejercicio. Puede estar siendo utilizado en otro registro.");
			return View(modelo);
		}
	}

	[HttpGet]
	public async Task<JsonResult> DetalleEjercicio(string id)
	{
		var ejercicio = await _ejercicios.ReadByIdAsync(new Guid(id));
		var modelo = new EjercicioViewModel(ejercicio);
		return Json(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> ListarTiposEjercicio()
	{
		IEnumerable<TipoEjercicio> listaTiposEjercicio = await _tiposEjercicio.ReadAllAsync();
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
			await _tiposEjercicio.CreateAsync(modelo.Entidad(), GetCurrentUser());
			TempData["ToastMessage"] = "Tipo de ejercicio agregado exitosamente";
			TempData["ToastType"] = "success";
			return RedirectToAction(nameof(ListarTiposEjercicio));
		}

		ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(TipoEjercicio)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> EditarTipoEjercicio(string id)
	{
		TipoEjercicio tipoEjercicio = await _tiposEjercicio.ReadByIdAsync(new Guid(id));
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
			await _tiposEjercicio.UpdateAsync(tipoEjercicio, GetCurrentUser());
			TempData["ToastMessage"] = "Tipo de ejercicio actualizado exitosamente";
			TempData["ToastType"] = "success";
			return RedirectToAction(nameof(ListarTiposEjercicio));
		}

		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoEjercicio)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> EliminarTipoEjercicio(string id)
	{
		TipoEjercicio tipoEjercicio = await _tiposEjercicio.ReadByIdAsync(new Guid(id));
		if (tipoEjercicio == null) return NotFound();
		EliminarTipoEjercicioViewModel modelo = new(tipoEjercicio);
		return View(modelo);
	}

	[HttpPost]
	public async Task<ActionResult> EliminarTipoEjercicio(EliminarTipoEjercicioViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			try
			{
				await _tiposEjercicio.DeleteAsync(new Guid(modelo.IdTipoEjercicio));
				TempData["ToastMessage"] = "Tipo de ejercicio eliminado exitosamente";
				TempData["ToastType"] = "success";
				return RedirectToAction(nameof(ListarTiposEjercicio));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error al eliminar tipo de ejercicio");
				ModelState.AddModelError("", "Error al eliminar el tipo de ejercicio. Puede estar siendo utilizado en otro registro.");
				return View(modelo);
			}
		}

		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoEjercicio)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<JsonResult> DetalleTipoEjercicio(string id)
	{
		TipoEjercicio tipoEjercicio = await _tiposEjercicio.ReadByIdAsync(new Guid(id));
		var modelo = new TipoEjercicioViewModel(tipoEjercicio);
		return Json(modelo);
	}
}
