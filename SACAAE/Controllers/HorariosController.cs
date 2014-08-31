using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SACAAE.Models
{
    public class HorariosController : Controller
    {
        //private RepositorioPlanesDeEstudio PlanesDeEstudio = new RepositorioPlanesDeEstudio();
        //private repositorioSedes Sedes = new repositorioSedes();
        //private repositorioModalidades Modalidades = new repositorioModalidades();
        //private repositorioGrupos Grupos = new repositorioGrupos();
        //private RepositorioCursos Cursos = new RepositorioCursos();
        //private RepositorioHorario Horario = new RepositorioHorario();
        //private RepositorioCursosXGrupo CursosXGrupo = new RepositorioCursosXGrupo();

        //public ActionResult Horarios()
        //{
        //    int Grupo;
        //    int Bloque;
        //    String PlanDeEstudio;
        //    String Modalidad;
          
        //    //
        //    try
        //    {
        //        Grupo = Convert.ToInt32(Request.Cookies["SelIdGrupo"].Value);
        //        PlanDeEstudio = Request.Cookies["SelPlanDeEstudio"].Value;
        //        Modalidad = Request.Cookies["SelModalidad"].Value;
        //        Bloque = Convert.ToInt32(Request.Cookies["SelBloque"].Value);
        //    }
        //    catch (Exception e)
        //    {
        //        Grupo = 0;
        //        throw new ArgumentException("No se detecto ningun Grupo o Plan de Estudio" + e.Message);
        //    }
        //    int IdPlanDeEstudio=PlanesDeEstudio.IdPlanDeEstudio(PlanDeEstudio,Modalidad);
        //    List<String> NombreCursos = new List<String>();
        //    IQueryable<Curso> ListaCursos = Cursos.ObtenerCursos(IdPlanDeEstudio, Bloque);
        //    foreach (var item in ListaCursos)
        //    {
        //        NombreCursos.Add(item.Nombre);
        //    }
        //    ViewBag.Cursos = NombreCursos;

        //    List<String> Dias = new List<String>();
        //    Dias.Add("Lunes");
        //    Dias.Add("Martes");
        //    Dias.Add("Miercoles");
        //    Dias.Add("Jueves");
        //    Dias.Add("Viernes");
        //    Dias.Add("Sabado");
        //    Dias.Add("Domingo");
        //    ViewBag.Dias = Dias;

        //    List<String> Horas = new List<String>();
        //    for (int i=0;i<24;i++)
        //    {
        //        if(i<10)
        //        Horas.Add("0"+i.ToString());
        //        else
        //        Horas.Add(i.ToString());
        //    }
        //    ViewBag.Horas = Horas;

        //    List<String> Minutos = new List<String>();
        //    for (int i = 0; i < 60; i+=10)
        //    {
        //        if (i < 10)
        //            Minutos.Add("0" + i.ToString());
        //        else
        //            Minutos.Add(i.ToString());
        //    }
        //    ViewBag.Minutos = Minutos;


        //    IQueryable<Detalle_Curso> TodosCursos = Cursos.ObtenerDetalleCursos();
        //    List<Detalle_Curso> Actuales= new List<Detalle_Curso>();
        //    int j=1;
        //    int cantidad = 0;
        //    foreach (Detalle_Curso item in TodosCursos)
        //    {
        //        if (item.CursosXGrupo.Grupo == Grupo)
        //        {
        //            Actuales.Add(item);
        //            IQueryable<Dia> dias = Horario.ObtenerDias(item.Horario);
        //            foreach (Dia elementodia in dias)
        //            {
        //                HttpCookie Cookie = new HttpCookie("Cookie" + j.ToString());
        //                Cookie.Value = item.CursosXGrupo.Curso1.Nombre+"|"+elementodia.Dia1+"|"+elementodia.Hora_Inicio+"|"+elementodia.Hora_Fin;
        //                Cookie.Path = "/Horarios";
        //                Response.Cookies.Set(Cookie);
        //                cantidad++;
        //                j++;
        //            }
        //         }
        //     }

        //    HttpCookie CookieCantidadACargar =new HttpCookie("ACargar");
        //    CookieCantidadACargar.Value = cantidad.ToString();
        //    CookieCantidadACargar.Path = "/Horarios";
        //    Response.Cookies.Set(CookieCantidadACargar);

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult GuardarCambios()
        //{
        //    int Cantidad;
        //    int Grupo;
        //    int PlanDeEstudio;
        //    try
        //    {
        //         Cantidad = Convert.ToInt32(Request.Cookies["Cantidad"].Value);
        //    }
        //    catch (Exception e)
        //    {
        //        Cantidad = 0;
        //    }

        //    try
        //    {
        //        Grupo = Convert.ToInt32(Request.Cookies["SelIdGrupo"].Value);
        //        PlanDeEstudio = PlanesDeEstudio.IdPlanDeEstudio(Request.Cookies["SelPlanDeEstudio"].Value, Request.Cookies["SelModalidad"].Value);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException("No se detecto ningun Grupo o Plan de Estudio" + e.Message);
        //    }

        //    //Limpiar Horario Cursos
        //    IQueryable<Detalle_Curso> Viejos = Cursos.ObtenerDetalleCursos();
        //    foreach (Detalle_Curso item in Viejos)
        //    {
        //        if(item.CursosXGrupo.Grupo==Grupo)
        //        {
        //            Horario.EliminarDias(item.Horario);
        //        }
        //    }

        //    if (Cantidad == 0) {return RedirectToAction("Resultado");}//En caso de que el horario este vacio se asume que se desea borrar por lo que se limpian los dias y se termina, este return evita que falle el programa cuando no hay cookies
        //    //Guardar Datos
        //    for (int i = 1; i <= Cantidad; i++)
        //    {
        //        String Detalles= Request.Cookies["Cookie" + i].Value;//Obtiene los datos de la cookie
        //        string[] Partes = Detalles.Split('|');
        //        String Curso = Partes[0];
        //        String Dia = Partes[1];
        //        String HoraInicio = Partes[2];
        //        String HoraFin = Partes[3];
        //        int IdCurso = Cursos.IdCursos(Curso, PlanDeEstudio);
        //        int Existe=CursosXGrupo.Existe(IdCurso, Grupo);//Existe el curso para ese grupo?

        //        if (Existe!=-1)
        //        {
        //            int IdHorario = Cursos.IdHorarioCurso(Existe, PlanDeEstudio);
        //            Horario.AgregarDia(Dia, IdHorario, Convert.ToInt32(HoraInicio), Convert.ToInt32(HoraFin));
        //        }
        //        else
        //        {
        //            int IdCursoXGrupo = CursosXGrupo.GuardarCursoXGrupo(Grupo, IdCurso);
        //            int HorarioNuevo = Horario.NuevoHorario();
        //            Horario.AgregarDia(Dia, HorarioNuevo, Convert.ToInt32(HoraInicio), Convert.ToInt32(HoraFin));
        //            int Profesor = Cursos.CursoSinProfesor(1);//En este caso es 1 xq en la base de datos el profesor 1 va a ser sin asignar
        //            Cursos.GuardarDetallesCurso(IdCursoXGrupo, HorarioNuevo, "",Profesor);
        //        }
        //    }
        //    Response.Cookies.Clear();
        //    return RedirectToAction("Resultado");
        //}

        //public ActionResult Resultado()
        //{
        //    return View();
        //}
    }
}
