using System;
using System.Collections.Generic;

namespace fitcare.Models.DataAccess.EntityFramework
{
    public partial class GruposMusculares
    {
        public GruposMusculares()
        {
            GruposMuscularesEjercicio = new HashSet<GruposMuscularesEjercicio>();
        }

        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<GruposMuscularesEjercicio> GruposMuscularesEjercicio { get; set; }
    }
}
