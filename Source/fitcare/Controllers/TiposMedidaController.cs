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
// 	public class TiposMedidaController : BaseController
// 	{
// 		private readonly IDataCRUDBase<TipoMedida> _repoTiposMedida;
// 		private readonly ILogger<TiposMedidaController> _logger;

// 		public TiposMedidaController(IDataCRUDBase<TipoMedida> repoTiposMedida,
// 									 UserManager<ApplicationUser> userManager,
// 									 RoleManager<ApplicationRole> roleManager,
// 									 IConfiguration configuration,
// 									 IHttpContextAccessor contextAccesor,
// 									 ILogger<TiposMedidaController> logger,
// 									 IWebHostEnvironment environment,
// 									 IDataCRUDBase<Imagen> repoImagenes)
// 		: base(userManager, roleManager, configuration, contextAccesor, environment, repoImagenes)
// 		{
// 			_repoTiposMedida = repoTiposMedida;
// 			_logger = logger;
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Inicio()
// 		{
// 			IEnumerable<TipoMedida> listaTiposMedida = await _repoTiposMedida.ReadAllAsync();
// 			IEnumerable<InicioTipoMedidaViewModel> modelo = listaTiposMedida.Select(x => new InicioTipoMedidaViewModel(x)).ToList();
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public ActionResult Nuevo()
// 		{
// 			return View();
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Nuevo(NuevoTipoMedidaViewModel Modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				TipoMedida tipoMedida = Modelo.Entidad();
// 				await _repoTiposMedida.CreateAsync(tipoMedida);
// 				return RedirectToAction("Inicio");
// 			}

// 			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(TipoMedida)));
// 			return View(Modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Editar(string id)
// 		{
// 			TipoMedida tipoMedida = await _repoTiposMedida.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarTipoMedidaViewModel Modelo = new(tipoMedida);

// 			return View(Modelo);
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Editar(EditarTipoMedidaViewModel Modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				TipoMedida tipoMedida = Modelo.Entidad();
// 				await _repoTiposMedida.UpdateAsync(tipoMedida);
// 				return RedirectToAction("Inicio");
// 			}

// 			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(TipoMedida)));
// 			return View(Modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Eliminar(string id)
// 		{
// 			TipoMedida tipoMedida = await _repoTiposMedida.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarTipoMedidaViewModel modelo = new(tipoMedida);

// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		public async Task<ActionResult> Eliminar(EditarTipoEjercicioViewModel modelo)
// 		{
// 			Guid idObjeto = Factory.SetGuid(modelo.Id);
// 			await _repoTiposMedida.DeleteAsync(idObjeto);

// 			return RedirectToAction("Inicio");
// 		}

// 		[HttpGet]
// 		public async Task<JsonResult> Detalle(string id)
// 		{
// 			TipoMedida tipoMedida = await _repoTiposMedida.ReadByIdAsync(Factory.SetGuid(id));
// 			DetalleTipoMedidaViewModel modelo = new(tipoMedida);

// 			return Json(modelo);
// 		}
// 	}
// }
