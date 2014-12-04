/*******************************************************************************************
* NOMBRE:			SeleccionarPreOrdenDetalleSinOrden
* FECHA CREACIÓN:	21-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Busca los productos de una preorden que todavía no es relacionada a una orden de compra temporal
*					
* PARÁMETROS:		Clave					Folio de la preorden de compra
*					SesionId				Identificador de la sesión de usuario
*
*********************************************************************************************/
CREATE PROCEDURE [dbo].[SeleccionarPreOrdenDetalleSinOrden]
(
	@Clave VARCHAR(10),
	@SesionId VARCHAR(36)
)

AS

SET NOCOUNT ON

		SELECT PreOrdenEncabezado.Clave AS ClavePreOrden, PreOrdenDetalle.PreOrdenId, PreOrdenDetalle.ProductoId, PreOrdenDetalle.Cantidad,
			Producto.Clave AS ClaveProducto, Producto.Descripcion AS NombreProducto, SubFamilia.Nombre As NombreSubFamilia,
			Familia.Nombre AS NombreFamilia, Marca.Nombre As NombreMarca
			FROM PreOrdenEncabezado
			INNER JOIN PreOrdenDetalle
			ON PreOrdenEncabezado.PreOrdenId = PreOrdenDetalle.PreOrdenId
			INNER JOIN Producto
			ON PreOrdenDetalle.ProductoId = Producto.ProductoId
			INNER JOIN SubFamilia
			ON Producto.SubFamiliaId = SubFamilia.SubFamiliaId
			INNER JOIN Familia
			ON SubFamilia.FamiliaId = Familia.FamiliaId
			INNER JOIN Marca
			ON Producto.MarcaId = Marca.MarcaId
			LEFT JOIN
			(
				SELECT OrdenEncabezadoTemp.OrdenId, OrdenEncabezadoTemp.PreOrdenId, OrdenDetalleTemp.ProductoId
					FROM OrdenEncabezadoTemp
					INNER JOIN OrdenDetalleTemp
					ON OrdenEncabezadoTemp.OrdenId = OrdenDetalleTemp.OrdenId
					WHERE (OrdenEncabezadoTemp.SesionId = @SesionId)
			) Orden
			ON PreOrdenEncabezado.PreOrdenId = Orden.PreOrdenId
			AND PreOrdenDetalle.ProductoId = Orden.ProductoId
			WHERE (PreOrdenEncabezado.Clave = @Clave)
			AND (Orden.OrdenId IS NULL)

SET NOCOUNT OFF
