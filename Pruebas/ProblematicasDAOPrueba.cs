using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using FluentAssertions;
using System.Collections.ObjectModel;
using LogicaDelNegocio;

namespace Pruebas
{
    [TestClass]
    public class ProblematicasDAOPrueba
    {
        private Problematicas problematicas;
        private ProblematicasDAO problematicasDAO;
        public ProblematicasDAOPrueba()
        {
            this.problematicas = new Problematicas();
            this.problematicasDAO = new ProblematicasDAO();
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
        public void ObtenerProblematicas_Exito()
        {
            int numeroDeProblematicasEsperadas = 2;
            ObservableCollection<Dominio.Problematicas> problematicas = new ObservableCollection<Problematicas>();
            problematicas = problematicasDAO.ObtenerProblematicas();
            problematicas.Count.Should().Be(numeroDeProblematicasEsperadas);
        }

        //Para la siguiente prueba es necesario desconectar la conexion con la Base de datos
        //O comentar las lineas donde se recupera la informacion:
        //Desde var problematicasEncontradas = entidadesTutorias.Problematicas.ToList(); hasta el return
        [TestMethod]
        public void ObtenerProblematicas_Fallo_PerdidaDeConexion()
        {
            int numeroDeProblematicasEsperadas = 0;
            ObservableCollection<Dominio.Problematicas> problematicas = new ObservableCollection<Problematicas>();
            problematicas = problematicasDAO.ObtenerProblematicas();
            problematicas.Count.Should().Be(numeroDeProblematicasEsperadas);
        }

        [TestMethod]
        public void RegistrarProblematicasAcademicas_Exito()
        {
            problematicas.Titulo = "Hola";
            problematicas.Descripcion = "Prueba";
            problematicas.IdTutoriaAcademica = 1;
            problematicas.NRC = 78494;
            problematicas.Matricula = "S19014012";
            problematicasDAO.RegistrarProblematicasAcademicas(problematicas).Should().BeTrue();
        }

        [TestMethod]
        public void RegistrarProblematicasAcademicas_Fallo_ProblematicaInvalida()
        {
            problematicas = null;
            problematicasDAO.RegistrarProblematicasAcademicas(problematicas).Should().BeFalse();
        }
        [TestMethod]
        public void ObtenerProblematicasPorIdTutoriaYMatriculaExitoso()
        {
            int ID_TUTORIA = 3;
            string MATRICULA = "S20015690";
            ObservableCollection<Dominio.Problematicas> problematicas = new ObservableCollection<Problematicas>();
            problematicas = problematicasDAO.ObtenerProblematicasPorIdTutoriaYMatricula(ID_TUTORIA, MATRICULA);
            Assert.AreEqual(problematicas.Count, 1);
        }

        [TestMethod]
        public void ObtenerProblematicasPorIdTutoriaYMatriculaFallido()
        {
            int ID_TUTORIA = -1;
            string MATRICULA = "S20015690";
            ObservableCollection<Dominio.Problematicas> problematicas = new ObservableCollection<Problematicas>();
            problematicas = problematicasDAO.ObtenerProblematicasPorIdTutoriaYMatricula(ID_TUTORIA, MATRICULA);
            Assert.AreEqual(problematicas.Count, 0);
        }

        [TestMethod]
        public void ModificarProblematica_Existente_ModificacionExitosa()
        {
      
            Dominio.Problematicas problematicaExistente = new Dominio.Problematicas
            {
                IdProblematica = 6,
                Titulo = "Título test",
                Descripcion = "Descripción test",
                IdTutoriaAcademica = 1,
                NRC = 78990,
                Matricula = "Matrícula existente"
            };

            bool resultado = problematicasDAO.ModificarProblematica(problematicaExistente);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ModificarProblematica_NoExistente_ModificacionFallida()
        {
            Dominio.Problematicas problematicaNoExistente = new Dominio.Problematicas
            {
                IdProblematica = 10,
                Titulo = "Título Test",
                Descripcion = "Descripción Test",
                IdTutoriaAcademica = 1,
                NRC = 78,
                Matricula = "S19014012"
            };

            bool resultado = problematicasDAO.ModificarProblematica(problematicaNoExistente);

            Assert.IsFalse(resultado);
        }
    }
}
