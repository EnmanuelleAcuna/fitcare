using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fitcare.Models.Identity;

namespace fitcare.Models.Entities;

[Table("PROVINCIAS", Schema = "fitcare")]
public class Provincia : Base
{
	private Provincia() : base()
	{
		Cantones = new HashSet<Canton>();
		Usuarios = new HashSet<ApplicationUser>();
	}

	public Provincia(Guid id, string nombre, bool activo, string creadoPor, DateTime creadoEl)
	: base(creadoPor, creadoEl)
	{
		Id = id;
		Nombre = nombre;
		Estado = activo;
	}

	[Key]
	public Guid Id { get; set; }
	public string Nombre { get; set; }
	public bool Estado { get; set; }

	[InverseProperty("Cantones")]
	public virtual ICollection<Canton> Cantones { get; set; }

	[InverseProperty("Usuarios")]
	public virtual ICollection<ApplicationUser> Usuarios { get; set; }
}

[Table("CANTONES", Schema = "fitcare")]
public class Canton : Base
{
	private Canton() : base()
	{
		Distritos = new HashSet<Distrito>();
		Usuarios = new HashSet<ApplicationUser>();
	}

	public Canton(Guid id, string nombre, bool activo, int idINEC, Provincia provincia, string createdBy, DateTime dateCreated)
	{
		Id = id;
		Nombre = nombre;
		Estado = activo;
		IdCantonInec = idINEC;
		IdProvincia = provincia.Id;
		Provincia = provincia;
	}

	[Key]
	public Guid Id { get; set; }
	public string Nombre { get; set; }
	public bool Estado { get; set; }
	public int IdCantonInec { get; set; }

	[ForeignKey("Provincia")]
	public Guid IdProvincia { get; set; }
	public virtual Provincia Provincia { get; set; }

	[InverseProperty("Distritos")]
	public virtual ICollection<Distrito> Distritos { get; set; }

	[InverseProperty("Usuarios")]
	public virtual ICollection<ApplicationUser> Usuarios { get; set; }
}

[Table("DISTRITOS", Schema = "fitcare")]
public class Distrito : Base
{
	private Distrito() : base()
	{
		Usuarios = new HashSet<ApplicationUser>();
	}

	public Distrito(Guid id, string nombre, bool activo, int idINEC, Canton canton)
	{
		Id = id;
		Nombre = nombre;
		Estado = activo;
		IdDistritoInec = idINEC;
		IdCanton = canton.Id;
		Canton = canton;
	}

	[Key]
	public Guid Id { get; set; }
	public string Nombre { get; set; }
	public bool Estado { get; set; }
	public int IdDistritoInec { get; set; }

	[ForeignKey("Canton")]
	public Guid IdCanton { get; set; }
	public virtual Canton Canton { get; set; }

	[InverseProperty("Usuarios")]
	public virtual ICollection<ApplicationUser> Usuarios { get; set; }
}
