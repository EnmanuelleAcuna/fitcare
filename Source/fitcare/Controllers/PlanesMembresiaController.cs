using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models;
using fitcare.Models.Entities;
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
public class PlanesMembresiaController : BaseController
{
	private readonly IBaseCore<PlanMembresia> _planesMembresia;
	private readonly ILogger<PlanesMembresiaController> _logger;

	public PlanesMembresiaController(IBaseCore<PlanMembresia> planesMembresia,
									 IDivisionTerritorial divisionTerritorial,
									 ApplicationUserManager<ApplicationUser> userManager,
									 RoleManager<ApplicationRole> roleManager,
									 IConfiguration configuration,
									 IHttpContextAccessor contextAccesor,
									 ILogger<PlanesMembresiaController> logger,
									 IWebHostEnvironment environment)
	: base(divisionTerritorial, userManager, roleManager, configuration, contextAccesor, environment)
	{
		_planesMembresia = planesMembresia;
		_logger = logger;
	}

	[HttpGet]
	public ActionResult Listar()
	{
		return View();
	}

	[HttpGet]
	public async Task<JsonResult> ObtenerPlanes()
	{
		try
		{
			IEnumerable<PlanMembresia> listaPlanes = await _planesMembresia.ReadAllAsync();
			IEnumerable<PlanMembresiaViewModel> modelo = listaPlanes.Select(x => new PlanMembresiaViewModel(x)).ToList();
			return Json(new { success = true, data = modelo });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al obtener planes de membresía");
			return Json(new { success = false, message = "Error al obtener los planes" });
		}
	}

	[HttpGet]
	public async Task<JsonResult> Obtener(string id)
	{
		try
		{
			PlanMembresia plan = await _planesMembresia.ReadByIdAsync(new Guid(id));
			var modelo = new PlanMembresiaViewModel(plan);
			return Json(new { success = true, modelo = modelo });
		}
		catch (KeyNotFoundException)
		{
			return Json(new { success = false, message = "Plan de membresía no encontrado" });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al obtener plan de membresía");
			return Json(new { success = false, message = "Error al obtener el plan" });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<JsonResult> AgregarAjax([FromBody] AgregarPlanMembresiaViewModel modelo)
	{
		try
		{
			if (ModelState.IsValid)
			{
				await _planesMembresia.CreateAsync(modelo.Entidad(), GetCurrentUser());
				return Json(new { success = true, message = "Plan de membresía agregado exitosamente" });
			}

			var errors = ModelState
				.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(
					kvp => kvp.Key,
					kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
				);

			return Json(new { success = false, errors = errors });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al agregar plan de membresía");
			return Json(new { success = false, message = "Error al agregar el plan de membresía" });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<JsonResult> EditarAjax([FromBody] EditarPlanMembresiaViewModel modelo)
	{
		try
		{
			if (ModelState.IsValid)
			{
				await _planesMembresia.UpdateAsync(modelo.Entidad(), GetCurrentUser());
				return Json(new { success = true, message = "Plan de membresía actualizado exitosamente" });
			}

			var errors = ModelState
				.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(
					kvp => kvp.Key,
					kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
				);

			return Json(new { success = false, errors = errors });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al actualizar plan de membresía");
			return Json(new { success = false, message = "Error al actualizar el plan de membresía" });
		}
	}

}
