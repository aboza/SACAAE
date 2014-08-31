using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SACAAE.Models
{
    public class ReportesController : Controller
    {
        //private RepositorioCursosXGrupo CursosXGrupo = new RepositorioCursosXGrupo();
        //private RepositorioCursos repoCursos = new RepositorioCursos();
        //private RepositorioHorario repoHorario = new RepositorioHorario();
        //private RepositorioProyecto repoProyecto = new RepositorioProyecto();
        //private RepositorioComision repoComisiones = new RepositorioComision();
        //private RepositorioPeriodos repoPeriodos = new RepositorioPeriodos();

        //public FileResult Download()
        //{
        //    var fi = new FileInfo("myfile.txt");
        //    byte[] bytes;
        //    try
        //    {
        //        fi.Delete();
        //    }
        //    catch (Exception e)
        //    { }
        //    using (Stream fs = new MemoryStream())
        //    {
        //        StreamWriter sw = new StreamWriter(fs);
        //        sw.WriteLine("Tipo;Grupo;Nombre;Profesor;Dia;Hora Inicio;Hora Fin;Cupo;Plan de Estudio;Modalidad;Sede; Horas Teoricas; Horas Practicas; Externo;Carga Estimada");
        //        String Periodo = Request.Cookies["Periodo"].Value;
        //        int IdPeriodo = repoPeriodos.IdPeriodo(Periodo);
        //        IQueryable<CursosXGrupo> Cursos = CursosXGrupo.ObtenerCursosGrupoPeriodo(IdPeriodo);
        //        foreach (CursosXGrupo item in Cursos)
        //        {
        //            IQueryable<Detalle_Curso> Detalles = repoCursos.ObtenerDetalleCursos(item.ID);
        //            foreach (Detalle_Curso Detalle in Detalles)
        //            {
        //                IQueryable<Dia> Dias = repoHorario.ObtenerDias(Detalle.Horario);
        //                foreach (Dia Dia in Dias)
        //                {

        //                    string HoraInicio = (Dia.Hora_Inicio / 100).ToString() + ":" + (Dia.Hora_Inicio % 100).ToString();
        //                    string HoraFin = (Dia.Hora_Fin / 100).ToString() + ":" + (Dia.Hora_Fin % 100).ToString();
        //                    double Carga = 0;
        //                    if (!item.Curso1.Externo)
        //                    {
        //                        Carga = ((item.Curso1.HorasTeoricas * 2) + ((int)item.Curso1.HorasPracticas * 1.75));
        //                        double CargaCupo = this.CalculoCupo(Convert.ToInt32(Detalle.Cupo), Convert.ToInt32(item.Curso1.HorasTeoricas), Convert.ToInt32(item.Curso1.HorasPracticas));
        //                        Carga = Carga + CargaCupo;
        //                    }
        //                    sw.WriteLine("Curso;" +
        //                                item.Grupo1.Nombre + ";" +
        //                                item.Curso1.Nombre + ";" +
        //                                Detalle.ProfesoresXCurso.Profesore.Nombre + ";" +
        //                                Dia.Dia1 + ";" +
        //                                HoraInicio + ";" +
        //                                HoraFin + ";" +
        //                                Detalle.Cupo + ";" +
        //                                item.Grupo1.PlanesDeEstudioXSede.PlanesDeEstudio.Nombre + ";" +
        //                                item.Grupo1.PlanesDeEstudioXSede.PlanesDeEstudio.Modalidade.Nombre + ";" +
        //                                item.Grupo1.PlanesDeEstudioXSede.Sede1.Nombre + ";" +
        //                                item.Curso1.HorasTeoricas + ";" +
        //                                item.Curso1.HorasPracticas + ";" +
        //                                item.Curso1.Externo.ToString() + ";" +
        //                                Carga
        //                                 );
        //                }
        //            }
        //        }

        //        //Proyecto

        //        IQueryable<Proyecto> Proyectos = repoProyecto.ObtenerTodosProyectos();
        //        foreach (Proyecto Proyecto in Proyectos)
        //        {
        //            IQueryable<ProyectosXProfesor> Profesores = repoProyecto.ObtenerProyectoXProfesor(Proyecto.ID);
        //            foreach (ProyectosXProfesor Profe in Profesores)
        //            {
        //                IQueryable<Dia> Dias = repoHorario.ObtenerDias(Profe.Horario);
        //                foreach (Dia Dia in Dias)
        //                {
        //                    string HoraInicio = (Dia.Hora_Inicio / 100).ToString() + ":" + (Dia.Hora_Inicio % 100).ToString();
        //                    string HoraFin = (Dia.Hora_Fin / 100).ToString() + ":" + (Dia.Hora_Fin % 100).ToString();

        //                    sw.WriteLine("Proyecto;N/A;" +
        //                                Proyecto.Nombre + ";" +
        //                                Profe.Profesore.Nombre + ";" +
        //                                Dia.Dia1 + ";" +
        //                                HoraInicio + ";" +
        //                                HoraFin + ";N/A;N/A;N/A;N/A;N/A;N/A;N/A;" +
        //                                (((Dia.Hora_Fin - Dia.Hora_Inicio) / 100) * 3)
        //                                );
        //                }
        //            }
        //        }


        //        //Comisiones

        //        IQueryable<Comisione> Comisiones = repoComisiones.ObtenerTodasComisiones();
        //        foreach (Comisione Comision in Comisiones)
        //        {
        //            IQueryable<ComisionesXProfesor> Profesores = repoComisiones.ObtenerComisionesXProfesor(Comision.ID);
        //            foreach (ComisionesXProfesor Profe in Profesores)
        //            {
        //                IQueryable<Dia> Dias = repoHorario.ObtenerDias(Profe.Horario);
        //                foreach (Dia Dia in Dias)
        //                {
        //                    string HoraInicio = (Dia.Hora_Inicio / 100).ToString() + ":" + (Dia.Hora_Inicio % 100).ToString();
        //                    string HoraFin = (Dia.Hora_Fin / 100).ToString() + ":" + (Dia.Hora_Fin % 100).ToString();

        //                    sw.WriteLine("Comision;N/A;" +
        //                                Comision.Nombre + ";" +
        //                                Profe.Profesore.Nombre + ";" +
        //                                Dia.Dia1 + ";" +
        //                                HoraInicio + ";" +
        //                                HoraFin + ";N/A;N/A;N/A;N/A;N/A;N/A;N/A;" +
        //                                ((Dia.Hora_Fin - Dia.Hora_Inicio) / 100)
        //                                );
        //                }
        //            }
        //        }

        //        sw.Flush();
        //        fs.Flush();
        //        fs.Position = 0;
        //        bytes = new byte[fs.Length];
        //        fs.Read(bytes, 0, bytes.Length);
        //        sw.Close();
        //        sw.Dispose();

        //    }
        //    return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Reporte.csv");
        //}


        //public double CalculoCupo(int Cupo, int HorasTeoria, int HorasPractica)
        //{

        //    double resultado = 0;
        //    if (HorasPractica == 0)
        //    {
        //        if (Cupo < 15)
        //        {
        //            resultado = 2;
        //        }
        //        else if (Cupo < 25)
        //        {
        //            resultado = 3;
        //        }
        //        else if (Cupo < 35)
        //        {
        //            resultado = 4;
        //        }
        //        else if (Cupo < 45)
        //        {
        //            resultado = 5;
        //        }
        //        else
        //        {
        //            resultado = 6;
        //        }

        //        if (HorasTeoria >= 5)
        //        {
        //            resultado = resultado + 0.75;
        //        }
        //    }
        //    if (HorasTeoria == 0)
        //    {
        //        if (Cupo < 15)
        //        {
        //            resultado = 3;
        //        }
        //        else if (Cupo < 25)
        //        {
        //            resultado = 4.5;
        //        }
        //        else
        //        {
        //            resultado = 6;
        //        }
        //    }
        //    else
        //    {
        //        if (Cupo < 15)
        //        {
        //            resultado = 2.5;
        //        }
        //        else if (Cupo < 25)
        //        {
        //            resultado = 3.75;
        //        }
        //        else if (Cupo < 35)
        //        {
        //            resultado = 5.25;
        //        }
        //        else
        //        {
        //            resultado = 6.5;
        //        }
        //    }
        //    return resultado;
        //}

    }
}
