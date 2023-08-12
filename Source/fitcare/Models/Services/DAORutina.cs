// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Threading.Tasks;
// using fitcare.Models.Contracts;
// using fitcare.Models.DataAccess.EntityFramework;
// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Storage;

// namespace fitcare.Models.DataAccess;

// public class DAORutina : IDataCRUDBase<Rutina>
// {
// 	private readonly Fitcare_DB_Context _dbContext;

// 	public DAORutina(Fitcare_DB_Context dbContext)
// 	{
// 		_dbContext = dbContext;
// 	}

// 	public async Task<IList<Rutina>> ReadAllAsync()
// 	{
// 		DbSet<Rutinas> rutinasBD = _dbContext.Rutinas;

// 		rutinasBD.Include(i => i.IdInstructorNavigation);
// 		rutinasBD.Include(i => i.IdInstructorNavigation.IdDistritoNavigation).ThenInclude(d => d.IdCantonNavigation).ThenInclude(c => c.IdProvinciaNavigation);

// 		rutinasBD.Include(c => c.IdClienteNavigation);
// 		rutinasBD.Include(c => c.IdClienteNavigation.IdDistritoNavigation).ThenInclude(d => d.IdCantonNavigation).ThenInclude(c => c.IdProvinciaNavigation);

// 		rutinasBD.Include(r => r.MedidasRutina).ThenInclude(m => m.IdTipoMedidaNavigation);
// 		rutinasBD.Include(r => r.EjerciciosRutina).ThenInclude(e => e.IdEjercicioNavigation).ThenInclude(e => e.IdTipoEjercicioNavigation);

// 		IList<Rutinas> listaRutinasBD = await rutinasBD.ToListAsync();

// 		return listaRutinasBD.Select(x => x.ConvertDBModelToDomain()).ToList();
// 	}

// 	public async Task<Rutina> ReadByIdAsync(Guid id)
// 	{
// 		IQueryable<Rutinas> rutinasBD = _dbContext.Rutinas.Where(x => x.Id.Equals(id.ToString()));

// 		rutinasBD.Include(i => i.IdInstructorNavigation);
// 		rutinasBD.Include(i => i.IdInstructorNavigation.IdDistritoNavigation).ThenInclude(d => d.IdCantonNavigation).ThenInclude(c => c.IdProvinciaNavigation);

// 		rutinasBD.Include(c => c.IdClienteNavigation);
// 		rutinasBD.Include(c => c.IdClienteNavigation.IdDistritoNavigation).ThenInclude(d => d.IdCantonNavigation).ThenInclude(c => c.IdProvinciaNavigation);

// 		rutinasBD.Include(r => r.MedidasRutina).ThenInclude(m => m.IdTipoMedidaNavigation);
// 		rutinasBD.Include(r => r.EjerciciosRutina).ThenInclude(e => e.IdEjercicioNavigation).ThenInclude(e => e.IdTipoEjercicioNavigation);

// 		Rutinas rutinaBD = await rutinasBD.FirstOrDefaultAsync();

// 		return rutinaBD.ConvertDBModelToDomain();
// 	}

// 	public async Task CreateAsync(Rutina rutina)
// 	{
// 		using IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();

// 		Rutinas rutinaBD = new(rutina);
// 		_dbContext.Rutinas.Add(rutinaBD);
// 		_dbContext.Entry(rutinaBD).State = EntityState.Added;

// 		AsignarEjercicios(rutina, rutinaBD);
// 		AsignarMedidas(rutina, rutinaBD);

// 		await _dbContext.SaveChangesAsync();
// 		transaction.Commit();
// 	}

// 	public async Task UpdateAsync(Rutina rutina)
// 	{
// 		using IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();

// 		Rutinas rutinaBD = await _dbContext.Rutinas.Where(x => x.Id.Equals(rutina.Id.ToString()))
// 													   .Include(r => r.MedidasRutina).ThenInclude(m => m.IdTipoMedidaNavigation)
// 													   .Include(r => r.EjerciciosRutina).ThenInclude(e => e.IdEjercicioNavigation).ThenInclude(e => e.IdTipoEjercicioNavigation)
// 													   .FirstOrDefaultAsync();

// 		// Actualizar campos necesarios en BD
// 		rutinaBD.IdInstructor = Factory.SetGuid(rutina.Instructor.Id);
// 		rutinaBD.IdCliente = Factory.SetGuid(rutina.Cliente.Id);
// 		rutinaBD.FechaInicio = rutina.FechaInicio;
// 		rutinaBD.FechaFin = rutina.FechaFin;
// 		rutinaBD.Objetivo = rutina.Objetivo;

// 		_dbContext.Rutinas.Update(rutinaBD);
// 		_dbContext.Entry(rutinaBD).State = EntityState.Modified;

// 		AsignarEjercicios(rutina, rutinaBD);
// 		AsignarMedidas(rutina, rutinaBD);

// 		await _dbContext.SaveChangesAsync();
// 		transaction.Commit();
// 	}

// 	public async Task DeleteAsync(Guid id)
// 	{
// 		using IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();

// 		Rutinas rutinaBD = await _dbContext.Rutinas.Where(x => x.Id.Equals(id.ToString()))
// 													   .Include(r => r.MedidasRutina).ThenInclude(m => m.IdTipoMedidaNavigation)
// 													   .Include(r => r.EjerciciosRutina).ThenInclude(e => e.IdEjercicioNavigation).ThenInclude(e => e.IdTipoEjercicioNavigation)
// 													   .FirstOrDefaultAsync();

// 		rutinaBD.MedidasRutina.Clear();
// 		rutinaBD.EjerciciosRutina.Clear();

// 		_dbContext.Rutinas.Remove(rutinaBD);
// 		_dbContext.Entry(rutinaBD).State = EntityState.Deleted;

// 		await _dbContext.SaveChangesAsync();
// 		transaction.Commit();
// 	}

// 	internal void AsignarEjercicios(Rutina rutina, Rutinas rutinaBD)
// 	{
// 		if (_dbContext.Entry(rutinaBD).State.Equals(EntityState.Modified)) rutinaBD.EjerciciosRutina.Clear();

// 		foreach (EjercicioRutina ejercicioRutina in rutina.Ejercicios)
// 		{
// 			rutinaBD.EjerciciosRutina.Add(new EjerciciosRutina(rutina.Id, ejercicioRutina));
// 		}
// 	}

// 	internal void AsignarMedidas(Rutina rutina, Rutinas rutinaBD)
// 	{
// 		if (_dbContext.Entry(rutinaBD).State.Equals(EntityState.Modified)) rutinaBD.MedidasRutina.Clear();

// 		foreach (MedidaRutina medidaRutina in rutina.Medidas)
// 		{
// 			rutinaBD.MedidasRutina.Add(new MedidasRutina(rutina.Id, medidaRutina));
// 		}
// 	}

// 	public IEnumerable<EjercicioRutina> ReadEjerciciosByRutina(Guid idRutina)
// 	{
// 		var ejerciciosRutinaBD = _dbContext.EjerciciosRutina.Where(dr => dr.IdRutina.Equals(idRutina.ToString()));
// 		ejerciciosRutinaBD.Include(e => e.IdEjercicioNavigation).ThenInclude(e => e.IdTipoEjercicioNavigation);
// 		IEnumerable<EjerciciosRutina> listaEjerciciosRutinaBD = ejerciciosRutinaBD.ToList();
// 		IEnumerable<EjercicioRutina> ejerciciosRutina = listaEjerciciosRutinaBD.Select(d => d.ConvertDBModelToDomain()).ToList();
// 		return ejerciciosRutina;
// 	}

// 	public IEnumerable<MedidaRutina> ReadMedidasByRutina(Guid idRutina)
// 	{
// 		var medidasRutinaBD = _dbContext.MedidasRutina.Where(mr => mr.IdRutina.Equals(idRutina.ToString()));
// 		medidasRutinaBD.Include(m => m.IdTipoMedidaNavigation);
// 		IEnumerable<MedidasRutina> listaMedidasRutinaBD = medidasRutinaBD.ToList();
// 		IEnumerable<MedidaRutina> medidasRutina = listaMedidasRutinaBD.Select(d => d.ConvertDBModelToDomain()).ToList();
// 		return medidasRutina;
// 	}

// 	public IEnumerable<Rutina> ReadAllAsyncByCliente(string idCliente)
// 	{
// 		var rutinasBD = _dbContext.Rutinas.Where(r => r.IdCliente.Equals(idCliente));

// 		rutinasBD.Include(i => i.IdInstructorNavigation);
// 		rutinasBD.Include(i => i.IdInstructorNavigation.IdDistritoNavigation).ThenInclude(d => d.IdCantonNavigation).ThenInclude(c => c.IdProvinciaNavigation);

// 		rutinasBD.Include(c => c.IdClienteNavigation);
// 		rutinasBD.Include(c => c.IdClienteNavigation.IdDistritoNavigation).ThenInclude(d => d.IdCantonNavigation).ThenInclude(c => c.IdProvinciaNavigation);

// 		rutinasBD.Include(r => r.MedidasRutina).ThenInclude(m => m.IdTipoMedidaNavigation);
// 		rutinasBD.Include(r => r.EjerciciosRutina).ThenInclude(e => e.IdEjercicioNavigation).ThenInclude(e => e.IdTipoEjercicioNavigation);

// 		IEnumerable<Rutinas> listaRutinasBD = rutinasBD.ToList();

// 		IEnumerable<Rutina> rutinas = listaRutinasBD.Select(x => x.ConvertDBModelToDomain()).ToList();
// 		return rutinas;
// 	}

// 	//public IEnumerable<ReporteRutinaResumido> ObtenerReporteRutinasResumido(string idInstructor, string idCliente)
// 	//{
// 	//	using MySqlConnection conexion = new MySqlConnection(ConnectionString);
// 	//	IEnumerable<ReporteRutinaResumido> datosReporte = conexion.Query<ReporteRutinaResumido>("OBTENER_REPORTE_RUTINAS_RESUMIDO", new { idInstructor, idCliente }, commandType: CommandType.StoredProcedure).ToList();
// 	//	return datosReporte;
// 	//}

// 	//public IEnumerable<ReporteRutinaDetallado> ObtenerReporteRutinasDetallado(string idInstructor, string idCliente)
// 	//{
// 	//	IEnumerable<ReporteRutinaDetallado> datosReporte;

// 	//	using (var Conexion = new MySqlConnection(ConnectionString))
// 	//	{
// 	//		datosReporte = Conexion.Query<ReporteRutinaDetallado>("OBTENER_REPORTE_RUTINAS_RESUMIDO", new { idInstructor, idCliente }, commandType: CommandType.StoredProcedure).ToList();
// 	//	}

// 	//	foreach (var rutina in datosReporte)
// 	//	{
// 	//		IEnumerable<EjercicioRutina> ejercicios = _repositorio.ObtenerEjerciciosRutina(rutina.Id);
// 	//		IEnumerable<MedidaRutina> medidas = _repositorio.ObtenerMedidasRutina(rutina.Id);

// 	//		rutina.Ejercicios = ejercicios;
// 	//		rutina.Medidas = medidas;
// 	//	}

// 	//	return datosReporte;
// 	//}
// }
