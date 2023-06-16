using AccesoADatos;
using CsvHelper;
using CsvHelper.Configuration;
using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Presentacion
{
    internal class ImportarEstudiantesVistaModelo : INotifyPropertyChanged
    {
        public ICollectionView Estudiantes { get; set; }
        private ObservableCollection<EstudianteImportado> estudiantesCollection { get; set; }
        private readonly ValidadorEstudianteImportado validador = new ValidadorEstudianteImportado();
        private readonly EstudiantesDAO estudiantesDAO = new EstudiantesDAO();
        private readonly Predicate<object> predicadoErrores =
            objeto =>
            {
                EstudianteImportado estudiante = objeto as EstudianteImportado;
                return estudiante.HasErrors == true;
            };

        private readonly int idProgramaEducativo = SingletonUsuario.Instance.IdProgramaEducativo;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool terminaCarga = true;
        public bool TerminaCarga {
            set
            {
                this.terminaCarga = value;
                this.VisibilidadPanelCarga = value ? Visibility.Collapsed : Visibility.Visible;
                NotifyPropertyChanged();
            }
            get
            {
                return this.terminaCarga;
            }
        }

        private Visibility visibilidadPanelCarga;
        public Visibility VisibilidadPanelCarga
        {
            set 
            {
                this.visibilidadPanelCarga = value;
                NotifyPropertyChanged();
            }
            get
            {
                return this.visibilidadPanelCarga;
            }
        }

        public ImportarEstudiantesVistaModelo()
        {

            this.estudiantesCollection = new ObservableCollection<EstudianteImportado>();
            this.Estudiantes = CollectionViewSource.GetDefaultView(estudiantesCollection);
            this.validador = new ValidadorEstudianteImportado()
            {
                MatriculasEnBaseDeDatos =
                    this.estudiantesDAO.ObtenerMatriculasActivasPorProgramaEducativo()
            };
            this.validador.estudiantesEnLista = this.estudiantesCollection;
            this.VisibilidadPanelCarga = Visibility.Collapsed;

        }

        internal void AgregarEstudiante()
        {
            EstudianteImportado estudiante = new EstudianteImportado()
            {
                companeros = this.estudiantesCollection,
            };
            estudiante.Validador = this.validador;
            this.validador.Validar(estudiante);
            this.estudiantesCollection.Add(estudiante);
        }

        internal void EliminarEstudiante(EstudianteImportado estudiante)
        {
            this.estudiantesCollection.Remove(estudiante);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task ImportarEstudiantesPorCSVAsync(string ruta)
        {

            this.TerminaCarga = false;
            await Task.Factory.StartNew(() =>
            {
                var encoding = Encoding.GetEncoding("windows-1254");
                using (TextReader textReader = new StreamReader(ruta, encoding))
                    try
                    {
                        using (CsvReader csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture))
                        {
                            csvReader.Context.RegisterClassMap<EstudiantesMap>();
                            var estudiantes = csvReader.GetRecords<EstudianteImportado>();
                            foreach (EstudianteImportado estudiante in estudiantes)
                            {
                                estudiante.companeros = estudiantesCollection;
                                estudiante.Validador = this.validador;
                                this.validador.Validar(estudiante);
                                App.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    estudiantesCollection.Add(estudiante);
                                });
                            }
                        }
                    }
                    catch (HeaderValidationException)
                    {
                        MessageBox.Show("El archivo seleccionado no tiene el formato adecuado.");
                    }
            });
            this.TerminaCarga = true;
            this.MostrarErrores();
        }
        internal void ExportarEstudiantesEnCSV(string ruta)
        {
            EstudianteImportado comodin = new EstudianteImportado();
            this.estudiantesCollection.Add(comodin);
            using (var writer = new StreamWriter(ruta))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<EstudiantesMap>();
                csv.WriteRecords(this.Estudiantes);
            }
            this.estudiantesCollection.Remove(comodin);
        }

        public sealed class EstudiantesMap : ClassMap<EstudianteImportado>
        {

            public EstudiantesMap()
            {
                AutoMap(CultureInfo.InvariantCulture);
                Map(e => e.Validador).Ignore();
                Map(e => e.companeros).Ignore();   
                Map(e => e.HasErrors).Ignore();
                Map(e => e.idProgramaEducativo).Ignore();
                Map(e => e.MatriculaCompleta).Ignore();
            }
        }

        internal async Task RegistrarEstudiantes()
        {
            MostrarErrores();
            Dictionary<string, EstudianteImportado> estudiantesImportados = 
                new Dictionary<string, EstudianteImportado>();
            foreach (EstudianteImportado estudianteImportado in Estudiantes)
            {
                estudianteImportado.idProgramaEducativo = this.idProgramaEducativo;
                estudiantesImportados.Add(estudianteImportado.Matricula, estudianteImportado);
            }

            this.TerminaCarga = false;
            await Task.Factory.StartNew(() =>
            {

                if (this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantesImportados).Result)
                {
                    ActualizarMatriculasEnBaseDeDatos(estudiantesImportados);
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.estudiantesCollection.Clear();
                    });
                    MensajesHelper.OperacionExitosa();
                }
                else
                {
                    MensajesHelper.FaltaDeConexion();
                }
            });
            this.TerminaCarga = true;
      
        }

        public void ActualizarMatriculasEnBaseDeDatos(Dictionary<string, EstudianteImportado> estudiantesRegistrados)
        {

            HashSet<string> matriculasEsperadas = new HashSet<string>();
            foreach (KeyValuePair<string, EstudianteImportado> registro in estudiantesRegistrados)
            { 
                matriculasEsperadas.Add(registro.Value.MatriculaCompleta);
            }
            int numMatriculasObtenidas = matriculasEsperadas.Count();
            for (int i = 0; i<numMatriculasObtenidas; i++)
            {
                this.validador.MatriculasEnBaseDeDatos.Add(matriculasEsperadas.ElementAt(i));
            }
        }

        public void MostrarErrores()
        {          
        
            if (this.estudiantesCollection.Count == 0)
            {
                throw new ArgumentException("Debe agregar al menos un estudiante antes de registrar.");
            }
            if (estudiantesCollection.Any(e => e.HasErrors))
            {
                throw new ArgumentException("Hay estudiantes con información inválida. " +
                            "Por favor, verifíque la información e intente de nuevo.");
            }
        }

        internal void FiltrarPropiedades(string consulta)
        {
            Predicate<object> predicado =
            objeto =>
            {
                EstudianteImportado estudiante = objeto as EstudianteImportado;
                return 
                    estudiante.MatriculaCompleta.Contains(consulta) 
                    || estudiante.Nombre.Contains(consulta) 
                    || estudiante.ApellidoPaterno.Contains(consulta) 
                    || estudiante.ApellidoMaterno.Contains(consulta) 
                    || estudiante.CorreoPersonal.Contains(consulta);
            };
            this.Estudiantes.Filter = predicado;
        }

        public void FiltrarErrores(bool filtrar)
        {
            if (filtrar)
            {
                this.Estudiantes.Filter += predicadoErrores;
            }
            else
            {
                this.Estudiantes.Filter -= predicadoErrores;
            }
        }
    }

}
