/*******************************************************************************************
* NOMBRE:			ActualizaSubFamiliaProcedimiento
* FECHA CREACIÓN:	20-Abril-2012
* AUTOR:			Olivia Guzman
* DESCRIPCIÓN:		Actualiza un registro de una SubFamilia
*					
* PARÁMETROS:		@DireccionId smallint,
*					@DependenciaId smallint,
*					@EstatusId smallint,
*					@UsuarioIdInserto smallint,
*					@UsuarioIdModifico smallint,
*					@Nombre varchar(100),
*					@FechaUltimaModificacion datetime,
*					@FechaInserto datetime

*********************************************************************************************/
CREATE PROCEDURE [dbo].[ActualizarSubFamiliaProcedimiento]
	@SubFamiliaId smallint,
	@FamiliaId smallint,
	@Nombre varchar(100),
	@EstatusId smallint,
	@UsuarioIdModifico smallint	

AS

SET NOCOUNT ON	
	update SubFamilia
	set FamiliaId = @FamiliaId,
	Nombre = @Nombre,
	EstatusId = @EstatusId,
	FechaUltimaModificacion = GETDATE(),
	UsuarioIdModifico=@UsuarioIdModifico
	where SubFamiliaId = @SubFamiliaId

SET NOCOUNT OFF
