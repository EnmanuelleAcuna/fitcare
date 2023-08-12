using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fitcare.Models.Contracts;
using fitcare.Models.DataAccess.EntityFramework;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace fitcare.Models.DataAccess;

public class DAOMaquina : IRepository<Maquina>
{
	private readonly FitcareDBContext _dbContext;
	private readonly IMapper _mapper;
	private readonly ILogger _logger;

	public DAOMaquina(FitcareDBContext dbContext, IMapper mapper, ILogger logger)
	{
		_dbContext = dbContext;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<IList<Maquina>> ReadAllAsync()
	{
		var listaMaquinasBD = await _dbContext.Maquinas
											  .Include(m => m.IdTipoMaquinaNavigation)
											  .ToListAsync();

		if (listaMaquinasBD == null || listaMaquinasBD.Count == 0)
			return new List<Maquina>();

		var listaMaquinas = _mapper.Map<List<Maquina>>(listaMaquinasBD);
		return listaMaquinas;
	}

	public async Task<Maquina> ReadByIdAsync(Guid id)
	{
		var maquinaBD = await _dbContext.Maquinas
										.Where(m => m.Id.Equals(id))
										.Include(m => m.IdTipoMaquinaNavigation)
										.FirstOrDefaultAsync();
		var maquina = _mapper.Map<Maquina>(maquinaBD);
		return maquina;
	}

	public async Task CreateAsync(Maquina maquina)
	{
		var maquinaBD = _mapper.Map<Maquinas>(maquina);
		await _dbContext.Maquinas.AddAsync(maquinaBD);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Maquina maquina)
	{
		var maquinaBD = await _dbContext.Maquinas
										.Where(m => m.Id.Equals(maquina.Id))
										.Include(m => m.IdTipoMaquinaNavigation)
										.FirstOrDefaultAsync();

		if (maquinaBD == null)
		{
			_logger.LogInformation("No se encontró una máquina con el id", maquina.Id);
			await Task.CompletedTask;
		}

		maquinaBD.Codigo = maquina.Codigo;
		maquinaBD.Nombre = maquina.Nombre;
		maquinaBD.NumeroActivo = maquina.CodigoActivo;
		maquinaBD.Estado = maquina.Estado;
		maquinaBD.IdTipoMaquina = maquina.TipoMaquina.Id;
		maquinaBD.FechaAdquisicion = maquina.FechaAdquisicion;
		maquinaBD.UpdatedBy = maquina.EditadoPor;
		maquinaBD.DateUpdated = maquina.EditadoEl;

		_dbContext.Maquinas.Update(maquinaBD);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		Maquinas maquinaBD = await _dbContext.Maquinas.FindAsync(id);

		if (maquinaBD == null)
		{
			_logger.LogInformation("No se encontró una máquina con el id", id);
			await Task.CompletedTask;
		}

		_dbContext.Maquinas.Remove(maquinaBD);
		await _dbContext.SaveChangesAsync();
	}
}

public class DAOTipoMaquina : IRepository<TipoMaquina>
{
	private readonly FitcareDBContext _dbContext;
	private readonly IMapper _mapper;
	private readonly ILogger _logger;

	public DAOTipoMaquina(FitcareDBContext dbContext, IMapper mapper, ILogger logger)
	{
		_dbContext = dbContext;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<IList<TipoMaquina>> ReadAllAsync()
	{
		var listaTiposMaquinaBD = await _dbContext.TiposMaquina.ToListAsync();

		if (listaTiposMaquinaBD == null || listaTiposMaquinaBD.Count == 0)
			return new List<TipoMaquina>();

		var listaTiposMaquina = _mapper.Map<List<TipoMaquina>>(listaTiposMaquinaBD);
		return listaTiposMaquina;
	}

	public async Task<TipoMaquina> ReadByIdAsync(Guid id)
	{
		var tipoMaquinaBD = await _dbContext.TiposMaquina.FindAsync(id);
		var tipoMaquina = _mapper.Map<TipoMaquina>(tipoMaquinaBD);
		return tipoMaquina;
	}

	public async Task CreateAsync(TipoMaquina tipoMaquina)
	{
		TiposMaquina tipoMaquinaBD = _mapper.Map<TiposMaquina>(tipoMaquina); ;
		_dbContext.TiposMaquina.Add(tipoMaquinaBD);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(TipoMaquina tipoMaquina)
	{
		TiposMaquina tipoMaquinaBD = await _dbContext.TiposMaquina.FindAsync(tipoMaquina.Id);

		if (tipoMaquinaBD == null)
		{
			_logger.LogInformation("No se encontró un tipo de máquina con el id", tipoMaquina.Id);
			await Task.CompletedTask;
		}

		tipoMaquinaBD.Nombre = tipoMaquina.Nombre;
		tipoMaquinaBD.Estado = tipoMaquina.Estado;
		tipoMaquinaBD.DateUpdated = tipoMaquina.EditadoEl;
		tipoMaquinaBD.UpdatedBy = tipoMaquina.EditadoPor;

		_dbContext.TiposMaquina.Update(tipoMaquinaBD);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		TiposMaquina tipoMaquinaBD = await _dbContext.TiposMaquina.FindAsync(id);

		if (tipoMaquinaBD == null)
		{
			_logger.LogInformation("No se encontró un tipo de máquina con el id", id);
			await Task.CompletedTask;
		}

		_dbContext.TiposMaquina.Remove(tipoMaquinaBD);
		await _dbContext.SaveChangesAsync();
	}
}
