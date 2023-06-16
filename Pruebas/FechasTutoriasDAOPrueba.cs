using System;
using System.Collections.Generic;
using System.Text;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using FluentAssertions;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace Pruebas
{
    [TestClass]
    public class FechasTutoriasDAOPrueba
    {
        private FechasTutoriasDAO fechasTutoriasDAO;
        private FechasTutorias fechaTutoria;
        public FechasTutoriasDAOPrueba()
        {
            this.fechasTutoriasDAO = new FechasTutoriasDAO();
            this.fechaTutoria = new FechasTutorias();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ObtenerFechasTutoriasPorPeriodosEscolaresYProgramaEducativo()
        {
            int tamañoEsperado = 3;
            int idPeriodo = 6;
            int idProgramaEducativo = 2;
            int tamañoObtenido = fechasTutoriasDAO.ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(idPeriodo, idProgramaEducativo).Count;
            Assert.AreEqual(tamañoEsperado, tamañoObtenido);
        }

        [TestMethod]

        public void ObtenerFechaTutoriaPorIdTutoria_Exito()
        {
            FechasTutorias fechaTutoriaEsperada = new FechasTutorias();
            fechaTutoriaEsperada.IdFechaTutoria = 1;
            fechaTutoriaEsperada.FechaTutoria = DateTime.Parse("2023-02-07");
            fechaTutoriaEsperada.FechaCierreDeReporte = DateTime.Parse("2010-02-23");
            fechaTutoriaEsperada.NumeroSesion = 1;
            fechaTutoriaEsperada.IdPeriodo = 2;
            fechaTutoriaEsperada.IdProgramaEducativo = 1;
            fechaTutoria = fechasTutoriasDAO.ObtenerFechaTutoriaPorIdFechaTutoria(fechaTutoriaEsperada.IdFechaTutoria);
            fechaTutoria.Should().BeEquivalentTo(fechaTutoriaEsperada, options =>
            {
                options.Excluding(info => info.PeriodosEscolares);
                return options;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void ObtenerFechaTutoriaPorIdTutoria_FalloIdFechaTutoriaInexistente()
        {
            FechasTutorias fechaTutoriaEsperada = new FechasTutorias();
            fechaTutoriaEsperada.IdFechaTutoria = -1;
            fechaTutoria = fechasTutoriasDAO.ObtenerFechaTutoriaPorIdFechaTutoria(fechaTutoriaEsperada.IdFechaTutoria);
            fechaTutoria.Should().BeNull(fechaTutoria.FechaCierreDeReporte.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void ObtenerFechaTutoriaPorIdTutoria_FalloIdFechaTutoriaInvalido()
        {
            FechasTutorias fechaTutoriaEsperada = new FechasTutorias();
            fechaTutoria = fechasTutoriasDAO.ObtenerFechaTutoriaPorIdFechaTutoria(fechaTutoriaEsperada.IdFechaTutoria);
            fechaTutoria.Should().BeNull(fechaTutoria.FechaCierreDeReporte.ToString());
        }

        public void ObtenerFechasTutoriasPorPeriodosEscolaresYProgramaEducativo_Fallo1()
        {
            int tamañoEsperado = 0;
            int idPeriodo = -1;
            int idProgramaEducativo = 2;
            int tamañoObtenido = fechasTutoriasDAO.ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(idPeriodo, idProgramaEducativo).Count;
            Assert.AreEqual(tamañoEsperado, tamañoObtenido);
        }

        [TestMethod]
        public void ObtenerFechasTutoriasPorPeriodosEscolaresYProgramaEducativo_Fallo2()
        {
            int tamañoEsperado = 0;
            int idPeriodo = 6;
            int idProgramaEducativo = 0;
            int tamañoObtenido = fechasTutoriasDAO.ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(idPeriodo, idProgramaEducativo).Count;
            Assert.AreEqual(tamañoEsperado, tamañoObtenido);
        }

        [TestMethod]
        public void ObtenerFechasTutoriasPorPeriodosEscolaresYProgramaEducativo_Fallo3()
        {
            int tamañoEsperado = 0;
            int idPeriodo = 0;
            int idProgramaEducativo = 0;
            int tamañoObtenido = fechasTutoriasDAO.ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(idPeriodo, idProgramaEducativo).Count;
            Assert.AreEqual(tamañoEsperado, tamañoObtenido);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void ObtenerFechasTutoriasPorPeriodosEscolaresYProgramaEducativo_Fallo4()
        {
            int idPeriodo = 99;
            int idProgramaEducativo = 2;
            fechasTutoriasDAO.ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(idPeriodo, idProgramaEducativo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void ObtenerFechasTutoriasPorPeriodosEscolaresYProgramaEducativo_Fallo5()
        {
            int idPeriodo = 1;
            int idProgramaEducativo = 200;
            fechasTutoriasDAO.ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(idPeriodo, idProgramaEducativo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void ObtenerFechasTutoriasPorPeriodosEscolaresYProgramaEducativo_Fallo6()
        {
            int idPeriodo = 200;
            int idProgramaEducativo = 200;
            fechasTutoriasDAO.ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(idPeriodo, idProgramaEducativo);
        }


        [TestMethod]
        public void RegistrarFechasDeTutoria_Exito()
        {   //Se prueba sólo el éxito, puesto que los escenarios de falla son de ValidadorFechasTutorias, el cual ya fue probado.
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("05/02/2020");
            periodo.FechaFin = DateTime.Parse("15/08/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/02/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/02/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 6;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 6;
            sesion2.NumeroSesion = 2;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 6;
            sesion3.NumeroSesion = 3;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas =
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            bool resultadoEsperado = true;
            bool resultadoObtenido =  this.fechasTutoriasDAO.RegistrarFechasDeTutoria(fechas);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void RegistrarFechasDeTutoria_Fallo_PeriodoInexistente()
        {   //Se prueba sólo el éxito, puesto que los escenarios de falla son de ValidadorFechasTutorias, el cual ya fue probado.
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("05/02/2020");
            periodo.FechaFin = DateTime.Parse("15/08/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/02/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/02/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 99;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 6;
            sesion2.NumeroSesion = 2;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 6;
            sesion3.NumeroSesion = 3;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas =
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            bool resultadoEsperado = true;
            bool resultadoObtenido = this.fechasTutoriasDAO.RegistrarFechasDeTutoria(fechas);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void RegistrarFechasDeTutoria_Fallo_ProgramaInexistente()
        {   //Se prueba sólo el éxito, puesto que los escenarios de falla son de ValidadorFechasTutorias, el cual ya fue probado.
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("05/02/2020");
            periodo.FechaFin = DateTime.Parse("15/08/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/02/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/02/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 6;
            sesion1.IdProgramaEducativo = 999;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 6;
            sesion2.NumeroSesion = 2;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 6;
            sesion3.NumeroSesion = 3;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas =
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            bool resultadoEsperado = true;
            bool resultadoObtenido = this.fechasTutoriasDAO.RegistrarFechasDeTutoria(fechas);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        public void ModificarFechasDeTutoria_Exito()
        {   //Se prueba sólo el éxito, puesto que los escenarios de falla son de ValidadorFechasTutorias, el cual ya fue probado.
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("05/02/2020");
            periodo.FechaFin = DateTime.Parse("15/08/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.IdFechaTutoria = 39;
            sesion1.FechaTutoria = DateTime.Parse("07/03/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("15/03/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 6;
            sesion1.IdProgramaEducativo = 2;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.IdFechaTutoria = 40;
            sesion2.FechaTutoria = DateTime.Parse("16/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("17/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 6;
            sesion2.NumeroSesion = 2;
            sesion2.IdProgramaEducativo = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.IdFechaTutoria = 41;
            sesion3.FechaTutoria = DateTime.Parse("18/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("19/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 6;
            sesion3.NumeroSesion = 3;
            sesion3.IdProgramaEducativo = 2;
            ObservableCollection<Dominio.FechasTutorias> fechas =
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            bool resultadoEsperado = true;
            bool resultadoObtenido = this.fechasTutoriasDAO.ModificarFechasDeTutoria(fechas);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        public void ModificarFechasDeTutoria_Fallo_FechaInexistente()
        {   //Se prueba sólo el éxito, puesto que los escenarios de falla son de ValidadorFechasTutorias, el cual ya fue probado.
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("05/02/2020");
            periodo.FechaFin = DateTime.Parse("15/08/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.IdFechaTutoria = 999;
            sesion1.FechaTutoria = DateTime.Parse("10/03/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("15/03/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 999;
            sesion1.IdProgramaEducativo = 2;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.IdFechaTutoria = 40;
            sesion2.FechaTutoria = DateTime.Parse("16/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("17/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 6;
            sesion2.NumeroSesion = 2;
            sesion2.IdProgramaEducativo = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.IdFechaTutoria = 41;
            sesion3.FechaTutoria = DateTime.Parse("18/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("19/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 6;
            sesion3.NumeroSesion = 3;
            sesion3.IdProgramaEducativo = 2;
            ObservableCollection<Dominio.FechasTutorias> fechas =
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            bool resultadoEsperado = false;
            bool resultadoObtenido = this.fechasTutoriasDAO.ModificarFechasDeTutoria(fechas);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }
        [TestMethod]
        public void ObtenerFechasDeTutoriaActualesPorIdPeriodoEscolarProgramaEducativo_Exito()
        {
            int cantidadEsperada = 1;
            int idPeriodoEscolar = 2;
            int idProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechasObtenidas = 
                fechasTutoriasDAO.ObtenerFechasDeTutoriaActualesPorIdPeriodoEscolarProgramaEducativo(idPeriodoEscolar, idProgramaEducativo);
            int canidadObtenida = fechasObtenidas.Count;
            Assert.AreEqual(cantidadEsperada, canidadObtenida);
        }
        [TestMethod]
        public void ObtenerFechasDeTutoriaActualesPorIdPeriodoEscolarProgramaEducativo_PeriodoNoExiste()
        {
            int cantidadEsperada = 0;
            int idPeriodoEscolar = 10;
            int idProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechasObtenidas =
                fechasTutoriasDAO.ObtenerFechasDeTutoriaActualesPorIdPeriodoEscolarProgramaEducativo(idPeriodoEscolar, idProgramaEducativo);
            int canidadObtenida = fechasObtenidas.Count;
            Assert.AreEqual(cantidadEsperada, canidadObtenida);
        }
        [TestMethod]
        public void ObtenerFechasDeTutoriaActualesPorIdPeriodoEscolarProgramaEducativo_ProgramaEducativoNoExiste()
        {
            int cantidadEsperada = 0;
            int idPeriodoEscolar = 2;
            int idProgramaEducativo = 10;
            ObservableCollection<Dominio.FechasTutorias> fechasObtenidas =
                fechasTutoriasDAO.ObtenerFechasDeTutoriaActualesPorIdPeriodoEscolarProgramaEducativo(idPeriodoEscolar, idProgramaEducativo);
            int canidadObtenida = fechasObtenidas.Count;
            Assert.AreEqual(cantidadEsperada, canidadObtenida);
        }
        public void ObtenerFechasDeTutoriaDelPeriodoEscolarPorIdPeriodoEscolarProgramaEducativo_Exito()
        {
            int cantidadEsperada = 3;
            int idPeriodoEscolar = 2;
            int idProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechasObtenidas =
                fechasTutoriasDAO.ObtenerFechasDeTutoriaDelPeriodoEscolarPorIdPeriodoEscolarProgramaEducativo(idPeriodoEscolar, idProgramaEducativo);
            int canidadObtenida = fechasObtenidas.Count;
            Assert.AreEqual(cantidadEsperada, canidadObtenida);
        }
        [TestMethod]
        public void ObtenerFechasDeTutoriaDelPeriodoEscolarPorIdPeriodoEscolarProgramaEducativo_PeriodoNoExiste()
        {
            int cantidadEsperada = 0;
            int idPeriodoEscolar = 10;
            int idProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechasObtenidas =
                fechasTutoriasDAO.ObtenerFechasDeTutoriaDelPeriodoEscolarPorIdPeriodoEscolarProgramaEducativo(idPeriodoEscolar, idProgramaEducativo);
            int canidadObtenida = fechasObtenidas.Count;
            Assert.AreEqual(cantidadEsperada, canidadObtenida);
        }
        
        [TestMethod]
        public void ObtenerFechasDeTutoriaDelPeriodoEscolarPorIdPeriodoEscolarProgramaEducativo_ProgramaEducativoNoExiste()
        {
            int cantidadEsperada = 0;
            int idPeriodoEscolar = 2;
            int idProgramaEducativo = 10;
            ObservableCollection<Dominio.FechasTutorias> fechasObtenidas =
                fechasTutoriasDAO.ObtenerFechasDeTutoriaDelPeriodoEscolarPorIdPeriodoEscolarProgramaEducativo(idPeriodoEscolar, idProgramaEducativo);
            int canidadObtenida = fechasObtenidas.Count;
            Assert.AreEqual(cantidadEsperada, canidadObtenida);
        }

        [TestMethod]
        public void ObtenerFechasTutoriasPeriodoEscolar_Exito()
        {
            int idPeriodoEscolar = 2;
            fechasTutoriasDAO.ObtenerFechasTutoriasPeriodoEscolar(idPeriodoEscolar).Count.Should().Be(2);
        }

        [TestMethod]
        public void ObtenerFechasTutoriasPeriodoEscolar_Fallo_IdPeriodoInexistente()
        {
            int idPeriodoEscolar = -2;
            fechasTutoriasDAO.ObtenerFechasTutoriasPeriodoEscolar(idPeriodoEscolar).Count.Should().Be(0);
        }
    }
}
