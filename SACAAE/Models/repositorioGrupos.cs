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


        public IQueryable<Grupo> ObtenerTodosGrupos()
        {
            return from Grupo in entidades.Grupoes
                   orderby Grupo.Nombre
                   select Grupo;
        }
        public IQueryable<Grupo> ListaGrupos(int PlanDeEstudio, int Periodo, int Bloque)
        {
            return from Grupo in entidades.Grupoes
                   where Grupo.PlanDeEstudio == PlanDeEstudio && Grupo.Bloque == Bloque && Grupo.Periodo == Periodo
                   select Grupo;
        }
        
        public bool existeGrupo(string name, int periodo, int IDplan)
        {
            return (entidades.Grupoes.SingleOrDefault(c => c.Nombre == name && 
                                                                        c.Periodo == periodo && c.PlanDeEstudio ==IDplan) != null); 
        }
        public IQueryable<Grupo> tomarGruposconCondiciones(int plan, int bloque, int periodo)
        {
            return from grupo in entidades.Grupoes
                   where grupo.PlanDeEstudio == plan &&
                   grupo.Periodo == periodo &&
                   grupo.Bloque == bloque
                   orderby grupo.Nombre
                   select grupo; 
        }

        public void guardaGrupo(Grupo grupo, int periodo)
        {

            if (existeGrupo(grupo.Nombre, periodo, grupo.PlanDeEstudio))
                return;
            
            grupo.Periodo = periodo; 
            entidades.Grupoes.Add(grupo);
            Save();

        }

        public void eliminarGrupo(int id)
        {
            var temp = entidades.Grupoes.Find(id);
            if (temp != null)
            {
                entidades.Grupoes.Remove(temp);
            }
            Save();
        }

        
        private void Save()
        {
            entidades.SaveChanges();
        }
    }
}