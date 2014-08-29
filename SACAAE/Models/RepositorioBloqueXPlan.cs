using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SACAAE.Models
{
    public class RepositorioBloqueXPlan
    {
        private SACAAEEntities entidades = new SACAAEEntities();


        public IQueryable<BloqueAcademicoXPlanDeEstudio> ListarBloquesXPlan(int pPlanID)
        {
            return from BloquesXPlan in entidades.BloqueAcademicoXPlanDeEstudios
                   join Planes in entidades.PlanesDeEstudios on BloquesXPlan.PlanID equals Planes.ID
                   join Bloque in entidades.BloqueAcademicoes on BloquesXPlan.BloqueID equals Bloque.ID
                   where Planes.ID == pPlanID
                   select BloquesXPlan;
        }

        public bool existeRelacionBloqueXPlan(int pPlanID, int pBloqueID)
        {
            return (entidades.BloqueAcademicoXPlanDeEstudios.SingleOrDefault(relacion => relacion.PlanID == pPlanID && relacion.BloqueID == pBloqueID) != null);
        }

        public int idBloqueXPlan(int pPlanID,int pBloqueID)
        {
            return (from BloquesXPlan in entidades.BloqueAcademicoXPlanDeEstudios
                    where BloquesXPlan.PlanID == pPlanID && BloquesXPlan.BloqueID == pBloqueID
                    select BloquesXPlan).FirstOrDefault().ID;
        }

        public void crearRelacionBloqueXPlan(BloqueAcademicoXPlanDeEstudio pBloqueXPlan)
        {
            if (existeRelacionBloqueXPlan(pBloqueXPlan.PlanID, pBloqueXPlan.BloqueID))
                return;
            else
            {
                entidades.BloqueAcademicoXPlanDeEstudios.Add(pBloqueXPlan);
                Save();
            }
        }

        private void Save()
        {
            entidades.SaveChanges();
        }

    }
}