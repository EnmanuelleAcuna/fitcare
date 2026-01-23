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
	private readonly IGeneradorCodigo<Ejercicio> _generadorCodigoEjercicio;
	private readonly IBaseCore<TipoEjercicio> _tiposEjercicio;
	private readonly IGeneradorCodigo<TipoEjercicio> _generadorCodigoTipoEjercicio;
	private readonly IBaseCore<GrupoMuscular> _gruposMusculares;
	private readonly IBaseCore<Maquina> _maquinas;
	private readonly ILogger<EjerciciosController> _logger;

	public EjerciciosController(IBaseCore<Ejercicio> ejercicios,
								IGeneradorCodigo<Ejercicio> generadorCodigoEjercicio,
								IBaseCore<TipoEjercicio> tiposEjercicio,
								IGeneradorCodigo<TipoEjercicio> generadorCodigoTipoEjercicio,
								IBaseCore<GrupoMuscular> gruposMusculares,
								IBaseCore<Maquina> maquinas,
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
		_generadorCodigoEjercicio = generadorCodigoEjercicio;
		_tiposEjercicio = tiposEjercicio;
		_generadorCodigoTipoEjercicio = generadorCodigoTipoEjercicio;
		_gruposMusculares = gruposMusculares;
		_maquinas = maquinas;
		_logger = logger;
	}

	[HttpGet]
	public async Task<ActionResult> Ejercicios()
	{
		var ejercicios = await _ejercicios.ReadAllAsync();
		var modelo = ejercicios.Select(x => new EjercicioViewModel(x)).ToList();
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> AgregarEjercicio()
	{
		var modelo = await AgregarEjercicioViewModel.CrearAsync(_generadorCodigoEjercicio);
		ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicio.ReadAllAsync());
		ViewBag.ListaGruposMusculares = await CargarListaGruposMusculares();
		ViewBag.ListaMaquinas = await CargarListaMaquinas();
		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> AgregarEjercicio(AgregarEjercicioViewModel modelo)
	{
		if (!ModelState.IsValid)
		{
			ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicio.ReadAllAsync());
			ViewBag.ListaGruposMusculares = await CargarListaGruposMusculares();
			ViewBag.ListaMaquinas = await CargarListaMaquinas();
			ModelState.AddModelError("", Messages.MensajeModeloInvalido);
			return View(modelo);
		}

		try
		{
			var ejercicio = modelo.Entidad();

			if (modelo.IdsGruposMusculares != null && modelo.IdsGruposMusculares.Any())
			{
				foreach (var idGrupo in modelo.IdsGruposMusculares)
				{
					var grupo = await _gruposMusculares.ReadByIdAsync(new Guid(idGrupo));
					if (grupo != null)
						ejercicio.GruposMusculares.Add(grupo);
				}
			}

			if (modelo.IdsMaquinas != null && modelo.IdsMaquinas.Any())
			{
				foreach (var idMaquina in modelo.IdsMaquinas)
				{
					var maquina = await _maquinas.ReadByIdAsync(new Guid(idMaquina));
					if (maquina != null)
						ejercicio.Maquinas.Add(maquina);
				}
			}

			await _ejercicios.CreateAsync(ejercicio, GetCurrentUser());
			TempData["ToastMessage"] = "Ejercicio agregado exitosamente";
			TempData["ToastType"] = "success";
			return RedirectToAction(nameof(Ejercicios));
		}
		catch (Microsoft.EntityFrameworkCore.DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE") == true)
		{
			await modelo.RegenerarCodigoAsync(_generadorCodigoEjercicio);
			ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicio.ReadAllAsync());
			ViewBag.ListaGruposMusculares = await CargarListaGruposMusculares();
			ViewBag.ListaMaquinas = await CargarListaMaquinas();
			ModelState.AddModelError("", "El código fue asignado a otro registro. Se ha generado uno nuevo.");
			return View(modelo);
		}
	}

	[HttpGet]
	public async Task<ActionResult> EditarEjercicio(string id)
	{
		var ejercicio = await _ejercicios.ReadByIdAsync(new Guid(id));
		ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _tiposEjercicio.ReadAllAsync());
		ViewBag.ListaGruposMusculares = await CargarListaGruposMusculares();
		ViewBag.ListaMaquinas = await CargarListaMaquinas();
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
			ViewBag.ListaGruposMusculares = await CargarListaGruposMusculares();
			ViewBag.ListaMaquinas = await CargarListaMaquinas();
			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(Ejercicio)));
			return View(modelo);
		}

		var ejercicio = modelo.Entidad();

		// Agregar grupos musculares seleccionados
		if (modelo.IdsGruposMusculares != null && modelo.IdsGruposMusculares.Any())
		{
			foreach (var idGrupo in modelo.IdsGruposMusculares)
			{
				var grupo = await _gruposMusculares.ReadByIdAsync(new Guid(idGrupo));
				if (grupo != null)
					ejercicio.GruposMusculares.Add(grupo);
			}
		}

		// Agregar máquinas seleccionadas
		if (modelo.IdsMaquinas != null && modelo.IdsMaquinas.Any())
		{
			foreach (var idMaquina in modelo.IdsMaquinas)
			{
				var maquina = await _maquinas.ReadByIdAsync(new Guid(idMaquina));
				if (maquina != null)
					ejercicio.Maquinas.Add(maquina);
			}
		}

		await _ejercicios.UpdateAsync(ejercicio, GetCurrentUser());
		TempData["ToastMessage"] = "Ejercicio actualizado exitosamente";
		TempData["ToastType"] = "success";
		return RedirectToAction(nameof(Ejercicios));
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
			return RedirectToAction(nameof(Ejercicios));
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
	public async Task<ActionResult> TiposEjercicio()
	{
		IEnumerable<TipoEjercicio> listaTiposEjercicio = await _tiposEjercicio.ReadAllAsync();
		IEnumerable<TipoEjercicioViewModel> modelo = listaTiposEjercicio.Select(x => new TipoEjercicioViewModel(x)).ToList();
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> AgregarTipoEjercicio()
	{
		var modelo = await AgregarTipoEjercicioViewModel.CrearAsync(_generadorCodigoTipoEjercicio);
		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> AgregarTipoEjercicio(AgregarTipoEjercicioViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			try
			{
				await _tiposEjercicio.CreateAsync(modelo.Entidad(), GetCurrentUser());
				TempData["ToastMessage"] = "Tipo de ejercicio agregado exitosamente";
				TempData["ToastType"] = "success";
				return RedirectToAction(nameof(TiposEjercicio));
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE") == true)
			{
				await modelo.RegenerarCodigoAsync(_generadorCodigoTipoEjercicio);
				ModelState.AddModelError("", "El código fue asignado a otro registro. Se ha generado uno nuevo.");
				return View(modelo);
			}
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
			try
			{
				TipoEjercicio tipoEjercicio = modelo.Entidad();
				await _tiposEjercicio.UpdateAsync(tipoEjercicio, GetCurrentUser());
				TempData["ToastMessage"] = "Tipo de ejercicio actualizado exitosamente";
				TempData["ToastType"] = "success";
				return RedirectToAction(nameof(TiposEjercicio));
			}
			catch (InvalidOperationException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(modelo);
			}
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
				return RedirectToAction(nameof(TiposEjercicio));
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

	// Métodos helper para cargar listas
	private async Task<List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>> CargarListaGruposMusculares()
	{
		var grupos = await _gruposMusculares.ReadAllAsync();
		return grupos
			.Where(g => g.Estado)
			.OrderBy(g => g.Nombre)
			.Select(g => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			{
				Value = g.Id.ToString(),
				Text = g.Nombre
			}).ToList();
	}

	private async Task<List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>> CargarListaMaquinas()
	{
		var maquinas = await _maquinas.ReadAllAsync();
		return maquinas
			.Where(m => m.Estado)
			.OrderBy(m => m.Nombre)
			.Select(m => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			{
				Value = m.Id.ToString(),
				Text = m.Nombre
			}).ToList();
	}
}
