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
    public partial class ConcentradoAsistenciasGeneral : UserControl
    {
        private ControlDeAsistenciasVistaModelo contexto;
        public ConcentradoAsistenciasGeneral(ControlDeAsistenciasVistaModelo contexto)
        {
            InitializeComponent();
            this.DataContext = contexto;
            this.contexto = contexto;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.contexto.MostrarTotalesDeAcademicoSeleccionado();
        }

    }
}
