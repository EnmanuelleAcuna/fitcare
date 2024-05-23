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

namespace fitcare.Controllers
{
	[Authorize]
	public class TiposMedidaController : BaseController
	{
		private readonly IManager<TipoMedida> _tiposMedidaManager;
		private readonly ILogger<TiposMedidaController> _logger;

		public TiposMedidaController(IManager<TipoMedida> tiposMedidaManager,
									 IDivisionTerritorialManager divisionTerritorialManager,
									 ApplicationUserManager<ApplicationUser> userManager,
									 RoleManager<ApplicationRole> roleManager,
									 IConfiguration configuration,
									 IHttpContextAccessor contextAccesor,
									 ILogger<TiposMedidaController> logger,
									 IWebHostEnvironment environment)
		: base(divisionTerritorialManager, userManager, roleManager, configuration, contextAccesor, environment)
		{
			_tiposMedidaManager = tiposMedidaManager;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult> Listar()
		{
			IEnumerable<TipoMedida> listaTiposMedida = await _tiposMedidaManager.ReadAllAsync();
			IEnumerable<TipoMedidaViewModel> modelo = listaTiposMedida.Select(x => new TipoMedidaViewModel(x)).ToList();
			return View(modelo);
		}

		[HttpGet]
		public ActionResult Agregar()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Agregar(AgregarTipoMedidaViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _tiposMedidaManager.CreateAsync(modelo.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(Listar));
			}

			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(TipoMedida)));
			return View(modelo);
		}

		[HttpGet]
		public async Task<ActionResult> Editar(string id)
		{
			TipoMedida tipoMedida = await _tiposMedidaManager.ReadByIdAsync(new Guid(id));
			if (tipoMedida == null) return NotFound();
			EditarTipoMedidaViewModel Modelo = new(tipoMedida);
			return View(Modelo);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Editar(EditarTipoMedidaViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _tiposMedidaManager.UpdateAsync(modelo.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(Listar));
			}

			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoMedida)));
			return View(modelo);
		}

		[HttpGet]
		public async Task<ActionResult> Eliminar(string id)
		{
			TipoMedida tipoMedida = await _tiposMedidaManager.ReadByIdAsync(new Guid(id));
			if (tipoMedida == null) return NotFound();
			EliminarTipoMedidaViewModel modelo = new(tipoMedida);
			return View(modelo);
		}

		[HttpPost]
		public async Task<ActionResult> Eliminar(EliminarTipoMedidaViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _tiposMedidaManager.DeleteAsync(new Guid(modelo.IdTipoMedida));
				return RedirectToAction(nameof(Listar));
			}

			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoEjercicio)));
			return View(modelo);
		}

		[HttpGet]
		public async Task<JsonResult> Detalle(string id)
		{
			TipoMedida tipoMedida = await _tiposMedidaManager.ReadByIdAsync(new Guid(id));
			var modelo = new TipoMedidaViewModel(tipoMedida);
			return Json(modelo);
		}
	}
}
