// using System;
// using System.ComponentModel.DataAnnotations;
// using System.Threading.Tasks;
// using fitcare.Models.Contracts;
// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using fitcare.Models.Identity;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;

// namespace fitcare.Models.ViewModels;

// public class NuevoInstructorViewModel
// {
// 	[Display(Name = "Usuario")]
// 	[Required(ErrorMessage = "El usuario es requerido")]
// 	public string IdUsuario { get; set; }

// 	[Display(Name = "Distrito")]
// 	[Required(ErrorMessage = "El distrito es requerido")]
// 	public string IdDistrito { get; set; }

// 	[Display(Name = "Fecha de ingreso")]
// 	[Required(ErrorMessage = "La fecha de ingreso es requerida")]
// 	[DataType(DataType.Date)]
// 	public string FechaIngreso { get; set; }

// 	[Display(Name = "Fotografía")]
// 	[Required(ErrorMessage = "La fotografía del instructor es requerida")]
// 	public IFormFile Fotografia { get; set; }

// 	public async Task<Instructor> Entidad(IDataCRUDBase<Distrito> repoDistritos)
// 	{
// 		Distrito distrito = await repoDistritos.ReadByIdAsync(Factory.SetGuid(IdDistrito));
// 		Instructor instructor = new(Factory.SetGuid(IdUsuario), Convert.ToDateTime(FechaIngreso), distrito);
// 		return instructor;
// 	}
// }

// public class ModificarInstructorViewModel : InicioClientesInstructoresViewModel
// {
// 	public ModificarInstructorViewModel() { }

// 	public ModificarInstructorViewModel(ApplicationUser usuario, Instructor instructor) : base(usuario)
// 	{
// 		IdDistrito = instructor.Distrito.Id.ToString();
// 		FechaIngreso = instructor.FechaIngreso;
// 		URLImagen = instructor.Foto?.URL; // TODO: hace falta conseguir la foto en el controlador antes de llamar este metodo
// 	}

// 	[Display(Name = "Distrito")]
// 	[Required(ErrorMessage = "El distrito es requerido")]
// 	public string IdDistrito { get; set; }

// 	[Display(Name = "Fecha de ingreso")]
// 	[Required(ErrorMessage = "La fecha de ingreso es requerida")]
// 	[DataType(DataType.Date)]
// 	public DateTime FechaIngreso { get; set; }

// 	[Display(Name = "Fotografía actual")]
// 	public Uri URLImagen { get; set; } // Foto actual

// 	[Display(Name = "Nueva fotografía")]
// 	[Required(ErrorMessage = "La fotografía del instructor es requerida")]
// 	public IFormFile Imagen { get; set; } // Nueva foto

// 	public async Task<Instructor> Entidad(IDataCRUDBase<Distrito> repoDistritos)
// 	{
// 		Distrito distrito = await repoDistritos.ReadByIdAsync(Factory.SetGuid(IdDistrito));
// 		Instructor instructor = new(Factory.SetGuid(IdUsuario), Convert.ToDateTime(FechaIngreso), distrito);
// 		return instructor;
// 	}
// }

// public class ReporteInstructorViewModel
// {
// 	public ReporteInstructorViewModel()
// 	{
// 		//NumeroIdentificacion = instructor.NumeroIdentificacion;
// 		//Nombre = string.Format(new CultureInfo("es-CR"), "{0} {1} {2}", instructor.Nombre, instructor.PrimerApellido, instructor.SegundoApellido);
// 		//Correo = instructor.Email;
// 		//Domicilio = string.Format(new CultureInfo("es-CR"), "{0}, cantón {1}, distrito {2}", instructor.Distrito.Canton.Provincia.Nombre, instructor.Distrito.Canton.Nombre, instructor.Distrito.Nombre);
// 		//FechaIngreso = instructor.FechaIngreso.ToString("dd/MM/yyyy");
// 		//Estado = instructor.Activo ? "Activo" : "Inactivo";
// 	}

// 	[Display(Name = "Número de identificación")]
// 	public string NumeroIdentificacion { get; set; }

// 	[Display(Name = "Nombre")]
// 	public string Nombre { get; set; }

// 	[Display(Name = "Correo electrónico")]
// 	public string Correo { get; set; }

// 	[Display(Name = "Domicilio")]
// 	public string Domicilio { get; set; }

// 	[Display(Name = "Fecha de ingreso")]
// 	public string FechaIngreso { get; set; }

// 	[Display(Name = "Estado")]
// 	public string Estado { get; set; }
// }
