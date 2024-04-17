using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models.Contracts;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models;

public class RutinasManager : IRutinasManager<Rutina>
{
	private readonly FitcareDBContext _db;

	public RutinasManager(FitcareDBContext db) => _db = db;

	public async Task<IList<Rutina>> ReadAllAsync()
	{
		// var rutinasDBSet = _db.Rutinas.Include(i => i.Instructor).Include(c => c.Cliente).Include(r => r.Medidas).ThenInclude(m => m.TipoMedida).Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.TipoEjercicio).Include(r => r.GruposMusculares).ThenInclude(e => e.GrupoMuscular);
		var rutinasDBSet = _db.Rutinas.Include(i => i.Instructor).Include(c => c.Cliente).Include(e => e.Ejercicios);

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

		var rutina = await rutinasDBSet.FirstOrDefaultAsync();

		if (rutina == null)
			throw new KeyNotFoundException($"No se encontró una rutina con el id {id}");

		return rutina;
	}

	public async Task CreateAsync(Rutina rutina, string user)
	{
		rutina.DateCreated = DateTime.UtcNow;
		rutina.CreatedBy = user;

		var existingUsuarioInstructor = await _db.Usuarios.FirstOrDefaultAsync(x => x.Id == rutina.IdInstructor);

		if (existingUsuarioInstructor == null)
			throw new Exception($"El usuario instructor {rutina.IdInstructor} para la rutina no se ha encontrado en la BD.");
		else
			rutina.Instructor = existingUsuarioInstructor;

		var existingUsuarioCliente = await _db.Usuarios.FirstOrDefaultAsync(x => x.Id == rutina.IdCliente);

		if (existingUsuarioCliente == null)
			throw new Exception($"El usuario cliente {rutina.IdCliente} para la rutina no se ha encontrado en la BD.");
		else
			rutina.Cliente = existingUsuarioCliente;

		// Recorrer cada ejercicioRutina, medidaRutina y grupoMuscularRutina
		// para establecer los valores de fecha y usuario de insercion
		foreach (var ejercicioRutina in rutina.Ejercicios)
		{
			var existingEjercicio = await _db.Ejercicios.Include(e => e.TipoEjercicio).Where(e => e.Id == ejercicioRutina.IdEjercicio).FirstOrDefaultAsync();

			if (existingEjercicio == null)
				throw new Exception($"El ejercicio {ejercicioRutina.IdEjercicio} para la rutina no se ha encontrado en la BD.");
			else
				ejercicioRutina.Ejercicio = existingEjercicio;

			var existingMaquina = await _db.Maquinas.Include(m => m.TipoMaquina).Where(m => m.Id == ejercicioRutina.IdMaquina).FirstOrDefaultAsync();

			if (existingMaquina == null)
				throw new Exception($"La máquina {ejercicioRutina.IdMaquina} para la rutina no se ha encontrado en la BD.");
			else
				ejercicioRutina.Maquina = existingMaquina;

			ejercicioRutina.CreatedBy = user;
			ejercicioRutina.DateCreated = DateTime.UtcNow;
		}

		foreach (var medida in rutina.Medidas)
		{
			var existingTipoMedida = await _db.TiposMedida.FindAsync(medida.IdTipoMedida);

			if (existingTipoMedida == null)
				throw new Exception($"El tipo de medida {medida.IdTipoMedida} para la rutina no se ha encontrado en la BD.");
			else
				medida.TipoMedida = existingTipoMedida;

			medida.CreatedBy = user;
			medida.DateCreated = DateTime.UtcNow;
		}

		await _db.Rutinas.AddAsync(rutina);
		await _db.SaveChangesAsync();
	}

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
