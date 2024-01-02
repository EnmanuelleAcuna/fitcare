using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models.DataAccess;

// public class EjerciciosManager : IManager<Ejercicio>
// {
// 	private readonly Fitcare_DB_Context _dbContext;

// 	public DAOEjercicio(Fitcare_DB_Context dbContext)
// 	{
// 		_dbContext = dbContext;
// 	}

// 	public async Task<IList<Ejercicio>> ReadAllAsync()
// 	{
// 		IList<Ejercicios> listaEjerciciosBD = await _dbContext.Ejercicios.Include(z => z.IdTipoEjercicioNavigation)
// 																		 .Include(g => g.IdGrupoMuscular)
// 																		 .Include(x => x.IdAccesorio)
// 																		 .Include(y => y.IdMaquina).ThenInclude(tm => tm.IdTipoMaquinaNavigation)
// 																		 .ToListAsync();
// 		return listaEjerciciosBD.Select(x => x.ConvertDBModelToDomain()).ToList();
// 	}

// 	public async Task<Ejercicio> ReadByIdAsync(Guid id)
// 	{
// 		Ejercicios ejercicioBD = await _dbContext.Ejercicios.Where(x => x.Id.Equals(id))
// 															.Include(z => z.IdTipoEjercicioNavigation)
// 															.Include(g => g.IdGrupoMuscular)
// 															.Include(x => x.IdAccesorio)
// 															.Include(y => y.IdMaquina).ThenInclude(tm => tm.IdTipoMaquinaNavigation)
// 															.FirstOrDefaultAsync();
// 		return ejercicioBD.ConvertDBModelToDomain();
// 	}

// 	public async Task CreateAsync(Ejercicio ejercicio)
// 	{
// 		using IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();

// 		Ejercicios EjercicioBD = new(ejercicio);
// 		_dbContext.Ejercicios.Add(EjercicioBD);

// 		_dbContext.Entry(EjercicioBD).State = EntityState.Added;

// 		AsignarAccesorios(ejercicio, EjercicioBD);
// 		AsignarMaquinas(ejercicio, EjercicioBD);
// 		AsignarGruposMusculares(ejercicio, EjercicioBD);

// 		await _dbContext.SaveChangesAsync();
// 		transaction.Commit();
// 	}

// 	public async Task UpdateAsync(Ejercicio ejercicio)
// 	{
// 		using IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();

// 		Ejercicios ejercicioBD = await _dbContext.Ejercicios.Where(x => x.Id.Equals(ejercicio.Id))
// 																.Include(z => z.IdTipoEjercicioNavigation)
// 																.Include(g => g.IdGrupoMuscular)
// 																.Include(x => x.IdAccesorio)
// 																.Include(y => y.IdMaquina).ThenInclude(tm => tm.IdTipoMaquinaNavigation)
// 																.FirstOrDefaultAsync();

// 		// Actualizar campos necesarios en BD
// 		ejercicioBD.Codigo = ejercicio.Codigo;
// 		ejercicioBD.Nombre = ejercicio.Nombre;
// 		ejercicioBD.Estado = ejercicio.Estado;
// 		ejercicioBD.IdTipoEjercicio = ejercicio.TipoEjercicio.Id;

// 		_dbContext.Ejercicios.Update(ejercicioBD);

// 		_dbContext.Entry(ejercicioBD).State = EntityState.Modified;

// 		AsignarAccesorios(ejercicio, ejercicioBD);
// 		AsignarMaquinas(ejercicio, ejercicioBD);
// 		AsignarGruposMusculares(ejercicio, ejercicioBD);

// 		await _dbContext.SaveChangesAsync();
// 		transaction.Commit();
// 	}

// 	public async Task DeleteAsync(Guid id)
// 	{
// 		using IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();

// 		Ejercicios ejercicioBD = await _dbContext.Ejercicios.Where(x => x.Id.Equals(id))
// 																.Include(z => z.IdTipoEjercicioNavigation)
// 																.Include(g => g.IdGrupoMuscular)
// 																.Include(x => x.IdAccesorio)
// 																.Include(y => y.IdMaquina).ThenInclude(tm => tm.IdTipoMaquinaNavigation)
// 																.FirstOrDefaultAsync();

// 		ejercicioBD.IdAccesorio.Clear();
// 		ejercicioBD.IdMaquina.Clear();
// 		ejercicioBD.IdGrupoMuscular.Clear();

// 		_dbContext.Ejercicios.Remove(ejercicioBD);

// 		_dbContext.Entry(ejercicioBD).State = EntityState.Deleted;

// 		await _dbContext.SaveChangesAsync();
// 		transaction.Commit();
// 	}

// 	internal void AsignarAccesorios(Ejercicio ejercicio, Ejercicios ejercicioBD)
// 	{
// 		// Limpiar los accesorios existentes cuando se está modificando el ejercicio
// 		if (_dbContext.Entry(ejercicioBD).State.Equals(EntityState.Modified)) ejercicioBD.IdAccesorio.Clear();

// 		// Recorrer la lista de accesorios relacionados que contiene la entidad y buscar cada accesorio y agregarlo a la relacion con ejercicio
// 		foreach (Accesorio accesorio in ejercicio.Accesorios)
// 		{
// 			Accesorios accesorioBD = _dbContext.Accesorios.Find(accesorio.Id);
// 			if (accesorioBD != null) ejercicioBD.IdAccesorio.Add(accesorioBD);
// 		}
// 	}

// 	internal void AsignarMaquinas(Ejercicio ejercicio, Ejercicios ejercicioBD)
// 	{
// 		// Limpiar las maquinas existentes cuando se está modificando el ejercicio
// 		if (_dbContext.Entry(ejercicioBD).State.Equals(EntityState.Modified)) ejercicioBD.IdMaquina.Clear();

// 		// Recorrer la lista de máquinas relacionadss que contiene la entidad y buscar cada maquina y agregarlo a la relacion con ejercicio
// 		foreach (var maquina in ejercicio.Maquinas)
// 		{
// 			Maquinas maquinaBD = _dbContext.Maquinas.Find(maquina.Id);
// 			if (maquinaBD != null) ejercicioBD.IdMaquina.Add(maquinaBD);
// 		}
// 	}

// 	internal void AsignarGruposMusculares(Ejercicio ejercicio, Ejercicios ejercicioBD)
// 	{
// 		// Limpiar los grupos musuclares existentes cuando se está modificando el ejercicio
// 		if (_dbContext.Entry(ejercicioBD).State.Equals(EntityState.Modified)) ejercicioBD.IdGrupoMuscular.Clear();

// 		foreach (GrupoMuscular grupoMuscular in ejercicio.GruposMusculares)
// 		{
// 			GruposMusculares grupoMuscularBD = _dbContext.GruposMusculares.Find(grupoMuscular.Id);
// 			if (grupoMuscularBD != null) ejercicioBD.IdGrupoMuscular.Add(grupoMuscularBD);
// 		}
// 	}
// }

public class TiposEjercicioManager : IManager<TipoEjercicio>
{
	private readonly FitcareDBContext _dbContext;

	public TiposEjercicioManager(FitcareDBContext dbContext) => _dbContext = dbContext;

	public async Task<IList<TipoEjercicio>> ReadAllAsync()
	{
		var tiposEjercicio = await _dbContext.TiposEjercicio.ToListAsync();
		return tiposEjercicio ?? new List<TipoEjercicio>();
	}

	public async Task<TipoEjercicio> ReadByIdAsync(Guid id)
	{
		TipoEjercicio tipoEjercicio = await _dbContext.TiposEjercicio.FindAsync(id);

		if (tipoEjercicio == null)
			throw new KeyNotFoundException($"No se encontró el tipo  de ejercicio con el id {id}");

		return tipoEjercicio;
	}

	public async Task CreateAsync(TipoEjercicio tipoEjercicio, string user)
	{
		tipoEjercicio.DateCreated = DateTime.Now;
		tipoEjercicio.CreatedBy = user;

		await _dbContext.AddAsync(tipoEjercicio);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(TipoEjercicio tipoEjercicio, string user)
	{
		TipoEjercicio record = await ReadByIdAsync(tipoEjercicio.Id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró el tipo  de ejercicio con el id {tipoEjercicio.Id}");

		record.Codigo = tipoEjercicio.Codigo;
		record.Nombre = tipoEjercicio.Nombre;
		record.Estado = tipoEjercicio.Estado;

		record.DateUpdated = DateTime.Now;
		record.UpdatedBy = user;

		_dbContext.Update(record);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		TipoEjercicio record = await ReadByIdAsync(id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró el tipo  de ejercicio con el id {id}");

		_dbContext.Remove(record);
		await _dbContext.SaveChangesAsync();
	}
}
