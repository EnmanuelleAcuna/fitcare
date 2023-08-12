using System;
using System.Collections.Generic;

namespace fitcare.Models.DataAccess.EntityFramework
{
    public partial class GruposMuscularesEjercicio
    {
        public Guid Id { get; set; }
        public Guid IdEjercicio { get; set; }
        public Guid IdGrupoMuscular { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Ejercicios IdEjercicioNavigation { get; set; }
        public virtual GruposMusculares IdGrupoMuscularNavigation { get; set; }
    }
}
