using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ValidadorEstudianteImportado
    {
        public HashSet<string> MatriculasEnBaseDeDatos = new HashSet<string>();
        public ObservableCollection<EstudianteImportado> estudiantesEnLista =
            new ObservableCollection<EstudianteImportado>();
        public void Validar(EstudianteImportado estudiante)
        { 
            ValidarMatricula(estudiante);
            ValidarNombreParcialmente(estudiante, "Nombre");
            ValidarNombreParcialmente(estudiante, "ApellidoPaterno");
            ValidarNombreParcialmente(estudiante, "ApellidoMaterno");
            ValidarCorreoPersonal(estudiante);
        }

        public void ValidarMatricula(EstudianteImportado estudiante)
        {
            estudiante.LimpiarErrores(nameof(estudiante.Matricula));
            ValidarMatriculaLlena(estudiante);
            ValidarMatriculaLongitud(estudiante);
            ValidarMatriculaNumerica(estudiante);
            BuscarMatriculasEnCSV(estudiante);
            BuscarMatriculasConBaseDeDatos(estudiante);
        }

        private void ValidarMatriculaLlena(EstudianteImportado estudiante)
        {
            if (estudiante.Matricula == null || String.IsNullOrWhiteSpace(estudiante.Matricula))
            {
                estudiante.AgregarError(nameof(estudiante.Matricula), "● Todos los campos son obligatorios");
                return;
            }
        }

        private void ValidarMatriculaLongitud(EstudianteImportado estudiante)
        {
            if (estudiante.Matricula != null && estudiante.Matricula.Count() != 8)
            {
                estudiante.AgregarError(nameof(estudiante.Matricula), $"● La matrícula debe contener 8 números.");
            }
        }

        private void ValidarMatriculaNumerica(EstudianteImportado estudiante)
        {
            long matriculaNumerica = 0;
            if (!long.TryParse(estudiante.Matricula, out matriculaNumerica))
            {
                estudiante.AgregarError(nameof(estudiante.Matricula), $"● Sólo puede contener números. " +
                $"\n   El sufijo 'S' se agregará automáticamente al " +
                $"\n   registrar el estudiante en la base de datos.");
            }
        }

        private void BuscarMatriculasEnCSV(EstudianteImportado estudiante)
        {
            bool laMatriculaSeRepite = this.estudiantesEnLista.Any(companero =>
                companero.Matricula != null
                && companero.Matricula.Equals(estudiante.Matricula)
                && (companero.GetHashCode() != estudiante.GetHashCode())
            );

            if(laMatriculaSeRepite)
            {
                estudiante.AgregarError(nameof(estudiante.Matricula),
                    $"● Esta matrícula se repite con otra matrícula en la lista.");
            }
        }

        private void BuscarMatriculasConBaseDeDatos(EstudianteImportado estudiante)
        {
            if (MatriculasEnBaseDeDatos.Contains(estudiante.MatriculaCompleta))
            {
                estudiante.AgregarError(nameof(estudiante.Matricula),
                $"● Esta matrícula ya existe en la base de datos.");
            }
        }

        public void ValidarNombreParcialmente(EstudianteImportado estudiante, String nombrePropiedad)
        {
            switch (nombrePropiedad)
            {
                case "Nombre":
                    ValidarNombre(estudiante, nombrePropiedad, estudiante.Nombre);
                    break;
                case "ApellidoPaterno":
                    ValidarNombre(estudiante, nombrePropiedad, estudiante.ApellidoPaterno);
                    break;
                case "ApellidoMaterno":
                    ValidarNombre(estudiante, nombrePropiedad, estudiante.ApellidoMaterno);
                    break;
            }
        }

        public void ValidarNombre(EstudianteImportado estudiante, String nombrePropiedad, String valor)
        {
            estudiante.LimpiarErrores(nombrePropiedad);

            if (valor == null || String.IsNullOrWhiteSpace(valor))
            {
                estudiante.AgregarError(nombrePropiedad, "● Todos los campos son obligatorios");
                return;
            }

            if (!Validador.ValidarCadenaAlfabetica(valor))
            {
                estudiante.AgregarError(nombrePropiedad, $"● Sólo puede contener letras.");
            }
        }

        public void ValidarCorreoPersonal(EstudianteImportado estudiante)
        {
            estudiante.LimpiarErrores(nameof(estudiante.CorreoPersonal));

            if (CadenaVacia(estudiante.CorreoPersonal))
            {
                estudiante.AgregarError(nameof(estudiante.CorreoPersonal), "● Todos los campos son obligatorios");
                return;
            }

            if (!Validador.ValidarCorreoElectronico(estudiante.CorreoPersonal))
            {
                estudiante.AgregarError(nameof(estudiante.CorreoPersonal), $"● El correo electrónico es inválido.");
            }
        }

        private bool CadenaVacia(string cadena)
        {
            return cadena == null || String.IsNullOrWhiteSpace(cadena);
        }
    }
}
