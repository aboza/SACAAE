//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SACAAE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlanesDeEstudioXSede
    {
        public PlanesDeEstudioXSede()
        {
            this.Grupoes = new HashSet<Grupo>();
        }
    
        public int ID { get; set; }
        public int Sede { get; set; }
        public int PlanDeEstudio { get; set; }
    
        public virtual Sede Sede1 { get; set; }
        public virtual PlanesDeEstudio PlanesDeEstudio { get; set; }
        public virtual ICollection<Grupo> Grupoes { get; set; }
    }
}
