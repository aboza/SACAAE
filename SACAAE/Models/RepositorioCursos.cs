using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SACAAE.Models
{
    public class RepositorioCursos
    {
        private SACAAEEntities entidades;

        public RepositorioCursos()
        {
            entidades = new SACAAEEntities(); 
        }

        public int IdCursos(string CursoBuscado, int PlanDeEstudioCurso)
        {

            IQueryable<Curso> Resultado =
                from Curso in entidades.Cursos
                where (Curso.Nombre == CursoBuscado && Curso.PlanDeEstudio == PlanDeEstudioCurso)
                select Curso;

            try
            {
                return Resultado.FirstOrDefault().ID;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int IdHorarioCurso(int CursoBuscado, int PlanDeEstudioCurso)
        {
            IQueryable<Detalle_Curso> Resultado =
                from Detalle_Curso in entidades.Detalle_Curso
                where Detalle_Curso.Curso == CursoBuscado//aqui necesito el id del curso x grupo
                select Detalle_Curso;
            return Resultado.FirstOrDefault().Horario;
        }

        public IQueryable<Curso> ObtenerCursos(int PlanDeEstudio)
        {
            return from Curso in entidades.Cursos
                   where Curso.PlanDeEstudio == PlanDeEstudio
                   select Curso;
        }

        public IQueryable<Curso> ObtenerCursos()
        {
            return from Curso in entidades.Cursos
                   select Curso;
        }

        public IQueryable<Curso> ObtenerCursos(int PlanDeEstudio, int bloque)
        {
            return from Curso in entidades.Cursos
                   where Curso.PlanDeEstudio == PlanDeEstudio && Curso.Bloque == bloque
                   select Curso;
        }

        public IQueryable<Detalle_Curso> ObtenerDetalleCursos()
        {
            return from Detalle_Curso in entidades.Detalle_Curso
                   select Detalle_Curso;
        }

        public int CursoSinProfesor(int Profesor)
        {
            ProfesoresXCurso NuevoProfesorXCurso = new ProfesoresXCurso();
            NuevoProfesorXCurso.Profesor = Profesor;
            NuevoProfesorXCurso.Horas = 0;
            try
            {
                entidades.ProfesoresXCursoes.Add(NuevoProfesorXCurso);//Agrega la nueva entidad en el modelo local
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
            entidades.SaveChanges();//Pasa los cambios a la base de datos
            return NuevoProfesorXCurso.Id;
        }

        public int GuardarDetallesCurso(int CursoXGrupo, int Horario, string Aula, int Profesor)
        {
            Detalle_Curso DetallesNuevos = new Detalle_Curso();
            DetallesNuevos.Curso = CursoXGrupo;
            DetallesNuevos.Horario = Horario;
            DetallesNuevos.Aula = Aula;
            DetallesNuevos.Profesor = Profesor;
            DetallesNuevos.Cupo = 0;
            try
            {
                entidades.Detalle_Curso.Add(DetallesNuevos);//Agrega la nueva entidad en el modelo local
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
            entidades.SaveChanges();//Pasa los cambios a la base de datos
            return DetallesNuevos.Id;
        }
        
        public void guardarCurso(Curso curso)
        {

            if (existeCurso(curso.PlanDeEstudio, curso.Nombre))
                return; 
            entidades.Cursos.Add(curso);
            Save(); 
           
        }

        public IQueryable<Curso> ObtenerCursosDePlan(int id)
        {
            return from curso in entidades.Cursos
                   where curso.PlanDeEstudio == id
                   orderby curso.Nombre
                   select curso; 
        }

        public void borrarCurso(int planDeEstudio, int Curso)
        {
            if (existeCurso(planDeEstudio, Curso))
            {
                var curso = entidades.Cursos.SingleOrDefault(c => c.ID == Curso && c.PlanDeEstudio == planDeEstudio);
                if (curso != null)
                {
                    entidades.Cursos.Remove(curso);
                    Save(); 
                }

            }
        }

        public IQueryable<Detalle_Curso> ObtenerDetalleCursos(int CursoXGrupo)
        {
            return from Detalle_Curso in entidades.Detalle_Curso
                   where Detalle_Curso.Curso == CursoXGrupo
                   select Detalle_Curso;
        }

        private bool existeCurso(int planDeEstudio, string Curso)
        {
          
            return (entidades.Cursos.SingleOrDefault(c => c.Nombre == Curso && c.PlanDeEstudio == planDeEstudio) != null); 
           
        }

        private bool existeCurso(int planDeEstudio, int Curso)
        {

            return (entidades.Cursos.SingleOrDefault(c => c.ID == Curso && c.PlanDeEstudio == planDeEstudio) != null);

        }

        public Boolean existeCursoEnBloque(int planDeEstudio, int Bloque)
        {
            var request = from curso in entidades.Cursos
                          where curso.PlanDeEstudio == planDeEstudio && curso.Bloque == Bloque
                          select curso;
            if (request.Any())
                return true;
            return false;
        }

        private void Save()
        {
            entidades.SaveChanges(); 
        }
    
    }
}