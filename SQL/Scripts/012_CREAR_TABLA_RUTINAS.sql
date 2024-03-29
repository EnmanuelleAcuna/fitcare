USE fitcare;

CREATE TABLE fitcare.RUTINAS (
	Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_RUTINAS PRIMARY KEY,
	IdInstructor NVARCHAR (450) NOT NULL CONSTRAINT FK_RUTINAS_INSTRUCTORES FOREIGN KEY REFERENCES dbo.AspNetUsers (Id),
	IdCliente NVARCHAR (450) NOT NULL CONSTRAINT FK_RUTINAS_CLIENTES FOREIGN KEY REFERENCES fitcare.CLIENTES (IdUsuario),
	Fecha_Realizacion DATETIME NOT NULL,
	Fecha_Inicio DATETIME NOT NULL,
	Fecha_Fin DATETIME NOT NULL,
	Objetivo VARCHAR(4000) NOT NULL,
	DateCreated DATETIME NOT NULL CONSTRAINT DF_Rutinas_DateCreated DEFAULT GETDATE(),
	CreatedBy NVARCHAR(100) NOT NULL,
	DateUpdated DATETIME NULL,
	UpdatedBy NVARCHAR(100) NULL
);

SELECT * FROM fitcare.RUTINAS;
