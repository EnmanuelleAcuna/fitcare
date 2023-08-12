// using fitcare.Models.Contracts;
// using fitcare.Models.Entities;
// using fitcare.Models.Extras;
// using System;
// using System.Threading.Tasks;

// namespace fitcare.Web.Models.Services
// {
// 	public class LInstructor
// 	{
// 		private readonly IDataCRUDBase<Instructor> _repoInstructores;

// 		public LInstructor(IDataCRUDBase<Instructor> repoInstructores)
// 		{
// 			_repoInstructores = repoInstructores;
// 		}

// 		public async Task CreateAsync(Instructor instructor)
// 		{
// 			Validators.ValidateInstructor(instructor);
// 			//Validators.ValidateArchivoSubido(archivoFoto);

// 			Guid idInstructor = Factory.SetGuid(instructor.Id);

// 			//await _archivos.AgregarImagenAsync(idInstructor, archivoFoto, "Instructores", bitacora);

// 			await _repoInstructores.CreateAsync(instructor);

// 			//if (!instructorAgregado)
// 			//{
// 			//	await _archivos.EliminarImagenAsync(idInstructor, bitacora.SetAccionEliminar());
// 			//}
// 		}

// 		public async Task UpdateAsync(Instructor instructor)
// 		{
// 			Validators.ValidateInstructor(instructor);

// 			await _repoInstructores.UpdateAsync(instructor);

// 			//await _archivos.ActualizarImagenAsync(Factory.SetGuid(instructor.Id), archivoFoto, "Instructores", bitacora);
// 		}

// 		public async Task DeleteAsync(Guid id, Bitacora bitacora)
// 		{
// 			await _repoInstructores.DeleteAsync(id);

// 			//await _archivos.EliminarImagenAsync(id, bitacora);
// 		}
// 	}
// }
