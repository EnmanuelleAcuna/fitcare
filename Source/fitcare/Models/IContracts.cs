using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fitcare.Models.Entities;

namespace fitcare.Models.Contracts;

public interface IManager<T>
{
	Task<IList<T>> ReadAllAsync();
	Task<T> ReadByIdAsync(Guid id);
	Task CreateAsync(T model);
	Task UpdateAsync(T model);
	Task DeleteAsync(Guid id);
}

public interface IDivisionTerritorialManager
{
	IManager<Provincia> Provincias { get; }
	IManager<Canton> Cantones { get; }
	IManager<Distrito> Distritos { get; }
}
