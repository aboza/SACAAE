//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SACAAE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BloqueXPlanXCurso
    {
        public BloqueXPlanXCurso()
        {
            this.Grupoes = new HashSet<Grupo>();
        }
    
        public int ID { get; set; }
        public int BloqueXPlanID { get; set; }
        public int CursoID { get; set; }
    
        public virtual BloqueAcademicoXPlanDeEstudio BloqueAcademicoXPlanDeEstudio { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual ICollection<Grupo> Grupoes { get; set; }
    }
}
