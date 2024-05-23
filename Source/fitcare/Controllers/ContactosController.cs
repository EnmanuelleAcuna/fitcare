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

namespace fitcare.Controllers;

[Authorize]
public class ContactosController : BaseController
{
	private readonly IContactoManager<Contacto> _contactosManager;
	private readonly ILogger<ContactosController> _logger;

	public ContactosController(IContactoManager<Contacto> repoContactos,
							   IDivisionTerritorialManager divisionTerritorialManager,
							   ApplicationUserManager<ApplicationUser> userManager,
							   RoleManager<ApplicationRole> roleManager,
							   IConfiguration configuration,
							   IHttpContextAccessor contextAccesor,
							   ILogger<ContactosController> logger,
							   IWebHostEnvironment environment)
	: base(divisionTerritorialManager, userManager, roleManager, configuration, contextAccesor, environment)
	{
		_contactosManager = repoContactos;
		_logger = logger;
	}

	[HttpGet]
	public async Task<ActionResult> Index()
	{
		var contactos = await _contactosManager.ReadAllAsync();
		var viewModel = contactos.Select(x => new ContactoViewModel(x));
		return View(viewModel);
	}

	[HttpGet]
	public async Task<JsonResult> Detalle(string id)
	{
		var contacto = await _contactosManager.ReadByIdAsync(new Guid(id));
		var viewModel = new ContactoViewModel(contacto);
		return Json(viewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[AllowAnonymous]
	public async Task<ActionResult> Agregar(AgregarContactoViewModel viewModel)
	{
		if (ModelState.IsValid)
		{
			await _contactosManager.CreateAsync(viewModel.Entidad());
			return View(nameof(ConfirmacionContacto));
		}

		ModelState.AddModelError("", Messages.MensajeErrorEnviarInformacion);
		return View(viewModel);
	}

	[HttpGet]
	public ActionResult ConfirmacionContacto()
	{
		return View();
	}

	[HttpGet]
	public async Task<ActionResult> Eliminar(string id)
	{
		var contacto = await _contactosManager.ReadByIdAsync(new Guid(id));
		if (contacto == null) return NotFound();
		var viewModel = new EliminarContactoViewModel(contacto);
		return View(viewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Eliminar(EliminarContactoViewModel viewModel)
	{
		if (ModelState.IsValid)
		{
			await _contactosManager.DeleteAsync(new Guid(viewModel.Id));
			return RedirectToAction(nameof(Index));
		}

		ModelState.AddModelError("", Messages.MensajeErrorEliminar(nameof(Contacto)));
		return View(viewModel);
	}
}
