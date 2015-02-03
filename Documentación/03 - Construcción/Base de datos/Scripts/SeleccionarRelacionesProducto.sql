/*******************************************************************************************
* NOMBRE:			SeleccionarRelacionesProducto
* FECHA CREACIÓN:	03-Febrero-2015
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Busca las relaciones de productos con otras tablas.
*					
* PARÁMETROS:		CadenaProductoId			Cadena de caracteres con los identificadores de los productos separados por comas
*
*********************************************************************************************/
ALTER PROCEDURE [dbo].[SeleccionarRelacionesProducto]
(
	@CadenaProductoId VARCHAR(8000)
)

AS

	SET NOCOUNT ON

		-- Requisición
		IF EXISTS(SELECT TOP 1 1 FROM RequisicionDetalle WHERE (@CadenaProductoId LIKE '%,' + CONVERT(VARCHAR(36), ProductoId) + ',%'))
				SELECT 'RequisicionDetalle' AS TablaRelacionada

		-- Pre orden de compra
		IF EXISTS(SELECT TOP 1 1 FROM PreOrdenDetalle WHERE (@CadenaProductoId LIKE '%,' + CONVERT(VARCHAR(36), ProductoId) + ',%'))
			SELECT 'PreOrdenDetalle' AS TablaRelacionada

		-- Orden de compra
		IF EXISTS(SELECT TOP 1 1 FROM OrdenDetalle WHERE (@CadenaProductoId LIKE '%,' + CONVERT(VARCHAR(36), ProductoId) + ',%'))
			SELECT 'OrdenDetalle' AS TablaRelacionada

		-- Recepción
		IF EXISTS(SELECT TOP 1 1 FROM RecepcionDetalle WHERE (@CadenaProductoId LIKE '%,' + CONVERT(VARCHAR(36), ProductoId) + ',%'))
			SELECT 'RecepcionDetalle' AS TablaRelacionada

		-- Orden de salida
		IF EXISTS(SELECT TOP 1 1 FROM OrdenSalidaDetalle WHERE (@CadenaProductoId LIKE '%,' + CONVERT(VARCHAR(36), ProductoId) + ',%'))
			SELECT 'OrdenSalidaDetalle' AS TablaRelacionada

	SET NOCOUNT OFF
