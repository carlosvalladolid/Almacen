-- =============================================
-- Author:		<Rubén Rodríguez>
-- Create date: <01/22/2015>
-- Description:	<Busca las PreOrdenes de compra con rangos de fechas>
-- =============================================
ALTER PROCEDURE [dbo].[SeleccionarPreOrdenEncabezadoPorRangoDeFechas]
	@Clave VARCHAR(10) = '',
	@FechaInicio VARCHAR(30),
	@FechaFin VARCHAR(30)
AS

BEGIN	

SET NOCOUNT ON;

	DECLARE @SeccionEstatusPreOrden INT = '14',			-- Estatus para las preórdenes de compra
			@EstatusId INT = 33							-- Estatus de preorden sin orden de compra

	IF @Clave <> ''
	BEGIN
		SELECT 
			   PreOrdenEncabezado.Clave,
			   [DefensoriaDB.Catalogo].dbo.Empleado.Nombre AS NombreEmpleado,
			   [DefensoriaDB.Seguridad].[dbo].Estatus.Nombre AS Estatus,
			   PreOrdenEncabezado.FechaPreOrden
		  FROM PreOrdenEncabezado
		  INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado
		  ON [DefensoriaDB.Catalogo].dbo.Empleado.EmpleadoId = PreOrdenEncabezado.EmpleadoId
		  INNER JOIN [DefensoriaDB.Seguridad].[dbo].Estatus
		  ON  [DefensoriaDB.Seguridad].[dbo].Estatus.EstatusId = PreOrdenEncabezado.EstatusId 
		  INNER JOIN [DefensoriaDB.Seguridad].[dbo].[Seccion]
		  ON [DefensoriaDB.Seguridad].[dbo].[Seccion].SeccionId = @SeccionEstatusPreOrden
		  WHERE (PreOrdenEncabezado.Clave LIKE  '%'+@Clave+'%') AND 
				(PreOrdenEncabezado.FechaPreOrden BETWEEN CAST(@FechaInicio AS smalldatetime) AND CAST(@FechaFin AS smalldatetime))
				AND(PreOrdenEncabezado.EstatusId = @EstatusId)
			
	END
	

	IF @Clave = ''
	BEGIN
		SELECT 
			   PreOrdenEncabezado.Clave,
			   [DefensoriaDB.Catalogo].dbo.Empleado.Nombre AS NombreEmpleado,
			   [DefensoriaDB.Seguridad].dbo.Estatus.Nombre AS Estatus,
			   PreOrdenEncabezado.FechaPreOrden
		  FROM PreOrdenEncabezado
		  INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado
		  ON [DefensoriaDB.Catalogo].dbo.Empleado.EmpleadoId = PreOrdenEncabezado.EmpleadoId
		  INNER JOIN [DefensoriaDB.Seguridad].dbo.Estatus
		  ON [DefensoriaDB.Seguridad].dbo.Estatus.EstatusId = PreOrdenEncabezado.EstatusId 
		  INNER JOIN [DefensoriaDB.Seguridad].[dbo].[Seccion]
		  ON [DefensoriaDB.Seguridad].[dbo].[Seccion].SeccionId = @SeccionEstatusPreOrden
		  WHERE (PreOrdenEncabezado.FechaPreOrden  BETWEEN CAST(@FechaInicio AS smalldatetime) AND CAST(@FechaFin AS smalldatetime))
			AND(PreOrdenEncabezado.EstatusId = @EstatusId)
		  
	END

END

