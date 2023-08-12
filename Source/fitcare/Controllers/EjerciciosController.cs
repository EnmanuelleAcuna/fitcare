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
// 	public class EjerciciosController : BaseController
// 	{
// 		private readonly IDataCRUDBase<Ejercicio> _repoEjercicios;
// 		private readonly IDataCRUDBase<Accesorio> _repoAccesorios;
// 		private readonly IDataCRUDBase<Maquina> _repoMaquinas;
// 		private readonly IDataCRUDBase<GrupoMuscular> _repoGruposMusculares;
// 		private readonly IDataCRUDBase<TipoEjercicio> _repoTiposEjercicio;
// 		private readonly ILogger<EjerciciosController> _logger;

// 		public EjerciciosController(IDataCRUDBase<Ejercicio> repoEjercicios,
// 									IDataCRUDBase<Accesorio> repoAccesorios,
// 									IDataCRUDBase<Maquina> repoMaquinas,
// 									IDataCRUDBase<GrupoMuscular> repoGruposMusculares,
// 									IDataCRUDBase<TipoEjercicio> repoTiposEjercicio,
// 									UserManager<ApplicationUser> userManager,
// 									RoleManager<ApplicationRole> roleManager,
// 									IConfiguration configuration,
// 									IHttpContextAccessor contextAccesor,
// 									ILogger<EjerciciosController> logger,
// 									IWebHostEnvironment environment,
// 									IDataCRUDBase<Imagen> repoImagenes)
// 		: base(userManager, roleManager, configuration, contextAccesor, environment, repoImagenes)
// 		{
// 			_repoEjercicios = repoEjercicios;
// 			_repoAccesorios = repoAccesorios;
// 			_repoMaquinas = repoMaquinas;
// 			_repoGruposMusculares = repoGruposMusculares;
// 			_repoTiposEjercicio = repoTiposEjercicio;
// 			_logger = logger;
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Inicio()
// 		{
// 			IEnumerable<Ejercicio> listaEjercicios = await _repoEjercicios.ReadAllAsync();
// 			IEnumerable<InicioEjercicioViewModel> modelo = listaEjercicios.Select(x => new InicioEjercicioViewModel(x)).ToList();
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Nuevo()
// 		{
// 			await CargarViewBags();
// 			return View();
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Nuevo(NuevoEjercicioViewModel modelo, IFormCollection collection)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				modelo.SetDependencies(_repoAccesorios, _repoMaquinas, _repoGruposMusculares, _repoTiposEjercicio);
// 				Ejercicio ejercicio = await modelo.Entidad(collection);
// 				await _repoEjercicios.CreateAsync(ejercicio);
// 				return RedirectToAction("Inicio");
// 			}

// 			// Si se llega a este punto, hubo un error
// 			await CargarViewBags();
// 			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Ejercicio)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Editar(string id)
// 		{
// 			Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarEjercicioViewModel modelo = new(ejercicio);

// 			await CargarViewBags();
// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<ActionResult> Editar(EditarEjercicioViewModel modelo, IFormCollection collection)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				modelo.SetDependencies(_repoAccesorios, _repoMaquinas, _repoGruposMusculares, _repoTiposEjercicio);
// 				Ejercicio ejercicio = await modelo.Entidad(collection);
// 				await _repoEjercicios.UpdateAsync(ejercicio);
// 				return RedirectToAction("Inicio");
// 			}

// 			// Si se llega a este punto, hubo un error
// 			await CargarViewBags();
// 			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(Ejercicio)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Eliminar(string id)
// 		{
// 			Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarEjercicioViewModel modelo = new(ejercicio);

// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		public async Task<ActionResult> Eliminar(EditarEjercicioViewModel modelo)
// 		{
// 			Guid idObjeto = Factory.SetGuid(modelo.Id);
// 			await _repoEjercicios.DeleteAsync(idObjeto);

// 			return RedirectToAction("Inicio");
// 		}

// 		[HttpGet]
// 		public async Task<JsonResult> Detalle(string id)
// 		{
// 			Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(id));
// 			Bitacora registroBitacoraAgregar = await DAOBitacora.ReadNewestByIdObjetoAsync(ejercicio.Id, AccionesBitacora.Agregar);
// 			Bitacora registroBitacoraModificar = await DAOBitacora.ReadNewestByIdObjetoAsync(ejercicio.Id, AccionesBitacora.Modificar);
// 			EjerciciosViewModel modelo = new(ejercicio, registroBitacoraAgregar, registroBitacoraModificar);


// 			//Ejercicio ejercicio = await _repoEjercicios.ReadByIdAsync(Factory.SetGuid(id));
// 			DetalleEjercicioViewModel modeloVista = new(ejercicio);

// 			return Json(modeloVista);
// 		}

// 		private async Task CargarViewBags()
// 		{
// 			ViewBag.ListaTiposEjercicio = CargarListaSeleccionTiposEjercicio(await _repoTiposEjercicio.ReadAllAsync());
// 			ViewBag.Accesorios = CargarListaSeleccionAccesorios(await _repoAccesorios.ReadAllAsync());
// 			ViewBag.Maquinas = CargarListaSeleccionMaquinas(await _repoMaquinas.ReadAllAsync());
// 			ViewBag.GruposMusculares = CargarListaSeleccionGruposMusculares(await _repoGruposMusculares.ReadAllAsync());
// 		}
// 	}
// }
