using System;
using System.Collections.Generic;

namespace fitcare.Models.DataAccess.EntityFramework
{
    public partial class Maquinas
    {
        public Maquinas()
        {
            DetalleRutina = new HashSet<DetalleRutina>();
        }

        public Guid Id { get; set; }
        public Guid IdTipoMaquina { get; set; }
        public string Codigo { get; set; }
        public string NumeroActivo { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual TiposMaquina IdTipoMaquinaNavigation { get; set; }
        public virtual ICollection<DetalleRutina> DetalleRutina { get; set; }
    }
}
