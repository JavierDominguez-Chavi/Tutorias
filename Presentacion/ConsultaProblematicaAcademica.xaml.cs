using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
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
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    public partial class ConsultaProblematicaAcademica : Page
    {
        public Dominio.Problematicas ProblematicaConsulta { get; set; }

        public AccesoADatos.Soluciones SolucionConsultada { get; set; }

        public ConsultaProblematicaAcademica(Dominio.Problematicas problematica)
        {
            InitializeComponent();
            this.ProblematicaConsulta = problematica;
            ValidarPermisosDeUsuario();
        }

        private void ValidarPermisosDeUsuario()
        {
            if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == (int)EnumRolesDeUsuario.Rol_Tutor_Academico)
            {
                this.Label_DescripcionSolucion.Visibility = Visibility.Collapsed;
                this.TextBlock_DescripcionSolucion.Visibility = Visibility.Collapsed;
                this.Label_FechaDeRegistro.Visibility = Visibility.Collapsed;
                this.TextBox_FechaDeRegistro.Visibility = Visibility.Collapsed;
                this.Label_PersonaQueRegistro.Visibility = Visibility.Collapsed;
                this.TextBox_PersonaQueRegistro.Visibility = Visibility.Collapsed;
                Grid_Problematicas.RowDefinitions.Remove(GridRow_Fila3);
                Grid_Problematicas.RowDefinitions.Remove(GridRow_Fila4);
            }
            MostrarCamposProblematica();
            //*******************************ESTA PARTE ES DEL CASO DE USO CONSULTAR SOLUCION******************************************
            if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == (int)EnumRolesDeUsuario.Rol_Coordinador ||
                SingletonUsuario.Instance.AcademicosRoles.First().idRol == (int)EnumRolesDeUsuario.Rol_Jefe_De_Carrrera)
            {
                getSolucion(ProblematicaConsulta.IdProblematica);
            }
        }

        private void MostrarCamposProblematica()
        {
            ValidarFuncionModificar();
            if (!String.IsNullOrWhiteSpace(ProblematicaConsulta.ExperienciasEducativas_Academicos.Academicos.Nombre)
                && !String.IsNullOrWhiteSpace(ProblematicaConsulta.Estudiantes.Nombre))
            {
                this.TextBlock_TituloProblematica.Text = ProblematicaConsulta.Titulo;
                this.TextBox_ExperienciaEducativa.Text = ProblematicaConsulta.ExperienciasEducativas_Academicos.ExperienciasEducativas.Nombre;
                this.TextBox_Profesor.Text = ProblematicaConsulta.ExperienciasEducativas_Academicos.Academicos.NombreCompleto;
                this.TextBox_Nrc.Text = ProblematicaConsulta.ExperienciasEducativas_Academicos.NRC.ToString();
                this.TextBox_EstudiantQueReporta.Text = ProblematicaConsulta.Estudiantes.NombreCompleto;
        
                this.TextBlock_DescripcionProblematica.Text = ProblematicaConsulta.Descripcion;
            }
            else
            {
                MessageBox.Show(mensajes.messageBoxText_Conexion, mensajes.messageBoxTitle_Conexion,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ValidarFuncionModificar()
        {
            if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == (int)EnumRolesDeUsuario.Rol_Tutor_Academico)
            {
                if (DateTime.Compare(DateTime.Now, ProblematicaConsulta.TutoriasAcademicas.FechasTutorias.FechaCierreDeReporte) > 0 ||
                    DateTime.Compare(DateTime.Now, ProblematicaConsulta.TutoriasAcademicas.FechasTutorias.FechaTutoria) < 0)
                {
                   
                }
            }
        }

        private void CancelarConsulta(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void ModificarProblematicaOSolucion(object sender, RoutedEventArgs e)
        {
            if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == (int)EnumRolesDeUsuario.Rol_Tutor_Academico)
            {
                ProblematicaAcademica problematicaAcademica = new ProblematicaAcademica(ProblematicaConsulta);
				this.NavigationService.Navigate(problematicaAcademica);
            }
            else{
                String ExperienciaEducativa = Label_ExperienciaEducativa.Content.ToString();
                AcademicosDAO academicosDAO = new AcademicosDAO();
                ControladorModificarSolucionAProblematica modificarSolucionAProblematica = new ControladorModificarSolucionAProblematica(SolucionConsultada, ProblematicaConsulta, ExperienciaEducativa
                    , academicosDAO.ObtenerAcademicoPorIdAcademico(SolucionConsultada.IdAcademico));
            }



        }

        public void getSolucion(int IdProblematica)
        {

                SolucionAProblematicaAcademicaDAO solucionesAProblematicaAcademicaDAO = new SolucionAProblematicaAcademicaDAO();

                SolucionConsultada = solucionesAProblematicaAcademicaDAO.ObtenerSolucion(IdProblematica);

            if (SolucionConsultada == null)
            {
                Button_Solucionar.Visibility = Visibility.Visible;
                this.Label_DescripcionSolucion.Visibility = Visibility.Collapsed;
                this.TextBlock_DescripcionSolucion.Visibility = Visibility.Collapsed;
                this.Label_FechaDeRegistro.Visibility = Visibility.Collapsed;
                this.TextBox_FechaDeRegistro.Visibility = Visibility.Collapsed;
                this.Label_PersonaQueRegistro.Visibility = Visibility.Collapsed;
                this.TextBox_PersonaQueRegistro.Visibility = Visibility.Collapsed;
            }
            else {
               TextBlock_DescripcionSolucion.Text =  SolucionConsultada.Descripcion;
                TextBox_FechaDeRegistro.Text = SolucionConsultada.Fecha.ToShortDateString();
                AcademicosDAO academicosDAO = new AcademicosDAO();
                TextBox_PersonaQueRegistro.Text = academicosDAO.ObtenerAcademicoPorIdAcademico(SolucionConsultada.IdAcademico).Nombre +" "+ academicosDAO.ObtenerAcademicoPorIdAcademico(SolucionConsultada.IdAcademico).ApellidoPaterno +" "+ academicosDAO.ObtenerAcademicoPorIdAcademico(SolucionConsultada.IdAcademico).ApellidoMaterno ;
            }

        }

        private void Button_Solucionar_Click(object sender, RoutedEventArgs e)
        {
            //TextBlock_DescripcionSolucion.Visibility = Visibility.Hidden;
            TextBox_DescripcionSolucion.Visibility = Visibility.Visible;
            Button_GuardarSolucion.Visibility = Visibility.Visible;
            Button_Solucionar.IsEnabled = false;
            Button_Modificar.IsEnabled = false;
        }

        private void Button_GuardarSolucion_Click(object sender, RoutedEventArgs e)
        {
            string solucionEscrita = TextBox_DescripcionSolucion.Text;
            if (!String.IsNullOrWhiteSpace(solucionEscrita))
            {
                Dominio.Soluciones solucion = new Dominio.Soluciones();
                solucion.Fecha = DateTime.Parse(DateTime.Now.ToShortDateString());
                solucion.Descripcion = solucionEscrita;
                //se toma desde el singleton
                int ID_ACADEMICO_PRUEBA = 1;
                solucion.IdAcademico = ID_ACADEMICO_PRUEBA;
                //se toma desde la problemática que se está viendo
                solucion.IdProblematica = this.ProblematicaConsulta.IdProblematica;
                    SolucionAProblematicaAcademicaDAO solucionesDAO = new SolucionAProblematicaAcademicaDAO();
                    Boolean registroExitoso = solucionesDAO.RegistrarSolucion(solucion);
                    if (registroExitoso == true)
                    {
                        MessageBox.Show("La información se ha guardado con éxito");
                        TextBox_DescripcionSolucion.IsEnabled = false;
                        TextBox_DescripcionSolucion.Visibility = Visibility.Collapsed;
                        TextBlock_DescripcionProblematica.Visibility = Visibility.Visible;
                        getSolucion(this.ProblematicaConsulta.IdProblematica);
                        Button_GuardarSolucion.Visibility = Visibility.Collapsed;
                        Button_Modificar.IsEnabled = true;
                    Button_Solucionar.Visibility = Visibility.Collapsed;

                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al registrar");
                    }
            }
            else
            {
                MessageBox.Show("Los datos no pueden estar en blanco, revise la información", "Error");
            }
        }

    }
    }