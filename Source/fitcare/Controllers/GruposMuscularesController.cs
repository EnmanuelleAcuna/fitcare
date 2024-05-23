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
	public class GruposMuscularesController : BaseController
	{
		private readonly IManager<GrupoMuscular> _gruposMuscularesManager;
		private readonly ILogger<GruposMuscularesController> _logger;

		public GruposMuscularesController(IManager<GrupoMuscular> gruposMuscularesManager,
									 	  IDivisionTerritorialManager divisionTerritorialManager,
									 	  ApplicationUserManager<ApplicationUser> userManager,
										  RoleManager<ApplicationRole> roleManager,
										  IConfiguration configuration,
										  IHttpContextAccessor contextAccesor,
										  ILogger<GruposMuscularesController> logger,
										  IWebHostEnvironment environment)
		: base(divisionTerritorialManager, userManager, roleManager, configuration, contextAccesor, environment)
		{
			_gruposMuscularesManager = gruposMuscularesManager;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult> Listar()
		{
			IEnumerable<GrupoMuscular> listaGruposMusculares = await _gruposMuscularesManager.ReadAllAsync();
			IEnumerable<GrupoMuscularViewModel> modelo = listaGruposMusculares.Select(x => new GrupoMuscularViewModel(x)).ToList();
			return View(modelo);
		}

		[HttpGet]
		public ActionResult Agregar()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Agregar(AgregarGrupoMuscularViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _gruposMuscularesManager.CreateAsync(modelo.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(Listar));
			}

			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(GrupoMuscular)));
			return View(modelo);
		}

		[HttpGet]
		public async Task<ActionResult> Editar(string id)
		{
			GrupoMuscular grupoMuscular = await _gruposMuscularesManager.ReadByIdAsync(new Guid(id));
			if (grupoMuscular == null) return NotFound();
			EditarGrupoMuscularViewModel modelo = new(grupoMuscular);
			return View(modelo);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Editar(EditarGrupoMuscularViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _gruposMuscularesManager.UpdateAsync(modelo.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(Listar));
			}

			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(GrupoMuscular)));
			return View(modelo);
		}

		[HttpGet]
		public async Task<ActionResult> Eliminar(string id)
		{
			GrupoMuscular grupoMuscular = await _gruposMuscularesManager.ReadByIdAsync(new Guid(id));
			EliminarGrupoMuscularViewModel modelo = new(grupoMuscular);

			return View(modelo);
		}

		[HttpPost]
		public async Task<ActionResult> Eliminar(EliminarGrupoMuscularViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _gruposMuscularesManager.DeleteAsync(new Guid(modelo.IdGrupoMuscular));
				return RedirectToAction(nameof(Listar));
			}

			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoEjercicio)));
			return View(modelo);
		}

		[HttpGet]
		public async Task<JsonResult> Detalle(string id)
		{
			GrupoMuscular grupoMuscular = await _gruposMuscularesManager.ReadByIdAsync(new Guid(id));
			var modelo = new GrupoMuscularViewModel(grupoMuscular);
			return Json(modelo);
		}
	}
}
