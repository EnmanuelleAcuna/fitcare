
CREATE PROCEDURE [dbo].[PA_ACTUALIZAR_TIPO_EJERCICIO]
	@ID_TIPO_EJERCICIO INT,
	@NOMBRE_EJERCICIO VARCHAR(255),
	@ESTADO VARCHAR(255)

AS
BEGIN
	UPDATE  Tipo_Ejercicio SET NOMBRE_EJERCICIO=@NOMBRE_EJERCICIO,ESTADO=@ESTADO WHERE ID_TIPO_EJERCICIO=@ID_TIPO_EJERCICIO
END

CREATE PROCEDURE [dbo].[PA_ACTUALIZAR_TIPO_EJERCICIO]
	@ID_TIPO_EJERCICIO INT,
	@NOMBRE_EJERCICIO VARCHAR(255),
	@ESTADO VARCHAR(255)

AS
BEGIN
	UPDATE  Tipo_Ejercicio SET NOMBRE_EJERCICIO=@NOMBRE_EJERCICIO,ESTADO=@ESTADO WHERE ID_TIPO_EJERCICIO=@ID_TIPO_EJERCICIO
END

CREATE PROCEDURE dbo.PA_ACTUALIZAR_TIPO_EJERCICIO
	@ID_TIPO_EJERCICIO INT,
	@NOMBRE_EJERCICIO VARCHAR(255),
	@ESTADO VARCHAR(255)

AS
BEGIN
	UPDATE  Tipo_Ejercicio SET NOMBRE_EJERCICIO=@NOMBRE_EJERCICIO,ESTADO=@ESTADO WHERE ID_TIPO_EJERCICIO=@ID_TIPO_EJERCICIO
END
