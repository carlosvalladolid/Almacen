/*******************************************************************************************
* NOMBRE:			InsertaProductoOrdenDetalleTemp
* FECHA CREACIÓN:	18-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Inserta un registro en la tabla temporal de OrdenDetalleTemp
*					
* PARÁMETROS:		OrdenId					Identificador de la orden de compra temporal
*					PreOrdenId				Identificador de la preorden
*					ProductoId				Identificador del producto
*********************************************************************************************/
ALTER PROCEDURE [dbo].[InsertaProductoOrdenDetalleTemp]
(
	@OrdenId VARCHAR(36),
	@PreOrdenId VARCHAR(36),
	@ProductoId VARCHAR(36)
)

AS

	SET NOCOUNT ON

		DECLARE @Cantidad SMALLINT

		SELECT @Cantidad = Cantidad
			FROM PreOrdenDetalle
			WHERE (PreOrdenId = @PreOrdenId)
			AND (ProductoId = @ProductoId)

		INSERT INTO OrdenDetalleTemp(OrdenId, ProductoId, Cantidad)
			VALUES(@OrdenId, @ProductoId, @Cantidad)

	SET NOCOUNT OFF
