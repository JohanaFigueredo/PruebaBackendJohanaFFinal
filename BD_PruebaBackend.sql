USE [master]
GO
/****** Object:  Database [PruebaBackend]    Script Date: 26/9/2021 4:33:47 p. m. ******/
CREATE DATABASE [PruebaBackend]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PruebaBackend', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\PruebaBackend.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PruebaBackend_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\PruebaBackend_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PruebaBackend] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PruebaBackend].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PruebaBackend] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PruebaBackend] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PruebaBackend] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PruebaBackend] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PruebaBackend] SET ARITHABORT OFF 
GO
ALTER DATABASE [PruebaBackend] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PruebaBackend] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PruebaBackend] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PruebaBackend] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PruebaBackend] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PruebaBackend] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PruebaBackend] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PruebaBackend] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PruebaBackend] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PruebaBackend] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PruebaBackend] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PruebaBackend] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PruebaBackend] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PruebaBackend] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PruebaBackend] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PruebaBackend] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PruebaBackend] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PruebaBackend] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PruebaBackend] SET  MULTI_USER 
GO
ALTER DATABASE [PruebaBackend] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PruebaBackend] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PruebaBackend] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PruebaBackend] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PruebaBackend] SET DELAYED_DURABILITY = DISABLED 
GO
USE [PruebaBackend]
GO
/****** Object:  Table [dbo].[tb_trnEventos]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_trnEventos](
	[IdTipoEvento] [nvarchar](10) NULL,
	[IdFuncion] [nvarchar](10) NULL,
	[DescripcionEvento] [nvarchar](max) NULL,
	[FechaCarga] [datetime] NULL CONSTRAINT [tb_trnEvento_FechaCarga]  DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_trnOrganizacion]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_trnOrganizacion](
	[IDUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [nvarchar](50) NULL,
	[Apellidos] [nvarchar](50) NULL,
	[Direccion] [nvarchar](max) NOT NULL,
	[FechaCarga] [datetime] NULL CONSTRAINT [tb_trnOrganizacion_FechaCarga]  DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_trnUsuario]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_trnUsuario](
	[IDUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [nvarchar](50) NULL,
	[Apellidos] [nvarchar](50) NULL,
	[CI] [nvarchar](50) NULL,
	[FechaNacimiento] [nchar](10) NULL,
	[Direccion] [nvarchar](max) NULL,
	[Correo] [nvarchar](max) NULL,
	[Telefono] [nvarchar](50) NULL,
	[OrganizacionLaboral] [nvarchar](max) NULL,
	[FechaCarga] [datetime] NULL CONSTRAINT [tb_trnUsuario_FechaCarga]  DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarRegistro]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ActualizarRegistro]
(
	@Nombres AS VARCHAR(50),
	@Apellidos AS VARCHAR(50),
	@Cedula AS VARCHAR(50),
	@FechaNac AS VARCHAR(50),
	@Direccion AS VARCHAR(500),
	@correo AS VARCHAR(500),
	@Telefono AS VARCHAR(50),
	@Organizacion AS VARCHAR(500)
)
AS
	SET NOCOUNT ON;
	BEGIN TRY

	--guardo en el log de eventos
	execute sp_RegistrarEvento 'I' ,12, 'Inicia el proceso actualización de registro en la base de datos'

	BEGIN TRANSACTION ActualizarRegistro
	
	UPDATE tb_trnUsuario SET 
			Nombres = @Nombres ,
			Apellidos = @Apellidos,
			CI = @Cedula,
			FechaNacimiento =@FechaNac ,
			Direccion = @Direccion,
			Correo = @correo,
			Telefono = @Telefono,
			OrganizacionLaboral = @Organizacion
	WHERE CI = @Cedula
		
	execute sp_RegistrarEvento 'I' ,12, 'Termina el proceso actualización de registro en la base de datos'

	COMMIT TRANSACTION ActualizarRegistro
	RETURN

END TRY  
BEGIN CATCH  
	ROLLBACK TRANSACTION ActualizarRegistro 
	execute sp_RegistrarEvento 'E' ,12, 'Error el proceso actualización de registro en la base de datos'
	RETURN -9
END CATCH 
	SET NOCOUNT OFF

	
GO
/****** Object:  StoredProcedure [dbo].[sp_ConsultarData]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ConsultarData]
    @DatoConsulta AS VARCHAR(500) 
AS
	SET NOCOUNT ON;
	BEGIN TRY

	--guardo en el log de eventos
	execute sp_RegistrarEvento 'I' ,9, 'Inicia el proceso de consulta de registro en la base de datos'

	BEGIN TRANSACTION BuscarRegistro
	
		SELECT Nombres,Apellidos,Ci, FechaNacimiento,Direccion,Correo,Telefono,OrganizacionLaboral 
		FROM tb_trnUsuario WHERE ci= @DatoConsulta

		SELECT Nombres,Apellidos,Ci, FechaNacimiento,Direccion,Correo,Telefono,OrganizacionLaboral 
		FROM tb_trnUsuario WHERE Correo= @DatoConsulta

		SELECT Nombres,Apellidos,Ci, FechaNacimiento,Direccion,Correo,Telefono,OrganizacionLaboral 
		FROM tb_trnUsuario WHERE Telefono= @DatoConsulta

	execute sp_RegistrarEvento 'I' ,9, 'Termina el proceso de consulta de registro en la base de datos'

	COMMIT TRANSACTION BuscarRegistro
	RETURN

END TRY  
BEGIN CATCH  
	ROLLBACK TRANSACTION BuscarRegistro 
	execute sp_RegistrarEvento 'E' ,9, 'Error el proceso de consulta de registro en la base de datos'
	RETURN -9
END CATCH 
	SET NOCOUNT OFF
return 


GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarRegistroBD]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EliminarRegistroBD]
(
	@DatoConsulta AS VARCHAR(500)
)
AS
	SET NOCOUNT ON;
	BEGIN TRY

	--guardo en el log de eventos
	execute sp_RegistrarEvento 'I' ,11, 'Inicia el proceso de eliminar registro la base de datos'

	BEGIN TRANSACTION EliminarReg
	
	DELETE tb_trnUsuario WHERE Ci = @DatoConsulta
    DELETE tb_trnUsuario WHERE Correo = @DatoConsulta
	DELETE tb_trnUsuario WHERE Telefono = @DatoConsulta
		
	execute sp_RegistrarEvento 'I' ,11, 'Termina el proceso de eliminar registro la base de datos'

	COMMIT TRANSACTION EliminarReg
	RETURN

END TRY  
BEGIN CATCH  
	ROLLBACK TRANSACTION EliminarReg 
	execute sp_RegistrarEvento 'E' ,11, 'Error el proceso de eliminar registro la base de datos'
	RETURN -9
END CATCH 
	SET NOCOUNT OFF
	return
	
GO
/****** Object:  StoredProcedure [dbo].[sp_GuardarBD]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GuardarBD]
(
	@Nombres AS VARCHAR(50),
	@Apellidos AS VARCHAR(50),
	@Cedula AS VARCHAR(50),
	@FechaNac AS VARCHAR(50),
	@Direccion AS VARCHAR(500),
	@correo AS VARCHAR(500),
	@Telefono AS VARCHAR(50),
	@Organizacion AS VARCHAR(500)
)
AS
	SET NOCOUNT ON;
	BEGIN TRY

	--guardo en el log de eventos
	execute sp_RegistrarEvento 'I' ,5, 'Inicia el proceso de guardar en la base de datos'

	BEGIN TRANSACTION InsertarBD
	
	INSERT INTO [dbo].[tb_trnUsuario]
           (Nombres,
           Apellidos,
           CI,
           FechaNacimiento,
           Direccion,
           Correo,
           Telefono,
           OrganizacionLaboral)
     VALUES
           (@Nombres,
           @Apellidos,
           @Cedula,
           @FechaNac,
           @Direccion,
           @correo,
           @Telefono,
           @Organizacion)
		
	execute sp_RegistrarEvento 'I' ,5, 'Termina el proceso de guardar en la base de datos' 

	COMMIT TRANSACTION InsertarBD
	RETURN

END TRY  
BEGIN CATCH  
	ROLLBACK TRANSACTION InsertarBD 
	execute sp_RegistrarEvento 'E' ,5, 'Error el proceso de guardar en la base de datos' 
	RETURN -9
END CATCH 
	SET NOCOUNT OFF

	
GO
/****** Object:  StoredProcedure [dbo].[sp_GuardarOrganizacion]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GuardarOrganizacion]
(
	@Nombres AS VARCHAR(50),
	@Apellidos AS VARCHAR(50),
	@Direccion AS VARCHAR(500)
)
AS
	SET NOCOUNT ON;
	BEGIN TRY

	--guardo en el log de eventos
	execute sp_RegistrarEvento 'I' ,7, 'Inicia el proceso de guardar en la base de datos tabla organizacion'

	BEGIN TRANSACTION InsertarB
		INSERT INTO [dbo].[tb_trnOrganizacion]
           (Nombres,
		   Apellidos,
           Direccion)
		VALUES
           (@Nombres,
		   @Apellidos,
			@Direccion)

	execute sp_RegistrarEvento 'I' ,7, 'Termina el proceso de guardar en la base de datos tabla organizacion' 

	COMMIT TRANSACTION InsertarBD
	RETURN
END TRY  
BEGIN CATCH  
	ROLLBACK TRANSACTION InsertarBD 
	execute sp_RegistrarEvento 'E' ,7, 'Error el proceso de guardar en la base de datos tabla organizacion' 
	RETURN -9
END CATCH 
	SET NOCOUNT OFF

	
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarEvento]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarEvento]
(
	@idTipoEvento varchar(10),
	@idFuncion int = NULL,
	@DescripcionEvento varchar(4000)
)
AS 
	DECLARE @Result int

	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION GuardoEventos

			INSERT INTO tb_trnEventos
								 ( idTipoEvento,IdFuncion, DescripcionEvento)
			VALUES        (@idTipoEvento,@IdFuncion,UPPER(@DescripcionEvento));

		COMMIT TRANSACTION GuardoEventos
	END TRY  

BEGIN CATCH  
	ROLLBACK TRANSACTION GuardoEventos 
	RETURN -9
END CATCH 
SET NOCOUNT OFF
RETURN isnull(@Result,-1)	


GO
/****** Object:  StoredProcedure [dbo].[sp_VerDuplicidadCedula]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_VerDuplicidadCedula]
    @Cedula AS VARCHAR(50) 
AS
	SET NOCOUNT ON;
	BEGIN TRY

	--guardo en el log de eventos
	execute sp_RegistrarEvento 'I' ,6, 'Inicia el proceso de verificar duplicidad de cedula en la base de datos'

	BEGIN TRANSACTION VerificarCI
	
    SELECT CI FROM tb_trnUsuario WHERE ci= @Cedula

	execute sp_RegistrarEvento 'I' ,6, 'Termina el proceso de verificar duplicidad de cedula en la base de datos'

	COMMIT TRANSACTION VerificarCI
	RETURN

END TRY  
BEGIN CATCH  
	ROLLBACK TRANSACTION VerificarCI 
	execute sp_RegistrarEvento 'E' ,6, 'Error el proceso de verificar duplicidad de cedula en la base de datos'
	RETURN -9
END CATCH 
	SET NOCOUNT OFF
return 


GO
/****** Object:  StoredProcedure [dbo].[sp_VerDuplicidadCorreo]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_VerDuplicidadCorreo]
    @Correo AS VARCHAR(500) 
AS
	SET NOCOUNT ON;
	BEGIN TRY

	--guardo en el log de eventos
	execute sp_RegistrarEvento 'I' ,6, 'Inicia el proceso de validacion de duplicidad de correo en la base de datos'

	BEGIN TRANSACTION ValidarCorreo
	
    SELECT Correo FROM tb_trnUsuario WHERE Correo= @Correo

	execute sp_RegistrarEvento 'I' ,6, 'Termina el proceso de validacion de duplicidad de correo en la base de datos'

	COMMIT TRANSACTION ValidarCorreo
	RETURN

END TRY  
BEGIN CATCH  
	ROLLBACK TRANSACTION ValidarCorreo 
	execute sp_RegistrarEvento 'E' ,6, 'Error el proceso de validacion de duplicidad de correo en la base de datos'
	RETURN -9
END CATCH 
	SET NOCOUNT OFF


GO
/****** Object:  StoredProcedure [dbo].[sp_VerDuplicidadTelefono]    Script Date: 26/9/2021 4:33:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_VerDuplicidadTelefono]
    @Telefono AS VARCHAR(500) 
AS
	SET NOCOUNT ON;
	BEGIN TRY

	--guardo en el log de eventos
	execute sp_RegistrarEvento 'I' ,6, 'Inicia el proceso de validacion de duplicidad del telefono en la base de datos'

	BEGIN TRANSACTION ValidacionTelefono

    SELECT Telefono FROM tb_trnUsuario WHERE Telefono= @Telefono

	execute sp_RegistrarEvento 'I' ,6, 'Termina proceso de validacion de duplicidad del telefono en la base de datos'

	COMMIT TRANSACTION ValidacionTelefono
	RETURN

END TRY  
BEGIN CATCH  
	ROLLBACK TRANSACTION ValidacionTelefono 
	execute sp_RegistrarEvento 'E' ,6, 'Error el proceso de validacion de duplicidad del telefono en la base de datos'
	RETURN -9
END CATCH 
	SET NOCOUNT OFF


GO
USE [master]
GO
ALTER DATABASE [PruebaBackend] SET  READ_WRITE 
GO
