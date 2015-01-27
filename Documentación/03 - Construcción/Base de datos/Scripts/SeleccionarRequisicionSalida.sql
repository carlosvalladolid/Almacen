/*******************************************************************************************
* NOMBRE:			SeleccionarRequisicionSalida
* FECHA CREACIÓN:	23-Enero-2015
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Busca información de las requisiciones que coincidan con los parámetros enviados.
*					
* PARÁMETROS:		RequisicionId				Identificador de la requisición
*					Empleado					Nombre del empleado
*					FechaInicial				Fecha inicial del rango de búsqueda
*					FechaFinal					Fecha final del rango de búsqueda
*					EstatusId					Estatus de la requisición
*					
*********************************************************************************************/
ALTER PROCEDURE [dbo].[SeleccionarRequisicionSalida]
(
	@RequisicionId VARCHAR(36) = '',
	@Empleado VARCHAR(50) = '',
	@FechaInicial VARCHAR(10) = '',
	@FechaFinal VARCHAR(10) = '',
	@EstatusId SMALLINT = 0
)

AS

	SET NOCOUNT ON

		DECLARE @RequisicionIdGuid UNIQUEIDENTIFIER

		IF(@RequisicionId <> '')
			SET @RequisicionIdGuid = CONVERT(VARCHAR(36), @RequisicionId)

		SELECT RequisicionEncabezado.RequisicionId, RequisicionEncabezado.Clave,
			(Empleado.Nombre + ' ' + Empleado.ApellidoPaterno + ' ' + ISNULL(Empleado.ApellidoMaterno, '')) AS NombreEmpleado,
			Estatus.Nombre AS NombreEstatus, RequisicionEncabezado.FechaInserto
			FROM RequisicionEncabezado
			INNER JOIN RequisicionDetalle
			ON RequisicionEncabezado.RequisicionId = RequisicionDetalle.RequisicionId
			INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado Empleado
			ON RequisicionEncabezado.EmpleadoId = Empleado.EmpleadoId
			INNER JOIN [DefensoriaDB.Seguridad].dbo.Estatus Estatus
			ON RequisicionEncabezado.EstatusId = Estatus.EstatusId
			WHERE (RequisicionEncabezado.RequisicionId = @RequisicionIdGuid OR @RequisicionId = '')
			AND ((Empleado.Nombre + ' ' + Empleado.ApellidoPaterno) LIKE '%' + @Empleado + '%' OR @Empleado = '')
			AND (RequisicionEncabezado.EstatusId = @EstatusId OR @EstatusId = 0)
			GROUP BY RequisicionEncabezado.RequisicionId, RequisicionEncabezado.Clave, Empleado.Nombre, Empleado.ApellidoPaterno,
			Empleado.ApellidoMaterno, Estatus.Nombre, RequisicionEncabezado.FechaInserto
			ORDER BY RequisicionEncabezado.FechaInserto DESC

	SET NOCOUNT OFF

