using Dominio;
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

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorConsultarEstudiante.xaml
    /// </summary>
    public partial class ControladorConsultarEstudiante : Window
    {
        private Dominio.Estudiantes estudianteAModificar;
        public ControladorConsultarEstudiante(Estudiantes estudianteConsultado)
        {
            InitializeComponent();
            Label_Nombre.Content = estudianteConsultado.Nombre + " " + estudianteConsultado.ApellidoPaterno + " " + estudianteConsultado.ApellidoMaterno;
            Label_Matricula.Content = estudianteConsultado.Matricula;
            Label_CorreoPersonal.Content = estudianteConsultado.CorreoPersonal;
            Label_CorreoInstitucional.Content = estudianteConsultado.CorreoInstitucional;
            if(estudianteConsultado.Academicos == null)
            {
                Label_Tutor.Content = "Sin tutor";
            }
            else
            {
                Label_Tutor.Content = estudianteConsultado.Academicos.NombreCompleto;
            }
            if(estudianteConsultado.EnRiesgo)
            {
                Label_EnRiesgo.Content = "Si";
            }
            else
            {
                Label_EnRiesgo.Content = "No";
            }
            Label_Semestre.Content = estudianteConsultado.SemestreQueCursa.ToString();
            estudianteAModificar = estudianteConsultado;
            this.Button_ModificarEstudiante.Visibility = Visibility.Collapsed;
            ValidarRoles();
        }

        private void ValidarRoles()
        {
            //*************************QUITAR ESTE ROLE ESTATICO CUANDO SE TENGA EL LOGIN***************
            int rolCoordinador = 5610;
            //*************************QUITAR ESTOS ROLES ESTATICOS CUANDO SE TENGA EL LOGIN***************
            if (rolCoordinador == (int)EnumRolesDeUsuario.Rol_Coordinador)
            {
                this.Button_ModificarEstudiante.Visibility = Visibility.Visible;
            }
        }

        private void Button_Regresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.DataContext = null;
        }

        private void ModificarDatosEstudiante(object sender, RoutedEventArgs e)
        {
            this.Close();
            ModificarEstudiante modificarEstudiante = new ModificarEstudiante(estudianteAModificar);
            modificarEstudiante.ShowDialog();
        }
    }
}
