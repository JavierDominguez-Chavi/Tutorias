using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ValidadorFechasTutorias
    {
        public ValidadorFechasTutorias(ObservableCollection<FechasTutorias> fechas) 
        {
            ValidarFechasEntrantes(fechas);

            ValidarOrdenDeSesiones(fechas);
            ValidarFechasDentroDePeriodo(fechas);
        }


        private void ValidarFechasDentroDePeriodo(ObservableCollection<FechasTutorias> fechas)
        {
            foreach (FechasTutorias fecha in fechas)
            {
                if ( ! (fecha.PeriodosEscolares.FechaFin >= fecha.FechaCierreDeReporte) )
                {
                    throw new ArgumentException(message: "Problema en sesión "+fecha.NumeroSesion+": " + "La fecha de entrega del reporte debe estar dentro del periodo escolar.");
                }
                if ( ! (fecha.PeriodosEscolares.FechaInicio <= fecha.FechaTutoria))
                {
                    throw new ArgumentException(message: "Problema en sesión " + fecha.NumeroSesion + ": " + "La fecha de tutoría debe estar dentro del periodo escolar.");
                }
            }
        }


        private void ValidarOrdenDeSesiones(ObservableCollection<FechasTutorias> fechas)
        {
            int numeroFechas = fechas.Count;

            for (int i = 0; i < numeroFechas; i++)
            {
                if (i<numeroFechas-1 && ! (fechas[i].FechaCierreDeReporte < fechas[i + 1].FechaTutoria) )
                {
                    throw new ArgumentException(message: "Problema en sesiones " + fechas[i].NumeroSesion + " y "+ fechas[i+1].NumeroSesion + ": " + "Las fechas deben estar en orden ascendente.");
                }
                if ( ! (fechas[i].FechaTutoria < fechas[i].FechaCierreDeReporte))
                {
                    throw new ArgumentException(message: "Problema en sesión " + fechas[i].NumeroSesion + ": " + "La fecha de tutoría debe suceder antes que la fecha de entrega de su reporte.");
                }
            }
        }


        private void ValidarFechasEntrantes(ObservableCollection<FechasTutorias> fechas)
        {
            if (fechas == null)
            {
                throw new ArgumentException("El arreglo de fechas está vacío.");
            }
            else if (fechas.Count != 3)
            {
                throw new ArgumentException("Se intentó registrar ["+fechas.Count+"] fecha(s) de tutoría. " +
                "Se deben registrar exactamente tres (3) fechas de " +
                "tutoría por programa educativo, cada periodo escolar.");
            }
            foreach (Dominio.FechasTutorias fecha in fechas)
            {
                ValidarFechaNoEsNula(fecha);
                ValidarIdPeriodo(fecha);
                ValidarIdProgramaEducativo(fecha);
                ValidarNumeroSesion(fecha);
            }
        }

        private void ValidarFechaNoEsNula(FechasTutorias fecha) 
        {
            if (fecha == null)
            {
                throw new ArgumentException("Las fechas están nulas.");
            }
        }

        private void ValidarIdPeriodo(FechasTutorias fecha)
        {
            if (fecha.IdPeriodo < 1)
            {
                throw new ArgumentException("Las fechas tienen un periodo escolar inválido.");
            }
        }

        private void ValidarIdProgramaEducativo(FechasTutorias fecha)
        {
            if (fecha.IdProgramaEducativo < 1)
            {
                throw new ArgumentException("Las fechas tienen un programa educativo inválido.");
            }
        }

        private void ValidarNumeroSesion(FechasTutorias fecha)
        {
            if (fecha.NumeroSesion <= 0 || fecha.NumeroSesion >= 4)
            {
                throw new ArgumentException("Las fechas tienen un numero de sesión inválido.");
            }
        }

        enum Resultados 
        { 
        
        }
    }

}
