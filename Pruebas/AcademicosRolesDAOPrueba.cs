using FluentAssertions;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;

namespace Pruebas
{
    [TestClass]
    public class AcademicosRolesDAOPrueba
    {
        private AcademicosRolesDAO academicosRolesDAO;
        private const int ID_PROGRAMA_EDUCATIVO = 1;
        private const int ID_TUTOR_ACADEMICO = 1039;
        private const int INDEX_PROGRAMA_EDUCATIVO_INVALIDO = -1;
        private const int NUMERO_ACADEMICOS = 11;
        private const int ID_ACADEMICO = 1;
        private const int ID_USUARIO = 1;
        private const int ID_ROL_SIN_ROL = 1265;
        private Dominio.AcademicosRoles academicoRol;
        public AcademicosRolesDAOPrueba()
        {
            this.academicosRolesDAO = new AcademicosRolesDAO();
            academicoRol = new Dominio.AcademicosRoles();
            academicoRol.idProgramaEducativo = ID_PROGRAMA_EDUCATIVO;
            academicoRol.idUsuario = ID_USUARIO;
            academicoRol.idAcademico = ID_ACADEMICO;
            academicoRol.idRol = ID_TUTOR_ACADEMICO;
        }

        [TestMethod]
        public void ObtenerAcademicosPorProgramaEducativo()
        {
            int resultadoEsperado = NUMERO_ACADEMICOS;
            int resultadoObtenido = academicosRolesDAO.ObtenerAcademicosPorProgramaEducativo(ID_PROGRAMA_EDUCATIVO).Count;
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        public void ObtenerAcademicosPorProgramaEducativoFallido()
        {
            int resultadoEsperado = 0;
            int resultadoObtenido = academicosRolesDAO.ObtenerAcademicosPorProgramaEducativo(INDEX_PROGRAMA_EDUCATIVO_INVALIDO).Count;
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        public void RegistrarAcademicoRol_Exito()
        {
            Boolean resultadoObtenido = academicosRolesDAO.RegistrarAcademicoRol(academicoRol);
            academicosRolesDAO.EliminarAcademicoRol(academicoRol);
            Assert.IsTrue(resultadoObtenido);
        }

        //Para la siguiente prueba es necesario desconectar la conexion con la Base de datos
        //O comentar la linea donde se registra la informacion:
        //entidades.AcademicosRoles.Add(academicoRolRegistrar.ConvertirAcademicoRolDominioEnAcademicoRolAccesoADatos());
        [TestMethod]
        public void RegistrarAcademicoRol_Fallo_PerdidaDeConexion()
        {
            academicosRolesDAO.RegistrarAcademicoRol(academicoRol).Should().BeFalse();
        }

        [TestMethod]
        public void EliminarAcademicoRol()
        {
            academicosRolesDAO.RegistrarAcademicoRol(academicoRol);
            bool resultadoObtenido = academicosRolesDAO.EliminarAcademicoRol(academicoRol);
            Assert.IsTrue(resultadoObtenido);
        }

        [TestMethod]
        public void ConsultarInformacionAcademico_Exito()
        {
            academicoRol.idAcademico = 2020;
            int numeroDeAcademicoRolesEsperado = 1;
            academicosRolesDAO.ConsultarInformacionAcademico(academicoRol.idAcademico).Count.Should().Be(numeroDeAcademicoRolesEsperado);
        }

        [TestMethod]
        public void ConsultarInformacionAcademico_Fallo_IdAcademicoInexistente()
        {
            academicoRol.idAcademico = -1;
            int numeroDeAcademicoRolesEsperado = 0;
            academicosRolesDAO.ConsultarInformacionAcademico(academicoRol.idAcademico).Count.Should().Be(numeroDeAcademicoRolesEsperado);
        }

        [TestMethod]
        public void ConsultarInformacionAcademico_Fallo_IdAcademicoInvalido()
        {
            int idAcademico = new int();
            int numeroDeAcademicoRolesEsperado = 0;
            academicosRolesDAO.ConsultarInformacionAcademico(idAcademico).Count.Should().Be(numeroDeAcademicoRolesEsperado);
        }

        [TestMethod]
        public void EliminarRolAcademicoPorProgramaEducativo_Exito()
        {
            academicoRol.idAcademico = 2020;
            academicoRol.idRol = 1265;
            academicoRol.idProgramaEducativo = 2;
            academicoRol.idUsuario = 1;
            academicosRolesDAO.RegistrarAcademicoRol(academicoRol);
            academicosRolesDAO.EliminarRolAcademicoPorProgramaEducativo(academicoRol).Should().BeTrue();
        }


        [TestMethod]
        public void EliminarRolAcademicoPorProgramaEducativo_Fallo_IdAcademicoInexistente()
        {
            academicoRol.idAcademico = -1;
            academicoRol.idRol = 1265;
            academicoRol.idProgramaEducativo = 2;
            academicoRol.idUsuario = 1;
            academicosRolesDAO.EliminarRolAcademicoPorProgramaEducativo(academicoRol).Should().BeFalse();
        }

        [TestMethod]
        public void EliminarRolAcademicoPorProgramaEducativo_Fallo_IdAcademicoInvalido()
        {
            academicoRol.idAcademico = new int();
            academicoRol.idRol = 1265;
            academicoRol.idProgramaEducativo = 2;
            academicoRol.idUsuario = 1;
            academicosRolesDAO.EliminarRolAcademicoPorProgramaEducativo(academicoRol).Should().BeFalse();
        }

        [TestMethod]
        public void EliminarRolAcademicoPorProgramaEducativo_Fallo_IdProgramaEducativoInexistente()
        {
            academicoRol.idAcademico = 2020;
            academicoRol.idRol = 1265;
            academicoRol.idProgramaEducativo = -2;
            academicoRol.idUsuario = 1;
            academicosRolesDAO.EliminarRolAcademicoPorProgramaEducativo(academicoRol).Should().BeFalse();
        }

        [TestMethod]
        public void EliminarRolAcademicoPorProgramaEducativo_Fallo_IdProgramaEducativoInvalido()
        {
            academicoRol.idAcademico = 2020;
            academicoRol.idRol = 1265;
            academicoRol.idProgramaEducativo = new int();
            academicoRol.idUsuario = 1;
            academicosRolesDAO.EliminarRolAcademicoPorProgramaEducativo(academicoRol).Should().BeFalse();
        }

        [TestMethod]
        public void ConsularTutoresAcademicosPorProgramaEducativo_Exito()
        {
            Dominio.ProgramasEducativos programasEducativos = new Dominio.ProgramasEducativos();
            programasEducativos.NombreProgramaEducativo = "INGENIERIA DE SOFTWARE";
            programasEducativos.IdProgramaEducativo = 1;
            int numeroDeTutoressEsperados = 2;
            academicosRolesDAO.ConsularTutoresAcademicosPorProgramaEducativo(programasEducativos.IdProgramaEducativo).Count.Should().Be(numeroDeTutoressEsperados);
        }

        [TestMethod]
        public void ConsularTutoresAcademicosPorProgramaEducativo_Fallo_IdProgramaEducativoInexistente()
        {
            Dominio.ProgramasEducativos programasEducativos = new Dominio.ProgramasEducativos();
            programasEducativos.NombreProgramaEducativo = "INGENIERIA DE SOFTWARE";
            programasEducativos.IdProgramaEducativo = -001;
            int numeroDeTutoressEsperados = 0;
            academicosRolesDAO.ConsularTutoresAcademicosPorProgramaEducativo(programasEducativos.IdProgramaEducativo).Count.Should().Be(numeroDeTutoressEsperados);
        }

        [TestMethod]
        public void ObtenerAcademicosUsuarios_DebeRetornarAcademicosUsuariosCorrecto()
        {
            int idAcademico = 4021;
            int idProgramaEducativo = 1;
            var academicosUsuarios = academicosRolesDAO.ObtenerAcademicosUsuarios(idAcademico, idProgramaEducativo);
            Assert.IsNotNull(academicosUsuarios);
            
        }

       

        [TestMethod]
        public void ObtenerProgramasEducativosDeAcademicos_DebeRetornarProgramasEducativosCorrectos()
        {
            string correoInstitucional = "mariohernandez02@uv.mx";
            var programasEducativos = academicosRolesDAO.ObtenerProgramasEducativosDeAcademicos(correoInstitucional);
            Assert.IsNotNull(programasEducativos);
            
        }

        [TestMethod]
        public void ModificarRolesDeAcademicos_DebeModificarRolesCorrectamente()
        {
            int idAcademico = 4021;
            int idProgramaEducativo = 1;
            int idUsuario = 7048;
            ObservableCollection<int> rolesAAsignar = new ObservableCollection<int> { 3807 };
            bool resultado = academicosRolesDAO.ModificarRolesDeAcademicos(idAcademico, idProgramaEducativo, idUsuario, rolesAAsignar);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void EliminarRolesDeAcademicos_DebeEliminarRolesCorrectamente()
        {
            // Arrange
            int idAcademico = 4021;
            int idProgramaEducativo = 1;
            int idUsuario = 7048;
            ObservableCollection<int> rolesAEliminar = new ObservableCollection<int> { 3807 };
            bool resultado = academicosRolesDAO.EliminarRolesDeAcademicos(idAcademico, idProgramaEducativo, idUsuario, rolesAEliminar);
            Assert.IsTrue(resultado);
        }
        [TestMethod]
        public void ObtenerRolesDeAcademicos_DebeRetornarRolesCorrectos()
        {
            string correoInstitucional = "mariohernandez02@uv.mx";
            int idUsuario = 7049;
            var roles = academicosRolesDAO.ObtenerRolesDeAcademicos(correoInstitucional, idUsuario);
            Assert.IsNotNull(roles);
           
        }

        [TestMethod]
        public void ValidarRolJefeDeCarreraPorProgramaEducativo_DebeRetornarTrueSiNoHayRolJefeDeCarrera()
        {
            int idProgramaEducativo = 1;
            bool resultado = academicosRolesDAO.ValidarRolJefeDeCarreraPorProgramaEducativo(idProgramaEducativo);
            Assert.IsTrue(resultado);
        }

    }
}
