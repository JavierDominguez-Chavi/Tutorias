using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using FluentAssertions;
using LogicaDelNegocio;
using AccesoADatos;


namespace Pruebas
{
    [TestClass]
    public class TutoriasAcademicasDAOPrueba
    {
        private Dominio.TutoriasAcademicas tutoriaAcademica;
        private TutoriasAcademicasDAO tutoriasAcademicasDAO;
        public TutoriasAcademicasDAOPrueba()
        {
            this.tutoriaAcademica = new Dominio.TutoriasAcademicas();
            this.tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
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
        public void TestObtenerTutoriaAcademicaPorIdTutoriaAcademica_Exito()
        {
            Dominio.TutoriasAcademicas tutoriaAcademicaEsperada = new Dominio.TutoriasAcademicas();
            tutoriaAcademicaEsperada.IdTutoriaAcademica = 2;
            tutoriaAcademicaEsperada.ComentarioGeneral = "Algunos alumnos llegaron tarde";
            tutoriaAcademicaEsperada.ReporteEntregado = true;
            tutoriaAcademicaEsperada.IdAcademico = 2;
            tutoriaAcademicaEsperada.IdFechaTuria = 3;
            tutoriaAcademica = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdTutoriaAcademica(tutoriaAcademicaEsperada.IdTutoriaAcademica);
            tutoriaAcademica.Should().BeEquivalentTo(tutoriaAcademicaEsperada,options =>
            {
                options.Excluding(info => info.FechasTutorias);
                return options;
            });
        }

        [TestMethod]
        public void TestObtenerTutoriaAcademicaPorIdTutoriaAcademica_FalloIdTutoriaAcademicaInexistente()
        {
            Dominio.TutoriasAcademicas tutoriaAcademicaEsperada = new Dominio.TutoriasAcademicas();
            tutoriaAcademicaEsperada.IdTutoriaAcademica = -2;
            tutoriaAcademica = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdTutoriaAcademica(tutoriaAcademicaEsperada.IdTutoriaAcademica);
            tutoriaAcademica.ComentarioGeneral.Should().BeNull();
        }

        [TestMethod]
        public void TestObtenerTutoriaAcademicaPorIdTutoriaAcademica_FalloIdTutoriaAcademicaInvalido()
        {
            Dominio.TutoriasAcademicas tutoriaAcademicaEsperada = new Dominio.TutoriasAcademicas();
            tutoriaAcademica = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdTutoriaAcademica(tutoriaAcademicaEsperada.IdTutoriaAcademica);
            tutoriaAcademica.ComentarioGeneral.Should().BeNull();
        }
        [TestMethod]
        public void TestObtenerTutoriaAcademicaPorIdFechaTutoria_Exito()
        {
            Dominio.TutoriasAcademicas tutoriaAcademicaEsperada = new Dominio.TutoriasAcademicas();
            tutoriaAcademicaEsperada.IdTutoriaAcademica = 3;
            tutoriaAcademicaEsperada.ComentarioGeneral = "Prueba";
            tutoriaAcademicaEsperada.ReporteEntregado = true;
            tutoriaAcademicaEsperada.IdAcademico = 1;
            tutoriaAcademicaEsperada.IdFechaTuria = 11;
            tutoriaAcademica = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdFechaTutoria(tutoriaAcademicaEsperada.IdFechaTuria);
            tutoriaAcademica.Should().BeEquivalentTo(tutoriaAcademicaEsperada, options =>
            {
                options.Excluding(info => info.FechasTutorias);
                return options;
            });
        }
        [TestMethod]
        public void TestObtenerTutoriaAcademicaPorIdFechaTutoria_FalloIdFechaNoExistente()
        {
            Dominio.TutoriasAcademicas tutoriaAcademicaEsperada = new Dominio.TutoriasAcademicas();
            tutoriaAcademicaEsperada.IdTutoriaAcademica = 3;
            tutoriaAcademicaEsperada.ComentarioGeneral = "Prueba";
            tutoriaAcademicaEsperada.ReporteEntregado = true;
            tutoriaAcademicaEsperada.IdAcademico = 1;
            tutoriaAcademicaEsperada.IdFechaTuria = 34;
            tutoriaAcademica = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdFechaTutoria(tutoriaAcademicaEsperada.IdFechaTuria);
            tutoriaAcademica.Should().NotBeEquivalentTo(tutoriaAcademicaEsperada);
        }

        [TestMethod]
        public void ConsultarInformacionCompletaTutoriaAcademica_Exito()
        {
            int idTutoriaAcademicaBuscar = 2;
            Dominio.TutoriasAcademicas tutoriaAcademicaEsperada = new Dominio.TutoriasAcademicas();
            tutoriaAcademicaEsperada.IdTutoriaAcademica = 2;
            tutoriaAcademicaEsperada.ComentarioGeneral = "Algunos alumnos llegaron tarde";
            tutoriaAcademicaEsperada.ReporteEntregado = true;
            tutoriaAcademicaEsperada.IdAcademico = 2;
            tutoriaAcademicaEsperada.IdFechaTuria = 3;
            tutoriaAcademica = tutoriasAcademicasDAO.ConsultarInformacionCompletaTutoriaAcademica(idTutoriaAcademicaBuscar);
            tutoriaAcademica.Should().BeEquivalentTo(tutoriaAcademicaEsperada, options =>
            {
                options.Excluding(info => info.FechasTutorias );
                return options;
            });
        }

        [TestMethod]
        public void ConsultarInformacionCompletaTutoriaAcademica_Fallo_IdTutoriaInexistente()
        {
            int idTutoriaAcademicaBuscar = -2;
            Dominio.TutoriasAcademicas tutoriaAcademicaEsperada = new Dominio.TutoriasAcademicas();
            tutoriaAcademicaEsperada.IdTutoriaAcademica = 2;
            tutoriaAcademicaEsperada.ComentarioGeneral = "Algunos alumnos llegaron tarde";
            tutoriaAcademicaEsperada.ReporteEntregado = true;
            tutoriaAcademicaEsperada.IdAcademico = 2;
            tutoriaAcademicaEsperada.IdFechaTuria = 3;
            tutoriaAcademica = tutoriasAcademicasDAO.ConsultarInformacionCompletaTutoriaAcademica(idTutoriaAcademicaBuscar);
            tutoriaAcademica.Should().NotBeEquivalentTo(tutoriaAcademicaEsperada, options =>
            {
                options.Excluding(info => info.FechasTutorias);
                return options;
            });
        }

        [TestMethod]
        public void GuardarInformacionTutoriaAcademica_Exito()
        {
            tutoriaAcademica.IdTutoriaAcademica = 1;
            tutoriaAcademica.ComentarioGeneral = "TEST";
            tutoriaAcademica.ReporteEntregado = true;
            tutoriasAcademicasDAO.GuardarInformacionTutoriaAcademica(tutoriaAcademica,false).Should().BeTrue();
        }

        [TestMethod]
        public void GuardarInformacionTutoriaAcademica_Fallo_IdTutoriaInexistente()
        {
            tutoriaAcademica.IdTutoriaAcademica = -11;
            tutoriaAcademica.ComentarioGeneral = "TEST";
            tutoriaAcademica.ReporteEntregado = true;
            tutoriasAcademicasDAO.GuardarInformacionTutoriaAcademica(tutoriaAcademica,false).Should().BeFalse();

        }

        [TestMethod]
        public void GuardarInformacionTutoriaAcademica_Fallo_IdTutoriaInvalido()
        {
            tutoriaAcademica.ComentarioGeneral = "TEST";
            tutoriaAcademica.ReporteEntregado = true;
            tutoriasAcademicasDAO.GuardarInformacionTutoriaAcademica(tutoriaAcademica, false).Should().BeFalse();

        }

        [TestMethod]
        public void ObtenerTutoriasAcademicasPorIdFechaTutoriaEIdAcademicoExitoso()
        {
            int ID_FECHA_TUTORIA = 11;
            int ID_ACADEMICO = 5;
            Assert.AreEqual(tutoriasAcademicasDAO.ObtenerTutoriasAcademicasPorIdFechaTutoriaEIdAcademico(ID_FECHA_TUTORIA, ID_ACADEMICO).Count,
                1);
        }

        [TestMethod]
        public void ValidarQueExistanReportesDeTutoria_Exito()
        {
            int idFechaTutoria = 1;
            tutoriasAcademicasDAO.ValidarQueExistanReportesDeTutoria(idFechaTutoria).Should().BeTrue();
        }

        [TestMethod]
        public void ValidarQueExistanReportesDeTutoria_Fallo_IdFechaTutoriaInexistente()
        {
            int idFechaTutoria = -1;
            tutoriasAcademicasDAO.ValidarQueExistanReportesDeTutoria(idFechaTutoria).Should().BeFalse();
        }

        [TestMethod]
        public void ObtenerComentariosGeneralesDeTutoriasAcademicas_Exito()
        {
            int idFechaTutoria = 1;
            tutoriasAcademicasDAO.ObtenerComentariosGeneralesDeTutoriasAcademicas(idFechaTutoria).Count.Should().Be(1);
        }

        [TestMethod]
        public void ObtenerComentariosGeneralesDeTutoriasAcademicas_Fallo_IdTutoriaInexistente()
        {
            int idFechaTutoria = -1;
            tutoriasAcademicasDAO.ObtenerComentariosGeneralesDeTutoriasAcademicas(idFechaTutoria).Count.Should().Be(0);
        }
    }
}
