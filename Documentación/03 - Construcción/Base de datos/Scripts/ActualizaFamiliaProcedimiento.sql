/*******************************************************************************************
* NOMBRE:			ActualizaFamiliaProcedimiento
* FECHA CREACIÓN:	20-Abril-2012
* AUTOR:			Olivia Guzman
* DESCRIPCIÓN:		Actualiza un registro de una Familia
*					
* PARÁMETROS:		@FamiliaId smallint,
*					@DependenciaId smallint,
*					@Nombre varchar(100),
*					@EstatusId smallint,
*					@FechaUltimaModificacion datetime,
*					@UsuarioIdModifico smallint

*********************************************************************************************/
CREATE PROCEDURE [dbo].[ActualizarFamiliaProcedimiento]
	@FamiliaId smallint,
	@DependenciaId smallint,
	@Nombre varchar(100),
	@EstatusId smallint,
	@UsuarioIdModifico smallint
	
AS

SET NOCOUNT ON	
	update Familia
	set DependenciaId = @DependenciaId,
	Nombre = @Nombre,
	EstatusId = @EstatusId,
	FechaUltimaModificacion = GETDATE(),
	UsuarioIdModifico = @UsuarioIdModifico
	where FamiliaId = @FamiliaId

SET NOCOUNT OFF
