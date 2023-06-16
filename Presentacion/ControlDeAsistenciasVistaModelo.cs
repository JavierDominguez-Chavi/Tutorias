using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Dominio;
using System.Runtime.CompilerServices;

namespace Presentacion
{
    public class ControlDeAsistenciasVistaModelo : INotifyPropertyChanged
    {
        public ICollectionView Tutores { private set; get; }
        public ICollectionView PeriodosEscolares { private set; get; }
        private readonly HorarioDAO horarioDAO = new HorarioDAO();
        private int idProgramaEducativoDeCoordinadorEnSesion = SingletonUsuario.Instance.IdProgramaEducativo;

        public int TotalAsistencias1 { private set; get; }
        public int TotalAsistencias2 { private set; get; }
        public int TotalAsistencias3 { private set; get; }
        public int TotalRiesgos { private set; get; }

        public int TotalGeneral1 { private set; get; }
        public int TotalGeneral2 { private set; get; }
        public int TotalGeneral3 { private set; get; }

        public double[] TotalesAsistencias { private set; get; }
        public double[] TotalesSIT { private set; get; }
        public double[] PorcentajesAsistencias { private set; get; }
        public double[] PorcentajesFaltas { private set; get; }


        public ControlDeAsistenciasVistaModelo()
        {
            this.Tutores =
                CollectionViewSource.GetDefaultView(
                    new AcademicosDAO().ObtenerTutoresPorIdProgramaEducativo(
                        idProgramaEducativoDeCoordinadorEnSesion));
            this.PeriodosEscolares =
                CollectionViewSource.GetDefaultView(
                    new PeriodosEscolaresDAO().ObtenerPeriodosEscolares());

            this.TotalesAsistencias = new double[3];
            this.TotalesSIT = new double[3];
            this.PorcentajesAsistencias = new double[3];
            this.PorcentajesFaltas = new double[3];

        }
        public void ObtenerConcentradoDeAsistencias()
        {
            foreach (Academicos tutor in this.Tutores)
            {
                if (!tutor.ObtuvoTutorados)
                {
                    ObservableCollection<Estudiantes> tutoradosObtenidos =
                        new EstudiantesDAO().ObtenerTutoradosPorProgramaEducativo(
                            tutor, this.idProgramaEducativoDeCoordinadorEnSesion);
                    foreach (Estudiantes tutorado in tutoradosObtenidos)
                    {
                        tutor.Estudiantes.Add(tutorado);
                    }
                    tutor.ObtuvoTutorados = true;
                }
                ObtenerHorariosDeTutorados(tutor);
                CalcularTotalesGenerales(tutor);
            }
            MostrarTotalesDeAcademicoSeleccionado();
        }
        private void ObtenerHorariosDeTutorados(Academicos tutor)
        {
            PeriodosEscolares periodoEscolarSeleccionado = 
                PeriodosEscolares.CurrentItem as PeriodosEscolares;
            foreach (Estudiantes tutorado in tutor.Estudiantes)
            {
                tutorado.Horarios =
                    this.horarioDAO
                        .ObtenerHorariosPorMatriculaYPeriodoEscolar
                (
                    tutorado.Matricula,
                    periodoEscolarSeleccionado.IdPeriodoEscolar
                );
                EstablecerRiesgo(tutorado);
            }
            tutor.CalcularAsistencias();
        }
        public void MostrarTotalesDeAcademicoSeleccionado()
        {
            LimpiarTotales();
            foreach (Estudiantes tutorado in (this.Tutores.CurrentItem as Academicos).Estudiantes)
            {
                this.TotalRiesgos += tutorado.EnRiesgo ? 1 : 0;
            }
            OnPropertyChanged(nameof(TotalRiesgos));
        }
        private void EstablecerRiesgo(Estudiantes tutorado) 
        {
            Horarios ultimoHorario = tutorado.Horarios.LastOrDefault(h => h.IdTutoriaAcademica > 0);
            tutorado.EnRiesgo = false;
            if (ultimoHorario != null ) 
            {
                tutorado.EnRiesgo = ultimoHorario.Riesgo;
            }
        }
        private void LimpiarTotales()
        {
            for (int i = 0; i < 3; i++)
            { 
                this.TotalesAsistencias[i] = 0;
                this.TotalesSIT[i] = 0;
                this.PorcentajesFaltas[i] = 0;
                this.PorcentajesAsistencias[i] = 0;
            }
            this.TotalAsistencias1 = 0;
            this.TotalAsistencias2 = 0;
            this.TotalAsistencias3 = 0;
            this.TotalRiesgos = 0;
        }

        private void CalcularTotalesGenerales(Academicos tutor)
        {
            for (int i = 0; i < 3; i++)
            {
                this.TotalesAsistencias[i] += tutor.Asistencias[i];
                this.TotalesSIT[i] += tutor.Estudiantes.Count;
                this.PorcentajesAsistencias[i] = (TotalesAsistencias[i] * 100) / TotalesSIT[i];
                this.PorcentajesFaltas[i] = 
                    ((TotalesSIT[i] - TotalesAsistencias[i]) * 100) / TotalesSIT[i];
            }
            OnPropertyChanged(nameof(this.TotalesAsistencias));
            OnPropertyChanged(nameof(this.TotalesSIT));
            OnPropertyChanged(nameof(this.PorcentajesAsistencias));
            OnPropertyChanged(nameof(this.PorcentajesFaltas));
            
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
