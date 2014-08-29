using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SACAAE.Models;

namespace SACAAE.Controllers
{
    [HandleError]
    public class BloqueXPlanController : Controller
    {
        private RepositorioPlanesDeEstudio vRepoPlanes = new RepositorioPlanesDeEstudio();
        private RepositorioBloqueAcademico vRepoBloques = new RepositorioBloqueAcademico();
        private RepositorioBloqueXPlan vRepoBloquesXPlan = new RepositorioBloqueXPlan();
        private const string TempDataMessageKey = "MessageError";
        private const string TempDataMessageKeySuccess = "MessageSuccess";
        // GET: BloqueXPlan
        [Authorize]
        public ActionResult CrearBloqueXPlan()
        {
            var vBloqueXPlan = new BloqueAcademicoXPlanDeEstudio();
            ViewBag.Planes = vRepoPlanes.ObtenerTodosPlanesDeEstudio();
            ViewBag.Bloques = vRepoBloques.ListarBloquesAcademicos();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CrearBloqueXPlan(BloqueAcademicoXPlanDeEstudio pBloqueXPlan, string selectPlanDeEstudio, string selectBloqueAcademico)
        {
            if (pBloqueXPlan != null && selectPlanDeEstudio != null && selectBloqueAcademico != null)
            {
                int PlanID = Int16.Parse(selectPlanDeEstudio); ;
                int BloqueID = Int16.Parse(selectBloqueAcademico);
                pBloqueXPlan.PlanID = PlanID;
                pBloqueXPlan.BloqueID = BloqueID;
                if (vRepoBloquesXPlan.existeRelacionBloqueXPlan(pBloqueXPlan.PlanID, pBloqueXPlan.BloqueID))
                {
                    TempData[TempDataMessageKey] = "Este plan ya cuenta con el bloque seleccionado. Por Favor intente de nuevo.";
                    return RedirectToAction("CrearBloqueXPlan");
                }
                vRepoBloquesXPlan.crearRelacionBloqueXPlan(pBloqueXPlan);
                TempData[TempDataMessageKeySuccess] = "El bloque ha sido asignado al plan de estudio exitosamente";
                return RedirectToAction("CrearBloqueXPlan");
                
            }
            TempData[TempDataMessageKey] = "Datos ingresados son inválidos";
            return RedirectToAction("CrearBloqueXPlan");
        }
    }
}