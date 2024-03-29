using System;
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
	public class DivisionTerritorialController : BaseController
	{
		private readonly IDivisionTerritorialManager _divisionTerritorialManager;
		private readonly ILogger<DivisionTerritorialController> _logger;

		public DivisionTerritorialController(IDivisionTerritorialManager divisionTerritorialManager,
											 ApplicationUserManager<ApplicationUser> userManager,
											 RoleManager<ApplicationRole> roleManager,
											 IConfiguration configuration,
											 IHttpContextAccessor contextAccesor,
											 ILogger<DivisionTerritorialController> logger,
											 IWebHostEnvironment environment)
		: base(divisionTerritorialManager, userManager, roleManager, configuration, contextAccesor, environment)
		{
			_divisionTerritorialManager = divisionTerritorialManager;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> ListarProvincias()
		{
			var provincias = await _divisionTerritorialManager.Provincias.ReadAllAsync();
			var viewModel = provincias.Select(x => new ProvinciaViewModel(x));
			return View(viewModel);
		}

		[HttpGet]
		public ActionResult AgregarProvincia()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AgregarProvincia(AgregarProvinciaViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				await _divisionTerritorialManager.Provincias.CreateAsync(viewModel.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(ListarProvincias));
			}

			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Provincia)));
			return View(viewModel);
		}

		[HttpGet]
		public async Task<ActionResult> EditarProvincia(string id)
		{
			var provincia = await _divisionTerritorialManager.Provincias.ReadByIdAsync(new Guid(id));
			if (provincia == null) return NotFound();
			var viewModel = new EditarProvinciaViewModel(provincia);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditarProvincia(EditarProvinciaViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				await _divisionTerritorialManager.Provincias.UpdateAsync(viewModel.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(ListarProvincias));
			}

			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(Provincia)));
			return View(viewModel);
		}

		[HttpGet]
		public async Task<ActionResult> EliminarProvincia(string id)
		{
			var provincia = await _divisionTerritorialManager.Provincias.ReadByIdAsync(new Guid(id));
			if (provincia == null) return NotFound();
			var viewModel = new EliminarProvinciaViewModel(provincia);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EliminarProvincia(EliminarProvinciaViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				await _divisionTerritorialManager.Provincias.DeleteAsync(new Guid(viewModel.Id));
				return RedirectToAction(nameof(ListarProvincias));
			}

			ModelState.AddModelError("", Messages.MensajeErrorEliminar(nameof(Provincia)));
			return View(viewModel);
		}

		[HttpGet]
		public async Task<ActionResult> ListarCantones()
		{
			var cantones = await _divisionTerritorialManager.Cantones.ReadAllAsync();
			var viewModel = cantones.Select(x => new CantonViewModel(x));
			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> AgregarCanton()
		{
			ViewBag.ListaProvincias = await CargarListaSeleccionProvincias();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AgregarCanton(AgregarCantonViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _divisionTerritorialManager.Cantones.CreateAsync(modelo.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(ListarCantones));
			}

			ViewBag.ListaProvincias = await CargarListaSeleccionProvincias();
			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Canton)));

			return View(modelo);
		}

		[HttpGet]
		public async Task<ActionResult> EditarCanton(string id)
		{
			var canton = await _divisionTerritorialManager.Cantones.ReadByIdAsync(new Guid(id));
			if (canton == null) return NotFound();
			ViewBag.ListaProvincias = await CargarListaSeleccionProvincias();
			var viewModel = new EditarCantonViewModel(canton);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditarCanton(EditarCantonViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _divisionTerritorialManager.Cantones.UpdateAsync(modelo.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(ListarCantones));
			}

			ViewBag.ListaProvincias = await CargarListaSeleccionProvincias();
			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(Canton)));

			return View(modelo);
		}

		[HttpGet]
		public async Task<ActionResult> EliminarCanton(string id)
		{
			var canton = await _divisionTerritorialManager.Cantones.ReadByIdAsync(new Guid(id));
			if (canton == null) return NotFound();
			var viewModel = new EliminarCantonViewModel(canton);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EliminarCanton(EliminarCantonViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				await _divisionTerritorialManager.Cantones.DeleteAsync(new Guid(viewModel.Id));
				return RedirectToAction(nameof(ListarCantones));
			}

			ModelState.AddModelError("", Messages.MensajeErrorEliminar(nameof(Canton)));
			return View(viewModel);

		}

		[HttpGet]
		public async Task<ActionResult> ListarDistritos()
		{
			var distritos = await _divisionTerritorialManager.Distritos.ReadAllAsync();
			var viewModel = distritos.Select(x => new DistritoViewModel(x));
			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> AgregarDistrito()
		{
			ViewBag.ListaCantones = await CargarListaSeleccionCantones();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AgregarDistrito(AgregarDistritoViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _divisionTerritorialManager.Distritos.CreateAsync(modelo.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(ListarDistritos));
			}

			ViewBag.ListaCantones = CargarListaSeleccionCantones();
			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Distrito)));

			return View(modelo);
		}

		[HttpGet]
		public async Task<ActionResult> EditarDistrito(string id)
		{
			var distrito = await _divisionTerritorialManager.Distritos.ReadByIdAsync(new Guid(id));
			if (distrito == null) return NotFound();
			ViewBag.ListaCantones = await CargarListaSeleccionCantones();
			var viewModel = new EditarDistritoViewModel(distrito);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditarDistrito(EditarDistritoViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				await _divisionTerritorialManager.Distritos.UpdateAsync(modelo.Entidad(), GetCurrentUser());
				return RedirectToAction(nameof(ListarDistritos));
			}

			ViewBag.ListaCantones = await CargarListaSeleccionCantones();
			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(Distrito)));

			return View(modelo);
		}

		[HttpGet]
		public async Task<ActionResult> EliminarDistrito(string id)
		{
			var distrito = await _divisionTerritorialManager.Distritos.ReadByIdAsync(new Guid(id));
			if (distrito == null) return NotFound();
			var viewModel = new EliminarDistritoViewModel(distrito);
			return View(viewModel);
		}

		[HttpPost]
		public async Task<ActionResult> EliminarDistrito(EliminarDistritoViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				await _divisionTerritorialManager.Distritos.DeleteAsync(new Guid(viewModel.Id));
				return RedirectToAction(nameof(ListarDistritos));
			}

			ModelState.AddModelError("", Messages.MensajeErrorEliminar(nameof(Distrito)));
			return View(viewModel);
		}
	}
}
