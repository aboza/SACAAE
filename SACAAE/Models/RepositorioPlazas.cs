using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SACAAE.Models
{
    public class RepositorioPlazas
    {
        private SACAAEEntities entidades = new SACAAEEntities();

        private const string FaltaPlaza = "Plaza no existe";
        private const string MuchoPlaza = "Plaza ya existe";


        public int NumeroPlazas
        {
            get
            {
                return this.entidades.Plazas.Count();
            }
        }

        public RepositorioPlazas()
        {
            this.entidades = new SACAAEEntities();
        }


        public IQueryable<Plaza> ObtenerTodasPlazas()
        {
            return from plaza in entidades.Plazas
                   orderby plaza.Codigo_Plaza
                   select plaza;
        }

        /// <summary>
        /// Se obtiene una plaza de acuerdo a un Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Plaza ObtenerPlaza(int id)
        {
            return entidades.Plazas.SingleOrDefault(plaza => plaza.ID == id);
        }

        /// <summary>
        /// Se obtiene una plaza de acuerdo a su nombre.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        /// 
        public IQueryable<PlazaXProfesor> ObtenerPlazaXProfesor(int Plaza)
        {
            return from PlazaXProfesor in entidades.PlazaXProfesors
                   where PlazaXProfesor.Plaza == Plaza
                   select PlazaXProfesor;
        }

        public IQueryable<Profesores> ObtenerProfesoresPlaza()
        {
            return from profesor in entidades.Profesores
                   orderby profesor.Nombre                  
                   select profesor;

          
        }

        public Plaza ObtenerPlaza(string codigo)
        {
            return entidades.Plazas.SingleOrDefault(plaza => plaza.Codigo_Plaza == codigo);
        }


        private void AgregarPlaza(Plaza plaza)
        {
            if (ExistePlaza(plaza))
                throw new ArgumentException(MuchoPlaza);

            entidades.Plazas.Add(plaza);
        }

        public void CrearPlaza(string codigo, string tipo_plaza, string tipo_tiempo, int? horas_totales, int? tiempo_total_vigencia)
        {
            if (string.IsNullOrEmpty(codigo.Trim()))
                throw new ArgumentException("El codigo de la plaza no es válido. Por favor, intente de nuevo.");

            Plaza plazaNueva = new Plaza()
            {
                Codigo_Plaza = codigo,
                Tipo_de_plaza = tipo_plaza,
                Tipo_segun_tiempo = tipo_tiempo,
                Horas_Totales = horas_totales,
                Tiempo_de_vigencia = tiempo_total_vigencia
            };

            try
            {
                AgregarPlaza(plazaNueva);
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

        /// <summary>
        /// Comprueba que exista una plaza determinado.
        /// </summary>
        /// <param name="plaza"></param>
        /// <returns></returns>
        public bool ExistePlaza(Plaza plaza)
        {
            if (plaza == null)
                return false;
            return (entidades.Plazas.SingleOrDefault(p => p.ID == plaza.ID ||
                p.Codigo_Plaza == plaza.Codigo_Plaza) != null);
        }

        public void Save()
        {
            entidades.SaveChanges();
        }

        public void BorrarPlaza(Plaza plaza)
        {
            if (!ExistePlaza(plaza))
                throw new ArgumentException(FaltaPlaza);

            var temp = entidades.Plazas.Find(plaza.ID);
            if (temp != null)
            {
                entidades.Plazas.Remove(temp);
                Save();
            }
            // Save();
        }

        public void BorrarPlaza(String codigo)
        {
            BorrarPlaza(ObtenerPlaza(codigo));
        }

        public void Actualizar(Plaza plaza)
        {
            if (!ExistePlaza(plaza))
                AgregarPlaza(plaza);

            var temp = entidades.Plazas.Find(plaza.ID);

            if (temp != null)
            {
                entidades.Entry(temp).Property(p => p.Codigo_Plaza).CurrentValue = plaza.Codigo_Plaza;
                entidades.Entry(temp).Property(p => p.Tipo_de_plaza).CurrentValue = plaza.Tipo_de_plaza;
                entidades.Entry(temp).Property(p => p.Tipo_segun_tiempo).CurrentValue = plaza.Tipo_segun_tiempo;
                entidades.Entry(temp).Property(p => p.Horas_Totales).CurrentValue = plaza.Horas_Totales;
                entidades.Entry(temp).Property(p => p.Tiempo_de_vigencia).CurrentValue = plaza.Tiempo_de_vigencia;
            }

            Save();

        }

        //public void AsignarPlaza(int codigoPlaza, int codigoProfesor, int? horasAsignadas)
        //{
        //   // if (string.IsNullOrEmpty(codigoPlaza.Trim()))
        //     //   throw new ArgumentException("El codigo de la plaza no es válido. Por favor, intente de nuevo.");

        //    PlazaXProfesor asignarPlaza = new PlazaXProfesor()
        //    {
        //        Plaza = codigoPlaza,
        //        Profesor = codigoProfesor,
        //        Horas_Asignadas = horasAsignadas
        //    };

        //    try
        //    {
        //        AgregarPlazaProfesor(asignarPlaza);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        throw e;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException("El proveedor de autenticación retornó un error. Por favor, intente de nuevo. " +
        //            "Si el problema persiste, por favor contacte un administrador.\n" + e.Message);
        //    }

        //    Save();
        //}

        //private void AgregarPlazaProfesor(PlazaXProfesor asignarPlaza)
        //{
        //    entidades.PlazaXProfesors.Add(asignarPlaza);
        //}

    }
}