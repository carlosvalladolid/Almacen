/*******************************************************************************************
* NOMBRE:			EliminarExistenciaProducto
* FECHA CREACI�N:	03-Febrero-2015
* AUTOR:			Carlos Valladolid
* DESCRIPCI�N:		Borra los registros de existencia de los productos enviados como par�metro.
*					
* PAR�METROS:		CadenaProductoId			Cadena de caracteres con los identificadores de los productos separados por comas
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
