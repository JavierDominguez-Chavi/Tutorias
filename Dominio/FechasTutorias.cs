using System;
using System.Collections.ObjectModel;

namespace Dominio
{
    public class FechasTutorias
    {
        public FechasTutorias()
        {
            this.TutoriasAcademicas = new ObservableCollection<TutoriasAcademicas>();
        }

        public FechasTutorias(AccesoADatos.FechasTutorias fechasTutorias)
        {
            this.PeriodosEscolares = new Dominio.PeriodosEscolares(fechasTutorias.PeriodosEscolares);
            this.IdFechaTutoria = fechasTutorias.IdFechaTutoria;
            this.FechaTutoria = fechasTutorias.FechaTutoria;
            this.FechaCierreDeReporte = fechasTutorias.FechaCierreDeReporte;
            this.NumeroSesion = fechasTutorias.NumeroSesion;
            this.IdPeriodo = fechasTutorias.IdPeriodo;
            this.IdProgramaEducativo = fechasTutorias.IdProgramaEducativo;

            
            this.TutoriasAcademicas = new ObservableCollection<TutoriasAcademicas>();
        }

        public int IdFechaTutoria { get; set; }

        public System.DateTime FechaTutoria 
        {
            get;
            set;
        }
        public System.DateTime FechaCierreDeReporte 
        {
            get;
            set;
        }
        public int IdPeriodo { get; set; }
        public int NumeroSesion { get; set; }
        public int IdProgramaEducativo { get; set; }
        public string IntervaloInicioFin => $"{FechaTutoria.ToShortDateString()} {"-"} {FechaCierreDeReporte.ToShortDateString()}";
        public PeriodosEscolares PeriodosEscolares { get; set; }
        public ObservableCollection<TutoriasAcademicas> TutoriasAcademicas { get; set; }

        public string TipoDeSesion => DeterminarTipoDeSesion();

        public string DeterminarTipoDeSesion()
        {
            switch (NumeroSesion)
            {
                case 1:
                    return "PRIMERA TUTORÍA ACADÉMICA";
                case 2:
                    return "SEGUNDA TUTORÍA ACADÉMICA";
                case 3:
                    return "TERCERA TUTORÍA ACADÉMICA";
                default:
                    return "TUTORÍA ACADÉMICA EXTRAORDINARIA";
            }
            return "";
        }

        public AccesoADatos.FechasTutorias AAccesoADatos()
        {
            return new AccesoADatos.FechasTutorias()
            {
                FechaTutoria = this.FechaTutoria,
                FechaCierreDeReporte = this.FechaCierreDeReporte,
                IdPeriodo = this.IdPeriodo,
                IdProgramaEducativo = this.IdProgramaEducativo,
                NumeroSesion = this.NumeroSesion
            };
        }
    }
}
