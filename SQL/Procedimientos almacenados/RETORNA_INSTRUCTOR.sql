USE [Gimnacio]
GO
/****** Object:  StoredProcedure [dbo].[PA_RETORNA_CLIENTES]    Script Date: 26/2/2019 23:37:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PA_RETORNA_INSTRUCTOR]
@ID_INSTRUCTOR INT
AS
BEGIN 


SELECT   INS.ID_INSTRUCTOR,INS.CORREO,INS.CEDULA,INS.NOMBRE,INS.PRIMERAPELLIDO,INS.SEGUNDOAPELLIDO,INS.PROVINCIA,INS.CANTON,
INS.DISTRITO,INS.FOTO,INS.FECHA_DE_INGRESO,INS.FECHA_DE_INSERCION  
FROM Instructores  AS INS
WHERE ID_INSTRUCTOR=@ID_INSTRUCTOR

END 