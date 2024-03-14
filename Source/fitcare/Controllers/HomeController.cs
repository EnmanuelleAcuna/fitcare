using System;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace fitcare.Controllers
{
	public class HomeController : BaseController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ApplicationUserManager<ApplicationUser> userManager,
							  RoleManager<ApplicationRole> roleManager,
							  IDivisionTerritorialManager divisionterritorial,
							  IConfiguration configuration,
							  IHttpContextAccessor contextAccesor,
							  ILogger<HomeController> logger,
							  IWebHostEnvironment environment)
		: base(divisionterritorial, userManager, roleManager, configuration, contextAccesor, environment)
		{
			_userManager = userManager;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Index()
		{
			_logger.LogInformation($"Entering fitcare.");
			return View();
		}

		[HttpGet]
		public ActionResult Contacto()
		{
			_logger.LogInformation($"Entering contacto.");
			return View();
		}

		public IActionResult Privacidad()
		{
			return View();
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult> Administracion()
		{
			_logger.LogInformation($"Entering administracion.");

			string nombreUsuarioConectado = User.Identity.Name;
			ApplicationUser usuarioConectado = await _userManager.FindByNameAsync(nombreUsuarioConectado);
			ViewBag.NombreUsuario = string.Format("{0} {1} {2}", usuarioConectado.Name, usuarioConectado.FirstLastName, usuarioConectado.SecondLastName);
			ViewBag.UltimoIngreso = Convert.ToDateTime(usuarioConectado.LastSession).ToString("dd/MM/yyyy hh:mm:ss");
			return View();
		}
	}
}
