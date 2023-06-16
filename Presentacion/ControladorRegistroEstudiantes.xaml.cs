using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailValidation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using LogicaDelNegocio;
using Dominio;
using System.Data.Entity.Infrastructure;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorRegistroEstudiantes.xaml
    /// </summary>
    public partial class ControladorRegistroEstudiantes : Page
    {
        public ControladorRegistroEstudiantes()
        {
            InitializeComponent();
        }
        public string nombre = "";
        public string apellidoPaterno = "";
        public string apellidoMaterno = "";
        public string matricula = "";
        public string correoPersonal = "";
        public string correoInstitucional = "";
        public int idProgramaEducativo = SingletonUsuario.Instance.AcademicosRoles.First().ProgramasEducativos.IdProgramaEducativo;
        public int semestre = 0;
        public bool enRiesgo = false;
        EstudiantesDAO estudiantesDAO = new EstudiantesDAO();

        private void registrarEstudiante(object sender, RoutedEventArgs e)
        {
            try
            {
                nombre = TextBox_Nombres.Text;
                apellidoPaterno = TextBox_ApellidoPaterno.Text;
                apellidoMaterno = TextBox_ApellidoMaterno.Text;
                matricula = TextBox_Matricula.Text;
                correoPersonal = TextBox_CorreoPersonal.Text;
                correoInstitucional = TextBox_CorreoInstitucional.Text;
                semestre = int.Parse(ComboBox_Semestre.Text);
                bool sinCamposVacios = validarCamposVacíos();
                bool camposCorrectos = validarCampos();
                if (sinCamposVacios)
                {
                    if (camposCorrectos)
                    {
                        bool usuarioNoRegistrado = estudiantesDAO.EstudianteNoExistente(matricula);
                        if (usuarioNoRegistrado)
                        {
                            try
                            {
                                Dominio.Estudiantes estudianteNuevo = new Dominio.Estudiantes();
                                estudianteNuevo.Matricula = matricula;
                                estudianteNuevo.Nombre = nombre;
                                estudianteNuevo.CorreoPersonal = correoPersonal;
                                estudianteNuevo.CorreoInstitucional = correoInstitucional;
                                estudianteNuevo.ApellidoPaterno = apellidoPaterno;
                                estudianteNuevo.ApellidoMaterno = apellidoMaterno;
                                estudianteNuevo.EnRiesgo = enRiesgo;
                                estudianteNuevo.IdProgramaEducativo = idProgramaEducativo;
                                estudianteNuevo.SemestreQueCursa = semestre;
                                bool registroRealizado = estudiantesDAO.RegistrarEstudiante(estudianteNuevo);
                                if (registroRealizado)
                                {
                                    MessageBox.Show("Se ha registrado correctamente", "Notificación", MessageBoxButton.OK);
                                    limpiarCampos();
                                }
                            }
                            catch(DbUpdateException)
                            {
                                MessageBox.Show("No se pudo conectar con la base de datos. Por favor, inténtelo más tarde", "Error", MessageBoxButton.OK);
                                limpiarCampos();
                            }
                        }
                        else
                        {
                            MessageBox.Show("El estudiante ya se encuentra registrado en el sistema", "Error", MessageBoxButton.OK);
                            limpiarCampos();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Los datos ingresados son incorrectos, por favor verifique la información", "Error", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("No puede dejar ningún campo vacío. Por favor, verifique que todos los campos estén llenos", "Error", MessageBoxButton.OK);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("No puede dejar ningún campo vacío. Por favor, verifique que todos los campos estén llenos", "Error", MessageBoxButton.OK);
            }
            
        }
        public void limpiarCampos()
        {
            TextBox_Nombres.Clear();
            TextBox_ApellidoPaterno.Clear();
            TextBox_ApellidoMaterno.Clear();
            TextBox_Matricula.Clear();
            TextBox_CorreoPersonal.Clear();
            TextBox_CorreoInstitucional.Clear();
        }
        public bool validarCamposVacíos()
        {
            bool camposLlenos = false;
            if(!String.IsNullOrWhiteSpace(nombre) && !String.IsNullOrWhiteSpace(apellidoPaterno) && !String.IsNullOrWhiteSpace(matricula) && !String.IsNullOrWhiteSpace(correoPersonal) && !String.IsNullOrWhiteSpace(correoInstitucional) && !String.IsNullOrWhiteSpace(matricula.ToString()))
			{
				camposLlenos = true;
			}
            return camposLlenos;
        }
        public bool validarCampos()
        {
            bool camposCorrectos = false;
            camposCorrectos = EmailValidator.Validate(correoPersonal);
            camposCorrectos = camposCorrectos && EmailValidator.Validate(correoInstitucional);
            camposCorrectos = camposCorrectos && matricula.Length == 9;
            camposCorrectos = camposCorrectos && matricula.Substring(0, 1) == "S";
            camposCorrectos = camposCorrectos && (correoInstitucional == "z" + matricula + "@estudiantes.uv.mx" || correoInstitucional == "z" + matricula.Substring(0,1).ToLower() + matricula.Substring(1,8) + "@estudiantes.uv.mx");
            camposCorrectos = camposCorrectos && validarCaracteresEspeciales(nombre);
            camposCorrectos = camposCorrectos && validarCaracteresEspeciales(apellidoPaterno);
            camposCorrectos = camposCorrectos && validarCaracteresEspeciales(apellidoMaterno);
            return camposCorrectos;
        }
        public bool validarCaracteresEspeciales(string campoVerificar)
        {
            bool sinCaracteresEspeciales = true;
            string especialCharacters = "*#+-_;.@%&/()=!?¿¡{}[]^<>";
            foreach (char character in campoVerificar)
            {
                if (character >= '0' && character <= '9')
                {
                    sinCaracteresEspeciales = false;
                }
            }
            foreach (char character in campoVerificar)
            {
                foreach (char especialCharacter in especialCharacters)
                {
                    if (character == especialCharacter)
                    {
                        sinCaracteresEspeciales = true;
                    }

                }

                if (sinCaracteresEspeciales)
                {
                    break;
                }
            }
            return sinCaracteresEspeciales;
        }

        private void Button_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cancelar el registro de estudiante?", "Confirmación", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                this.NavigationService.Navigate(new PaginaPrincipalCoordinadorDeTutorias());
            }
        }
    }
}
