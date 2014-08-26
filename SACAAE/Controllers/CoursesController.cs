using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SACAAE.Models;

namespace SACAAE.Controllers
{
    using Models; 
    public class CoursesController : Controller
    {
        

        private RepositorioCursos repoCuros = new RepositorioCursos();
        private repositorioPlanesEstudio repoPlanes = new repositorioPlanesEstudio();
        private const string TempDataMessageKey = "MessageError";
        private const string TempDataMessageKeySuccess = "MessageSuccess";


        [Authorize]
        public ActionResult Index()
        {
            var model = new Curso();
            ViewBag.oPlanes = repoPlanes.ObtenerTodosPlanes();
            return View(model);
         
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index (Curso curso, int HorasPracticas, int HorasTeoricas, string PlanesDeEstudio, int Bloque)
        {


            if (curso != null && PlanesDeEstudio != null && (HorasPracticas > 0 || HorasTeoricas > 0))
            {
                curso.HorasPracticas = HorasPracticas;
                curso.HorasTeoricas = HorasTeoricas;
                curso.PlanDeEstudio = Int16.Parse(PlanesDeEstudio);
                curso.Bloque = Bloque;
                repoCuros.guardarCurso(curso);
                TempData[TempDataMessageKeySuccess] = "Curso Ingresado";
                return RedirectToAction("Index");
            }
            TempData[TempDataMessageKey] = "Datos ingresados son inválidos";
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Eliminar()
        {
           
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Eliminar(string planesDeEstudio, string cursosPlan)
        {

            if (cursosPlan != null && planesDeEstudio != null)
            {
                repoCuros.borrarCurso(Int32.Parse(planesDeEstudio), Int32.Parse(cursosPlan));
                ViewBag.message = "Curso Eliminado";
                
            }
            else
                ViewBag.message = "Campos inválidos en formulario";
                 
          
            return RedirectToAction("Eliminar");
       

        }
 

        public ActionResult planesLista()
        {
            IQueryable listaPlanes = repoPlanes.ObtenerTodosPlanes();
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                        listaPlanes,
                        "ID", 
                        "Nombre"), JsonRequestBehavior.AllowGet
                        ); 
            }
            return View(listaPlanes); 
        }

        public ActionResult cursosPlanLista(string idPlan)
        {
            IQueryable listaCursos = repoCuros.ObtenerCursosDePlan(Int16.Parse(idPlan));
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(listaCursos, "ID", "Nombre"), JsonRequestBehavior.AllowGet); 
            }
            return View(listaCursos); 
        }




      
    }
}
