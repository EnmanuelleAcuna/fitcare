// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using fitcare.Models.Contracts;
// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using fitcare.Models.Identity;
// using fitcare.Models.ViewModels;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

// namespace fitcare.Controllers
// {
// 	[Authorize]
// 	public class TiposEjercicioController : BaseController
// 	{
// 		private readonly IDataCRUDBase<TipoEjercicio> _repoTiposEjercicio;
// 		private readonly ILogger<TiposEjercicioController> _logger;

// 		public TiposEjercicioController(IDataCRUDBase<TipoEjercicio> repoTiposEjercicio,
// 										UserManager<ApplicationUser> userManager,
// 										RoleManager<ApplicationRole> roleManager,
// 										IConfiguration configuration,
// 										IHttpContextAccessor contextAccesor,
// 										ILogger<TiposEjercicioController> logger,
// 										IWebHostEnvironment environment,
// 										IDataCRUDBase<Imagen> repoImagenes)
// 		: base(userManager, roleManager, configuration, contextAccesor, environment, repoImagenes)
// 		{
// 			_repoTiposEjercicio = repoTiposEjercicio;
// 			_logger = logger;
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Inicio()
// 		{
// 			IEnumerable<TipoEjercicio> listaTiposEjercicio = await _repoTiposEjercicio.ReadAllAsync();
// 			IEnumerable<InicioTipoEjercicioViewModel> modelo = listaTiposEjercicio.Select(x => new InicioTipoEjercicioViewModel(x)).ToList();
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public ActionResult Nuevo()
// 		{
// 			return View();
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Nuevo(NuevoTipoEjercicioViewModel modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				TipoEjercicio tipoEjercicio = modelo.Entidad();
// 				await _repoTiposEjercicio.CreateAsync(tipoEjercicio);
// 				return RedirectToAction("Inicio");
// 			}

// 			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(TipoEjercicio)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Editar(string id)
// 		{
// 			TipoEjercicio tipoEjercicio = await _repoTiposEjercicio.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarTipoEjercicioViewModel modelo = new(tipoEjercicio);

// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Editar(EditarTipoEjercicioViewModel modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				TipoEjercicio tipoEjercicio = modelo.Entidad();
// 				await _repoTiposEjercicio.UpdateAsync(tipoEjercicio);
// 				return RedirectToAction("Inicio");
// 			}

// 			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoEjercicio)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Eliminar(string id)
// 		{
// 			TipoEjercicio tipoEjercicio = await _repoTiposEjercicio.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarTipoEjercicioViewModel modelo = new(tipoEjercicio);

// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		public async Task<ActionResult> Eliminar(EditarTipoEjercicioViewModel modelo)
// 		{
// 			Guid idObjeto = Factory.SetGuid(modelo.Id);
// 			await _repoTiposEjercicio.DeleteAsync(idObjeto);

// 			return RedirectToAction("Inicio");

// 		}

// 		[HttpGet]
// 		public async Task<JsonResult> Detalle(string id)
// 		{
// 			TipoEjercicio TipoEjercicio = await _repoTiposEjercicio.ReadByIdAsync(Factory.SetGuid(id));
// 			DetalleTipoEjercicioViewModel Modelo = new(TipoEjercicio);

// 			return Json(Modelo);
// 		}
// 	}
// }
