/*******************************************************************************************
* NOMBRE:			InsertarSubFamiliaProcedimiento
* FECHA CREACIÓN:	20-Abril-2012
* AUTOR:			Olivia Guzman
* DESCRIPCIÓN:		Busca una SubFamilia con los valores enviados como parámetros
*					
* PARÁMETROS:		@SubFamiliaId smallint,
*					@FamiliaId smallint,
*					@Nombre varchar(100),
*					@StatusId smallint,
*********************************************************************************************/
ALTER PROCEDURE [dbo].[InsertarSubFamiliaProcedimiento] 

	@FamiliaId smallint,
	@Nombre varchar(100),
	@EstatusId smallint,
	@UsuarioIdInserto smallint
	
AS

BEGIN
	
SET NOCOUNT ON;
	insert into SubFamilia(FamiliaId,Nombre,EstatusId,UsuarioIdInserto,FechaInserto)
	values(@FamiliaId,@Nombre,@EstatusId,@UsuarioIdInserto,GETDATE())
END
