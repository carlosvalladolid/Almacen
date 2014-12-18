/*******************************************************************************************
* NOMBRE:			InsertaOrdenDetalle
* FECHA CREACIÓN:	18-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Copia la información de una orden de compra de la tabla temporal a la tabla definitiva
*					
* PARÁMETROS:		OrdenId					Identificador de la orden de compra
*********************************************************************************************/
ALTER PROCEDURE [dbo].[InsertaOrdenDetalle]
(
	@OrdenId VARCHAR(36),
	@ProductoId VARCHAR(36),
	@Cantidad INT
)

AS

	SET NOCOUNT ON

		INSERT INTO OrdenDetalle(OrdenId, ProductoId, Cantidad)
			VALUES (@OrdenId, @ProductoId, @Cantidad)

	SET NOCOUNT OFF
