USE [DefensoriaDB.Almacen]
GO
/****** Object:  StoredProcedure [dbo].[SeleccionarOrdenEncabezadoPorRangoDeFechas]    Script Date: 26/02/2015 09:35:37 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Rubén Rodríguez>
-- Create date: <01/26/2015>
-- Description:	<Busca las Ordenes de compra con rangos de fechas>
-- =============================================
ALTER PROCEDURE [dbo].[SeleccionarOrdenEncabezadoPorRangoDeFechas]
	@Clave VARCHAR(10) = '',
	@FechaInicio VARCHAR(30),
	@FechaFin VARCHAR(30)
AS

BEGIN	

SET NOCOUNT ON;

	DECLARE @SeccionEstatusOrden INT = '15',
			@EstatusId INT = 31							-- Estatus de preorden sin orden de compra

	IF @Clave <> ''
	BEGIN
		SELECT 
			   OrdenEncabezado.Clave,
			   [DefensoriaDB.Catalogo].dbo.Empleado.Nombre AS NombreEmpleado,
			   [DefensoriaDB.Seguridad].[dbo].Estatus.Nombre AS Estatus,
			   [DefensoriaDB.Almacen].[dbo].Proveedor.Nombre AS Proveedor,
			   OrdenEncabezado.FechaOrden
		  FROM OrdenEncabezado
		  INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado
		  ON [DefensoriaDB.Catalogo].dbo.Empleado.EmpleadoId = OrdenEncabezado.EmpleadoId
		  INNER JOIN [DefensoriaDB.Seguridad].[dbo].Estatus
		  ON  [DefensoriaDB.Seguridad].[dbo].Estatus.EstatusId = OrdenEncabezado.EstatusId 
		  INNER JOIN [DefensoriaDB.Seguridad].[dbo].[Seccion]
		  ON [DefensoriaDB.Seguridad].[dbo].[Seccion].SeccionId = @SeccionEstatusOrden
		  INNER JOIN [DefensoriaDB.Almacen].[dbo].[Proveedor]
		  ON  [DefensoriaDB.Almacen].[dbo].[Proveedor].ProveedorId = OrdenEncabezado.ProveedorId
		  WHERE (OrdenEncabezado.Clave LIKE  '%'+@Clave+'%') AND 
				(OrdenEncabezado.FechaOrden BETWEEN CAST(@FechaInicio AS smalldatetime) AND CAST(@FechaFin AS smalldatetime))
				AND(OrdenEncabezado.EstatusId = @EstatusId)
			
	END
	

	IF @Clave = ''
	BEGIN
		SELECT 
			   OrdenEncabezado.Clave,
			   [DefensoriaDB.Catalogo].dbo.Empleado.Nombre AS NombreEmpleado,
			   [DefensoriaDB.Seguridad].[dbo].Estatus.Nombre AS Estatus,
			   [DefensoriaDB.Almacen].[dbo].Proveedor.Nombre AS Proveedor,
			   OrdenEncabezado.FechaOrden
		  FROM OrdenEncabezado
		  INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado
		  ON [DefensoriaDB.Catalogo].dbo.Empleado.EmpleadoId = OrdenEncabezado.EmpleadoId
		  INNER JOIN [DefensoriaDB.Seguridad].[dbo].Estatus
		  ON  [DefensoriaDB.Seguridad].[dbo].Estatus.EstatusId = OrdenEncabezado.EstatusId 
		  INNER JOIN [DefensoriaDB.Seguridad].[dbo].[Seccion]
		  ON [DefensoriaDB.Seguridad].[dbo].[Seccion].SeccionId = @SeccionEstatusOrden
		  INNER JOIN [DefensoriaDB.Almacen].[dbo].[Proveedor]
		  ON  [DefensoriaDB.Almacen].[dbo].[Proveedor].ProveedorId = OrdenEncabezado.ProveedorId
		  WHERE (OrdenEncabezado.FechaOrden  BETWEEN CAST(@FechaInicio AS smalldatetime) AND CAST(@FechaFin AS smalldatetime))
				AND(OrdenEncabezado.EstatusId = @EstatusId)
		  
	END

END

