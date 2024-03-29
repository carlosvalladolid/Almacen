-- =============================================
-- Author:		<olguca>
-- Create date: <08/12/2014>
-- Description:	<Sirve para Mostrar un registro de la tabla RequisicionDetalleTemp y RequisicionEncabezadoTemp para Modificarlo>
-- =============================================
ALTER PROCEDURE [dbo].[SeleccionarRequisicionDetalleTempProcedimiento]
	(
	@RequisicionId varchar(36),
	@ProductoId varchar(36) = ''
	)
AS
BEGIN
	
	SET NOCOUNT ON;

		DECLARE @ProductoIdGUID uniqueidentifier

		IF (@ProductoId <> '')
			SET @ProductoIdGUID = CAST(@ProductoId AS uniqueidentifier)

		select RequisicionDetalleTemp.RequisicionId,RequisicionDetalleTemp.ProductoId,
		RequisicionDetalleTemp.Cantidad,Producto.Clave ,Producto.Descripcion,
		Familia.FamiliaId,Familia.Nombre as Familia,
		SubFamilia.Nombre as SubFamilia , SubFamilia.SubFamiliaId,
		Marca.Nombre as Marca
	
		from RequisicionDetalleTemp
		Inner join RequisicionEncabezadoTemp on RequisicionDetalleTemp.RequisicionId =  RequisicionEncabezadoTemp.RequisicionId
		Inner join Producto on RequisicionDetalleTemp.ProductoId = Producto.ProductoId
		Inner join SubFamilia On Producto.SubFamiliaId = SubFamilia.SubFamiliaId
		Inner join Familia On SubFamilia.FamiliaId = Familia.FamiliaId
		Inner join Marca On Producto.MarcaId = Marca.MarcaId
		where (RequisicionDetalleTemp.RequisicionId = @RequisicionId)
			AND (RequisicionDetalleTemp.ProductoId = @ProductoIdGUID OR @ProductoId = '')
END
