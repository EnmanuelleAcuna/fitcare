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

public class DAOCanton : IRepository<Entities.Canton>
{
	private readonly FitcareDBContext _dbContext;
	private readonly IMapper _mapper;
	private readonly ILogger<DAOCanton> _logger;

	public DAOCanton(FitcareDBContext dbContext, IMapper mapper, ILogger<DAOCanton> logger)
	{
		_dbContext = dbContext;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<IList<Entities.Canton>> ReadAllAsync()
	{
		IList<Canton> listaCantonesBD = await _dbContext.Cantones
														  .Include(c => c.IdProvinciaNavigation)
														  .ToListAsync();

		if (listaCantonesBD == null || listaCantonesBD.Count == 0)
			return new List<Entities.Canton>();

		var listaCantones = _mapper.Map<List<Entities.Canton>>(listaCantonesBD);
		return listaCantones;
	}

	public async Task<Entities.Canton> ReadByIdAsync(Guid id)
	{
		Canton cantonBD = await _dbContext.Cantones.Where(c => c.Id.Equals(id))
													 .Include(c => c.IdProvinciaNavigation)
													 .FirstOrDefaultAsync();
		var canton = _mapper.Map<Entities.Canton>((object)cantonBD);
		return canton;
	}

	public async Task CreateAsync(Entities.Canton canton)
	{
		EntityFramework.Canton cantonBD = _mapper.Map<EntityFramework.Canton>(canton);
		_dbContext.Cantones.Add(cantonBD);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Entities.Canton canton)
	{
		EntityFramework.Canton cantonBD = await _dbContext.Cantones.FindAsync(canton.Id);

		if (cantonBD == null)
		{
			_logger.LogInformation("No se encontró un cantón con el id", canton.Id);
			await Task.CompletedTask;
		}

		cantonBD.Nombre = canton.Nombre;
		cantonBD.Estado = canton.Estado;
		cantonBD.IdProvincia = canton.Provincia.Id;
		cantonBD.IdCantonInec = canton.IdINEC;
		cantonBD.DateUpdated = canton.EditadoEl;
		cantonBD.UpdatedBy = canton.EditadoPor;

		_dbContext.Cantones.Update(cantonBD);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		EntityFramework.Canton cantonBD = await _dbContext.Cantones.FindAsync(id);

		if (cantonBD == null)
		{
			_logger.LogInformation("No se encontró un cantón con el id", id);
			await Task.CompletedTask;
		}

		_dbContext.Cantones.Remove(cantonBD);
		await _dbContext.SaveChangesAsync();
	}
}
