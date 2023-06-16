using Dominio;
using GongSolutions.Wpf.DragDrop;
using LogicaDelNegocio;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Presentacion
{
    public class TutoresYTutoradosVistaModelo : IDropTarget
    {
        public ICollectionView Tutores { private set; get; }
        public  ICollectionView EstudiantesSinTutor { private set; get; }

        private Dictionary<String, Estudiantes> estudiantesReasignados;

        private AcademicosDAO academicosDAO;

        private EstudiantesDAO estudiantesDAO;

        private int idProgramaEducativoDeCoordinadorEnSesion = SingletonUsuario.Instance.IdProgramaEducativo;
        public TutoresYTutoradosVistaModelo() 
        {
            this.academicosDAO = new AcademicosDAO();
            this.estudiantesDAO = new EstudiantesDAO();
            this.estudiantesReasignados = new Dictionary<String, Estudiantes>();

            this.Tutores = CollectionViewSource.GetDefaultView(
                academicosDAO.ObtenerTutoresPorIdProgramaEducativo(idProgramaEducativoDeCoordinadorEnSesion));

            this.EstudiantesSinTutor =
                CollectionViewSource.GetDefaultView(
                    estudiantesDAO.ObtenerEstudiantesSinTutorPorProgramaEducativo(
                        this.idProgramaEducativoDeCoordinadorEnSesion));

        }

        public void DragEnter(IDropInfo dropInfo)
        {
        }

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.Effects = DragDropEffects.Move;
            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
        }

        public void DragLeave(IDropInfo dropInfo)
        {
        }

        public void Drop(IDropInfo dropInfo)
        {
            IList estudiantes = new ObservableCollection<Estudiantes>();
            if (dropInfo.Data is Estudiantes)
            {
                estudiantes.Add(dropInfo.Data as Estudiantes);
            }
            else
            {
                estudiantes = dropInfo.Data as IList;
            }
            ValidarNumeroDeTutorados(dropInfo, estudiantes);
        }

        private void ValidarNumeroDeTutorados(IDropInfo dropInfo, IList estudiantes)
        {
            Academicos tutorSeleccionado = (dropInfo.TargetItem as Academicos);
            int tutoradosActuales = tutorSeleccionado.NumeroTutorados;
            int tutoradosNuevos = estudiantes.Count;
            MessageBoxResult messageBoxResult = MessageBoxResult.Yes;
            if ((tutoradosActuales + tutoradosNuevos) > 30)
            { 
                messageBoxResult = 
                    System.Windows.MessageBox.Show($"El tutor {tutorSeleccionado.NombreCompleto} " +
                                                   $"tendrá {tutoradosActuales + tutoradosNuevos} " +
                                                   $"tutorados. ¿Está seguro de hacer esta asignación?", 
                                                   "Asignación supera los 30 tutorados.", 
                                                   System.Windows.MessageBoxButton.YesNo);
            }
            if (messageBoxResult == MessageBoxResult.Yes) 
            { 
                AsignarEstudiantes(tutorSeleccionado, estudiantes);
            }
        }

        private void AsignarEstudiantes(Academicos tutorSeleccionado, IList estudiantes)
        {
            foreach (Estudiantes estudiante in estudiantes)
            {
                tutorSeleccionado.AsignarTutorado(estudiante);

                Academicos tutorAnterior = estudiante.Academicos;
                if (tutorAnterior != null)
                {
                    tutorAnterior.RemoverTutorado(estudiante);
                }
                else
                {
                    (this.EstudiantesSinTutor.SourceCollection as ObservableCollection<Estudiantes>).Remove(estudiante);
                }
                estudiante.IdAcademico = tutorSeleccionado.IdAcademico;
                estudiante.Academicos = tutorSeleccionado;
                ActualizarEstudiantesReasignados(estudiante);
            }
        }

        private void ActualizarEstudiantesReasignados(Estudiantes estudiante)
        {
            if (this.estudiantesReasignados.ContainsKey(estudiante.Matricula))
            {
                estudiantesReasignados[estudiante.Matricula] = estudiante;
            }
            else
            {
                estudiantesReasignados.Add(estudiante.Matricula, estudiante);
            }
        }

        public bool GuardarAsignaciones()
        {
            int asignacionesExitosas = this.estudiantesDAO.AsignarTutorAEstudiantes(estudiantesReasignados); ;
            return asignacionesExitosas == this.estudiantesReasignados.Count;
        }

        public void FiltrarEstudiantes(String consulta)
        {

            Predicate<object> predicado =
                objeto =>
                {
                    Estudiantes estudiante = objeto as Estudiantes;
                    return estudiante.NombreCompleto.Contains(consulta)
                            || estudiante.Matricula.Contains(consulta);
                };
            this.EstudiantesSinTutor.Filter = predicado;
            ((Academicos)this.Tutores.CurrentItem).CollectionView_Estudiantes.Filter = predicado;
        }

        public void ObtenerTutorados(Academicos tutor)
        {
            ObservableCollection<Estudiantes> tutoradosObtenidos =
                this.estudiantesDAO.ObtenerTutoradosPorProgramaEducativo(
                    tutor, this.idProgramaEducativoDeCoordinadorEnSesion);
            foreach (Estudiantes tutorado in tutoradosObtenidos)
            {
                tutorado.Academicos = tutor;
                tutor.Estudiantes.Add(tutorado);
            }
        }
    }
}
