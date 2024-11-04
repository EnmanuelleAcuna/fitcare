using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fitcare.Models.Entities;
using fitcare.Models.ViewModels;

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
	
	IList<ReporteRutinaResumido> ObtenerReporteRutinasResumido(string idInstructor, string idCliente);
}
