using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fitcare.Models.Entities;

namespace fitcare.Models;

public interface IBaseCore<T>
{
	Task<IList<T>> ReadAllAsync();
	Task<T> ReadByIdAsync(Guid id);
	Task CreateAsync(T model, string user);
	Task UpdateAsync(T model, string user);
	Task DeleteAsync(Guid id);
}

public interface IContactos<T>
{
	Task<IList<T>> ReadAllAsync();
	Task<T> ReadByIdAsync(Guid id);
	Task CreateAsync(T model);
	Task DeleteAsync(Guid id);
}

public interface IDivisionTerritorial
{
	IBaseCore<Provincia> Provincias { get; }
	IBaseCore<Canton> Cantones { get; }
	IBaseCore<Distrito> Distritos { get; }
}

public interface IRutinas<T>
{
	Task<IList<T>> ReadAllAsync();
	Task<T> ReadByIdAsync(Guid id);
	Task CreateAsync(T model, string user);

	Task<IList<Rutina>> ObtenerReporteRutinas(string idInstructor, string idCliente);

	Task AgregarEjercicioAsync(Guid idRutina, EjercicioRutina ejercicio, string user);
	Task EditarEjercicioAsync(Guid idEjercicioRutina, Guid idEjercicio, int series, int repeticiones, int minutosDescanso, string user);
	Task EliminarEjercicioAsync(Guid idEjercicioRutina);

	Task AgregarMedidaAsync(Guid idRutina, MedidaRutina medida, string user);
	Task EditarMedidaAsync(Guid idMedidaRutina, Guid idTipoMedida, string valor, string comentario, string user);
	Task EliminarMedidaAsync(Guid idMedidaRutina);

	// Exportación
	Task<IList<Rutina>> ObtenerRutinasParaExportarAsync(string idInstructor, string idCliente);
	byte[] ExportarRutinasExcel(IList<Rutina> rutinas);
}
