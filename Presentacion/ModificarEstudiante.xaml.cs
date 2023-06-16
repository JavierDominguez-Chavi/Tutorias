using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Presentacion.Properties;
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    public partial class ModificarEstudiante : Window
    {
        private String matriculaEstudiante;
        public Dominio.Estudiantes Estudiante { get; set; }
        private Boolean activarConfirmacionCancelar = true;
        public ModificarEstudiante(Dominio.Estudiantes estudianteAModificar)
        {
            InitializeComponent();
            Estudiante = estudianteAModificar;
            matriculaEstudiante = estudianteAModificar.Matricula;
            this.DataContext = Estudiante;
        }

        private void BloquearCaracteresNoNumericos(object sender, TextCompositionEventArgs textCompositionEvent)
        {
            Regex regex = new Regex("[^0-9]+");
            textCompositionEvent.Handled = regex.IsMatch(textCompositionEvent.Text);
        }

        private void GuardarEstudianteModificado(object sender, RoutedEventArgs e)
        {
            LimpiarMensajesInvalidos();
            Boolean camposAlfanumericos = ValidarCamposAlfanumerico();
            Boolean camposAlfabeticos = ValidarCamposAlfabeticos();
            Boolean camposCorreoValidos = ValidarCamposCorreo();
            Boolean limiteDeCaracteres = ValidarLongitudDeCampos();
            if (camposAlfanumericos && camposAlfabeticos && camposCorreoValidos && limiteDeCaracteres)
            {
                MessageBoxResult resultadoMessageBox =
                MessageBox.Show(mensajes.messageBoxText_ConfirmacionCambios, mensajes.messageBoxTitle_ConfirmacionCambios,
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (resultadoMessageBox == MessageBoxResult.Yes)
                {
                    ValidarEstudianteComoNuevo();
                }
            }

        }

        private void LimpiarMensajesInvalidos()
        {
            this.TextBlock_MatriculaInvalida.Text = "";
            this.TexBlock_NombreInvalido.Text = "";
            this.TextBlock_ApellidoPaternoInvalido.Text = "";
            this.TextBlock_ApellidoMaternoInvalido.Text = "";
            this.TextBlock_CorreoPersonalInvalido.Text = "";
            this.TextBlock_CorreoInstitucionalInvalido.Text = "";
        }
        private Boolean ValidarCamposAlfanumerico()
        {
            Boolean camposAlfanumericos = true;
            if (!Validador.ValidarCadenaAlfanumerica(this.TextBox_MatriculaEstudiante.Text))
            {
                this.TextBlock_MatriculaInvalida.Text = mensajes.message_ContenidoAlfanumericoRequerido;
                camposAlfanumericos = false;
            }
            return camposAlfanumericos;
        }

        private Boolean ValidarCamposAlfabeticos()
        {
            Boolean camposAlfabeticos = true;
            if (!Validador.ValidarCadenaAlfabetica(this.TextBox_NombreEstudiante.Text))
            {
                this.TexBlock_NombreInvalido.Text = mensajes.message_ContenidoAlfabeticoRequerido;
                camposAlfabeticos = false;
            }
            if (!Validador.ValidarCadenaAlfabetica(this.TextBox_ApellidoPaternoEstudiante.Text))
            {
                this.TextBlock_ApellidoPaternoInvalido.Text = mensajes.message_ContenidoAlfabeticoRequerido;
                camposAlfabeticos = false;
            }
            if (!Validador.ValidarCadenaAlfabetica(this.TextBox_ApellidoMaternoEstudiante.Text))
            {
                this.TextBlock_ApellidoMaternoInvalido.Text = mensajes.message_ContenidoAlfabeticoRequerido;
                camposAlfabeticos = false;
            }
            return camposAlfabeticos;
        }

        private Boolean ValidarCamposCorreo()
        {
            Boolean correoValido = true;
            if (!Validador.ValidarCorreoElectronico(this.TextBox_CorreoInstitucionalEstudiante.Text))
            {
                this.TextBlock_CorreoInstitucionalInvalido.Text = mensajes.message_ContenidoCorreoInstitucionalInvalido;
                correoValido = false;
            }
            if (!Validador.ValidarCorreoElectronico(this.TextBox_CorreoPersonalEstudiante.Text))
            {
                this.TextBlock_CorreoPersonalInvalido.Text = mensajes.message_ContenidoCorreoPersonalInvalido;
                correoValido = false;
            }
            return correoValido;
        }

        private Boolean ValidarLongitudDeCampos()
        {
            Boolean longitudValida = true;
            if (!Validador.ValidarLongitudCaracteresNombreYApellidos(Estudiante.Nombre) ||
                !Validador.ValidarLongitudCaracteresNombreYApellidos(Estudiante.ApellidoMaterno) ||
                !Validador.ValidarLongitudCaracteresNombreYApellidos(Estudiante.ApellidoMaterno))
            {
                MessageBox.Show(mensajes.messageBoxText_CamposNombreYApellidosExcedenLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            if (!Validador.ValidarLongitudCaracteresCorreoElectronicoPersonal(Estudiante.CorreoPersonal))
            {
                MessageBox.Show(mensajes.messageBoxText_CampoCorreoPersonalExcedeLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            if (!Validador.ValidarLongitudCaracteresCorreoElectronicoInstitucional(Estudiante.CorreoInstitucional))
            {
                MessageBox.Show(mensajes.messageBoxText_CampoCorreoInstitucionalExcedeLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            if (!Validador.ValidarLongitudCaracteresMatricula(Estudiante.Matricula))
            {
                MessageBox.Show(mensajes.messageBoxText_CampoMatriculaExcedeLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            return longitudValida;
        }

        private void ValidarEstudianteComoNuevo()
        {
            EstudiantesDAO estudiantesDAO = new EstudiantesDAO();
            if (!estudiantesDAO.ValidarEstudianteComoNuevo(Estudiante))
            {
                GuardarCambiosEstudiante();
            }
            else 
            {
                MessageBox.Show(mensajes.messageBoxText_EstudianteExistente, mensajes.messageBoxTitle_EstudianteExistente, 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void GuardarCambiosEstudiante()
        {
            EstudiantesDAO estudiantesDAO = new EstudiantesDAO();
            Boolean actualizado = false;
            if (matriculaEstudiante.Equals(Estudiante.Matricula))
            {
                actualizado = estudiantesDAO.ActualizarEstudiante(matriculaEstudiante, Estudiante);
                ValidarActualizacionExitosa(actualizado);
            }
            else
            {
                actualizado = estudiantesDAO.ActualizarEstudianteConMatricula(matriculaEstudiante, Estudiante);
                ValidarActualizacionExitosa(actualizado);
            }
        }

        private void ValidarActualizacionExitosa(Boolean actualizacionExitosa)
        {
            if (actualizacionExitosa)
            {
                MensajesHelper.OperacionExitosa();
                activarConfirmacionCancelar = false;
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
            }
        }
        private void CancelarModificacion(object sender, RoutedEventArgs e)
        {
            if (activarConfirmacionCancelar)
            {
                MessageBoxResult resultadoMessageBox =
                   MessageBox.Show(mensajes.messageBoxText_CancelarOperacion, mensajes.messageBoxTitle_CancelarOperacion,
                   MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultadoMessageBox == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else 
            {
                this.Close();
            }
        }
    }
}
