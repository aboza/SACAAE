using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SACAAE
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*Rutas Cortés*/
            routes.MapRoute(
             "ObtenerPlanesEstudio",
             "CursoProfesor/Planes/List/{sede}/{modalidad}",
             new { Controller = "CursoProfesor", action = "ObtenerPlanesEstudio" });

            routes.MapRoute(
             "ObtenerCursos",
             "CursoProfesor/Cursos/List/{plan}",
             new { Controller = "CursoProfesor", action = "ObtenerCursos" });

            routes.MapRoute(
             "ObtenerGrupos",
             "CursoProfesor/Grupos/List/{curso}",
             new { Controller = "CursoProfesor", action = "ObtenerGrupos" });

            routes.MapRoute(
             "ObtenerInfo",
             "CursoProfesor/Grupos/Info/{cursoxgrupo}",
             new { Controller = "CursoProfesor", action = "ObtenerInfo" });

            routes.MapRoute(
             "ObtenerHorario",
             "CursoProfesor/Horarios/Info/{cursoxgrupo}",
             new { Controller = "CursoProfesor", action = "ObtenerHorario" });

            routes.MapRoute(
            "ObtenerCursosPorProfesor",
            "CursoProfesor/Profesor/Cursos/{idProfesor}",
            new { Controller = "CursoProfesor", action = "ObtenerCursosPorProfesor" });

            routes.MapRoute(
            "ObtenerComisionesXProfesor",
            "ComisionProfesor/Profesor/Comisiones/{idProfesor}",
            new { Controller = "ComisionProfesor", action = "ObtenerComisionesXProfesor" });

            routes.MapRoute(
            "ObtenerProyectosXProfesor",
            "ProfesoresProyectos/Profesor/Proyectos/{idProfesor}",
            new { Controller = "ProfesoresProyectos", action = "ObtenerProyectosXProfesor" });    

            /*Fin Rutas Cortés*/
            /*Rutas Alvaro*/
            routes.MapRoute(
                "planesLista",
                "Courses/Planes/List",
                new { Controller = "Courses", action = "planesLista", ID = "" });

            routes.MapRoute(
               "cursosPlanLista2",
               "Courses/Cursos/List/{idPlan}",
               new { Controller = "Courses", action = "cursosPlanLista" });



            routes.MapRoute(
                "sedesLista",
                "Groups/Sedes/List",
                new { Controller = "Groups", action = "sedesLista" });

            routes.MapRoute(
                "modalidadesLista",
                "Groups/Modalidades/List",
                new { Controller = "Groups", action = "modalidadesLista" });

            routes.MapRoute(
             "planesSedeModalidadLista",
             "Groups/Planes/List/{IDModalidad}/{IDSede}",
             new { Controller = "Groups", action = "planesSedeModalidadLista" });

            routes.MapRoute(
             "gruposLista",
             "Groups/Grupos/List/{plan}/{bloque}/{sede}",
             new { Controller = "Groups", action = "gruposLista" });
            /*Fin rutas Alvaro*/
            /*rutas avila*/
            routes.MapRoute(
              "cursosPlanLista",
              "Seleccionar/Cursos/List/{idPlan}",
              new { Controller = "Seleccionar", action = "cursosPlanLista" }); 
            /*Fin rutas avila*/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}