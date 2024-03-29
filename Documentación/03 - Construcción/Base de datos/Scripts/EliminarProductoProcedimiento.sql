USE [DefensoriaDB.Almacen]
GO
/****** Object:  StoredProcedure [dbo].[EliminarProductoProcedimiento]    Script Date: 03/02/2015 11:37:12 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<olguca>
-- Create date: <03/11/2014>
-- Description:	<Elimina un registro de la tabla de productos>
-- =============================================
ALTER PROCEDURE [dbo].[EliminarProductoProcedimiento]
(
	@CadenaProductoId VARCHAR(200) = ''
)

AS
SET NOCOUNT ON

		DELETE Producto  
			WHERE (@CadenaProductoId LIKE '%,' + CONVERT(VARCHAR(36), ProductoId) + ',%')

	SET NOCOUNT OFF