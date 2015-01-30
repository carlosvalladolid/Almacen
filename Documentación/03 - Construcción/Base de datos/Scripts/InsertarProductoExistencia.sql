/*******************************************************************************************
* NOMBRE:			InsertarProductoExistencia
* FECHA CREACIÓN:	29-Enero-2015
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Guarda la existencia inicial de un producto.
*					
* PARÁMETROS:		ProductoId				Identificador del producto
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
