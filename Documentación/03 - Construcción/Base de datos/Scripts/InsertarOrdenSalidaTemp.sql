/*******************************************************************************************
* NOMBRE:			InsertarOrdenSalidaTemp
* FECHA CREACIÓN:	28-Enero-2014
* AUTOR:			Rubén Rodríguez
* DESCRIPCIÓN:		Guarda un registro nuevo en la tabla temporal de orden de salida.
*					
* PARÁMETROS:		OrdenSalidaId			Identificador de la orden de salida
*					RequisicionId			Identificador de la requisicion
*					EstatusId				Estatus de la requisicion
*					UsuarioInserto			Usuario que inserto
*					Clave					[Folio]Clave de la requisicion
*********************************************************************************************/
CREATE PROCEDURE [dbo].[InsertarOrdenSalidaTemp]
(
	@OrdenSalidaId VARCHAR(36),
	@RequisicionId VARCHAR(36),
	@EstatusId SMALLINT,
	@UsuarioInserto INT
)
AS

	SET NOCOUNT ON
	INSERT INTO OrdenSalidaEncabezadoTemp([OrdenSalidaId],[RequisicionId],[EstatusId],[UsuarioIdInserto],[Clave],[FechaInserto])
	VALUES(@OrdenSalidaId,@RequisicionId,@EstatusId,@UsuarioInserto,NULL,GETDATE());
	SET NOCOUNT OFF
