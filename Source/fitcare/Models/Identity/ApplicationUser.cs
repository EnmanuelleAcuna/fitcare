using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using fitcare.Models.Entities;
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

	public ApplicationUser(string id, Guid idProvincia, Guid idCanton, Guid idDistrito, DateTime fechaIngreso)
	{
		Id = id;
		IdProvincia = idProvincia;
		IdCanton = idCanton;
		IdDistrito = idDistrito;
		FechaIngreso = fechaIngreso;
	}

	public string IdentificationNumber { get; private set; }
	public string Name { get; private set; }
	public string FirstLastName { get; private set; }
	public string SecondLastName { get; private set; }

	public DateTime LastSession { get; private set; }
	public DateTime SetLastSession(DateTime value) => LastSession = value;

	public bool? Active { get; private set; }

	[ForeignKey(nameof(Provincia))]
	public Guid IdProvincia { get; set; }
	public Provincia Provincia { get; set; }

	[ForeignKey(nameof(Canton))]
	public Guid IdCanton { get; set; }
	public Provincia Canton { get; set; }

	[ForeignKey(nameof(Distrito))]
	public Guid IdDistrito { get; set; }
	public Provincia Distrito { get; set; }

	public string URLFotografia { get; set; }

	public DateTime? FechaIngreso { get; set; }

	[NotMapped]
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
		Name = name;
		FirstLastName = firstLastName;
		SecondLastName = secondLastName;
		IdentificationNumber = identification;
	}

	public override string ToString() => JsonSerializer.Serialize(this);
}
