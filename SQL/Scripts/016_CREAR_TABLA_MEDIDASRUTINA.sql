USE fitcare;

CREATE TABLE fitcare.MedidasRutina (
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_MedidasRutina PRIMARY KEY,
    IdRutina
        UNIQUEIDENTIFIER NOT NULL CONSTRAINT FK_MedidasRutina_Rutinas
        FOREIGN KEY REFERENCES fitcare.Rutinas (Id) ON DELETE CASCADE,
    IdTipoMedida
        UNIQUEIDENTIFIER NOT NULL CONSTRAINT FK_MEdidasRutina_TiposMedida
        FOREIGN KEY REFERENCES fitcare.TiposMedida (Id) ON DELETE CASCADE,
    Valor VARCHAR (50) NOT NULL,
    Comentario VARCHAR(4000) NULL,
	DateCreated DATETIME NOT NULL CONSTRAINT DF_MedidasRutina_DateCreated DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100) NOT NULL,
	DateUpdated DATETIME NULL,
	UpdatedBy NVARCHAR(100) NULL
);

SELECT * FROM fitcare.MedidasRutina;
