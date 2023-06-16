using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicaDelNegocio;
using Presentacion.Properties;

namespace Presentacion
{
    public partial class DetallesTutorAcademico : Page
    {
        public Dominio.AcademicosRoles TutorAcademico { get; set; }
        public DetallesTutorAcademico(Dominio.AcademicosRoles academicosRolSeleccionado)
        {
            InitializeComponent();
            TutorAcademico = academicosRolSeleccionado;
            this.DataContext = TutorAcademico;
        }

        private void CargarInformacionPagina(object sender, RoutedEventArgs e)
        {
            EstudiantesDAO estudiantesDAO = new EstudiantesDAO();
            ObservableCollection<Dominio.Estudiantes> estudiantesTutor = 
                new ObservableCollection<Dominio.Estudiantes>(estudiantesDAO.ObtenerEstudiantesPorIdAcademico(TutorAcademico.idAcademico));
            if (estudiantesTutor.FirstOrDefault() != null)
            {
                CollectionViewSource ViewSource_ProgramasEducativos = ((CollectionViewSource)(this.FindResource("ViewSource_Estudiantes")));
                ViewSource_ProgramasEducativos.Source = estudiantesTutor;
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
                this.NavigationService.GoBack();
            }
        }

        private void RegresarPaginaAnterior(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
