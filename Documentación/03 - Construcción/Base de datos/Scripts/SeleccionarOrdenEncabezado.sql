/*******************************************************************************************
* NOMBRE:			SeleccionarOrdenEncabezado
* FECHA CREACIÓN:	18-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Busca órdenes de compra que coincidan con los parámetros enviados.
*					
* PARÁMETROS:		OrdenId					Identificador de la orden de compra
*					PreOrdenId				Identificador de la preorden de compra
*
*********************************************************************************************/
ALTER PROCEDURE [dbo].[SeleccionarOrdenEncabezado]
(
	@OrdenId VARCHAR(36) = '',
	@PreOrdenId VARCHAR(10) = ''
)

AS

	SET NOCOUNT ON

		DECLARE	@OrdenIdGuid UNIQUEIDENTIFIER,
				@PreOrdenIdGuid UNIQUEIDENTIFIER

		IF(@OrdenId <> '')
			SET @OrdenIdGuid = CONVERT(VARCHAR(36), @OrdenId)

		IF(@PreOrdenId <> '')
			BEGIN
				SELECT @PreOrdenIdGuid = PreOrdenId
					FROM PreOrdenEncabezado
						WHERE (Clave = @PreOrdenId)
			END

		SELECT OrdenId, PreOrdenId, EmpleadoId, JefeId, ProveedorId, EstatusId, Clave, FechaOrden, FechaInserto
			FROM OrdenEncabezado
			WHERE (OrdenId = @OrdenIdGuid OR @OrdenId = '')
			OR (PreOrdenId = @PreOrdenIdGuid OR @PreOrdenId = '')

	SET NOCOUNT OFF
