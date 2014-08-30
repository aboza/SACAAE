using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SACAAE.Models;

namespace SACAAE.Controllers
{
    public class PlansController : Controller
    {
        private RepositorioPlanesDeEstudio repoPlanes = new RepositorioPlanesDeEstudio();
        private RepositorioBloqueAcademico repoBloques = new RepositorioBloqueAcademico();
        private RepositorioCursos repoCursos = new RepositorioCursos();
        private repositorioPlanesXSedes repoPlanesXSedes = new repositorioPlanesXSedes();
        private repositorioModalidades repoModalidades = new repositorioModalidades();
        private repositorioSedes repoSedes = new repositorioSedes();
        private const string TempDataMessageKey = "MessageError";
        private const string TempDataMessageKeySuccess = "MessageSuccess";
        //
        // GET: /Plan/
        [Authorize]
        public ActionResult CrearPlan()
        {
            ViewBag.Modalidades = repoModalidades.ObtenerTodosModalidades();
            ViewBag.Sedes = repoSedes.ObtenerTodosSedes();
            var model = new PlanesDeEstudio();
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Modalidades = repoModalidades.ObtenerTodosModalidades();
            ViewBag.Sedes = repoSedes.ObtenerTodosSedes();
            var model = new PlanesDeEstudio();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string sltPlan)
        {
            int PlanID = Int16.Parse(sltPlan);
            if (sltPlan == null)
            {
                TempData[TempDataMessageKey] = "Seleccione un Plan";
                return RedirectToAction("CrearPlan");
            }
            return RedirectToAction("BloqueXPlan", new { plan = PlanID });
        }

        public ActionResult BloqueXPlan(int plan)
        {
            ViewBag.Bloques = repoBloques.obtenerBloques(plan);
            ViewBag.Plan = repoPlanes.ObtenerUnPlanDeEstudio(plan);
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult BloqueXPlan(string sltPlan,string sltBloque)
        {
            int PlanID = Int16.Parse(sltPlan);
            int BloqueID = Int16.Parse(sltBloque);
            return RedirectToAction("CursoXPlanXBloque", new { plan = PlanID , bloque= BloqueID});
        }

        [Authorize]
        public ActionResult CursoXPlanXBloque(int plan,int bloque)
        {
            ViewBag.Cursos = repoCursos.ObtenerCursos(plan,bloque);
            var model = new Curso();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CrearPlan(PlanesDeEstudio plan,int Modalidades,List<int> Sedes)
        {
            if (plan.Nombre == null)
            {
                TempData[TempDataMessageKey] = "Ingrese un Nombre";
                return RedirectToAction("CrearPlan");
            }
            if (repoPlanes.existe(plan.Nombre,Modalidades) != null)
            {
                TempData[TempDataMessageKey] = "Ya existe ese plan de estudio";
                return RedirectToAction("CrearPlan");
            }
            if(Sedes==null)
            {
                TempData[TempDataMessageKey] = "Seleccione al menos una sede";
                return RedirectToAction("CrearPlan");   
            }
            plan.Modalidad = Modalidades;
            repoPlanes.agregarPlan(plan);
            int idplan = repoPlanes.IdPlanDeEstudioPorIdModalidad(plan.Nombre, Modalidades);
            PlanesDeEstudioXSede planXSede = new PlanesDeEstudioXSede();
            planXSede.PlanDeEstudio = idplan;
            foreach (int idsede in Sedes)
            {
                planXSede.Sede = idsede;
                repoPlanesXSedes.agregrarPlanXSede(planXSede);
            }
            TempData[TempDataMessageKeySuccess] = "Plan Creado Exitosamente";
            return RedirectToAction("CrearBloqueXPlan", "BloqueXPlan", new { plan = idplan });
        }

        
    }
}
