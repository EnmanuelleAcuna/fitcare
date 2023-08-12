using System;
using System.Collections.Generic;

namespace fitcare.Models.DataAccess.EntityFramework
{
    public partial class DetalleRutina
    {
        public Guid Id { get; set; }
        public Guid IdRutina { get; set; }
        public Guid IdEjercicio { get; set; }
        public Guid? IdMaquina { get; set; }
        public int NumSeries { get; set; }
        public int NumRepeticiones { get; set; }
        public int MinutosDescanso { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Ejercicios IdEjercicioNavigation { get; set; }
        public virtual Maquinas IdMaquinaNavigation { get; set; }
        public virtual Rutinas IdRutinaNavigation { get; set; }
    }
}
