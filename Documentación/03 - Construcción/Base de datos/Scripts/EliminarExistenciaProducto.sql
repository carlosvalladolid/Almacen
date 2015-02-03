/*******************************************************************************************
* NOMBRE:			EliminarExistenciaProducto
* FECHA CREACIÓN:	03-Febrero-2015
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Borra los registros de existencia de los productos enviados como parámetro.
*					
* PARÁMETROS:		CadenaProductoId			Cadena de caracteres con los identificadores de los productos separados por comas
*
*********************************************************************************************/
ALTER PROCEDURE [dbo].[EliminarExistenciaProducto]
(
	@CadenaProductoId VARCHAR(8000)
)

AS

	SET NOCOUNT ON

		DELETE Existencia
			WHERE (@CadenaProductoId LIKE '%,' + CONVERT(VARCHAR(36), ProductoId) + ',%')

	SET NOCOUNT OFF
