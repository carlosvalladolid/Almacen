/*******************************************************************************************
* NOMBRE:			SeleccionarProductoExistencia
* FECHA CREACI�N:	07-Enero-2015
* AUTOR:			Carlos Valladolid
* DESCRIPCI�N:		Busca la existencia de un producto.
*					
* PAR�METROS:		ProductoId					Identificador del producto
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
