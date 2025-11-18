USE fitcare;

CREATE TABLE fitcare.GruposMusculares (
	Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_GruposMusculares PRIMARY KEY,
	Nombre VARCHAR (255) NOT NULL,
    Descripcion VARCHAR (4000) NOT NULL,
    Estado BIT NOT NULL,
	DateCreated DATETIME NOT NULL CONSTRAINT DF_GruposMusculares_DateCreated DEFAULT GETUTCDATE(),
	CreatedBy NVARCHAR(100) NOT NULL,
	DateUpdated DATETIME NULL,
	UpdatedBy NVARCHAR(100) NULL
);

SELECT * FROM fitcare.GruposMusculares;
