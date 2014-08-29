using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SACAAE.Models;

namespace SACAAE.Controllers
{
    [HandleError]
    public class BloqueXPlanXCursoController : Controller
    {
        private RepositorioPlanesDeEstudio vRepoPlanes = new RepositorioPlanesDeEstudio();
        private RepositorioBloqueAcademico vRepoBloques = new RepositorioBloqueAcademico();
        private RepositorioBloqueXPlan vRepoBloqueXPlan = new RepositorioBloqueXPlan();
        private RepositorioCursos vRepoCursos = new RepositorioCursos();
        private RepositorioBloqueXPlanXCurso vRepoBloquesXPlanXCurso = new RepositorioBloqueXPlanXCurso();
        private const string TempDataMessageKey = "MessageError";
        private const string TempDataMessageKeySuccess = "MessageSuccess";
        // GET: BloqueXPlanXCurso
        [Authorize]
        public ActionResult CrearBloqueXPlanXCurso()
        {
            var vBloqueXPlan = new BloqueAcademicoXPlanDeEstudio();
            ViewBag.Planes = vRepoPlanes.ObtenerTodosPlanesDeEstudio();
            ViewBag.Bloques = vRepoBloques.ListarBloquesAcademicos();
            ViewBag.Cursos = vRepoCursos.ObtenerCursos();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CrearBloqueXPlanXCurso(BloqueXPlanXCurso pBloqueXPlanXCurso, string selectPlanDeEstudio, string selectBloqueAcademico,string selectCurso)
        {
            if (pBloqueXPlanXCurso != null && selectPlanDeEstudio != null && selectBloqueAcademico != null && selectCurso != null)
            {
                int PlanID = Int16.Parse(selectPlanDeEstudio); ;
                int BloqueID = Int16.Parse(selectBloqueAcademico);
                int CursoID = Int16.Parse(selectCurso);

                pBloqueXPlanXCurso.CursoID = CursoID;
                pBloqueXPlanXCurso.BloqueXPlanID = vRepoBloqueXPlan.idBloqueXPlan(PlanID,BloqueID);
                if (vRepoBloquesXPlanXCurso.existeRelacionBloqueXPlanXCurso(pBloqueXPlanXCurso.BloqueXPlanID, pBloqueXPlanXCurso.CursoID))
                {
                    TempData[TempDataMessageKey] = "El Bloque académico de este plan de estudio ya cuenta con el curso seleccionado. Por Favor intente de nuevo.";
                    return RedirectToAction("CrearBloqueXPlanXCurso");
                }
                if (vRepoBloquesXPlanXCurso.existeRelacionCursoEnPlan(PlanID, pBloqueXPlanXCurso.CursoID))
                {
                    TempData[TempDataMessageKey] = "El Plan de estudio  ya cuenta con el curso seleccionado. Por Favor intente de nuevo.";
                    return RedirectToAction("CrearBloqueXPlanXCurso");
                }
                vRepoBloquesXPlanXCurso.crearRelacionBloqueXPlanXCurso(pBloqueXPlanXCurso);
                TempData[TempDataMessageKeySuccess] = "El curso ha sido asignado al bloque académico del plan de estudio exitosamente";
                return RedirectToAction("CrearBloqueXPlanXCurso");

            }
            TempData[TempDataMessageKey] = "Datos ingresados son inválidos";
            return RedirectToAction("CrearBloqueXPlanXCurso");
        }
    }
}