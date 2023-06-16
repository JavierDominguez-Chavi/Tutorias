using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EstudianteImportado : INotifyDataErrorInfo
    {
        private string matricula;
        public String Matricula
        {
            set 
            { 
                matricula = value;
                Validador.ValidarMatricula(this);
            }
            get { return matricula; }
        }
        public String MatriculaCompleta => $"S{this.Matricula}";

        private string nombre;
        public String Nombre
        {
            set
            {
                nombre = value;
                Validador.ValidarNombreParcialmente(this, "Nombre");
            }
            get { return nombre; }
        }

        private string apellidoPaterno;
        public String ApellidoPaterno
        {
            set
            {
                apellidoPaterno = value;
                Validador.ValidarNombreParcialmente(this, "ApellidoPaterno");
            }
            get { return apellidoPaterno; }
        }

        private string apellidoMaterno;
        public String ApellidoMaterno
        {
            set
            {
                apellidoMaterno = value;
                Validador.ValidarNombreParcialmente(this, "ApellidoMaterno");
            }
            get { return apellidoMaterno; }
        }

        public int idProgramaEducativo { set; get; }

        private string correoPersonal;
        public String CorreoPersonal
        {
            set
            {
                correoPersonal = value;
                Validador.ValidarCorreoPersonal(this);
            }
            get { return correoPersonal; }
        }
        public ObservableCollection<EstudianteImportado> companeros = 
            new ObservableCollection<EstudianteImportado>();

        public ValidadorEstudianteImportado Validador = new ValidadorEstudianteImportado();

        private readonly Dictionary<string, List<string>> errores = 
            new Dictionary<string, List<string>>();

        public bool HasErrors => errores.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errores = new List<string>();
            if (propertyName == null) return errores;
            if (this.errores.TryGetValue(propertyName, out List<string> erroresEncontrados))
            {
                errores = erroresEncontrados;
            }
            return errores;
        }

        public void AgregarError(string nombrePropiedad, string mensajeError)
        {
            if (!errores.ContainsKey(nombrePropiedad))
            {
                errores.Add(nombrePropiedad, new List<string>());
            }
            errores[nombrePropiedad].Add(mensajeError);
            ErroresCambiaron(nombrePropiedad);
        }

        private void ErroresCambiaron(string nombrePropiedad)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nombrePropiedad));
        }

        public void LimpiarErrores(string nombrePropiedad)
        {
            if (errores.Remove(nombrePropiedad))
            {
                ErroresCambiaron(nombrePropiedad);
            }
        }

        public AccesoADatos.Estudiantes AAccesoADatos()
        { 
            return new AccesoADatos.Estudiantes
            {
                Matricula = $"S{this.Matricula}",
                Nombre = this.Nombre,
                ApellidoPaterno = this.ApellidoPaterno,
                ApellidoMaterno = this.ApellidoMaterno,
                CorreoPersonal = this.CorreoPersonal,
                CorreoInstitucional = $"ZS{this.Matricula}@ESTUDIANTES.UV.MX",
                IdProgramaEducativo = this.idProgramaEducativo,
                EnRiesgo = false,
                SemestreQueCursa = 1,
                IdAcademico = null,
            };
        }
    }
}
