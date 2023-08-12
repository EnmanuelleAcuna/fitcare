USE [Gimnacio]
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_JUGADOR]    Script Date: 8/2/2019 17:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[INSERTA_JUGADOR]
    @ID_JUGADOR INT,
    @CORREO VARCHAR(150),
    @CEDULA VARCHAR(150),
    @NOMBRE VARCHAR(150),
    @PRIMER_APELLIDO VARCHAR(150),
    @SEGUNDO_APELLIDO VARCHAR(150),
    @FECHA_DEBUT DATETIME,
    @PROVINCIA VARCHAR(150),
    @CANTON VARCHAR(150),
    @DISTRITO VARCHAR(150),
    @FOTO IMAGE,
    @FECHA_DE_INGRESO DATETIME,
    @FECHA_DE_INSERCION DATETIME,
    @FECHA_DE_MODIFICACION DATETIME,
    @USUARIO_MODIFICACION INT
AS
BEGIN
    INSERT INTO Jugadores
    VALUES(@ID_JUGADOR, @CORREO, @NOMBRE, @PRIMER_APELLIDO, @SEGUNDO_APELLIDO, @FECHA_DEBUT, @PROVINCIA, @CANTON, @DISTRITO, @FOTO, @FECHA_DE_INGRESO, @FECHA_DE_INSERCION, @FECHA_DE_MODIFICACION, @USUARIO_MODIFICACION)
END

CREATE PROCEDURE dbo.INSERTA_JUGADOR
    @ID_JUGADOR INT NULL,
    @CORREO VARCHAR(150),
    @CEDULA VARCHAR(150),
    @NOMBRE VARCHAR(150),
    @PRIMER_APELLIDO VARCHAR(150),
    @SEGUNDO_APELLIDO VARCHAR(150),
    @FECHA_DEBUT DATETIME,
    @PROVINCIA VARCHAR(150),
    @CANTON VARCHAR(150),
    @DISTRITO VARCHAR(150),
    @FOTO IMAGE NULL,
    @FECHA_DE_INGRESO DATETIME,
    @FECHA_DE_INSERCION DATETIME,
    @FECHA_DE_MODIFICACION DATETIME,
    @USUARIO_MODIFICACION INT NULL
AS
BEGIN
    INSERT INTO Jugadores
    VALUES(@ID_JUGADOR, @CORREO, @NOMBRE, @PRIMER_APELLIDO, @SEGUNDO_APELLIDO, @FECHA_DEBUT, @PROVINCIA, @CANTON, @DISTRITO, @FOTO, @FECHA_DE_INGRESO, @FECHA_DE_INSERCION, @FECHA_DE_MODIFICACION, @USUARIO_MODIFICACION)
END
