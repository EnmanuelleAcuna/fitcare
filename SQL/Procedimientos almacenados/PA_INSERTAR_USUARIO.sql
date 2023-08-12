CREATE PROCEDURE PA_INSERTAR_USUARIO

@NOMBRE VARCHAR(255),
@APELLIDO1 VARCHAR(255),
@APELLIDO2 VARCHAR(255),
@NUMERODEIDENTIFICACION VARCHAR(255),
@FOTOGRAFIA IMAGE,
@CORREOELECTRONICO VARCHAR(255),
@CONTRASE�A VARCHAR
(255),
@FK_PROVINCIA INT ,
@FK_CANTON INT,
@FK_DISTRITO INT,
@FK_TIPO_USUARIO INT

AS
BEGIN

    INSERT INTO USUARIOS
    VALUES(@NOMBRE, @APELLIDO1, @APELLIDO2, @NUMERODEIDENTIFICACION, @FOTOGRAFIA,
            @CORREOELECTRONICO, @CONTRASE
    �A,@FK_PROVINCIA,@FK_CANTON,@FK_DISTRITO,@FK_TIPO_USUARIO)

END
GO

CREATE PROCEDURE dbo.PA_INSERTAR_USUARIO

    @NOMBRE VARCHAR(255),
    @APELLIDO1 VARCHAR(255),
    @APELLIDO2 VARCHAR(255),
    @NUMERODEIDENTIFICACION VARCHAR(255),
    @FOTOGRAFIA IMAGE,
    @CORREOELECTRONICO VARCHAR(255),
    @CONTRASEÑA VARCHAR(255),
    @FK_PROVINCIA INT ,
    @FK_CANTON INT,
    @FK_DISTRITO INT,
    @FK_TIPO_USUARIO INT

AS
BEGIN

    INSERT INTO USUARIOS
    VALUES(@NOMBRE, @APELLIDO1, @APELLIDO2, @NUMERODEIDENTIFICACION, @FOTOGRAFIA,
            @CORREOELECTRONICO, @CONTRASEÑA, @FK_PROVINCIA, @FK_CANTON, @FK_DISTRITO, @FK_TIPO_USUARIO)

END

CREATE PROCEDURE PA_INSERTAR_USUARIO

@NOMBRE VARCHAR(255),
@APELLIDO1 VARCHAR(255),
@APELLIDO2 VARCHAR(255),
@NUMERODEIDENTIFICACION VARCHAR(255),
@FOTOGRAFIA IMAGE,
@CORREOELECTRONICO VARCHAR(255),
@CONTRASE�A VARCHAR
(255),
@FK_PROVINCIA INT ,
@FK_CANTON INT,
@FK_DISTRITO INT,
@FK_TIPO_USUARIO INT

AS
BEGIN

    INSERT INTO USUARIOS
    VALUES(@NOMBRE, @APELLIDO1, @APELLIDO2, @NUMERODEIDENTIFICACION, @FOTOGRAFIA,
            @CORREOELECTRONICO, @CONTRASE
    �A,@FK_PROVINCIA,@FK_CANTON,@FK_DISTRITO,@FK_TIPO_USUARIO)

END
