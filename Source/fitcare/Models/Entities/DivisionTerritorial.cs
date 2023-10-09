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

	public Provincia(Guid id, string nombre, bool activo)
	{
		Id = id;
		Nombre = nombre;
		Estado = activo;
	}

	public Provincia(Guid id)
	{
		Id = id;
	}

	[Key]
	public Guid Id { get; private set; }
	public string Nombre { get; private set; }
	public bool Estado { get; private set; }

	public virtual ICollection<Canton> Cantones { get; set; }

	// [InverseProperty("Usuarios")]
	public virtual ICollection<ApplicationUser> Usuarios { get; set; }

	public void UpdateFrom(Provincia provincia)
	{
		Nombre = provincia.Nombre;
		Estado = provincia.Estado;
	}
}

[Table("CANTONES", Schema = "fitcare")]
public class Canton : Base
{
	private Canton() : base()
	{
		Distritos = new HashSet<Distrito>();
		Usuarios = new HashSet<ApplicationUser>();
	}

	public Canton(Guid id, string nombre, bool activo, int idINEC, Provincia provincia)
	{
		Id = id;
		Nombre = nombre;
		Estado = activo;
		IdCantonInec = idINEC;

		IdProvincia = provincia.Id;
		Provincia = provincia;
	}

	public Canton(Guid id, string nombre, bool activo, int idINEC, Guid idProvincia)
	{
		Id = id;
		Nombre = nombre;
		Estado = activo;
		IdCantonInec = idINEC;

		IdProvincia = idProvincia;
		Provincia = new Provincia(idProvincia);
	}

	public Canton(Guid id)
	{
		Id = id;
	}

	[Key]
	public Guid Id { get; private set; }
	public string Nombre { get; private set; }
	public bool Estado { get; private set; }
	[Column("Id_Canton_INEC")]
	public int IdCantonInec { get; private set; }

	[ForeignKey(nameof(Provincia))]
	[Column("Id_Provincia")]
	public Guid IdProvincia { get; private set; }
	public virtual Provincia Provincia { get; set; }

	public virtual ICollection<Distrito> Distritos { get; set; }

	// [InverseProperty("Usuarios")]
	public virtual ICollection<ApplicationUser> Usuarios { get; set; }

	public void UpdateFrom(Canton canton)
	{
		Nombre = canton.Nombre;
		Estado = canton.Estado;
		IdCantonInec = canton.IdCantonInec;
		IdProvincia = canton.IdProvincia;
	}
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
	public Guid Id { get; private set; }
	public string Nombre { get; private set; }
	public bool Estado { get; private set; }
	public int IdDistritoInec { get; private set; }

	[ForeignKey(nameof(Canton))]
	public Guid IdCanton { get; private set; }
	public virtual Canton Canton { get; set; }

	// [InverseProperty("Usuarios")]
	public virtual ICollection<ApplicationUser> Usuarios { get; set; }

	public void UpdateFrom(Distrito distrito)
	{
		Nombre = distrito.Nombre;
		Estado = distrito.Estado;
		IdDistritoInec = distrito.IdDistritoInec;
		IdCanton = distrito.IdCanton;
	}
}
