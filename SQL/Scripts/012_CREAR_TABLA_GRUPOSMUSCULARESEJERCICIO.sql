USE fitcare;

CREATE TABLE fitcare.GruposMuscularesEjercicio (
    IdEjercicio UNIQUEIDENTIFIER NOT NULL,
    IdGrupoMuscular UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_GruposMuscularesEjercicio PRIMARY KEY (IdEjercicio, IdGrupoMuscular),
    CONSTRAINT FK_GruposMuscularesEjercicio_Ejercicios
        FOREIGN KEY (IdEjercicio) REFERENCES fitcare.Ejercicios (Id) ON DELETE CASCADE,
    CONSTRAINT FK_GruposMuscularesEjercicio_GruposMusculares
        FOREIGN KEY (IdGrupoMuscular) REFERENCES fitcare.GruposMusculares (Id) ON DELETE CASCADE
);

SELECT * FROM fitcare.GruposMuscularesEjercicio;
