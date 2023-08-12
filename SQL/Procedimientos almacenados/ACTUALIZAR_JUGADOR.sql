USE [Gimnacio]
GO
/****** Object:  StoredProcedure [dbo].[ACTUALIZAR_JUGADOR]    Script Date: 8/2/2019 17:30:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ACTUALIZAR_JUGADOR]
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
    @FOTO IMAGE NULL,
    @FECHA_DE_INGRESO DATETIME,
    @FECHA_DE_INSERCION DATETIME,
    @FECHA_DE_MODIFICACION DATETIME,
    @USUARIO_MODIFICACION INT
AS
BEGIN
    UPDATE Jugadores SET
 Correo=@CORREO,Cedula=@CEDULA,Nombre=@NOMBRE,PrimerApellido=@PRIMER_APELLIDO
,SegundoApellido=@SEGUNDO_APELLIDO,Fecha_Debut=@FECHA_DEBUT,Provincia=@PROVINCIA,Canton=@CANTON,Distrito=@DISTRITO,Foto=@FOTO,Fecha_de_ingreso=@FECHA_DE_INGRESO
,Fecha_de_insercion=@FECHA_DE_INSERCION,Fecha_de_modificacion=@FECHA_DE_MODIFICACION,Usuario_modificacion=@USUARIO_MODIFICACION
where id_Jugador=@ID_JUGADOR

END

CREATE PROCEDURE dbo.ACTUALIZAR_JUGADOR
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
    UPDATE Jugadores SET
 Correo=@CORREO,Cedula=@CEDULA,Nombre=@NOMBRE,PrimerApellido=@PRIMER_APELLIDO
,SegundoApellido=@SEGUNDO_APELLIDO,Fecha_Debut=@FECHA_DEBUT,Provincia=@PROVINCIA,Canton=@CANTON,Distrito=@DISTRITO,Foto=@FOTO,Fecha_de_ingreso=@FECHA_DE_INGRESO
,Fecha_de_insercion=@FECHA_DE_INSERCION,Fecha_de_modificacion=@FECHA_DE_MODIFICACION,Usuario_modificacion=@USUARIO_MODIFICACION
where id_Jugador=@ID_JUGADOR

END
