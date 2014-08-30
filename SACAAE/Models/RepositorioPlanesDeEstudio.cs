using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SACAAE.Models
{
    public class RepositorioPlanesDeEstudio
    {
        private SACAAEEntities entidades = new SACAAEEntities();

        public IQueryable<PlanesDeEstudio> ObtenerTodosPlanesDeEstudio()
        {
            return from PlanesDeEstudio in entidades.PlanesDeEstudios
                   orderby PlanesDeEstudio.ID
                   select PlanesDeEstudio;
        }

        public IQueryable<PlanesDeEstudio> ObtenerUnPlanDeEstudio(int plan)
        {
            return from PlanesDeEstudio in entidades.PlanesDeEstudios
                   where PlanesDeEstudio.ID==plan
                   select PlanesDeEstudio;
        }

        public void agregarPlan(PlanesDeEstudio Plan)
        {
            entidades.PlanesDeEstudios.Add(Plan);
            Save();
        }

        private void Save()
        {
            entidades.SaveChanges();
        }
        public PlanesDeEstudio existe(string nombre, int modalidad)
        {
            return (from PlanesDeEstudio in entidades.PlanesDeEstudios
                    where PlanesDeEstudio.Nombre == nombre && PlanesDeEstudio.Modalidad== modalidad
                    select PlanesDeEstudio).FirstOrDefault();
        }
        public int IdPlanDeEstudioPorIdModalidad(String Nombre, int IdModalidad)
        {
            IQueryable<PlanesDeEstudio> result = from PlanesDeEstudio in entidades.PlanesDeEstudios
                   orderby PlanesDeEstudio.Nombre
                   where PlanesDeEstudio.Nombre == Nombre && PlanesDeEstudio.Modalidad == IdModalidad
                   select PlanesDeEstudio;
            try
            {
                return result.FirstOrDefault().ID;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        
        public int IdPlanDeEstudio(String Nombre, string Modalidad)
        {
            int IdModalidad= (from Modalidade in entidades.Modalidades
                    where Modalidade.Nombre == Modalidad
                    select Modalidade).FirstOrDefault().ID;

            IQueryable<PlanesDeEstudio> result = from PlanesDeEstudio in entidades.PlanesDeEstudios
                   orderby PlanesDeEstudio.Nombre
                   where PlanesDeEstudio.Nombre == Nombre && PlanesDeEstudio.Modalidad == IdModalidad
                   select PlanesDeEstudio;
            try
            {
                return result.FirstOrDefault().ID;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int IdPlanDeEstudioXSede(String Nombre, String Modalidad,String Sede)
        {
            int IdModalidad = (from Modalidade in entidades.Modalidades
                               where Modalidade.Nombre == Modalidad
                               select Modalidade).FirstOrDefault().ID;
            int IdSede = (from Sedes in entidades.Sedes
                          where Sedes.Nombre == Sede
                          select Sedes).FirstOrDefault().ID;

            int IdPlanDeEstudio = this.IdPlanDeEstudio(Nombre, Modalidad);

            IQueryable<PlanesDeEstudioXSede> result = from PlanesDeEstudioXSede in entidades.PlanesDeEstudioXSedes
                                                      where PlanesDeEstudioXSede.Sede==IdSede && PlanesDeEstudioXSede.PlanDeEstudio==IdPlanDeEstudio
                                                      select PlanesDeEstudioXSede;
            try
            {
                return result.FirstOrDefault().ID;
            }
            catch(Exception e)
            {
                return -1;
            }
        }


    }
}