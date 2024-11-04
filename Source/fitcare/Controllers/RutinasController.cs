using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models;
using fitcare.Models.Entities;
using fitcare.Models.Extras;
using fitcare.Models.Identity;
using fitcare.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace fitcare.Controllers;

[Authorize]
public class RutinasController : BaseController
{
	private readonly IRutinas<Rutina> _rutinas;
	private readonly IBaseCore<TipoMedida> _tiposMedida;
	private readonly IBaseCore<Ejercicio> _ejercicios;
	private readonly IBaseCore<GrupoMuscular> _gruposMusculares;
	private readonly IBaseCore<Maquina> _maquinas;
	private readonly ApplicationUserManager<ApplicationUser> _userManager;
	private readonly IEmailSender _emailSender;
	private readonly ILogger<RutinasController> _logger;
	private readonly IConfiguration _configuration;

	public RutinasController(IRutinas<Rutina> rutinas,
							 IBaseCore<TipoMedida> tiposMedida,
							 IBaseCore<Ejercicio> repoEjercicios,
							 IBaseCore<GrupoMuscular> gruposMusculares,
							 IBaseCore<Maquina> maquinas,
							 IEmailSender emailSender,
							 IDivisionTerritorial divisionTerritorial,
							 ApplicationUserManager<ApplicationUser> userManager,
							 RoleManager<ApplicationRole> roleManager,
							 IConfiguration configuration,
							 IHttpContextAccessor contextAccesor,
							 ILogger<RutinasController> logger,
							 IWebHostEnvironment environment)
	: base(divisionTerritorial, userManager, roleManager, configuration, contextAccesor, environment)
	{
		_rutinas = rutinas;
		_tiposMedida = tiposMedida;
		_ejercicios = repoEjercicios;
		_gruposMusculares = gruposMusculares;
		_maquinas = maquinas;
		_userManager = userManager;
		_emailSender = emailSender;
		_logger = logger;
		_configuration = configuration;
	}

	[HttpGet]
	public async Task<ActionResult> Listar()
	{
		IEnumerable<Rutina> listaRutinas = await _rutinas.ReadAllAsync();
		IEnumerable<RutinaViewModel> modelo = listaRutinas.Select(x => new RutinaViewModel(x)).ToList();
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> Agregar()
	{
		var listaInstructores = await _userManager.GetUsersInRoleAsync("Instructor");
		var listaClientes = await _userManager.GetUsersInRoleAsync("Cliente");

		ViewBag.ListaInstructores = CargarListaSeleccionUsuarios(listaInstructores);
		ViewBag.ListaClientes = CargarListaSeleccionUsuarios(listaClientes);

		await CargarViewBags();

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Agregar(AgregarRutinaViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			if (modelo.IdInstructor.Equals(modelo.IdCliente)){
				ModelState.AddModelError("", "No se puede registrar una rutina donde el cliente es el mismo usuario instructor seleccionado.");
				return View(modelo);
			}

			ApplicationUser usuarioInstructor = await _userManager.FindByIdAsync(modelo.IdInstructor);
			if (usuarioInstructor == null) return NotFound();

			ApplicationUser usuarioCliente = await _userManager.FindByIdAsync(modelo.IdCliente);
			if (usuarioCliente == null) return NotFound();

			Rutina rutina = modelo.Entidad(usuarioInstructor, usuarioCliente);

			await _rutinas.CreateAsync(rutina, CurrentUser);

			// Obtener informacion del cliente y enviar correo de notificación de creación de rutina
			var cliente = await _userManager.FindByIdAsync(rutina.Cliente.Id);
			string urlVisualizacionRutina = Url.Action("Detalle", "Rutinas", new { id = rutina.Id }, protocol: Request.Scheme);
			string mensajeDeCorreo = string.Format(new CultureInfo("es-CR"), "Hola {0} <br /> Se ha registrado su rutina en fitcare. <br /> Para verla o darle seguimiento puede ir al siguiente <a href=\"{1}\">enlace</a>", "", urlVisualizacionRutina);
			await _emailSender.SendEmailAsync(cliente.Email, "fitcare: Registro de rutina", mensajeDeCorreo);

			return RedirectToAction(nameof(Listar));
		}

		await CargarViewBags();
		ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Rutina)));
		return View(modelo);
	}

	[HttpGet]
	public async Task<IActionResult> Detalle(string id)
	{
		var rutina = await _rutinas.ReadByIdAsync(new Guid(id));
		if (rutina == null) return NotFound();
		var modelo = new DetalleRutinaViewModel(rutina);
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> ReporteResumido()
	{
		await CargarViewBags();
		var viewModel = new List<ReporteRutinaResumidoViewModel>();
		return View(viewModel);
	}

	[HttpPost]
	public async Task<ActionResult> ReporteResumido(string idInstructor, string idCliente)
	{
		await CargarViewBags();

		IEnumerable<ReporteRutinaResumido> datosReporte = _rutinas.ObtenerReporteRutinasResumido(idInstructor, idCliente);
		IEnumerable<ReporteRutinaResumidoViewModel> viewModel = datosReporte.Select(i => new ReporteRutinaResumidoViewModel(i, _configuration)).ToList();

		return View(viewModel);
	}

	private async Task CargarViewBags()
	{
		ViewBag.ListaTiposMedida = CargarListaSeleccionTiposMedida(await _tiposMedida.ReadAllAsync());
		ViewBag.ListaEjercicios = CargarListaSeleccionEjercicios(await _ejercicios.ReadAllAsync());
		ViewBag.ListaGruposMusculares = CargarListaSeleccionGruposMusculares(await _gruposMusculares.ReadAllAsync());
		ViewBag.ListaMaquinas = CargarListaSeleccionMaquinas(await _maquinas.ReadAllAsync());
		
		ViewBag.ListaClientes = CargarListaSeleccionClientes(await _userManager.GetUsersInRoleAsync("Cliente"));
		ViewBag.ListaInstructores = CargarListaSeleccionInstructores(await _userManager.GetUsersInRoleAsync("Instructor"));
	}
}
