using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
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
			.Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.Maquinas).ThenInclude(m => m.TipoMaquina)
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

	#region Exportación

	/// <summary>
	/// Obtiene rutinas con detalle completo para exportación
	/// </summary>
	public async Task<IList<Rutina>> ObtenerRutinasParaExportarAsync(string idInstructor, string idCliente) =>
		await _db.Rutinas
			.Include(r => r.Instructor)
			.Include(r => r.Cliente)
			.Include(r => r.Medidas).ThenInclude(m => m.TipoMedida)
			.Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.GruposMusculares)
			.Include(r => r.Ejercicios).ThenInclude(e => e.Ejercicio).ThenInclude(e => e.Maquinas).ThenInclude(m => m.TipoMaquina)
			.Where(r =>
				(idInstructor == null || r.IdInstructor == idInstructor) &&
				(idCliente == null || r.IdCliente == idCliente))
			.OrderByDescending(r => r.FechaRealizacion)
			.ToListAsync();

	/// <summary>
	/// Genera archivo Excel con las rutinas y su detalle
	/// </summary>
	public byte[] ExportarRutinasExcel(IList<Rutina> rutinas)
	{
		using var workbook = new XLWorkbook();
		var worksheet = workbook.Worksheets.Add("Rutinas");

		int row = 1;

		foreach (var rutina in rutinas)
		{
			// Encabezado de la rutina
			worksheet.Cell(row, 1).Value = "RUTINA";
			worksheet.Range(row, 1, row, 7).Merge();
			worksheet.Range(row, 1, row, 7).Style.Font.Bold = true;
			worksheet.Range(row, 1, row, 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#1e3a5f");
			worksheet.Range(row, 1, row, 7).Style.Font.FontColor = XLColor.White;
			row++;

			// Información general de la rutina
			worksheet.Cell(row, 1).Value = "Instructor";
			worksheet.Cell(row, 2).Value = "Cliente";
			worksheet.Cell(row, 3).Value = "Fecha Inicio";
			worksheet.Cell(row, 4).Value = "Fecha Fin";
			worksheet.Cell(row, 5).Value = "Ejercicios";
			worksheet.Cell(row, 6).Value = "Objetivo";
			worksheet.Range(row, 1, row, 6).Style.Font.Bold = true;
			worksheet.Range(row, 1, row, 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#e9ecef");
			row++;

			worksheet.Cell(row, 1).Value = rutina.Instructor?.FullName;
			worksheet.Cell(row, 2).Value = rutina.Cliente?.FullName;
			worksheet.Cell(row, 3).Value = rutina.FechaInicio.ToString("dd/MM/yyyy");
			worksheet.Cell(row, 4).Value = rutina.FechaFin.ToString("dd/MM/yyyy");
			worksheet.Cell(row, 5).Value = rutina.Ejercicios?.Count ?? 0;
			worksheet.Cell(row, 6).Value = rutina.Objetivo;
			row++;

			// Sección de ejercicios (indentada una columna)
			if (rutina.Ejercicios != null && rutina.Ejercicios.Any())
			{
				row++;
				worksheet.Cell(row, 2).Value = "EJERCICIOS";
				worksheet.Range(row, 2, row, 7).Merge();
				worksheet.Range(row, 2, row, 7).Style.Font.Bold = true;
				worksheet.Range(row, 2, row, 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#28a745");
				worksheet.Range(row, 2, row, 7).Style.Font.FontColor = XLColor.White;
				row++;

				worksheet.Cell(row, 2).Value = "Ejercicio";
				worksheet.Cell(row, 3).Value = "Grupos Musculares";
				worksheet.Cell(row, 4).Value = "Máquinas";
				worksheet.Cell(row, 5).Value = "Series";
				worksheet.Cell(row, 6).Value = "Repeticiones";
				worksheet.Cell(row, 7).Value = "Descanso";
				worksheet.Range(row, 2, row, 7).Style.Font.Bold = true;
				worksheet.Range(row, 2, row, 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#d4edda");
				row++;

				foreach (var ejercicio in rutina.Ejercicios)
				{
					worksheet.Cell(row, 2).Value = ejercicio.Ejercicio?.Nombre;
					worksheet.Cell(row, 3).Value = ejercicio.Ejercicio?.GruposMusculares != null
						? string.Join(", ", ejercicio.Ejercicio.GruposMusculares.Select(g => g.Nombre))
						: string.Empty;
					worksheet.Cell(row, 4).Value = ejercicio.Ejercicio?.Maquinas != null
						? string.Join(", ", ejercicio.Ejercicio.Maquinas.Select(m => $"{m.Nombre} ({m.TipoMaquina?.Nombre})"))
						: string.Empty;
					worksheet.Cell(row, 5).Value = ejercicio.Series;
					worksheet.Cell(row, 6).Value = ejercicio.Repeticiones;
					worksheet.Cell(row, 7).Value = $"{ejercicio.MinutosDescanso} {(ejercicio.MinutosDescanso == 1 ? "minuto" : "minutos")}";
					row++;
				}
			}

			// Sección de medidas (indentada una columna)
			if (rutina.Medidas != null && rutina.Medidas.Any())
			{
				row++;
				worksheet.Cell(row, 2).Value = "MEDIDAS";
				worksheet.Range(row, 2, row, 7).Merge();
				worksheet.Range(row, 2, row, 7).Style.Font.Bold = true;
				worksheet.Range(row, 2, row, 7).Style.Fill.BackgroundColor = XLColor.FromHtml("#007bff");
				worksheet.Range(row, 2, row, 7).Style.Font.FontColor = XLColor.White;
				row++;

				worksheet.Cell(row, 2).Value = "Tipo de Medida";
				worksheet.Cell(row, 3).Value = "Valor";
				worksheet.Cell(row, 4).Value = "Comentario";
				worksheet.Range(row, 2, row, 4).Style.Font.Bold = true;
				worksheet.Range(row, 2, row, 4).Style.Fill.BackgroundColor = XLColor.FromHtml("#cce5ff");
				row++;

				foreach (var medida in rutina.Medidas)
				{
					worksheet.Cell(row, 2).Value = medida.TipoMedida?.Nombre;
					worksheet.Cell(row, 3).Value = medida.Valor;
					worksheet.Cell(row, 4).Value = medida.Comentario;
					row++;
				}
			}

			// Espacio entre rutinas
			row += 2;
		}

		worksheet.Columns().AdjustToContents();

		using var stream = new MemoryStream();
		workbook.SaveAs(stream);
		return stream.ToArray();
	}

	#endregion
}
