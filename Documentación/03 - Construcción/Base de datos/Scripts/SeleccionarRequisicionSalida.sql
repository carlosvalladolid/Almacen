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

		DECLARE @RequisicionIdGuid UNIQUEIDENTIFIER,
				@Inicio DATETIME,
				@Fin DATETIME

		IF(@RequisicionId <> '')
			SET @RequisicionIdGuid = CONVERT(VARCHAR(36), @RequisicionId)

		IF(@FechaInicial = '' OR @FechaFinal = '')
			BEGIN
				SELECT RequisicionEncabezado.RequisicionId, RequisicionEncabezado.Clave,
					(Empleado.Nombre + ' ' + Empleado.ApellidoPaterno + ' ' + ISNULL(Empleado.ApellidoMaterno, '')) AS NombreEmpleado,
					Estatus.Nombre AS NombreEstatus, RequisicionEncabezado.FechaInserto, Dependencia.Nombre AS NombreDependencia,
					Direccion.Nombre AS NombreDireccion, Departamento.Nombre AS NombreDepartamento, Puesto.Nombre AS NombrePuesto,
					(EmpleadoJefe.Nombre + ' ' + EmpleadoJefe.ApellidoPaterno + ' ' + ISNULL(EmpleadoJefe.ApellidoMaterno, '')) AS NombreJefe
					FROM RequisicionEncabezado
					INNER JOIN [DefensoriaDB.Seguridad].dbo.Estatus Estatus
					ON RequisicionEncabezado.EstatusId = Estatus.EstatusId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado Empleado
					ON RequisicionEncabezado.EmpleadoId = Empleado.EmpleadoId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Departamento Departamento
					ON Empleado.DepartamentoId = Departamento.DepartamentoId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Direccion Direccion
					ON Departamento.DireccionId = Direccion.DireccionId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Dependencia Dependencia
					ON Direccion.DependenciaId = Direccion.DependenciaId
					INNER JOIN [DefensoriaDB.Catalogo].[dbo].[Puesto] Puesto
					ON Empleado.PuestoId = Puesto.PuestoId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Jefe
					ON Empleado.EmpleadoIdJefe = Jefe.EmpleadoId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado EmpleadoJefe
					ON Jefe.EmpleadoId = EmpleadoJefe.EmpleadoId
					WHERE (RequisicionEncabezado.RequisicionId = @RequisicionIdGuid OR @RequisicionId = '')
					AND ((Empleado.Nombre + ' ' + Empleado.ApellidoPaterno) LIKE '%' + @Empleado + '%' OR @Empleado = '')
					AND (RequisicionEncabezado.EstatusId = @EstatusId OR @EstatusId = 0)
					ORDER BY RequisicionEncabezado.FechaInserto DESC
			END
		ELSE
			BEGIN
				SET @Inicio = @FechaInicial + '00:00:00:000'
				SET @Fin = @FechaFinal + '23:59:59:999'

				SELECT RequisicionEncabezado.RequisicionId, RequisicionEncabezado.Clave,
					(Empleado.Nombre + ' ' + Empleado.ApellidoPaterno + ' ' + ISNULL(Empleado.ApellidoMaterno, '')) AS NombreEmpleado,
					Estatus.Nombre AS NombreEstatus, RequisicionEncabezado.FechaInserto, Dependencia.Nombre AS NombreDependencia,
					Direccion.Nombre AS NombreDireccion, Departamento.Nombre AS NombreDepartamento, Puesto.Nombre AS NombrePuesto,
					(EmpleadoJefe.Nombre + ' ' + EmpleadoJefe.ApellidoPaterno + ' ' + ISNULL(EmpleadoJefe.ApellidoMaterno, '')) AS NombreJefe
					FROM RequisicionEncabezado
					INNER JOIN [DefensoriaDB.Seguridad].dbo.Estatus Estatus
					ON RequisicionEncabezado.EstatusId = Estatus.EstatusId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado Empleado
					ON RequisicionEncabezado.EmpleadoId = Empleado.EmpleadoId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Departamento Departamento
					ON Empleado.DepartamentoId = Departamento.DepartamentoId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Direccion Direccion
					ON Departamento.DireccionId = Direccion.DireccionId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Dependencia Dependencia
					ON Direccion.DependenciaId = Direccion.DependenciaId
					INNER JOIN [DefensoriaDB.Catalogo].[dbo].[Puesto] Puesto
					ON Empleado.PuestoId = Puesto.PuestoId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Jefe
					ON Empleado.EmpleadoIdJefe = Jefe.EmpleadoId
					INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado EmpleadoJefe
					ON Jefe.EmpleadoId = EmpleadoJefe.EmpleadoId
					WHERE (RequisicionEncabezado.RequisicionId = @RequisicionIdGuid OR @RequisicionId = '')
					AND ((Empleado.Nombre + ' ' + Empleado.ApellidoPaterno) LIKE '%' + @Empleado + '%' OR @Empleado = '')
					AND (RequisicionEncabezado.EstatusId = @EstatusId OR @EstatusId = 0)
					AND (RequisicionEncabezado.FechaInserto BETWEEN @Inicio AND @Fin)
					ORDER BY RequisicionEncabezado.FechaInserto DESC
			END

	SET NOCOUNT OFF

