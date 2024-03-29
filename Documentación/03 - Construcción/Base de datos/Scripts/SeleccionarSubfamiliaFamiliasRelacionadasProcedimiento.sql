/*******************************************************************************************
* NOMBRE:			SeleccionarSubfamiliaFamiliasRelacionadasProcedimiento
* FECHA CREACIÓN:	10-Mayo-2012
* AUTOR:			Antonio Silva
* DESCRIPCIÓN:		Busca si una familia tiene subfamilias relacionadas
*					
* PARÁMETROS:		@CadenaFamiliaId		Cadena de Ids de familias
*					
*********************************************************************************************/
CREATE PROCEDURE [dbo].[SeleccionarSubfamiliaFamiliasRelacionadasProcedimiento]
(
	@CadenaFamiliaId VARCHAR(200) = ''
)

AS

	SET NOCOUNT ON

		SELECT 1 FROM SubFamilia   
			WHERE (@CadenaFamiliaId LIKE '%,' + CONVERT(VARCHAR, FamiliaId) + ',%')

	SET NOCOUNT OFF