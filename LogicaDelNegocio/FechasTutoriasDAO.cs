using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDelNegocio
{
    public class FechasTutoriasDAO
    {
        public Dominio.FechasTutorias ObtenerFechaTutoriaPorIdFechaTutoria(int idFechaTutoria)
        {
            Dominio.FechasTutorias fechaTutoriaObtenida;
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var fechaTutoriaEncontrada = entidadesTutorias.FechasTutorias.Find(idFechaTutoria);
                fechaTutoriaObtenida = new Dominio.FechasTutorias(fechaTutoriaEncontrada);
            }
            return fechaTutoriaObtenida;
        }

        public ObservableCollection<Dominio.FechasTutorias>
        ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(int idPeriodoEscolar, int idProgramaEducativo)
        {
            ObservableCollection<Dominio.FechasTutorias> fechasTutoriasObtenidas
             = new ObservableCollection<Dominio.FechasTutorias>();
            if (idPeriodoEscolar > 0 && idProgramaEducativo > 0)
            {
                using (var entidades = new EntidadesTutorias())
                {
                    var periodo = entidades.PeriodosEscolares.Find(idPeriodoEscolar);
                    var fechasEncontradas =
                        this.ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativoSinConvertir(idPeriodoEscolar,
                                                                                                  idProgramaEducativo);
                    foreach (AccesoADatos.FechasTutorias fecha in fechasEncontradas)
                    {
                        fecha.PeriodosEscolares = periodo;
                        fechasTutoriasObtenidas.Add(new Dominio.FechasTutorias(fecha));
                    }
                }
            }
            return fechasTutoriasObtenidas;
        }

        internal ObservableCollection<AccesoADatos.FechasTutorias>
        ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativoSinConvertir(int idPeriodoEscolar, int idProgramaEducativo)
        {
            ObservableCollection<AccesoADatos.FechasTutorias> fechasTutoriasObtenidas;
            using (var entidades = new EntidadesTutorias())
            {
                var periodo = entidades.PeriodosEscolares.Find(idPeriodoEscolar);
                var fechasTutoriasEncontradas = entidades.Entry(periodo)
                                                         .Collection(p => p.FechasTutorias)
                                                         .Query()
                                                         .Where(f => f.IdProgramaEducativo == idProgramaEducativo)
                                                         .ToList();
                fechasTutoriasObtenidas = new ObservableCollection<AccesoADatos.FechasTutorias>(fechasTutoriasEncontradas);
            }
            return fechasTutoriasObtenidas;
        }


        public bool RegistrarFechasDeTutoria(ObservableCollection<Dominio.FechasTutorias>fechasNuevas)
        {
            new ValidadorFechasTutorias(fechasNuevas);
            bool registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                List<AccesoADatos.FechasTutorias> fechasAccesoADatos = new List<AccesoADatos.FechasTutorias>(); 
                foreach(Dominio.FechasTutorias fechaDeDominio in fechasNuevas)
                {
                    AccesoADatos.FechasTutorias fechaAccesoADatos = fechaDeDominio.AAccesoADatos();
                    entidades.FechasTutorias.Add(fechaAccesoADatos);
                    fechasAccesoADatos.Add(fechaAccesoADatos);
                }
                int numeroDeRegistros = entidades.SaveChanges();
                registroExitoso = numeroDeRegistros == 3;

                for (int i = 0; i < numeroDeRegistros; i++)
                {
                    fechasNuevas[i].IdFechaTutoria = fechasAccesoADatos[i].IdFechaTutoria;
                }
            }
            return registroExitoso;
        }
        public bool ModificarFechasDeTutoria(ObservableCollection<Dominio.FechasTutorias> fechasExistentes)
        {
            ValidadorFechasTutorias validador = new ValidadorFechasTutorias(fechasExistentes);
            bool modificacionExitosa = false;
            using (var entidades = new EntidadesTutorias())
            {
                foreach (Dominio.FechasTutorias fecha in fechasExistentes)
                {
                    var fechaTutoriaEncontrada =
                        entidades.FechasTutorias
                                 .SingleOrDefault(f => f.IdFechaTutoria == fecha.IdFechaTutoria);
                    if (fechaTutoriaEncontrada != null)
                    {
                        fechaTutoriaEncontrada.FechaTutoria = fecha.FechaTutoria;
                        fechaTutoriaEncontrada.FechaCierreDeReporte = fecha.FechaCierreDeReporte;
                    }
                }
                int count = entidades.SaveChanges();
                modificacionExitosa = count > 0;
            }
            return modificacionExitosa;
        }
        public ObservableCollection<Dominio.FechasTutorias> ObtenerFechasDeTutoriaActualesPorIdPeriodoEscolarProgramaEducativo(int idPeriodoEscolar, int idProgramaEducativo)
        {
            ObservableCollection<Dominio.FechasTutorias> fechasTutoriasObtenidas = new ObservableCollection<Dominio.FechasTutorias>();
            if (idPeriodoEscolar > 0)
            {
                using (var entidades = new EntidadesTutorias())
                {
                    var fechasEncontradas = (from FechasTutorias in entidades.FechasTutorias where FechasTutorias.IdPeriodo == idPeriodoEscolar
                                             && FechasTutorias.FechaCierreDeReporte > DateTime.Now
                                             && FechasTutorias.FechaTutoria > DateTime.Now
                                             && FechasTutorias.IdProgramaEducativo == idProgramaEducativo select FechasTutorias).ToList(); 
                    foreach (AccesoADatos.FechasTutorias fecha in fechasEncontradas)
                    {
                        fechasTutoriasObtenidas.Add(new Dominio.FechasTutorias(fecha));
                    }
                }
            }
            return fechasTutoriasObtenidas;

        }
        public ObservableCollection<Dominio.FechasTutorias> ObtenerFechasDeTutoriaDelPeriodoEscolarPorIdPeriodoEscolarProgramaEducativo(int idPeriodoEscolar, int idProgramaEducativo)
        {
            ObservableCollection<Dominio.FechasTutorias> fechasTutoriasObtenidas = new ObservableCollection<Dominio.FechasTutorias>();
            if (idPeriodoEscolar > 0)
            {
                using (var entidades = new EntidadesTutorias())
                {
                    var fechasEncontradas = (from FechasTutorias in entidades.FechasTutorias
                                             where FechasTutorias.IdPeriodo == idPeriodoEscolar
                                             && FechasTutorias.IdProgramaEducativo == idProgramaEducativo
                                             select FechasTutorias).ToList();
                    foreach (AccesoADatos.FechasTutorias fecha in fechasEncontradas)
                    {
                        fechasTutoriasObtenidas.Add(new Dominio.FechasTutorias(fecha));
                    }
                }
            }
            return fechasTutoriasObtenidas;

        }

        public ObservableCollection<Dominio.FechasTutorias> ObtenerFechasTutoriasPeriodoEscolar(int idPeriodoEscolar)
        {
            ObservableCollection<Dominio.FechasTutorias> fechasTutoriasObtenidas = new ObservableCollection<Dominio.FechasTutorias>();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var fechasTutoriasEncontradas = (from FechasTutorias in entidadesTutorias.FechasTutorias
                                                 where FechasTutorias.IdPeriodo == idPeriodoEscolar
                                                 select FechasTutorias).ToList().OrderBy(x => x.NumeroSesion);
                if (fechasTutoriasEncontradas.FirstOrDefault() != null)
                {
                    foreach (AccesoADatos.FechasTutorias fechaTutoria in fechasTutoriasEncontradas)
                    {
                        fechasTutoriasObtenidas.Add(new Dominio.FechasTutorias(fechaTutoria));
                    }
                }
            }
            return fechasTutoriasObtenidas;
        }

        public Dominio.FechasTutorias ObtenerIdFechaTutoriaActualPorFechaDelSistema(DateTime fechaDelSistema)
        {
            Dominio.FechasTutorias fechaTutoriasObtenida = new Dominio.FechasTutorias();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var fechaTutoriaEncontrada = (from FechasTutorias in entidadesTutorias.FechasTutorias
                                              where FechasTutorias.FechaTutoria <= fechaDelSistema &&
                                              FechasTutorias.FechaCierreDeReporte >= fechaDelSistema
                                              select FechasTutorias).ToList();
                if (fechaTutoriaEncontrada.FirstOrDefault() != null)
                {
                    fechaTutoriasObtenida.IdFechaTutoria = fechaTutoriaEncontrada.FirstOrDefault().IdFechaTutoria;
                    return fechaTutoriasObtenida;
                }
            }
            return null;
        }
    }
}
