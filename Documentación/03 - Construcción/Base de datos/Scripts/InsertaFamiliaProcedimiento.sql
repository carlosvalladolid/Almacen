/*******************************************************************************************
* NOMBRE:			InsertaFamiliaProcedimiento
* FECHA CREACIÓN:	20-Abril-2012
* AUTOR:			Olivia
* DESCRIPCIÓN:		Inserta un registro de una Familia
*					
* PARÁMETROS:		@FamiliaId smallint,
*					@DependenciaId smallint,
*					@Nombre varchar(100),
*					@EstatusId smallint,

*********************************************************************************************/
CREATE PROCEDURE [dbo].[InsertarFamiliaProcedimiento]
	@DependenciaId smallint,
	@Nombre varchar(100),
	@EstatusId smallint,
	@UsuarioIdInserto smallint


AS

BEGIN
	
SET NOCOUNT ON;
	insert into Familia(DependenciaId,Nombre,EstatusId,UsuarioIdInserto,FechaInserto)
	values(@DependenciaId,@Nombre,@EstatusId,@UsuarioIdInserto,GETDATE())
END
