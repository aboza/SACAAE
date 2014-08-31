using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SACAAE.Models
{
    public class repositorioGrupos
    {
        private SACAAEEntities entidades;

        
        public repositorioGrupos()
        {
            entidades = new SACAAEEntities(); 
        }

        public int ObtenerUltimoNumeroGrupo(int PlanXSedeID, int PeriodoID, int BloqueXPlanXCursoID)
        {
            var vGrupos = from grupos in entidades.Grupoes
                          where grupos.PlanDeEstudio == PlanXSedeID && grupos.Periodo == PeriodoID && grupos.BloqueXPlanXCursoID == BloqueXPlanXCursoID
                          orderby grupos.Numero descending
                           select grupos;
            if (vGrupos.Any())
                return vGrupos.First().Numero;
            else
                return 1;
        }

        //public IQueryable<Grupo> ObtenerTodosGrupos()
        //{
        //    return from Grupo in entidades.Grupoes
        //           orderby Grupo.Nombre
        //           select Grupo;
        //}
        //public IQueryable<Grupo> ListaGrupos(int PlanDeEstudio, int Periodo, int Bloque)
        //{
        //    return from Grupo in entidades.Grupoes
        //           where Grupo.PlanDeEstudio == PlanDeEstudio && Grupo.Bloque == Bloque && Grupo.Periodo == Periodo
        //           select Grupo;
        //}
        
        //public bool existeGrupo(string name, int periodo, int IDplan)
        //{
        //    return (entidades.Grupoes.SingleOrDefault(c => c.Nombre == name && 
        //                                                                c.Periodo == periodo && c.PlanDeEstudio ==IDplan) != null); 
        //}
        //public IQueryable<Grupo> tomarGruposconCondiciones(int plan, int bloque, int periodo)
        //{
        //    return from grupo in entidades.Grupoes
        //           where grupo.PlanDeEstudio == plan &&
        //           grupo.Periodo == periodo &&
        //           grupo.Bloque == bloque
        //           orderby grupo.Nombre
        //           select grupo; 
        //}

        public void agregarGrupo(Grupo pGrupo)
        {
                entidades.Grupoes.Add(pGrupo);
                Save();

        }

        //public void eliminarGrupo(int id)
        //{
        //    var temp = entidades.Grupoes.Find(id);
        //    if (temp != null)
        //    {
        //        entidades.Grupoes.Remove(temp);
        //    }
        //    Save();
        //}


        private void Save()
        {
            entidades.SaveChanges();
        }
    }
}