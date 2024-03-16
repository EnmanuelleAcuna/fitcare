using System;
using fitcare.Models;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using fitcare.Models.Identity;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace fitcare;

class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
		IConfiguration builderConfiguration = builder.Configuration;

		builder.Services.AddApplicationInsightsTelemetry(options => new ApplicationInsightsServiceOptions { EnableAdaptiveSampling = false});
		builder.Services.AddLogging(b => b.AddConsole().AddApplicationInsights());

		builder.Services.Configure<ConnectionStringOptions>(builderConfiguration.GetSection("ConnectionStrings"));
		builder.Services.AddOptions<ConnectionStringOptions>();

		builder.Services.AddDbContext<FitcareDBContext>(options => options.UseSqlServer(builderConfiguration.GetConnectionString("DefaultConnection")));
		builder.Services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(builderConfiguration.GetConnectionString("DefaultConnection")));

		// ASP.Net Identity
		builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
		{
			options.User.RequireUniqueEmail = true;
			options.SignIn.RequireConfirmedAccount = false;
			options.Password.RequiredLength = 6;
		})
		.AddEntityFrameworkStores<IdentityDBContext>()
		.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider)
		.AddUserManager<ApplicationUserManager<ApplicationUser>>();

		builder.Services.Configure<CookiePolicyOptions>(options =>
		{
			options.CheckConsentNeeded = context => false;
			options.MinimumSameSitePolicy = SameSiteMode.Lax;
		});

		builder.Services.Configure<CookieOptions>(options =>
		{
			options.Expires = DateTime.Now.AddMinutes(20);
			options.SameSite = SameSiteMode.Strict;
			options.Secure = true;
		});

		builder.Services.ConfigureApplicationCookie(options =>
		{
			options.Cookie.Name = ".AspNetCore.Identity.Application";
			options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
			options.SlidingExpiration = true;
			options.LoginPath = "/Cuentas/IniciarSesion";
			options.Cookie.SameSite = SameSiteMode.Strict;
		});

		builder.Services.AddScoped<IManager<Provincia>, ProvinciaManager>();
		builder.Services.AddScoped<IManager<Canton>, CantonManager>();
		builder.Services.AddScoped<IManager<Distrito>, DistritoManager>();
		builder.Services.AddTransient<IDivisionTerritorialManager, DivisionTerritorialManager>();
		builder.Services.AddScoped<IContactoManager<Contacto>, ContactosManager>();
		builder.Services.AddTransient<IManager<TipoMaquina>, TiposMaquinaManager>();
		builder.Services.AddTransient<IManager<Maquina>, MaquinasManager>();
		builder.Services.AddTransient<IManager<TipoEjercicio>, TiposEjercicioManager>();
		builder.Services.AddTransient<IManager<Ejercicio>, EjerciciosManager>();
		builder.Services.AddTransient<IManager<TipoMedida>, TiposMedidaManager>();
		builder.Services.AddTransient<IManager<GrupoMuscular>, GruposMuscularesManager>();
		builder.Services.AddTransient<IRutinasManager<Rutina>, RutinasManager>();
		// builder.Services.AddTransient<IRepository<Cliente>, DAOCliente>();
		// builder.Services.AddTransient<IRepository<Instructor>, DAOInstructor>();
		// builder.Services.AddTransient<IRepository<Accesorio>, DAOAccesorio>();
		builder.Services.AddScoped<IEmailSender, EmailSender>();

		builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

		WebApplication app = builder.Build();

		app.UseExceptionHandler(app.Environment.IsDevelopment() ? "/Error/ErrorDevelopment" : "/Error/Error");
		app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseCookiePolicy();
		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
		app.Run();
	}
}

class ConnectionStringOptions
{
	public string DefaultConnection { get; set; }
}
