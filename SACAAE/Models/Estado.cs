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
    
    public partial class Estado
    {
        public Estado()
        {
            this.Comisiones = new HashSet<Comisione>();
            this.Proyectos = new HashSet<Proyecto>();
            this.Profesores = new HashSet<Profesores>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<Comisione> Comisiones { get; set; }
        public virtual ICollection<Proyecto> Proyectos { get; set; }
        public virtual ICollection<Profesores> Profesores { get; set; }
    }
}
