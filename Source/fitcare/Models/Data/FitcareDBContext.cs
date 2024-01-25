using Microsoft.EntityFrameworkCore;

namespace fitcare.Models.Entities;

public class FitcareDBContext : DbContext
{
	public FitcareDBContext() { }

	public FitcareDBContext(DbContextOptions<FitcareDBContext> options) : base(options) { }

	public virtual DbSet<Provincia> Provincias { get; set; }
	public virtual DbSet<Canton> Cantones { get; set; }
	public virtual DbSet<Distrito> Distritos { get; set; }
	public virtual DbSet<Contacto> Contactos { get; set; }
	public virtual DbSet<TipoMaquina> TiposMaquina { get; set; }
	public virtual DbSet<Maquina> Maquinas { get; set; }
	public virtual DbSet<TipoEjercicio> TiposEjercicio { get; set; }
	public virtual DbSet<Ejercicio> Ejercicios { get; set; }
	public virtual DbSet<TipoMedida> TiposMedida { get; set; }
	public virtual DbSet<GrupoMuscular> GruposMusculares { get; set; }
	public virtual DbSet<Rutina> Rutinas { get; set; }
	// public virtual DbSet<DetalleMedidas> DetalleMedidas { get; set; }
	// public virtual DbSet<DetalleRutina> DetalleRutina { get; set; }
	// public virtual DbSet<GruposMuscularesEjercicio> GruposMuscularesEjercicio { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
			optionsBuilder.UseSqlServer("DefaultConnection");
	}
}
