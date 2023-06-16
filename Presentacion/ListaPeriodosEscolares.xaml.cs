using AccesoADatos;
using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;
using PeriodosEscolares = Dominio.PeriodosEscolares;

namespace Presentacion
{
    /// <summary>
    /// Interaction logic for PeriodosEscolares.xaml
    /// </summary>
    public partial class ListaPeriodosEscolares : Page
    {

        private PeriodosEscolaresVistaModelo contexto;
        private bool userEsCoordinador = false;
        private bool userEsAdmin = false;
        private PeriodosEscolaresDAO periodosEscolaresDAO;
        private Dominio.PeriodosEscolares periodoEscolarSeleccionado;
        public ListaPeriodosEscolares() 
        {
            InitializeComponent();
            this.contexto = this.DataContext as PeriodosEscolaresVistaModelo;
            if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == (int)EnumRolesDeUsuario.Rol_Coordinador)
            {
                userEsCoordinador = true;
            }
            if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == (int)EnumRolesDeUsuario.Rol_Aministrador)
            {
                userEsAdmin = true;
            }
            MostrarFuncionesPorRol();
            periodosEscolaresDAO = new PeriodosEscolaresDAO();
        }

        private void MostrarFuncionesPorRol()
        {
            if (userEsAdmin)
            {
                this.Column_Editar.Visibility = Visibility.Visible;
                this.Button_RegistrarPeriodoEscolar.Visibility = Visibility.Visible;
            }

            if (this.userEsCoordinador)
            {
                this.Column_TieneFechas.Visibility = Visibility.Visible;
                this.Column_Gridsplitter.Width = new GridLength(5, GridUnitType.Pixel);
                this.Column_FechasTutorias.Width = new GridLength(2, GridUnitType.Star);
            }
        }

        private void Button_ConsultarFechasDeTutoria_Click(object sender, RoutedEventArgs e)
        {
            this.DataGrid_PeriodosEscolares.SelectedIndex= 0;
            ObtenerFechasTutoriasDePeriodoEscolar();
        }

        private void ObtenerFechasTutoriasDePeriodoEscolar()
        {
            if (contexto != null && this.userEsCoordinador)
            {
                Dominio.PeriodosEscolares periodoSeleccionado = ObtenerPeriodoEscolarSeleccionado();
                this.contexto.ObtenerFechasTutoriasDePeriodoEscolar(periodoSeleccionado);
                this.DataGrid_FechasTutorias.ItemsSource = periodoSeleccionado.FechasTutorias;
            }
        }
        private void DataGrid_PeriodosEscolares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObtenerFechasTutoriasDePeriodoEscolar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button fuente = sender as Button;
            switch (fuente.Content)
            {
                case "Guardar cambios":
                    GuardarCambios();
                    break;
            }
        }

        private void GuardarCambios()
        {
            try
            {
                Terminar(this.contexto.AsignarFechasDeTutoria(ObtenerPeriodoEscolarSeleccionado()));
            }
            catch (ArgumentException aException)
            {
                MostrarMensajeDatosIncorrectos(aException.Message);
            }
        }

        private void Terminar(bool operacionExitosa)
        {
            if (operacionExitosa)
            {
                MessageBox.Show(Properties.Mensajes.messageBoxText_Exito,
                                Properties.Mensajes.caption_Exito,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            else
            {
                MostrarMensajeConexion();
            }
        }

        private void MostrarMensajeConexion()
        {
            MessageBox.Show(Properties.Mensajes.messageBoxText_Conexion,
                            Properties.Mensajes.caption_Error,
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
        }

        private void MostrarMensajeDatosIncorrectos(string mensaje)
        {
            MessageBox.Show(mensaje,
                            Properties.Mensajes.caption_Aviso,
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
        }

        private Dominio.PeriodosEscolares ObtenerPeriodoEscolarSeleccionado()
        { 
            return this.DataGrid_PeriodosEscolares.SelectedItem as Dominio.PeriodosEscolares;
        }

        private void Button_RegistrarPeriodoEscolar_Click(object sender, RoutedEventArgs e)
        {
            Label_Contenido.Content = "Agregar Periodo Escolar";
            this.Row_RegistrarPeriodoEscolar.Height = GridLength.Auto;
            this.WrapPanel_RegistrarPeriodoEscolar.Visibility = Visibility.Visible;
            this.Button_RegistrarPeriodoEscolar.IsEnabled = false;
            this.DataPicker_FechaInicio.SelectedDate = DateTime.Today;
            this.Button_Eliminar.Visibility = Visibility.Collapsed;
        }

        private void Button_GuardarPeriodo_Click(object sender, RoutedEventArgs e)
        {
            if (DataPicker_FechaInicio.SelectedDate >= DataPicker_FechaFin.SelectedDate)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor o igual a la fecha de fin", "Fechas inválidas", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                int mesInicio = DataPicker_FechaInicio.SelectedDate.Value.Month;
                int mesFin = DataPicker_FechaFin.SelectedDate.Value.Month;
                Dominio.PeriodosEscolares periodosEscolares = new Dominio.PeriodosEscolares();
                periodosEscolares.Nombre = periodosEscolares.Meses[mesInicio - 1] + " " + DataPicker_FechaInicio.SelectedDate.Value.Year +
                    " - " + periodosEscolares.Meses[mesFin - 1] + " " + DataPicker_FechaFin.SelectedDate.Value.Year;
                periodosEscolares.FechaInicio = DataPicker_FechaInicio.SelectedDate.Value;
                periodosEscolares.FechaFin = DataPicker_FechaFin.SelectedDate.Value;

                if (Label_Contenido.Content.ToString().Contains("Agregar"))
                {
                    RegistrarPeriodoEscolar(periodosEscolares);
                }
                else if (Label_Contenido.Content.ToString().Contains("Modificar"))
                {
                    ModificarPeriodoEscolar(periodosEscolares);
                }
            }
            this.Button_RegistrarPeriodoEscolar.IsEnabled = true;
        }

        private bool RegistrarPeriodoEscolar(Dominio.PeriodosEscolares periodoEscolarARegistrar)
        {
            bool registroExitoso = false;
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea registrar el periodo escolar " +
                    periodoEscolarARegistrar.Nombre + "?", "Registrar periodo escolar", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                    if (ValidarPeriodoEscolar(periodoEscolarARegistrar))
                    {
                        registroExitoso = periodosEscolaresDAO.RegistrarPeriodoEscolar(periodoEscolarARegistrar);
                        if (registroExitoso)
                        {
                            MessageBox.Show("El periodo escolar " + periodoEscolarARegistrar.Nombre + " se registró exitosamente",
                                "Registro Exitoso");
                            DataGrid_PeriodosEscolares.ItemsSource = this.contexto.ActualizarLista();
                            WrapPanel_RegistrarPeriodoEscolar.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error al registrar el periodo escolar. Intente de nuevo más tarde", "Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El periodo escolar que está trantando de ingresar no es válido, ya que coincide con las fechas de un periodo escolar ya registrado.", "Favor de verificar la información", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
            }
            return registroExitoso;
        }

        private bool ModificarPeriodoEscolar(Dominio.PeriodosEscolares periodoEscolarAModificar)
        {
            bool actualizacionExitosa = false;
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea actualizar el periodo escolar " +
                    periodoEscolarAModificar.Nombre + "?", "Actualizar periodo escolar", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                if (periodoEscolarSeleccionado != null)
                {
                    if (ValidarPeriodoEscolar(periodoEscolarAModificar, ObtenerPeriodoEscolarSeleccionado().IdPeriodoEscolar))
                    {
                        periodoEscolarAModificar.IdPeriodoEscolar = periodoEscolarSeleccionado.IdPeriodoEscolar;
                        actualizacionExitosa = periodosEscolaresDAO.ModificarPeriodoEscolar(periodoEscolarAModificar);
                        if (actualizacionExitosa)
                        {
                            MessageBox.Show("El periodo escolar " + periodoEscolarAModificar.Nombre + " se actualizó exitosamente",
                                "Actualización Exitoso");
                            WrapPanel_RegistrarPeriodoEscolar.Visibility = Visibility.Collapsed;
                            DataGrid_PeriodosEscolares.ItemsSource = this.contexto.ActualizarLista();
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error al actualizar el periodo escolar. Intente de nuevo más tarde", "Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El periodo escolar que está modificando no es válido, ya que coincide con las fechas de un periodo escolar ya registrado.", 
                            "Favor de verificar la información", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar el periodo escolar que desea modificar", "Faltan datos", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
                
            return actualizacionExitosa;
        }

        private void Button_CancelarPeriodo_Click(object sender, RoutedEventArgs e)
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            this.Row_RegistrarPeriodoEscolar.Height = (GridLength)gridLengthConverter.ConvertFromString(((double)0).ToString());
            this.WrapPanel_RegistrarPeriodoEscolar.Visibility = Visibility.Collapsed;
            this.Button_RegistrarPeriodoEscolar.IsEnabled = true;
        }


        

        private void DataPicker_FechaFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataPicker_FechaFin.SelectedDate != DataPicker_FechaInicio.SelectedDate)
            {
                this.Button_GuardarPeriodo.IsEnabled = true;
            }
        }

        private void DataPicker_FechaInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DataPicker_FechaFin.SelectedDate = this.DataPicker_FechaInicio.SelectedDate;
        }

        private void Button_EditarPeriodoEscolar_Click(object sender, RoutedEventArgs e)
        {
            Label_Contenido.Content = "Modificar Periodo Escolar";
            this.Row_RegistrarPeriodoEscolar.Height = GridLength.Auto;
            this.WrapPanel_RegistrarPeriodoEscolar.Visibility = Visibility.Visible;
            periodoEscolarSeleccionado = ObtenerPeriodoEscolarSeleccionado();
            this.DataPicker_FechaInicio.SelectedDate = periodoEscolarSeleccionado.FechaInicio;
            this.DataPicker_FechaFin.SelectedDate = periodoEscolarSeleccionado.FechaFin;
            this.Button_GuardarPeriodo.IsEnabled = true;
            this.Button_RegistrarPeriodoEscolar.IsEnabled = true;
            this.Button_Eliminar.Visibility = Visibility.Visible;

        }

        private void Button_Eliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea eliminar el periodo escolar?", 
                "Eliminar periodo escolar", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                if (periodosEscolaresDAO.EliminarPeriodoEscolar(ObtenerPeriodoEscolarSeleccionado()))
                {
                    MessageBox.Show("El periodo escolar se eliminó exitosamente",
                    "Eliminaciión Exitosa");
                    DataGrid_PeriodosEscolares.ItemsSource = this.contexto.ActualizarLista();
                    WrapPanel_RegistrarPeriodoEscolar.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al eliminar el periodo escolar. Intente de nuevo más tarde", "Error");
                }
            }
        }

        private bool ValidarPeriodoEscolar(Dominio.PeriodosEscolares periodosEscolares, int idProgramaEducativoAModificar = -1)
        {
            bool esValido = true;
            ObservableCollection<Dominio.PeriodosEscolares> periodosEscolaresRecuperados = periodosEscolaresDAO.ObtenerPeriodosEscolares();
            ValidadorPeriodosEscolares validadorPeriodo = new ValidadorPeriodosEscolares(); 
            for (int i = 0; i < periodosEscolaresRecuperados.Count && esValido == true; i++)
            {
                if (periodosEscolaresRecuperados.ElementAt(i).IdPeriodoEscolar != idProgramaEducativoAModificar)
                {
                    esValido = validadorPeriodo.ValidarPeriodoEscolarAIngresarConPeriodoEscolarExistente(periodosEscolares, periodosEscolaresRecuperados.ElementAt(i));
                }
            }
            return esValido;
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void TextBox_FiltrarPeriodoEscolar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = TextBox_FiltrarPeriodoEscolar.Text.ToLower();
            ICollectionView vista = CollectionViewSource.GetDefaultView(DataGrid_PeriodosEscolares.ItemsSource);
            if (vista != null)
            {
                vista.Filter = periodoFiltrado =>
                {
                    PeriodosEscolares periodo = periodoFiltrado as PeriodosEscolares;
                    if (periodo != null)
                    {
                        return periodo.Nombre.ToLower().Contains(filtro);
                    }
                    return false;
                };
            }
        }
    }
}
