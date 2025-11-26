using System;
using fitcare.Models.Identity;
using fitcare.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models;

public class ApplicationDbContext : DbContext
{
	public virtual DbSet<Provincia> Provincias { get; set; }
	public virtual DbSet<Canton> Cantones { get; set; }
	public virtual DbSet<Distrito> Distritos { get; set; }
	public virtual DbSet<TipoMaquina> TiposMaquina { get; set; }
	public virtual DbSet<Maquina> Maquinas { get; set; }
	public virtual DbSet<TipoEjercicio> TiposEjercicio { get; set; }
	public virtual DbSet<Ejercicio> Ejercicios { get; set; }
	public virtual DbSet<TipoMedida> TiposMedida { get; set; }
	public virtual DbSet<GrupoMuscular> GruposMusculares { get; set; }
	public virtual DbSet<Rutina> Rutinas { get; set; }
	public virtual DbSet<ApplicationUser> Usuarios { get; set; }
	// public virtual DbSet<DetalleMedidas> DetalleMedidas { get; set; }
	// public virtual DbSet<DetalleRutina> DetalleRutina { get; set; }
	// public virtual DbSet<GruposMuscularesEjercicio> GruposMuscularesEjercicio { get; set; }
	
	public ApplicationDbContext() { }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
			optionsBuilder.UseSqlServer("DefaultConnection");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<ApplicationUser>(b =>
		{
			b.ToTable("AspNetUsers", "dbo"); // Remap to table with different name
		});

		// Configuración de relación muchos-a-muchos entre Ejercicio y GrupoMuscular
		modelBuilder.Entity<Ejercicio>()
			.HasMany(e => e.GruposMusculares)
			.WithMany(g => g.Ejercicios)
			.UsingEntity(j =>
			{
				j.ToTable("GruposMuscularesEjercicio", "fitcare");
				j.Property<Guid>("EjerciciosId").HasColumnName("IdEjercicio");
				j.Property<Guid>("GruposMuscularesId").HasColumnName("IdGrupoMuscular");
			});

		// Configuración de relación muchos-a-muchos entre Ejercicio y Maquina
		modelBuilder.Entity<Ejercicio>()
			.HasMany(e => e.Maquinas)
			.WithMany(m => m.Ejercicios)
			.UsingEntity(j =>
			{
				j.ToTable("MaquinasEjercicio", "fitcare");
				j.Property<Guid>("MaquinasId").HasColumnName("IdMaquina");
				j.Property<Guid>("EjerciciosId").HasColumnName("IdEjercicio");
			});
	}
}
