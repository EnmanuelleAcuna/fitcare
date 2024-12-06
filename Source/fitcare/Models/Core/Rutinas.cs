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
			.Include(r => r.Ejercicios).ThenInclude(e => e.Maquina).ThenInclude(e => e.TipoMaquina)
			.Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.TipoEjercicio)
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

		// Recorrer cada ejercicioRutina, medidaRutina y grupoMuscularRutina
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

			var existingMaquina = await _db.Maquinas.Include(m => m.TipoMaquina)
				.Where(m => m.Id == ejercicioRutina.IdMaquina).FirstOrDefaultAsync();

			if (existingMaquina == null)
				throw new Exception(
					$"La máquina {ejercicioRutina.IdMaquina} para la rutina no se ha encontrado en la BD.");
			else
				ejercicioRutina.Maquina = existingMaquina;

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
}
