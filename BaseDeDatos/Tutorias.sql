USE [Tutorias]
GO
ALTER TABLE [dbo].[TutoriasAcademicas] DROP CONSTRAINT [FK_TutoriasAcademicas_Academicos]
GO
ALTER TABLE [dbo].[TutoriasAcademicas] DROP CONSTRAINT [FK_TutoriasAcademicas.Id_FechaTuria]
GO
ALTER TABLE [dbo].[Soluciones] DROP CONSTRAINT [FK_Soluciones.IdProblematica]
GO
ALTER TABLE [dbo].[Soluciones] DROP CONSTRAINT [FK_Soluciones.Id_Academico]
GO
ALTER TABLE [dbo].[Problematicas] DROP CONSTRAINT [FK_Problematicas.NRC]
GO
ALTER TABLE [dbo].[Problematicas] DROP CONSTRAINT [FK_Problematicas.matricula]
GO
ALTER TABLE [dbo].[Problematicas] DROP CONSTRAINT [FK_Problematicas.Id_Tutoria_Academica]
GO
ALTER TABLE [dbo].[Horarios] DROP CONSTRAINT [FK_Horarios.matricula]
GO
ALTER TABLE [dbo].[Horarios] DROP CONSTRAINT [FK_Horarios.Id_Tutoria_academica]
GO
ALTER TABLE [dbo].[FechasTutorias] DROP CONSTRAINT [FK_FechasTutorias_ProgramasEducativos]
GO
ALTER TABLE [dbo].[FechasTutorias] DROP CONSTRAINT [FK_FechasTutorias.	Id_Periodo]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_PeriodosEscolares] DROP CONSTRAINT [FK_ExperienciasEducativas_PeriodosEscolares.NRC]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_PeriodosEscolares] DROP CONSTRAINT [FK_ExperienciasEducativas_PeriodosEscolares.Id_Periodo]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Estudiantes] DROP CONSTRAINT [FK_ExperienciasEducativas_Estudiantes.NRC]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Estudiantes] DROP CONSTRAINT [FK_ExperienciasEducativas_Estudiantes.matricula]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Academicos] DROP CONSTRAINT [FK_ExperienciasEduacativas_Academicos.Id_Programa_Educativo]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Academicos] DROP CONSTRAINT [FK_ExperienciasEduacativas_Academicos.Id_Experiencia_Educativa]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Academicos] DROP CONSTRAINT [FK_ExperienciasEduacativas_Academicos.Id_Academico]
GO
ALTER TABLE [dbo].[Estudiantes] DROP CONSTRAINT [FK_Estudiantes.idAcademico]
GO
ALTER TABLE [dbo].[Estudiantes] DROP CONSTRAINT [FK_Estudiantes.id_Programa_Educativo]
GO
ALTER TABLE [dbo].[AcademicosRoles] DROP CONSTRAINT [FK_AcademicosRoles.Id_Usuario]
GO
ALTER TABLE [dbo].[AcademicosRoles] DROP CONSTRAINT [FK_AcademicosRoles.Id_Rol]
GO
ALTER TABLE [dbo].[AcademicosRoles] DROP CONSTRAINT [FK_AcademicosRoles.Id_Programa_Educativo]
GO
ALTER TABLE [dbo].[AcademicosRoles] DROP CONSTRAINT [FK_AcademicosRoles.Id_Academico]
GO
/****** Object:  Table [dbo].[TutoriasAcademicas]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TutoriasAcademicas]') AND type in (N'U'))
DROP TABLE [dbo].[TutoriasAcademicas]
GO
/****** Object:  Table [dbo].[Soluciones]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Soluciones]') AND type in (N'U'))
DROP TABLE [dbo].[Soluciones]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
DROP TABLE [dbo].[Roles]
GO
/****** Object:  Table [dbo].[ProgramasEducativos]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProgramasEducativos]') AND type in (N'U'))
DROP TABLE [dbo].[ProgramasEducativos]
GO
/****** Object:  Table [dbo].[Problematicas]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Problematicas]') AND type in (N'U'))
DROP TABLE [dbo].[Problematicas]
GO
/****** Object:  Table [dbo].[PeriodosEscolares]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PeriodosEscolares]') AND type in (N'U'))
DROP TABLE [dbo].[PeriodosEscolares]
GO
/****** Object:  Table [dbo].[Horarios]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Horarios]') AND type in (N'U'))
DROP TABLE [dbo].[Horarios]
GO
/****** Object:  Table [dbo].[FechasTutorias]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FechasTutorias]') AND type in (N'U'))
DROP TABLE [dbo].[FechasTutorias]
GO
/****** Object:  Table [dbo].[ExperienciasEducativas_PeriodosEscolares]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExperienciasEducativas_PeriodosEscolares]') AND type in (N'U'))
DROP TABLE [dbo].[ExperienciasEducativas_PeriodosEscolares]
GO
/****** Object:  Table [dbo].[ExperienciasEducativas_Estudiantes]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExperienciasEducativas_Estudiantes]') AND type in (N'U'))
DROP TABLE [dbo].[ExperienciasEducativas_Estudiantes]
GO
/****** Object:  Table [dbo].[ExperienciasEducativas_Academicos]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExperienciasEducativas_Academicos]') AND type in (N'U'))
DROP TABLE [dbo].[ExperienciasEducativas_Academicos]
GO
/****** Object:  Table [dbo].[ExperienciasEducativas]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExperienciasEducativas]') AND type in (N'U'))
DROP TABLE [dbo].[ExperienciasEducativas]
GO
/****** Object:  Table [dbo].[Estudiantes]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Estudiantes]') AND type in (N'U'))
DROP TABLE [dbo].[Estudiantes]
GO
/****** Object:  Table [dbo].[AcademicosUsuarios]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AcademicosUsuarios]') AND type in (N'U'))
DROP TABLE [dbo].[AcademicosUsuarios]
GO
/****** Object:  Table [dbo].[AcademicosRoles]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AcademicosRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AcademicosRoles]
GO
/****** Object:  Table [dbo].[Academicos]    Script Date: 01/06/2023 12:05:42 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Academicos]') AND type in (N'U'))
DROP TABLE [dbo].[Academicos]
GO
/****** Object:  User [UsuarioTutorias]    Script Date: 01/06/2023 12:05:42 a. m. ******/
DROP USER [UsuarioTutorias]
GO
USE [master]
GO
/****** Object:  Database [Tutorias]    Script Date: 01/06/2023 12:05:42 a. m. ******/
DROP DATABASE [Tutorias]
GO
/****** Object:  Database [Tutorias]    Script Date: 01/06/2023 12:05:42 a. m. ******/
CREATE DATABASE [Tutorias]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Tutorias', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Tutorias.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Tutorias_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Tutorias_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Tutorias] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tutorias].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tutorias] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tutorias] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tutorias] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tutorias] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tutorias] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tutorias] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Tutorias] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tutorias] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tutorias] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tutorias] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tutorias] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tutorias] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tutorias] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tutorias] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tutorias] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Tutorias] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tutorias] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tutorias] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tutorias] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tutorias] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tutorias] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Tutorias] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tutorias] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Tutorias] SET  MULTI_USER 
GO
ALTER DATABASE [Tutorias] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tutorias] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Tutorias] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Tutorias] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Tutorias] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Tutorias] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Tutorias] SET QUERY_STORE = OFF
GO
USE [Tutorias]
GO
/****** Object:  User [UsuarioTutorias]    Script Date: 01/06/2023 12:05:42 a. m. ******/
CREATE USER [UsuarioTutorias] FOR LOGIN [UsuarioTutorias] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [UsuarioTutorias]
GO
/****** Object:  Table [dbo].[Academicos]    Script Date: 01/06/2023 12:05:42 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Academicos](
	[IdAcademico] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[ApellidoMaterno] [nvarchar](50) NOT NULL,
	[ApellidoPaterno] [nvarchar](50) NOT NULL,
	[CorreoPersonal] [nvarchar](100) NOT NULL,
	[CorreoInstitucional] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK__Academic__CC5EDD81FF466019] PRIMARY KEY CLUSTERED 
(
	[IdAcademico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AcademicosRoles]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicosRoles](
	[idAcademico] [int] NOT NULL,
	[idRol] [int] NOT NULL,
	[idProgramaEducativo] [int] NOT NULL,
	[idUsuario] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AcademicosUsuarios]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicosUsuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [nvarchar](50) NOT NULL,
	[Contrasena] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK__Academic__63C76BE24B5A9EEE] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiantes]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiantes](
	[Matricula] [nvarchar](10) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[ApellidoMaterno] [nvarchar](50) NOT NULL,
	[ApellidoPaterno] [nvarchar](50) NOT NULL,
	[SemestreQueCursa] [int] NOT NULL,
	[EnRiesgo] [bit] NOT NULL,
	[CorreoPersonal] [nvarchar](100) NOT NULL,
	[CorreoInstitucional] [nvarchar](30) NOT NULL,
	[IdProgramaEducativo] [int] NOT NULL,
	[IdAcademico] [int] NULL,
 CONSTRAINT [PK__Estudian__30962D14CBBD4784] PRIMARY KEY CLUSTERED 
(
	[Matricula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExperienciasEducativas]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperienciasEducativas](
	[IdExperienciaEducativa] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Experien__F35D69342F6EA1AE] PRIMARY KEY CLUSTERED 
(
	[IdExperienciaEducativa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExperienciasEducativas_Academicos]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperienciasEducativas_Academicos](
	[NRC] [int] NOT NULL,
	[IdProgramaEducativo] [int] NOT NULL,
	[IdExperienciaEducativa] [int] NOT NULL,
	[IdAcademico] [int] NOT NULL,
 CONSTRAINT [PK__Experien__C7DEEA5B90FA66AC] PRIMARY KEY CLUSTERED 
(
	[NRC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExperienciasEducativas_Estudiantes]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperienciasEducativas_Estudiantes](
	[NRC] [int] NOT NULL,
	[Matricula] [nvarchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExperienciasEducativas_PeriodosEscolares]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperienciasEducativas_PeriodosEscolares](
	[NRC] [int] NOT NULL,
	[IdPeriodo] [int] NOT NULL,
 CONSTRAINT [PK__Experien__C7DEEA5B94898825] PRIMARY KEY CLUSTERED 
(
	[NRC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FechasTutorias]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FechasTutorias](
	[IdFechaTutoria] [int] IDENTITY(1,1) NOT NULL,
	[FechaTutoria] [date] NOT NULL,
	[FechaCierreDeReporte] [date] NOT NULL,
	[NumeroSesion] [int] NOT NULL,
	[IdPeriodo] [int] NOT NULL,
	[IdProgramaEducativo] [int] NOT NULL,
 CONSTRAINT [PK__FechasTu__4B960741DF53C7C4] PRIMARY KEY CLUSTERED 
(
	[IdFechaTutoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Horarios]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horarios](
	[IdHorarios] [int] IDENTITY(1,1) NOT NULL,
	[Asistencia] [bit] NOT NULL,
	[Riesgo] [bit] NOT NULL,
	[HoraTutoria] [time](7) NOT NULL,
	[IdTutoriaAcademica] [int] NOT NULL,
	[Matricula] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK__Horarios__006C93F4B1431BDE] PRIMARY KEY CLUSTERED 
(
	[IdHorarios] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PeriodosEscolares]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeriodosEscolares](
	[IdPeriodoEscolar] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[FechaInicio] [date] NOT NULL,
	[FechaFin] [date] NOT NULL,
 CONSTRAINT [PK__Periodos__486580971B170035] PRIMARY KEY CLUSTERED 
(
	[IdPeriodoEscolar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Problematicas]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Problematicas](
	[IdProblematica] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [text] NOT NULL,
	[Descripcion] [text] NOT NULL,
	[IdTutoriaAcademica] [int] NOT NULL,
	[NRC] [int] NOT NULL,
	[Matricula] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK__Problema__4977106407CE1FDF] PRIMARY KEY CLUSTERED 
(
	[IdProblematica] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProgramasEducativos]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramasEducativos](
	[IdProgramaEducativo] [int] IDENTITY(1,1) NOT NULL,
	[NombreProgramaEducativo] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Programa__38C6630E0D62AB55] PRIMARY KEY CLUSTERED 
(
	[IdProgramaEducativo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [nchar](50) NOT NULL,
 CONSTRAINT [PK__Roles__55932E86FD0B2CF1] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Soluciones]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Soluciones](
	[IdSolucion] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Descripcion] [text] NOT NULL,
	[IdProblematica] [int] NOT NULL,
	[IdAcademico] [int] NOT NULL,
 CONSTRAINT [PK__Solucion__A392314FC0832B97] PRIMARY KEY CLUSTERED 
(
	[IdSolucion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TutoriasAcademicas]    Script Date: 01/06/2023 12:05:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TutoriasAcademicas](
	[IdTutoriaAcademica] [int] IDENTITY(1,1) NOT NULL,
	[ComentarioGeneral] [text] NOT NULL,
	[ReporteEntregado] [bit] NOT NULL,
	[IdAcademico] [int] NOT NULL,
	[IdFechaTuria] [int] NOT NULL,
 CONSTRAINT [PK__Tutorias__86F26D97AB4C3728] PRIMARY KEY CLUSTERED 
(
	[IdTutoriaAcademica] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Academicos] ON 

INSERT [dbo].[Academicos] ([IdAcademico], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [CorreoPersonal], [CorreoInstitucional]) VALUES (1, N'Mario Alberto', N'Pérez', N'Hernández', N'mariohernandez02@gmail.com', N'mariohernandez02@uv.mx')
INSERT [dbo].[Academicos] ([IdAcademico], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [CorreoPersonal], [CorreoInstitucional]) VALUES (2, N'Juan Luis', N'Herrera', N'López', N'julopez@gmail.com', N'julopez@uv.mx')
INSERT [dbo].[Academicos] ([IdAcademico], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [CorreoPersonal], [CorreoInstitucional]) VALUES (2020, N'María Angelica', N'Restrepo', N'Cordial', N'maricordial@gmail.com', N'macordial@uv.mx')
INSERT [dbo].[Academicos] ([IdAcademico], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [CorreoPersonal], [CorreoInstitucional]) VALUES (4018, N'Norma', N'Díaz', N'Maldonado', N'normita@gmail.com', N'nmaldonado@uv.mx')
INSERT [dbo].[Academicos] ([IdAcademico], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [CorreoPersonal], [CorreoInstitucional]) VALUES (4020, N'Julietas', N'Del Niño', N'Cruz', N'juliCruz09@gmal.com', N'JCruz@estudiantes.uv.mx')
INSERT [dbo].[Academicos] ([IdAcademico], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [CorreoPersonal], [CorreoInstitucional]) VALUES (4021, N'Mario', N'Ion', N'Cabrera', N'mario@gmail.com', N'mario@uv.com.mx')
INSERT [dbo].[Academicos] ([IdAcademico], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [CorreoPersonal], [CorreoInstitucional]) VALUES (4022, N'Ale', N'Munguia', N'Pedraza', N'Ale@gmail.com', N'AMunguia@uv.com.mx')
INSERT [dbo].[Academicos] ([IdAcademico], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [CorreoPersonal], [CorreoInstitucional]) VALUES (4024, N'Javier', N'Cabrera', N'Casas', N'casasJa@gmail.com', N'JCasas@uv.com.mx')
SET IDENTITY_INSERT [dbo].[Academicos] OFF
GO
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (1, 3807, 1, 7043)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (1, 1039, 1, 7049)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (2, 1039, 1, 7044)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (2020, 5610, 1, 7045)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4018, 1039, 1, 7046)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4020, 2938, 1, 7047)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4021, 8792, 1, 7048)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (2, 8792, 1, 7051)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (2020, 8792, 1, 7052)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4018, 8792, 1, 7053)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4020, 1039, 2, 7054)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4021, 3807, 2, 7055)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4022, 8792, 2, 7057)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4022, 1039, 2, 7059)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4024, 5610, 2, 7060)
INSERT [dbo].[AcademicosRoles] ([idAcademico], [idRol], [idProgramaEducativo], [idUsuario]) VALUES (4024, 1039, 2, 7061)
GO
SET IDENTITY_INSERT [dbo].[AcademicosUsuarios] ON 

INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7043, N'RestrepoJefe', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7044, N'JuanLTutor', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7045, N'MariAngCoor', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7046, N'MalNorTutor', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7047, N'JuliAdmin', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7048, N'MarIonProf', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7049, N'RestrepoTutor', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7051, N'JuanLProf', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7052, N'MariAngProf', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7053, N'MalNorProf', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7054, N'JuliTutor', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7055, N'MarIonJefe', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7057, N'AleProf', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7059, N'AleTutor', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7060, N'JavCoor', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
INSERT [dbo].[AcademicosUsuarios] ([IdUsuario], [NombreUsuario], [Contrasena]) VALUES (7061, N'JavTutor', N'e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773')
SET IDENTITY_INSERT [dbo].[AcademicosUsuarios] OFF
GO
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S18939345', N'Hector', N'Altamirano', N'Ruiz', 12, 1, N'Hecto98@hotmail.com', N'zS18939345@estudiantes.uv.mx', 2, 4020)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S19014012', N'Ale', N'Pedraza', N'Costa', 7, 0, N'GIRL@GURL.COM', N'S19014012@ESTUDIANTES.UV.MX', 1, 1)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S19014016', N'Rafa', N'Ñañez', N'Casas', 8, 1, N'rafa@gmail.com', N'zS19014016@estudiantes.uv.mx', 1, 2)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S19016655', N'Carlos', N'Muñoz', N'Alcantara', 9, 0, N'Carlos@gmail.com', N'zS19016655@estudiantes.uv.mx', 1, 2)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S20000000', N'Pedro', N'Perez', N'Perez', 1, 1, N'PEDRO@GMAIL.COM', N'S2000000@ESTUDIANTES.UV.MX', 1, 1)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S20000001', N'Marcos', N'Herrera', N'Herrera', 5, 0, N'Marcus@gmail.com', N'zS20000001@estudiantes.uv.mx', 1, 2)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S20006655', N'Alejandro', N'Fernández', N'Chacon', 6, 0, N'chacon@protomail.com', N'zS20006655@estudiantes.uv.mx', 1, 2)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S20015588', N'Rosaura', N'Herrera', N'Costa', 3, 1, N'rouss@gmail.com', N'zS20015588@estudiantes.uv.mx', 1, 4018)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S20015677', N'Sarai', N'Castillo', N'Castillo', 6, 0, N'sarita@gmail.com', N'zS20015677@estudiantes.uv.mx', 1, 4018)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S20015697', N'José Javier', N'Carmona', N'Domínguez', 6, 0, N'javier@gmail.com', N'zS20015697@estudiantes.uv.mx', 1, 2)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S20018989', N'Moises', N'Orduña', N'Cortez', 3, 1, N'moi@gmail.com', N'zS20018989@estudiantes.uv.mx', 1, 1)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S20436478', N'Oscar', N'Carsi', N'Olivares', 6, 0, N'carsiano@gmail.com', N'zS20436478@estudiantes.uv.mx', 2, 4024)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S21436478', N'David', N'Domínguez', N'Carmona', 2, 1, N'davi789@gmail.com', N'zS21436478@estudiantes.uv.mx', 2, 4024)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S22093485', N'Jose Luis', N'Ronzon', N'Cardenas', 2, 0, N'wicho@gmail.com', N'zS22093485@estudiantes.uv.mx', 2, 4022)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S22374859', N'Valentina', N'Carmona', N'Sandoval', 1, 0, N'valevale@gmail.com', N'zS22374859@estudiantes.uv.mx', 2, 4022)
INSERT [dbo].[Estudiantes] ([Matricula], [Nombre], [ApellidoMaterno], [ApellidoPaterno], [SemestreQueCursa], [EnRiesgo], [CorreoPersonal], [CorreoInstitucional], [IdProgramaEducativo], [IdAcademico]) VALUES (N'S23098756', N'Marisol', N'Castillo', N'Castillo', 1, 0, N'maris@gmail.com', N'zS23098756@estudiantes.uv.mx', 2, 4020)
GO
SET IDENTITY_INSERT [dbo].[ExperienciasEducativas] ON 

INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1, N'PROGRAMACION')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (2, N'BASES DE DATOS')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (3, N'REQUERIMIENTOS DE SOFTWARE')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (4, N'PRUEBA DE SOFTWARE')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (5, N'ADMINISTRACION DE BASES DE DATOS')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (6, N'PRUEBAS DE PENETRACION')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (7, N'INTRODUCCION A LA PROGRAMACION')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (8, N'DISEÑO DE SOFTWARE')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1002, N'LABORATORIO DE RESOLUCION DE PROBLEMAS')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1003, N'PRUEBAS DE PENETRACIÓN')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1004, N'DESARROLLO WEB')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1005, N'TECNOLOGÍAS PARA EL DESARROLLO DE SOFTWARE')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1006, N'DESARROLLO DE APLICACIONES')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1007, N'REDES')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1008, N'CYBERSEGURIDAD')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1009, N'PROGRAMACIÓN SEGURA')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1010, N'DISEÑO DE INTERFACES DE USUARIO')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1011, N'PROYECTO GUIADO')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1012, N'PRACTICAS DE INGENIERÍA DE SOFTWARE')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1013, N'EXPERIENCIA RECEPCIONAL')
INSERT [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa], [Nombre]) VALUES (1014, N'SERVICIO SOCIAL')
SET IDENTITY_INSERT [dbo].[ExperienciasEducativas] OFF
GO
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (34567, 2, 4, 4022)
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (46453, 2, 1003, 4022)
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (56678, 1, 8, 2020)
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (56778, 1, 5, 4021)
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (56789, 1, 3, 4018)
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (67754, 1, 1002, 4018)
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (76884, 1, 7, 2)
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (77879, 1, 2, 4021)
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (78494, 1, 1, 2)
INSERT [dbo].[ExperienciasEducativas_Academicos] ([NRC], [IdProgramaEducativo], [IdExperienciaEducativa], [IdAcademico]) VALUES (78990, 1, 6, 2020)
GO
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (34567, N'S18939345')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (78494, N'S20018989')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (77879, N'S20015697')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (78990, N'S20015677')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (56789, N'S20015588')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (56778, N'S20006655')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (76884, N'S20000001')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (56678, N'S20000000')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (67754, N'S19016655')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (78494, N'S19014016')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (77879, N'S19014012')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (78990, N'S20018989')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (56789, N'S20015697')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (56778, N'S20015677')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (76884, N'S20015588')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (56678, N'S20006655')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (67754, N'S20000001')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (46453, N'S23098756')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (34567, N'S22374859')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (34567, N'S22093485')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (34567, N'S21436478')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (46453, N'S22374859')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (46453, N'S18939345')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (46453, N'S22093485')
INSERT [dbo].[ExperienciasEducativas_Estudiantes] ([NRC], [Matricula]) VALUES (46453, N'S21436478')
GO
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (34567, 2)
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (46453, 2)
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (56678, 2)
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (56778, 2)
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (56789, 2)
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (67754, 2)
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (76884, 2)
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (77879, 2)
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (78494, 2)
INSERT [dbo].[ExperienciasEducativas_PeriodosEscolares] ([NRC], [IdPeriodo]) VALUES (78990, 2)
GO
SET IDENTITY_INSERT [dbo].[FechasTutorias] ON 

INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (1, CAST(N'2023-05-30' AS Date), CAST(N'2023-06-17' AS Date), 4, 2, 1)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (3, CAST(N'2023-02-09' AS Date), CAST(N'2023-02-11' AS Date), 1, 2, 1)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (5, CAST(N'2023-03-02' AS Date), CAST(N'2023-03-04' AS Date), 2, 2, 1)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (6, CAST(N'2023-08-15' AS Date), CAST(N'2023-08-17' AS Date), 1, 4, 1)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (7, CAST(N'2023-08-31' AS Date), CAST(N'2023-09-02' AS Date), 2, 4, 1)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (8, CAST(N'2023-05-02' AS Date), CAST(N'2023-05-12' AS Date), 3, 2, 1)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (10, CAST(N'2023-09-30' AS Date), CAST(N'2023-10-01' AS Date), 3, 4, 1)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (11, CAST(N'2023-05-30' AS Date), CAST(N'2023-06-18' AS Date), 3, 2, 2)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (12, CAST(N'2023-02-09' AS Date), CAST(N'2023-02-11' AS Date), 1, 2, 2)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (13, CAST(N'2023-03-02' AS Date), CAST(N'2023-03-04' AS Date), 2, 2, 2)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (14, CAST(N'2023-08-15' AS Date), CAST(N'2023-08-17' AS Date), 1, 4, 2)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (15, CAST(N'2023-08-31' AS Date), CAST(N'2023-09-02' AS Date), 2, 4, 2)
INSERT [dbo].[FechasTutorias] ([IdFechaTutoria], [FechaTutoria], [FechaCierreDeReporte], [NumeroSesion], [IdPeriodo], [IdProgramaEducativo]) VALUES (16, CAST(N'2023-09-30' AS Date), CAST(N'2023-10-01' AS Date), 3, 4, 2)
SET IDENTITY_INSERT [dbo].[FechasTutorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Horarios] ON 

INSERT [dbo].[Horarios] ([IdHorarios], [Asistencia], [Riesgo], [HoraTutoria], [IdTutoriaAcademica], [Matricula]) VALUES (1, 0, 0, CAST(N'13:59:59' AS Time), 1, N'S19014012')
INSERT [dbo].[Horarios] ([IdHorarios], [Asistencia], [Riesgo], [HoraTutoria], [IdTutoriaAcademica], [Matricula]) VALUES (4, 0, 0, CAST(N'14:59:59' AS Time), 1, N'S20000000')
INSERT [dbo].[Horarios] ([IdHorarios], [Asistencia], [Riesgo], [HoraTutoria], [IdTutoriaAcademica], [Matricula]) VALUES (6, 0, 0, CAST(N'15:59:59' AS Time), 1, N'S20018989')
SET IDENTITY_INSERT [dbo].[Horarios] OFF
GO
SET IDENTITY_INSERT [dbo].[PeriodosEscolares] ON 

INSERT [dbo].[PeriodosEscolares] ([IdPeriodoEscolar], [Nombre], [FechaInicio], [FechaFin]) VALUES (2, N'FEBRERO 2023 - JULIO 2023', CAST(N'2023-02-01' AS Date), CAST(N'2023-07-31' AS Date))
INSERT [dbo].[PeriodosEscolares] ([IdPeriodoEscolar], [Nombre], [FechaInicio], [FechaFin]) VALUES (4, N'AGOSTO 2023 - ENERO 2024', CAST(N'2023-08-01' AS Date), CAST(N'2024-01-31' AS Date))
SET IDENTITY_INSERT [dbo].[PeriodosEscolares] OFF
GO
SET IDENTITY_INSERT [dbo].[Problematicas] ON 

INSERT [dbo].[Problematicas] ([IdProblematica], [Titulo], [Descripcion], [IdTutoriaAcademica], [NRC], [Matricula]) VALUES (1, N'PROFESOR SE AUSENTA EN CLASES', N'El profesor no acude a clases y cuando lo hace llega tarde sin dar explicaciones', 1, 77879, N'S19014012')
INSERT [dbo].[Problematicas] ([IdProblematica], [Titulo], [Descripcion], [IdTutoriaAcademica], [NRC], [Matricula]) VALUES (6, N'MAESTRO LLEGA ENOJADO', N'El maestro se enoja cuando le preguntan y se niega a responder', 1, 78990, N'S20018989')
SET IDENTITY_INSERT [dbo].[Problematicas] OFF
GO
SET IDENTITY_INSERT [dbo].[ProgramasEducativos] ON 

INSERT [dbo].[ProgramasEducativos] ([IdProgramaEducativo], [NombreProgramaEducativo]) VALUES (1, N'INGENIERIA DE SOFTWARE')
INSERT [dbo].[ProgramasEducativos] ([IdProgramaEducativo], [NombreProgramaEducativo]) VALUES (2, N'REDES Y SERVICIO DE COMPUTO')
INSERT [dbo].[ProgramasEducativos] ([IdProgramaEducativo], [NombreProgramaEducativo]) VALUES (3, N'GEOGRAFÍA')
INSERT [dbo].[ProgramasEducativos] ([IdProgramaEducativo], [NombreProgramaEducativo]) VALUES (4, N'TECNOLOGÍAS COMPUTACIONALES')
SET IDENTITY_INSERT [dbo].[ProgramasEducativos] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([IdRol], [NombreRol]) VALUES (1039, N'TutorAcademico                                    ')
INSERT [dbo].[Roles] ([IdRol], [NombreRol]) VALUES (1265, N'SinRol                                            ')
INSERT [dbo].[Roles] ([IdRol], [NombreRol]) VALUES (2938, N'Administrador                                     ')
INSERT [dbo].[Roles] ([IdRol], [NombreRol]) VALUES (3807, N'JefeDeCarrera                                     ')
INSERT [dbo].[Roles] ([IdRol], [NombreRol]) VALUES (5610, N'Coordinador                                       ')
INSERT [dbo].[Roles] ([IdRol], [NombreRol]) VALUES (8792, N'Profesor                                          ')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Soluciones] ON 

INSERT [dbo].[Soluciones] ([IdSolucion], [Fecha], [Descripcion], [IdProblematica], [IdAcademico]) VALUES (1, CAST(N'2023-06-01' AS Date), N'Se hablo con el profesor y se le dará de baja, pronto llegará un maestro sustituto.', 1, 1)
SET IDENTITY_INSERT [dbo].[Soluciones] OFF
GO
SET IDENTITY_INSERT [dbo].[TutoriasAcademicas] ON 

INSERT [dbo].[TutoriasAcademicas] ([IdTutoriaAcademica], [ComentarioGeneral], [ReporteEntregado], [IdAcademico], [IdFechaTuria]) VALUES (1, N'Sin comentarios', 0, 1, 1)
SET IDENTITY_INSERT [dbo].[TutoriasAcademicas] OFF
GO
ALTER TABLE [dbo].[AcademicosRoles]  WITH CHECK ADD  CONSTRAINT [FK_AcademicosRoles.Id_Academico] FOREIGN KEY([idAcademico])
REFERENCES [dbo].[Academicos] ([IdAcademico])
GO
ALTER TABLE [dbo].[AcademicosRoles] CHECK CONSTRAINT [FK_AcademicosRoles.Id_Academico]
GO
ALTER TABLE [dbo].[AcademicosRoles]  WITH CHECK ADD  CONSTRAINT [FK_AcademicosRoles.Id_Programa_Educativo] FOREIGN KEY([idProgramaEducativo])
REFERENCES [dbo].[ProgramasEducativos] ([IdProgramaEducativo])
GO
ALTER TABLE [dbo].[AcademicosRoles] CHECK CONSTRAINT [FK_AcademicosRoles.Id_Programa_Educativo]
GO
ALTER TABLE [dbo].[AcademicosRoles]  WITH CHECK ADD  CONSTRAINT [FK_AcademicosRoles.Id_Rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[Roles] ([IdRol])
GO
ALTER TABLE [dbo].[AcademicosRoles] CHECK CONSTRAINT [FK_AcademicosRoles.Id_Rol]
GO
ALTER TABLE [dbo].[AcademicosRoles]  WITH CHECK ADD  CONSTRAINT [FK_AcademicosRoles.Id_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[AcademicosUsuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[AcademicosRoles] CHECK CONSTRAINT [FK_AcademicosRoles.Id_Usuario]
GO
ALTER TABLE [dbo].[Estudiantes]  WITH CHECK ADD  CONSTRAINT [FK_Estudiantes.id_Programa_Educativo] FOREIGN KEY([IdProgramaEducativo])
REFERENCES [dbo].[ProgramasEducativos] ([IdProgramaEducativo])
GO
ALTER TABLE [dbo].[Estudiantes] CHECK CONSTRAINT [FK_Estudiantes.id_Programa_Educativo]
GO
ALTER TABLE [dbo].[Estudiantes]  WITH CHECK ADD  CONSTRAINT [FK_Estudiantes.idAcademico] FOREIGN KEY([IdAcademico])
REFERENCES [dbo].[Academicos] ([IdAcademico])
GO
ALTER TABLE [dbo].[Estudiantes] CHECK CONSTRAINT [FK_Estudiantes.idAcademico]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Academicos]  WITH CHECK ADD  CONSTRAINT [FK_ExperienciasEduacativas_Academicos.Id_Academico] FOREIGN KEY([IdAcademico])
REFERENCES [dbo].[Academicos] ([IdAcademico])
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Academicos] CHECK CONSTRAINT [FK_ExperienciasEduacativas_Academicos.Id_Academico]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Academicos]  WITH CHECK ADD  CONSTRAINT [FK_ExperienciasEduacativas_Academicos.Id_Experiencia_Educativa] FOREIGN KEY([IdExperienciaEducativa])
REFERENCES [dbo].[ExperienciasEducativas] ([IdExperienciaEducativa])
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Academicos] CHECK CONSTRAINT [FK_ExperienciasEduacativas_Academicos.Id_Experiencia_Educativa]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Academicos]  WITH CHECK ADD  CONSTRAINT [FK_ExperienciasEduacativas_Academicos.Id_Programa_Educativo] FOREIGN KEY([IdProgramaEducativo])
REFERENCES [dbo].[ProgramasEducativos] ([IdProgramaEducativo])
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Academicos] CHECK CONSTRAINT [FK_ExperienciasEduacativas_Academicos.Id_Programa_Educativo]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Estudiantes]  WITH CHECK ADD  CONSTRAINT [FK_ExperienciasEducativas_Estudiantes.matricula] FOREIGN KEY([Matricula])
REFERENCES [dbo].[Estudiantes] ([Matricula])
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Estudiantes] CHECK CONSTRAINT [FK_ExperienciasEducativas_Estudiantes.matricula]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Estudiantes]  WITH CHECK ADD  CONSTRAINT [FK_ExperienciasEducativas_Estudiantes.NRC] FOREIGN KEY([NRC])
REFERENCES [dbo].[ExperienciasEducativas_Academicos] ([NRC])
GO
ALTER TABLE [dbo].[ExperienciasEducativas_Estudiantes] CHECK CONSTRAINT [FK_ExperienciasEducativas_Estudiantes.NRC]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_PeriodosEscolares]  WITH CHECK ADD  CONSTRAINT [FK_ExperienciasEducativas_PeriodosEscolares.Id_Periodo] FOREIGN KEY([IdPeriodo])
REFERENCES [dbo].[PeriodosEscolares] ([IdPeriodoEscolar])
GO
ALTER TABLE [dbo].[ExperienciasEducativas_PeriodosEscolares] CHECK CONSTRAINT [FK_ExperienciasEducativas_PeriodosEscolares.Id_Periodo]
GO
ALTER TABLE [dbo].[ExperienciasEducativas_PeriodosEscolares]  WITH CHECK ADD  CONSTRAINT [FK_ExperienciasEducativas_PeriodosEscolares.NRC] FOREIGN KEY([NRC])
REFERENCES [dbo].[ExperienciasEducativas_Academicos] ([NRC])
GO
ALTER TABLE [dbo].[ExperienciasEducativas_PeriodosEscolares] CHECK CONSTRAINT [FK_ExperienciasEducativas_PeriodosEscolares.NRC]
GO
ALTER TABLE [dbo].[FechasTutorias]  WITH CHECK ADD  CONSTRAINT [FK_FechasTutorias.	Id_Periodo] FOREIGN KEY([IdPeriodo])
REFERENCES [dbo].[PeriodosEscolares] ([IdPeriodoEscolar])
GO
ALTER TABLE [dbo].[FechasTutorias] CHECK CONSTRAINT [FK_FechasTutorias.	Id_Periodo]
GO
ALTER TABLE [dbo].[FechasTutorias]  WITH CHECK ADD  CONSTRAINT [FK_FechasTutorias_ProgramasEducativos] FOREIGN KEY([IdProgramaEducativo])
REFERENCES [dbo].[ProgramasEducativos] ([IdProgramaEducativo])
GO
ALTER TABLE [dbo].[FechasTutorias] CHECK CONSTRAINT [FK_FechasTutorias_ProgramasEducativos]
GO
ALTER TABLE [dbo].[Horarios]  WITH CHECK ADD  CONSTRAINT [FK_Horarios.Id_Tutoria_academica] FOREIGN KEY([IdTutoriaAcademica])
REFERENCES [dbo].[TutoriasAcademicas] ([IdTutoriaAcademica])
GO
ALTER TABLE [dbo].[Horarios] CHECK CONSTRAINT [FK_Horarios.Id_Tutoria_academica]
GO
ALTER TABLE [dbo].[Horarios]  WITH CHECK ADD  CONSTRAINT [FK_Horarios.matricula] FOREIGN KEY([Matricula])
REFERENCES [dbo].[Estudiantes] ([Matricula])
GO
ALTER TABLE [dbo].[Horarios] CHECK CONSTRAINT [FK_Horarios.matricula]
GO
ALTER TABLE [dbo].[Problematicas]  WITH CHECK ADD  CONSTRAINT [FK_Problematicas.Id_Tutoria_Academica] FOREIGN KEY([IdTutoriaAcademica])
REFERENCES [dbo].[TutoriasAcademicas] ([IdTutoriaAcademica])
GO
ALTER TABLE [dbo].[Problematicas] CHECK CONSTRAINT [FK_Problematicas.Id_Tutoria_Academica]
GO
ALTER TABLE [dbo].[Problematicas]  WITH CHECK ADD  CONSTRAINT [FK_Problematicas.matricula] FOREIGN KEY([Matricula])
REFERENCES [dbo].[Estudiantes] ([Matricula])
GO
ALTER TABLE [dbo].[Problematicas] CHECK CONSTRAINT [FK_Problematicas.matricula]
GO
ALTER TABLE [dbo].[Problematicas]  WITH CHECK ADD  CONSTRAINT [FK_Problematicas.NRC] FOREIGN KEY([NRC])
REFERENCES [dbo].[ExperienciasEducativas_Academicos] ([NRC])
GO
ALTER TABLE [dbo].[Problematicas] CHECK CONSTRAINT [FK_Problematicas.NRC]
GO
ALTER TABLE [dbo].[Soluciones]  WITH CHECK ADD  CONSTRAINT [FK_Soluciones.Id_Academico] FOREIGN KEY([IdAcademico])
REFERENCES [dbo].[Academicos] ([IdAcademico])
GO
ALTER TABLE [dbo].[Soluciones] CHECK CONSTRAINT [FK_Soluciones.Id_Academico]
GO
ALTER TABLE [dbo].[Soluciones]  WITH CHECK ADD  CONSTRAINT [FK_Soluciones.IdProblematica] FOREIGN KEY([IdProblematica])
REFERENCES [dbo].[Problematicas] ([IdProblematica])
GO
ALTER TABLE [dbo].[Soluciones] CHECK CONSTRAINT [FK_Soluciones.IdProblematica]
GO
ALTER TABLE [dbo].[TutoriasAcademicas]  WITH CHECK ADD  CONSTRAINT [FK_TutoriasAcademicas.Id_FechaTuria] FOREIGN KEY([IdFechaTuria])
REFERENCES [dbo].[FechasTutorias] ([IdFechaTutoria])
GO
ALTER TABLE [dbo].[TutoriasAcademicas] CHECK CONSTRAINT [FK_TutoriasAcademicas.Id_FechaTuria]
GO
ALTER TABLE [dbo].[TutoriasAcademicas]  WITH CHECK ADD  CONSTRAINT [FK_TutoriasAcademicas_Academicos] FOREIGN KEY([IdAcademico])
REFERENCES [dbo].[Academicos] ([IdAcademico])
GO
ALTER TABLE [dbo].[TutoriasAcademicas] CHECK CONSTRAINT [FK_TutoriasAcademicas_Academicos]
GO
USE [master]
GO
ALTER DATABASE [Tutorias] SET  READ_WRITE 
GO
