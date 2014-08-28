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
            return RedirectToAction("Index","Courses");
        }
    }
}
