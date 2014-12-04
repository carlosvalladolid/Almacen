/*******************************************************************************************
* NOMBRE:			SeleccionarOrdenEncabezado
* FECHA CREACIÓN:	18-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Busca órdenes de compra que coincidan con los parámetros enviados.
*					
* PARÁMETROS:		OrdenId					Identificador de la orden de compra
*					Clave					Folio de la Preorden
*
*********************************************************************************************/
ALTER PROCEDURE [dbo].[SeleccionarOrdenEncabezado]
(
	@OrdenId VARCHAR(36) = '',
	@Clave VARCHAR(10) = ''
)

AS

	SET NOCOUNT ON

		DECLARE	@OrdenIdGuid UNIQUEIDENTIFIER,
				@PreOrdenIdGuid UNIQUEIDENTIFIER

		IF(@OrdenId <> '')
			SET @OrdenIdGuid = CONVERT(VARCHAR(36), @OrdenId)

		IF(@Clave <> '')
			BEGIN
				SELECT @PreOrdenIdGuid = PreOrdenEncabezado.PreOrdenId
					FROM OrdenEncabezado
					INNER JOIN PreOrdenEncabezado
						ON OrdenEncabezado.PreordenId = PreOrdenEncabezado.PreOrdenId
						WHERE (PreOrdenEncabezado.Clave = @Clave)
			END

		SELECT OrdenId, PreOrdenId, EmpleadoId, JefeId, ProveedorId, EstatusId, Clave, FechaOrden, FechaInserto
			FROM OrdenEncabezado
			WHERE (OrdenId = @OrdenIdGuid OR @OrdenId = '')
			OR (PreOrdenId = @PreOrdenIdGuid OR @Clave = '')

	SET NOCOUNT OFF
