// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using fitcare.Models.Contracts;
// using fitcare.Models.DataAccess.EntityFramework;
// using fitcare.Models.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;

// namespace fitcare.Models.DataAccess;

// public class DAOTipoMedida : IManager<TipoMedida>
// {
// 	private readonly FitcareDBContext _dbContext;
// 	private readonly ILogger _logger;

// 	public DAOTipoMedida(FitcareDBContext dbContext, ILogger logger)
// 	{
// 		_dbContext = dbContext;
// 		_logger = logger;
// 	}

// 	public async Task<IList<TipoMedida>> ReadAllAsync()
// 	{
// 		var listaTiposMedidaBD = await _dbContext.TiposMedida.ToListAsync();

// 		if (listaTiposMedidaBD == null || listaTiposMedidaBD.Count == 0)
// 			return new List<TipoMedida>();


// 		var listaTiposMedida = _mapper.Map<List<TipoMedida>>(listaTiposMedidaBD);
// 		return listaTiposMedida;
// 	}

// 	public async Task<TipoMedida> ReadByIdAsync(Guid id)
// 	{
// 		var tipoMedidaBD = await _dbContext.TiposMedida.FindAsync(id);
// 		var tipoMedida = _mapper.Map<TipoMedida>(tipoMedidaBD);
// 		return tipoMedida;
// 	}

// 	public async Task CreateAsync(TipoMedida tipoMedida)
// 	{
// 		var tipoMedidaBD = _mapper.Map<TiposMedida>(tipoMedida);
// 		await _dbContext.TiposMedida.AddAsync(tipoMedidaBD);
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task UpdateAsync(TipoMedida tipoMedida)
// 	{
// 		var tipoMedidaBD = await _dbContext.TiposMedida.FindAsync(tipoMedida.Id);

// 		if (tipoMedidaBD == null)
// 		{
// 			_logger.LogInformation("No se encontró una provincia con el id", tipoMedida.Id);
// 			await Task.CompletedTask;
// 		}

// 		tipoMedidaBD.Codigo = tipoMedida.Codigo;
// 		tipoMedidaBD.Nombre = tipoMedida.Nombre;
// 		tipoMedidaBD.Estado = tipoMedida.Estado;
// 		tipoMedidaBD.UpdatedBy = tipoMedida.EditadoPor;
// 		tipoMedidaBD.DateUpdated = tipoMedida.EditadoEl;

// 		_dbContext.TiposMedida.Update(tipoMedidaBD);
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task DeleteAsync(Guid id)
// 	{
// 		TiposMedida tipoMedidaBD = await _dbContext.TiposMedida.FindAsync(id);

// 		if (tipoMedidaBD == null)
// 		{
// 			_logger.LogInformation("No se encontró una provincia con el id", id);
// 			await Task.CompletedTask;
// 		}

// 		_dbContext.TiposMedida.Remove(tipoMedidaBD);
// 		await _dbContext.SaveChangesAsync();
// 	}
// }
