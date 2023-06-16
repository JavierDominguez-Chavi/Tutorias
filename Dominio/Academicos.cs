using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace Dominio
{
    public class Academicos : INotifyPropertyChanged
    {
        public Academicos()
        {
            this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
            this.Estudiantes = new ObservableCollection<Estudiantes>();
            this.CollectionView_Estudiantes = CollectionViewSource.GetDefaultView(this.Estudiantes);
            this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
            this.Soluciones = new ObservableCollection<Soluciones>();
            this.Asistencias = new int[3];
        }

        public Academicos(AccesoADatos.Academicos academico)
        {
            try
            {
                this.IdAcademico = academico.IdAcademico;
                this.Nombre = academico.Nombre;
                this.ApellidoPaterno = academico.ApellidoPaterno;
                this.ApellidoMaterno = academico.ApellidoMaterno;
                this.CorreoPersonal = academico.CorreoPersonal;
                this.CorreoInstitucional = academico.CorreoInstitucional;

                this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
                this.Estudiantes = new ObservableCollection<Estudiantes>();

                this.CollectionView_Estudiantes = CollectionViewSource.GetDefaultView(this.Estudiantes);
                this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
                this.Soluciones = new ObservableCollection<Soluciones>();

                this.Asistencias = new int[3];
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Estudiantes sin tutor");
            }

        }

        public AccesoADatos.Academicos ConvertirAcademicoDominioEnAcademicoAccesoADatos()
        {
            return new AccesoADatos.Academicos()
            {
                Nombre = this.Nombre,
                ApellidoMaterno = this.ApellidoMaterno,
                ApellidoPaterno = this.ApellidoPaterno,
                CorreoPersonal = this.CorreoPersonal,
                CorreoInstitucional  = this.CorreoInstitucional
            };
        }

        override
        public String ToString()
        {
            return this.Nombre + " " + this.ApellidoPaterno + " " + this.ApellidoMaterno;
        }

        public int IdAcademico { get; set; }
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string CorreoPersonal { get; set; }
        public string CorreoInstitucional { get; set; }
        public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";
        public string ApellidosNombre => $"{ApellidoPaterno} {ApellidoMaterno} {Nombre}";
        private int numeroTutorados;

        public Boolean AcademicoSeleccionado { get; set; }

        public int[] Asistencias { set; get; }
        public void CalcularAsistencias()
        {
            Asistencias[0] = 0;
            Asistencias[1] = 0;
            Asistencias[2] = 0;
            foreach (Estudiantes tutorado in this.Estudiantes)
            {
                Asistencias[0] += tutorado.Horario1.Asistencia ? 1 : 0;
                Asistencias[1] += tutorado.Horario2.Asistencia ? 1 : 0;
                Asistencias[2] += tutorado.Horario3.Asistencia ? 1 : 0;
            }
            OnPropertyChanged(nameof(Asistencias));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int NumeroTutorados
        {
            get { return this.numeroTutorados; }
            set
            {
                this.numeroTutorados = value;
                OnPropertyChanged();
            }
        }

        public bool ObtuvoTutorados { set; get; }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void AsignarTutorado(Estudiantes estudiante)
        {
            this.Estudiantes.Add(estudiante);
            this.NumeroTutorados = this.NumeroTutorados + 1;
        }
        public void RemoverTutorado(Estudiantes estudiante)
        {
            this.Estudiantes.Remove(estudiante);
            this.NumeroTutorados = this.NumeroTutorados - 1;
        }


        public ObservableCollection<AcademicosRoles> AcademicosRoles { get; set; }

        public ObservableCollection<Estudiantes> Estudiantes { get; set; }
        public ICollectionView CollectionView_Estudiantes { get; set; }

        public ObservableCollection<ExperienciasEducativas_Academicos> ExperienciasEducativas_Academicos { get; set; }

        public ObservableCollection<Soluciones> Soluciones { get; set; }

       
    }
}
