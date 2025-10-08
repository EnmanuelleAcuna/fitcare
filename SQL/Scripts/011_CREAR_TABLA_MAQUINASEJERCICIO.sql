USE fitcare;

CREATE TABLE fitcare.MaquinasEjercicio (
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_MaquinasEjercicio PRIMARY KEY,
    IdEjercicio UNIQUEIDENTIFIER
        NOT NULL CONSTRAINT FK_MaquinasEjercicio_Ejercicios
        FOREIGN KEY REFERENCES fitcare.Ejercicios (Id) ON DELETE CASCADE,
    IdMaquina
        UNIQUEIDENTIFIER NOT NULL CONSTRAINT FK_MaquinasEjercicio_Maquinas
        FOREIGN KEY REFERENCES fitcare.Maquinas (Id) ON DELETE CASCADE,
	DateCreated DATETIME NOT NULL CONSTRAINT DF_MaquinasEjercicio_DateCreated DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100) NOT NULL,
	DateUpdated DATETIME NULL,
	UpdatedBy NVARCHAR(100) NULL
);

SELECT * FROM fitcare.MaquinasEjercicio;
