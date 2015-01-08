/*******************************************************************************************
* NOMBRE:			SeleccionarProductoExistencia
* FECHA CREACIÓN:	07-Enero-2015
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Busca la existencia de un producto.
*					
* PARÁMETROS:		ProductoId					Identificador del producto
*
*********************************************************************************************/
ALTER PROCEDURE [dbo].[SeleccionarProductoExistencia]
(
	@ProductoId VARCHAR(36)
)

AS

	SET NOCOUNT ON

		SELECT ProductoId, Existencia
			FROM Existencia
			WHERE (ProductoId = @ProductoId)

	SET NOCOUNT OFF
