/*******************************************************************************************
* NOMBRE:			SeleccionarOrdenDetalleTemp
* FECHA CREACIÓN:	18-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Realiza una búsqueda del detalle de una orden de compra temporal.
*					
* PARÁMETROS:		OrdenId					Identificador de la orden de compra temporal
*********************************************************************************************/
ALTER PROCEDURE [dbo].[SeleccionarOrdenDetalleTemp]
(
	@OrdenId VARCHAR(36)
)

AS

	SET NOCOUNT ON

		SELECT OrdenEncabezadoTemporal.OrdenId, OrdenEncabezadoTemporal.PreOrdenId, OrdenEncabezadoTemporal.EmpleadoId,
			OrdenEncabezadoTemporal.JefeId, OrdenEncabezadoTemporal.ProveedorId, OrdenEncabezadoTemporal.EstatusId,
			OrdenEncabezadoTemporal.Clave AS ClaveOrden, OrdenEncabezadoTemporal, Producto.Clave AS ClaveProducto,
			Producto.Descripcion AS DescripcionProducto, Familia.Nombre AS NombreFamilia, Marca.Nombre AS NombreMarca,
			OrdenDetalleTemporal.Cantidad
			FROM OrdenEncabezadoTemporal
			INNER JOIN OrdenDetalleTemporal
			ON OrdenEncabezadoTemporal.OrdenId = OrdenDetalleTemporal.OrdenId
			INNER JOIN Producto
			ON OrdenDetalleTemporal.ProductoId = Producto.ProductoId
			INNER JOIN SubFamilia
			ON Producto.SubFamiliaId = SubFamilia.SubFamiliaId
			INNER JOIN Familia
			ON SubFamilia.FamiliaId = Familia.FamiliaId
			INNER JOIN Marca
			ON Producto.MarcaId = Marca.MarcaId
			WHERE (OrdenEncabezadoTemporal.OrdenId = @OrdenId)

	SET NOCOUNT OFF
