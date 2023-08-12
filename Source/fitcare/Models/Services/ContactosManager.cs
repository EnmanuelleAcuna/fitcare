using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models.DataAccess;

public class ContactosManager : IManager<Contacto>
{
	private readonly FitcareDBContext _db;

	public ContactosManager(FitcareDBContext db) => _db = db;

	public async Task<IList<Contacto>> ReadAllAsync()
	{
		IList<Contacto> contactos = await _db.Contactos.ToListAsync();
		return contactos ?? new List<Contacto>();
	}

	public async Task<Contacto> ReadByIdAsync(Guid id)
	{
		Contacto contacto = await _db.Contactos.FindAsync(id);

		if (contacto == null)
			throw new KeyNotFoundException($"No se encontró un Contacto con el id {id}");

		return contacto;
	}

	public async Task CreateAsync(Contacto contacto)
	{
		await _db.Contactos.AddAsync(contacto);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateAsync(Contacto contacto)
	{
		Contacto record = await _db.Contactos.FindAsync(contacto.Id);

		if (contacto == null)
			throw new KeyNotFoundException($"No se encontró un Contacto con el id {contacto.Id}");

		record.NombreCompleto = contacto.NombreCompleto;
		record.CorreoElectronico = contacto.CorreoElectronico;
		record.Telefono = contacto.Telefono;
		record.Mensaje = contacto.Mensaje;

		_db.Contactos.Update(record);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		Contacto record = await ReadByIdAsync(id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró un Contacto con el id {id}");

		_db.Contactos.Remove(record);
		await _db.SaveChangesAsync();
	}
}
