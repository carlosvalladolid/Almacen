/*******************************************************************************************
* NOMBRE:			SeleccionarProductoMaximoPermitido
* FECHA CREACIÓN:	07-Enero-2015
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Busca la existencia de un producto.
*					
* PARÁMETROS:		ProductoId					Identificador del producto
*
*********************************************************************************************/
ALTER PROCEDURE [dbo].[SeleccionarProductoMaximoPermitido]
(
	@ProductoId VARCHAR(36)
)

AS

	SET NOCOUNT ON

		SELECT ProductoId, Maximo
			FROM Producto
			WHERE (ProductoId = @ProductoId)

	SET NOCOUNT OFF
