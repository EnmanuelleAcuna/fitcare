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

// public class DAOInstructor : IDataCRUDBase<Instructor>
// {
// 	private readonly Fitcare_DB_Context _dbContext;

// 	public DAOInstructor(Fitcare_DB_Context dbContext)
// 	{
// 		_dbContext = dbContext;
// 	}

// 	public async Task<IList<Instructor>> ReadAllAsync()
// 	{
// 		IList<Instructores> listaInstructoresBD = await _dbContext.Instructores.Include(x => x.IdDistritoNavigation)
// 																			   .ToListAsync();
// 		return listaInstructoresBD.Select(x => x.ConvertDBModelToDomain()).ToList();
// 	}

// 	public async Task<Instructor> ReadByIdAsync(Guid id)
// 	{
// 		Instructores instructorBD = await _dbContext.Instructores.Where(x => x.IdUsuario.Equals(id))
// 																 .Include(x => x.IdDistritoNavigation)
// 																 .Include(d => d.IdDistritoNavigation.IdCantonNavigation)
// 																 .Include(c => c.IdDistritoNavigation.IdCantonNavigation.IdProvinciaNavigation)
// 																 .FirstOrDefaultAsync();
// 		return instructorBD.ConvertDBModelToDomain();
// 	}

// 	public async Task CreateAsync(Instructor instructor)
// 	{
// 		Instructores instructorBD = new(instructor);
// 		_dbContext.Instructores.Add(instructorBD);
// 		_dbContext.Entry(instructorBD).State = EntityState.Added;
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task UpdateAsync(Instructor instructor)
// 	{
// 		Instructores instructorBD = await _dbContext.Instructores.FindAsync(instructor.Id);

// 		// Actualizar campos necesarios en BD
// 		instructorBD.FechaIngreso = instructor.FechaIngreso;
// 		instructorBD.IdDistrito = instructor.Distrito.Id;

// 		_dbContext.Instructores.Update(instructorBD);
// 		_dbContext.Entry(instructorBD).State = EntityState.Modified;
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task DeleteAsync(Guid id)
// 	{
// 		Instructores instructorBD = await _dbContext.Instructores.FindAsync(id);

// 		_dbContext.Instructores.Remove(instructorBD);
// 		_dbContext.Entry(instructorBD).State = EntityState.Deleted;
// 		await _dbContext.SaveChangesAsync();
// 	}
// }
