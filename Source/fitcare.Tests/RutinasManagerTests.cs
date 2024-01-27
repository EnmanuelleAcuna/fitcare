using System;
using System.Threading.Tasks;
using fitcare.Models;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace fitcare.Tests;

public class RutinasManagerTests
{
	// [Fact]
	// public void ShouldCreateRutina()
	// {
	// 	Rutina rutina = new Rutina();

	// 	FitcareDBContext db = new FitcareDBContext();
	// 	IManager<Rutina> rutinasManager = new RutinasManager(db);
	// }

	[Fact]
	public async Task CreateAsync_Should_AddRutinaToDbContext()
	{
		// Arrange
		var dbContextOptions = new DbContextOptionsBuilder<FitcareDBContext>()
			.UseInMemoryDatabase(databaseName: "InMemoryDatabase")
			.ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;

		using (var dbContext = new FitcareDBContext(dbContextOptions))
		{
			var rutinasManager = new RutinasManager(dbContext);

			var rutina = new Rutina()
			{
				Id = Guid.NewGuid(),
				FechaRealizacion = DateTime.Now,
				FechaInicio = DateTime.Now,
				FechaFin = DateTime.Now,
				Objetivo = "Prueba",
				IdInstructor = "2e887a63-0635-45bd-812f-ef1a44759c1d",
				IdCliente = "dfcbbdf8-6980-4185-8527-333839ed7ec0"
			};

			await rutinasManager.CreateAsync(rutina, "testUser");
			var existingRutina = await dbContext.Rutinas.FirstOrDefaultAsync(r => r.Id == rutina.Id);

			Assert.Single(dbContext.Rutinas);
			Assert.Equal(rutina.Id.ToString(), existingRutina.Id.ToString());
		}
	}
}
