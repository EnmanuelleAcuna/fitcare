using Microsoft.EntityFrameworkCore;

namespace fitcare.Models.Entities;

public partial class FitcareDBContext : DbContext
{
	public FitcareDBContext() { }

	public FitcareDBContext(DbContextOptions<FitcareDBContext> options) : base(options) { }

	public virtual DbSet<Provincia> Provincias { get; set; }
	public virtual DbSet<Canton> Cantones { get; set; }
	public virtual DbSet<Distrito> Distritos { get; set; }
	public virtual DbSet<Contacto> Contactos { get; set; }
	// public virtual DbSet<DetalleMedidas> DetalleMedidas { get; set; }
	// public virtual DbSet<DetalleRutina> DetalleRutina { get; set; }
	// public virtual DbSet<Ejercicios> Ejercicios { get; set; }
	// public virtual DbSet<GruposMusculares> GruposMusculares { get; set; }
	// public virtual DbSet<GruposMuscularesEjercicio> GruposMuscularesEjercicio { get; set; }
	// public virtual DbSet<Maquinas> Maquinas { get; set; }
	// public virtual DbSet<Rutinas> Rutinas { get; set; }
	// public virtual DbSet<TiposEjercicio> TiposEjercicio { get; set; }
	// public virtual DbSet<TiposMaquina> TiposMaquina { get; set; }
	// public virtual DbSet<TiposMedida> TiposMedida { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
			optionsBuilder.UseSqlServer("DefaultConnection");
	}
}
