using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models.Core;

public class Rutinas : IRutinas<Rutina>
{
	private readonly ApplicationDbContext _db;

	public Rutinas(ApplicationDbContext db) => _db = db;

	public async Task<IList<Rutina>> ReadAllAsync() =>
		await _db.Rutinas
			.Include(i => i.Instructor)
			.Include(c => c.Cliente)
			.Include(e => e.Ejercicios)
			.ToListAsync();

	public async Task<Rutina> ReadByIdAsync(Guid id)
	{
		var rutina = await _db.Rutinas.Include(i => i.Instructor)
			.Include(c => c.Cliente)
			.Include(r => r.Medidas).ThenInclude(m => m.TipoMedida)
			.Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.TipoEjercicio)
			.Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.GruposMusculares)
			.Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.Maquinas)
			.FirstOrDefaultAsync(x => x.Id.ToString().Equals(id.ToString()));

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
			throw new Exception(
				$"El usuario instructor {rutina.IdInstructor} para la rutina no se ha encontrado en la BD.");
		else
			rutina.Instructor = existingUsuarioInstructor;

		var existingUsuarioCliente = await _db.Usuarios.FirstOrDefaultAsync(x => x.Id == rutina.IdCliente);

		if (existingUsuarioCliente == null)
			throw new Exception($"El usuario cliente {rutina.IdCliente} para la rutina no se ha encontrado en la BD.");
		else
			rutina.Cliente = existingUsuarioCliente;

		// Recorrer cada ejercicioRutina y medidaRutina
		// para establecer los valores de fecha y usuario de insercion
		foreach (var ejercicioRutina in rutina.Ejercicios)
		{
			var existingEjercicio = await _db.Ejercicios.Include(e => e.TipoEjercicio)
				.Where(e => e.Id == ejercicioRutina.IdEjercicio).FirstOrDefaultAsync();

			if (existingEjercicio == null)
				throw new Exception(
					$"El ejercicio {ejercicioRutina.IdEjercicio} para la rutina no se ha encontrado en la BD.");
			else
				ejercicioRutina.Ejercicio = existingEjercicio;

			ejercicioRutina.CreatedBy = user;
			ejercicioRutina.DateCreated = DateTime.UtcNow;
		}

		foreach (var medida in rutina.Medidas)
		{
			var existingTipoMedida = await _db.TiposMedida.FindAsync(medida.IdTipoMedida);

			if (existingTipoMedida == null)
				throw new Exception(
					$"El tipo de medida {medida.IdTipoMedida} para la rutina no se ha encontrado en la BD.");
			else
				medida.TipoMedida = existingTipoMedida;

			medida.CreatedBy = user;
			medida.DateCreated = DateTime.UtcNow;
		}

		await _db.Rutinas.AddAsync(rutina);
		await _db.SaveChangesAsync();
	}

	public async Task<IList<Rutina>> ObtenerReporteRutinas(string idInstructor, string idCliente) =>
		await _db.Rutinas
			.Include(ir => ir.Instructor)
			.Include(cr => cr.Cliente)
			.Include(er => er.Ejercicios)
			.Where(r =>
				(idInstructor == null || r.IdInstructor == idInstructor) &&
				(idCliente == null || r.IdCliente == idCliente))
			.ToListAsync();

	public async Task AgregarEjercicioAsync(Guid idRutina, EjercicioRutina ejercicio, string user)
	{
		// Verificar que la rutina existe
		var rutinaExists = await _db.Rutinas.AnyAsync(r => r.Id == idRutina);
		if (!rutinaExists)
			throw new KeyNotFoundException($"No se encontró una rutina con el id {idRutina}");

		// Verificar que el ejercicio existe sin rastrearlo
		var ejercicioExists = await _db.Ejercicios.AnyAsync(e => e.Id == ejercicio.IdEjercicio);
		if (!ejercicioExists)
			throw new Exception($"El ejercicio {ejercicio.IdEjercicio} no se ha encontrado en la BD.");

		// Limpiar la navegación para evitar conflictos de tracking
		ejercicio.Ejercicio = null;
		ejercicio.Rutina = null;
		ejercicio.IdRutina = idRutina;
		ejercicio.CreatedBy = user;
		ejercicio.DateCreated = DateTime.UtcNow;

		// Agregar directamente sin cargar la rutina completa
		await _db.Set<EjercicioRutina>().AddAsync(ejercicio);
		await _db.SaveChangesAsync();
	}

	public async Task EditarEjercicioAsync(Guid idEjercicioRutina, Guid idEjercicio, int series, int repeticiones, int minutosDescanso, string user)
	{
		var ejercicioRutina = await _db.Set<EjercicioRutina>().FindAsync(idEjercicioRutina);
		if (ejercicioRutina == null)
			throw new KeyNotFoundException($"No se encontró el ejercicio con id {idEjercicioRutina}");

		// Verificar que el nuevo ejercicio existe
		var ejercicioExists = await _db.Ejercicios.AnyAsync(e => e.Id == idEjercicio);
		if (!ejercicioExists)
			throw new Exception($"El ejercicio {idEjercicio} no se ha encontrado en la BD.");

		ejercicioRutina.IdEjercicio = idEjercicio;
		ejercicioRutina.Series = series;
		ejercicioRutina.Repeticiones = repeticiones;
		ejercicioRutina.MinutosDescanso = minutosDescanso;
		ejercicioRutina.DateUpdated = DateTime.UtcNow;
		ejercicioRutina.UpdatedBy = user;

		await _db.SaveChangesAsync();
	}

	public async Task EliminarEjercicioAsync(Guid idEjercicioRutina)
	{
		var ejercicioRutina = await _db.Set<EjercicioRutina>().FindAsync(idEjercicioRutina);
		if (ejercicioRutina == null)
			throw new KeyNotFoundException($"No se encontró el ejercicio con id {idEjercicioRutina}");

		_db.Set<EjercicioRutina>().Remove(ejercicioRutina);
		await _db.SaveChangesAsync();
	}

	public async Task AgregarMedidaAsync(Guid idRutina, MedidaRutina medida, string user)
	{
		// Verificar que la rutina existe
		var rutinaExists = await _db.Rutinas.AnyAsync(r => r.Id == idRutina);
		if (!rutinaExists)
			throw new KeyNotFoundException($"No se encontró una rutina con el id {idRutina}");

		// Verificar que el tipo de medida existe sin rastrearlo
		var tipoMedidaExists = await _db.TiposMedida.AnyAsync(tm => tm.Id == medida.IdTipoMedida);
		if (!tipoMedidaExists)
			throw new Exception($"El tipo de medida {medida.IdTipoMedida} no se ha encontrado en la BD.");

		// Limpiar la navegación para evitar conflictos de tracking
		medida.TipoMedida = null;
		medida.Rutina = null;
		medida.IdRutina = idRutina;
		medida.CreatedBy = user;
		medida.DateCreated = DateTime.UtcNow;

		// Agregar directamente sin cargar la rutina completa
		await _db.Set<MedidaRutina>().AddAsync(medida);
		await _db.SaveChangesAsync();
	}

	public async Task EditarMedidaAsync(Guid idMedidaRutina, Guid idTipoMedida, string valor, string comentario, string user)
	{
		var medidaRutina = await _db.Set<MedidaRutina>().FindAsync(idMedidaRutina);
		if (medidaRutina == null)
			throw new KeyNotFoundException($"No se encontró la medida con id {idMedidaRutina}");

		// Verificar que el nuevo tipo de medida existe
		var tipoMedidaExists = await _db.TiposMedida.AnyAsync(tm => tm.Id == idTipoMedida);
		if (!tipoMedidaExists)
			throw new Exception($"El tipo de medida {idTipoMedida} no se ha encontrado en la BD.");

		medidaRutina.IdTipoMedida = idTipoMedida;
		medidaRutina.Valor = valor;
		medidaRutina.Comentario = comentario;
		medidaRutina.DateUpdated = DateTime.UtcNow;
		medidaRutina.UpdatedBy = user;

		await _db.SaveChangesAsync();
	}

	public async Task EliminarMedidaAsync(Guid idMedidaRutina)
	{
		var medidaRutina = await _db.Set<MedidaRutina>().FindAsync(idMedidaRutina);
		if (medidaRutina == null)
			throw new KeyNotFoundException($"No se encontró la medida con id {idMedidaRutina}");

		_db.Set<MedidaRutina>().Remove(medidaRutina);
		await _db.SaveChangesAsync();
	}
}
