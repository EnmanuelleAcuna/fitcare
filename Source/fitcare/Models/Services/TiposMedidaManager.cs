using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models;

public class TiposMedidaManager : IManager<TipoMedida>
{
	private readonly FitcareDBContext _dbContext;

	public TiposMedidaManager(FitcareDBContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IList<TipoMedida>> ReadAllAsync()
	{
		var tiposMedida = await _dbContext.TiposMedida.ToListAsync();
		return tiposMedida ?? new List<TipoMedida>();
	}

	public async Task<TipoMedida> ReadByIdAsync(Guid id)
	{
		var tipoMedida = await _dbContext.TiposMedida.FindAsync(id);

		if (tipoMedida == null)
			throw new KeyNotFoundException($"No se encontró el tipo  de medida con el id {id}");

		return tipoMedida;
	}

	public async Task CreateAsync(TipoMedida tipoMedida, string user)
	{
		tipoMedida.CreatedBy = user;
		tipoMedida.DateCreated = DateTime.UtcNow;

		await _dbContext.AddAsync(tipoMedida);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(TipoMedida tipoMedida, string user)
	{
		TipoMedida record = await ReadByIdAsync(tipoMedida.Id);

		record.Codigo = tipoMedida.Codigo;
		record.Nombre = tipoMedida.Nombre;
		record.Estado = tipoMedida.Estado;

		record.UpdatedBy = user;
		record.DateUpdated = DateTime.UtcNow;

		_dbContext.TiposMedida.Update(tipoMedida);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		TipoMedida record = await ReadByIdAsync(id);

		_dbContext.Remove(record);
		await _dbContext.SaveChangesAsync();
	}
}
