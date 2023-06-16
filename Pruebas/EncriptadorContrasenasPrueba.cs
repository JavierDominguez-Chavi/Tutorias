using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Dominio;

namespace Pruebas
{
    [TestClass]
    public class EncriptadorContrasenasPrueba
    {
        public EncriptadorContrasenasPrueba()
        {
            
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
        public void Encriptar_Exito()
        {
            String contrasena = "Hola123";
            String contrasenaEncriptadaEsperada = "6c3d38e697548832678da3a85f70b61115639a468d51313ad8d84e89950b13668015bbb77" +
                "fe54ea862d360a96e64e020c601e6f1e2682abeb1642fafa511c9f9";
            EncriptadorContrasenas.Encriptar(contrasena).Should().Be(contrasenaEncriptadaEsperada);
        }
    }
}
