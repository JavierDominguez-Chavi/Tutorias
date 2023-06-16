using Dominio;
using LogicaDelNegocio;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorModificarSolucionAProblematica.xaml
    /// </summary>
    public partial class ControladorModificarSolucionAProblematica : Page
    {
        AccesoADatos.Soluciones solucion;
        public ControladorModificarSolucionAProblematica(AccesoADatos.Soluciones solucionRecibida, Problematicas problematica, String experienciaEducativa, Academicos academico)
        {
            InitializeComponent();
            RTxtDescripcionProblematica.IsEnabled = false;
            LbExperienciaEducativa.Content = experienciaEducativa;
            LbNRC.Content = problematica.NRC;
            LbProfesor.Content = academico.Nombre + " " + academico.ApellidoPaterno + " " + academico.ApellidoMaterno;
            LbTituloProblematica.Content = problematica.Titulo;
            RunDescripcionProblematica.Text = problematica.Descripcion;
            RunSolucion.Text = solucionRecibida.Descripcion;
            solucion = new AccesoADatos.Soluciones()
            {
                IdSolucion = solucionRecibida.IdSolucion,
                IdAcademico = solucionRecibida.IdAcademico,
                IdProblematica = solucionRecibida.IdProblematica,
                Fecha = solucionRecibida.Fecha,
                Descripcion = solucionRecibida.Descripcion
            };
        }

        private void Button_Click_Guardar(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(
                    RTxtSolucion.Document.ContentStart,
                    RTxtSolucion.Document.ContentEnd
                  );
            if (textRange.IsEmpty == true)
            {


                solucion.Descripcion = textRange.Text;
                SolucionAProblematicaAcademicaDAO SolucionDao = new SolucionAProblematicaAcademicaDAO();
                if (SolucionDao.ModificarSolucion(solucion))
                {
                    this.NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Realizado sin exito");
                }
            }
            else
            {
                MessageBox.Show("No puede dejar campos en blanco", "Campos en blanco", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_Cancelar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

    }
}

