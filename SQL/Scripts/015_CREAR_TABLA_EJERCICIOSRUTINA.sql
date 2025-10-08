USE fitcare;

CREATE TABLE fitcare.EjerciciosRutina (
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_EjerciciosRutina PRIMARY KEY,
    IdRutina UNIQUEIDENTIFIER
        NOT NULL CONSTRAINT FK_EjerciciosRutina_Rutinas
        FOREIGN KEY REFERENCES fitcare.Rutinas (Id) ON DELETE CASCADE,
    IdEjercicio UNIQUEIDENTIFIER
        NOT NULL CONSTRAINT FK_EjerciciosRutina_Ejercicios
        FOREIGN KEY REFERENCES fitcare.Ejercicios (Id) ON DELETE CASCADE,
	Series INT NOT NULL,
	Repeticiones INT NOT NULL,
    MinutosDescanso INT NOT NULL,
	DateCreated DATETIME NOT NULL CONSTRAINT DF_EjerciciosRutina_DateCreated DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100) NOT NULL,
	DateUpdated DATETIME NULL,
	UpdatedBy NVARCHAR(100) NULL
);

SELECT * FROM fitcare.EjerciciosRutina;
