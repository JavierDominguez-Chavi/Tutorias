using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
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
using Presentacion.Properties;
using Xceed.Wpf.AvalonDock.Controls;
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    public partial class RegistrarAcademico : Page
    {
        private Academicos academico = new Academicos();
        private int numeroProgramasEducativosNoAsignados = 0;
        private bool academicoRegistrado = false;
        ObservableCollection<Dominio.ProgramasEducativos> programasEducativos;
        public RegistrarAcademico()
        {
            InitializeComponent();
        }

        private void CargarInformacionPagina(object sender, RoutedEventArgs e)
        {
            ProgramasEducativosDAO programasEducativosDAO = new ProgramasEducativosDAO();
            programasEducativos =
                new ObservableCollection<Dominio.ProgramasEducativos>(programasEducativosDAO.ObtenerProgramasEducativos());
            if (programasEducativos.FirstOrDefault() != null)
            {
                CollectionViewSource ViewSource_ProgramasEducativos =
                        ((CollectionViewSource)(this.FindResource("ViewSource_ProgramasEducativos")));
                ViewSource_ProgramasEducativos.Source = programasEducativos;
            }
            else
            {
                MessageBox.Show(mensajes.messageBoxText_Conexion, mensajes.messageBoxTitle_Conexion,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void RegistrarNuevoAcademico(object sender, RoutedEventArgs e)
        {
            LimpiarMensajesInvalidos();
            Boolean camposNombreValidos = true;
            Boolean camposCorreoValidos = true;
            Boolean programasEducativosSeleccionados = true;
            Boolean limiteDeCaracteres = true;
            camposNombreValidos = ValidarCamposAlfabeticos();
            camposCorreoValidos = ValidarCamposCorreo();
            programasEducativosSeleccionados = ValidarProgramasEducativosSeleccionados();
            limiteDeCaracteres = ValidarLongitudDeCampos();
            if (camposNombreValidos && camposCorreoValidos && programasEducativosSeleccionados && limiteDeCaracteres)
            {
                academico.Nombre = this.TextBox_NombreAcademico.Text;
                academico.ApellidoPaterno = this.TextBox_ApellidoPaternoAcademico.Text;
                academico.ApellidoMaterno = this.TextBox_ApellidoMaternoAcademico.Text;
                academico.CorreoPersonal = this.TextBox_CorreoPersonalAcademico.Text;
                academico.CorreoInstitucional = this.TextBox_CorreoInstitucionalAcademico.Text;
                ValidarAcademicoComoNuevo();
            }
        }

        private void LimpiarMensajesInvalidos()
        {
            this.TextBlock_NombreInvalido.Text = "";
            this.TexBlock_ApellidoPaternoInvalido.Text = "";
            this.TextBlock_ApellidoMaternoInvalido.Text = "";
            this.TextBlock_CorreoPersonalInvalido.Text = "";
            this.TextBlock_CorreoInstitucionalInvalido.Text = "";
        }

        private Boolean ValidarCamposAlfabeticos() 
        {
            Boolean camposAlfabeticos = true;
            if (!Validador.ValidarCadenaAlfabetica(this.TextBox_NombreAcademico.Text))
            {
                this.TextBlock_NombreInvalido.Text = mensajes.message_ContenidoAlfabeticoRequerido;
                camposAlfabeticos = false;
            }
            if (!Validador.ValidarCadenaAlfabetica(this.TextBox_ApellidoPaternoAcademico.Text))
            { 
                this.TexBlock_ApellidoPaternoInvalido.Text = mensajes.message_ContenidoAlfabeticoRequerido;
                camposAlfabeticos = false;
            }
            if (!Validador.ValidarCadenaAlfabetica(this.TextBox_ApellidoMaternoAcademico.Text))
            {
                this.TextBlock_ApellidoMaternoInvalido.Text = mensajes.message_ContenidoAlfabeticoRequerido;
                camposAlfabeticos = false;
            }
            return camposAlfabeticos;
        }

        private Boolean ValidarCamposCorreo()
        {
            Boolean correoValido = true;
            if (!Validador.ValidarCorreoElectronico(this.TextBox_CorreoInstitucionalAcademico.Text))
            {
                this.TextBlock_CorreoInstitucionalInvalido.Text = mensajes.message_ContenidoCorreoInstitucionalInvalido;
                correoValido = false;
            }
            if (!Validador.ValidarCorreoElectronico(this.TextBox_CorreoPersonalAcademico.Text))
            {
                this.TextBlock_CorreoPersonalInvalido.Text = mensajes.message_ContenidoCorreoPersonalInvalido;
                correoValido = false;
            }
            return correoValido;
        }


        private Boolean ValidarProgramasEducativosSeleccionados()
        {
            bool programasEducativosSeleccionados = true;
            int numeroDeProgramasEducativosSeleccionados = 0;
            foreach (ProgramasEducativos programasEducativos in DataGrid_ProgramasEducativos.ItemsSource)
            {
                if (programasEducativos.ProgramaEducativoSeleccionado)
                {
                    numeroDeProgramasEducativosSeleccionados++;
                }
            }
            programasEducativosSeleccionados=
                ValidarNumeroProgramasEducativosSeleccionados(numeroDeProgramasEducativosSeleccionados, programasEducativosSeleccionados);
            
           return programasEducativosSeleccionados;
        }


        private Boolean ValidarNumeroProgramasEducativosSeleccionados(int numeroDeProgramasEducativosSeleccionados, 
                                                                bool programaEducativosSeleccionados)
        {
            if (numeroDeProgramasEducativosSeleccionados == 0)
            {
                MessageBox.Show(mensajes.messageBoxText_ProgramasEducativosNoSeleccionados, mensajes.messageBoxTitle_ProgramasEducativosNoSeleccionados,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                programaEducativosSeleccionados = false;
            }
            return programaEducativosSeleccionados;
        }

        private Boolean ValidarLongitudDeCampos()
        { 
            Boolean longitudValida = true;
            if (!Validador.ValidarLongitudCaracteresNombreYApellidos(this.TextBox_ApellidoMaternoAcademico.Text) ||
                !Validador.ValidarLongitudCaracteresNombreYApellidos(this.TextBox_ApellidoPaternoAcademico.Text) ||
                !Validador.ValidarLongitudCaracteresNombreYApellidos(this.TextBox_NombreAcademico.Text))
            {
                MessageBox.Show(mensajes.messageBoxText_CamposNombreYApellidosExcedenLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            if (!Validador.ValidarLongitudCaracteresCorreoElectronicoPersonal(this.TextBox_CorreoPersonalAcademico.Text))
            {
                MessageBox.Show(mensajes.messageBoxText_CampoCorreoPersonalExcedeLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            if (!Validador.ValidarLongitudCaracteresCorreoElectronicoInstitucional(this.TextBox_CorreoInstitucionalAcademico.Text))
            {
                MessageBox.Show(mensajes.messageBoxText_CampoCorreoInstitucionalExcedeLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            return longitudValida;
        }

        private void ValidarAcademicoComoNuevo()
        {
            AcademicosDAO academicosDAO = new AcademicosDAO();
            if (!academicosDAO.BuscarAcademicoPorNombreYApellidos(academico))
            {
                RegistrarDatosAcademico();
                if (academicoRegistrado)
                {
                     MessageBox.Show(mensajes.messageBoxText_RegistroExitoso, mensajes.caption_Exito, MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    LimpiarInformacionIngresada();
                }
            }
            else
            {
                MessageBox.Show(mensajes.messageBoxText_AcademicoExistente, mensajes.messageBoxTitle_Conexion,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LimpiarInformacionIngresada()
        {
            this.TextBox_NombreAcademico.Text = ""; 
            this.TextBox_ApellidoPaternoAcademico.Text = ""; 
            this.TextBox_ApellidoMaternoAcademico.Text = ""; 
            this.TextBox_CorreoPersonalAcademico.Text = ""; 
            this.TextBox_CorreoInstitucionalAcademico.Text = "";
            ObservableCollection<Dominio.ProgramasEducativos> programasEducativosRestaurados =
                new ObservableCollection<Dominio.ProgramasEducativos>();
            foreach (ProgramasEducativos programaEducativo in programasEducativos) {
                programaEducativo.ProgramaEducativoSeleccionado = false;
                programasEducativosRestaurados.Add(programaEducativo);
            }
            CollectionViewSource ViewSource_ProgramasEducativos =
                       ((CollectionViewSource)(this.FindResource("ViewSource_ProgramasEducativos")));
            ViewSource_ProgramasEducativos.Source = programasEducativosRestaurados;
        }


        private void RegistrarDatosAcademico()
        {
            AcademicosDAO academicosDAO = new AcademicosDAO();
            AcademicosUsuariosDAO academicosUsuariosDAO = new AcademicosUsuariosDAO();
            if (academicosDAO.RegistrarAcademico(academico))
            {
                academico.IdAcademico = academicosDAO.ObtenerIdAcademicoPorNombreYApellidos(academico);
                RegistrarAcademicoUsuarioConRol();
            }
            else 
            {
                MessageBox.Show(mensajes.messageBoxText_Conexion, mensajes.messageBoxTitle_Conexion,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void RegistrarAcademicoUsuarioConRol()
        {
            bool academicoUsuarioRegistrado = false;
            string programasEducativosNoAsignados = "";
            int rolesAsignados = 0;
            if (academico.IdAcademico != -1)
            {
                foreach (ProgramasEducativos programasEducativos in DataGrid_ProgramasEducativos.ItemsSource)
                {
                    if (programasEducativos.ProgramaEducativoSeleccionado)
                    {
                        AcademicosUsuariosDAO academicoUsuarioDAO = new AcademicosUsuariosDAO();
                        AcademicosUsuarios academicoUsuario = new AcademicosUsuarios();
                        AcademicosRolesDAO academicosRolesDAO = new AcademicosRolesDAO();
                        academicoUsuario.NombreUsuario = academico.NombreCompleto + programasEducativos.IdProgramaEducativo;
                        academicoUsuario.Contrasena = 
                            EncriptadorContrasenas.Encriptar(academico.NombreCompleto + programasEducativos.NombreProgramaEducativo);
                        academicoUsuarioRegistrado = academicoUsuarioDAO.RegistrarAcademicoUsuario(academicoUsuario);
                        programasEducativosNoAsignados =
                            ValidarAcademicoUsuarioRegistrado(academicoUsuarioRegistrado, programasEducativos, programasEducativosNoAsignados);
                        academicoUsuario.IdUsuario = academicoUsuarioDAO.ObtenerIdAcademicoUsuarioPorNombreYContrasena(academicoUsuario);
                        AcademicosRoles academicosRoles = new AcademicosRoles();
                        academicosRoles.idAcademico = academico.IdAcademico;
                        academicosRoles.idRol = (int)EnumRolesDeUsuario.Rol_Sin_Rol;
                        academicosRoles.idProgramaEducativo = programasEducativos.IdProgramaEducativo;
                        academicosRoles.idUsuario = academicoUsuario.IdUsuario;
                        if (academicosRolesDAO.RegistrarAcademicoRol(academicosRoles))
                        {
                            rolesAsignados++;
                        }
                    }
                }
                ValidarProgramasEducativosAsignados(rolesAsignados, programasEducativosNoAsignados);
            }
            else 
            {
                MessageBox.Show(mensajes.messageBoxText_Conexion, mensajes.messageBoxTitle_Conexion,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ValidarAcademicoUsuarioRegistrado(bool academicoUsuarioRegistrado, ProgramasEducativos programasEducativos, 
                                                         string programasEducativosNoAsignados)
        {
            if (!academicoUsuarioRegistrado)
            {
                programasEducativosNoAsignados = programasEducativosNoAsignados + ", " + programasEducativos;
                numeroProgramasEducativosNoAsignados++;
            }
            return programasEducativosNoAsignados;
        }

        private void ValidarProgramasEducativosAsignados(int rolesAsignados, string programasEducativosNoAsignados)
        {
            if (numeroProgramasEducativosNoAsignados != 0)
            {
                MessageBox.Show(mensajes.messageBoxText_ProgramasEducativosNoAsignados + programasEducativosNoAsignados, 
                mensajes.messageBoxTitle_Conexion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ValidarRolesAsignados(rolesAsignados);
            }
        }

        private void ValidarRolesAsignados(int numeroDeRolesAsignados)
        {
            if (numeroDeRolesAsignados == 0)
            {
                MessageBox.Show(mensajes.messageBoxText_Conexion, mensajes.messageBoxTitle_Conexion,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                academicoRegistrado = true;
            }
        }

        private void CancelarRegistroAcademico(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultadoMessageBox =
                MessageBox.Show(mensajes.messageBoxText_CancelarOperacion, mensajes.messageBoxTitle_CancelarOperacion,
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultadoMessageBox == MessageBoxResult.Yes)
            {
                this.NavigationService.GoBack();    
            }
        }
    }
}
