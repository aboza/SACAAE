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

            Save();
        }

        private void AgregarPlazaProfesor(PlazaXProfesor asignarPlaza)
        {
            entidades.PlazaXProfesors.Add(asignarPlaza);
        }

        public void Save()
        {
            entidades.SaveChanges();
        }
    }
}