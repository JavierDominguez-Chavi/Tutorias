using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Dominio
{
    public class ExperienciasEducativas : INotifyPropertyChanged, IRevertibleChangeTracking
    {
        public int IdExperienciaEducativa { get; set; }

        private string nombre;
        public string Nombre
        {
            get { return this.nombre; }
            set
            {
                this.nombre = value;
                NotifyPropertyChanged();
            }

        }
        private string nombreComodin;
        public string NombreComodin
        {
            set
            {
                this.nombreComodin = value;
                NotifyPropertyChanged();
            }
            get { return this.nombreComodin; }
        }
        public ObservableCollection<ExperienciasEducativas_Academicos> ExperienciasEducativas_Academicos { get; set; }
        public ExperienciasEducativas()
        {
            this.NombreComodin = this.Nombre;
            this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
        }

        public ExperienciasEducativas(AccesoADatos.ExperienciasEducativas experienciasEducativas)
        {
            this.IdExperienciaEducativa = experienciasEducativas.IdExperienciaEducativa;
            this.Nombre = experienciasEducativas.Nombre;
            this.NombreComodin = experienciasEducativas.Nombre;

            this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
        }

        public AccesoADatos.ExperienciasEducativas AAccesoADatos() 
        {
            return new AccesoADatos.ExperienciasEducativas()
                    {
                        IdExperienciaEducativa = this.IdExperienciaEducativa,
                        Nombre = this.Nombre,
                    };
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsChanged => !NombreComodin.Equals(this.Nombre);
        public void RejectChanges()
        {
            if (!this.IsChanged)
            { return; }
            this.NombreComodin = Nombre;
        }
        public void AcceptChanges()
        {
            if (!this.IsChanged)
            { return; }
            this.Nombre = this.NombreComodin;
        }
    }
}
