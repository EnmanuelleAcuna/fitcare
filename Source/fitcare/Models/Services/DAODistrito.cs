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

public class DAODistrito : IRepository<Entities.Distrito>
{
	private readonly FitcareDBContext _dbContext;
	private readonly IMapper _mapper;
	private readonly ILogger<DAODistrito> _logger;

	public DAODistrito(FitcareDBContext dbContext, IMapper mapper, ILogger<DAODistrito> logger)
	{
		_dbContext = dbContext;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<IList<Entities.Distrito>> ReadAllAsync()
	{
		IList<Distrito> listaDistritosBD = await _dbContext.Distritos
															.Include(d => d.IdCantonNavigation)
															.Include(d => d.IdCantonNavigation.IdProvinciaNavigation)
															.ToListAsync();

		if (listaDistritosBD == null || listaDistritosBD.Count == 0)
			return new List<Entities.Distrito>();

		var listaDistritos = _mapper.Map<List<Entities.Distrito>>(listaDistritosBD);
		return listaDistritos;
	}

	public async Task<Entities.Distrito> ReadByIdAsync(Guid id)
	{
		Distrito distritoBD = await _dbContext.Distritos.Where(d => d.Id.Equals(id))
														 .Include(d => d.IdCantonNavigation)
														 .Include(d => d.IdCantonNavigation.IdProvinciaNavigation)
														 .FirstOrDefaultAsync();
		var distrito = _mapper.Map<Entities.Distrito>((object)distritoBD);
		return distrito;
	}

	public async Task CreateAsync(Entities.Distrito distrito)
	{
		EntityFramework.Distrito distritoBD = _mapper.Map<EntityFramework.Distrito>(distrito);
		_dbContext.Distritos.Add(distritoBD);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Entities.Distrito distrito)
	{
		EntityFramework.Distrito distritoBD = await _dbContext.Distritos.FindAsync(distrito.Id);

		if (distritoBD == null)
		{
			_logger.LogInformation("No se encontró un distrito con el id", distrito.Id);
			await Task.CompletedTask;
		}

		distritoBD.Nombre = distrito.Nombre;
		distritoBD.Estado = distrito.Estado;
		distritoBD.IdCanton = distrito.Canton.Id;
		distritoBD.IdDistritoInec = distrito.IdINEC;
		distritoBD.DateUpdated = distrito.EditadoEl;
		distritoBD.UpdatedBy = distrito.EditadoPor;

		_dbContext.Distritos.Update(distritoBD);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		EntityFramework.Distrito distritoBD = await _dbContext.Distritos.FindAsync(id);

		if (distritoBD == null)
		{
			_logger.LogInformation("No se encontró un distrito con el id", id);
			await Task.CompletedTask;
		}

		_dbContext.Distritos.Remove(distritoBD);
		await _dbContext.SaveChangesAsync();
	}
}
