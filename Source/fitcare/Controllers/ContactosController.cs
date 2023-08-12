using System.Collections.Generic;
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
	private readonly IManager<Contacto> _contactosManager;
	private readonly ILogger<ContactosController> _logger;

	public ContactosController(IManager<Contacto> repoContactos,
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
		var listaContactos = (List<ContactoViewModel>)await _contactosManager.ReadAllAsync();
		return View(listaContactos);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[AllowAnonymous]
	public async Task<ActionResult> Agregar(AgregarContactoViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			await _contactosManager.CreateAsync(modelo.Entidad());
			return View(nameof(ConfirmacionContacto));
		}

		ModelState.AddModelError("", Messages.MensajeErrorEnviarInformacion);
		return View(modelo);
	}

	[HttpGet]
	public ActionResult ConfirmacionContacto()
	{
		return View();
	}

	[HttpGet]
	public async Task<ActionResult> Eliminar(string id)
	{
		var contacto = await _contactosManager.ReadByIdAsync(Factory.NewGUID(id));
		if (contacto == null) return NotFound();
		return View(new ContactoViewModel(contacto));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Eliminar(ContactoViewModel modelo)
	{
		await _contactosManager.DeleteAsync(Factory.NewGUID(modelo.Id));
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<JsonResult> Detalle(string id)
	{
		var contacto = await _contactosManager.ReadByIdAsync(Factory.NewGUID(id));
		return Json(new ContactoViewModel(contacto));
	}
}
