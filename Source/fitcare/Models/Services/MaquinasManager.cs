using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models;

public class MaquinasManager : IManager<Maquina>
{
	private readonly FitcareDBContext _dbContext;
	private readonly IManager<TipoMaquina> _tiposMaquinaManager;

	public MaquinasManager(FitcareDBContext dbContext, IManager<TipoMaquina> tipoMaquinaManager)
	{
		_dbContext = dbContext;
		_tiposMaquinaManager = tipoMaquinaManager;
	}

	public async Task<IList<Maquina>> ReadAllAsync()
	{
		var maquinas = await _dbContext.Maquinas.Include(m => m.TipoMaquina).ToListAsync();
		return maquinas ?? new List<Maquina>();
	}

	public async Task<Maquina> ReadByIdAsync(Guid id)
	{
		var maquina = await _dbContext.Maquinas.Include(m => m.TipoMaquina).FirstAsync(m => m.Id == id);
		return maquina ?? throw new KeyNotFoundException($"No se encontró un Cantón con el id {id}");
	}

	public async Task CreateAsync(Maquina maquina, string user)
	{
		var existingTipoMaquina = await _tiposMaquinaManager.ReadByIdAsync(maquina.IdTipoMaquina);

		if (existingTipoMaquina == null)
			throw new Exception($"El tipo de máquina {maquina.IdTipoMaquina} para la máquina no se ha encontrado en la BD.");
		else
			maquina.TipoMaquina = existingTipoMaquina;

		maquina.CreatedBy = user;
		maquina.DateCreated = DateTime.Now;

		await _dbContext.AddAsync(maquina);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Maquina maquina, string user)
	{
		var record = await ReadByIdAsync(maquina.Id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró una máquina con el id {maquina.Id}");

		record.Codigo = maquina.Codigo;
		record.Nombre = maquina.Nombre;
		record.CodigoActivo = maquina.CodigoActivo;
		record.Estado = maquina.Estado;
		record.IdTipoMaquina = maquina.IdTipoMaquina;
		record.FechaAdquisicion = maquina.FechaAdquisicion;

		record.UpdatedBy = user;
		record.DateUpdated = DateTime.Now;

		_dbContext.Update(record);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var record = await ReadByIdAsync(id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró una máquina con el id {id}");

		_dbContext.Remove(record);
		await _dbContext.SaveChangesAsync();
	}
}

public class TiposMaquinaManager : IManager<TipoMaquina>
{
	private readonly FitcareDBContext _dbContext;

	public TiposMaquinaManager(FitcareDBContext dbContext) => _dbContext = dbContext;

	public async Task<IList<TipoMaquina>> ReadAllAsync()
	{
		var tiposMaquina = await _dbContext.TiposMaquina.ToListAsync();
		return tiposMaquina ?? new List<TipoMaquina>();
	}

	public async Task<TipoMaquina> ReadByIdAsync(Guid id)
	{
		TipoMaquina tipoMaquina = await _dbContext.TiposMaquina.FindAsync(id);

		if (tipoMaquina == null)
			throw new KeyNotFoundException($"No se encontró el tipo de máquina con el id {id}");

		return tipoMaquina;
	}

	public async Task CreateAsync(TipoMaquina tipoMaquina, string user)
	{
		tipoMaquina.DateCreated = DateTime.Now;
		tipoMaquina.CreatedBy = user;

		await _dbContext.AddAsync(tipoMaquina);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(TipoMaquina tipoMaquina, string user)
	{
		TipoMaquina record = await ReadByIdAsync(tipoMaquina.Id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró el tipo de máquina con el id {tipoMaquina.Id}");

		record.Nombre = tipoMaquina.Nombre;
		record.Codigo = tipoMaquina.Codigo;
		record.Estado = tipoMaquina.Estado;
		record.DateUpdated = DateTime.Now;
		record.UpdatedBy = user;

		_dbContext.Update(record);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		TipoMaquina record = await ReadByIdAsync(id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró el tipo de máquina con el id {id}");

		_dbContext.Remove(record);
		await _dbContext.SaveChangesAsync();
	}
}
