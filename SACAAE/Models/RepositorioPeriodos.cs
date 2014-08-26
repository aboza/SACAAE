using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SACAAE.Models
{
    public class RepositorioPeriodos
    {
        private SACAAEEntities entidades = new SACAAEEntities();

        public IQueryable<Periodo> ListaPeriodos()
        {
            return from Periodos in entidades.Periodos
                   select Periodos;
        }
        public int IdPeriodo(string nombre)
        {
            return (from Periodos in entidades.Periodos
                   where Periodos.Nombre==nombre
                   select Periodos).FirstOrDefault().ID;
        }

        public Periodo existe (string nombre)
        {
            return (from Periodos in entidades.Periodos
                    where Periodos.Nombre == nombre
                    select Periodos).FirstOrDefault();
        }


        public void  agregarPeriodo(Periodo Nombre){
            entidades.Periodos.Add(Nombre);
            Save(); 
        }

        private void Save()
        {
            entidades.SaveChanges();
        }

    }
}