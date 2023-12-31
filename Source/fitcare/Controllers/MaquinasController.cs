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
public class MaquinasController : BaseController
{
	private readonly IManager<TipoMaquina> _tiposMaquinaManager;
	private readonly IManager<Maquina> _maquinasManager;
	private readonly ILogger<MaquinasController> _logger;

	[HttpGet]
	public async Task<IActionResult> ListarMaquinas()
	{
		var maquinas = await _maquinasManager.ReadAllAsync();
		var modeloVista = maquinas.Select(x => new MaquinaViewModel(x)).ToList();
		return View(modeloVista);
	}

	[HttpGet]
	public async Task<IActionResult> AgregarMaquina()
	{
		ViewBag.ListaTiposMaquina = CargarListaSeleccionTiposMaquina(await _tiposMaquinaManager.ReadAllAsync());
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> AgregarMaquina(AgregarMaquinaViewModel modeloVista)
	{
		if (!ModelState.IsValid)
		{
			ViewBag.ListaTiposMaquina = CargarListaSeleccionTiposMaquina(await _tiposMaquinaManager.ReadAllAsync());
			ModelState.AddModelError("", Messages.MensajeModeloInvalido);
			return View(modeloVista);
		}

		await _maquinasManager.CreateAsync(modeloVista.Entidad(), CurrentUser);
		return RedirectToAction(nameof(ListarMaquinas));
	}

	[HttpGet]
	public async Task<IActionResult> EditarMaquina(string id)
	{
		var maquina = await _maquinasManager.ReadByIdAsync(new Guid(id));
		if (maquina == null) return NotFound();
		ViewBag.ListaTiposMaquina = CargarListaSeleccionTiposMaquina(await _tiposMaquinaManager.ReadAllAsync());
		var modelo = new EditarMaquinaViewModel(maquina);
		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> EditarMaquina(EditarMaquinaViewModel modeloVista)
	{
		if (!ModelState.IsValid)
		{
			ViewBag.ListaTiposMaquina = CargarListaSeleccionTiposMaquina(await _tiposMaquinaManager.ReadAllAsync());
			ModelState.AddModelError("", Messages.MensajeModeloInvalido);
			return View(modeloVista);
		}

		await _maquinasManager.UpdateAsync(modeloVista.Entidad(), CurrentUser);
		return RedirectToAction(nameof(ListarMaquinas));
	}

	[HttpGet]
	public async Task<IActionResult> EliminarMaquina(string id)
	{
		var maquina = await _maquinasManager.ReadByIdAsync(new Guid(id));
		if (maquina == null) return NotFound();
		EliminarMaquinaViewModel modeloVista = new(maquina);
		return View(modeloVista);
	}

	[HttpPost]
	public async Task<IActionResult> EliminarMaquina(EliminarMaquinaViewModel modelo)
	{
		if (!ModelState.IsValid)
		{
			ModelState.AddModelError("", Messages.MensajeErrorEliminar(nameof(Maquina)));
			return View(modelo);
		}

		await _maquinasManager.DeleteAsync(new Guid(modelo.Id));
		return RedirectToAction(nameof(ListarMaquinas));
	}

	[HttpGet]
	public async Task<JsonResult> DetalleMaquina(string id)
	{
		Maquina maquina = await _maquinasManager.ReadByIdAsync(new Guid(id));
		var modelo = new MaquinaViewModel(maquina);
		return Json(modelo);
	}

	public MaquinasController(IManager<TipoMaquina> tiposMaquinaManager,
							  IManager<Maquina> maquinasManager,
							  IDivisionTerritorialManager divisionTerritorialManager,
							  ApplicationUserManager<ApplicationUser> userManager,
							  RoleManager<ApplicationRole> roleManager,
							  IConfiguration configuration,
							  IHttpContextAccessor contextAccesor,
							  ILogger<MaquinasController> logger,
							  IWebHostEnvironment environment)
	: base(divisionTerritorialManager, userManager, roleManager, configuration, contextAccesor, environment)
	{
		_tiposMaquinaManager = tiposMaquinaManager;
		_maquinasManager = maquinasManager;
		_logger = logger;
	}

	[HttpGet]
	public async Task<ActionResult> ListarTiposMaquina()
	{
		IEnumerable<TipoMaquina> listaTiposMaquina = await _tiposMaquinaManager.ReadAllAsync();
		IEnumerable<TipoMaquinaViewModel> modelo = listaTiposMaquina.Select(x => new TipoMaquinaViewModel(x)).ToList();
		return View(modelo);
	}

	[HttpGet]
	public ActionResult AgregarTipoMaquina()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> AgregarTipoMaquina(AgregarTipoMaquinaViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			await _tiposMaquinaManager.CreateAsync(modelo.Entidad(), GetCurrentUser());
			return RedirectToAction(nameof(ListarTiposMaquina));
		}

		ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(TipoMaquina)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> EditarTipoMaquina(string id)
	{
		TipoMaquina tipoMaquina = await _tiposMaquinaManager.ReadByIdAsync(new Guid(id));
		if (tipoMaquina == null) return NotFound();
		var modelo = new EditarTipoMaquinaViewModel(tipoMaquina);
		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> EditarTipoMaquina(EditarTipoMaquinaViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			TipoMaquina tipoMaquina = modelo.Entidad();
			await _tiposMaquinaManager.UpdateAsync(tipoMaquina, GetCurrentUser());
			return RedirectToAction(nameof(ListarTiposMaquina));
		}

		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoMaquina)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> EliminarTipoMaquina(string id)
	{
		TipoMaquina tipoMaquina = await _tiposMaquinaManager.ReadByIdAsync(new Guid(id));
		if (tipoMaquina == null) return NotFound();
		var modelo = new EliminarTipoMaquinaViewModel(tipoMaquina);
		return View(modelo);
	}

	[HttpPost]
	public async Task<ActionResult> EliminarTipoMaquina(EliminarTipoMaquinaViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			await _tiposMaquinaManager.DeleteAsync(new Guid(modelo.Id));
			return RedirectToAction(nameof(ListarTiposMaquina));
		}

		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoMaquina)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<JsonResult> DetalleTipoMaquina(string id)
	{
		TipoMaquina tipoMaquina = await _tiposMaquinaManager.ReadByIdAsync(new Guid(id));
		var modelo = new TipoMaquinaViewModel(tipoMaquina);
		return Json(modelo);
	}
}
