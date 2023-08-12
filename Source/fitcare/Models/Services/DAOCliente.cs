// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Threading.Tasks;
// using fitcare.Models.Contracts;
// using fitcare.Models.DataAccess.EntityFramework;
// using fitcare.Models.Entities;
// using Microsoft.EntityFrameworkCore;

// namespace fitcare.Models.DataAccess;

// public class DAOCliente : IRepository<Cliente>
// {
// 	private readonly Fitcare_DB_Context _dbContext;

// 	public DAOCliente(Fitcare_DB_Context dbContext)
// 	{
// 		_dbContext = dbContext;
// 	}

// 	public async Task<IList<Cliente>> ReadAllAsync()
// 	{
// 		IList<Clientes> listaClientesBD = await _dbContext.Clientes.Include(x => x.IdDistritoNavigation)
// 																   .ToListAsync();
// 		return listaClientesBD.Select(x => x.ConvertDBModelToDomain()).ToList();
// 	}

// 	public async Task<Cliente> ReadByIdAsync(Guid id)
// 	{
// 		Clientes clienteBD = await _dbContext.Clientes.Where(c => c.IdUsuario.Equals(id.ToString()))
// 													  .Include(d => d.IdDistritoNavigation)
// 													  .FirstOrDefaultAsync();
// 		return clienteBD.ConvertDBModelToDomain();
// 	}

// 	public async Task CreateAsync(Cliente cliente)
// 	{
// 		Clientes clienteBD = new(cliente);
// 		_dbContext.Clientes.Add(clienteBD);
// 		_dbContext.Entry(clienteBD).State = EntityState.Added;
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task UpdateAsync(Cliente cliente)
// 	{
// 		Clientes clienteBD = _dbContext.Clientes.Find(cliente.Id);

// 		// Actualizar campos necesarios en BD
// 		clienteBD.FechaInscripcion = cliente.FechaInscripcion;
// 		clienteBD.FechaRenovacion = cliente.FechaRenovacion;
// 		clienteBD.IdDistrito = cliente.Distrito.Id;

// 		_dbContext.Clientes.Update(clienteBD);
// 		_dbContext.Entry(clienteBD).State = EntityState.Modified;
// 		await _dbContext.SaveChangesAsync();
// 	}

// 	public async Task DeleteAsync(Guid id)
// 	{
// 		Clientes clienteBD = await _dbContext.Clientes.FindAsync(id);

// 		_dbContext.Clientes.Remove(clienteBD);
// 		_dbContext.Entry(clienteBD).State = EntityState.Deleted;
// 		await _dbContext.SaveChangesAsync();
// 	}
// }
