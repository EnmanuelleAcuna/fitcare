create procedure [dbo].[PA_LISTAR_TIPO_USUARIO_ID]

    @ID_TIPO_USUARIO INT
AS
BEGIN
    SELECT *
    FROM TIPO_USUARIO
    WHERE ID_TIPO_USUARIO=@ID_TIPO_USUARIO
END


create procedure [dbo].[PA_LISTAR_TIPO_USUARIO]
AS
BEGIN
    SELECT *
    FROM TIPO_USUARIO
END

create procedure PA_LISTAR_TIPO_USUARIO
AS
BEGIN
    SELECT *
    FROM TIPO_USUARIO
END

create procedure PA_LISTAR_TIPO_USUARIO_ID

    @ID_TIPO_USUARIO INT
AS
BEGIN
    SELECT *
    FROM TIPO_USUARIO
    WHERE ID_TIPO_USUARIO=@ID_TIPO_USUARIO
END
