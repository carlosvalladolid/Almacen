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

		SELECT OrdenEncabezadoTemp.OrdenId, OrdenEncabezadoTemp.PreOrdenId, OrdenEncabezadoTemp.EmpleadoId, OrdenEncabezadoTemp.JefeId,
			OrdenEncabezadoTemp.ProveedorId, OrdenEncabezadoTemp.EstatusId, OrdenEncabezadoTemp.Clave AS ClaveOrden, Producto.Clave AS ClaveProducto,
			Producto.Descripcion AS DescripcionProducto, Familia.Nombre AS NombreFamilia, Marca.Nombre AS NombreMarca,
			OrdenDetalleTemp.Cantidad
			FROM OrdenEncabezadoTemp
			INNER JOIN OrdenDetalleTemp
			ON OrdenEncabezadoTemp.OrdenId = OrdenDetalleTemp.OrdenId
			INNER JOIN Producto
			ON OrdenDetalleTemp.ProductoId = Producto.ProductoId
			INNER JOIN SubFamilia
			ON Producto.SubFamiliaId = SubFamilia.SubFamiliaId
			INNER JOIN Familia
			ON SubFamilia.FamiliaId = Familia.FamiliaId
			INNER JOIN Marca
			ON Producto.MarcaId = Marca.MarcaId
			WHERE (OrdenEncabezadoTemp.OrdenId = @OrdenId)

	SET NOCOUNT OFF
