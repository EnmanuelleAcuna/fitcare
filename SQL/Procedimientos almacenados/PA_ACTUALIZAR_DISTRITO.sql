
CREATE PROCEDURE PA_ACTUALIZAR_DISTRITO

    @ID_DISTRITO INT,
    @ID_CANTON INT,
    @NOMBRE VARCHAR(100),
    @USUARIO_CREA VARCHAR (50),
    @FECHA_CREA DATETIME,
    @USUARIO_MODIFICA DATETIME,
    @FECHA_MODIFICA DATETIME,
    @VC_ESTADO VARCHAR(3),
    @ID_DISTRITOINEC INT

AS
BEGIN

    UPDATE DISTRITO SET ID_CANTON=@ID_CANTON,NOMBRE=@NOMBRE,USUARIO_CREA=@USUARIO_CREA,fecha_Crea=@FECHA_CREA,usuario_Modifica=@USUARIO_MODIFICA,
fecha_Modifica=@FECHA_MODIFICA,vc_Estado=@VC_ESTADO,id_DistritoInec=@ID_DISTRITOINEC WHERE id_Distrito=@ID_DISTRITO


END

CREATE PROCEDURE [dbo].[PA_ACTUALIZAR_DISTRITO]

    @ID_DISTRITO INT,
    @ID_CANTON INT,
    @NOMBRE VARCHAR(100),
    @USUARIO_CREA VARCHAR (50),
    @FECHA_CREA DATETIME,
    @USUARIO_MODIFICA DATETIME,
    @FECHA_MODIFICA DATETIME,
    @VC_ESTADO VARCHAR(3),
    @ID_DISTRITOINEC INT

AS
BEGIN

    UPDATE DISTRITO SET ID_CANTON=@ID_CANTON,NOMBRE=@NOMBRE,USUARIO_CREA=@USUARIO_CREA,fecha_Crea=@FECHA_CREA,usuario_Modifica=@USUARIO_MODIFICA,
fecha_Modifica=@FECHA_MODIFICA,vc_Estado=@VC_ESTADO,id_DistritoInec=@ID_DISTRITOINEC WHERE id_Distrito=@ID_DISTRITO


END

CREATE PROCEDURE dbo.PA_ACTUALIZAR_DISTRITO

    @ID_DISTRITO INT,
    @ID_CANTON INT,
    @NOMBRE VARCHAR(100),
    @USUARIO_CREA VARCHAR (50),
    @FECHA_CREA DATETIME,
    @USUARIO_MODIFICA DATETIME,
    @FECHA_MODIFICA DATETIME,
    @VC_ESTADO VARCHAR(3),
    @ID_DISTRITOINEC INT

AS
BEGIN

    UPDATE DISTRITO SET ID_CANTON=@ID_CANTON,NOMBRE=@NOMBRE,USUARIO_CREA=@USUARIO_CREA,fecha_Crea=@FECHA_CREA,usuario_Modifica=@USUARIO_MODIFICA,
fecha_Modifica=@FECHA_MODIFICA,vc_Estado=@VC_ESTADO,id_DistritoInec=@ID_DISTRITOINEC WHERE id_Distrito=@ID_DISTRITO


END
