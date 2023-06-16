using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorRegistrarProgramaEducativo.xaml
    /// </summary>
    public partial class ControladorRegistrarProgramaEducativo : Page
    {
        private ProgramasEducativosDAO programasEducativosDAO;
        private List<ProgramasEducativos> programasEducativos;
        private static readonly int PROGRAMA_EDUCATIVO_VALIDO = 1;
        private static readonly int PROGRAMA_EDUCATIVO_DUPLICADO = 2;
        private static readonly int PROGRAMA_EDUCATIVO_CARACTERES_INVALIDOS = 3;
        private static readonly int PROGRAMA_EDUCATIVO_VACIO = 4;
        private static readonly string INSTRUCCION_MODIFICAR = "Ingrese el nuevo nombre del Programa Educativo:";
        private static readonly string INSTRUCCION_AGREGAR = "Agregar nuevo programa educativo:";
        private ProgramasEducativos programaEducativoModificado;
        public ControladorRegistrarProgramaEducativo()
        {
            InitializeComponent();
            programasEducativosDAO = new ProgramasEducativosDAO();
            MostrarProgramasEducativos();
            Button_Guardar.IsEnabled = false;
        }

       private void MostrarProgramasEducativos()
        {
            DataGrid_ProgramasEducativos.Items.Clear();
            this.programasEducativos = programasEducativosDAO.ObtenerProgramasEducativos();
            foreach (var pe in programasEducativos)
            {
                DataGrid_ProgramasEducativos.Items.Add(pe);
            }
        }

        private void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            Label_Instruccion.Content = INSTRUCCION_MODIFICAR;
            programaEducativoModificado = programasEducativos[DataGrid_ProgramasEducativos.SelectedIndex];
            TextBox_ProgramaEducativo.Text = programaEducativoModificado.NombreProgramaEducativo;
        }

        private void Button_Guardar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarTextBox();
            TextBox_ProgramaEducativo.Text = TextBox_ProgramaEducativo.Text.Trim();
            TextBox_ProgramaEducativo.Text = TextBox_ProgramaEducativo.Text.ToUpper();
            String programaEducativo = TextBox_ProgramaEducativo.Text;
            int validez = ValidarCampos(programaEducativo);
            bool resultadoOperacion = false;
            string operacion = "";
            if (validez == PROGRAMA_EDUCATIVO_VALIDO)
            {
                if (Label_Instruccion.Content.ToString() == INSTRUCCION_AGREGAR)
                {
                    ProgramasEducativos pE = new ProgramasEducativos();
                    pE.NombreProgramaEducativo = programaEducativo;
                    resultadoOperacion = programasEducativosDAO.RegistrarProgramaEducativo(pE);
                    operacion = "registró";
                }
                else
                {
                    programaEducativoModificado.NombreProgramaEducativo = programaEducativo;
                    resultadoOperacion = programasEducativosDAO.ModificarProgramaEducativo(programaEducativoModificado);
                    operacion = "actualizó";
                }
                if (resultadoOperacion)
                {
                    MessageBox.Show("El programa educativo se " + operacion + " exitosamente", "Registro exitoso", MessageBoxButton.OK);
                    TextBox_ProgramaEducativo.Text = "";
                    MostrarProgramasEducativos();
                }
                else
                {
                    MessageBox.Show("No se pudo conectar con la base de datos. Por favor, inténtelo más tarde.", "No es posible registrar el programa educativo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (validez == PROGRAMA_EDUCATIVO_CARACTERES_INVALIDOS)
            {
                Label_CampoAcepta.Visibility = Visibility.Visible;
                TextBox_ProgramaEducativo.BorderBrush = Brushes.Red;
            }
            else if (validez == PROGRAMA_EDUCATIVO_VACIO)
            {
                MessageBox.Show("No se pueden dejar los campos vacíos");
            }
        }
        private int ValidarCampos(string programaEducativo)
        {
            int validez = 0;
            if (!String.IsNullOrWhiteSpace(programaEducativo)){
                Regex regex = new Regex(@"^(?=.*?[#?!@$%^&*-])");
                Match match = regex.Match(programaEducativo);
                if (!match.Success)
                {
                    if (programasEducativosDAO.BuscarProgramaEducativoExistente(programaEducativo))
                    {
                        MessageBox.Show("El programa educativo que ingresó ya se encuentra registrado en el sistema", "Error al registrar el programa educativo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        validez = PROGRAMA_EDUCATIVO_DUPLICADO;
                    }
                    else
                    {
                        validez = PROGRAMA_EDUCATIVO_VALIDO;
                    }
                }
                else
                {
                    validez = PROGRAMA_EDUCATIVO_CARACTERES_INVALIDOS;
                }
            }
            else
            {
                validez = PROGRAMA_EDUCATIVO_VACIO;
            }
            return validez;
        }

        private void Button_Regresar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void TextBox_ProgramaEducativo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBox_ProgramaEducativo.Text))
            {
                Button_Guardar.IsEnabled = true;
            }
        }

        private void LimpiarTextBox()
        {
            Label_CampoAcepta.Visibility = Visibility.Collapsed;
            TextBox_ProgramaEducativo.BorderBrush = Brushes.Gray;
        }

        private void DataGrid_ProgramasEducativos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox_ProgramaEducativo.Text = "";
            Button_Guardar.IsEnabled = false;
        }


        private void Botton_Cerrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Label_Instruccion.Content = INSTRUCCION_AGREGAR;
            TextBox_ProgramaEducativo.Text = "";
            Button_Guardar.IsEnabled = false;
            LimpiarTextBox();
        }
    }
}
