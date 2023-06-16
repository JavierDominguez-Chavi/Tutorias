using AccesoADatos;
using Dominio;
using FluentAssertions;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Pruebas
{
    [TestClass]
    public class PeriodosEscolaresDAOPrueba
    {
        private PeriodosEscolaresDAO periodosEscolaresDAO = new PeriodosEscolaresDAO();
        private Dominio.PeriodosEscolares periodoEscolar = new Dominio.PeriodosEscolares();

        [TestMethod]
        public void ObtenerPeriodosEscolares()
        {
            int tamañoEsperado = 0;
            using (var entidades = new EntidadesTutorias())
            {
                tamañoEsperado = entidades.PeriodosEscolares.ToList().Count;
            }
            int tamañoObtenido = periodosEscolaresDAO.ObtenerPeriodosEscolares().Count;
            Assert.AreEqual(tamañoEsperado, tamañoObtenido);
        }

        [TestMethod]
        public void BuscarPeriodoEscolarPorNombre_ExistenteExistoso()
        {
            Dominio.PeriodosEscolares periodoEscolarDominio = new Dominio.PeriodosEscolares(){ FechaFin = DateTime.Today,
                FechaInicio = DateTime.Today.AddDays(30)};
            String nombrePeriodo = periodoEscolarDominio.Meses[periodoEscolarDominio.FechaInicio.Month] + " " +
                periodoEscolarDominio.FechaInicio.Year + " - " + periodoEscolarDominio.Meses[periodoEscolarDominio.FechaFin.Month]
                + " " + periodoEscolarDominio.FechaFin.Year;
            periodoEscolarDominio.Nombre = nombrePeriodo;
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.PeriodosEscolares periodoEscolarAccesoADatos = periodoEscolarDominio.AAccesoADatos();
                entidades.PeriodosEscolares.Add(periodoEscolarAccesoADatos);
                entidades.SaveChanges();
            }
            int resultadoObtenido = periodosEscolaresDAO.BuscarPeriodoEscolarPorNombre(nombrePeriodo);
            using (var entidades = new EntidadesTutorias())
            {
                entidades.PeriodosEscolares.Remove(entidades.PeriodosEscolares.FirstOrDefault(
                    pE => pE.Nombre == nombrePeriodo &&
                    pE.FechaInicio == periodoEscolarDominio.FechaInicio &&
                    pE.FechaFin == periodoEscolarDominio.FechaFin));
                entidades.SaveChanges();
            }
            Assert.IsTrue(resultadoObtenido != 0);
        }
        [TestMethod]
        public void BuscarPeriodoEscolarPorNombre_InexistenteExitoso()
        {
            string periodoEscolarExistente = "MES_INVALIDO 1995 - MES_INVALIDO 1995";
            int resultadoObtenido = periodosEscolaresDAO.BuscarPeriodoEscolarPorNombre(periodoEscolarExistente);
            Assert.IsTrue(resultadoObtenido == 0);
        }

        [TestMethod]
        public void RegistrarPeriodoEscolar_Exitoso()
        {
            bool resultadoObtenido = false;
            bool existePeriodoEscolar = false;
            Dominio.PeriodosEscolares periodoEscolar = new Dominio.PeriodosEscolares();
            periodoEscolar.Nombre = "ENERO 00001 - DICIEMBRE 9999";
            periodoEscolar.FechaInicio = DateTime.MinValue;
            periodoEscolar.FechaFin = DateTime.MaxValue;
            resultadoObtenido = periodosEscolaresDAO.RegistrarPeriodoEscolar(periodoEscolar);
            using (var entidades = new EntidadesTutorias())
            {
                existePeriodoEscolar = (entidades.PeriodosEscolares.FirstOrDefault(pE => pE.Nombre == periodoEscolar.Nombre) != null);
                entidades.PeriodosEscolares.Remove(entidades.PeriodosEscolares.FirstOrDefault(pE => pE.Nombre == periodoEscolar.Nombre));
                entidades.SaveChanges();
            }
            Assert.IsTrue(existePeriodoEscolar && resultadoObtenido);
        }
        [TestMethod]
        public void ObtenerProgramaEducativoActual_Exitoso()
        {
            Dominio.PeriodosEscolares periodoEscolarEsperado = new Dominio.PeriodosEscolares();
            DateTime fechaInicioEsperada = new DateTime(2023, 02, 01);
            DateTime fechaFinEsperada = new DateTime(2023, 07, 31);
            periodoEscolarEsperado.Nombre = "FEBRERO 2023 - JULIO 2023";
            periodoEscolarEsperado.FechaInicio = fechaInicioEsperada;
            periodoEscolarEsperado.FechaFin = fechaFinEsperada;
            periodoEscolarEsperado.IdPeriodoEscolar = 2;
            periodoEscolar = periodosEscolaresDAO.ObtenerPeriodoEscolarActual();
            periodoEscolar.Should().BeEquivalentTo(periodoEscolarEsperado);
        }
        [TestMethod]
        public void ObtenerProgramaEducativoActual_Error()
        {
            Dominio.PeriodosEscolares periodoEscolarEsperado = new Dominio.PeriodosEscolares();
            DateTime fechaInicioEsperada = new DateTime(2023, 08, 01);
            DateTime fechaFinEsperada = new DateTime(2024, 01, 31);
            periodoEscolarEsperado.Nombre = "AGOSTO 2023 - ENERO 2024";
            periodoEscolarEsperado.FechaInicio = fechaInicioEsperada;
            periodoEscolarEsperado.FechaFin = fechaFinEsperada;
            periodoEscolarEsperado.IdPeriodoEscolar = 4;
            periodoEscolar = periodosEscolaresDAO.ObtenerPeriodoEscolarActual();
            periodoEscolar.Should().NotBeEquivalentTo(periodoEscolarEsperado);
        }
        [TestMethod]
        public void ModificarPeriodoEscolar_Exitoso()
        {
            bool resultadoObtenido = false;
            Dominio.PeriodosEscolares periodosEscolares = new Dominio.PeriodosEscolares();
            periodosEscolares.Nombre = "DICIEMBRE 9999 - ENERO 0001";
            periodosEscolares.FechaFin = DateTime.MinValue;
            periodosEscolares.FechaInicio = DateTime.MaxValue;
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.PeriodosEscolares periodoEscolarAccesoADatos = periodosEscolares.AAccesoADatos();
                entidades.PeriodosEscolares.Add(periodoEscolarAccesoADatos);
                entidades.SaveChanges();
                periodosEscolares.IdPeriodoEscolar = entidades.PeriodosEscolares.FirstOrDefault(pE => pE.Nombre == periodosEscolares.Nombre).IdPeriodoEscolar;
            }
            periodosEscolares.Nombre = "NOVIEMBRE 9999 - FEBRERO 0001";
            resultadoObtenido = periodosEscolaresDAO.ModificarPeriodoEscolar(periodosEscolares);
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.PeriodosEscolares periodoEscolarAccesoADatos = entidades.PeriodosEscolares.FirstOrDefault(pe => pe.IdPeriodoEscolar == periodosEscolares.IdPeriodoEscolar);
                Console.WriteLine(periodoEscolarAccesoADatos.Nombre);
                entidades.PeriodosEscolares.Remove(periodoEscolarAccesoADatos);
                entidades.SaveChanges();
            }
            Assert.IsTrue(resultadoObtenido);
        }
    }
}
