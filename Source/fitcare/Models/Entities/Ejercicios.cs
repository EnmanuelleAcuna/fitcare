using System;
using System.Collections.Generic;

namespace fitcare.Models.DataAccess.EntityFramework
{
    public partial class Ejercicios
    {
        public Ejercicios()
        {
            DetalleRutina = new HashSet<DetalleRutina>();
            GruposMuscularesEjercicio = new HashSet<GruposMuscularesEjercicio>();
        }

        public Guid Id { get; set; }
        public Guid IdTipoEjercicio { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual TiposEjercicio IdTipoEjercicioNavigation { get; set; }
        public virtual ICollection<DetalleRutina> DetalleRutina { get; set; }
        public virtual ICollection<GruposMuscularesEjercicio> GruposMuscularesEjercicio { get; set; }
    }
}
