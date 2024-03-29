USE fitcare;

CREATE TABLE fitcare.TIPOS_EJERCICIO (
	Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_TIPOS_EJERCICIO PRIMARY KEY,
	Codigo VARCHAR (100) NOT NULL CONSTRAINT UQ_TiposEjercicio_Codigo UNIQUE,
	Nombre VARCHAR(255) NOT NULL,
    Estado BIT NOT NULL,
	DateCreated DATETIME NOT NULL CONSTRAINT DF_TiposEjerccicio_DateCreated DEFAULT GETDATE(),
	CreatedBy NVARCHAR(100) NOT NULL,
	DateUpdated DATETIME NULL,
	UpdatedBy NVARCHAR(100) NULL
);

SELECT * FROM fitcare.TIPOS_EJERCICIO;
