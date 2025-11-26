CREATE TABLE fitcare.MaquinasEjercicio (
    IdMaquina UNIQUEIDENTIFIER NOT NULL,
    IdEjercicio UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_MaquinasEjercicio PRIMARY KEY (IdMaquina, IdEjercicio),
    CONSTRAINT FK_MaquinasEjercicio_Maquinas
        FOREIGN KEY (IdMaquina) REFERENCES fitcare.MAQUINAS (Id) ON DELETE CASCADE,
    CONSTRAINT FK_MaquinasEjercicio_Ejercicios
        FOREIGN KEY (IdEjercicio) REFERENCES fitcare.Ejercicios (Id) ON DELETE CASCADE
);

SELECT * FROM fitcare.MaquinasEjercicio;
