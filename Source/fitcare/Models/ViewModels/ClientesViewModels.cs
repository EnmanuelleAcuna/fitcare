// using fitcare.Models.Contracts;
// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using fitcare.Models.Identity;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using System;
// using System.ComponentModel.DataAnnotations;
// using System.Threading.Tasks;

// namespace fitcare.Models.ViewModels;

// public class NuevoClienteViewModel
// {
// 	[Display(Name = "Usuario")]
// 	[Required(ErrorMessage = "El usuario es requerido")]
// 	public string IdUsuario { get; set; }

// 	[Display(Name = "Distrito")]
// 	[Required(ErrorMessage = "El distrito es requerido")]
// 	public string IdDistrito { get; set; }

// 	[Display(Name = "Fecha de inscripción")]
// 	[Required(ErrorMessage = "La fecha de inscripción es requerida")]
// 	[DataType(DataType.Date)]
// 	public string FechaInscripcion { get; set; }

// 	[Display(Name = "Fotografía")]
// 	[Required(ErrorMessage = "La fotografía del instructor es requerida")]
// 	public IFormFile Fotografia { get; set; }

// 	// Funciones / métodos
// 	public async Task<Cliente> Entidad(IDataCRUDBase<Distrito> repoDistritos, UserManager<ApplicationUser> userManager)
// 	{
// 		Distrito distrito = await repoDistritos.ReadByIdAsync(Factory.SetGuid(IdDistrito));
// 		_ = await userManager.FindByIdAsync(IdUsuario);
// 		// Imagen imagen = new(Builder.SetGuid(IdUsuario), new Uri(string.Empty));
// 		Cliente cliente = new(IdUsuario, Convert.ToDateTime(FechaInscripcion), DateTime.Now, distrito);
// 		cliente.Inscribir();
// 		return cliente;
// 	}
// }

// public class ReporteClienteViewModel
// {
// 	public ReporteClienteViewModel()
// 	{
// 		//NumeroIdentificacion = cliente.NumeroIdentificacion;
// 		//Nombre = string.Format(new CultureInfo("es-CR"), "{0} {1} {2}", cliente.Nombre, cliente.PrimerApellido, cliente.SegundoApellido);
// 		//Correo = cliente.Email;
// 		//Domicilio = string.Format(new CultureInfo("es-CR"), "{0}, cantón {1}, distrito {2}", cliente.Distrito.Canton.Provincia.Nombre, cliente.Distrito.Canton.Nombre, cliente.Distrito.Nombre);
// 		//FechaInscripcion = cliente.FechaInscripcion.ToString("dd/MM/yyyy");
// 		//FechaRenovacion = cliente.FechaRenovacion.ToString("dd/MM/yyyy");
// 		//Estado = cliente.Activo ? "Activo" : "Inactivo";
// 	}

// 	[Display(Name = "Número de identificación")]
// 	public string NumeroIdentificacion { get; set; }

// 	[Display(Name = "Nombre")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Correo electrónico")]
// 	public string Correo { get; set; }

// 	[Display(Name = "Domicilio")]
// 	public string Domicilio { get; set; }

// 	[Display(Name = "Fecha de inscripción")]
// 	public string FechaInscripcion { get; set; }

// 	[Display(Name = "Fecha de renovación")]
// 	public string FechaRenovacion { get; set; }

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }
