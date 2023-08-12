using System;
using System.Collections.Generic;

namespace fitcare.Models.DataAccess.EntityFramework
{
    public partial class DetalleMedidas
    {
        public Guid Id { get; set; }
        public Guid IdRutina { get; set; }
        public Guid IdTipoMedida { get; set; }
        public string ValorMedida { get; set; }
        public string Comentario { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Rutinas IdRutinaNavigation { get; set; }
        public virtual TiposMedida IdTipoMedidaNavigation { get; set; }
    }
}
