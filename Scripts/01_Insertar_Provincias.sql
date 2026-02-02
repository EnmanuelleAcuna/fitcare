-- =============================================
-- Script: Insertar todas las provincias de Costa Rica
-- Para recreación de base de datos
-- =============================================

INSERT INTO fitcare.PROVINCIAS (Id, Nombre, Estado, DateCreated, CreatedBy)
VALUES
    ('5364075E-8516-420C-B188-D6ADABDD8BB6', 'San José',     1, GETDATE(), 'Script'),
    ('922543D7-83A6-4719-8DE2-5C5AC0CCF244', 'Alajuela',     1, GETDATE(), 'Script'),
    ('D961B291-DA4D-4D20-A551-AB147A5C4565', 'Cartago',      1, GETDATE(), 'Script'),
    ('78D6E110-0C49-41F6-B80B-5BD53813CB59', 'Heredia',      1, GETDATE(), 'Script'),
    ('A1B2C3D4-1111-4000-A000-000000000001', 'Guanacaste',   1, GETDATE(), 'Script'),
    ('A1B2C3D4-2222-4000-A000-000000000002', 'Puntarenas',   1, GETDATE(), 'Script'),
    ('A1B2C3D4-3333-4000-A000-000000000003', 'Limón',        1, GETDATE(), 'Script');
GO

SELECT Id, Nombre, Estado FROM fitcare.PROVINCIAS ORDER BY Nombre;
GO
