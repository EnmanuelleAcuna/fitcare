// using fitcare.Models.Contracts;
// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using System;
// using System.Threading.Tasks;

// namespace fitcare.Web.Models.Services
// {
// 	public class LCliente
// 	{
// 		private readonly IDataCRUDBase<Cliente> _repoClientes;

// 		public LCliente(IDataCRUDBase<Cliente> repoClientes)
// 		{
// 			_repoClientes = repoClientes;
// 		}

// 		public async Task CreateAsync(Cliente cliente)
// 		{
// 			Validators.ValidateCliente(cliente);

// 			Guid idCliente = Factory.SetGuid(cliente.Id);

// 			// Guardar imagen del cliente
// 			//await _archivos.AgregarImagenAsync(idCliente, archivoFoto, "Clientes", bitacora);

// 			cliente.Inscribir();

// 			await _repoClientes.CreateAsync(cliente);

// 			//if (!clienteAgregado)
// 			//{
// 			//	await _archivos.EliminarImagenAsync(idCliente, bitacora.SetAccionEliminar());
// 			//}
// 		}

// 		public async Task UpdateAsync(Cliente cliente)
// 		{
// 			Validators.ValidateCliente(cliente);

// 			await _repoClientes.UpdateAsync(cliente);

// 			//await _archivos.ActualizarImagenAsync(Factory.SetGuid(cliente.Id), archivoFoto, "Clientes", bitacora);
// 		}

// 		public async Task DeleteAsync(Guid id, Bitacora bitacora)
// 		{
// 			await _repoClientes.DeleteAsync(id);
// 			//await _archivos.EliminarImagenAsync(id, bitacora);
// 		}
// 	}
// }
