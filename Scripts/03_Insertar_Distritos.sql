-- =============================================
-- Script: Insertar todos los distritos de Costa Rica
-- Para recreación de base de datos
-- Referencia: División Territorial Administrativa de CR
-- =============================================

-- =============================================
-- Distritos existentes (con IDs conocidos)
-- =============================================

-- Higuito (Desamparados, INEC 103)
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT '29A90705-1EA9-4B5D-A525-07DB47525655', 'Higuito', 1, 14, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c WHERE c.Id_Canton_INEC = 103;

-- Santa Bárbara (Barva, INEC 402)
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT 'E431BE4E-B334-4F45-9AF0-62C327FB7E0C', 'Santa Bárbara', 1, 7, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c WHERE c.Id_Canton_INEC = 402;

-- Pocosol (San Carlos, INEC 210)
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT '661634F4-44F7-4E83-A06C-7391DAC6D4B6', 'Pocosol', 1, 13, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c WHERE c.Id_Canton_INEC = 210;

-- El Tejar (El Guarco, INEC 308)
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT '53ECE2AD-83DF-475F-A324-C916CDA6C4C8', 'El Tejar', 1, 1, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c WHERE c.Id_Canton_INEC = 308;

-- =============================================================================
-- PROVINCIA: SAN JOSÉ
-- =============================================================================

-- Canton: San José (INEC 101) - 11 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Carmen', 1), ('Merced', 2), ('Hospital', 3), ('Catedral', 4),
    ('Zapote', 5), ('San Francisco de Dos Ríos', 6), ('Uruca', 7),
    ('Mata Redonda', 8), ('Pavas', 9), ('Hatillo', 10), ('San Sebastián', 11)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 101;

-- Canton: Escazú (INEC 102) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Escazú', 1), ('San Antonio', 2), ('San Rafael', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 102;

-- Canton: Desamparados (INEC 103) - 13 distritos + Higuito (insertado arriba)
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Desamparados', 1), ('San Miguel', 2), ('San Juan de Dios', 3),
    ('San Rafael Arriba', 4), ('San Antonio', 5), ('Frailes', 6),
    ('Patarrá', 7), ('San Cristóbal', 8), ('Rosario', 9),
    ('Damas', 10), ('San Rafael Abajo', 11), ('Gravilias', 12), ('Los Guido', 13)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 103;

-- Canton: Puriscal (INEC 104) - 9 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Santiago', 1), ('Mercedes Sur', 2), ('Barbacoas', 3), ('Grifo Alto', 4),
    ('San Rafael', 5), ('Candelarita', 6), ('Desamparaditos', 7),
    ('San Antonio', 8), ('Chires', 9)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 104;

-- Canton: Tarrazú (INEC 105) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Marcos', 1), ('San Lorenzo', 2), ('San Carlos', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 105;

-- Canton: Aserrí (INEC 106) - 7 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Aserrí', 1), ('Tarbaca', 2), ('Vuelta de Jorco', 3), ('San Gabriel', 4),
    ('Legua', 5), ('Monterrey', 6), ('Salitrillos', 7)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 106;

-- Canton: Mora (INEC 107) - 7 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Colón', 1), ('Guayabo', 2), ('Tabarcia', 3), ('Piedras Negras', 4),
    ('Picagres', 5), ('Jaris', 6), ('Quitirrisí', 7)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 107;

-- Canton: Goicoechea (INEC 108) - 7 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Guadalupe', 1), ('San Francisco', 2), ('Calle Blancos', 3),
    ('Mata de Plátano', 4), ('Ipís', 5), ('Rancho Redondo', 6), ('Purral', 7)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 108;

-- Canton: Santa Ana (INEC 109) - 6 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Santa Ana', 1), ('Salitral', 2), ('Pozos', 3),
    ('Uruca', 4), ('Piedades', 5), ('Brasil', 6)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 109;

-- Canton: Alajuelita (INEC 110) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Alajuelita', 1), ('San Josecito', 2), ('San Antonio', 3),
    ('Concepción', 4), ('San Felipe', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 110;

-- Canton: Vázquez de Coronado (INEC 111) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Isidro', 1), ('San Rafael', 2), ('Dulce Nombre de Jesús', 3),
    ('Patalillo', 4), ('Cascajal', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 111;

-- Canton: Acosta (INEC 112) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Ignacio', 1), ('Guaitil', 2), ('Palmichal', 3),
    ('Cangrejal', 4), ('Sabanillas', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 112;

-- Canton: Tibás (INEC 113) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Juan', 1), ('Cinco Esquinas', 2), ('Anselmo Llorente', 3),
    ('León XIII', 4), ('Colima', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 113;

-- Canton: Moravia (INEC 114) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Vicente', 1), ('San Jerónimo', 2), ('La Trinidad', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 114;

-- Canton: Montes de Oca (INEC 115) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Pedro', 1), ('Sabanilla', 2), ('Mercedes', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 115;

-- Canton: Turrubares (INEC 116) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Pablo', 1), ('San Pedro', 2), ('San Juan de Mata', 3),
    ('San Luis', 4), ('Carara', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 116;

-- Canton: Dota (INEC 117) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Santa María', 1), ('Jardín', 2), ('Copey', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 117;

-- Canton: Curridabat (INEC 118) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Curridabat', 1), ('Granadilla', 2), ('Sánchez', 3), ('Tirrases', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 118;

-- Canton: Pérez Zeledón (INEC 119) - 11 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Isidro de El General', 1), ('El General', 2), ('Daniel Flores', 3),
    ('Rivas', 4), ('San Pedro', 5), ('Platanares', 6), ('Pejibaye', 7),
    ('Cajón', 8), ('Barú', 9), ('Río Nuevo', 10), ('Páramo', 11)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 119;

-- Canton: León Cortés Castro (INEC 120) - 6 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Pablo', 1), ('San Andrés', 2), ('Llano Bonito', 3),
    ('San Isidro', 4), ('Santa Cruz', 5), ('San Antonio', 6)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 120;

-- =============================================================================
-- PROVINCIA: ALAJUELA
-- =============================================================================

-- Canton: Alajuela (INEC 201) - 14 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Alajuela', 1), ('San José', 2), ('Carrizal', 3), ('San Antonio', 4),
    ('Guácima', 5), ('San Isidro', 6), ('Sabanilla', 7), ('San Rafael', 8),
    ('Río Segundo', 9), ('Desamparados', 10), ('Turrúcares', 11),
    ('Tambor', 12), ('Garita', 13), ('Sarapiquí', 14)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 201;

-- Canton: San Ramón (INEC 202) - 14 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Ramón', 1), ('Santiago', 2), ('San Juan', 3), ('Piedades Norte', 4),
    ('Piedades Sur', 5), ('San Rafael', 6), ('San Isidro', 7), ('Ángeles', 8),
    ('Alfaro', 9), ('Volio', 10), ('Concepción', 11), ('Zapotal', 12),
    ('Peñas Blancas', 13), ('San Lorenzo', 14)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 202;

-- Canton: Grecia (INEC 203) - 7 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Grecia', 1), ('San Isidro', 2), ('San José', 3), ('San Roque', 4),
    ('Tacares', 5), ('Puente de Piedra', 6), ('Bolívar', 7)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 203;

-- Canton: San Mateo (INEC 204) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Mateo', 1), ('Desmonte', 2), ('Jesús María', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 204;

-- Canton: Atenas (INEC 205) - 8 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Atenas', 1), ('Jesús', 2), ('Mercedes', 3), ('San Isidro', 4),
    ('Concepción', 5), ('San José', 6), ('Santa Eulalia', 7), ('Escobal', 8)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 205;

-- Canton: Naranjo (INEC 206) - 8 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Naranjo', 1), ('San Miguel', 2), ('San José', 3), ('Cirrí Sur', 4),
    ('San Jerónimo', 5), ('San Juan', 6), ('El Rosario', 7), ('Palmitos', 8)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 206;

-- Canton: Palmares (INEC 207) - 7 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Palmares', 1), ('Zaragoza', 2), ('Buenos Aires', 3), ('Santiago', 4),
    ('Candelaria', 5), ('Esquipulas', 6), ('La Granja', 7)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 207;

-- Canton: Poás (INEC 208) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Pedro', 1), ('San Juan', 2), ('San Rafael', 3),
    ('Carrillos', 4), ('Sabana Redonda', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 208;

-- Canton: Orotina (INEC 209) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Orotina', 1), ('El Mastate', 2), ('Hacienda Vieja', 3),
    ('Coyolar', 4), ('La Ceiba', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 209;

-- Canton: San Carlos (INEC 210) - 12 distritos (Pocosol insertado arriba)
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Quesada', 1), ('Florencia', 2), ('Buenavista', 3), ('Aguas Zarcas', 4),
    ('Venecia', 5), ('Pital', 6), ('La Fortuna', 7), ('La Tigra', 8),
    ('La Palmera', 9), ('Venado', 10), ('Cutris', 11), ('Monterrey', 12)
    -- Pocosol (13) insertado arriba con ID existente
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 210;

-- Canton: Zarcero (INEC 211) - 7 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Zarcero', 1), ('Laguna', 2), ('Tapesco', 3), ('Guadalupe', 4),
    ('Palmira', 5), ('Zapote', 6), ('Brisas', 7)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 211;

-- Canton: Sarchí (INEC 212) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Sarchí Norte', 1), ('Sarchí Sur', 2), ('Toro Amarillo', 3),
    ('San Pedro', 4), ('Rodríguez', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 212;

-- Canton: Upala (INEC 213) - 8 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Upala', 1), ('Aguas Claras', 2), ('San José', 3), ('Bijagua', 4),
    ('Delicias', 5), ('Dos Ríos', 6), ('Yolillal', 7), ('Canalete', 8)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 213;

-- Canton: Los Chiles (INEC 214) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Los Chiles', 1), ('Caño Negro', 2), ('El Amparo', 3), ('San Jorge', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 214;

-- Canton: Guatuso (INEC 215) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Rafael', 1), ('Buenavista', 2), ('Cote', 3), ('Katira', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 215;

-- Canton: Río Cuarto (INEC 216) - 1 distrito
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Río Cuarto', 1)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 216;

-- =============================================================================
-- PROVINCIA: CARTAGO
-- =============================================================================

-- Canton: Cartago (INEC 301) - 11 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Oriental', 1), ('Occidental', 2), ('Carmen', 3), ('San Nicolás', 4),
    ('Aguacaliente', 5), ('Guadalupe', 6), ('Corralillo', 7),
    ('Tierra Blanca', 8), ('Dulce Nombre', 9), ('Llano Grande', 10),
    ('Quebradilla', 11)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 301;

-- Canton: Paraíso (INEC 302) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Paraíso', 1), ('Santiago', 2), ('Orosi', 3),
    ('Cachí', 4), ('Llanos de Santa Lucía', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 302;

-- Canton: La Unión (INEC 303) - 8 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Tres Ríos', 1), ('San Diego', 2), ('San Juan', 3), ('San Rafael', 4),
    ('Concepción', 5), ('Dulce Nombre', 6), ('San Ramón', 7), ('Río Azul', 8)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 303;

-- Canton: Jiménez (INEC 304) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Juan Viñas', 1), ('Tucurrique', 2), ('Pejibaye', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 304;

-- Canton: Turrialba (INEC 305) - 12 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Turrialba', 1), ('La Suiza', 2), ('Peralta', 3), ('Santa Cruz', 4),
    ('Santa Teresita', 5), ('Pavones', 6), ('Tuis', 7), ('Tayutic', 8),
    ('Santa Rosa', 9), ('Tres Equis', 10), ('La Isabel', 11), ('Chirripó', 12)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 305;

-- Canton: Alvarado (INEC 306) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Pacayas', 1), ('Cervantes', 2), ('Capellades', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 306;

-- Canton: Oreamuno (INEC 307) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Rafael', 1), ('Cot', 2), ('Potrero Cerrado', 3),
    ('Cipreses', 4), ('Santa Rosa', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 307;

-- Canton: El Guarco (INEC 308) - 3 distritos (El Tejar insertado arriba)
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    -- El Tejar (1) insertado arriba con ID existente
    ('San Isidro', 2), ('Tobosi', 3), ('Patio de Agua', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 308;

-- =============================================================================
-- PROVINCIA: HEREDIA
-- =============================================================================

-- Canton: Heredia (INEC 401) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Heredia', 1), ('Mercedes', 2), ('San Francisco', 3),
    ('Ulloa', 4), ('Varablanca', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 401;

-- Canton: Barva (INEC 402) - 6 distritos (Santa Bárbara insertada arriba)
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Barva', 1), ('San Pedro', 2), ('San Pablo', 3),
    ('San Roque', 4), ('Santa Lucía', 5), ('San José de la Montaña', 6)
    -- Santa Bárbara (7) insertada arriba con ID existente
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 402;

-- Canton: Santo Domingo (INEC 403) - 8 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Santo Domingo', 1), ('San Vicente', 2), ('San Miguel', 3),
    ('Paracito', 4), ('Santo Tomás', 5), ('Santa Rosa', 6),
    ('Tures', 7), ('Pará', 8)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 403;

-- Canton: Santa Bárbara (INEC 404) - 6 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Santa Bárbara', 1), ('San Pedro', 2), ('San Juan', 3),
    ('Jesús', 4), ('Santo Domingo', 5), ('Purabá', 6)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 404;

-- Canton: San Rafael (INEC 405) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Rafael', 1), ('San Josecito', 2), ('Santiago', 3),
    ('Ángeles', 4), ('Concepción', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 405;

-- Canton: San Isidro (INEC 406) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Isidro', 1), ('San José', 2), ('Concepción', 3), ('San Francisco', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 406;

-- Canton: Belén (INEC 407) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Antonio', 1), ('La Ribera', 2), ('La Asunción', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 407;

-- Canton: Flores (INEC 408) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Joaquín', 1), ('Barrantes', 2), ('Llorente', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 408;

-- Canton: San Pablo (INEC 409) - 2 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Pablo', 1), ('Rincón de Sabanilla', 2)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 409;

-- Canton: Sarapiquí (INEC 410) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Puerto Viejo', 1), ('La Virgen', 2), ('Las Horquetas', 3),
    ('Llanuras del Gaspar', 4), ('Cureña', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 410;

-- =============================================================================
-- PROVINCIA: GUANACASTE
-- =============================================================================

-- Canton: Liberia (INEC 501) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Liberia', 1), ('Cañas Dulces', 2), ('Mayorga', 3),
    ('Nacascolo', 4), ('Curubandé', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 501;

-- Canton: Nicoya (INEC 502) - 7 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Nicoya', 1), ('Mansión', 2), ('San Antonio', 3), ('Quebrada Honda', 4),
    ('Sámara', 5), ('Nosara', 6), ('Belén de Nosarita', 7)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 502;

-- Canton: Santa Cruz (INEC 503) - 9 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Santa Cruz', 1), ('Bolsón', 2), ('Veintisiete de Abril', 3),
    ('Tempate', 4), ('Cartagena', 5), ('Cuajiniquil', 6),
    ('Diriá', 7), ('Cabo Velas', 8), ('Tamarindo', 9)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 503;

-- Canton: Bagaces (INEC 504) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Bagaces', 1), ('La Fortuna', 2), ('Mogote', 3), ('Río Naranjo', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 504;

-- Canton: Carrillo (INEC 505) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Filadelfia', 1), ('Palmira', 2), ('Sardinal', 3), ('Belén', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 505;

-- Canton: Cañas (INEC 506) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Cañas', 1), ('Palmira', 2), ('San Miguel', 3),
    ('Bebedero', 4), ('Porozal', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 506;

-- Canton: Abangares (INEC 507) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Las Juntas', 1), ('Sierra', 2), ('San Juan', 3), ('Colorado', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 507;

-- Canton: Tilarán (INEC 508) - 7 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Tilarán', 1), ('Quebrada Grande', 2), ('Tronadora', 3),
    ('Santa Rosa', 4), ('Líbano', 5), ('Tierras Morenas', 6), ('Arenal', 7)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 508;

-- Canton: Nandayure (INEC 509) - 6 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Carmona', 1), ('Santa Rita', 2), ('Zapotal', 3),
    ('San Pablo', 4), ('Porvenir', 5), ('Bejuco', 6)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 509;

-- Canton: La Cruz (INEC 510) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('La Cruz', 1), ('Santa Cecilia', 2), ('La Garita', 3), ('Santa Elena', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 510;

-- Canton: Hojancha (INEC 511) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Hojancha', 1), ('Monte Romo', 2), ('Puerto Carrillo', 3), ('Huacas', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 511;

-- =============================================================================
-- PROVINCIA: PUNTARENAS
-- =============================================================================

-- Canton: Puntarenas (INEC 601) - 15 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Puntarenas', 1), ('Pitahaya', 2), ('Chomes', 3), ('Lepanto', 4),
    ('Paquera', 5), ('Manzanillo', 6), ('Guacimal', 7), ('Barranca', 8),
    ('Isla del Coco', 9), ('Cóbano', 10), ('Chacarita', 11),
    ('El Roble', 12), ('Arancibia', 13), ('La Esperanza', 14),
    ('San Isidro', 15)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 601;

-- Canton: Esparza (INEC 602) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Espíritu Santo', 1), ('San Juan Grande', 2), ('Macacona', 3),
    ('San Rafael', 4), ('San Jerónimo', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 602;

-- Canton: Buenos Aires (INEC 603) - 9 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Buenos Aires', 1), ('Volcán', 2), ('Potrero Grande', 3), ('Boruca', 4),
    ('Pilas', 5), ('Colinas', 6), ('Chánguena', 7), ('Biolley', 8),
    ('Brunka', 9)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 603;

-- Canton: Montes de Oro (INEC 604) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Miramar', 1), ('La Unión', 2), ('San Isidro', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 604;

-- Canton: Osa (INEC 605) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Puerto Cortés', 1), ('Palmar', 2), ('Sierpe', 3),
    ('Bahía Ballena', 4), ('Piedras Blancas', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 605;

-- Canton: Quepos (INEC 606) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Quepos', 1), ('Savegre', 2), ('Naranjito', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 606;

-- Canton: Golfito (INEC 607) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Golfito', 1), ('Puerto Jiménez', 2), ('Guaycará', 3), ('Pavón', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 607;

-- Canton: Coto Brus (INEC 608) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('San Vito', 1), ('Sabalito', 2), ('Aguabuena', 3),
    ('Limoncito', 4), ('Pittier', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 608;

-- Canton: Parrita (INEC 609) - 1 distrito
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Parrita', 1)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 609;

-- Canton: Corredores (INEC 610) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Corredor', 1), ('La Cuesta', 2), ('Canoas', 3),
    ('Laurel', 4), ('Paso Canoas', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 610;

-- Canton: Garabito (INEC 611) - 2 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Jacó', 1), ('Tárcoles', 2)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 611;

-- Canton: Monteverde (INEC 612) - 1 distrito
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Monteverde', 1)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 612;

-- =============================================================================
-- PROVINCIA: LIMÓN
-- =============================================================================

-- Canton: Limón (INEC 701) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Limón', 1), ('Valle La Estrella', 2), ('Río Blanco', 3), ('Matama', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 701;

-- Canton: Pococí (INEC 702) - 6 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Guápiles', 1), ('Jiménez', 2), ('La Rita', 3),
    ('Roxana', 4), ('Cariari', 5), ('Colorado', 6)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 702;

-- Canton: Siquirres (INEC 703) - 6 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Siquirres', 1), ('Pacuarito', 2), ('Florida', 3),
    ('Germania', 4), ('Cairo', 5), ('Alegría', 6)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 703;

-- Canton: Talamanca (INEC 704) - 4 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Bratsi', 1), ('Sixaola', 2), ('Cahuita', 3), ('Telire', 4)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 704;

-- Canton: Matina (INEC 705) - 3 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Matina', 1), ('Batán', 2), ('Carrandi', 3)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 705;

-- Canton: Guácimo (INEC 706) - 5 distritos
INSERT INTO fitcare.DISTRITOS (Id, Nombre, Estado, Id_Distrito_INEC, Id_Canton, DateCreated, CreatedBy)
SELECT NEWID(), d.Nombre, 1, d.Inec, c.Id, GETDATE(), 'Script'
FROM fitcare.CANTONES c
CROSS JOIN (VALUES
    ('Guácimo', 1), ('Mercedes', 2), ('Pocora', 3),
    ('Río Jiménez', 4), ('Duacarí', 5)
) AS d(Nombre, Inec)
WHERE c.Id_Canton_INEC = 706;

-- =============================================================================
-- Verificación
-- =============================================================================
SELECT p.Nombre AS Provincia, ca.Nombre AS Canton, COUNT(d.Id) AS TotalDistritos
FROM fitcare.PROVINCIAS p
INNER JOIN fitcare.CANTONES ca ON ca.Id_Provincia = p.Id
LEFT JOIN fitcare.DISTRITOS d ON d.Id_Canton = ca.Id
GROUP BY p.Nombre, ca.Nombre
ORDER BY p.Nombre, ca.Nombre;
GO
