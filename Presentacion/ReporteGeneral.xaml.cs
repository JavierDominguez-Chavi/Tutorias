using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
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
using Dominio;
using LogicaDelNegocio;

namespace Presentacion
{
    public partial class ReporteGeneral : Page
    {
        Dominio.FechasTutorias FechaTutoriaReporte { get; set; }
        public ReporteGeneral(Dominio.FechasTutorias fechaConPeriodo)
        {
            InitializeComponent();
            FechaTutoriaReporte = fechaConPeriodo;
            this.DataContext = FechaTutoriaReporte;
            RecuperarTotalAsistentesYEnRiesgoDeEstudiantesPorIdTutoria();
            MostrarCabecerasReporte();
        }

        private void RecuperarTotalAsistentesYEnRiesgoDeEstudiantesPorIdTutoria()
        {
            HorarioDAO horarioDAO = new HorarioDAO();
            try
            {
                Dictionary<String, int> totalAsistentesYEnRiesgo =
                    horarioDAO.RecuperarTotalAsistentesYEnRiesgoDeEstudiantesPorIdTutoria(FechaTutoriaReporte.IdFechaTutoria);
                float porcentaje = (totalAsistentesYEnRiesgo["Asistentes"] * 100) / totalAsistentesYEnRiesgo["TotalEstudiantes"];
                this.TextBox_TotalAsistentes.Text = totalAsistentesYEnRiesgo["Asistentes"] + " " + "(" + porcentaje.ToString("F2") + 
                    "%" + ")" + " de " + totalAsistentesYEnRiesgo["TotalEstudiantes"];
                porcentaje = (totalAsistentesYEnRiesgo["EnRiesgo"] * 100) / totalAsistentesYEnRiesgo["TotalEstudiantes"];
                this.TextBox_TotalEnRiesgo.Text = totalAsistentesYEnRiesgo["EnRiesgo"] + " " + "(" + porcentaje.ToString("F2") + "%" + ")" + 
                    " de " + totalAsistentesYEnRiesgo["TotalEstudiantes"];
            }
            catch (ObjectNotFoundException ex)
            {
                MensajesHelper.FaltaDeConexion();
            }

        }

        private void MostrarCabecerasReporte()
        {
            TextBlock_FechaTutoria.Text = "FECHA TUTORÍA: " + FechaTutoriaReporte.FechaTutoria.ToString("dd MMMM yyyy");
            TextBlock_ProgramaEducativo.Text = 
                SingletonUsuario.Instance.AcademicosRoles.First().ProgramasEducativos.NombreProgramaEducativo;
        }

        private void CargarInformacionTablas(object sender, RoutedEventArgs e)
        {
            ProblematicasDAO problematicasDAO = new ProblematicasDAO();
            ObservableCollection<Dominio.Problematicas> problematicas =
                problematicasDAO.RecuperarProblematicasAcademicasPorIdFechaTutoria(FechaTutoriaReporte.IdFechaTutoria);
            if (problematicas.FirstOrDefault() != null)
            {
                CollectionViewSource ViewSource_Problematicas = ((CollectionViewSource)(this.FindResource("ViewSource_Problematicas")));
                ViewSource_Problematicas.Source = problematicas;
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
            }
            ObtenerComentariosGeneralesDeTutoriasAcademicas();
        }

        private void ObtenerComentariosGeneralesDeTutoriasAcademicas()
        {
            TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
            ObservableCollection<Dominio.TutoriasAcademicas> tutoriasAcademicas =
                tutoriasAcademicasDAO.ObtenerComentariosGeneralesDeTutoriasAcademicas(FechaTutoriaReporte.IdFechaTutoria);
            if (tutoriasAcademicas.FirstOrDefault() != null)
            {
                CollectionViewSource ViewSource_Tutorias = ((CollectionViewSource)(this.FindResource("ViewSource_Tutorias")));
                ViewSource_Tutorias.Source = tutoriasAcademicas;
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
            }
        }

        private void CerrarVentanaReporteGeneral(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
