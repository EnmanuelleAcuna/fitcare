// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Threading.Tasks;
// using fitcare.Models.Contracts;
// using fitcare.Models.DataAccess.EntityFramework;
// using fitcare.Models.Entities;
// using Microsoft.EntityFrameworkCore;

// namespace fitcare.Models.DataAccess;

// public class DAOGrupoMuscular : IDataCRUDBase<GrupoMuscular>
// {
// 	private readonly Fitcare_DB_Context _dbContext;

// 	public DAOGrupoMuscular(Fitcare_DB_Context dbContext)
// 	{
// 		_dbContext = dbContext;
// 	}

// 	public async Task<IList<GrupoMuscular>> ReadAllAsync()
// 	{
// 		IList<GruposMusculares> listaGruposMuscularesBD = await _dbContext.GruposMusculares.ToListAsync();
// 		return listaGruposMuscularesBD.Select(g => g.ConvertDBModelToDomain()).ToList();
// 	}

// 	public async Task<GrupoMuscular> ReadByIdAsync(Guid id)
// 	{
// 		GruposMusculares grupomuscularBD = await _dbContext.GruposMusculares.FindAsync(id);
// 		return grupomuscularBD.ConvertDBModelToDomain();
// 	}

// 	public async Task CreateAsync(GrupoMuscular grupoMuscular)
// 	{
// 		GruposMusculares grupoMuscularBD = new(grupoMuscular);
// 		_dbContext.GruposMusculares.Add(grupoMuscularBD);
// 		_dbContext.Entry(grupoMuscularBD).State = EntityState.Added;
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task UpdateAsync(GrupoMuscular grupoMuscular)
// 	{
// 		GruposMusculares grupoMuscularBD = await _dbContext.GruposMusculares.FindAsync(grupoMuscular.Id);

// 		// Actualizar campos necesarios en BD
// 		grupoMuscularBD.Nombre = grupoMuscular.Nombre;
// 		grupoMuscularBD.Descripcion = grupoMuscular.Descripcion;
// 		grupoMuscularBD.Estado = grupoMuscular.Estado;

// 		_dbContext.GruposMusculares.Update(grupoMuscularBD);
// 		_dbContext.Entry(grupoMuscularBD).State = EntityState.Modified;
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task DeleteAsync(Guid id)
// 	{
// 		GruposMusculares grupoMuscularBD = _dbContext.GruposMusculares.Find(id);

// 		_dbContext.GruposMusculares.Remove(grupoMuscularBD);
// 		_dbContext.Entry(grupoMuscularBD).State = EntityState.Deleted;
// 		await _dbContext.SaveChangesAsync();
// 	}
// }
