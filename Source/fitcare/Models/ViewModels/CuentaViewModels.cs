using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using fitcare.Models.Identity;

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
	public UsuarioViewModel(ApplicationUser usuario)
	{
		IdUsuario = usuario.Id;
		Nombre = string.Format(new CultureInfo("es-CR"), "{0} {1} {2}", usuario.Name, usuario.FirstLastName, usuario.SecondLastName);
		Correo = usuario.Email;
		Estado = (bool)usuario.Active ? "Activo" : "Inactivo";
	}

	public string IdUsuario { get; set; }

	public string Nombre { get; set; }

	[Display(Name = "Correo electrónico")]
	public string Correo { get; set; }

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

public class ClienteInstructorViewModel
{
	public ClienteInstructorViewModel() { }

	public ClienteInstructorViewModel(ApplicationUser usuario)
	{
		IdUsuario = usuario.Id;
		Nombre = String.Format(new CultureInfo("es-CR"), "{0} {1} {2}", usuario.Name, usuario.FirstLastName, usuario.SecondLastName);
		Correo = usuario.Email;
		Estado = (bool)usuario.Active ? "Activo" : "Inactivo";
	}

	public string IdUsuario { get; set; }

	public string Nombre { get; set; }

	[Display(Name = "Correo electrónico")]
	public string Correo { get; set; }

	public string Estado { get; set; }
}
