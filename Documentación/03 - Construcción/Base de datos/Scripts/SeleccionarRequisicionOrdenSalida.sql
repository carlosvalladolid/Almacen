/*******************************************************************************************
* NOMBRE:			SeleccionarRequisicionOrdenSalida
* FECHA CREACI�N:	01-Diciembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCI�N:		Busca la informaci�n de una requisici�n para generar la orden de salida.
*					
* PAR�METROS:		Clave					Folio de la requisici�n.
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
