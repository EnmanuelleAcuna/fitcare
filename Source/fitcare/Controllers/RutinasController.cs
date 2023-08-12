// using System.Collections.Generic;
// using System.Globalization;
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
// using Microsoft.AspNetCore.Identity.UI.Services;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

// namespace fitcare.Controllers
// {
// 	[Authorize]
// 	public class RutinasController : BaseController
// 	{
// 		private readonly IDataCRUDBase<Rutina> _repoRutinas;
// 		private readonly IDataCRUDBase<TipoMedida> _repoTiposMedida;
// 		private readonly IDataCRUDBase<Ejercicio> _repoEjercicios;
// 		private readonly IDataCRUDBase<Instructor> _repoInstructores;
// 		private readonly IDataCRUDBase<Cliente> _repoClientes;
// 		private readonly IEmailSender _emailSender;
// 		private readonly ILogger<RutinasController> _logger;

// 		public RutinasController(IDataCRUDBase<Rutina> repoRutinas,
// 								 IDataCRUDBase<TipoMedida> repoTiposMedida,
// 								 IDataCRUDBase<Ejercicio> repoEjercicios,
// 								 IDataCRUDBase<Instructor> repoInstructores,
// 								 IDataCRUDBase<Cliente> repoClientes,
// 								 UserManager<ApplicationUser> userManager,
// 								 RoleManager<ApplicationRole> roleManager,
// 								 IConfiguration configuration,
// 								 IHttpContextAccessor contextAccesor,
// 								 IEmailSender emailSender,
// 								 ILogger<RutinasController> logger,
// 								 IWebHostEnvironment environment,
// 								 IDataCRUDBase<Imagen> repoImagenes)
// 		: base(userManager, roleManager, configuration, contextAccesor, environment, repoImagenes)
// 		{
// 			_repoRutinas = repoRutinas;
// 			_repoTiposMedida = repoTiposMedida;
// 			_repoEjercicios = repoEjercicios;
// 			_repoInstructores = repoInstructores;
// 			_repoClientes = repoClientes;
// 			_emailSender = emailSender;
// 			_logger = logger;
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Inicio()
// 		{
// 			IEnumerable<Rutina> listaRutinas = await _repoRutinas.ReadAllAsync();
// 			IEnumerable<InicioRutinasViewModel> modelo = listaRutinas.Select(x => new InicioRutinasViewModel(x)).ToList();
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Nuevo()
// 		{
// 			await CargarViewBags();
// 			NuevaRutinaViewModel modelo = new();
// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		[ValidateAntiForgeryToken]
// 		public async Task<IActionResult> Nuevo(NuevaRutinaViewModel modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				Rutina rutina = await modelo.Entidad();
// 				await _repoRutinas.CreateAsync(rutina);

// 				// Obtener informacion del cliente y enviar correo de notificación de creación de rutina
// 				Cliente cliente = await _repoClientes.ReadByIdAsync(Factory.SetGuid(rutina.Cliente.Id));
// 				string urlVisualizacionRutina = Url.Action("Detalle", "Rutinas", new { id = rutina.Id }, protocol: Request.Scheme);
// 				string mensajeDeCorreo = string.Format(new CultureInfo("es-CR"), "Hola {0} <br /> Se ha registrado su rutina en fitcare. <br /> Para verla o darle seguimiento puede ir al siguiente <a href=\"{1}\">enlace</a>", "", urlVisualizacionRutina);
// 				//await _emailSender.SendEmailAsync(cliente.Email, "fitcare: Registro de rutina", mensajeDeCorreo);

// 				return RedirectToAction("Inicio");
// 			}

// 			// Si se llega a este punto, hubo un error
// 			await CargarViewBags();
// 			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Rutina)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Editar(string id)
// 		{
// 			Rutina rutina = await _repoRutinas.ReadByIdAsync(Factory.SetGuid(id));
// 			EditarRutinaViewModel modelo = new(rutina);

// 			await CargarViewBags();
// 			return View(modelo);
// 		}

// 		[HttpPost]
// 		public async Task<ActionResult> Editar(EditarRutinaViewModel modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				Rutina rutina = await modelo.Entidad();
// 				await _repoRutinas.UpdateAsync(rutina);
// 				return RedirectToAction("Inicio");
// 			}

// 			// Si se llega a este punto, hubo un error
// 			await CargarViewBags();
// 			ModelState.AddModelError("", Messages.MensajeErrorActualizar(nameof(Rutina)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> ReporteResumido()
// 		{
// 			await CargarViewBags();

// 			// TODO
// 			//IEnumerable<ReporteRutinaResumido> datosReporte = _repoRutinas.ObtenerReporteRutinasResumido(string.Empty, string.Empty);
// 			//IEnumerable<ReporteRutinaResumidoViewModel> modelo = datosReporte.Select(i => new ReporteRutinaResumidoViewModel(i, _configuracion)).ToList();

// 			//return View(modelo);
// 			return View();
// 		}

// 		[HttpPost]
// 		public async Task<ActionResult> ReporteResumido(string idInstructor, string idCliente)
// 		{
// 			await CargarViewBags();

// 			_ = idInstructor ?? string.Empty;
// 			_ = idCliente ?? string.Empty;

// 			// TODO
// 			//IEnumerable<ReporteRutinaResumido> datosReporte = _repoRutinas.ObtenerReporteRutinasResumido(idInstructor, idCliente);
// 			//IEnumerable<ReporteRutinaResumidoViewModel> Modelo = datosReporte.Select(i => new ReporteRutinaResumidoViewModel(i, _configuracion)).ToList();

// 			//return View(Modelo);
// 			return View();
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> ReporteDetallado()
// 		{
// 			await CargarViewBags();

// 			// TODO
// 			//IEnumerable<ReporteRutinaDetallado> datosReporte = _repoRutinas.ObtenerReporteRutinasDetallado(string.Empty, string.Empty);
// 			//IEnumerable<ReporteRutinaDetalladoViewModel> viewModelData = datosReporte.Select(i => new ReporteRutinaDetalladoViewModel(i, _configuracion)).ToList();

// 			//return View(viewModelData);
// 			return View();
// 		}

// 		[HttpPost]
// 		public async Task<ActionResult> ReporteDetallado(string idInstructor, string idCliente)
// 		{
// 			await CargarViewBags();

// 			_ = idInstructor ?? string.Empty;
// 			_ = idCliente ?? string.Empty;

// 			// TODO
// 			//IEnumerable<ReporteRutinaDetallado> datosReporte = _repoRutinas.ObtenerReporteRutinasDetallado(idInstructor, idCliente);
// 			//IEnumerable<ReporteRutinaDetalladoViewModel> viewModelData = datosReporte.Select(i => new ReporteRutinaDetalladoViewModel(i, _configuracion)).ToList();

// 			//return View(viewModelData);
// 			return View();
// 		}

// 		private async Task CargarViewBags()
// 		{
// 			//ViewBag.ListaInstructores = CargarListaSeleccionInstructores(await _repoInstructores.ReadAllAsync());
// 			//ViewBag.ListaClientes = CargarListaSeleccionClientes(await _repoClientes.ReadAllAsync());
// 			ViewBag.ListaTiposMedida = CargarListaSeleccionTiposMedida(await _repoTiposMedida.ReadAllAsync());
// 			ViewBag.ListaEjercicios = CargarListaSeleccionEjercicios(await _repoEjercicios.ReadAllAsync());
// 		}
// 	}
// }
