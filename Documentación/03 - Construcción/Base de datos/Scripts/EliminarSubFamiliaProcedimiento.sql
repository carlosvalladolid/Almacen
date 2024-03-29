/*******************************************************************************************
* NOMBRE:			EliminarSubFamiliaProcedimiento
* FECHA CREACIÓN:	10-Mayo-2012
* AUTOR:			Antonio Silva
* DESCRIPCIÓN:		Elimina un grupo de subfamilias de la aplicación
*					
* PARÁMETROS:		@CadenaSubFamiliaId		Cadena de Ids de subfamilias
*					
*********************************************************************************************/
CREATE PROCEDURE [dbo].[EliminarSubFamiliaProcedimiento]
(
	@CadenaSubFamiliaId VARCHAR(200) = ''
)

AS

	SET NOCOUNT ON

		DELETE SubFamilia   
			WHERE (@CadenaSubFamiliaId LIKE '%,' + CONVERT(VARCHAR, SubFamiliaId) + ',%')

	SET NOCOUNT OFF