-- =============================================
-- Script: Insertar todos los cantones de Costa Rica
-- Para recreación de base de datos
-- =============================================

-- IDs de provincias
DECLARE @SanJose     UNIQUEIDENTIFIER = '5364075E-8516-420C-B188-D6ADABDD8BB6';
DECLARE @Alajuela    UNIQUEIDENTIFIER = '922543D7-83A6-4719-8DE2-5C5AC0CCF244';
DECLARE @Cartago     UNIQUEIDENTIFIER = 'D961B291-DA4D-4D20-A551-AB147A5C4565';
DECLARE @Heredia     UNIQUEIDENTIFIER = '78D6E110-0C49-41F6-B80B-5BD53813CB59';
DECLARE @Guanacaste  UNIQUEIDENTIFIER = 'A1B2C3D4-1111-4000-A000-000000000001';
DECLARE @Puntarenas  UNIQUEIDENTIFIER = 'A1B2C3D4-2222-4000-A000-000000000002';
DECLARE @Limon       UNIQUEIDENTIFIER = 'A1B2C3D4-3333-4000-A000-000000000003';

-- =============================================
-- SAN JOSÉ (20 cantones)
-- Códigos INEC: provincia 1
-- =============================================
INSERT INTO fitcare.CANTONES (Id, Nombre, Estado, Id_Canton_INEC, Id_Provincia, DateCreated, CreatedBy)
VALUES
    (NEWID(), 'San José',            1, 101, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Escazú',              1, 102, @SanJose, GETDATE(), 'Script'),
    ('818B2D67-B602-4F9D-8CE0-DDBC779457AD', 'Desamparados', 1, 103, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Puriscal',            1, 104, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Tarrazú',             1, 105, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Aserrí',              1, 106, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Mora',                1, 107, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Goicoechea',          1, 108, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Santa Ana',           1, 109, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Alajuelita',          1, 110, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Vázquez de Coronado', 1, 111, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Acosta',              1, 112, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Tibás',               1, 113, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Moravia',             1, 114, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Montes de Oca',       1, 115, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Turrubares',          1, 116, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Dota',                1, 117, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Curridabat',          1, 118, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'Pérez Zeledón',       1, 119, @SanJose, GETDATE(), 'Script'),
    (NEWID(), 'León Cortés Castro',  1, 120, @SanJose, GETDATE(), 'Script');

-- =============================================
-- ALAJUELA (16 cantones)
-- Códigos INEC: provincia 2
-- =============================================
INSERT INTO fitcare.CANTONES (Id, Nombre, Estado, Id_Canton_INEC, Id_Provincia, DateCreated, CreatedBy)
VALUES
    (NEWID(), 'Alajuela',            1, 201, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'San Ramón',           1, 202, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Grecia',              1, 203, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'San Mateo',           1, 204, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Atenas',              1, 205, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Naranjo',             1, 206, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Palmares',            1, 207, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Poás',                1, 208, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Orotina',             1, 209, @Alajuela, GETDATE(), 'Script'),
    ('EDF10DDF-8A5C-425D-9CBE-730BB4FE72E5', 'San Carlos', 1, 210, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Zarcero',             1, 211, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Sarchí',              1, 212, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Upala',               1, 213, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Los Chiles',          1, 214, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Guatuso',             1, 215, @Alajuela, GETDATE(), 'Script'),
    (NEWID(), 'Río Cuarto',          1, 216, @Alajuela, GETDATE(), 'Script');

-- =============================================
-- CARTAGO (8 cantones)
-- Códigos INEC: provincia 3
-- =============================================
INSERT INTO fitcare.CANTONES (Id, Nombre, Estado, Id_Canton_INEC, Id_Provincia, DateCreated, CreatedBy)
VALUES
    (NEWID(), 'Cartago',             1, 301, @Cartago, GETDATE(), 'Script'),
    (NEWID(), 'Paraíso',             1, 302, @Cartago, GETDATE(), 'Script'),
    (NEWID(), 'La Unión',            1, 303, @Cartago, GETDATE(), 'Script'),
    (NEWID(), 'Jiménez',             1, 304, @Cartago, GETDATE(), 'Script'),
    (NEWID(), 'Turrialba',           1, 305, @Cartago, GETDATE(), 'Script'),
    (NEWID(), 'Alvarado',            1, 306, @Cartago, GETDATE(), 'Script'),
    (NEWID(), 'Oreamuno',            1, 307, @Cartago, GETDATE(), 'Script'),
    ('EA97071A-96F0-4011-A72B-71627981A46A', 'El Guarco', 1, 308, @Cartago, GETDATE(), 'Script');

-- =============================================
-- HEREDIA (10 cantones)
-- Códigos INEC: provincia 4
-- =============================================
INSERT INTO fitcare.CANTONES (Id, Nombre, Estado, Id_Canton_INEC, Id_Provincia, DateCreated, CreatedBy)
VALUES
    (NEWID(), 'Heredia',             1, 401, @Heredia, GETDATE(), 'Script'),
    ('C88822DC-46F9-4FE4-A87C-7B0B6C2423A5', 'Barva', 1, 402, @Heredia, GETDATE(), 'Script'),
    (NEWID(), 'Santo Domingo',       1, 403, @Heredia, GETDATE(), 'Script'),
    (NEWID(), 'Santa Bárbara',       1, 404, @Heredia, GETDATE(), 'Script'),
    (NEWID(), 'San Rafael',          1, 405, @Heredia, GETDATE(), 'Script'),
    (NEWID(), 'San Isidro',          1, 406, @Heredia, GETDATE(), 'Script'),
    (NEWID(), 'Belén',               1, 407, @Heredia, GETDATE(), 'Script'),
    (NEWID(), 'Flores',              1, 408, @Heredia, GETDATE(), 'Script'),
    (NEWID(), 'San Pablo',           1, 409, @Heredia, GETDATE(), 'Script'),
    (NEWID(), 'Sarapiquí',           1, 410, @Heredia, GETDATE(), 'Script');

-- =============================================
-- GUANACASTE (11 cantones)
-- Códigos INEC: provincia 5
-- =============================================
INSERT INTO fitcare.CANTONES (Id, Nombre, Estado, Id_Canton_INEC, Id_Provincia, DateCreated, CreatedBy)
VALUES
    (NEWID(), 'Liberia',             1, 501, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'Nicoya',              1, 502, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'Santa Cruz',          1, 503, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'Bagaces',             1, 504, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'Carrillo',            1, 505, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'Cañas',               1, 506, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'Abangares',           1, 507, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'Tilarán',             1, 508, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'Nandayure',           1, 509, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'La Cruz',             1, 510, @Guanacaste, GETDATE(), 'Script'),
    (NEWID(), 'Hojancha',            1, 511, @Guanacaste, GETDATE(), 'Script');

-- =============================================
-- PUNTARENAS (12 cantones)
-- Códigos INEC: provincia 6
-- =============================================
INSERT INTO fitcare.CANTONES (Id, Nombre, Estado, Id_Canton_INEC, Id_Provincia, DateCreated, CreatedBy)
VALUES
    (NEWID(), 'Puntarenas',          1, 601, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Esparza',             1, 602, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Buenos Aires',        1, 603, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Montes de Oro',       1, 604, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Osa',                 1, 605, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Quepos',              1, 606, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Golfito',             1, 607, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Coto Brus',           1, 608, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Parrita',             1, 609, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Corredores',          1, 610, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Garabito',            1, 611, @Puntarenas, GETDATE(), 'Script'),
    (NEWID(), 'Monteverde',          1, 612, @Puntarenas, GETDATE(), 'Script');

-- =============================================
-- LIMÓN (6 cantones)
-- Códigos INEC: provincia 7
-- =============================================
INSERT INTO fitcare.CANTONES (Id, Nombre, Estado, Id_Canton_INEC, Id_Provincia, DateCreated, CreatedBy)
VALUES
    (NEWID(), 'Limón',               1, 701, @Limon, GETDATE(), 'Script'),
    (NEWID(), 'Pococí',              1, 702, @Limon, GETDATE(), 'Script'),
    (NEWID(), 'Siquirres',           1, 703, @Limon, GETDATE(), 'Script'),
    (NEWID(), 'Talamanca',           1, 704, @Limon, GETDATE(), 'Script'),
    (NEWID(), 'Matina',              1, 705, @Limon, GETDATE(), 'Script'),
    (NEWID(), 'Guácimo',             1, 706, @Limon, GETDATE(), 'Script');

-- Verificación
SELECT p.Nombre AS Provincia, COUNT(c.Id) AS TotalCantones
FROM fitcare.PROVINCIAS p
LEFT JOIN fitcare.CANTONES c ON c.Id_Provincia = p.Id
GROUP BY p.Nombre
ORDER BY p.Nombre;
GO
