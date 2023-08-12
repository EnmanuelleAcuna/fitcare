﻿using System;
using System.Collections.Generic;

namespace fitcare.Models.DataAccess.EntityFramework
{
    public partial class TiposEjercicio
    {
        public TiposEjercicio()
        {
            Ejercicios = new HashSet<Ejercicios>();
        }

        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Ejercicios> Ejercicios { get; set; }
    }
}
