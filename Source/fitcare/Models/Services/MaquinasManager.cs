using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.DataAccess.EntityFramework;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace fitcare.Models;

// public class DAOMaquina : IManager<Maquina>
// {
// 	private readonly FitcareDBContext _dbContext;
// 	private readonly ILogger _logger;

// 	public DAOMaquina(FitcareDBContext dbContext, ILogger logger)
// 	{
// 		_dbContext = dbContext;
// 		_logger = logger;
// 	}

// 	public async Task<IList<Maquina>> ReadAllAsync()
// 	{
// 		var listaMaquinasBD = await _dbContext.Maquinas
// 											  .Include(m => m.IdTipoMaquinaNavigation)
// 											  .ToListAsync();

// 		if (listaMaquinasBD == null || listaMaquinasBD.Count == 0)
// 			return new List<Maquina>();

// 		var listaMaquinas = _mapper.Map<List<Maquina>>(listaMaquinasBD);
// 		return listaMaquinas;
// 	}

// 	public async Task<Maquina> ReadByIdAsync(Guid id)
// 	{
// 		var maquinaBD = await _dbContext.Maquinas
// 										.Where(m => m.Id.Equals(id))
// 										.Include(m => m.IdTipoMaquinaNavigation)
// 										.FirstOrDefaultAsync();
// 		var maquina = _mapper.Map<Maquina>(maquinaBD);
// 		return maquina;
// 	}

// 	public async Task CreateAsync(Maquina maquina)
// 	{
// 		var maquinaBD = _mapper.Map<Maquinas>(maquina);
// 		await _dbContext.Maquinas.AddAsync(maquinaBD);
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task UpdateAsync(Maquina maquina)
// 	{
// 		var maquinaBD = await _dbContext.Maquinas
// 										.Where(m => m.Id.Equals(maquina.Id))
// 										.Include(m => m.IdTipoMaquinaNavigation)
// 										.FirstOrDefaultAsync();

// 		if (maquinaBD == null)
// 		{
// 			_logger.LogInformation("No se encontró una máquina con el id", maquina.Id);
// 			await Task.CompletedTask;
// 		}

// 		maquinaBD.Codigo = maquina.Codigo;
// 		maquinaBD.Nombre = maquina.Nombre;
// 		maquinaBD.NumeroActivo = maquina.CodigoActivo;
// 		maquinaBD.Estado = maquina.Estado;
// 		maquinaBD.IdTipoMaquina = maquina.TipoMaquina.Id;
// 		maquinaBD.FechaAdquisicion = maquina.FechaAdquisicion;
// 		maquinaBD.UpdatedBy = maquina.EditadoPor;
// 		maquinaBD.DateUpdated = maquina.EditadoEl;

// 		_dbContext.Maquinas.Update(maquinaBD);
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task DeleteAsync(Guid id)
// 	{
// 		Maquinas maquinaBD = await _dbContext.Maquinas.FindAsync(id);

// 		if (maquinaBD == null)
// 		{
// 			_logger.LogInformation("No se encontró una máquina con el id", id);
// 			await Task.CompletedTask;
// 		}

// 		_dbContext.Maquinas.Remove(maquinaBD);
// 		await _dbContext.SaveChangesAsync();
// 	}
// }

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
