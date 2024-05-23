-- TSQL
USE master;

CREATE DATABASE fitcare
ON PRIMARY (NAME = N'fitcare_data', FILENAME = N'/var/opt/mssql/data/fitcare_data.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB)
LOG ON (NAME = N'fitcare_log', FILENAME = N'/var/opt/mssql/data/fitcare_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB);

-- Actualizar nombre de la base de datos
--USE master
--GO
--ALTER DATABASE fitcare MODIFY NAME = fitcareDB
--GO

-- Crear un usuario para ser utilizado por EntityFramework
CREATE LOGIN fitcareEF WITH PASSWORD = 'Hola123@#';

USE fitcare;

-- Otorgar permisos al usuario en BD fitcare
CREATE USER fitcareEF FOR LOGIN fitcareEF;

ALTER ROLE db_datareader ADD MEMBER fitcareEF;

ALTER ROLE db_datawriter ADD MEMBER fitcareEF;

GRANT CREATE TABLE TO fitcareEF AS dbo;

USE master;

-- Cambiar el modo de autenticación para la instancia y poder conectarse con el usuario a traves de MSSQL Management Studio
EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'LoginMode', REG_DWORD, 2;

CREATE SCHEMA fitcare;
