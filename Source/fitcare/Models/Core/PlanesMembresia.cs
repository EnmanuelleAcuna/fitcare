using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models.Core;

public class PlanesMembresia : IBaseCore<PlanMembresia>
{
	private readonly ApplicationDbContext _dbContext;

	public PlanesMembresia(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IList<PlanMembresia>> ReadAllAsync()
	{
		var planes = await _dbContext.PlanesMembresia
			.OrderBy(p => p.Nombre)
			.ToListAsync();
		return planes ?? new List<PlanMembresia>();
	}

	public async Task<PlanMembresia> ReadByIdAsync(Guid id)
	{
		var plan = await _dbContext.PlanesMembresia.FindAsync(id);
		if (plan == null)
			throw new KeyNotFoundException($"No se encontró el plan de membresía con el id {id}");
		return plan;
	}

	public async Task CreateAsync(PlanMembresia plan, string user)
	{
		plan.CreatedBy = user;
		plan.DateCreated = DateTime.UtcNow;

		await _dbContext.AddAsync(plan);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(PlanMembresia plan, string user)
	{
		PlanMembresia record = await ReadByIdAsync(plan.Id);

		record.Nombre = plan.Nombre;
		record.Dias = plan.Dias;
		record.Costo = plan.Costo;
		record.Estado = plan.Estado;

		record.UpdatedBy = user;
		record.DateUpdated = DateTime.UtcNow;

		_dbContext.PlanesMembresia.Update(record);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		PlanMembresia record = await ReadByIdAsync(id);
		_dbContext.Remove(record);
		await _dbContext.SaveChangesAsync();
	}
}
