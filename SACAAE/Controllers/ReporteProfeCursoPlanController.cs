using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SACAAE.Models
{
    public class ReporteProfeCursoPlanController : Controller
    {
        private RepositorioPlanesDeEstudio repoPlanes = new RepositorioPlanesDeEstudio();
        private RepositorioBloqueAcademico repoBloques = new RepositorioBloqueAcademico();
        private RepositorioAulas repoAulas = new RepositorioAulas();
        private repositorioSedes repoSedes = new repositorioSedes();
        private RepositorioPeriodos repoPeriodos = new RepositorioPeriodos();
        private repositorioModalidades repoModalidades = new repositorioModalidades();
        private repositorioGrupos Grupos = new repositorioGrupos();
        private RepositorioCursos Cursos = new RepositorioCursos();
        private RepositorioHorario Horario = new RepositorioHorario();
        private RepositorioCursosXGrupo CursosXGrupo = new RepositorioCursosXGrupo();

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Modalidades = repoModalidades.ObtenerTodosModalidades();
            ViewBag.Sedes = repoSedes.ObtenerTodosSedes();
            ViewBag.Periodos = repoPeriodos.obtenerTodosPeriodos();
            return View();
        }

    }
}