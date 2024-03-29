USE [GimnacioBD]
GO
/****** Object:  StoredProcedure [dbo].[PA_INSERTAR_DISTRITO]    Script Date: 17/8/2019 00:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PA_INSERTAR_DISTRITO]
    @id_Canton INT,
    @Nombre VARCHAR(100),
    @Usuario_Crea VARCHAR(50),
    @Fecha_Crea DATETIME,
    @Usuario_Modifica VARCHAR(50),
    @Fecha_Modifica DATETIME,
    @vc_Estado VARCHAR(3),
    @id_DistritoInec INT

AS
BEGIN

    INSERT INTO Distrito
    VALUES(@id_Canton, @Nombre, @Usuario_Crea, @Fecha_Crea, @Usuario_Modifica,
            @Fecha_Modifica, @vc_Estado, @id_DistritoInec)

END

CREATE PROCEDURE [dbo].[PA_INSERTAR_DISTRITO]
    @id_Canton INT,
    @Nombre VARCHAR(100),
    @Usuario_Crea VARCHAR(50),
    @Fecha_Crea DATETIME,
    @Usuario_Modifica VARCHAR(50),
    @Fecha_Modifica DATETIME,
    @vc_Estado VARCHAR(3),
    @id_DistritoInec INT

AS
BEGIN

    INSERT INTO Distrito
    VALUES(@ID_CANTON, @NOMBRE, @USUARIO_CREA, @FECHA_CREA, @USUARIO_MODIFICA,
            @FECHA_MODIFICA, @VC_ESTADO, @ID_DISTRITOINEC)

END

CREATE PROCEDURE dbo.PA_INSERTAR_DISTRITO
    @id_Canton INT,
    @Nombre VARCHAR(100),
    @Usuario_Crea VARCHAR(50),
    @Fecha_Crea DATETIME,
    @Usuario_Modifica VARCHAR(50),
    @Fecha_Modifica DATETIME,
    @vc_Estado VARCHAR(3),
    @id_DistritoInec INT

AS
BEGIN

    INSERT INTO Distrito
    VALUES(@ID_CANTON, @NOMBRE, @USUARIO_CREA, @FECHA_CREA, @USUARIO_MODIFICA,
            @FECHA_MODIFICA, @VC_ESTADO, @ID_DISTRITOINEC)

END

CREATE PROCEDURE PA_INSERTAR_DISTRITO
    @id_Canton INT,
    @Nombre VARCHAR(100),
    @Usuario_Crea VARCHAR(50),
    @Fecha_Crea DATETIME,
    @Usuario_Modifica VARCHAR(50),
    @Fecha_Modifica DATETIME,
    @vc_Estado VARCHAR(3),
    @id_DistritoInec INT

AS
BEGIN

    INSERT INTO Distrito
    VALUES(@ID_CANTON, @NOMBRE, @USUARIO_CREA, @FECHA_CREA, @USUARIO_MODIFICA,
            @FECHA_MODIFICA, @VC_ESTADO, @ID_DISTRITOINEC)

END
