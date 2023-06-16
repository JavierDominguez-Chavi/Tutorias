using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AccesoADatos;

namespace Dominio
{
    public class Estudiantes : INotifyPropertyChanged
    {
        public Estudiantes()
        {
            this.Horarios = new ObservableCollection<Horarios>();
            this.Problematicas = new ObservableCollection<Problematicas>();
            this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
        }
        

        public Estudiantes(AccesoADatos.Estudiantes estudiante)
        {
            this.Matricula = estudiante.Matricula;
            this.Nombre = estudiante.Nombre;
            this.ApellidoPaterno = estudiante.ApellidoPaterno;
            this.ApellidoMaterno = estudiante.ApellidoMaterno;
            this.SemestreQueCursa = estudiante.SemestreQueCursa;
            this.EnRiesgo = estudiante.EnRiesgo;
            this.CorreoPersonal = estudiante.CorreoPersonal;
            this.CorreoInstitucional = estudiante.CorreoInstitucional;
            this.IdProgramaEducativo = estudiante.IdProgramaEducativo;
            this.IdAcademico = estudiante.IdAcademico;

            if (estudiante.Academicos != null)
            {
                this.Academicos = new Academicos(estudiante.Academicos);
            }
            this.Horarios = new ObservableCollection<Horarios>();
            this.Problematicas = new ObservableCollection<Problematicas>();
            this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
        }

        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public int SemestreQueCursa { get; set; }
        private bool enRiesgo;
        public bool EnRiesgo
        {
            get { return this.enRiesgo; }
            set
            { 
                this.enRiesgo = value;
                NotifyPropertyChanged();
            }
        }
        public string CorreoPersonal { get; set; }
        public string CorreoInstitucional { get; set; }
        public int IdProgramaEducativo { get; set; }
        public int? IdAcademico { get; set; }
        public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";

        public Academicos Academicos { get; set; }
        public ProgramasEducativos ProgramasEducativos { get; set; }

        private ObservableCollection<Horarios> horarios;
        public ObservableCollection<Horarios> Horarios 
        {
            get { return this.horarios; } 
            set
            {
                this.horarios = value;
                NotifyPropertyChanged();
            }
        }

        public Horarios Horario1 => ObtenerHorarioPorNumeroSesion(1);

        public Horarios Horario2 => ObtenerHorarioPorNumeroSesion(2);

        public Horarios Horario3 => ObtenerHorarioPorNumeroSesion(3);
        private Horarios ObtenerHorarioPorNumeroSesion(int numeroSesion)
        {
            return horarios.FirstOrDefault(h => h.TutoriasAcademicas.FechasTutorias.NumeroSesion==numeroSesion)
                   ?? new Horarios();
        }

        public ObservableCollection<Problematicas> Problematicas { get; set; }
        public ObservableCollection<ExperienciasEducativas_Academicos> ExperienciasEducativas_Academicos { get; set; }

        public AccesoADatos.Estudiantes AAccesoADatos()
        {
            return new AccesoADatos.Estudiantes()
            {
                Matricula = this.Matricula,
                Nombre = this.Nombre,
                ApellidoMaterno = this.ApellidoMaterno,
                ApellidoPaterno = this.ApellidoPaterno,
                SemestreQueCursa = this.SemestreQueCursa,
                EnRiesgo = this.EnRiesgo,
                CorreoPersonal = this.CorreoPersonal,
                CorreoInstitucional = this.CorreoInstitucional,
                IdProgramaEducativo = this.IdProgramaEducativo,
                IdAcademico = this.IdAcademico
            };
        }


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
