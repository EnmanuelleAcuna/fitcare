USE fitcare;

CREATE TABLE fitcare.DISTRITOS (
	Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_DISTRITOS PRIMARY KEY,
	Id_Canton UNIQUEIDENTIFIER NOT NULL CONSTRAINT FK_DISTRITOS_CANTONES FOREIGN KEY REFERENCES fitcare.CANTONES (Id),
    Nombre VARCHAR (100) NOT NULL,
    Estado BIT NOT NULL,
    Id_Distrito_INEC INT NOT NULL,
	DateCreated DATETIME NOT NULL CONSTRAINT DF_Distritos_DateCreated DEFAULT GETDATE(),
	CreatedBy NVARCHAR(100) NOT NULL,
	DateUpdated DATETIME NULL,
	UpdatedBy NVARCHAR(100) NULL
);

SELECT * FROM fitcare.DISTRITOS;
