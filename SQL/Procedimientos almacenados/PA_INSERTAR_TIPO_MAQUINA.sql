
CREATE PROCEDURE PA_INSERTAR_TIPO_MAQUINA

    @Nombre_Maquina VARCHAR(255),
    @Estado VARCHAR(255)

AS
BEGIN

    INSERT INTO TIPO_MAQUINA
    VALUES(@Nombre_Maquina, @Estado)

END
GO

CREATE PROCEDURE [dbo].[PA_INSERTAR_TIPO_MAQUINA]

    @Nombre_Maquina VARCHAR(255),
    @Estado VARCHAR(255)

AS
BEGIN

    INSERT INTO TIPO_MAQUINA
    VALUES(@Nombre_Maquina, @Estado)

END

CREATE PROCEDURE dbo.PA_INSERTAR_TIPO_MAQUINA

    @Nombre_Maquina VARCHAR(255),
    @Estado VARCHAR(255)

AS
BEGIN

    INSERT INTO TIPO_MAQUINA
    VALUES(@Nombre_Maquina, @Estado)

END

CREATE PROCEDURE PA_TIPO_MAQUINA

@id_T�po_Maquina int,
@Nombre_Maquina VARCHAR
(255),
@Estado VARCHAR
(255)

AS
BEGIN

    INSERT INTO TIPO_MAQUINA
    VALUES(@id_Tipo_Maquina, @Nombre_Maquina, @Estado)

END
