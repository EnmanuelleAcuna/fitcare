
CREATE PROCEDURE PA_ACTUALIZAR_MAQUINA

    @ID_TIPO_MAQUINA INT,
    @NOMBRE_MAQUINA VARCHAR(255),
    @ESTADO VARCHAR(255)

AS
BEGIN

    UPDATE TIPO_MAQUINA SET NOMBRE_MAQUINA=@NOMBRE_MAQUINA,ESTADO=@ESTADO WHERE ID_TIPO_MAQUINA=@ID_TIPO_MAQUINA

END

CREATE PROCEDURE [dbo].[PA_ACTUALIZAR_MAQUINA]

    @ID_TIPO_MAQUINA INT,
    @NOMBRE_MAQUINA VARCHAR(255),
    @ESTADO VARCHAR(255)

AS
BEGIN

    UPDATE TIPO_MAQUINA SET NOMBRE_MAQUINA=@NOMBRE_MAQUINA,ESTADO=@ESTADO WHERE ID_TIPO_MAQUINA=@ID_TIPO_MAQUINA

END

CREATE PROCEDURE dbo.PA_ACTUALIZAR_MAQUINA

    @ID_TIPO_MAQUINA INT,
    @NOMBRE_MAQUINA VARCHAR(255),
    @ESTADO VARCHAR(255)

AS
BEGIN

    UPDATE TIPO_MAQUINA SET NOMBRE_MAQUINA=@NOMBRE_MAQUINA,ESTADO=@ESTADO WHERE ID_TIPO_MAQUINA=@ID_TIPO_MAQUINA

END
