// using System;
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
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

// namespace fitcare.Controllers
// {
// 	[Authorize]
// 	public class InstructoresController : BaseController
// 	{
// 		private readonly IDataCRUDBase<Instructor> _repoInstructores;
// 		private readonly IDataCRUDBase<Provincia> _repoProvincias;
// 		private readonly IDataCRUDBase<Canton> _repoCantones;
// 		private readonly IDataCRUDBase<Distrito> _repoDistritos;
// 		private readonly UserManager<ApplicationUser> _userManager;
// 		private readonly ILogger<InstructoresController> _logger;
// 		private readonly string NOMBRE_CARPETA_IMAGENES = "Instructores";

// 		public InstructoresController(IDataCRUDBase<Instructor> repoInstructores,
// 									  IDataCRUDBase<Provincia> repoProvincias,
// 									  IDataCRUDBase<Canton> repoCantones,
// 									  IDataCRUDBase<Distrito> repoDistritos,
// 									  UserManager<ApplicationUser> userManager,
// 									  RoleManager<ApplicationRole> roleManager,
// 									  IConfiguration configuration,
// 									  IHttpContextAccessor contextAccesor,
// 									  ILogger<InstructoresController> logger,
// 									  IWebHostEnvironment environment,
// 									  IDataCRUDBase<Imagen> repoImagenes)
// 		: base(userManager, roleManager, configuration, contextAccesor, environment, repoImagenes)
// 		{
// 			_repoInstructores = repoInstructores;
// 			_repoProvincias = repoProvincias;
// 			_repoCantones = repoCantones;
// 			_repoDistritos = repoDistritos;
// 			_userManager = userManager;
// 			_logger = logger;
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Inicio()
// 		{
// 			IEnumerable<ApplicationUser> listaUsuariosInstructores = await _userManager.GetUsersInRoleAsync("Instructor");
// 			IEnumerable<InicioClientesInstructoresViewModel> modelo = listaUsuariosInstructores.Select(u => new InicioClientesInstructoresViewModel(u)).ToList();
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
// 		public async Task<ActionResult> Nuevo(NuevoInstructorViewModel modelo)
// 		{
// 			if (!ModelState.IsValid)
// 			{
// 				await CargarViewBags();
// 				ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Instructor)));
// 				return View(modelo);
// 			}

// 			ApplicationUser usuario = await _userManager.FindByIdAsync(modelo.IdUsuario);
// 			IdentityResult rolAsignado = await _userManager.AddToRoleAsync(usuario, "Instructor");

// 			if (rolAsignado.Succeeded)
// 			{
// 				Instructor instructor = await modelo.Entidad(_repoDistritos);
// 				Imagen imagen = await GuardarImagenAsync(Factory.SetGuid(usuario.Id), modelo.Fotografia, NOMBRE_CARPETA_IMAGENES);
// 				if (imagen is null) throw new Exception(Messages.MensajeErrorGuardandoArchivo);
// 				await _repoInstructores.CreateAsync(instructor);
// 				return RedirectToAction(nameof(Inicio));
// 			}
// 			else
// 			{
// 				await CargarViewBags();
// 				ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Instructor)));
// 				return View(modelo);
// 			}
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Editar(string id)
// 		{
// 			ApplicationUser usuario = await _userManager.FindByIdAsync(id);
// 			if (usuario is null) throw new KeyNotFoundException();
// 			Instructor instructor = await _repoInstructores.ReadByIdAsync(Factory.SetGuid(usuario.Id));
// 			ModificarInstructorViewModel modelo = new(usuario, instructor);
// 			await CargarViewBags();
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Reporte()
// 		{
// 			IEnumerable<ApplicationUser> listaUsuariosInstructores = await _userManager.GetUsersInRoleAsync("Instructor");
// 			IEnumerable<Instructor> listaInstructores = (IEnumerable<Instructor>)listaUsuariosInstructores.Select(async u => await _repoInstructores.ReadByIdAsync(Factory.SetGuid(u.Id))).ToList();
// 			IEnumerable<ReporteInstructorViewModel> modelo = listaInstructores.Select(i => new ReporteInstructorViewModel()).ToList();
// 			return View(modelo);
// 		}

// 		private async Task CargarViewBags()
// 		{
// 			IList<ApplicationUser> usuariosInstructores = await _userManager.GetUsersInRoleAsync("Instructor");
// 			IList<ApplicationUser> listaUsuarios = await _userManager.Users.ToListAsync();
// 			IList<ApplicationUser> usuariosDisponibles = listaUsuarios.GetUsuariosDisponiblesParainstructor(usuariosInstructores); // Usuarios disponibles para registrar como instructor

// 			ViewBag.ListaUsuarios = usuariosDisponibles.Select(p => new SelectListItem { Value = Convert.ToString(p.Id, new CultureInfo("es-CR")), Text = string.Format("{0} {1} {2}", p.Name, p.FirstLastName, p.SecondLastName) }).ToList();
// 			ViewBag.ListaProvincias = await CargarListaSeleccionProvincias(_repoProvincias);
// 			ViewBag.ListaCantones = await CargarListaSeleccionCantones(_repoCantones);
// 			ViewBag.ListaDistritos = await CargarListaSeleccionDistritos(_repoDistritos);
// 			//ViewBag.ListaDistritos = await CargarListaSeleccionDistritosConCantonesProvincias(_repoDistritos);
// 		}
// 	}
// }
