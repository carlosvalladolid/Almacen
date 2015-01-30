/*******************************************************************************************
* NOMBRE:			InsertarProductoExistencia
* FECHA CREACI�N:	29-Enero-2015
* AUTOR:			Carlos Valladolid
* DESCRIPCI�N:		Guarda la existencia inicial de un producto.
*					
* PAR�METROS:		ProductoId				Identificador del producto
*					Existencia				Existencia del producto
*					
*********************************************************************************************/
ALTER PROCEDURE [dbo].[InsertarProductoExistencia]
(
	@ProductoId VARCHAR(36),
	@Existencia INT
)

AS

	SET NOCOUNT ON

		INSERT INTO Existencia(ProductoId, Existencia)
			VALUES(@ProductoId, @Existencia)

	SET NOCOUNT OFF
