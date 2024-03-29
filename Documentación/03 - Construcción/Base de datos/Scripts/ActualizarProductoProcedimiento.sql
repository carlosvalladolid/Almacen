USE [DefensoriaDB.Almacen]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarProductoProcedimiento]    Script Date: 03/02/2015 10:28:53 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		OLGUCA
-- Create date: 31/10/2014
-- Description:	Modifica un registro en la tabla de Productos
-- =============================================
ALTER PROCEDURE [dbo].[ActualizarProductoProcedimiento]
	@ProductoId varchar(36),
	@SubFamiliaId smallint,
	@MarcaId smallint,
	@UnidadMedidaId varchar(36),
	@EstatusId int,
	@Clave varchar(20),
	@Descripcion varchar(200),
	@Minimo smallint,
	@Maximo smallint,
	@MaximoPermitido smallint

AS
BEGIN
	
	SET NOCOUNT ON;
		update Producto
		set SubFamiliaId = @SubFamiliaId,
		MarcaId = @MarcaId,
		UnidadMedidaId = @UnidadMedidaId,
		EstatusId = @EstatusId,
		Clave = @Clave,
		Descripcion = @Descripcion,
		Minimo = @Minimo,
		Maximo = @Maximo,
		MaximoPermitido = @MaximoPermitido
		where ProductoId = @ProductoId
		
	END
