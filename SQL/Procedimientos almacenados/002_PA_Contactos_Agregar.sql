DELIMITER //

CREATE PROCEDURE PA_Contactos_Agregar (
	IN Fecha DATETIME,
    IN Nombre VARCHAR (255),
    IN Correo VARCHAR (50),
    IN Telefono VARCHAR (16),
    IN Mensaje LONGTEXT
)
BEGIN
	INSERT INTO CONTACTOS (FechaRegistro, NombreCompleto, CorreoElectronico, Telefono, Mensaje)
    VALUES (Fecha, Nombre, Correo, Telefono, Mensaje);
END 

//

DELIMITER ;

CALL PA_Contactos_Agregar();
