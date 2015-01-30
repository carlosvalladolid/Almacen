-- =============================================
-- Author:		<olguca>
-- Create date: <03/11/2014>
-- Description:	<Filtrar por eampo llave de producto c>
-- =============================================
ALTER PROCEDURE [dbo].[SeleccionarProductoparaEditarProcedimiento]
(
	@ProductoId varchar(36)=''
)
AS
	
SET NOCOUNT ON

	SELECT Producto.ProductoId, Producto.Clave, Producto.Descripcion AS NombreProducto, Producto.EstatusId, Producto.Minimo, Producto.Maximo,
		Producto.MaximoPermitido, Producto.UnidadMedidaId, SubFamilia.Nombre AS SubFamilia, Familia.Nombre AS Familia, Familia.FamiliaId, SubFamilia.SubFamiliaId,
		Marca.Nombre as Marca, Marca.MarcaId
		FROM Producto 
		INNER JOIN SubFamilia On Producto.SubFamiliaId = SubFamilia.SubFamiliaId
		INNER JOIN Familia On SubFamilia.FamiliaId = Familia.FamiliaId
		LEFT JOIN Marca On Producto.MarcaId = Marca.MarcaId
		WHERE Producto.ProductoId=@ProductoId

SET NOCOUNT OFF
