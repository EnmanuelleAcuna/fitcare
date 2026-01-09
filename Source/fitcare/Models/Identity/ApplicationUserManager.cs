using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace fitcare.Models.Identity;

public class ApplicationUserManager<TUser> : UserManager<ApplicationUser>
{
	private readonly IUserStore<ApplicationUser> _store;
	private readonly RoleManager<ApplicationRole> _roleManager;
	private readonly IDivisionTerritorial _divisionTerritorial;

	public ApplicationUserManager(IUserStore<ApplicationUser> store,
								  IOptions<IdentityOptions> optionsAccessor,
								  IPasswordHasher<ApplicationUser> passwordHasher,
								  IEnumerable<IUserValidator<ApplicationUser>> userValidators,
								  IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
								  ILookupNormalizer keyNormalizer,
								  IdentityErrorDescriber errors,
								  IServiceProvider services,
								  ILogger<UserManager<ApplicationUser>> logger,
								  RoleManager<ApplicationRole> roleManager,
								  IDivisionTerritorial divisionTerritorial)
								  : base(store,
										 optionsAccessor,
									 	 passwordHasher,
									 	 userValidators,
									 	 passwordValidators,
									 	 keyNormalizer,
									 	 errors,
									 	 services,
									 	 logger)
	{
		_store = store;
		_roleManager = roleManager;
		_divisionTerritorial = divisionTerritorial;
	}

	public async Task<IdentityResult> UpdateLastSession(ApplicationUser user)
	{
		user.SetLastSession(DateTime.Now);
		IdentityResult result = await _store.UpdateAsync(user, CancellationToken);
		return result;
	}

	public async Task<IdentityResult> UpdatePersonalInformation(ApplicationUser user)
	{
		var userRecord = await FindByIdAsync(user.Id);

		if (userRecord == null)
			throw new KeyNotFoundException($"No user was found with the id {user.Id}");

		userRecord.SetNewPersonalInformation(user.Name, user.FirstLastName, user.SecondLastName, user.IdentificationNumber, user.Active.Value);

		IdentityResult result = await UpdateAsync(userRecord);
		return result;
	}

	public async Task<IdentityResult> ActualizarRolesUsuario(ApplicationUser user, IEnumerable<string> roles)
	{
		var userRecord = await FindByIdAsync(user.Id);

		if (userRecord == null)
			throw new KeyNotFoundException($"No user was found with the id {user.Id}");

		IList<string> actualRoles = await GetRolesAsync(userRecord);

		IdentityResult rolesUnassigned = await RemoveFromRolesAsync(userRecord, actualRoles);

		if (!rolesUnassigned.Succeeded) return rolesUnassigned;

		IdentityResult rolesAssigned = await AddToRolesAsync(userRecord, roles);

		return rolesAssigned;
	}

	public async Task<IdentityResult> RegistrarUsuarioComoInstructor(ApplicationUser user, string filePath)
	{
		var userRecord = await FindByIdAsync(user.Id);

		if (userRecord == null)
			throw new KeyNotFoundException($"No user was found with the id {user.Id}");

		IList<string> roles = await GetRolesAsync(user);

		bool isInstructor = roles.Any(role => role.Equals("Instructor", StringComparison.OrdinalIgnoreCase));

		if (!isInstructor)
		{
			roles.Add("Instructor");
			var rolesActualizados = await ActualizarRolesUsuario(user, roles);
			if (!rolesActualizados.Succeeded) return rolesActualizados;
		}

		userRecord.IdProvincia = user.IdProvincia;
		userRecord.IdCanton = user.IdCanton;
		userRecord.IdDistrito = user.IdDistrito;
		userRecord.URLFotografia = filePath;
		userRecord.FechaIngresoInscripcion = user.FechaIngresoInscripcion;

		IdentityResult result = await UpdateAsync(userRecord);
		return result;
	}

	public async Task<IdentityResult> RegistrarUsuarioComoCliente(ApplicationUser user, string filePath)
	{
		var userRecord = await FindByIdAsync(user.Id);

		if (userRecord == null)
			throw new KeyNotFoundException($"No user was found with the id {user.Id}");

		IList<string> roles = await GetRolesAsync(user);

		bool isCliente = roles.Any(role => role.Equals("Cliente", StringComparison.OrdinalIgnoreCase));

		if (!isCliente)
		{
			roles.Add("Cliente");
			var rolesActualizados = await ActualizarRolesUsuario(user, roles);
			if (!rolesActualizados.Succeeded) return rolesActualizados;
		}

		userRecord.IdProvincia = user.IdProvincia;
		userRecord.IdCanton = user.IdCanton;
		userRecord.IdDistrito = user.IdDistrito;
		userRecord.URLFotografia = filePath;
		userRecord.FechaIngresoInscripcion = user.FechaIngresoInscripcion;
		userRecord.FechaRenovacion = user.FechaRenovacion;

		IdentityResult result = await UpdateAsync(userRecord);
		return result;
	}

	public async Task<IList<ApplicationUser>> GetUsersNotInRoleAsync(string roleName)
	{
		if (await _roleManager.FindByNameAsync(roleName) == null)
			throw new InvalidOperationException($"Rol '{roleName}' no encontrado.");

		var usersInRole = await GetUsersInRoleAsync(roleName);
		var allUsers = await Users.ToListAsync();
		var usersNotInRole = allUsers.Except(usersInRole).ToList();
		return usersNotInRole;
	}

	public async Task<IList<ApplicationUser>> GetUsersInRoleWithDivisionTerritorialInfoAsync(string roleName)
	{
		if (await _roleManager.FindByNameAsync(roleName) == null)
			throw new InvalidOperationException($"Rol '{roleName}' no encontrado.");

		var usersInRole = await GetUsersInRoleAsync(roleName);

		foreach(var user in usersInRole)
		{
			if (user.IdProvincia != null)
			{
				var provincia = await _divisionTerritorial.Provincias.ReadByIdAsync(new Guid(user.IdProvincia?.ToString()));

				if (provincia != null)
					user.Provincia = provincia;
			}

			if (user.IdCanton != null)
			{
				var canton = await _divisionTerritorial.Cantones.ReadByIdAsync(new Guid(user.IdCanton?.ToString()));

				if (canton != null)
					user.Canton = canton;
			}

			if (user.IdDistrito != null)
			{
				var distrito = await _divisionTerritorial.Distritos.ReadByIdAsync(new Guid(user.IdDistrito?.ToString()));

				if (distrito != null)
					user.Distrito = distrito;
			}
		}

		return usersInRole;
	}

	public async Task<IdentityResult> DesafiliarUsuarioComoCliente(string userId)
	{
		var user = await FindByIdAsync(userId);

		if (user == null)
			return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado" });

		var isCliente = await IsInRoleAsync(user, "Cliente");

		if (!isCliente)
			return IdentityResult.Failed(new IdentityError { Description = "El usuario no está afiliado como cliente" });

		return await RemoveFromRoleAsync(user, "Cliente");
	}

	public async Task<IdentityResult> DesafiliarUsuarioComoInstructor(string userId)
	{
		var user = await FindByIdAsync(userId);

		if (user == null)
			return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado" });

		var isInstructor = await IsInRoleAsync(user, "Instructor");

		if (!isInstructor)
			return IdentityResult.Failed(new IdentityError { Description = "El usuario no está afiliado como instructor" });

		return await RemoveFromRoleAsync(user, "Instructor");
	}

	public async Task<IdentityResult> ActualizarDatosCliente(ApplicationUser user)
	{
		var userRecord = await FindByIdAsync(user.Id);

		if (userRecord == null)
			return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado" });

		userRecord.IdProvincia = user.IdProvincia;
		userRecord.IdCanton = user.IdCanton;
		userRecord.IdDistrito = user.IdDistrito;
		userRecord.FechaIngresoInscripcion = user.FechaIngresoInscripcion;
		userRecord.FechaRenovacion = user.FechaRenovacion;

		return await UpdateAsync(userRecord);
	}

	public async Task<IdentityResult> ActualizarDatosInstructor(ApplicationUser user)
	{
		var userRecord = await FindByIdAsync(user.Id);

		if (userRecord == null)
			return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado" });

		userRecord.IdProvincia = user.IdProvincia;
		userRecord.IdCanton = user.IdCanton;
		userRecord.IdDistrito = user.IdDistrito;
		userRecord.FechaIngresoInscripcion = user.FechaIngresoInscripcion;

		return await UpdateAsync(userRecord);
	}
}
