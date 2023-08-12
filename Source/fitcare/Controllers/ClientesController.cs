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
// 	public class ClientesController : BaseController
// 	{
// 		private readonly IDataCRUDBase<Cliente> _repoClientes;
// 		private readonly IDataCRUDBase<Provincia> _repoProvincias;
// 		private readonly IDataCRUDBase<Canton> _repoCantones;
// 		private readonly IDataCRUDBase<Distrito> _repoDistritos;
// 		private readonly UserManager<ApplicationUser> _userManager;
// 		private readonly ILogger<ClientesController> _logger;

// 		public ClientesController(IDataCRUDBase<Cliente> repoClientes,
// 								  IDataCRUDBase<Provincia> repoProvincias,
// 								  IDataCRUDBase<Canton> repoCantones,
// 								  IDataCRUDBase<Distrito> repoDistritos,
// 								  UserManager<ApplicationUser> userManager,
// 								  RoleManager<ApplicationRole> roleManager,
// 								  IConfiguration configuration,
// 								  IHttpContextAccessor contextAccesor,
// 								  ILogger<ClientesController> logger,
// 								  IWebHostEnvironment environment,
// 								  IDataCRUDBase<Imagen> repoImagenes)
// 		: base(userManager, roleManager, configuration, contextAccesor, environment, repoImagenes)
// 		{
// 			_repoClientes = repoClientes;
// 			_repoProvincias = repoProvincias;
// 			_repoCantones = repoCantones;
// 			_repoDistritos = repoDistritos;
// 			_userManager = userManager;
// 			_logger = logger;
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Inicio()
// 		{
// 			IEnumerable<ApplicationUser> listaUsuariosClientes = await _userManager.GetUsersInRoleAsync("Cliente");
// 			IEnumerable<InicioClientesInstructoresViewModel> modelo = listaUsuariosClientes.Select(u => new InicioClientesInstructoresViewModel(u)).ToList();
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
// 		public async Task<ActionResult> Nuevo(NuevoClienteViewModel modelo)
// 		{
// 			if (ModelState.IsValid)
// 			{
// 				ApplicationUser usuario = await _userManager.FindByIdAsync(modelo.IdUsuario);
// 				IdentityResult rolAsignado = await _userManager.AddToRoleAsync(usuario, "Cliente");

// 				if (rolAsignado.Succeeded)
// 				{
// 					Cliente cliente = await modelo.Entidad(_repoDistritos, _userManager);
// 					// TODO: Solucionar esto.
// 					//ArchivoSubido_deprecated foto = ObtenerImagenSubida(modelo.Fotografia);
// 					//await _clientes.CreateAsync(cliente, foto, Factory.GetBitacoraAgregar(Factory.SetGuid(cliente.Id), User.Identity.Name));
// 					return RedirectToAction("Inicio");
// 				}

// 				IdentityResult rolEliminado = await _userManager.RemoveFromRoleAsync(usuario, "Cliente");
// 			}

// 			// Si se llega a este punto, hubo un error
// 			await CargarViewBags();
// 			ModelState.AddModelError("", Messages.MensajeErrorCrear(nameof(Cliente)));
// 			return View(modelo);
// 		}

// 		[HttpGet]
// 		public async Task<ActionResult> Reporte()
// 		{
// 			IEnumerable<ApplicationUser> listaUsuariosClientes = await _userManager.GetUsersInRoleAsync("Cliente");
// 			IEnumerable<Cliente> listaClientes = (IEnumerable<Cliente>)listaUsuariosClientes.Select(async u => await _repoClientes.ReadByIdAsync(Factory.SetGuid(u.Id))).ToList();
// 			IEnumerable<ReporteClienteViewModel> modelo = listaClientes.Select(c => new ReporteClienteViewModel()).ToList();
// 			return View(modelo);
// 		}

// 		private async Task CargarViewBags()
// 		{
// 			IList<ApplicationUser> usuariosCliente = await _userManager.GetUsersInRoleAsync("Cliente");
// 			IList<ApplicationUser> listaUsuarios = await _userManager.Users.ToListAsync();

// 			// Usuarios disponibles para registrar como cliente
// 			//listaUsuarios.RemoveAll(u => u.Id.Equals(usuariosCliente.Select(c => c.Id).ToArray()));

// 			ViewBag.ListaUsuarios = listaUsuarios.Select(p => new SelectListItem { Value = Convert.ToString(p.Id, new CultureInfo("es-CR")), Text = string.Format("{0} {1} {2}", p.Name, p.FirstLastName, p.SecondLastName) }).ToList();
// 			ViewBag.ListaProvincias = CargarListaSeleccionProvincias(_repoProvincias);
// 			ViewBag.ListaCantones = CargarListaSeleccionCantones(_repoCantones);
// 			ViewBag.ListaDistritos = CargarListaSeleccionDistritos(_repoDistritos);
// 		}
// 	}
// }
