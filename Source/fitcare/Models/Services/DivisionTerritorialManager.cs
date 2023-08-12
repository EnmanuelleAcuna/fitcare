using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models;

public class DivisionTerritorialManager : IDivisionTerritorialManager
{
	public IManager<Provincia> Provincias { get; private set; }
	public IManager<Canton> Cantones { get; private set; }
	public IManager<Distrito> Distritos { get; private set; }

	public DivisionTerritorialManager(IManager<Provincia> provincias, IManager<Canton> cantones, IManager<Distrito> distritos)
	{
		Provincias = provincias;
		Cantones = cantones;
		Distritos = distritos;
	}
}

public class ProvinciaManager : IManager<Provincia>
{
	private readonly FitcareDBContext _db;

	public ProvinciaManager(FitcareDBContext db) => _db = db;

	public async Task<IList<Provincia>> ReadAllAsync()
	{
		IList<Provincia> provincias = await _db.Provincias.ToListAsync();
		return provincias ?? new List<Provincia>();
	}

	public async Task<Provincia> ReadByIdAsync(Guid id)
	{
		Provincia provincia = await _db.Provincias.FindAsync(id);

		if (provincia == null)
			throw new KeyNotFoundException($"No se encontró una Provincia con el id {id}");

		return provincia;
	}

	public async Task CreateAsync(Provincia provincia)
	{
		await _db.Provincias.AddAsync(provincia);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateAsync(Provincia provincia)
	{
		Provincia record = await ReadByIdAsync(provincia.Id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró una Provincia con el id {provincia.Id}");

		record.Nombre = provincia.Nombre;
		record.Estado = provincia.Estado;
		record.EditadoEl = provincia.EditadoEl;
		record.EditadoPor = provincia.EditadoPor;

		_db.Provincias.Update(record);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		Provincia record = await ReadByIdAsync(id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró una Provincia con el id {id}");

		_db.Provincias.Remove(record);
		await _db.SaveChangesAsync();
	}
}

public class CantonManager : IManager<Canton>
{
	private readonly FitcareDBContext _db;

	public CantonManager(FitcareDBContext db) => _db = db;

	public async Task<IList<Canton>> ReadAllAsync()
	{
		IList<Canton> records = await _db.Cantones.Include(c => c.Provincia).ToListAsync();
		return records ?? new List<Canton>();
	}

	public async Task<Canton> ReadByIdAsync(Guid id)
	{
		Canton canton = await _db.Cantones.Include(c => c.Provincia).FirstAsync(c => c.Id == id);

		if (canton == null)
			throw new KeyNotFoundException($"No se encontró un Canton con el id {id}");

		return canton;
	}

	public async Task CreateAsync(Canton canton)
	{
		await _db.Cantones.AddAsync(canton);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateAsync(Canton canton)
	{
		Canton record = await ReadByIdAsync(canton.Id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró un canton con el id {canton.Id}");

		record.Nombre = canton.Nombre;
		record.Estado = canton.Estado;
		record.IdCantonInec = canton.IdCantonInec;
		record.IdProvincia = canton.IdProvincia;
		record.EditadoEl = canton.EditadoEl;
		record.EditadoPor = canton.EditadoPor;

		_db.Cantones.Update(record);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		Canton record = await ReadByIdAsync(id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró un Canton con el id {id}");

		_db.Cantones.Remove(record);
		await _db.SaveChangesAsync();
	}
}

public class DistritoManager : IManager<Distrito>
{
	private readonly FitcareDBContext _db;

	public DistritoManager(FitcareDBContext db) => _db = db;

	public async Task<IList<Distrito>> ReadAllAsync()
	{
		IList<Distrito> records = await _db.Distritos.Include(d => d.Canton).ThenInclude(c => c.Provincia).ToListAsync();
		return records ?? new List<Distrito>();
	}

	public async Task<Distrito> ReadByIdAsync(Guid id)
	{
		Distrito distrito = await _db.Distritos.Include(d => d.Canton).ThenInclude(c => c.Provincia).FirstAsync(c => c.Id == id);

		if (distrito == null)
			throw new KeyNotFoundException($"No se encontró un Distrito con el id {id}");

		return distrito;
	}

	public async Task CreateAsync(Distrito distrito)
	{
		await _db.Distritos.AddAsync(distrito);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateAsync(Distrito distrito)
	{
		Distrito record = await ReadByIdAsync(distrito.Id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró un canton con el id {distrito.Id}");

		record.Nombre = distrito.Nombre;
		record.Estado = distrito.Estado;
		record.IdDistritoInec = distrito.IdDistritoInec;
		record.IdCanton = distrito.IdCanton;
		record.EditadoEl = distrito.EditadoEl;
		record.EditadoPor = distrito.EditadoPor;

		_db.Distritos.Update(record);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		Distrito record = await ReadByIdAsync(id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró un Distrito con el id {id}");

		_db.Distritos.Remove(record);
		await _db.SaveChangesAsync();
	}
}
