using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;

namespace LogicaDelNegocio
{
    public class PeriodosEscolaresDAO
    {
        public ObservableCollection<Dominio.PeriodosEscolares> ObtenerPeriodosEscolares()
        {
            ObservableCollection<Dominio.PeriodosEscolares> periodosEscolaresObtenidos =
                new ObservableCollection<Dominio.PeriodosEscolares>();
            using (var entidades = new EntidadesTutorias())
            {
                var periodosEscolaresEntontrados = entidades.PeriodosEscolares.ToList().OrderByDescending(x => x.FechaFin);
                if (periodosEscolaresEntontrados.FirstOrDefault() != null)
                {
                    foreach (AccesoADatos.PeriodosEscolares periodoEscolarEncontrado in periodosEscolaresEntontrados)
                    {
                        periodosEscolaresObtenidos.Add(new Dominio.PeriodosEscolares(periodoEscolarEncontrado));
                    }
                }
            }
            return periodosEscolaresObtenidos;
        }

        public ObservableCollection<Dominio.PeriodosEscolares>
        ObtenerPeriodosEscolaresPorProgramaEducativo(int idProgramaEducativo)
        {
            ObservableCollection<Dominio.PeriodosEscolares> periodosEscolaresObtenidos =
                new ObservableCollection<Dominio.PeriodosEscolares>();
            using (var entidades = new EntidadesTutorias())
            {
                var periodosEscolaresEntontrados = entidades.PeriodosEscolares.ToList();
                foreach (AccesoADatos.PeriodosEscolares periodoEscolarEncontrado in periodosEscolaresEntontrados)
                {
                    Dominio.PeriodosEscolares periodoEscolarObtenido = new Dominio.PeriodosEscolares(periodoEscolarEncontrado);
                    periodoEscolarObtenido.TieneFechasDeTutoria =

                        new FechasTutoriasDAO().ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativoSinConvertir
                            (periodoEscolarEncontrado.IdPeriodoEscolar,idProgramaEducativo)
                                .Count > 0;

                    periodosEscolaresObtenidos.Add(periodoEscolarObtenido);
                }
            }
            return periodosEscolaresObtenidos;
        }

        public int BuscarPeriodoEscolarPorNombre(String nombrePeriodoEscolar)
        {
            int idPeriodoEscolarExistente = 0;
            using(var entidades = new EntidadesTutorias())
            {
                PeriodosEscolares periodoEscolarEncontrado = entidades.PeriodosEscolares.FirstOrDefault(pE => pE.Nombre == nombrePeriodoEscolar);
                if (periodoEscolarEncontrado != null)
                {
                    idPeriodoEscolarExistente = entidades.PeriodosEscolares.FirstOrDefault(pE => pE.Nombre == nombrePeriodoEscolar).IdPeriodoEscolar;
                }
            }
            return idPeriodoEscolarExistente;
        }

        public bool RegistrarPeriodoEscolar(Dominio.PeriodosEscolares periodoEscolar)
        {
            bool registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.PeriodosEscolares periodoEscolarAccesoADatos = periodoEscolar.AAccesoADatos();
                entidades.PeriodosEscolares.Add(periodoEscolarAccesoADatos);
                registroExitoso = entidades.SaveChanges() > 0;
            }
            return registroExitoso;
        }
        public Dominio.PeriodosEscolares ObtenerPeriodoEscolarActual()
        {
            Dominio.PeriodosEscolares periodoEscolarObtenido = new Dominio.PeriodosEscolares();
            using (var entidades = new EntidadesTutorias())
            {
                var periodoEncontrado = entidades.PeriodosEscolares
                    .FirstOrDefault(pE => pE.FechaInicio < DateTime.Now && pE.FechaFin > DateTime.Now);
                if (periodoEncontrado != null)
                {
                    periodoEscolarObtenido = new Dominio.PeriodosEscolares(periodoEncontrado);
                }
            }
            return periodoEscolarObtenido;
        }

        public bool ModificarPeriodoEscolar(Dominio.PeriodosEscolares periodoEscolar)
        {
            bool actualizacionExitosa = false;
            using (var entidades = new EntidadesTutorias())
            {
                var periodoEscolarEncontrado = entidades.PeriodosEscolares.SingleOrDefault(pE => pE.IdPeriodoEscolar == periodoEscolar.IdPeriodoEscolar);
                if (periodoEscolarEncontrado != null)
                {
                    periodoEscolarEncontrado.IdPeriodoEscolar = periodoEscolar.IdPeriodoEscolar;
                    periodoEscolarEncontrado.FechaInicio = periodoEscolar.FechaInicio;
                    periodoEscolarEncontrado.FechaFin = periodoEscolar.FechaFin;
                    periodoEscolarEncontrado.Nombre = periodoEscolar.Nombre;
                }
                actualizacionExitosa = entidades.SaveChanges() > 0;
            }
            return actualizacionExitosa;
        }

        public bool EliminarPeriodoEscolar(Dominio.PeriodosEscolares periodosEscolares)
        {
            bool eliminacionExitosa = false;
            using (var entidades = new EntidadesTutorias())
            {
                var periodoEscolarEncontrado = entidades.PeriodosEscolares.SingleOrDefault(pE => pE.IdPeriodoEscolar == periodosEscolares.IdPeriodoEscolar);
                if (periodoEscolarEncontrado != null)
                {
                    entidades.PeriodosEscolares.Remove(periodoEscolarEncontrado);
                }
                eliminacionExitosa = entidades.SaveChanges() > 0;
            }
            return eliminacionExitosa;
        }
    }
}
