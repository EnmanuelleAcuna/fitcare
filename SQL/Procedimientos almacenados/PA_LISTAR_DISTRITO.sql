USE [GimnacioBD]
GO
/****** Object:  StoredProcedure [dbo].[PA_LISTAR_DISTRITO]    Script Date: 21/9/2019 13:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[PA_LISTAR_DISTRITO]
AS
BEGIN
	SELECT C.id_Canton
	 , C.nombre
	  AS NOMBRE_CANTON,
		D.id_Distrito,
		D.nombre AS NOMBRE_DISTRITO,
		D.usuario_crea,
		D.fecha_Crea,
		D.Usuario_Modifica,
		D.vc_Estado,
		D.id_DistritoInec,
		D.fecha_Modifica
	FROM Distrito AS D
		INNER JOIN CANTON AS C ON C.id_Canton=C.id_Canton

END

  CREATE PROCEDURE [dbo].[PA_LISTAR_DISTRITO]
AS
BEGIN
	SELECT C.id_Canton
	 , C.nombre
	  AS NOMBRE_CANTON,
		D.id_Distrito,
		D.nombre AS NOMBRE_DISTRITO,
		D.usuario_crea,
		D.fecha_Crea,
		D.Usuario_Modifica,
		D.vc_Estado,
		D.id_DistritoInec,
		D.fecha_Modifica
	FROM Distrito AS D
		INNER JOIN CANTON AS C ON C.id_Canton=C.id_Canton

END

create procedure PA_LISTAR_DISTRITO

	@ID_DISTRITO INT
AS
BEGIN
	SELECT *
	FROM Distrito
	WHERE ID_DISTRITO=@ID_DISTRITO
END
