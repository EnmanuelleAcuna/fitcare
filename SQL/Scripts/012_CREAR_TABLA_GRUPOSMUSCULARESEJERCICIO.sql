USE fitcare;

CREATE TABLE fitcare.GruposMuscularesEjercicio (
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_GruposMuscularesEjercicio PRIMARY KEY,
    IdEjercicio
        UNIQUEIDENTIFIER NOT NULL CONSTRAINT FK_GruposMuscularesEjercicio_Ejercicios
        FOREIGN KEY REFERENCES fitcare.Ejercicios (Id) ON DELETE CASCADE,
    IdGrupoMuscular
        UNIQUEIDENTIFIER NOT NULL CONSTRAINT FK_GruposMuscularesEjercicio_GruposMusculares
        FOREIGN KEY REFERENCES fitcare.GruposMusculares (Id) ON DELETE CASCADE,
	DateCreated DATETIME NOT NULL CONSTRAINT DF_GruposMuscularesEjercicio_DateCreated DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100) NOT NULL,
	DateUpdated DATETIME NULL,
	UpdatedBy NVARCHAR(100) NULL
);

SELECT * FROM fitcare.GruposMuscularesEjercicio;
