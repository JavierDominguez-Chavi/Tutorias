using System;

namespace Dominio
{
    public class Horarios
    {
        public Horarios() { }
        public Horarios(AccesoADatos.Horarios horario) 
        {
            this.IdHorarios = horario.IdHorarios;
            this.Asistencia = horario.Asistencia;
            this.Riesgo = horario.Riesgo;
            this.HoraTutoria = horario.HoraTutoria;
            this.IdTutoriaAcademica = horario.IdTutoriaAcademica;
            this.Matricula = horario.Matricula;

            this.Estudiantes = new Estudiantes(horario.Estudiantes);
        }
        public AccesoADatos.Horarios AAccesoADatos()
        {
            return new AccesoADatos.Horarios()
            {
                IdHorarios = this.IdHorarios,
                Asistencia = this.Asistencia,
                Riesgo = this.Riesgo,
                HoraTutoria = this.HoraTutoria,
                IdTutoriaAcademica = this.IdTutoriaAcademica,
                Matricula = this.Matricula,
            };
        }
        public int IdHorarios { get; set; }
        public bool Asistencia { get; set; }
        public bool Riesgo { get; set; }
        public TimeSpan HoraTutoria { get; set; }
        public int IdTutoriaAcademica { get; set; }
        public string Matricula { get; set; }

        public Estudiantes Estudiantes { get; set; }
        public TutoriasAcademicas TutoriasAcademicas { get; set; } = new TutoriasAcademicas();
    }
}
