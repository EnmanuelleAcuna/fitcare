USE [Gimnacio]
GO
/****** Object:  StoredProcedure [dbo].[ACTUALIZAR_RUTINA]    Script Date: 8/2/2019 16:53:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ACTUALIZAR_RUTINA]
    @ID_RUTINA INT,
    @ID_INSTRUCTOR INT ,
    @ID_JUGADOR INT,
    @FECHA_RUTINA DATETIME,
    @FECHA_INICIO_RUTINA DATETIME,
    @FECHA_FIN_RUTINA DATETIME,
    @OBJETIVOS VARCHAR (300),
    @FECHA_DE_INGRESO DATETIME,
    @FECHA_DE_INSERCION DATETIME,
    @FECHA_DE_MODIFICACION DATETIME,
    @USUARIO_MODIFICACION INT
AS
BEGIN
    UPDATE Rutinas SET
 id_Instructor=@ID_INSTRUCTOR,id_Jugador=@ID_JUGADOR,Fecha_Rutina=@FECHA_RUTINA,Fecha_Inicio_Rutina=@FECHA_INICIO_RUTINA
,Fecha_Fin_Rutina=@FECHA_FIN_RUTINA,Objetivos=@OBJETIVOS,Fecha_de_Ingreso=@FECHA_DE_INGRESO,Fecha_de_insercion=@FECHA_DE_INSERCION
,Fecha_de_modificacion=@FECHA_DE_MODIFICACION,Usuario_modificacion=@USUARIO_MODIFICACION
where id_Rutina=@ID_RUTINA

END

CREATE PROCEDURE dbo.PA_ACTUALIZAR_RUTINAS
    @ID_RUTINA INT,
    @FECHA_INICIO_RUTINA VARCHAR (255),
    @FECHA_FIN_RUTINA VARCHAR (255),
    @OBJETIVOS VARCHAR(800),
    @NUMERO_DE_REPETICIONES VARCHAR(255),
    @NUMERO_DE_DESCANSOS VARCHAR(255),
    @CUELLO VARCHAR(255),
    @HOMBRO VARCHAR(255),
    @PECHO VARCHAR(255),
    @ESPALDA VARCHAR(255),
    @ABDOMEN VARCHAR(255),
    @ANTEBRAZOS VARCHAR(255),
    @PIERNA VARCHAR(255),
    @PANTORRILLAS VARCHAR(255),
    @BICEPS VARCHAR(255),
    @TRICEPS VARCHAR(255),
    @OBLICUOS VARCHAR(255),
    @FK_TIPO_MAQUINA INT,
    @FK_TIPO_EJERCICIO INT,
    @FK_USUARIO_INSTRUCTOR INT,
    @FK_USUARIO_JUGADOR INT


AS
BEGIN

    UPDATE NUEVARUTINA SET FECHA_INICIO_RUTINA=@FECHA_INICIO_RUTINA,FECHA_FIN_RUTINA=@FECHA_FIN_RUTINA,
Objetivos=@OBJETIVOS,Numero_de_Repeticiones=@NUMERO_DE_REPETICIONES,Cuello=@CUELLO,
Hombros=@HOMBRO,Pecho=@PECHO,Espalda=@ESPALDA,Abdomen=@ABDOMEN,Antebrazos=@ANTEBRAZOS,Pierna=@PIERNA,
PANTORRILLAS=@PANTORRILLAS, BICEPS=@BICEPS,Triceps=@TRICEPS,Oblicuos=@OBLICUOS,FK_TIPO_MAQUINA=@FK_TIPO_MAQUINA,FK_TIPO_EJERCICIO=@FK_TIPO_EJERCICIO,
FK_USUARIO_INSTRUCTOR=@FK_USUARIO_INSTRUCTOR,FK_USUARIO_JUGADOR=@FK_USUARIO_JUGADOR
WHERE ID_RUTINA=@ID_RUTINA
END

CREATE PROCEDURE dbo.ACTUALIZAR_RUTINA
    @ID_RUTINA INT NULL,
    @ID_INSTRUCTOR INT  NULL,
    @ID_JUGADOR INT NULL,
    @FECHA_RUTINA DATETIME NULL,
    @FECHA_INICIO_RUTINA DATETIME NULL,
    @FECHA_FIN_RUTINA DATETIME NULL,
    @OBJETIVOS VARCHAR (300) NULL,
    @FECHA_DE_INGRESO DATETIME NULL,
    @FECHA_DE_INSERCION DATETIME NULL,
    @FECHA_DE_MODIFICACION DATETIME NULL,
    @USUARIO_MODIFICACION INT NULL
AS
BEGIN
    UPDATE Rutinas SET
 id_Instructor=@ID_INSTRUCTOR,id_Jugador=@ID_JUGADOR,Fecha_Rutina=@FECHA_RUTINA,Fecha_Inicio_Rutina=@FECHA_INICIO_RUTINA
,Fecha_Fin_Rutina=@FECHA_FIN_RUTINA,Objetivos=@OBJETIVOS,Fecha_de_Ingreso=@FECHA_DE_INGRESO,Fecha_de_insercion=@FECHA_DE_INSERCION
,Fecha_de_modificacion=@FECHA_DE_MODIFICACION,Usuario_modificacion=@USUARIO_MODIFICACION
where id_Rutina=@ID_RUTINA

END
