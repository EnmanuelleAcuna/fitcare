USE [GimnacioBD]
GO
/****** Object:  StoredProcedure [dbo].[PA_INSERTAR_CANTON]    Script Date: 16/8/2019 23:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PA_INSERTAR_CANTON]


    @id_Provincia INT,
    @Nombre VARCHAR(100),
    @Usuario_Crea VARCHAR(50),
    @Fecha_Creacion DATETIME,
    @Usuario_Modificacion VARCHAR(50),
    @Fecha_Modificacion DATETIME,
    @vc_Estado VARCHAR(3),
    @id_CantonesInec INT

AS
BEGIN

    INSERT INTO CANTON
    VALUES
        (@id_Provincia, @Nombre, @Usuario_Crea, @Fecha_Creacion, @Usuario_Modificacion, @Fecha_Modificacion,
            @vc_Estado, @id_CantonesInec)

END

CREATE PROCEDURE [dbo].[PA_INSERTAR_CANTON]


    @id_Provincia INT,
    @Nombre VARCHAR(100),
    @Usuario_Crea VARCHAR(50),
    @Fecha_Creacion DATETIME,
    @Usuario_Modificacion VARCHAR(50),
    @Fecha_Modificacion DATETIME,
    @vc_Estado VARCHAR(3),
    @id_CantonesInec INT

AS
BEGIN

    INSERT INTO  CANTON
    VALUES
        (@id_Provincia, @Nombre, @Usuario_Crea, @Fecha_Creacion, @Usuario_Modificacion, @Fecha_Modificacion,
            @vc_Estado, @id_CantonesInec)

END

CREATE PROCEDURE dbo.PA_INSERTAR_CANTON


    @id_Provincia INT,
    @Nombre VARCHAR(100),
    @Usuario_Crea VARCHAR(50),
    @Fecha_Creacion DATETIME,
    @Usuario_Modificacion DATETIME,
    @Fecha_Modificacion DATETIME,
    @vc_Estado VARCHAR(3),
    @id_CantonesInec INT

AS
BEGIN

    INSERT INTO  CANTON
    VALUES
        (@id_Provincia, @Nombre, @Usuario_Crea, @Fecha_Creacion, @Usuario_Modificacion, @Fecha_Modificacion,
            @vc_Estado, @id_CantonesInec)

END

CREATE PROCEDURE PA_INSERTAR_CANTON

    @id_Canton INT,
    @id_Provincia INT,
    @Nombre VARCHAR(100),
    @Usuario_Crea VARCHAR(50),
    @Fecha_Creacion DATETIME,
    @Usuario_Modificacion DATETIME,
    @Fecha_Modificacion DATETIME,
    @vc_Estado VARCHAR(3),
    @id_CantonesInec INT

AS
BEGIN

    INSERT INTO  CANTON
    VALUES
        (@id_Canton, @id_Provincia, @Nombre, @Usuario_Crea, @Fecha_Creacion, @Usuario_Modificacion, @Fecha_Modificacion,
            @Fecha_Modificacion, @vc_Estado, @id_CantonesInec)

END
