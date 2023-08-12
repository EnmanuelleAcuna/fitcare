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
// 	public class TiposMaquinaController : BaseController
// 	{
// 		private readonly IDataCRUDBase<TipoMaquina> _repoTiposMaquina;
// 		private readonly ILogger<TiposMaquinaController> _logger;

// 		public TiposMaquinaController(IDataCRUDBase<TipoMaquina> repoTiposMaquina,
// 									  UserManager<ApplicationUser> userManager,
// 									  RoleManager<ApplicationRole> roleManager,
// 									  IConfiguration configuration,
// 									  IHttpContextAccessor contextAccesor,
// 									  ILogger<TiposMaquinaController> logger,
// 									  IWebHostEnvironment environment,
// 									  IDataCRUDBase<Imagen> repoImagenes)
// 		: base(userManager, roleManager, configuration, contextAccesor, environment, repoImagenes)
// 		{
// 			_repoTiposMaquina = repoTiposMaquina;
// 			_logger = logger;
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Inicio()
// 		{
// 			IEnumerable<TipoMaquina> listaTiposMaquina = await _repoTiposMaquina.ReadAllAsync();
// 			IEnumerable<InicioTipoMaquinaViewModel> modelo = listaTiposMaquina.Select(x => new InicioTipoMaquinaViewModel(x)).ToList();
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public ActionResult Nuevo()
// 		{
// 			return View();
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Nuevo(NuevoTipoMaquinaViewModel Modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				TipoMaquina tipoMaquina = Modelo.Entidad();
// 				await _repoTiposMaquina.CreateAsync(tipoMaquina);
// 				return RedirectToAction("Inicio");
// 			}

// 			// Si se llega a este punto, hubo un error
// 			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(TipoMaquina)));
// 			return View(Modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Editar(string id)
// 		{
// 			TipoMaquina tipoMaquina = await _repoTiposMaquina.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarTipoMaquinaViewModel modelo = new(tipoMaquina);

// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Editar(EditarTipoMaquinaViewModel modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				TipoMaquina tipoMaquina = modelo.Entidad();
// 				await _repoTiposMaquina.UpdateAsync(tipoMaquina);
// 				return RedirectToAction("Inicio");
// 			}

// 			// Si se llega a este punto, hubo un error
// 			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoMaquina)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Eliminar(string id)
// 		{
// 			TipoMaquina tipoMaquina = await _repoTiposMaquina.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarTipoMaquinaViewModel modelo = new(tipoMaquina);

// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		public async Task<ActionResult> Eliminar(EditarTipoMaquinaViewModel modelo)
// 		{
// 			Guid idObjeto = Factory.SetGuid(modelo.Id);
// 			await _repoTiposMaquina.DeleteAsync(idObjeto);

// 			return RedirectToAction("Inicio");
// 		}

// 		[HttpGet]
// 		public async Task<JsonResult> Detalle(string id)
// 		{
// 			TipoMaquina tipoMaquina = await _repoTiposMaquina.ReadByIdAsync(Factory.SetGuid(id));
// 			DetalleTipoMaquinaViewModel modelo = new(tipoMaquina);

// 			return Json(modelo);
// 		}
// 	}
// }
