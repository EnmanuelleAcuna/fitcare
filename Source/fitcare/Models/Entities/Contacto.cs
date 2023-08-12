using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fitcare.Models.Extras;

namespace fitcare.Models.Entities;

[Table("CONTACTOS", Schema = "fitcare")]
public class Contacto : Base
{
	private Contacto() { }

	public Contacto(Guid id, string nombre, string correo, string telefono, string mensaje)
	{
		Id = id;
		NombreCompleto = nombre;
		CorreoElectronico = correo;
		Telefono = telefono;
		Mensaje = mensaje;
	}

	[Key]
	public Guid Id { get; set; } = Factory.NewGUID(Guid.NewGuid());
	public string NombreCompleto { get; set; }
	public string CorreoElectronico { get; set; }
	public string Telefono { get; set; }
	public string Mensaje { get; set; }
	public DateTime FechaRegistro { get; set; } = DateTime.Now;
}
