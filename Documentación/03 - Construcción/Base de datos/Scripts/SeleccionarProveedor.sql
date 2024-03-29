USE [DefensoriaDB.Almacen]
GO
/****** Object:  StoredProcedure [dbo].[SeleccionarProveedor]    Script Date: 15/12/2014 10:53:02 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<olguca>
-- Create date: <13/11/2014>
-- Description:	<Genera un registro de la tabla de Proveedor>
-- =============================================
ALTER PROCEDURE [dbo].[SeleccionarProveedor]
(
	@ProveedorId smallint=0,
	@DependenciaId smallint=0
)
	
AS

BEGIN
	
	SET NOCOUNT ON;
	select ProveedorId, Nombre, RFC, Calle, Numero, Colonia, CodigoPostal, Telefono, NombreContacto, Email, Clabe
		from Proveedor
		where (Proveedor.ProveedorId = @ProveedorId or @ProveedorId = 0)
		and (Proveedor.DependenciaId = @DependenciaId or @DependenciaId=0)
END

