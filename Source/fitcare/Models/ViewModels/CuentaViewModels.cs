using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using fitcare.Models.Identity;
using Microsoft.AspNetCore.Http;

namespace fitcare.Models.ViewModels;

public class IniciarSesionViewModel
{
	[Required(ErrorMessage = "El correo electrónico es requerido.")]
	[Display(Name = "Correo electrónico")]
	[EmailAddress(ErrorMessage = "Debe digitar un correo válido.")]
	public string Correo { get; set; }

	[Required(ErrorMessage = "La contraseña es requerida.")]
	[DataType(DataType.Password)]
	[Display(Name = "Contraseña")]
	public string Contrasena { get; set; }
}

public class OlvidoContrasenaViewModel
{
	[Display(Name = "Correo electrónico")]
	[EmailAddress(ErrorMessage = "Debe digitar una dirección de correo electrónico válida.")]
	[Required(ErrorMessage = "El correo electrónico es requerido.")]
	public string CorreoElectronico { get; set; }
}

public class RestablecerContrasenaViewModel
{
	[Display(Name = "Correo electrónico")]
	[Required(ErrorMessage = "El correo electrónico es requerido")]
	[EmailAddress(ErrorMessage = "Debe ser un correo válido")]
	public string CorreoElectronico { get; set; }

	[Display(Name = "Contraseña")]
	[Required(ErrorMessage = "La contraseña es requerida.")]
	[StringLength(100, ErrorMessage = "La {0} debe contener al menos {2} caracteres.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	public string Contrasena { get; set; }

	[Display(Name = "Confirmar contraseña")]
	[DataType(DataType.Password)]
	[Compare("Contrasena", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
	public string ConfirmarContrasena { get; set; }

	public string Code { get; set; }
}

public class UsuarioViewModel
{
	public UsuarioViewModel() { }

	public UsuarioViewModel(ApplicationUser usuario)
	{
		IdUsuario = usuario.Id;
		Nombre = string.Format(new CultureInfo("es-CR"), "{0} {1} {2}", usuario.Name, usuario.FirstLastName, usuario.SecondLastName);
		Correo = usuario.Email;
		NumeroIdentificacion = usuario.IdentificationNumber;
		Estado = (bool)usuario.Active ? "Activo" : "Inactivo";
	}

	public string IdUsuario { get; set; }

	public string Nombre { get; set; }

	[Display(Name = "Correo electrónico")]
	public string Correo { get; set; }

	public string NumeroIdentificacion { get; set; }

	public string Estado { get; set; }
}

public class AgregarUsuarioViewModel
{
	[Required(ErrorMessage = "El nombre es requerido.")]
	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
	public string Nombre { get; set; }

	[Display(Name = "Primer apellido")]
	[Required(ErrorMessage = "El primer apellido es requerido.")]
	[StringLength(50, ErrorMessage = "El primer apellido no puede exceder los 50 caracteres.")]
	public string PrimerApellido { get; set; }

	[Display(Name = "Segundo apellido")]
	[Required(ErrorMessage = "El segundo apellido es requerido.")]
	[StringLength(50, ErrorMessage = "El segundo apellido no puede exceder los 50 caracteres.")]
	public string SegundoApellido { get; set; }

	[Display(Name = "Número de identificación")]
	[Required(ErrorMessage = "El número de identificación es requerido.")]
	[StringLength(50, ErrorMessage = "El número de identificacion no puede exceder los 50 caracteres.")]
	public string NumeroIdentificacion { get; set; }

	[Display(Name = "Correo electrónico")]
	[Required(ErrorMessage = "El correo electrónico es requerido.")]
	[StringLength(50, ErrorMessage = "El correo electrónico no puede exceder los 50 caracteres.")]
	public string CorreoElectronico { get; set; }

	[Display(Name = "Contraseña")]
	[Required(ErrorMessage = "La contraseña es requerida.")]
	[StringLength(100, ErrorMessage = "La {0} debe contener al menos {2} caracteres.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	public string Contrasena { get; set; }

	[Display(Name = "Confirmar contraseña")]
	[Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
	[DataType(DataType.Password)]
	public string ConfirmarContrasena { get; set; }

	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public ApplicationUser Entidad()
	{
		ApplicationUser usuario = new(Guid.NewGuid().ToString(), CorreoElectronico, CorreoElectronico, Nombre, PrimerApellido, SegundoApellido, NumeroIdentificacion, DateTime.Now, true);
		return usuario;
	}
}

public class EditarUsuarioViewModel
{
	public EditarUsuarioViewModel() { }

	public EditarUsuarioViewModel(ApplicationUser usuario, IList<ApplicationRole> roles)
	{
		IdUsuario = usuario.Id;
		Nombre = usuario.Name;
		PrimerApellido = usuario.FirstLastName;
		SegundoApellido = usuario.SecondLastName;
		NumeroIdentificacion = usuario.IdentificationNumber;
		CorreoElectronico = usuario.Email;
		Estado = (bool)usuario.Active;

		NombreCompleto = String.Format("{0} {1} {2}", this.Nombre, this.PrimerApellido, this.SegundoApellido);
		Roles = roles;
	}

	[Required(ErrorMessage = "El id es requerido.")]
	public string IdUsuario { get; set; }

	[Required(ErrorMessage = "El nombre es requerido.")]
	[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
	public string Nombre { get; set; }

	[Display(Name = "Primer apellido")]
	[Required(ErrorMessage = "El primer apellido es requerido.")]
	[StringLength(50, ErrorMessage = "El primer apellido no puede exceder los 50 caracteres")]
	public string PrimerApellido { get; set; }

	[Display(Name = "Segundo apellido")]
	[Required(ErrorMessage = "El segundo apellido es requerido.")]
	[StringLength(50, ErrorMessage = "El segundo apellido no puede exceder los 50 caracteres.")]
	public string SegundoApellido { get; set; }

	[Display(Name = "Número de identificación")]
	[Required(ErrorMessage = "El número de identificación es requerido.")]
	[StringLength(50, ErrorMessage = "El número de identificación no puede exceder los 50 caracteres.")]
	public string NumeroIdentificacion { get; set; }

	[Display(Name = "Correo electrónico")]
	[Required(ErrorMessage = "El correo electrónico es requerido.")]
	[StringLength(50, ErrorMessage = "El correo electrónico no puede exceder los 50 caracteres.")]
	public string CorreoElectronico { get; set; }

	[Display(Name = "Activo")]
	public bool Estado { get; set; }

	public string NombreCompleto { get; }

	public IList<ApplicationRole> Roles { get; set; }

	public IList<string> RolesSeleccionados { get; set; }

	public ApplicationUser Entidad()
	{
		ApplicationUser usuario = new(IdUsuario, CorreoElectronico, CorreoElectronico, Nombre, PrimerApellido, SegundoApellido, NumeroIdentificacion, DateTime.Now, true);
		return usuario;
	}
}

public class AgregarInstructorViewModel : UsuarioViewModel
{
	public AgregarInstructorViewModel() : base() { }

	public AgregarInstructorViewModel(ApplicationUser user) : base(user) { }

	[Display(Name = "Provincia")]
	[Required(ErrorMessage = "La Provincia es requerida.")]
	public string IdProvincia { get; set; }

	[Display(Name = "Cantón")]
	[Required(ErrorMessage = "El Cantón es requerido.")]
	public string IdCanton { get; set; }

	[Display(Name = "Distrito")]
	[Required(ErrorMessage = "El Distrito es requerido.")]
	public string IdDistrito { get; set; }

	[Display(Name = "Fotografía")]
	[Required(ErrorMessage = "La fotografía es requerida.")]
	public IFormFile ProfilePicture { get; set; }

	[Display(Name = "Fecha de ingreso")]
	[Required(ErrorMessage = "La fecha de ingreso es requerida")]
	[DataType(DataType.Date, ErrorMessage = "La fecha no tiene formato correcto.")]
	public DateTime FechaIngreso { get; set; } = DateTime.Now;

	public ApplicationUser Entidad() => new(IdUsuario, new Guid(IdProvincia), new Guid(IdCanton), new Guid(IdDistrito), FechaIngreso);
}

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

