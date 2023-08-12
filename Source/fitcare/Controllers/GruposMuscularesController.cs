// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using fitcare.Models.Contracts;
// using fitcare.Models.DataAccess;
// using fitcare.Models.Entities;
// using fitcare.Models.Enums;
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
// 	public class GruposMuscularesController : BaseController
// 	{
// 		private readonly IDataCRUDBase<GrupoMuscular> _repoGruposMusculares;
// 		private readonly ILogger<GruposMuscularesController> _logger;

// 		public GruposMuscularesController(IDataCRUDBase<GrupoMuscular> repoGruposMusculares,
// 										  UserManager<ApplicationUser> userManager,
// 										  RoleManager<ApplicationRole> roleManager,
// 										  IConfiguration configuration,
// 										  IHttpContextAccessor contextAccesor,
// 										  ILogger<GruposMuscularesController> logger,
// 										  IWebHostEnvironment environment,
// 										  IDataCRUDBase<Imagen> repoImagenes)
// 		: base(userManager, roleManager, configuration, contextAccesor, environment, repoImagenes)
// 		{
// 			_repoGruposMusculares = repoGruposMusculares;
// 			_logger = logger;
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Inicio()
// 		{
// 			await DAOBitacora.ReadAllAsync();

// 			IEnumerable<GrupoMuscular> listaGruposMusculares = await _repoGruposMusculares.ReadAllAsync();
// 			IEnumerable<InicioGrupoMuscularViewModel> modelo = listaGruposMusculares.Select(x => new InicioGrupoMuscularViewModel(x)).ToList();
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public ActionResult Nuevo()
// 		{
// 			return View();
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Nuevo(NuevoGrupoMuscularViewModel modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				GrupoMuscular grupoMuscular = modelo.Entidad();
// 				await _repoGruposMusculares.CreateAsync(grupoMuscular);

// 				await DAOBitacora.CreateAsync(Factory.GetBitacoraAgregar(grupoMuscular.Id, GetCurrentUser(), grupoMuscular.ToString()));

// 				return RedirectToAction("Inicio");
// 			}

// 			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(GrupoMuscular)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Editar(string id)
// 		{
// 			GrupoMuscular grupoMuscular = await _repoGruposMusculares.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarGrupoMuscularViewModel modelo = new(grupoMuscular);

// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Editar(EditarGrupoMuscularViewModel modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				GrupoMuscular grupoMuscular = modelo.Entidad();
// 				await _repoGruposMusculares.UpdateAsync(grupoMuscular);

// 				await DAOBitacora.CreateAsync(Factory.GetBitacoraActualizar(grupoMuscular.Id, GetCurrentUser(), grupoMuscular.ToString()));

// 				return RedirectToAction("Inicio");
// 			}

// 			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(GrupoMuscular)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Eliminar(string id)
// 		{
// 			GrupoMuscular grupoMuscular = await _repoGruposMusculares.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarGrupoMuscularViewModel modelo = new(grupoMuscular);

// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		public async Task<ActionResult> Eliminar(EditarGrupoMuscularViewModel modelo)
// 		{
// 			Guid idObjeto = Factory.SetGuid(modelo.Id);
// 			await _repoGruposMusculares.DeleteAsync(idObjeto);

// 			return RedirectToAction("Inicio");
// 		}

// 		[HttpGet]
// 		public async Task<JsonResult> Detalle(string id)
// 		{
// 			GrupoMuscular grupoMuscular = await _repoGruposMusculares.ReadByIdAsync(Factory.SetGuid(id));
// 			Bitacora registroBitacoraAgregar = await DAOBitacora.ReadNewestByIdObjetoAsync(grupoMuscular.Id, AccionesBitacora.Agregar);
// 			Bitacora registroBitacoraModificar = await DAOBitacora.ReadNewestByIdObjetoAsync(grupoMuscular.Id, AccionesBitacora.Modificar);
// 			DetalleGrupoMuscularViewModel modelo = new(grupoMuscular, registroBitacoraAgregar, registroBitacoraModificar);

// 			return Json(modelo);
// 		}
// 	}
// }
