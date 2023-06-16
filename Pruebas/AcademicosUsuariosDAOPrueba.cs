using AccesoADatos;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Dominio;
using FluentAssertions;

namespace Pruebas
{
    [TestClass]
    public class AcademicosUsuariosDAOPrueba
    {
        private AcademicosUsuariosDAO academicosUsuariosDAO;
        private Dominio.AcademicosUsuarios academicoUsuario;
        private const int INDEX_ID_USUARIO_INVALIDO = -1;
        public AcademicosUsuariosDAOPrueba()
        {
            this.academicosUsuariosDAO = new AcademicosUsuariosDAO();
            this.academicoUsuario = new Dominio.AcademicosUsuarios();
        }

        [TestMethod]
        public void RegistrarAcademicoUsuario_Exito()
        {
            academicoUsuario.NombreUsuario = "UsuarioPreuba";
            academicoUsuario.Contrasena = "e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d" +
                "2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773";
            academicosUsuariosDAO.RegistrarAcademicoUsuario(academicoUsuario).Should().BeTrue();
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.AcademicosUsuarios academicoUsuarioRegistrado =
                    entidades.AcademicosUsuarios.FirstOrDefault(academicoBuscar => academicoBuscar.NombreUsuario == academicoUsuario.NombreUsuario &&
                        academicoBuscar.Contrasena == academicoUsuario.Contrasena);
                entidades.AcademicosUsuarios.Remove(academicoUsuarioRegistrado);
                entidades.SaveChanges();
            }
        }


        //Para la siguiente prueba es necesario desconectar la conexion con la Base de datos
        //O comentar la linea donde se registra la informacion:
        //entidades.AcademicosUsuarios.Add(academicoUsuarioRegistrar.ConvertirAcademicoUsuarioDominioEnAcademicoUsuarioAccesoADatos());
        [TestMethod]
        public void RegistrarAcademicoUsuario_Fallo_PerdidaDeConexion()
        {
            academicoUsuario.NombreUsuario = "UsuarioPreuba";
            academicoUsuario.Contrasena = "e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d" +
                "2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773";
            academicosUsuariosDAO.RegistrarAcademicoUsuario(academicoUsuario).Should().BeFalse();
        }

        [TestMethod]
        public void ObtenerIdAcademicoUsuarioPorNombreYContrasena_Exito()
        {
            int idAcademicoUsuarioEsperado = 1;
            academicoUsuario.NombreUsuario = "Restrepo";
            academicoUsuario.Contrasena = "5cf58927b41378bcc076b26b3b850a66ebcec3ace74f6b949da5405721dd39488a238f5afff793b5125" +
                "038bb1dd7184c1c11c47f4844d1ccbb310c9c75893b65";
            academicosUsuariosDAO.ObtenerIdAcademicoUsuarioPorNombreYContrasena(academicoUsuario).Should().Be(idAcademicoUsuarioEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoUsuarioPorNombreYContrasena_Fallo_NombreUsuarioInexistente()
        {
            int idAcademicoUsuarioEsperado = -1;
            academicoUsuario.NombreUsuario = "ESTEUSUARIONOEXISTE*()$";
            academicoUsuario.Contrasena = "5cf58927b41378bcc076b26b3b850a66ebcec3ace74f6b949da5405721dd39488a238f5afff793b5125" +
            "   038bb1dd7184c1c11c47f4844d1ccbb310c9c75893b65";
            academicosUsuariosDAO.ObtenerIdAcademicoUsuarioPorNombreYContrasena(academicoUsuario).Should().Be(idAcademicoUsuarioEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoUsuarioPorNombreYContrasena_Fallo_ContrasenaInexistente()
        {
            int idAcademicoUsuarioEsperado = -1;
            academicoUsuario.NombreUsuario = "Restrepo";
            academicoUsuario.Contrasena = "5cf58927b413Hola$$$";
            academicosUsuariosDAO.ObtenerIdAcademicoUsuarioPorNombreYContrasena(academicoUsuario).Should().Be(idAcademicoUsuarioEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoUsuarioPorNombreYContrasena_Fallo_NombreUsuarioInvalido()
        {
            int idAcademicoUsuarioEsperado = -1;
            academicoUsuario.Contrasena = "5cf58927b41378bcc076b26b3b850a66ebcec3ace74f6b949da5405721dd39488a238f5afff793b5125" +
            "   038bb1dd7184c1c11c47f4844d1ccbb310c9c75893b65";
            academicosUsuariosDAO.ObtenerIdAcademicoUsuarioPorNombreYContrasena(academicoUsuario).Should().Be(idAcademicoUsuarioEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoUsuarioPorNombreYContrasena_Fallo_ContrasenaInvalida()
        {
            int idAcademicoUsuarioEsperado = -1;
            academicoUsuario.NombreUsuario = "Restrepo";
            academicosUsuariosDAO.ObtenerIdAcademicoUsuarioPorNombreYContrasena(academicoUsuario).Should().Be(idAcademicoUsuarioEsperado);
        }

        [TestMethod]
        public void EliminarUsuariosAcademico_Exito()
        {
            academicoUsuario.NombreUsuario = "UsuarioPreuba";
            academicoUsuario.Contrasena = "e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d" +
                "2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773";
            academicosUsuariosDAO.RegistrarAcademicoUsuario(academicoUsuario);
            AccesoADatos.AcademicosUsuarios academicoUsuarioRegistrado = new AccesoADatos.AcademicosUsuarios();
            using (var entidades = new EntidadesTutorias())
            {
                    academicoUsuarioRegistrado =
                    entidades.AcademicosUsuarios.FirstOrDefault(academicoBuscar => academicoBuscar.NombreUsuario == academicoUsuario.NombreUsuario &&
                    academicoBuscar.Contrasena == academicoUsuario.Contrasena);
            }
            academicosUsuariosDAO.EliminarUsuariosAcademico(academicoUsuarioRegistrado.IdUsuario).Should().BeTrue();
        }

        [TestMethod]
        public void EliminarUsuariosAcademico_Fallo_IdAcademicoInexistente()
        {
            academicoUsuario.IdUsuario = -326;
            academicosUsuariosDAO.EliminarUsuariosAcademico(academicoUsuario.IdUsuario).Should().BeFalse();
        }

        [TestMethod]
        public void EliminarUsuariosAcademico_Fallo_IdAcademicoInvalido()
        {
            academicosUsuariosDAO.EliminarUsuariosAcademico(academicoUsuario.IdUsuario).Should().BeFalse();
        }

        [TestMethod]
        public void ObtenerRolesDeUsuario_DebeRetornarRolesCorrectos()
        {
          
            string usuario = "JuliAdmin";
            string contrasenia = "hola";
            var academicosRoles = academicosUsuariosDAO.ObtenerRolesDeUsuario(usuario, contrasenia);
            Assert.IsNotNull(academicosRoles);
        }

        [TestMethod]
        public void ObtenerUsuariosAcademico_DebeRetornarUsuariosCorrectos()
        {
          
            int idAcademico = 4021;
            int idProgramaEducativo = 1;
            var academicosUsuarios = academicosUsuariosDAO.ObtenerUsuariosAcademico(idAcademico, idProgramaEducativo);
            Assert.IsNotNull(academicosUsuarios);
        }
    }
}
