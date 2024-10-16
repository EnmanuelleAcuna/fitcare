USE fitcare;

DROP PROCEDURE OBTENER_REPORTE_RUTINAS_RESUMIDO;
GO;

CREATE PROCEDURE OBTENER_REPORTE_RUTINAS_RESUMIDO
    @idInstructor NVARCHAR(100),
    @idCliente NVARCHAR(100)
AS
BEGIN
	SELECT
		R.Id,
        CONCAT(I.Nombre, ' ', I.PrimerApellido, ' ', I.SegundoApellido) AS 'NombreInstructor',  
        CONCAT(C.Nombre, ' ', C.PrimerApellido, ' ', C.SegundoApellido) AS 'NombreCliente',  
        R.Fecha_Realizacion AS 'FechaRegistro',
        R.Fecha_Inicio AS 'FechaInicio',
        R.Fecha_Fin AS 'FechaFin',
        R.Objetivo,
        DATEDIFF(DAY, R.Fecha_Fin, R.Fecha_Inicio) AS 'DiasRutina',
        COUNT(DR.IdRutina) AS 'CantidadEjerciciosRegistrados'
	FROM RUTINAS AS R
	INNER JOIN USUARIOS I
		ON I.Id = R.IdInstructor
	INNER JOIN USUARIOS C
		ON C.Id = R.IdCliente
    LEFT JOIN DETALLES_RUTINA DR
        ON DR.IdRutina = R.Id
    WHERE   R.IdInstructor = CASE idInstructor WHEN '' THEN R.IdInstructor ELSE idInstructor END
             AND
             R.IdCliente = CASE idCliente WHEN '' THEN R.IdCliente ELSE idCliente END
    GROUP BY R.Id, I.Nombre, I.PrimerApellido, I.SegundoApellido, C.Nombre, C.PrimerApellido, C.SegundoApellido, R.Fecha_Realizacion, R.Fecha_Inicio, R.Fecha_Fin, R.Objetivo;
END;

EXEC OBTENER_REPORTE_RUTINAS_RESUMIDO '', '';
