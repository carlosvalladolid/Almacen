/*******************************************************************************************
* NOMBRE:			InsertaOrdenDetalle
* FECHA CREACI�N:	18-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCI�N:		Copia la informaci�n de una orden de compra de la tabla temporal a la tabla definitiva
*					
* PAR�METROS:		OrdenId					Identificador de la orden de compra
*********************************************************************************************/
ALTER PROCEDURE [dbo].[InsertaOrdenDetalle]
(
	@OrdenId VARCHAR(36)
)

AS

	SET NOCOUNT ON

		INSERT INTO OrdenDetalle(OrdenId, ProductoId, Cantidad)
			SELECT OrdenId, ProductoId, Cantidad
				FROM OrdenDetalleTemp
				WHERE (OrdenId = @OrdenId)

	SET NOCOUNT OFF
