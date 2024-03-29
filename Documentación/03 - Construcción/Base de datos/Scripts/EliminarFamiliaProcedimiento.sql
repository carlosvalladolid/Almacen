/*******************************************************************************************
* NOMBRE:			EliminarFamiliaProcedimiento
* FECHA CREACIÓN:	10-Mayo-2012
* AUTOR:			Antonio Silva
* DESCRIPCIÓN:		Elimina un grupo de familias de la aplicación
*					
* PARÁMETROS:		@CadenaFamiliaId		Cadena de Ids de familias
*					
*********************************************************************************************/
CREATE PROCEDURE [dbo].[EliminarFamiliaProcedimiento]
(
	@CadenaFamiliaId VARCHAR(200) = ''
)

AS

	SET NOCOUNT ON

		DELETE Familia   
			WHERE (@CadenaFamiliaId LIKE '%,' + CONVERT(VARCHAR, FamiliaId) + ',%')

	SET NOCOUNT OFF