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
                   orderby PlanesDeEstudio.Nombre
                   select PlanesDeEstudio;
        }

        public int  IdPlanDeEstudio(String Nombre, string Modalidad)
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