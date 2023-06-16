using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Dominio;
using FluentAssertions;
namespace Pruebas
{
    [TestClass]
    public class ValidadorPrueba
    {
        [TestMethod]
        public void ValidarCadenaAlfanumerica()
        {
            String nombre = "CADENA VALIDA 123";
            Assert.IsTrue(
                Validador.ValidarCadenaAlfanumerica(nombre)
            );
        }

        [TestMethod]
        public void ValidarCadenaAlfanumerica_Fallo()
        {
            String nombre = "CADENA INVALIDA--";
            Assert.IsFalse(
                Validador.ValidarCadenaAlfanumerica(nombre)
            );
        }

        [TestMethod]
        public void ValidarCadenaAlfanumerica_Fallo2()
        {
            String nombre = "CADENA.INVALIDA";
            Assert.IsFalse(
                Validador.ValidarCadenaAlfanumerica(nombre)
            );
        }

        [TestMethod]
        public void ValidarCadenaAlfabetica_Exito()
        {
            String nombre = "VALida";
            Validador.ValidarCadenaAlfabetica(nombre).Should().Be(true);
        }

        [TestMethod]
        public void ValidarCadenaAlfabetica_Fallo_CadenaNoAlfabetica()
        {
            String nombre = "INVALida12345";
            Validador.ValidarCadenaAlfabetica(nombre).Should().Be(false);
        }

        [TestMethod]
        public void ValidarCadenaAlfabetica_Fallo_CadenaVacia()
        {
            String nombre = "";
            Validador.ValidarCadenaAlfabetica(nombre).Should().Be(false);
        }

        [TestMethod]
        public void ValidarCorreoElectronico_Exito()
        {
            String correo = "correoprueba@prueba.prueba";
            Validador.ValidarCorreoElectronico(correo).Should().Be(true);
        }

        [TestMethod]
        public void ValidarCorreoElectronico_Falla_CorreoInvalido()
        {
            String correo = "correoprueba@prueba";
            Validador.ValidarCorreoElectronico(correo).Should().Be(false);
        }

        [TestMethod]
        public void ValidarCorreoElectronico_Falla_CorreoElectronicoInexistente()
        {
            String correo = "";
            Validador.ValidarCorreoElectronico(correo).Should().Be(false);
        }

        [TestMethod]
        public void ValidarLongitudCaracteresNombreYApellidos_Exito()
        {
            String longitudValida = "asnknsk aks   s d sd, d, ,asd ,a s,.d as,. d,sa d,";
            Validador.ValidarLongitudCaracteresNombreYApellidos(longitudValida).Should().Be(true);
        }

        [TestMethod]
        public void ValidarLongitudCaracteresNombreYApellidos_Fallo_LongitudMaximaExcedida()
        {
            String longitudValida = "asnknsk aks   s d sd, d, ,asd ,a s,.d as,. d,sa d, ";
            Validador.ValidarLongitudCaracteresNombreYApellidos(longitudValida).Should().Be(false);
        }

        [TestMethod]
        public void ValidarLongitudCaracteresCorreoElectronicoPersonal_Exito()
        {
            String longitudValida = "asnknsk aks   s d sd, d, ,asd ,a s,.d as,. d,sa d,knzxlknakxnlkas klnxlks s xklsnx sknxklnssknxankna";
            Validador.ValidarLongitudCaracteresCorreoElectronicoPersonal(longitudValida).Should().Be(true);
        }

        [TestMethod]
        public void ValidarLongitudCaracteresCorreoElectronicoPersonal_Fallo_LongitudMaximaExcedida()
        {
            String longitudValida = "asnknsk aks   s d sd, d, ,asd ,a s,.d as,. d,sa d,knzxlknakxnlkas klnxlks s xklsnx sknxklnssknxankna ";
            Validador.ValidarLongitudCaracteresCorreoElectronicoPersonal(longitudValida).Should().Be(false);
        }

        [TestMethod]
        public void ValidarLongitudCaracteresCorreoElectronicoInstitucional_Exito()
        {
            String longitudValida = "asnknsk aks   s d sd, d, ,asd ";
            Validador.ValidarLongitudCaracteresCorreoElectronicoInstitucional(longitudValida).Should().Be(true);
        }

        [TestMethod]
        public void ValidarLongitudCaracteresCorreoElectronicoInstitucional_Fallo_LongitudMaximaExcedida()
        {
            String longitudValida = "asnknsk aks   s d sd, d, ,asd  ";
            Validador.ValidarLongitudCaracteresCorreoElectronicoInstitucional(longitudValida).Should().Be(false);
        }

        [TestMethod]
        public void ValidarLongitudMatricula_Exito()
        {
            String longitudValida = "S20015697";
            Validador.ValidarLongitudCaracteresMatricula(longitudValida).Should().Be(true);
        }

        [TestMethod]
        public void ValidarLongitudMatricula_Fallo_LongitudMaximaExcedida()
        {
            String longitudInvalida = "zS200156977";
            Validador.ValidarLongitudCaracteresMatricula(longitudInvalida).Should().Be(false);
        }

        [TestMethod]
        public void ValidarLongitudMatricula_Fallo_CadenaVacia()
        {
            String longitudInvalida = "";
            Validador.ValidarLongitudCaracteresMatricula(longitudInvalida).Should().Be(false);
        }
    }
}
