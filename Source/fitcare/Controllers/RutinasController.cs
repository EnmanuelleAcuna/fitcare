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
	public async Task<ActionResult> Rutinas()
	{
		ApplicationUser user = await _userManager.GetUserAsync(User);
		bool esInstructor = await _userManager.IsInRoleAsync(user, "Instructor");
		bool esCliente = await _userManager.IsInRoleAsync(user, "Cliente");
		bool esAdmin = await _userManager.IsInRoleAsync(user, "Administrador");

		string idInstructor = null;
		string idCliente = null;

		// Filtrar según el rol del usuario
		if (esCliente && !esAdmin)
		{
			idCliente = user.Id;
		}
		else if (esInstructor && !esAdmin)
		{
			idInstructor = user.Id;
		}
		// Si es Admin, no filtra (muestra todas)

		ViewBag.EsAdmin = esAdmin;
		ViewBag.EsInstructor = esInstructor;
		ViewBag.EsCliente = esCliente;

		IEnumerable<Rutina> rutinas = await _rutinas.ObtenerReporteRutinas(idInstructor, idCliente);
		IEnumerable<RutinaListaViewModel> modelo = rutinas.Select(r => new RutinaListaViewModel(r)).ToList();

		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> Agregar()
	{
		var listaInstructores = await _userManager.GetUsersInRoleAsync("Instructor");
		var listaClientes = await _userManager.GetUsersInRoleAsync("Cliente");

		ViewBag.ListaInstructores = CargarListaSeleccionUsuarios(listaInstructores);
		ViewBag.ListaClientes = CargarListaSeleccionUsuarios(listaClientes);

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

			// Enviar correo de notificación de creación de rutina al cliente
			string urlVisualizacionRutina = Url.Action("Detalle", "Rutinas", new { id = rutina.Id }, protocol: Request.Scheme);
			string mensajeDeCorreo = string.Format(new CultureInfo("es-CR"), "Hola {0} <br /> Se ha registrado su rutina en fitcare. <br /> Para verla o darle seguimiento puede ir al siguiente <a href=\"{1}\">enlace</a>", "", urlVisualizacionRutina);
			await _emailSender.SendEmailAsync(usuarioCliente.Email, "fitcare: Registro de rutina", mensajeDeCorreo);

			return RedirectToAction(nameof(Rutinas));
		}

		var listaInstructores = await _userManager.GetUsersInRoleAsync("Instructor");
		var listaClientes = await _userManager.GetUsersInRoleAsync("Cliente");

		ViewBag.ListaInstructores = CargarListaSeleccionUsuarios(listaInstructores);
		ViewBag.ListaClientes = CargarListaSeleccionUsuarios(listaClientes);

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
	public async Task<IActionResult> Editar(string id)
	{
		var rutina = await _rutinas.ReadByIdAsync(new Guid(id));
		if (rutina == null) return NotFound();

		// Cargar listas para los modales
		ViewBag.ListaEjercicios = CargarListaSeleccionEjercicios(await _ejercicios.ReadAllAsync());
		ViewBag.ListaTiposMedida = CargarListaSeleccionTiposMedida(await _tiposMedida.ReadAllAsync());

		// Pasar IDs de ejercicios y tipos de medida ya agregados para filtrarlos
		ViewBag.EjerciciosAgregados = rutina.Ejercicios.Select(e => e.IdEjercicio.ToString()).ToList();
		ViewBag.TiposMedidaAgregados = rutina.Medidas.Select(m => m.IdTipoMedida.ToString()).ToList();

		var modelo = new DetalleRutinaViewModel(rutina);
		return View(modelo);
	}


	[HttpPost]
	public async Task<JsonResult> AgregarEjercicioAjax([FromBody] AgregarEjercicioRutinaViewModel modelo)
	{
		try
		{
			if (ModelState.IsValid)
			{
				EjercicioRutina ejercicio = modelo.Entidad();
				await _rutinas.AgregarEjercicioAsync(new Guid(modelo.IdRutina), ejercicio, CurrentUser);
				return Json(new { success = true, message = "Ejercicio agregado correctamente." });
			}

			var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault());
			return Json(new { success = false, errors = errors });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al agregar ejercicio a rutina");
			return Json(new { success = false, message = "Error al agregar el ejercicio. Por favor intente nuevamente." });
		}
	}

	[HttpPost]
	public async Task<JsonResult> EditarEjercicioAjax([FromBody] EditarEjercicioRutinaViewModel modelo)
	{
		try
		{
			if (ModelState.IsValid)
			{
				await _rutinas.EditarEjercicioAsync(
					new Guid(modelo.IdEjercicioRutina),
					new Guid(modelo.IdEjercicio),
					modelo.Series,
					modelo.Repeticiones,
					modelo.MinutosDescanso,
					CurrentUser);
				return Json(new { success = true, message = "Ejercicio actualizado correctamente." });
			}

			var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault());
			return Json(new { success = false, errors = errors });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al editar ejercicio de rutina");
			return Json(new { success = false, message = "Error al editar el ejercicio. Por favor intente nuevamente." });
		}
	}

	[HttpPost]
	public async Task<JsonResult> EliminarEjercicioAjax([FromBody] EliminarEjercicioRutinaViewModel modelo)
	{
		try
		{
			if (string.IsNullOrEmpty(modelo.IdEjercicioRutina))
			{
				return Json(new { success = false, message = "ID no válido" });
			}

			await _rutinas.EliminarEjercicioAsync(new Guid(modelo.IdEjercicioRutina));
			return Json(new { success = true, message = "Ejercicio eliminado correctamente." });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al eliminar ejercicio de rutina");
			return Json(new { success = false, message = "Error al eliminar el ejercicio." });
		}
	}

	[HttpPost]
	public async Task<JsonResult> AgregarMedidaAjax([FromBody] AgregarMedidaRutinaViewModel modelo)
	{
		try
		{
			if (ModelState.IsValid)
			{
				MedidaRutina medida = modelo.Entidad();
				await _rutinas.AgregarMedidaAsync(new Guid(modelo.IdRutina), medida, CurrentUser);
				return Json(new { success = true, message = "Medida agregada correctamente." });
			}

			var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault());
			return Json(new { success = false, errors = errors });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al agregar medida a rutina");
			return Json(new { success = false, message = "Error al agregar la medida. Por favor intente nuevamente." });
		}
	}

	[HttpPost]
	public async Task<JsonResult> EditarMedidaAjax([FromBody] EditarMedidaRutinaViewModel modelo)
	{
		try
		{
			if (ModelState.IsValid)
			{
				await _rutinas.EditarMedidaAsync(
					new Guid(modelo.IdMedidaRutina),
					new Guid(modelo.IdTipoMedida),
					modelo.Valor,
					modelo.Comentario,
					CurrentUser);
				return Json(new { success = true, message = "Medida actualizada correctamente." });
			}

			var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault());
			return Json(new { success = false, errors = errors });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al editar medida de rutina");
			return Json(new { success = false, message = "Error al editar la medida. Por favor intente nuevamente." });
		}
	}

	[HttpPost]
	public async Task<JsonResult> EliminarMedidaAjax([FromBody] EliminarMedidaRutinaViewModel modelo)
	{
		try
		{
			if (string.IsNullOrEmpty(modelo.IdMedidaRutina))
			{
				return Json(new { success = false, message = "ID no válido" });
			}

			await _rutinas.EliminarMedidaAsync(new Guid(modelo.IdMedidaRutina));
			return Json(new { success = true, message = "Medida eliminada correctamente." });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error al eliminar medida de rutina");
			return Json(new { success = false, message = "Error al eliminar la medida." });
		}
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
