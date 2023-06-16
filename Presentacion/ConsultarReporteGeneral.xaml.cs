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
using AccesoADatos;
using Dominio;
using LogicaDelNegocio;
using Presentacion.Properties;
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    public partial class ConsultarReporteGeneral : Page
    {
        public ConsultarReporteGeneral()
        {
            InitializeComponent();
        }

        private void ObtenerTodosLosPeriodosEscolares(object sender, RoutedEventArgs e)
        {
            PeriodosEscolaresDAO periodosEscolaresDAO = new PeriodosEscolaresDAO();
            ObservableCollection<Dominio.PeriodosEscolares> periodosEscolares = periodosEscolaresDAO.ObtenerPeriodosEscolares();
            if (periodosEscolares.FirstOrDefault() != null)
            {
                CollectionViewSource ViewSource_PeriodosEscolares = ((CollectionViewSource)(this.FindResource("ViewSource_PeriodosEscolares")));
                ViewSource_PeriodosEscolares.Source = periodosEscolares;
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
            }
        }

        private void ObtenerSesionesPeriodoSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            Dominio.PeriodosEscolares periodoEscolarSeleccionado = (Dominio.PeriodosEscolares) DataGrid_PeriodosEscolares.SelectedItem;
            FechasTutoriasDAO fechasTutoriasDAO = new FechasTutoriasDAO();
            ObservableCollection<Dominio.FechasTutorias> fechasPeriodo = 
                fechasTutoriasDAO.ObtenerFechasTutoriasPeriodoEscolar(periodoEscolarSeleccionado.IdPeriodoEscolar);
            if (fechasPeriodo.FirstOrDefault() != null)
            {
                CollectionViewSource ViewSource_SesionesPeriodo = ((CollectionViewSource)(this.FindResource("ViewSource_SesionesPeriodo")));
                ViewSource_SesionesPeriodo.Source = fechasPeriodo;
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
            }
        }


        private void ConsultaReporteGeneral(object sender, RoutedEventArgs e)
        {
            if (ValidarCamposSeleccionados())
            {
                Dominio.FechasTutorias fechaSeleccionada = (Dominio.FechasTutorias)DataGrid_SesionesPeriodo.SelectedItem;
                fechaSeleccionada.PeriodosEscolares = (Dominio.PeriodosEscolares)DataGrid_PeriodosEscolares.SelectedItem;
                try
                {
                    if (ValidarQueExistanReportesDeTutoria(fechaSeleccionada))
                    {
                        ReporteGeneral paginaReporteGeneral = new ReporteGeneral(fechaSeleccionada);
                        this.NavigationService.Navigate(paginaReporteGeneral);
                    }
                    else
                    {
                        MessageBox.Show(mensajes.messageBoxText_SinReportesRegistrados, mensajes.messageBoxTitle_SinReportesRegistrados,
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (ObjectNotFoundException ex)
                {
                    MensajesHelper.FaltaDeConexion();
                }
            }
            else 
            {
                MessageBox.Show(mensajes.messageBoxText_CamposNoSeleccionados, mensajes.messageBoxTitle_CamposVacios,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private Boolean ValidarCamposSeleccionados()
        {
            if (DataGrid_PeriodosEscolares.SelectedItem != null && DataGrid_SesionesPeriodo.SelectedItem != null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private bool ValidarQueExistanReportesDeTutoria(Dominio.FechasTutorias fechaSeleccionada)
        {
            TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
            if (tutoriasAcademicasDAO.ValidarQueExistanReportesDeTutoria(fechaSeleccionada.IdFechaTutoria))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SalirVentanaConsultarReporteGeneral(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
