using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SACAAE.Models
{
    public class RepositorioHorario
    {
        private SACAAEEntities entidades;

        public RepositorioHorario()
        {
            entidades = new SACAAEEntities();
        }

        public int NuevoHorario()
        {
            Horario HorarioNuevo = new Horario();
            try { entidades.Horarios.Add(HorarioNuevo); }
            catch (ArgumentException e) { throw e; }
            catch (Exception e)
            {
                throw new ArgumentException("El proveedor de autenticación retornó un error. Por favor, intente de nuevo. " +
                    "Si el problema persiste, por favor contacte un administrador.\n" + e.Message);
            }
            entidades.SaveChanges();//Pasa los cambios a la base de datos
            return HorarioNuevo.Id;
        }

        public int AgregarDia(string Dia, int Horario, int HoraInicio, int HoraFin)
        {
            Dia DiaNuevo = new Dia();
            DiaNuevo.Dia1 = Dia;
            DiaNuevo.Horario = Horario;
            DiaNuevo.Hora_Inicio = HoraInicio;
            DiaNuevo.Hora_Fin = HoraFin;
            try { entidades.Dias.Add(DiaNuevo); }
            catch (ArgumentException e) { throw e; }
            catch (Exception e)
            {
                throw new ArgumentException("El proveedor de autenticación retornó un error. Por favor, intente de nuevo. " +
                    "Si el problema persiste, por favor contacte un administrador.\n" + e.Message);
            }
            entidades.SaveChanges();//Pasa los cambios a la base de datos
            return DiaNuevo.Id;
        }

        public void EliminarDias(int IdHorario)
        {
            IQueryable<Dia> Resultado =
                from Dia in entidades.Dias
                where Dia.Horario == IdHorario
                select Dia;
            foreach (Dia item in Resultado)
            {
                try
                {
                    entidades.Dias.Remove(item);//Agrega la nueva entidad en el modelo local
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
            entidades.SaveChanges();//Pasa los cambios a la base de datos
        }
        public int CrearHorario()
        {
            Horario horario = new Horario();
            entidades.Horarios.Add(horario);
            entidades.SaveChanges();
            return horario.Id;
        }

        public int ObtenerUltimoHorario()
        {
            Horario last = (from horario in entidades.Horarios
                             orderby horario.Id descending
                             select horario).First();

            return last.Id;
        }

        public IQueryable<Dia> ObtenerDias(int Horario)
        {
            return from Dia in entidades.Dias
                   where Dia.Horario == Horario
                   select Dia;
        }
    }
}