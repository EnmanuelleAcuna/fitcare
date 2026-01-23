-- =============================================
-- Script: 018_AGREGAR_PLANMEMBRESIA_A_USUARIOS.sql
-- Descripcion: Agregar columna IdPlanMembresia a AspNetUsers
--              para relacionar clientes con planes de membresia
-- =============================================

-- Agregar columna IdPlanMembresia a AspNetUsers
ALTER TABLE [dbo].[AspNetUsers]
ADD IdPlanMembresia UNIQUEIDENTIFIER NULL;

-- Agregar foreign key hacia PlanesMembresia
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT FK_AspNetUsers_PlanesMembresia
FOREIGN KEY (IdPlanMembresia) REFERENCES [fitcare].[PlanesMembresia](Id);
