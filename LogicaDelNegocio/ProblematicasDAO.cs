using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos;

namespace LogicaDelNegocio
{
    public class ProblematicasDAO
    {
        //Validar en la capa grafica los valores a mostrar de esta lista pues pueden ser  de tipo null
        public ObservableCollection<Dominio.Problematicas> ObtenerProblematicas()
        {
            ObservableCollection<Dominio.Problematicas> problematicasObtenidas =
                new ObservableCollection<Dominio.Problematicas>();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var problematicasEncontradas = entidadesTutorias.Problematicas.ToList();
                if (problematicasEncontradas.FirstOrDefault() != null)
                {
                    foreach (Problematicas problematicaEncontrada in problematicasEncontradas)
                    {
                        EstudiantesDAO estudianteDAO = new EstudiantesDAO();
                        ExperienciasEducativas_AcademicosDAO experienciasEducativas_AcademicosDAO =
                            new ExperienciasEducativas_AcademicosDAO();
                        TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
                        Dominio.Problematicas problematicaObtenida = new Dominio.Problematicas(problematicaEncontrada);
                        problematicaObtenida.Estudiantes = estudianteDAO.ObtenerEstudiantePorMatricula(problematicaObtenida.Matricula);
                        problematicaObtenida.ExperienciasEducativas_Academicos =
                            experienciasEducativas_AcademicosDAO.ObtenerExperienciaEducativaAcademicoPorNRC(problematicaObtenida.NRC);
                        problematicaObtenida.TutoriasAcademicas =
                            tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdTutoriaAcademica(problematicaObtenida.IdTutoriaAcademica);
                        problematicasObtenidas.Add(problematicaObtenida);
                    }
                }
            }
            return problematicasObtenidas;
        }

        public Boolean RegistrarProblematicasAcademicas(Dominio.Problematicas problematica)
        {
            Boolean registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                if (problematica != null)
                {
                    entidades.Problematicas.Add(problematica.ConvertirProblematicaDominioEnProblematiicaAccesoADatos());
                    registroExitoso = entidades.SaveChanges() == 1;
                }
            }
            return registroExitoso;
        }

        public bool ModificarProblematica(Dominio.Problematicas problematica)
        {
            bool resultado = false;
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Titulo", problematica.Titulo));
            parametros.Add(new SqlParameter("@Descripcion", problematica.Descripcion));
            parametros.Add(new SqlParameter("@IdTutoria", problematica.IdTutoriaAcademica));
            parametros.Add(new SqlParameter("@NRC", problematica.NRC));
            parametros.Add(new SqlParameter("@Matricula", problematica.Matricula));
            parametros.Add(new SqlParameter("@IdProblematica", problematica.IdProblematica));
            String query = "UPDATE [dbo].[Problematicas]" +
                " SET Titulo = @Titulo, Descripcion = @Descripcion," +
                " IdTutoriaAcademica = @IdTutoria, " +
                "NRC = @NRC, " +
                "Matricula = @Matricula " +
                "WHERE IdProblematica = @IdProblematica;";
            using (var entidades = new EntidadesTutorias())
            {
                var modificacion = entidades.Database.ExecuteSqlCommand(query, parametros.ToArray());
                resultado = modificacion == 1;
                return resultado;
            }
        }

        public ObservableCollection<Dominio.Problematicas> ObtenerProblematicasPorIdTutoriaYMatricula(int idTutoria, string matricula)
        {
            ObservableCollection <Dominio.Problematicas> problematicasObtenidas = new ObservableCollection<Dominio.Problematicas>();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var problematicasEncontradas = (from Problematicas in entidadesTutorias.Problematicas
                                                where
                                                Problematicas.IdTutoriaAcademica == idTutoria &&
                                                Problematicas.Matricula == matricula
                                                select Problematicas).ToList();
                if (problematicasEncontradas.Any())
                {
                    foreach (AccesoADatos.Problematicas problematicas in problematicasEncontradas)
                    {
                        EstudiantesDAO estudianteDAO = new EstudiantesDAO();
                        ExperienciasEducativas_AcademicosDAO experienciasEducativas_AcademicosDAO =
                            new ExperienciasEducativas_AcademicosDAO();
                        TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
                        Dominio.Problematicas problematicaObtenida = new Dominio.Problematicas(problematicas);
                        problematicaObtenida.Estudiantes = estudianteDAO.ObtenerEstudiantePorMatricula(problematicaObtenida.Matricula);
                        problematicaObtenida.ExperienciasEducativas_Academicos =
                            experienciasEducativas_AcademicosDAO.ObtenerExperienciaEducativaAcademicoPorNRC(problematicaObtenida.NRC);
                        problematicaObtenida.TutoriasAcademicas =
                            tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdTutoriaAcademica(problematicaObtenida.IdTutoriaAcademica);
                        problematicasObtenidas.Add(problematicaObtenida);
                    }
                }
            }
            return problematicasObtenidas;
        }

        public ObservableCollection<Dominio.Problematicas> RecuperarProblematicasAcademicasPorIdFechaTutoria(int idFechaTutoria)
        {
            ObservableCollection<Dominio.Problematicas> problematicasObtenidas = new ObservableCollection<Dominio.Problematicas>();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var problematicasEncontradas = (from Tutorias in entidadesTutorias.TutoriasAcademicas
                                                join Problematicas in entidadesTutorias.Problematicas
                                                on Tutorias.IdTutoriaAcademica equals Problematicas.IdTutoriaAcademica
                                                where Tutorias.IdFechaTuria == idFechaTutoria
                                                select Problematicas).ToList();
                if (problematicasEncontradas.Any())
                {
                    foreach (AccesoADatos.Problematicas problematicas in problematicasEncontradas)
                    {
                        
                        ExperienciasEducativas_AcademicosDAO experienciasEducativas_AcademicosDAO =
                            new ExperienciasEducativas_AcademicosDAO();
                        ExperienciasEducativasDAO experienciasEducativasDAO = new ExperienciasEducativasDAO();
                        AcademicosDAO academicosDAO = new AcademicosDAO();
                        TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
                        Dominio.Problematicas problematicaObtenida = new Dominio.Problematicas(problematicas);
                        problematicaObtenida.ExperienciasEducativas_Academicos =
                            experienciasEducativas_AcademicosDAO.ObtenerExperienciaEducativaAcademicoPorNRC(problematicaObtenida.NRC);
                        problematicaObtenida.ExperienciasEducativas_Academicos.Academicos = 
                            academicosDAO.ObtenerAcademicoPorIdAcademico((int)problematicaObtenida.ExperienciasEducativas_Academicos.IdAcademico);
                        problematicaObtenida.TutoriasAcademicas =
                            tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdTutoriaAcademica(problematicas.IdTutoriaAcademica);
                        problematicaObtenida.TutoriasAcademicas.Academicos = 
                            academicosDAO.ObtenerAcademicoPorIdAcademico(problematicaObtenida.TutoriasAcademicas.IdAcademico);
                        problematicasObtenidas.Add(problematicaObtenida);
                        problematicasObtenidas.OrderBy(x => x.TutoriasAcademicas.Academicos.ApellidoPaterno);
                    }
                }
            }
            return problematicasObtenidas;
        }
    }
}
