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
// 	public class MaquinasController : BaseController
// 	{
// 		private readonly IDataCRUDBase<Maquina> _repoMaquinas;
// 		private readonly IDataCRUDBase<TipoMaquina> _repoTiposMaquina;
// 		private readonly IDataCRUDBase<GrupoMuscular> _repoGruposMusculares;
// 		private readonly ILogger<MaquinasController> _logger;
// 		private readonly string NOMBRE_CARPETA_IMAGENES = "Maquinas";

// 		public MaquinasController(IDataCRUDBase<Maquina> repoMaquinas,
// 								  IDataCRUDBase<TipoMaquina> repoTiposMaquina,
// 								  IDataCRUDBase<GrupoMuscular> repoGruposMusculares,
// 								  UserManager<ApplicationUser> userManager,
// 								  RoleManager<ApplicationRole> roleManager,
// 								  IConfiguration configuration,
// 								  IHttpContextAccessor contextAccesor,
// 								  ILogger<MaquinasController> logger,
// 								  IWebHostEnvironment environment,
// 								  IDataCRUDBase<Imagen> repoImagenes)
// 		: base(userManager, roleManager, configuration, contextAccesor, environment, repoImagenes)
// 		{
// 			_repoMaquinas = repoMaquinas;
// 			_repoTiposMaquina = repoTiposMaquina;
// 			_repoGruposMusculares = repoGruposMusculares;
// 			_logger = logger;
// 		}

// 		[HttpGet]
// 		public async Task<IActionResult> Index()
// 		{
// 			IEnumerable<Maquina> maquinas = await _repoMaquinas.ReadAllAsync();
// 			await CargarImagenMaquinas(maquinas);
// 			IEnumerable<MaquinaViewModel> modeloVista = maquinas.Select(c => new MaquinaViewModel(c)).ToList();
// 			return View(modeloVista);
// 		}

// 		[HttpGet]
// 		public async Task<IActionResult> Nuevo()
// 		{
// 			ViewBag.GruposMusculares = CargarListaSeleccionGruposMusculares(await _repoGruposMusculares.ReadAllAsync());
// 			ViewBag.ListaTiposMaquina = CargarListaSeleccionTiposMaquina(await _repoTiposMaquina.ReadAllAsync());
// 			return View();
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<IActionResult> Nuevo(NuevaMaquinaViewModel modeloVista, IFormCollection collection)
// 		{
// 			if (!ModelState.IsValid)
// 			{
// 				ViewBag.GruposMusculares = CargarListaSeleccionGruposMusculares(await _repoGruposMusculares.ReadAllAsync());
// 				ViewBag.ListaTiposMaquina = CargarListaSeleccionTiposMaquina(await _repoTiposMaquina.ReadAllAsync());
// 				ModelState.AddModelError("", Messages.MensajeModeloInvalido);
// 				return View(modeloVista);
// 			}

// 			modeloVista.SetDependencies(_repoTiposMaquina, _repoGruposMusculares);
// 			Maquina maquina = await modeloVista.Entidad(collection);
// 			Imagen imagen = await GuardarImagenAsync(maquina.Id, modeloVista.Imagen, NOMBRE_CARPETA_IMAGENES);
// 			if (imagen is null) throw new Exception(Messages.MensajeErrorGuardandoArchivo);
// 			await _repoMaquinas.CreateAsync(maquina);
// 			return RedirectToAction(nameof(Index));
// 		}

// 		[HttpGet]
// 		public async Task<IActionResult> Editar(string id)
// 		{
// 			Maquina maquina = await _repoMaquinas.ReadByIdAsync(Factory.SetGuid(id));
// 			maquina.SetImagen(await ObtenerImagen(maquina.Id));
// 			EditarMaquinaViewModel modelo = new(maquina);

// 			ViewBag.GruposMusculares = CargarListaSeleccionGruposMusculares(await _repoGruposMusculares.ReadAllAsync());
// 			ViewBag.ListaTiposMaquina = CargarListaSeleccionTiposMaquina(await _repoTiposMaquina.ReadAllAsync());

// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<IActionResult> Editar(EditarMaquinaViewModel modeloVista, IFormCollection collection)
// 		{
// 			if (!ModelState.IsValid)
// 			{
// 				ViewBag.GruposMusculares = CargarListaSeleccionGruposMusculares(await _repoGruposMusculares.ReadAllAsync());
// 				ViewBag.ListaTiposMaquina = CargarListaSeleccionTiposMaquina(await _repoTiposMaquina.ReadAllAsync());
// 				ModelState.AddModelError("", Messages.MensajeModeloInvalido);
// 				return View(modeloVista);
// 			}

// 			modeloVista.SetDependencies(_repoTiposMaquina, _repoGruposMusculares);

// 			Maquina maquina = await modeloVista.Entidad(collection);

// 			if (modeloVista.Imagen is not null)
// 			{
// 				Imagen imagen = await ActualizarImagenAsync(maquina.Id, modeloVista.Imagen, NOMBRE_CARPETA_IMAGENES);
// 				if (imagen is null) throw new Exception(Messages.MensajeErrorGuardandoArchivo);
// 			}

// 			await _repoMaquinas.UpdateAsync(maquina);
// 			return RedirectToAction(nameof(Index));
// 		}

// 		[HttpGet]
// 		public async Task<IActionResult> Eliminar(string id)
// 		{
// 			Maquina maquina = await _repoMaquinas.ReadByIdAsync(Factory.SetGuid(id));
// 			maquina.SetImagen(await ObtenerImagen(maquina.Id));
// 			EditarMaquinaViewModel modeloVista = new(maquina);
// 			return View(modeloVista);
// 		}

// 		[HttpPost]
// 		public async Task<IActionResult> Eliminar(EditarMaquinaViewModel modelo)
// 		{
// 			Guid idMaquina = Factory.SetGuid(modelo.Id);
// 			await _repoMaquinas.DeleteAsync(idMaquina);
// 			await EliminarImagenAsync(idMaquina);
// 			return RedirectToAction(nameof(Index));
// 		}

// 		[HttpGet]
// 		public async Task<JsonResult> Detalle(string id)
// 		{
// 			Maquina maquina = await _repoMaquinas.ReadByIdAsync(Factory.SetGuid(id));
// 			maquina.SetImagen(await ObtenerImagen(maquina.Id));
// 			MaquinaViewModel modelo = new(maquina);
// 			return Json(modelo);
// 		}
// 	}
// }
