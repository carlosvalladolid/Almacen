/*******************************************************************************************
* NOMBRE:			SeleccionarPreOrdenSinOrden
* FECHA CREACIÓN:	21-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Busca una preorden relacionada a una orden de compra por el folio de la preorden.
*					
* PARÁMETROS:		Clave					Folio de la preorden de compra
*
*********************************************************************************************/
ALTER PROCEDURE [dbo].[SeleccionarPreOrdenSinOrden]
(
	@Clave VARCHAR(10)
)

AS

	SET NOCOUNT ON

		SELECT PreOrdenEncabezado.PreOrdenId
			FROM OrdenEncabezado
			INNER JOIN PreOrdenEncabezado
				ON OrdenEncabezado.PreordenId = PreOrdenEncabezado.PreOrdenId
				WHERE (PreOrdenEncabezado.Clave = @Clave)

	SET NOCOUNT OFF
