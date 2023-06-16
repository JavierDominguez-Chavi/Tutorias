using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos;
using System.Data.Entity.Validation;
using System.Data.Entity.Core;

namespace LogicaDelNegocio
{
    public class TutoriasAcademicasDAO
    {
        public Dominio.TutoriasAcademicas ObtenerTutoriaAcademicaPorIdTutoriaAcademica(int idTutoriaAcademica)
        {
            Dominio.TutoriasAcademicas tutoriaAcademicaObtenida = new Dominio.TutoriasAcademicas();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var tutoriaAcademicaEncontrada = entidadesTutorias.TutoriasAcademicas.Find(idTutoriaAcademica);
                if (tutoriaAcademicaEncontrada != null)
                {
                    tutoriaAcademicaObtenida = new Dominio.TutoriasAcademicas(tutoriaAcademicaEncontrada);
                }
            }
            return tutoriaAcademicaObtenida;
        }
        public Dominio.TutoriasAcademicas ObtenerTutoriaAcademicaPorIdFechaTutoria(int idFechaTutoria)
        {
            Dominio.TutoriasAcademicas tutoriaAcademicaObtenida = new Dominio.TutoriasAcademicas();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var tutoriaAcademicaEncontrada = (from TutoriasAcademicas in entidadesTutorias.TutoriasAcademicas
                                                  where TutoriasAcademicas.IdFechaTuria == idFechaTutoria
                                                  select TutoriasAcademicas).FirstOrDefault();
                if (tutoriaAcademicaEncontrada != null)
                {
                    tutoriaAcademicaObtenida = new Dominio.TutoriasAcademicas(tutoriaAcademicaEncontrada);
                }

            }
            return tutoriaAcademicaObtenida;
        }

        public Dominio.TutoriasAcademicas ConsultarInformacionCompletaTutoriaAcademica(int idTutoriaAcademica)
        {
            Dominio.TutoriasAcademicas tutoriaAcademicaObtenida = new Dominio.TutoriasAcademicas();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var tutoriaAcademicaEncontrada = entidadesTutorias.TutoriasAcademicas.Find(idTutoriaAcademica);
                if (tutoriaAcademicaEncontrada != null)
                {
                    tutoriaAcademicaObtenida = new Dominio.TutoriasAcademicas(tutoriaAcademicaEncontrada);
                    var fechaTutoria = entidadesTutorias.FechasTutorias.Find(tutoriaAcademicaObtenida.IdFechaTuria);
                    tutoriaAcademicaObtenida.FechasTutorias = new Dominio.FechasTutorias(fechaTutoria);
                    var periodoEscolar = entidadesTutorias.PeriodosEscolares.Find(fechaTutoria.IdPeriodo);
                    tutoriaAcademicaObtenida.FechasTutorias.PeriodosEscolares = new Dominio.PeriodosEscolares(periodoEscolar);
                }

            }
            return tutoriaAcademicaObtenida;
        }

        public Boolean GuardarInformacionTutoriaAcademica(Dominio.TutoriasAcademicas tutoriaAcademica, bool sinComentario)
        {
            bool tutoriaRegistrada = false;
            if (tutoriaAcademica.IdTutoriaAcademica > 0)
            {
                using (var entidadesTutorias = new EntidadesTutorias())
                {
                    AccesoADatos.TutoriasAcademicas tutoriaObtenida =
                        entidadesTutorias.TutoriasAcademicas.Find(tutoriaAcademica.IdTutoriaAcademica);
                    if (!sinComentario) {
                        tutoriaObtenida.ComentarioGeneral = tutoriaAcademica.ComentarioGeneral;
                    }
                    tutoriaObtenida.ReporteEntregado = tutoriaAcademica.ReporteEntregado;
                    entidadesTutorias.TutoriasAcademicas.AddOrUpdate(tutoriaObtenida);
                    tutoriaRegistrada = entidadesTutorias.SaveChanges() == 1;
                }
            }
            return tutoriaRegistrada;
        }

        public ObservableCollection<Dominio.TutoriasAcademicas> ObtenerTutoriasAcademicasPorIdFechaTutoriaEIdAcademico(int idFechaTutoria, int idAcademico)
        {
            ObservableCollection<Dominio.TutoriasAcademicas> tutoriasAcademicas =
                new ObservableCollection<Dominio.TutoriasAcademicas>();
            List<AccesoADatos.TutoriasAcademicas> tutoriasAcademicasEncontradas;
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                tutoriasAcademicasEncontradas = (from TutoriasAcademicas in entidadesTutorias.TutoriasAcademicas where
                                                     TutoriasAcademicas.IdFechaTuria == idFechaTutoria &&
                                                     TutoriasAcademicas.IdAcademico == idAcademico
                                                     select TutoriasAcademicas).ToList();
                foreach (AccesoADatos.TutoriasAcademicas tutoriaAcademica in tutoriasAcademicasEncontradas)
                {
                    tutoriasAcademicas.Add(new Dominio.TutoriasAcademicas(tutoriaAcademica));
                }
            }

            return tutoriasAcademicas;
        }

        public Boolean ValidarQueExistanReportesDeTutoria(int idFechaTutoria)
        {
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var reportesEnontrados = (from TutoriasAcademicas in entidadesTutorias.TutoriasAcademicas
                                          where TutoriasAcademicas.IdFechaTuria == idFechaTutoria &&
                                          TutoriasAcademicas.ReporteEntregado == true
                                          select TutoriasAcademicas).ToList();
                if (reportesEnontrados != null)
                {
                    if (reportesEnontrados.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else 
                {
                    throw new ObjectNotFoundException();
                }
            }
        }

        public Boolean ValidarQueTutorNoEntregoYaElReporte(int idFechaTutoria, int idAcademico)
        {
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var reportesEnontrados = (from TutoriasAcademicas in entidadesTutorias.TutoriasAcademicas
                                          where TutoriasAcademicas.IdFechaTuria == idFechaTutoria &&
                                          TutoriasAcademicas.ReporteEntregado == true && TutoriasAcademicas.IdAcademico == idAcademico
                                          select TutoriasAcademicas).ToList();
                if (reportesEnontrados != null)
                {
                    if (reportesEnontrados.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new ObjectNotFoundException();
                }
            }
        }

        public ObservableCollection<Dominio.TutoriasAcademicas> ObtenerComentariosGeneralesDeTutoriasAcademicas(int idFechaTutoria)
        {
            ObservableCollection<Dominio.TutoriasAcademicas> tutoriasAcademicas = new ObservableCollection<Dominio.TutoriasAcademicas>();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var tutoriasAcademicasEncontradas = (from TutoriasAcademicas in entidadesTutorias.TutoriasAcademicas
                                                     where TutoriasAcademicas.IdFechaTuria == idFechaTutoria &&
                                                     TutoriasAcademicas.ReporteEntregado == true
                                                     select TutoriasAcademicas).ToList();
                if (tutoriasAcademicasEncontradas != null)
                {
                    foreach (AccesoADatos.TutoriasAcademicas tutoriaAcademica in tutoriasAcademicasEncontradas)
                    {
                        AcademicosDAO academicosDAO = new AcademicosDAO();
                        tutoriaAcademica.Academicos = 
                            academicosDAO.ObtenerAcademicoPorIdAcademico(tutoriaAcademica.IdAcademico).ConvertirAcademicoDominioEnAcademicoAccesoADatos();
                        tutoriasAcademicas.Add(new Dominio.TutoriasAcademicas(tutoriaAcademica));
                    }
                } 
            }

            return tutoriasAcademicas;

        }
        public bool RegistrarTutoriaAcademica(Dominio.TutoriasAcademicas tutoriaAcademicaNueva)
        {
            bool registroRealizado = false;
            using (var entidades = new EntidadesTutorias())
            {
                entidades.TutoriasAcademicas.Add(tutoriaAcademicaNueva.AAccesoADatos());
                entidades.SaveChanges();
                registroRealizado = true;
            }
            return registroRealizado;
        }
    }
}
