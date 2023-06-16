using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AccesoADatos;

namespace Dominio
{
    public class PeriodosEscolares : INotifyPropertyChanged
    {
        public PeriodosEscolares()
        {
            this.FechasTutorias = new ObservableCollection<FechasTutorias>();
            this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
        }

        public PeriodosEscolares(AccesoADatos.PeriodosEscolares periodosEscolares)
        {
            this.IdPeriodoEscolar = periodosEscolares.IdPeriodoEscolar;
            this.Nombre = periodosEscolares.Nombre;
            this.FechaInicio = periodosEscolares.FechaInicio;
            this.FechaFin = periodosEscolares.FechaFin;
            this.FechasTutorias = new ObservableCollection<FechasTutorias>();
            this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
        }


        public int IdPeriodoEscolar { get; set; }
        public string Nombre { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public string IntervaloInicioFin => $"{FechaInicio.ToShortDateString()} {"-"} {FechaFin.ToShortDateString()}";

        public string[] Meses = { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

        public ObservableCollection<FechasTutorias> FechasTutorias { get; set; }
        public ObservableCollection<ExperienciasEducativas_Academicos> ExperienciasEducativas_Academicos { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public String FechaInicioCorta
        {
            get
            {
                return this.FechaInicio.Date.ToString("dd/MM/yyyy");
            }

        }

        public String FechaFinCorta
        {
            get
            {
                return this.FechaFin.Date.ToString("dd/MM/yyyy");
            }
        }

        private bool tieneFechasDeTutoria;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public bool TieneFechasDeTutoria
        {
            get 
            {
                return tieneFechasDeTutoria;
            }
            set 
            { 
                this.tieneFechasDeTutoria = value;
                OnPropertyChanged();
            }

        }

        public AccesoADatos.PeriodosEscolares AAccesoADatos()
        {
            return new AccesoADatos.PeriodosEscolares()
            {
                Nombre = this.Nombre,
                FechaInicio = this.FechaInicio,
                FechaFin = this.FechaFin

            };

        }

        override
        public String ToString()
        {
            return this.Nombre;
        }
    }
}
