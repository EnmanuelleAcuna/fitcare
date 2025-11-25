using System;
using fitcare.Models;
using fitcare.Models.Core;
using fitcare.Models.Entities;
using fitcare.Models.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace fitcare;

class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		//builder.Services.AddLogging(); // This is called automatically by WebApplication.CreateBuilder(args)

		builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
		builder.Services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
			// options.CheckConsentNeeded = _ => false; // By default is false
			options.MinimumSameSitePolicy = SameSiteMode.Strict;
		});

		builder.Services.ConfigureApplicationCookie(options =>
		{
			options.Cookie.Name = ".AspNetCore.Identity.Application";
			options.Cookie.HttpOnly = true;
			options.Cookie.SameSite = SameSiteMode.Strict;
			options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
			options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
			options.SlidingExpiration = true;
			options.LoginPath = "/Cuentas/IniciarSesion";
		});

		builder.Services.AddScoped<IBaseCore<Provincia>, Provincias>();
		builder.Services.AddScoped<IBaseCore<Canton>, Cantones>();
		builder.Services.AddScoped<IBaseCore<Distrito>, Distritos>();
		builder.Services.AddTransient<IDivisionTerritorial, DivisionTerritorial>();
		builder.Services.AddTransient<IBaseCore<TipoMaquina>, TiposMaquina>();
		builder.Services.AddTransient<IBaseCore<Maquina>, Maquinas>();
		builder.Services.AddTransient<IBaseCore<TipoEjercicio>, TiposEjercicio>();
		builder.Services.AddTransient<IBaseCore<Ejercicio>, Ejercicios>();
		builder.Services.AddTransient<IBaseCore<TipoMedida>, TiposMedida>();
		builder.Services.AddTransient<IBaseCore<GrupoMuscular>, GruposMusculares>();
		builder.Services.AddTransient<IRutinas<Rutina>, Rutinas>();
		builder.Services.AddScoped<IEmailSender, EmailSender>();

		builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

		WebApplication app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseExceptionHandler("/Error/Error");
		}

		app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseCookiePolicy();
		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllerRoute(name: "default", pattern: "{controller=Cuentas}/{action=IniciarSesion}/{id?}");
		app.Run();
	}
}
