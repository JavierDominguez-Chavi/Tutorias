using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoADatos;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.Entity.Migrations;
using System.Data.Entity.Core;

namespace LogicaDelNegocio
{
    public class HorarioDAO
    {
        public ObservableCollection<Dominio.Horarios>
        ObtenerHorariosPorMatriculaYPeriodoEscolar(string matricula, int idPeriodoEscolar)
        {
            ObservableCollection<Dominio.Horarios> horariosObtenidos =
                new ObservableCollection<Dominio.Horarios>();
            if (!String.IsNullOrWhiteSpace(matricula) && idPeriodoEscolar > 0)
            {
                var entidades = new EntidadesTutorias();

                var horarios = (from horario in entidades.Horarios
                                where horario.Matricula.Equals(matricula)
                                join tutorias in entidades.TutoriasAcademicas
                                    on horario.IdTutoriaAcademica equals tutorias.IdTutoriaAcademica
                                join fecha in entidades.FechasTutorias
                                    on tutorias.IdFechaTuria equals fecha.IdFechaTutoria
                                where fecha.IdPeriodo == idPeriodoEscolar
                                select horario)
                                .ToList();

                for (int i = 0; i < horarios.Count; i++)
                {
                    horariosObtenidos.Add(
                        new Dominio.Horarios
                        {
                            Asistencia = horarios[i].Asistencia,
                            Riesgo = horarios[i].Riesgo,
                            IdTutoriaAcademica = horarios[i].IdTutoriaAcademica,
                            TutoriasAcademicas = new Dominio.TutoriasAcademicas(horarios[i].TutoriasAcademicas)
                        });
                }

                //Si se obtuvieron menos de 3 horarios, anade horarios comodin para evitar nullpointers.
                for (int i = horariosObtenidos.Count; i < 3; i++)
                {
                    horariosObtenidos.Add(
                        new Dominio.Horarios
                        {
                            Asistencia = false,
                            Riesgo = false,
                            IdTutoriaAcademica = 0
                        });
                }

                entidades.Dispose();
            }
            return horariosObtenidos;
        }
        public Boolean RegistrarHorarioDeSesionDeTutoria(Dominio.Horarios horarioNuevo)
        {
            Boolean registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                entidades.Horarios.Add(horarioNuevo.AAccesoADatos());
                entidades.SaveChanges();
                registroExitoso = true;
            }
            return registroExitoso;
        }
        public ObservableCollection<Dominio.Horarios> ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula(ObservableCollection<Dominio.Estudiantes> estudiantes, int idTutoria)
        {
            ObservableCollection<Dominio.Horarios> horariosObtenidos = new ObservableCollection<Dominio.Horarios>();
            using (var entidades = new EntidadesTutorias())
            {
                foreach (Dominio.Estudiantes estudianteAConsultar in estudiantes)
                {
                    var horariosEncontrados = (from Horarios in entidades.Horarios
                                               where Horarios.Matricula == estudianteAConsultar.Matricula
                                               && Horarios.IdTutoriaAcademica == idTutoria
                                               select Horarios).ToList();
                    foreach (AccesoADatos.Horarios horarios in horariosEncontrados)
                    {
                        horariosObtenidos.Add(new Dominio.Horarios(horarios));
                    }
                }
            }
            return horariosObtenidos;
        }
        public Boolean ModificarHorarioDeSesionDeTutoriaPorMatriculaYIdTutoria(Dominio.Horarios horarioAModificar)
        {
            Boolean modificacionExitosa = false;
            if (horarioAModificar != null)
            {
                using (var entidades = new EntidadesTutorias())
                {
                    var horarioModificar = entidades.Horarios.FirstOrDefault(horario => horario.Matricula == horarioAModificar.Matricula
                    && horario.IdTutoriaAcademica == horarioAModificar.IdTutoriaAcademica);
                    if (horarioModificar != null)
                    {
                        horarioModificar.HoraTutoria = horarioAModificar.HoraTutoria;
                        entidades.SaveChanges();
                        modificacionExitosa = true;
                    }
                }
            }

            return modificacionExitosa;
        }

        public ObservableCollection<Dominio.Horarios> ObtenerHorariosDeTutoriaPorIdTutoria(int idTutoria)
        {
            ObservableCollection<Dominio.Horarios> horariosObtenidos = new ObservableCollection<Dominio.Horarios>();
            using (var entidades = new EntidadesTutorias())
            {
                var horariosEncontrados = (from Horarios in entidades.Horarios
                                           where Horarios.IdTutoriaAcademica == idTutoria
                                           select Horarios).ToList();
                if (horariosEncontrados.FirstOrDefault() != null)
                {
                    foreach (AccesoADatos.Horarios horarioEncontrado in horariosEncontrados)
                    {
                        Dominio.Horarios horarioObtenido = new Dominio.Horarios(horarioEncontrado);
                        horariosObtenidos.Add(horarioObtenido);
                    }
                }
            }
            return horariosObtenidos;
        }

        public Boolean RegistrarAsistenciasYEstadoDeEstudiantes(Dominio.Horarios horario)
        {
            Boolean registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                var horarioEncontrado = entidades.Horarios.Find(horario.IdHorarios);
                if (horarioEncontrado != null)
                {
                    horarioEncontrado.Asistencia = horario.Asistencia;
                    horarioEncontrado.Riesgo = horario.Riesgo;
                    entidades.Horarios.AddOrUpdate(horarioEncontrado);
                    registroExitoso = entidades.SaveChanges() == 1;
                }
            }
            return registroExitoso;
        }

        public Dictionary<String, int> RecuperarTotalAsistentesYEnRiesgoDeEstudiantesPorIdTutoria(int idFechaTutoria)
        {
            Dictionary<String, int> totalAsistentesYEnRiesgo = new Dictionary<String, int>();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var horariosEncontrados = (from Tutorias in entidadesTutorias.TutoriasAcademicas
                                           join Horarios in entidadesTutorias.Horarios
                                           on Tutorias.IdTutoriaAcademica equals Horarios.IdTutoriaAcademica
                                           where Tutorias.IdFechaTuria == idFechaTutoria && Tutorias.ReporteEntregado == true
                                           select Horarios).ToList();
                if (horariosEncontrados != null)
                {
                    int numeroAsistentes = 0;
                    int numeroEnRiesgo = 0;
                    foreach (AccesoADatos.Horarios horario in horariosEncontrados)
                    {
                        if (horario.Asistencia == true)
                        {
                            numeroAsistentes++;
                        }
                        if (horario.Riesgo == true)
                        {
                            numeroEnRiesgo++;
                        }
                    }
                    totalAsistentesYEnRiesgo.Add("Asistentes", numeroAsistentes);
                    totalAsistentesYEnRiesgo.Add("EnRiesgo", numeroEnRiesgo);
                    totalAsistentesYEnRiesgo.Add("TotalEstudiantes", horariosEncontrados.Count);
                }
                else
                {
                    throw new ObjectNotFoundException();
                }
            }
            return totalAsistentesYEnRiesgo;
        }
    }
}
