using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models.Core;

public class Maquinas : IBaseCore<Maquina>, IGeneradorCodigo<Maquina>
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IBaseCore<TipoMaquina> _tiposMaquina;

	public Maquinas(ApplicationDbContext dbContext, IBaseCore<TipoMaquina> tipoMaquina)
	{
		_dbContext = dbContext;
		_tiposMaquina = tipoMaquina;
	}

	public async Task<IList<Maquina>> ReadAllAsync()
	{
		var maquinas = await _dbContext.Maquinas
			.Include(m => m.TipoMaquina)
			.OrderBy(m => m.Codigo.Length)
			.ThenBy(m => m.Codigo)
			.ToListAsync();
		return maquinas ?? new List<Maquina>();
	}

	public async Task<Maquina> ReadByIdAsync(Guid id)
	{
		var maquina = await _dbContext.Maquinas.Include(m => m.TipoMaquina).FirstAsync(m => m.Id == id);
		return maquina ?? throw new KeyNotFoundException($"No se encontró una máquina con el id {id}");
	}

	public async Task CreateAsync(Maquina maquina, string user)
	{
		var existingTipoMaquina = await _tiposMaquina.ReadByIdAsync(maquina.IdTipoMaquina);

		if (existingTipoMaquina == null)
			throw new Exception($"El tipo de máquina {maquina.IdTipoMaquina} para la máquina no se ha encontrado en la BD.");
		else
			maquina.TipoMaquina = existingTipoMaquina;

		maquina.CreatedBy = user;
		maquina.DateCreated = DateTime.Now;

		await _dbContext.AddAsync(maquina);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Maquina maquina, string user)
	{
		var record = await ReadByIdAsync(maquina.Id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró una máquina con el id {maquina.Id}");

		record.Codigo = maquina.Codigo;
		record.Nombre = maquina.Nombre;
		record.CodigoActivo = maquina.CodigoActivo;
		record.Estado = maquina.Estado;
		record.IdTipoMaquina = maquina.IdTipoMaquina;
		record.FechaAdquisicion = maquina.FechaAdquisicion;

		record.UpdatedBy = user;
		record.DateUpdated = DateTime.Now;

		_dbContext.Update(record);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var record = await ReadByIdAsync(id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró una máquina con el id {id}");

		_dbContext.Remove(record);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<string> GenerarCodigoAsync()
	{
		const string prefijo = "MAQ";

		var ultimaMaquina = await _dbContext.Maquinas
			.Where(m => m.Codigo.StartsWith(prefijo))
			.OrderByDescending(m => m.Codigo.Length)
			.ThenByDescending(m => m.Codigo)
			.FirstOrDefaultAsync();

		int siguienteNumero = 1;

		if (ultimaMaquina != null)
		{
			string numeroStr = ultimaMaquina.Codigo.Substring(prefijo.Length);
			if (int.TryParse(numeroStr, out int numeroActual))
			{
				siguienteNumero = numeroActual + 1;
			}
		}

		string formato = siguienteNumero <= 999 ? "D3" : "D0";
		return $"{prefijo}{siguienteNumero.ToString(formato)}";
	}
}

public class TiposMaquina : IBaseCore<TipoMaquina>, IGeneradorCodigo<TipoMaquina>
{
	private readonly ApplicationDbContext _dbContext;

	public TiposMaquina(ApplicationDbContext dbContext) => _dbContext = dbContext;

	public async Task<IList<TipoMaquina>> ReadAllAsync()
	{
		var tiposMaquina = await _dbContext.TiposMaquina
			.OrderBy(t => t.Codigo.Length)
			.ThenBy(t => t.Codigo)
			.ToListAsync();
		return tiposMaquina ?? new List<TipoMaquina>();
	}

	public async Task<TipoMaquina> ReadByIdAsync(Guid id)
	{
		TipoMaquina tipoMaquina = await _dbContext.TiposMaquina.FindAsync(id);

		if (tipoMaquina == null)
			throw new KeyNotFoundException($"No se encontró el tipo de máquina con el id {id}");

		return tipoMaquina;
	}

	public async Task CreateAsync(TipoMaquina tipoMaquina, string user)
	{
		tipoMaquina.DateCreated = DateTime.Now;
		tipoMaquina.CreatedBy = user;

		await _dbContext.AddAsync(tipoMaquina);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(TipoMaquina tipoMaquina, string user)
	{
		TipoMaquina record = await ReadByIdAsync(tipoMaquina.Id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró el tipo de máquina con el id {tipoMaquina.Id}");

		if (!tipoMaquina.Estado)
		{
			bool tieneMaquinasActivas = await _dbContext.Maquinas
				.AnyAsync(m => m.IdTipoMaquina == tipoMaquina.Id && m.Estado);

			if (tieneMaquinasActivas)
				throw new InvalidOperationException("No se puede desactivar el tipo de máquina porque tiene máquinas activas asignadas.");
		}

		record.Nombre = tipoMaquina.Nombre;
		record.Codigo = tipoMaquina.Codigo;
		record.Estado = tipoMaquina.Estado;
		record.DateUpdated = DateTime.Now;
		record.UpdatedBy = user;

		_dbContext.Update(record);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		TipoMaquina record = await ReadByIdAsync(id);

		if (record == null)
			throw new KeyNotFoundException($"No se encontró el tipo de máquina con el id {id}");

		_dbContext.Remove(record);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<string> GenerarCodigoAsync()
	{
		const string prefijo = "TM";

		var ultimoTipo = await _dbContext.TiposMaquina
			.Where(t => t.Codigo.StartsWith(prefijo))
			.OrderByDescending(t => t.Codigo.Length)
			.ThenByDescending(t => t.Codigo)
			.FirstOrDefaultAsync();

		int siguienteNumero = 1;

		if (ultimoTipo != null)
		{
			string numeroStr = ultimoTipo.Codigo.Substring(prefijo.Length);
			if (int.TryParse(numeroStr, out int numeroActual))
			{
				siguienteNumero = numeroActual + 1;
			}
		}

		string formato = siguienteNumero <= 999 ? "D3" : "D0";
		return $"{prefijo}{siguienteNumero.ToString(formato)}";
	}
}
