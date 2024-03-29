USE [GimnacioBD]
GO
/****** Object:  StoredProcedure [dbo].[PA_LISTAR_CANTON]    Script Date: 16/8/2019 23:22:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[PA_LISTAR_CANTON]
AS
BEGIN
     SELECT P.id_Provincia, P.nombre AS NOMBRE_PROVICIA, C.id_Canton, C.nombre AS NOMBRE_CANTON, C.usuario_crea AS Usuario_Crea, C.fecha_creacion AS Fecha_Creacion, C.usuario_modifcacion AS Usuario_Modificacion, C.fecha_modificacion AS Fecha_Modificacion, C.vc_Estado, C.id_CantonInec AS CantonInec
     FROM CANTON AS C
          INNER JOIN PROVINCIA AS P ON C.id_Provincia=P.id_Provincia
END

CREATE PROCEDURE [dbo].[PA_LISTAR_CANTON]
AS
BEGIN
     SELECT P.id_Provincia, P.nombre AS NOMBRE_PROVICIA, C.id_Canton, C.nombre AS NOMBRE_CANTON, C.usuario_crea AS Usuario_Crea, C.fecha_creacion AS Fecha_Creacion, C.usuario_modifcacion AS Usuario_Modificacion, C.fecha_modificacion AS Fecha_Modificacion, C.vc_Estado, C.id_CantonInec AS CantonInec
     FROM CANTON AS C
          INNER JOIN PROVINCIA AS P ON C.id_Provincia=P.id_Provincia
END

create procedure PA_LISTAR_CANTON

     @ID_CANTON INT
AS
BEGIN
     SELECT *
     FROM CANTON
     WHERE id_Canton=@ID_CANTON
END
