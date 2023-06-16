using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Dominio;

namespace Presentacion
{
    /// <summary>
    /// Interaction logic for ControlDeAsistencias.xaml
    /// </summary>
    public partial class ControlDeAsistencias : Page
    {
        private ControlDeAsistenciasVistaModelo vistaModelo;
        private ConcentradoAsistenciasTutor concentradoTutor;
        private ConcentradoAsistenciasGeneral concentradoGeneral;
        private bool viendoConcentradoGeneral = false; 
        public ControlDeAsistencias()
        {
            InitializeComponent();
            this.vistaModelo = this.DataContext as ControlDeAsistenciasVistaModelo;
            this.concentradoTutor = new ConcentradoAsistenciasTutor(vistaModelo);
            this.concentradoGeneral = new ConcentradoAsistenciasGeneral(vistaModelo);
            this.Frame_ConsultaGeneral.Navigate(concentradoGeneral);
            this.Frame_Concentrado.Navigate(concentradoTutor);
        }

        private void ComboBox_PeriodosEscolares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                this.vistaModelo.ObtenerConcentradoDeAsistencias();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.viendoConcentradoGeneral = true;
            this.Frame_Concentrado.Navigate(concentradoGeneral);
        }
        private void ObtenerTutores(object sender, RoutedEventArgs e)
        {
            this.vistaModelo.ObtenerConcentradoDeAsistencias();
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
