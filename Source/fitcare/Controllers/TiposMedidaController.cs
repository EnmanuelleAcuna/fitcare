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
public class TiposMedidaController : BaseController
{
	private readonly IBaseCore<TipoMedida> _tiposMedida;
	private readonly IGeneradorCodigo<TipoMedida> _generadorCodigo;
	private readonly ILogger<TiposMedidaController> _logger;

	public TiposMedidaController(IBaseCore<TipoMedida> tiposMedida,
								 IGeneradorCodigo<TipoMedida> generadorCodigo,
								 IDivisionTerritorial divisionTerritorial,
								 ApplicationUserManager<ApplicationUser> userManager,
								 RoleManager<ApplicationRole> roleManager,
								 IConfiguration configuration,
								 IHttpContextAccessor contextAccesor,
								 ILogger<TiposMedidaController> logger,
								 IWebHostEnvironment environment)
	: base(divisionTerritorial, userManager, roleManager, configuration, contextAccesor, environment)
	{
		_tiposMedida = tiposMedida;
		_generadorCodigo = generadorCodigo;
		_logger = logger;
	}

	[HttpGet]
	public ActionResult Listar()
	{
		return View();
	}

	[HttpGet]
	public async Task<JsonResult> ObtenerTiposMedida()
	{
		try
		{
			IEnumerable<TipoMedida> listaTiposMedida = await _tiposMedida.ReadAllAsync();
			IEnumerable<TipoMedidaViewModel> modelo = listaTiposMedida.Select(x => new TipoMedidaViewModel(x)).ToList();

			return Json(new { data = modelo });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al obtener tipos de medida para DataTables");
			return Json(new { data = new List<TipoMedidaViewModel>() });
		}
	}

	[HttpGet]
	public async Task<JsonResult> Agregar()
	{
		try
		{
			var modelo = await AgregarTipoMedidaViewModel.CrearAsync(_generadorCodigo);
			return Json(new { success = true, modelo = modelo });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al obtener modelo de agregar tipo de medida");
			return Json(new { success = false, message = "Error al obtener el modelo" });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<JsonResult> AgregarAjax([FromBody] AgregarTipoMedidaViewModel modelo)
	{
		try
		{
			if (ModelState.IsValid)
			{
				await _tiposMedida.CreateAsync(modelo.Entidad(), GetCurrentUser());
				return Json(new { success = true, message = "Tipo de medida agregado exitosamente" });
			}

			// Retornar errores de validación
			var errors = ModelState
				.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(
					kvp => kvp.Key,
					kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
				);

			return Json(new { success = false, errors = errors });
		}
		catch (Microsoft.EntityFrameworkCore.DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE") == true)
		{
			await modelo.RegenerarCodigoAsync(_generadorCodigo);
			return Json(new
			{
				success = false,
				codigoColision = true,
				modelo = modelo,
				message = "El código fue asignado a otro registro. Se ha generado uno nuevo."
			});
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al agregar tipo de medida mediante AJAX");
			return Json(new { success = false, message = "Error al agregar el tipo de medida" });
		}
	}

	[HttpGet]
	public async Task<JsonResult> Editar(string id)
	{
		try
		{
			TipoMedida tipoMedida = await _tiposMedida.ReadByIdAsync(new Guid(id));
			var modelo = new EditarTipoMedidaViewModel(tipoMedida);
			return Json(new { success = true, modelo = modelo });
		}
		catch (KeyNotFoundException)
		{
			return Json(new { success = false, message = "Tipo de medida no encontrado" });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al obtener tipo de medida para editar");
			return Json(new { success = false, message = "Error al obtener el tipo de medida" });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<JsonResult> EditarAjax([FromBody] EditarTipoMedidaViewModel modelo)
	{
		try
		{
			if (ModelState.IsValid)
			{
				await _tiposMedida.UpdateAsync(modelo.Entidad(), GetCurrentUser());
				return Json(new { success = true, message = "Tipo de medida actualizado exitosamente" });
			}

			// Retornar errores de validación
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
			_logger.LogError(ex, "Error al actualizar tipo de medida mediante AJAX");
			return Json(new { success = false, message = "Error al actualizar el tipo de medida" });
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<JsonResult> EliminarAjax([FromBody] EliminarTipoMedidaViewModel modelo)
	{
		try
		{
			if (string.IsNullOrEmpty(modelo.IdTipoMedida))
			{
				return Json(new { success = false, message = "ID no válido" });
			}

			await _tiposMedida.DeleteAsync(new Guid(modelo.IdTipoMedida));
			return Json(new { success = true, message = "Tipo de medida eliminado exitosamente" });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al eliminar tipo de medida mediante AJAX");
			return Json(new { success = false, message = "Error al eliminar el tipo de medida. Puede estar siendo utilizado en otro registro." });
		}
	}

	[HttpGet]
	public async Task<JsonResult> Detalle(string id)
	{
		TipoMedida tipoMedida = await _tiposMedida.ReadByIdAsync(new Guid(id));
		var modelo = new TipoMedidaViewModel(tipoMedida);
		return Json(modelo);
	}
}
