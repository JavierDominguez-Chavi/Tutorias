using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Presentacion
{
    public class PeriodosEscolaresVistaModelo
    {
        public ICollectionView PeriodosEscolares { get; private set; }
        private PeriodosEscolaresDAO periodosEscolaresDAO;
        private FechasTutoriasDAO fechasTutoriasDAO;
        private int idProgramaEducativo = SingletonUsuario.Instance.IdProgramaEducativo;
        public PeriodosEscolaresVistaModelo()
        {
            periodosEscolaresDAO = new PeriodosEscolaresDAO();
            fechasTutoriasDAO = new FechasTutoriasDAO();
            this.PeriodosEscolares = 
                CollectionViewSource
                .GetDefaultView(
                    periodosEscolaresDAO
                    .ObtenerPeriodosEscolaresPorProgramaEducativo(idProgramaEducativo));
        }

        public ICollectionView ActualizarLista()
        {
            this.PeriodosEscolares =
                CollectionViewSource
                .GetDefaultView(
                    periodosEscolaresDAO
                    .ObtenerPeriodosEscolaresPorProgramaEducativo(idProgramaEducativo));
            this.PeriodosEscolares.Refresh();
            return this.PeriodosEscolares;
        }

        public void ObtenerFechasTutoriasDePeriodoEscolar(PeriodosEscolares periodoEscolar)
        {
            if (periodoEscolar.TieneFechasDeTutoria)
            {
                periodoEscolar.FechasTutorias =
                this.fechasTutoriasDAO
                    .ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(
                        periodoEscolar.IdPeriodoEscolar, this.idProgramaEducativo);
            }
            else
            {
                GenerarFechasComodin(periodoEscolar);
            }
            
        }

        public void GenerarFechasComodin(PeriodosEscolares periodoEscolar)
        {
            periodoEscolar.FechasTutorias.Clear();
            for (int numeroSesion = 1; numeroSesion < 4; numeroSesion++)
            {
                periodoEscolar.FechasTutorias.Add(new FechasTutorias
                {
                    NumeroSesion = numeroSesion,
                    IdPeriodo = periodoEscolar.IdPeriodoEscolar,
                    PeriodosEscolares = periodoEscolar,
                    FechaTutoria = periodoEscolar.FechaInicio,
                    FechaCierreDeReporte = periodoEscolar.FechaFin,
                    IdProgramaEducativo = this.idProgramaEducativo
                });

            }
        }

        public bool AsignarFechasDeTutoria(PeriodosEscolares periodoEscolar)
        {
            bool operacionExitosa = false;
            if (periodoEscolar.TieneFechasDeTutoria)
            {
                operacionExitosa = this.fechasTutoriasDAO.ModificarFechasDeTutoria(periodoEscolar.FechasTutorias);
            }
            else
            { 
                operacionExitosa = this.fechasTutoriasDAO.RegistrarFechasDeTutoria(periodoEscolar.FechasTutorias);
                periodoEscolar.TieneFechasDeTutoria = operacionExitosa;
            }
            return operacionExitosa;
        }
    }
}
