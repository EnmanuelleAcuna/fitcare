using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace fitcare.Models;

public class RutinasManager : IManager<Rutina>
{
	private readonly FitcareDBContext _db;

	public RutinasManager(FitcareDBContext db) => _db = db;

	public async Task<IList<Rutina>> ReadAllAsync()
	{
		var rutinasDBSet = _db.Rutinas;

		rutinasDBSet.Include(i => i.Instructor);
		rutinasDBSet.Include(c => c.Cliente);
		rutinasDBSet.Include(r => r.Medidas).ThenInclude(m => m.TipoMedida);
		rutinasDBSet.Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.TipoEjercicio);
		rutinasDBSet.Include(r => r.GruposMusculares).ThenInclude(e => e.GrupoMuscular);

		var rutinas = await rutinasDBSet.ToListAsync();
		return rutinas ?? new List<Rutina>();
	}

	public async Task<Rutina> ReadByIdAsync(Guid id)
	{
		var rutinasDBSet = _db.Rutinas.Where(x => x.Id.Equals(id.ToString()));

		rutinasDBSet.Include(i => i.Instructor);
		rutinasDBSet.Include(c => c.Cliente);
		rutinasDBSet.Include(r => r.Medidas).ThenInclude(m => m.TipoMedida);
		rutinasDBSet.Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.TipoEjercicio);
		rutinasDBSet.Include(r => r.GruposMusculares).ThenInclude(e => e.GrupoMuscular);

		var rutina = await rutinasDBSet.FirstOrDefaultAsync();

		if (rutina == null)
			throw new KeyNotFoundException($"No se encontró una rutina con el id {id}");

		return rutina;
	}

	public async Task CreateAsync(Rutina rutina, string user)
	{
		using IDbContextTransaction transaction = _db.Database.BeginTransaction();

		rutina.DateCreated = DateTime.UtcNow;
		rutina.CreatedBy = user;

		await _db.Rutinas.AddAsync(rutina);
		// AsignarEjercicios(rutina, rutinaBD);
		// AsignarMedidas(rutina, rutinaBD);
		await _db.SaveChangesAsync();

		transaction.Commit();
	}

	public async Task UpdateAsync(Rutina rutina, string user)
	{
		using IDbContextTransaction transaction = _db.Database.BeginTransaction();

		var record = await ReadByIdAsync(rutina.Id);

		record.IdInstructor = rutina.IdInstructor;
		record.IdCliente = rutina.IdCliente;
		record.FechaRealizacion = rutina.FechaRealizacion;
		record.FechaInicio = rutina.FechaInicio;
		record.FechaFin = rutina.FechaFin;
		record.Objetivo = rutina.Objetivo;

		record.DateUpdated = DateTime.Now;
		record.UpdatedBy = user;

		_db.Update(record);
		// AsignarEjercicios(rutina, rutinaBD);
		// AsignarMedidas(rutina, rutinaBD);
		await _db.SaveChangesAsync();

		transaction.Commit();
	}

	public async Task DeleteAsync(Guid id)
	{
		using IDbContextTransaction transaction = _db.Database.BeginTransaction();

		var record = await ReadByIdAsync(id);

		// rutinaBD.MedidasRutina.Clear();
		// rutinaBD.EjerciciosRutina.Clear();

		_db.Remove(record);
		await _db.SaveChangesAsync();

		transaction.Commit();
	}

	// internal void AsignarEjercicios(Rutina rutina, Rutinas rutinaBD)
	// {
	// 	if (_db.Entry(rutinaBD).State.Equals(EntityState.Modified)) rutinaBD.EjerciciosRutina.Clear();

	// 	foreach (EjercicioRutina ejercicioRutina in rutina.Ejercicios)
	// 	{
	// 		rutinaBD.EjerciciosRutina.Add(new EjerciciosRutina(rutina.Id, ejercicioRutina));
	// 	}
	// }

	// internal void AsignarMedidas(Rutina rutina, Rutinas rutinaBD)
	// {
	// 	if (_db.Entry(rutinaBD).State.Equals(EntityState.Modified)) rutinaBD.MedidasRutina.Clear();

	// 	foreach (MedidaRutina medidaRutina in rutina.Medidas)
	// 	{
	// 		rutinaBD.MedidasRutina.Add(new MedidasRutina(rutina.Id, medidaRutina));
	// 	}
	// }

	// public IEnumerable<EjercicioRutina> ReadEjerciciosByRutina(Guid idRutina)
	// {
	// 	var ejerciciosRutinaBD = _db.EjerciciosRutina.Where(dr => dr.IdRutina.Equals(idRutina.ToString()));
	// 	ejerciciosRutinaBD.Include(e => e.IdEjercicioNavigation).ThenInclude(e => e.IdTipoEjercicioNavigation);
	// 	IEnumerable<EjerciciosRutina> listaEjerciciosRutinaBD = ejerciciosRutinaBD.ToList();
	// 	IEnumerable<EjercicioRutina> ejerciciosRutina = listaEjerciciosRutinaBD.Select(d => d.ConvertDBModelToDomain()).ToList();
	// 	return ejerciciosRutina;
	// }

	// public IEnumerable<MedidaRutina> ReadMedidasByRutina(Guid idRutina)
	// {
	// 	var medidasRutinaBD = _db.MedidasRutina.Where(mr => mr.IdRutina.Equals(idRutina.ToString()));
	// 	medidasRutinaBD.Include(m => m.IdTipoMedidaNavigation);
	// 	IEnumerable<MedidasRutina> listaMedidasRutinaBD = medidasRutinaBD.ToList();
	// 	IEnumerable<MedidaRutina> medidasRutina = listaMedidasRutinaBD.Select(d => d.ConvertDBModelToDomain()).ToList();
	// 	return medidasRutina;
	// }

	// public IEnumerable<Rutina> ReadAllAsyncByCliente(string idCliente)
	// {
	// 	var rutinasBD = _db.Rutinas.Where(r => r.IdCliente.Equals(idCliente));

	// 	rutinasBD.Include(i => i.IdInstructorNavigation);
	// 	rutinasBD.Include(i => i.IdInstructorNavigation.IdDistritoNavigation).ThenInclude(d => d.IdCantonNavigation).ThenInclude(c => c.IdProvinciaNavigation);

	// 	rutinasBD.Include(c => c.IdClienteNavigation);
	// 	rutinasBD.Include(c => c.IdClienteNavigation.IdDistritoNavigation).ThenInclude(d => d.IdCantonNavigation).ThenInclude(c => c.IdProvinciaNavigation);

	// 	rutinasBD.Include(r => r.MedidasRutina).ThenInclude(m => m.IdTipoMedidaNavigation);
	// 	rutinasBD.Include(r => r.EjerciciosRutina).ThenInclude(e => e.IdEjercicioNavigation).ThenInclude(e => e.IdTipoEjercicioNavigation);

	// 	IEnumerable<Rutinas> listaRutinasBD = rutinasBD.ToList();

	// 	IEnumerable<Rutina> rutinas = listaRutinasBD.Select(x => x.ConvertDBModelToDomain()).ToList();
	// 	return rutinas;
	// }

	//public IEnumerable<ReporteRutinaResumido> ObtenerReporteRutinasResumido(string idInstructor, string idCliente)
	//{
	//	using MySqlConnection conexion = new MySqlConnection(ConnectionString);
	//	IEnumerable<ReporteRutinaResumido> datosReporte = conexion.Query<ReporteRutinaResumido>("OBTENER_REPORTE_RUTINAS_RESUMIDO", new { idInstructor, idCliente }, commandType: CommandType.StoredProcedure).ToList();
	//	return datosReporte;
	//}

	//public IEnumerable<ReporteRutinaDetallado> ObtenerReporteRutinasDetallado(string idInstructor, string idCliente)
	//{
	//	IEnumerable<ReporteRutinaDetallado> datosReporte;

	//	using (var Conexion = new MySqlConnection(ConnectionString))
	//	{
	//		datosReporte = Conexion.Query<ReporteRutinaDetallado>("OBTENER_REPORTE_RUTINAS_RESUMIDO", new { idInstructor, idCliente }, commandType: CommandType.StoredProcedure).ToList();
	//	}

	//	foreach (var rutina in datosReporte)
	//	{
	//		IEnumerable<EjercicioRutina> ejercicios = _repositorio.ObtenerEjerciciosRutina(rutina.Id);
	//		IEnumerable<MedidaRutina> medidas = _repositorio.ObtenerMedidasRutina(rutina.Id);

	//		rutina.Ejercicios = ejercicios;
	//		rutina.Medidas = medidas;
	//	}

	//	return datosReporte;
	//}
}
