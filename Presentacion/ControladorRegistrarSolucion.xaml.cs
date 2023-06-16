using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorRegistrarSolucion.xaml
    /// </summary>
    public partial class ControladorRegistrarSolucion : Page
    {
        private SolucionAProblematicaAcademicaDAO solucionesDAO = new SolucionAProblematicaAcademicaDAO();
        private static int ID_PROBLEMATICA_PRUEBA = 1;
        private static int ID_ACADEMICO_PRUEBA = 1;
        public ControladorRegistrarSolucion()
        {
            InitializeComponent();
        }

        private void btnRegistrarSolucion_Click(object sender, RoutedEventArgs e)
        {
            if (solucionesDAO.BuscarSolucionPorIdProblematica(ID_PROBLEMATICA_PRUEBA))
            {
                MessageBox.Show("Ya se encuentra registrada una solución para esta problemática académica");
            }
            else
            {
                tbSolucion.Visibility = Visibility.Visible;
                btnRegistrarSolucion.Visibility = Visibility.Collapsed;
                btnAceptar.Visibility = Visibility.Visible;
                Button_Cancelar.Visibility = Visibility.Visible;
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string solucionEscrita = tbSolucion.Text;
            if (!String.IsNullOrEmpty(solucionEscrita))
            {
                Dominio.Soluciones solucion = new Dominio.Soluciones();
                solucion.Fecha = DateTime.Parse(DateTime.Now.ToShortDateString());
                solucion.Descripcion = solucionEscrita;
                //se toma desde el singleton
                solucion.IdAcademico = ID_ACADEMICO_PRUEBA;
                //se toma desde la problemática que se está viendo
                solucion.IdProblematica = ID_PROBLEMATICA_PRUEBA;
                try
                {
                    Boolean registroExitoso = solucionesDAO.RegistrarSolucion(solucion);
                    if (registroExitoso == true)
                    {
                        MessageBox.Show("La información se ha guardado con éxito");
                        Regresar();
                        tbSolucion.IsEnabled = false;
                        tbSolucion.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al registrar");
                    }
                }
                catch (DbEntityValidationException DBEVException)
                {
                    TextBloc_Error.Text = DBEVException.Message;
                }
                catch (ArgumentException AException)
                {
                    TextBloc_Error.Text = AException.Message;
                }
            }
            else
            {
                this.TextBloc_Error.Text = "Los datos no pueden estar en blanco, revise la información";
            }
        }

        private void Button_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Desea terminar la operación?", "Cancelar", MessageBoxButton.OKCancel);
            if (resultado == MessageBoxResult.OK)
            {
                Regresar();
            }
            
        }

        private void Regresar()
        {
            tbSolucion.Visibility = Visibility.Collapsed;
            btnAceptar.Visibility = Visibility.Collapsed;
            Button_Cancelar.Visibility = Visibility.Collapsed;
            btnRegistrarSolucion.Visibility= Visibility.Visible;
        }
    }
}
