-- Renombrar columna Dias a Meses
EXEC sp_rename 'fitcare.PlanesMembresia.Dias', 'Meses', 'COLUMN';

-- Convertir valores existentes (asumiendo 30 días = 1 mes)
UPDATE fitcare.PlanesMembresia
SET Meses = CEILING(Meses / 30.0);
