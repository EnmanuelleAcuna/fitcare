using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace fitcare.Models.Identity;

/// <summary>
/// ApplicationUser class to be used with ASP.Net Identity
/// </summary>
public class ApplicationUser : IdentityUser
{
	public ApplicationUser() : base() { }

	public ApplicationUser(string userName) : base(userName) { }

	public ApplicationUser(string id, string correo, string nombreUsuario, string nombre, string primerApellido, string segundoApellido, string identificacion, DateTime ultimaSesion, bool activo)
	{
		Id = id;
		Email = correo;
		UserName = nombreUsuario;
		Name = nombre;
		FirstLastName = primerApellido;
		SecondLastName = segundoApellido;
		IdentificationNumber = identificacion;
		LastSession = ultimaSesion;
		Active = activo;
	}

	public string IdentificationNumber { get; private set; }
	public string Name { get; private set; }
	public string FirstLastName { get; private set; }
	public string SecondLastName { get; private set; }

	public DateTime LastSession { get; private set; }
	public DateTime SetLastSession(DateTime value) => LastSession = value;

	public bool? Active { get; private set; }

	public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

	#region Identity properties that does not need to be mapped in the DB
	[NotMapped]
	public override bool EmailConfirmed { get; set; }

	[NotMapped]
	public override string PhoneNumber { get; set; }

	[NotMapped]
	public override bool PhoneNumberConfirmed { get; set; }

	[NotMapped]
	public override bool TwoFactorEnabled { get; set; }

	[NotMapped]
	public override DateTimeOffset? LockoutEnd { get; set; }

	[NotMapped]
	public override bool LockoutEnabled { get; set; }

	[NotMapped]
	public override int AccessFailedCount { get; set; }

	[NotMapped]
	public override string ConcurrencyStamp { get; set; }

	[NotMapped]
	public override string NormalizedEmail { get; set; }

	[NotMapped]
	public override string NormalizedUserName { get; set; }
	#endregion

	public void SetNewPersonalInformation(string name, string firstLastName, string secondLastName, string identification)
	{
		this.Name = name;
		this.FirstLastName = firstLastName;
		this.SecondLastName = secondLastName;
		this.IdentificationNumber = identification;
	}

	public override string ToString() => JsonSerializer.Serialize(this);
}
