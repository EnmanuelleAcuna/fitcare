using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
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
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace fitcare.Controllers;

[Authorize]
public class CuentasController : BaseController
{
	private readonly ApplicationUserManager<ApplicationUser> _userManager;
	private readonly RoleManager<ApplicationRole> _roleManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly IConfiguration _configuration;
	private readonly IEmailSender _emailSender;
	private readonly ILogger<CuentasController> _logger;
	private readonly IBaseCore<PlanMembresia> _planesMembresia;

	public CuentasController(ApplicationUserManager<ApplicationUser> userManager,
		RoleManager<ApplicationRole> roleManager,
		SignInManager<ApplicationUser> signInManager,
		IDivisionTerritorial divisionTerritorial,
		IConfiguration configuration,
		IHttpContextAccessor contextAccesor,
		IEmailSender emailSender,
		ILogger<CuentasController> logger,
		IWebHostEnvironment environment,
		IBaseCore<PlanMembresia> planesMembresia)
		: base(divisionTerritorial, userManager, roleManager, configuration, contextAccesor, environment)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_signInManager = signInManager;
		_configuration = configuration;
		_emailSender = emailSender;
		_logger = logger;
		_planesMembresia = planesMembresia;
	}

	[HttpGet]
	[AllowAnonymous]
	public IActionResult IniciarSesion(string returnUrl = null)
	{
		if (_signInManager.IsSignedIn(User))
		{
			return RedirectToAction("Admin", "Home");
		}
		
		ViewBag.ReturnUrl = returnUrl;
		// await CreateDefaultUser();
		return View();
	}

	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> IniciarSesion(IniciarSesionViewModel modelo, string returnUrl)
	{
		returnUrl ??= Url.Content("~/Home/Admin");

		if (!ModelState.IsValid)
		{
			ModelState.AddModelError("", "Datos incorrectos.");
			return View(modelo);
		}
		
		ApplicationUser usuario = await _userManager.FindByEmailAsync(modelo.Correo);
		if (usuario is null || (usuario.Active.HasValue && !usuario.Active.Value))
		{
			ModelState.AddModelError(string.Empty, "Correo electrónico y/o contraseña incorrectos.");
			return View(modelo);
		}

		// This doesn't count login failures towards account lockout
		// To enable password failures to trigger account lockout, change to shouldLockout: true
		SignInResult result = await _signInManager.PasswordSignInAsync(modelo.Correo, modelo.Contrasena,
			isPersistent: false, lockoutOnFailure: false);

		if (result.Succeeded)
		{
			IdentityResult ultimaConexionActualizada = await _userManager.UpdateLastSession(usuario);
			if (ultimaConexionActualizada.Succeeded) return RedirectToLocal(returnUrl);
		}
		else
		{
			ModelState.AddModelError(string.Empty, "Correo electrónico y/o contraseña incorrectos.");
			return View(modelo);
		}

		if (result.RequiresTwoFactor)
			return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = false });
		if (result.IsLockedOut) return RedirectToPage("./Lockout");

		// Si llega a este punto, quiere decir que hubo un error
		ModelState.AddModelError(string.Empty, "Ocurrió un error al iniciar sesión.");
		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CerrarSesion()
	{
		await _signInManager.SignOutAsync();
		return RedirectToAction("IniciarSesion", "Cuentas");
	}

	[HttpGet]
	[AllowAnonymous]
	public ActionResult SolicitarContrasena() => View();

	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> SolicitarContrasena(OlvidoContrasenaViewModel modelo)
	{
		if (!ModelState.IsValid)
		{
			ModelState.AddModelError("", "Error, el modelo no es válido.");
			return View(modelo);
		}

		ApplicationUser usuario = await _userManager.FindByEmailAsync(modelo.CorreoElectronico);

		if (usuario is null)
			return
				View(nameof(SolicitarContrasenaConfirmada)); // No revelar que el usuario no existe, redirigir a la confirmación

		if ((bool)usuario.Active)
		{
			string token =
				await _userManager
					.GeneratePasswordResetTokenAsync(usuario); // Generar un token de restablecimiento de contraseña

			string urlRestablecimientoContrasena = Url.Action(nameof(RestablecerContrasena), "Cuentas",
				new { userId = usuario.Id, code = token }, protocol: Request.Scheme); // Crear enlace

			// Configurar correo y enviarlo
			string mensajeCorreo = string.Format(new CultureInfo("es-CR"),
				"Para restablecer su contraseña haga click <a href=\"{0}\">aquí</a>", urlRestablecimientoContrasena);
			await _emailSender.SendEmailAsync(modelo.CorreoElectronico, "Restablecer contraseña", mensajeCorreo);

			return View(nameof(SolicitarContrasenaConfirmada));
		}
		else
		{
			ModelState.AddModelError("", "El usuario está inactivo");
			return View(modelo);
		}
	}

	[HttpGet]
	[AllowAnonymous]
	public ActionResult SolicitarContrasenaConfirmada() => View();

	[HttpGet]
	[AllowAnonymous]
	public ActionResult RestablecerContrasena(string code) => code == null ? View("Error") : View();

	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> RestablecerContrasena(RestablecerContrasenaViewModel modelo)
	{
		if (!ModelState.IsValid)
		{
			ModelState.AddModelError("", "Error, el modelo no es válido.");
			return View(modelo);
		}

		ApplicationUser usuario = await _userManager.FindByEmailAsync(modelo.CorreoElectronico);

		if (usuario is null)
			return
				View(nameof(
					RestablecerContrasenaConfirmada)); // No revelar que el usuario no existe, redirigir a la confirmación

		IdentityResult result = await _userManager.ResetPasswordAsync(usuario, modelo.Code, modelo.Contrasena);

		if (result.Succeeded) return View(nameof(RestablecerContrasenaConfirmada));

		AddErrors(result);
		return View();
	}

	[HttpGet]
	[AllowAnonymous]
	public ActionResult RestablecerContrasenaConfirmada() => View();

	[HttpGet]
	public ActionResult Usuarios()
	{
		var listaUsuarios = _userManager.Users.ToList();
		var modelo = listaUsuarios.Select(u => new UsuarioViewModel(u)).ToList();
		return View(modelo);
	}

	[HttpGet]
	public ActionResult AgregarUsuario()
	{
		ViewBag.ListaRoles = CargarListaSeleccionRolesSistema();
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> AgregarUsuario(AgregarUsuarioViewModel modelo, IFormCollection collection)
	{
		if (ModelState.IsValid)
		{
			ApplicationUser usuario = modelo.Entidad();

			IList<string> rolesSeleccionados = ObtenerRolesSeleccionados(collection);

			IdentityResult usuarioCreado = await _userManager.CreateAsync(usuario, modelo.Contrasena);
			IdentityResult rolesAsignados = usuarioCreado.Succeeded
				? await _userManager.AddToRolesAsync(usuario, rolesSeleccionados)
				: IdentityResult.Failed();

			if (usuarioCreado.Succeeded && rolesAsignados.Succeeded) return RedirectToAction(nameof(Usuarios));

			AddErrors(usuarioCreado);
			AddErrors(rolesAsignados);
		}

		ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(ApplicationUser)));
		ViewBag.ListaRoles = CargarListaSeleccionRolesSistema();
		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> EditarUsuario(string id)
	{
		ApplicationUser usuario = await _userManager.FindByIdAsync(id);

		if (usuario == null) return NotFound();

		IList<ApplicationRole> rolesUsuario = await ObtenerRolesUsuario(usuario);

		EditarUsuarioViewModel modelo = new(usuario, rolesUsuario);

		ViewBag.ListaRoles = CargarListaSeleccionRolesSistema();

		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> EditarUsuario(EditarUsuarioViewModel modelo, IFormCollection collection)
	{
		if (ModelState.IsValid)
		{
			ApplicationUser usuario = modelo.Entidad();

			IList<string> rolesSeleccionados = ObtenerRolesSeleccionados(collection);

			IdentityResult usuarioActualizado = await _userManager.UpdatePersonalInformation(usuario);
			IdentityResult rolesActualizados = usuarioActualizado.Succeeded
				? await _userManager.ActualizarRolesUsuario(usuario, rolesSeleccionados)
				: IdentityResult.Failed();

			if (usuarioActualizado.Succeeded && rolesActualizados.Succeeded)
			{
				return RedirectToAction(nameof(Usuarios));
			}
			else
			{
				AddErrors(usuarioActualizado);
				AddErrors(rolesActualizados);
			}
		}

		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(ApplicationUser)));
		ViewBag.ListaRoles = CargarListaSeleccionRolesSistema();

		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> EliminarUsuario(string id)
	{
		ApplicationUser usuario = await _userManager.FindByIdAsync(id);

		if (usuario == null) return NotFound();

		IList<ApplicationRole> rolesUsuario = await ObtenerRolesUsuario(usuario);

		EditarUsuarioViewModel modelo = new(usuario, rolesUsuario);

		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> EliminarUsuario(EditarUsuarioViewModel modelo)
	{
		ApplicationUser usuario = await _userManager.FindByIdAsync(modelo.IdUsuario);

		if (usuario == null) return NotFound();

		IdentityResult usuarioEliminado = await _userManager.DeleteAsync(usuario);

		if (usuarioEliminado.Succeeded) return RedirectToAction(nameof(Usuarios));

		// Si se llega a este punto, hubo un error
		AddErrors(usuarioEliminado);

		ModelState.AddModelError("", Messages.MensajeErrorEliminar(nameof(usuario)));

		return View(modelo);
	}

	[HttpGet]
	public IActionResult Roles()
	{
		IList<ApplicationRole> listaRoles = _roleManager.Roles.ToList();
		IList<InicioRolesViewModel> modelo = listaRoles.Select(x => new InicioRolesViewModel(x)).ToList();
		return View(modelo);
	}

	[HttpGet]
	public IActionResult AgregarRol() => View();

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> AgregarRol(NuevoRolViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			ApplicationRole rol = modelo.Entidad();
			IdentityResult rolCreado = await _roleManager.CreateAsync(rol);

			if (rolCreado.Succeeded) return RedirectToAction(nameof(Roles));

			AddErrors(rolCreado);
		}

		// Si se llega a este punto, hubo un error
		ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(ApplicationRole)));

		return View(modelo);
	}

	[HttpGet]
	public async Task<IActionResult> EditarRol(string id)
	{
		ApplicationRole rol = await _roleManager.FindByIdAsync(id);

		if (rol == null) return NotFound();

		EditarRolViewModel modelo = new(rol);

		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> EditarRol(EditarRolViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			ApplicationRole rol = await _roleManager.FindByIdAsync(modelo.IdRol);

			rol.ActualizarDatos(modelo.Nombre, modelo.Descripcion, modelo.Estado);

			IdentityResult RolActualizado = await _roleManager.UpdateAsync(rol);

			if (RolActualizado.Succeeded) return RedirectToAction(nameof(Roles));

			AddErrors(RolActualizado);
		}

		// Si se llega a este punto, hubo un error
		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(ApplicationRole)));

		return View(modelo);
	}

	[HttpGet]
	public async Task<ActionResult> EliminarRol(string id)
	{
		ApplicationRole rol = await _roleManager.FindByIdAsync(id);

		if (rol == null) return NotFound();

		EditarRolViewModel modelo = new(rol);

		return View(modelo);
	}

	[HttpPost]
	public async Task<ActionResult> EliminarRol(EditarRolViewModel modelo)
	{
		ApplicationRole rol = await _roleManager.FindByIdAsync(modelo.IdRol);

		if (rol == null) return NotFound();

		IdentityResult rolEliminado = await _roleManager.DeleteAsync(rol);

		if (rolEliminado.Succeeded) return RedirectToAction(nameof(Roles));

		// Si se llega a este punto, hubo un error
		AddErrors(rolEliminado);

		ModelState.AddModelError("", Messages.MensajeErrorEliminar(nameof(ApplicationRole)));

		return View(modelo);
	}

	public async Task<ActionResult> ListarInstructores()
	{
		var usuariosInstructor = await _userManager.GetUsersInRoleAsync("Instructor");
		var modelo = usuariosInstructor.Select(x => new UsuarioViewModel(x));
		return View(modelo);
	}

	public async Task<ActionResult> RegistrarNuevoInstructor()
	{
		var usuariosNoInstructor = await _userManager.GetUsersNotInRoleAsync("Instructor");
		var modelo = usuariosNoInstructor.Select(x => new UsuarioViewModel(x));
		return View(modelo);
	}

	[HttpGet]
	public async Task<IActionResult> RegistrarInstructor(string id)
	{
		var usuario = await _userManager.FindByIdAsync(id);
		var modelo = new AgregarInstructorViewModel(usuario);

		ViewBag.Provincias = await CargarListaSeleccionProvincias();
		ViewBag.Cantones = await CargarListaSeleccionCantones();
		ViewBag.Distritos = await CargarListaSeleccionDistritos();

		return View(modelo);
	}

	[HttpPost]
	public async Task<IActionResult> RegistrarInstructor(AgregarInstructorViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			// var rutaFotografia = GuardarImagenDisco(modelo.ProfilePicture);

			var usuarioRegistradoComoInstructor =
				await _userManager.RegistrarUsuarioComoInstructor(modelo.Entidad(), string.Empty);

			if (usuarioRegistradoComoInstructor.Succeeded) return RedirectToAction(nameof(Instructores));

			AddErrors(usuarioRegistradoComoInstructor);
		}

		ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(ApplicationUser)));

		ViewBag.Provincias = await CargarListaSeleccionProvincias();
		ViewBag.Cantones = await CargarListaSeleccionCantones();
		ViewBag.Distritos = await CargarListaSeleccionDistritos();

		return View(modelo);
	}

	public async Task<ActionResult> ListarClientes()
	{
		var usuariosCliente = await _userManager.GetUsersInRoleAsync("Cliente");
		var modelo = usuariosCliente.Select(x => new UsuarioViewModel(x));
		return View(modelo);
	}

	public async Task<ActionResult> RegistrarNuevoCliente()
	{
		var usuariosNoCliente = await _userManager.GetUsersNotInRoleAsync("Cliente");
		var modelo = usuariosNoCliente.Select(x => new UsuarioViewModel(x));
		return View(modelo);
	}

	[HttpGet]
	public async Task<IActionResult> RegistrarCliente(string id)
	{
		var usuario = await _userManager.FindByIdAsync(id);
		var modelo = new AgregarClienteViewModel(usuario);

		ViewBag.Provincias = await CargarListaSeleccionProvincias();
		ViewBag.Cantones = await CargarListaSeleccionCantones();
		ViewBag.Distritos = await CargarListaSeleccionDistritos();
		ViewBag.PlanesMembresia = await CargarListaSeleccionPlanesMembresia();

		return View(modelo);
	}

	[HttpPost]
	public async Task<IActionResult> RegistrarCliente(AgregarClienteViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			// var rutaFotografia = GuardarImagenDisco(modelo.ProfilePicture);

			var plan = await _planesMembresia.ReadByIdAsync(new Guid(modelo.IdPlanMembresia));

			var usuarioRegistradoComoCliente =
				await _userManager.RegistrarUsuarioComoCliente(modelo.Entidad(), string.Empty, new Guid(modelo.IdPlanMembresia), plan.Dias);

			if (usuarioRegistradoComoCliente.Succeeded) return RedirectToAction(nameof(Clientes));

			AddErrors(usuarioRegistradoComoCliente);
		}

		ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(ApplicationUser)));

		ViewBag.Provincias = await CargarListaSeleccionProvincias();
		ViewBag.Cantones = await CargarListaSeleccionCantones();
		ViewBag.Distritos = await CargarListaSeleccionDistritos();
		ViewBag.PlanesMembresia = await CargarListaSeleccionPlanesMembresia();

		return View(modelo);
	}

	public async Task<ActionResult> Instructores()
	{
		var usuariosInstructor = await _userManager.GetUsersInRoleWithDivisionTerritorialInfoAsync("Instructor");
		var modelo = usuariosInstructor.Select(x => new InstructorListViewModel(x));
		return View(modelo);
	}

	[HttpGet]
	public async Task<IActionResult> EditarInstructor(string id)
	{
		var usuario = await _userManager.FindByIdAsync(id);

		if (usuario == null) return NotFound();

		var modelo = new AgregarInstructorViewModel(usuario)
		{
			IdProvincia = usuario.IdProvincia?.ToString(),
			IdCanton = usuario.IdCanton?.ToString(),
			IdDistrito = usuario.IdDistrito?.ToString(),
			FechaIngreso = usuario.FechaIngresoInscripcion ?? DateTime.Now
		};

		ViewBag.Provincias = await CargarListaSeleccionProvincias();
		ViewBag.Cantones = await CargarListaSeleccionCantones();
		ViewBag.Distritos = await CargarListaSeleccionDistritos();

		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> EditarInstructor(AgregarInstructorViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			var resultado = await _userManager.ActualizarDatosInstructor(modelo.Entidad());

			if (resultado.Succeeded) return RedirectToAction(nameof(Instructores));

			AddErrors(resultado);
		}

		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(ApplicationUser)));

		ViewBag.Provincias = await CargarListaSeleccionProvincias();
		ViewBag.Cantones = await CargarListaSeleccionCantones();
		ViewBag.Distritos = await CargarListaSeleccionDistritos();

		return View(modelo);
	}

	[HttpPost]
	public async Task<JsonResult> DesafiliarInstructor([FromBody] DesafiliarUsuarioRequest request)
	{
		if (string.IsNullOrEmpty(request?.Id))
		{
			return Json(new { success = false, message = "ID de usuario no válido" });
		}

		var resultado = await _userManager.DesafiliarUsuarioComoInstructor(request.Id);

		if (resultado.Succeeded)
		{
			return Json(new { success = true, message = "Instructor desafiliado exitosamente" });
		}

		var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
		return Json(new { success = false, message = errores });
	}

	[HttpGet]
	public async Task<IActionResult> ExportarInstructores()
	{
		var usuariosInstructor = await _userManager.GetUsersInRoleWithDivisionTerritorialInfoAsync("Instructor");
		var instructores = usuariosInstructor.Select(x => new InstructorExportViewModel(x)).ToList();

		using var workbook = new XLWorkbook();
		var worksheet = workbook.Worksheets.Add("Instructores");

		// Headers
		worksheet.Cell(1, 1).Value = "Número de identificación";
		worksheet.Cell(1, 2).Value = "Nombre";
		worksheet.Cell(1, 3).Value = "Primer apellido";
		worksheet.Cell(1, 4).Value = "Segundo apellido";
		worksheet.Cell(1, 5).Value = "Nombre completo";
		worksheet.Cell(1, 6).Value = "Correo electrónico";
		worksheet.Cell(1, 7).Value = "Provincia";
		worksheet.Cell(1, 8).Value = "Cantón";
		worksheet.Cell(1, 9).Value = "Distrito";
		worksheet.Cell(1, 10).Value = "Dirección completa";
		worksheet.Cell(1, 11).Value = "Fecha de ingreso";
		worksheet.Cell(1, 12).Value = "Estado";

		// Style headers
		var headerRange = worksheet.Range(1, 1, 1, 12);
		headerRange.Style.Font.Bold = true;
		headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#198754");
		headerRange.Style.Font.FontColor = XLColor.White;

		// Data
		int row = 2;
		foreach (var instructor in instructores)
		{
			worksheet.Cell(row, 1).Value = instructor.NumeroIdentificacion;
			worksheet.Cell(row, 2).Value = instructor.Nombre;
			worksheet.Cell(row, 3).Value = instructor.PrimerApellido;
			worksheet.Cell(row, 4).Value = instructor.SegundoApellido;
			worksheet.Cell(row, 5).Value = instructor.NombreCompleto;
			worksheet.Cell(row, 6).Value = instructor.Correo;
			worksheet.Cell(row, 7).Value = instructor.Provincia;
			worksheet.Cell(row, 8).Value = instructor.Canton;
			worksheet.Cell(row, 9).Value = instructor.Distrito;
			worksheet.Cell(row, 10).Value = instructor.DireccionCompleta;
			worksheet.Cell(row, 11).Value = instructor.FechaIngreso?.ToString("dd/MM/yyyy");
			worksheet.Cell(row, 12).Value = instructor.Estado;
			row++;
		}

		worksheet.Columns().AdjustToContents();

		using var stream = new MemoryStream();
		workbook.SaveAs(stream);
		var content = stream.ToArray();
		var fileName = $"Instructores_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

		return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
	}

	public async Task<ActionResult> Clientes()
	{
		var usuariosCliente = await _userManager.GetUsersInRoleWithDivisionTerritorialInfoAsync("Cliente");
		var modelo = usuariosCliente.Select(x => new ClienteListViewModel(x));
		return View(modelo);
	}

	[HttpGet]
	public async Task<IActionResult> EditarCliente(string id)
	{
		var usuario = await _userManager.FindByIdAsync(id);

		if (usuario == null) return NotFound();

		var modelo = new AgregarClienteViewModel(usuario)
		{
			IdProvincia = usuario.IdProvincia?.ToString(),
			IdCanton = usuario.IdCanton?.ToString(),
			IdDistrito = usuario.IdDistrito?.ToString(),
			IdPlanMembresia = usuario.IdPlanMembresia?.ToString(),
			FechaInscripcion = usuario.FechaIngresoInscripcion ?? DateTime.Now,
			FechaRenovacion = usuario.FechaRenovacion ?? DateTime.Now.AddDays(30)
		};

		ViewBag.Provincias = await CargarListaSeleccionProvincias();
		ViewBag.Cantones = await CargarListaSeleccionCantones();
		ViewBag.Distritos = await CargarListaSeleccionDistritos();
		ViewBag.PlanesMembresia = await CargarListaSeleccionPlanesMembresia();

		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> EditarCliente(AgregarClienteViewModel modelo)
	{
		if (ModelState.IsValid)
		{
			var plan = await _planesMembresia.ReadByIdAsync(new Guid(modelo.IdPlanMembresia));

			var resultado = await _userManager.ActualizarDatosCliente(modelo.Entidad(), new Guid(modelo.IdPlanMembresia), plan.Dias);

			if (resultado.Succeeded) return RedirectToAction(nameof(Clientes));

			AddErrors(resultado);
		}

		ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(ApplicationUser)));

		ViewBag.Provincias = await CargarListaSeleccionProvincias();
		ViewBag.Cantones = await CargarListaSeleccionCantones();
		ViewBag.Distritos = await CargarListaSeleccionDistritos();
		ViewBag.PlanesMembresia = await CargarListaSeleccionPlanesMembresia();

		return View(modelo);
	}

	[HttpPost]
	public async Task<JsonResult> DesafiliarCliente([FromBody] DesafiliarUsuarioRequest request)
	{
		if (string.IsNullOrEmpty(request?.Id))
		{
			return Json(new { success = false, message = "ID de usuario no válido" });
		}

		var resultado = await _userManager.DesafiliarUsuarioComoCliente(request.Id);

		if (resultado.Succeeded)
		{
			return Json(new { success = true, message = "Cliente desafiliado exitosamente" });
		}

		var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
		return Json(new { success = false, message = errores });
	}

	[HttpGet]
	public async Task<IActionResult> ExportarClientes()
	{
		var usuariosCliente = await _userManager.GetUsersInRoleWithDivisionTerritorialInfoAsync("Cliente");
		var clientes = usuariosCliente.Select(x => new ClienteExportViewModel(x)).ToList();

		using var workbook = new XLWorkbook();
		var worksheet = workbook.Worksheets.Add("Clientes");

		// Headers
		worksheet.Cell(1, 1).Value = "Número de identificación";
		worksheet.Cell(1, 2).Value = "Nombre";
		worksheet.Cell(1, 3).Value = "Primer apellido";
		worksheet.Cell(1, 4).Value = "Segundo apellido";
		worksheet.Cell(1, 5).Value = "Nombre completo";
		worksheet.Cell(1, 6).Value = "Correo electrónico";
		worksheet.Cell(1, 7).Value = "Provincia";
		worksheet.Cell(1, 8).Value = "Cantón";
		worksheet.Cell(1, 9).Value = "Distrito";
		worksheet.Cell(1, 10).Value = "Dirección completa";
		worksheet.Cell(1, 11).Value = "Fecha de inscripción";
		worksheet.Cell(1, 12).Value = "Fecha de renovación";
		worksheet.Cell(1, 13).Value = "Estado";

		// Style headers
		var headerRange = worksheet.Range(1, 1, 1, 13);
		headerRange.Style.Font.Bold = true;
		headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#198754");
		headerRange.Style.Font.FontColor = XLColor.White;

		// Data
		int row = 2;
		foreach (var cliente in clientes)
		{
			worksheet.Cell(row, 1).Value = cliente.NumeroIdentificacion;
			worksheet.Cell(row, 2).Value = cliente.Nombre;
			worksheet.Cell(row, 3).Value = cliente.PrimerApellido;
			worksheet.Cell(row, 4).Value = cliente.SegundoApellido;
			worksheet.Cell(row, 5).Value = cliente.NombreCompleto;
			worksheet.Cell(row, 6).Value = cliente.Correo;
			worksheet.Cell(row, 7).Value = cliente.Provincia;
			worksheet.Cell(row, 8).Value = cliente.Canton;
			worksheet.Cell(row, 9).Value = cliente.Distrito;
			worksheet.Cell(row, 10).Value = cliente.DireccionCompleta;
			worksheet.Cell(row, 11).Value = cliente.FechaInscripcion?.ToString("dd/MM/yyyy");
			worksheet.Cell(row, 12).Value = cliente.FechaRenovacion?.ToString("dd/MM/yyyy");
			worksheet.Cell(row, 13).Value = cliente.Estado;
			row++;
		}

		worksheet.Columns().AdjustToContents();

		using var stream = new MemoryStream();
		workbook.SaveAs(stream);
		var content = stream.ToArray();
		var fileName = $"Clientes_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

		return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
	}

	[HttpGet]
	public async Task<JsonResult> ObtenerCantonesPorProvincia(string idProvincia)
	{
		if (string.IsNullOrEmpty(idProvincia))
		{
			return Json(new List<object>());
		}

		var cantones = await CargarListaSeleccionCantones();
		var cantonesFiltrados = cantones
			.Where(c => c.IdPadre == idProvincia)
			.Select(c => new { value = c.Value, text = c.Text })
			.ToList();

		return Json(cantonesFiltrados);
	}

	[HttpGet]
	public async Task<JsonResult> ObtenerDistritosPorCanton(string idCanton)
	{
		if (string.IsNullOrEmpty(idCanton))
		{
			return Json(new List<object>());
		}

		var distritos = await CargarListaSeleccionDistritos();
		var distritosFiltrados = distritos
			.Where(d => d.IdPadre == idCanton)
			.Select(d => new { value = d.Value, text = d.Text })
			.ToList();

		return Json(distritosFiltrados);
	}

	[HttpGet]
	public async Task<IActionResult> PagosMembresia()
	{
		var usuariosCliente = await _userManager.GetUsersInRoleAsync("Cliente");
		var modelo = new List<PagoMembresiaViewModel>();

		foreach (var cliente in usuariosCliente)
		{
			string nombrePlan = "Sin plan";
			if (cliente.IdPlanMembresia.HasValue)
			{
				try
				{
					var plan = await _planesMembresia.ReadByIdAsync(cliente.IdPlanMembresia.Value);
					nombrePlan = plan.Nombre;
				}
				catch
				{
					nombrePlan = "Plan no encontrado";
				}
			}

			modelo.Add(new PagoMembresiaViewModel
			{
				IdCliente = cliente.Id,
				NombreCompleto = cliente.FullName,
				NombrePlan = nombrePlan,
				FechaInscripcion = cliente.FechaIngresoInscripcion,
				FechaRenovacion = cliente.FechaRenovacion,
				EstaVencido = cliente.FechaRenovacion.HasValue && cliente.FechaRenovacion.Value < DateTime.Now
			});
		}

		return View(modelo);
	}

	[HttpGet]
	public async Task<IActionResult> ConfirmarPago(string id)
	{
		var cliente = await _userManager.FindByIdAsync(id);

		if (cliente == null) return NotFound();

		if (!cliente.IdPlanMembresia.HasValue)
		{
			TempData["Error"] = "El cliente no tiene un plan de membresía asignado";
			return RedirectToAction(nameof(PagosMembresia));
		}

		var plan = await _planesMembresia.ReadByIdAsync(cliente.IdPlanMembresia.Value);

		var modelo = new ConfirmarPagoViewModel
		{
			IdCliente = cliente.Id,
			NombreCompleto = cliente.FullName,
			NombrePlan = plan.Nombre,
			DiasPlan = plan.Dias,
			FechaInscripcion = cliente.FechaIngresoInscripcion ?? DateTime.Now,
			FechaRenovacionActual = cliente.FechaRenovacion ?? DateTime.Now,
			NuevaFechaRenovacion = (cliente.FechaRenovacion ?? DateTime.Now).AddDays(plan.Dias)
		};

		return View(modelo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> RegistrarPago(string id)
	{
		var cliente = await _userManager.FindByIdAsync(id);

		if (cliente == null) return NotFound();

		if (!cliente.IdPlanMembresia.HasValue)
		{
			TempData["Error"] = "El cliente no tiene un plan de membresía asignado";
			return RedirectToAction(nameof(PagosMembresia));
		}

		var plan = await _planesMembresia.ReadByIdAsync(cliente.IdPlanMembresia.Value);

		var resultado = await _userManager.ExtenderMembresiaCliente(id, plan.Dias);

		if (resultado.Succeeded)
		{
			TempData["Success"] = $"Pago registrado exitosamente. La membresía se extendió {plan.Dias} días.";
		}
		else
		{
			TempData["Error"] = "Error al registrar el pago";
		}

		return RedirectToAction(nameof(PagosMembresia));
	}

	private async Task<IList<SelectListItemWithData>> CargarListaSeleccionPlanesMembresia()
	{
		var planes = await _planesMembresia.ReadAllAsync();
		return planes
			.Where(p => p.Estado)
			.Select(p => new SelectListItemWithData
			{
				Value = p.Id.ToString(),
				Text = $"{p.Nombre} ({p.Dias} días - ₡{p.Costo:N0})",
				Dias = p.Dias
			})
			.ToList();
	}
}
