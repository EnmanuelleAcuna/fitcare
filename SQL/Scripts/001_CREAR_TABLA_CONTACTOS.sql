USE fitcare;

CREATE TABLE fitcare.CONTACTOS (
	Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_CONTACTOS PRIMARY KEY,
	FechaRegistro DATETIME NOT NULL,
	NombreCompleto VARCHAR (255) NOT NULL,
	CorreoElectronico VARCHAR (50) NOT NULL,
	Telefono VARCHAR (16) NOT NULL,
	Mensaje VARCHAR(4000) NOT NULL
);

SELECT * FROM fitcare.CONTACTOS;
