
Create procedure [dbo].[PA_ELIMINAR_RUTINA]
	@ID_RUTINA INT
AS
BEGIN
	DELETE NuevaRutina WHERE id_Rutina=@ID_RUTINA
END

USE [Gimnacio]
GO
/****** Object:  StoredProcedure [dbo].[ELIMINAR_INSTRUCTOR]    Script Date: 8/2/2019 17:33:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ELIMINAR_RUTINA]
	@ID_RUTINA INT
AS
BEGIN
	DELETE Rutinas WHERE id_Rutina=@ID_RUTINA
END

Create procedure [dbo].[PA_ELIMINAR_RUTINA]
	@ID_RUTINA INT
AS
BEGIN
	DELETE NuevaRutina WHERE id_Rutina=@ID_RUTINA
END

Create procedure dbo.PA_ELIMINAR_RUTINA
	@ID_RUTINA INT
AS
BEGIN
	DELETE NuevaRutina WHERE id_Rutina=@ID_RUTINA
END

CREATE PROCEDURE dbo.ELIMINAR_RUTINA
	@ID_RUTINA INT
AS
BEGIN
	DELETE Rutinas WHERE id_Rutina=@ID_RUTINA
END
