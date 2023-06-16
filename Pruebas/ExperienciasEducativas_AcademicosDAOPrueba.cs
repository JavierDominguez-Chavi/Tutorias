using System;
using System.Collections.Generic;
using System.Text;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using FluentAssertions;
using System.Data.Entity.Infrastructure;
using AccesoADatos;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Pruebas
{
    [TestClass]
    public class ExperienciasEducativas_AcademicosDAOPrueba
    {
        private ExperienciasEducativas_AcademicosDAO experienciasEducativas_AcademicosDAO;
        private Dominio.ExperienciasEducativas_Academicos experienciasEducativas_Academicos;
        public ExperienciasEducativas_AcademicosDAOPrueba()
        {
            this.experienciasEducativas_AcademicosDAO = new ExperienciasEducativas_AcademicosDAO();
            this.experienciasEducativas_Academicos = new Dominio.ExperienciasEducativas_Academicos();
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
        public void ObtenerExperienciaEducativaAcademicoPorNRC_Exito()
        {
            Dominio.ExperienciasEducativas_Academicos experienciasEducativas_AcademicosEsperada = new Dominio.ExperienciasEducativas_Academicos();
            experienciasEducativas_AcademicosEsperada.NRC = 77879;
            experienciasEducativas_AcademicosEsperada.IdProgramaEducativo = 1;
            experienciasEducativas_AcademicosEsperada.IdExperienciaEducativa = 2;
            experienciasEducativas_AcademicosEsperada.IdAcademico = 1;
            experienciasEducativas_Academicos =
                experienciasEducativas_AcademicosDAO.ObtenerExperienciaEducativaAcademicoPorNRC(experienciasEducativas_AcademicosEsperada.NRC);
            experienciasEducativas_Academicos.Should().BeEquivalentTo(experienciasEducativas_AcademicosEsperada, options =>
            {
                options.Excluding(info => info.Academicos);
                options.Excluding(info => info.ExperienciasEducativas);
                return options;
            });
        }

        [TestMethod]
        public void ObtenerExperienciaEducativaAcademicoPorNRC_FalloNRCInexistente()
        {
            Dominio.ExperienciasEducativas_Academicos experienciasEducativas_AcademicosEsperada = new Dominio.ExperienciasEducativas_Academicos();
            experienciasEducativas_AcademicosEsperada.NRC = 00879;
            experienciasEducativas_Academicos =
                experienciasEducativas_AcademicosDAO.ObtenerExperienciaEducativaAcademicoPorNRC(experienciasEducativas_AcademicosEsperada.NRC);
            experienciasEducativas_Academicos.ExperienciasEducativas.Should().BeNull(); ;
        }

        [TestMethod]
        public void ObtenerExperienciaEducativaAcademicoPorNRC_FalloNRCInvalido()
        {
            Dominio.ExperienciasEducativas_Academicos experienciasEducativas_AcademicosEsperada = new Dominio.ExperienciasEducativas_Academicos();
            experienciasEducativas_Academicos =
                experienciasEducativas_AcademicosDAO.ObtenerExperienciaEducativaAcademicoPorNRC(experienciasEducativas_AcademicosEsperada.NRC);
            experienciasEducativas_Academicos.ExperienciasEducativas.Should().BeNull();
        }
        [TestMethod]
        public void AsignarExperienciaEducativaAProfesor()
        {
            bool resultadoEsperado = true;
            Dominio.ExperienciasEducativas_Academicos experienciaARegistrar = new Dominio.ExperienciasEducativas_Academicos();
            experienciaARegistrar.NRC = 23641;
            experienciaARegistrar.IdProgramaEducativo = 1;
            experienciaARegistrar.IdAcademico = 1;
            experienciaARegistrar.IdExperienciaEducativa = 1;
            bool resuktadoObtenido = experienciasEducativas_AcademicosDAO.AsignarExperienciaEducativaAAcademico(experienciaARegistrar); 
            Assert.AreEqual(resultadoEsperado, resuktadoObtenido);
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.ExperienciasEducativas_Academicos experienciaAsignada =
                    entidades.ExperienciasEducativas_Academicos.FirstOrDefault(experiencia => experiencia.NRC == experienciaARegistrar.NRC);
                experienciaAsignada.IdAcademico = null;
                entidades.SaveChanges();
            }
        }
        [TestMethod]
        public void AsignarExperienciaEducativaAProfesor_Fallo()
        {
            Dominio.ExperienciasEducativas_Academicos experienciaARegistrar = new Dominio.ExperienciasEducativas_Academicos();
            experienciaARegistrar.NRC = -1283;
            experienciaARegistrar.IdProgramaEducativo = -2920;
            experienciaARegistrar.IdAcademico = 2;
            experienciaARegistrar.IdExperienciaEducativa = 0;
            experienciasEducativas_AcademicosDAO.AsignarExperienciaEducativaAAcademico(experienciaARegistrar);
;
        }
        [TestMethod]
        public void ObtenerExperienciasEducativasSinAcademico()
        {
            int cantidadEsperada = 4;
            List<Dominio.ExperienciasEducativas_Academicos> experienciasObtenidas = experienciasEducativas_AcademicosDAO.ObtenerExperienciasEducativasSinAcademico();
            int cantidadObtenida = experienciasObtenidas.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }
        [TestMethod]
        public void ObtenerExperienciasEducativasSinAcademico_Fallo()
        {
            int cantidadEsperada = 12;
            List<Dominio.ExperienciasEducativas_Academicos> experienciasObtenidas = experienciasEducativas_AcademicosDAO.ObtenerExperienciasEducativasSinAcademico();
            int cantidadObtenida = experienciasObtenidas.Count;
            Assert.AreNotEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void obtenerExperieniciasEducativasPorPeriodo_DebeRetornarExperienciasEducativasPorPeriodo()
        {
            
            string nombrePeriodo = "FEBRERO 2023 - JULIO 2023";

            ObservableCollection<Dominio.ExperienciasEducativas_Academicos> experiencias = experienciasEducativas_AcademicosDAO.obtenerExperieniciasEducativasPorPeriodo(nombrePeriodo);

            Assert.IsNotNull(experiencias);
            Assert.AreEqual(10, experiencias.Count);
        }

        [TestMethod]
        public void ActualizarExperienciaEducativas_Academicos_DebeRetornarResultadoExitoso()
        {
            var experienciaEducativaAcademica = new Dominio.ExperienciasEducativas_Academicos();
            experienciaEducativaAcademica.NRC = 34561;
            experienciaEducativaAcademica.IdExperienciaEducativa = 1;
            experienciaEducativaAcademica.IdAcademico = 1;
            experienciaEducativaAcademica.IdProgramaEducativo = 1;
            int resultado = experienciasEducativas_AcademicosDAO.ActualizarExperienciaEducativas_Academicos(experienciaEducativaAcademica);

            Assert.AreEqual(200, resultado);
           
        }

        [TestMethod]
        public void RegistrarExperienciasEducativas_Academicos_DebeRetornarResultadoExitoso()
        {
            
            var experienciaEducativaAcademica = new Dominio.ExperienciasEducativas_Academicos();
            experienciaEducativaAcademica.NRC = 55555;
            experienciaEducativaAcademica.IdExperienciaEducativa = 1;
            experienciaEducativaAcademica.IdAcademico = 1;
            experienciaEducativaAcademica.IdProgramaEducativo = 1;
            int resultado = experienciasEducativas_AcademicosDAO.RegistrarExperienciasEducativas_Academicos(experienciaEducativaAcademica);
           
            Assert.AreEqual(200, resultado);
           
        }
    }
}
