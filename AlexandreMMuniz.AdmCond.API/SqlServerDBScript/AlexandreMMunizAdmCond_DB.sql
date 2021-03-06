USE [master]
GO
/****** Object:  Database [AlexandreMMunizAdmCond]    Script Date: 02/01/2020 20:42:50 ******/
CREATE DATABASE [AlexandreMMunizAdmCond]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AlexandreMMunizAdmCond', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AlexandreMMunizAdmCond.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AlexandreMMunizAdmCond_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AlexandreMMunizAdmCond_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AlexandreMMunizAdmCond].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET ARITHABORT OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET  MULTI_USER 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET QUERY_STORE = OFF
GO
USE [AlexandreMMunizAdmCond]
GO
/****** Object:  Table [dbo].[Administradoras]    Script Date: 02/01/2020 20:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administradoras](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Administradoras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Condominios]    Script Date: 02/01/2020 20:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Condominios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[IdAdministradora] [int] NOT NULL,
	[Responsavel] [tinyint] NOT NULL,
 CONSTRAINT [PK_Condominios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 02/01/2020 20:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[IdCondominio] [int] NOT NULL,
	[TipoUsuario] [tinyint] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Condominios]  WITH CHECK ADD  CONSTRAINT [FK_Condominios_Administradoras] FOREIGN KEY([IdAdministradora])
REFERENCES [dbo].[Administradoras] ([Id])
GO
ALTER TABLE [dbo].[Condominios] CHECK CONSTRAINT [FK_Condominios_Administradoras]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Condominios] FOREIGN KEY([IdCondominio])
REFERENCES [dbo].[Condominios] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Condominios]
GO
ALTER TABLE [dbo].[Condominios]  WITH CHECK ADD  CONSTRAINT [CK_Condominios] CHECK  (([Responsavel]=(2) OR [Responsavel]=(4)))
GO
ALTER TABLE [dbo].[Condominios] CHECK CONSTRAINT [CK_Condominios]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [CK_Usuarios] CHECK  (([TipoUsuario]>=(1) AND [TipoUsuario]<=(4)))
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [CK_Usuarios]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'2 = Síndico e 4 = Zelador' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Condominios', @level2type=N'CONSTRAINT',@level2name=N'CK_Condominios'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Morador; 2 = Síndico - Responsável; 3 = Administradora - Responsável e 4 = Zelador - Responsável.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Usuarios', @level2type=N'CONSTRAINT',@level2name=N'CK_Usuarios'
GO
USE [master]
GO
ALTER DATABASE [AlexandreMMunizAdmCond] SET  READ_WRITE 
GO
