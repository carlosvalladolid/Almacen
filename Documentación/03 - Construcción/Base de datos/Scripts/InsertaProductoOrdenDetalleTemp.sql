/*******************************************************************************************
* NOMBRE:			InsertaProductoOrdenDetalleTemp
* FECHA CREACIÓN:	18-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Inserta un registro en la tabla temporal de OrdenDetalleTemp
*					
* PARÁMETROS:		OrdenId					Identificador de la orden de compra temporal
*					ProductoId				Identificador del producto
*********************************************************************************************/
ALTER PROCEDURE [dbo].[InsertaProductoOrdenDetalleTemp]
(
	@OrdenId VARCHAR(36),
	@ProductoId VARCHAR(36)
)

AS

	SET NOCOUNT ON

		DECLARE @Cantidad SMALLINT

		SELECT @Cantidad = PreOrdenDetalle.Cantidad
			FROM OrdenEncabezado
			INNER JOIN PreOrdenEncabezado
			ON OrdenEncabezado.PreOrdenId = PreOrdenEncabezado.PreOrdenId
			INNER JOIN PreOrdenDetalle
			ON PreOrdenEncabezado.PreOrdenId = PreOrdenDetalle.PreOrdenId
			WHERE (OrdenEncabezado.OrdenId = @OrdenId)
			AND (PreOrdenDetalle.ProductoId = @ProductoId)

		INSERT INTO OrdenDetalleTemp(OrdenId, ProductoId, Cantidad)
			VALUES(@OrdenId, @ProductoId, @Cantidad)

	SET NOCOUNT OFF
