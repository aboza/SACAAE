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
    
    public partial class Grupo
    {
        public int ID { get; set; }
        public int Numero { get; set; }
        public int PlanDeEstudio { get; set; }
        public int Periodo { get; set; }
        public int BloqueXPlanXCursoID { get; set; }
    
        public virtual BloqueXPlanXCurso BloqueXPlanXCurso { get; set; }
        public virtual Periodo Periodo1 { get; set; }
        public virtual PlanesDeEstudioXSede PlanesDeEstudioXSede { get; set; }
    }
}
