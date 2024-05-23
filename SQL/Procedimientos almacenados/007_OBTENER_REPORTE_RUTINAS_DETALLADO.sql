USE fitcare;

DROP PROCEDURE OBTENER_REPORTE_RUTINAS_DETALLADO;

DELIMITER //

CREATE PROCEDURE OBTENER_REPORTE_RUTINAS_DETALLADO (
    IN idInstructor NVARCHAR(100),
    IN idCliente NVARCHAR(100)
)
BEGIN
	SELECT
        CONCAT(I.Nombre, ' ', I.PrimerApellido, ' ', I.SegundoApellido) AS 'NombreInstructor',  
        CONCAT(C.Nombre, ' ', C.PrimerApellido, ' ', C.SegundoApellido) AS 'NombreCliente',  
        R.Fecha_Realizacion AS 'FechaRegistro',
        R.Fecha_Inicio AS 'FechaInicio',
        R.Fecha_Fin AS 'FechaFin',
        R.Objetivo,
        DATEDIFF(R.Fecha_Inicio, R.Fecha_Fin) + 1 AS 'DiasRutina'
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
    GROUP BY I.Nombre, I.PrimerApellido, I.SegundoApellido, C.Nombre, C.PrimerApellido, C.SegundoApellido, R.Fecha_Realizacion, R.Fecha_Inicio, R.Fecha_Fin, R.Objetivo;

	SELECT 
		DM.ValorMedida AS 'ValorMedida',
		DM.Comentario AS 'Comentario',
		DM.Fecha_Insercion AS 'FechaInsercion',
		DM.Usuario_Insercion AS 'UsuarioInsercion',
		DM.Fecha_Modificacion AS 'FechaModificacion',
		DM.Usuario_Modificacion AS 'UsarioModificacion'
	FROM MEDIDAS_RUTINA AS DM
	INNER JOIN RUTINAS R
		ON R.Id = R.IdCliente;
END //

DELIMITER ;

CALL OBTENER_REPORTE_RUTINAS_DETALLADO ('', '');

SELECT * FROM MEDIDAS_RUTINA;