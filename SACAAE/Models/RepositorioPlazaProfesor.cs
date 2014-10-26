using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SACAAE.Models
{
    public class RepositorioPlazaProfesor
    {
        private SACAAEEntities entidades = new SACAAEEntities();

        public void AsignarPlaza(string codigoPlaza, string codigoProfesor, int? horasAsignadas)
        {
            // if (string.IsNullOrEmpty(codigoPlaza.Trim()))
            //   throw new ArgumentException("El codigo de la plaza no es válido. Por favor, intente de nuevo.");

           
            RepositorioProfesor repoProfesor = new RepositorioProfesor();
            RepositorioPlazas repoPlaza = new RepositorioPlazas();

            var IDProfesor = repoProfesor.ObtenerProfesor(Int16.Parse(codigoProfesor));
            var IDPlaza = repoPlaza.ObtenerPlaza(Int16.Parse(codigoPlaza));

            PlazaXProfesor asignarPlaza = new PlazaXProfesor()
            {
                Plaza = Int16.Parse(IDPlaza.ID.ToString()),
                Profesor = Int16.Parse(IDProfesor.ID.ToString()),
                Horas_Asignadas = horasAsignadas
            };
            if (!ExistePlaza(asignarPlaza))
            {
                try
                {
                    AgregarPlazaProfesor(asignarPlaza);
                }
                catch (ArgumentException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    throw new ArgumentException("El proveedor de autenticación retornó un error. Por favor, intente de nuevo. " +
                        "Si el problema persiste, por favor contacte un administrador.\n" + e.Message);
                }
            }
            else
            {
                asignarPlaza = repoPlaza.ObtenerPlazaXProfesor(IDPlaza.ID, IDProfesor.ID);
                Actualizar(asignarPlaza);
            }

            Save();
        }

        private void AgregarPlazaProfesor(PlazaXProfesor asignarPlaza)
        {
            entidades.PlazaXProfesors.Add(asignarPlaza);
        }

        public bool ExistePlaza(PlazaXProfesor plazaXProfesor)
        {
            if (plazaXProfesor == null)
                return false;
            return (entidades.PlazaXProfesors.SingleOrDefault(p => p.Plaza == plazaXProfesor.Plaza && p.Profesor==plazaXProfesor.Profesor) != null);
        }

        public void Actualizar(PlazaXProfesor plaza)
        {            

            var temp = entidades.PlazaXProfesors.Find(plaza.ID);

            if (temp != null)
            {
                entidades.Entry(temp).Property(p => p.Horas_Asignadas).CurrentValue += plaza.Horas_Asignadas;
            }

            Save();

        }
        public void Save()
        {
            entidades.SaveChanges();
        }
    }
}