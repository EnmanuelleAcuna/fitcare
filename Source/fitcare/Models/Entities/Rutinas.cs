using System;
using System.Collections.Generic;
using fitcare.Models.Identity;

namespace fitcare.Models.DataAccess.EntityFramework
{
	public partial class Rutinas
	{
		public Rutinas()
		{
			DetalleMedidas = new HashSet<DetalleMedidas>();
			DetalleRutina = new HashSet<DetalleRutina>();
		}

		public Guid Id { get; set; }
		public Guid IdInstructor { get; set; }
		public string IdCliente { get; set; }
		public DateTime FechaRealizacion { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public string Objetivo { get; set; }
		public DateTime DateCreated { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? DateUpdated { get; set; }
		public string UpdatedBy { get; set; }

		public virtual ApplicationUser Usuarios { get; set; }
		public virtual ICollection<DetalleMedidas> DetalleMedidas { get; set; }
		public virtual ICollection<DetalleRutina> DetalleRutina { get; set; }
	}
}
