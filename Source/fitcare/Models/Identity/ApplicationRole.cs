using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace fitcare.Models.Identity;

/// <summary>
/// Clase Rol para ser utilizado con Identity
/// </summary>
public partial class ApplicationRole : IdentityRole
{
	// Constructores
	public ApplicationRole() : base() { }

	public ApplicationRole(string roleName) : base(roleName) { }

	public ApplicationRole(string id, string nombre, string descripcion, bool estado)
	{
		Id = id;
		Name = nombre;
		Description = descripcion;
		Estado = estado;
	}

	// Propiedades
	public string Description { get; set; }
	
	public bool Estado { get; set; }

	[NotMapped]
	public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

	[NotMapped]
	public override string ConcurrencyStamp { get; set; }

	[NotMapped]
	public override string NormalizedName { get; set; }

	// Metodos
	// Sobreescribir metodo ToString() de la clase para devolver el objeto en una cadena string en formato JSON
	public override string ToString() => JsonSerializer.Serialize(this);

	public void ActualizarDatos(string nombre, string descripcion, bool estado)
	{
		Name = nombre;
		Description = descripcion;
		Estado = estado;
	}
}
