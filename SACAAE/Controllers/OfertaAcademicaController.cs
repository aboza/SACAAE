using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SACAAE.Models;

namespace SACAAE.Controllers
{
    public class OfertaAcademicaController : Controller
    {
        private RepositorioPlanesDeEstudio vRepoPlanes = new RepositorioPlanesDeEstudio();
        private repositorioModalidades vRepoModalidades = new repositorioModalidades();
        private repositorioSedes vRepoSedes = new repositorioSedes();
        private RepositorioPeriodos vRepoPeriodos = new RepositorioPeriodos();
        private repositorioPlanesXSedes vRepoPlanXSedes = new repositorioPlanesXSedes();
        private RepositorioBloqueXPlan vRepoBloqueXPlan = new RepositorioBloqueXPlan();
        private RepositorioBloqueXPlanXCurso vRepoBloqueXPlanXCurso = new RepositorioBloqueXPlanXCurso();
        private repositorioGrupos vRepoGrupos = new repositorioGrupos();
        private const string TempDataMessageKey = "MessageError";
        private const string TempDataMessageKeySuccess = "MessageSuccess";
        // GET: OfertaAcademica
        [Authorize]
        public ActionResult CrearOfertaAcademica()
        {
            ViewBag.Modalidades = vRepoModalidades.ObtenerTodosModalidades();
            ViewBag.Sedes = vRepoSedes.ObtenerTodosSedes();
            ViewBag.Periodos = vRepoPeriodos.obtenerTodosPeriodos();
            return View();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult CrearOfertaAcademica(string sltPeriodo,string sltSede,string sltPlan,
            string sltBloque,string sltCurso, int cantidadGrupos)
        {
            int vPeriodoID = Int16.Parse(sltPeriodo);
            int vSedeID = Int16.Parse(sltSede);
            int vPlanID = Int16.Parse(sltPlan);
            int vBloqueID = Int16.Parse(sltBloque);
            int vCursoID = Int16.Parse(sltCurso);

            int vPlanXSedeID = vRepoPlanXSedes.tomarIDPlanXSede(vSedeID, vPlanID).ID;
            int vBloqueXPlanID = vRepoBloqueXPlan.obtenerIdBloqueXPlan(vPlanID, vBloqueID);
            int vBloqueXPlanXCursoID = vRepoBloqueXPlanXCurso.obtenerBloqueXPlanXCursoID(vBloqueXPlanID, vCursoID);
            for (int vContadorGrupos = 0; vContadorGrupos < cantidadGrupos; vContadorGrupos++)
            {
                int vNumeroGrupo = vRepoGrupos.ObtenerUltimoNumeroGrupo(vPlanXSedeID, vPeriodoID, vBloqueXPlanXCursoID) + 1;
                Grupo vNewGrupo = new Grupo();
                vNewGrupo.Numero = vNumeroGrupo;
                vNewGrupo.PlanDeEstudio = vPlanXSedeID;
                vNewGrupo.Periodo = vPeriodoID;
                vNewGrupo.BloqueXPlanXCursoID = vBloqueXPlanXCursoID;

                vRepoGrupos.agregarGrupo(vNewGrupo);
            }
            TempData[TempDataMessageKeySuccess] = "Los Grupos fueron creados correctamente";
            return RedirectToAction("Index");
        }
    }
}