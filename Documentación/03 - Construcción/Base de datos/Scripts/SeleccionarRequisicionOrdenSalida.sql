/*******************************************************************************************
* NOMBRE:			SeleccionarRequisicionOrdenSalida
* FECHA CREACIÓN:	01-Diciembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Busca la información de una requisición para generar la orden de salida.
*					
* PARÁMETROS:		Clave					Folio de la requisición.
*
*********************************************************************************************/
ALTER PROCEDURE [dbo].[SeleccionarRequisicionOrdenSalida]
(
	@Clave VARCHAR(10) = ''
)

AS

	SET NOCOUNT ON

		SELECT RequisicionEncabezado.RequisicionId, RequisicionEncabezado.EmpleadoId, RequisicionEncabezado.JefeId, RequisicionEncabezado.EstatusId,
			RequisicionEncabezado.Clave, RequisicionDetalle.ProductoId, RequisicionDetalle.Cantidad, Producto.Descripcion AS NombreProducto
			FROM RequisicionEncabezado
			INNER JOIN RequisicionDetalle
			ON RequisicionEncabezado.RequisicionId = RequisicionDetalle.RequisicionId
			INNER JOIN Producto
			ON RequisicionDetalle.ProductoId = Producto.ProductoId
			INNER JOIN
			(
				SELECT EmpleadoId, (Nombre + ' ' + ApellidoPaterno + ' ' + ISNULL(ApellidoMaterno, '')) AS NombreEmpleado
					FROM [DefensoriaDB.Catalogo].[dbo].Empleado
			) Empleado
			ON RequisicionEncabezado.EmpleadoId = Empleado.EmpleadoId
			WHERE (RequisicionEncabezado.Clave = @Clave)

	SET NOCOUNT OFF
