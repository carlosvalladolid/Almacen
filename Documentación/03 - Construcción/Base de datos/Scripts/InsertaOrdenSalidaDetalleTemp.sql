USE [DefensoriaDB.Almacen]
GO
/****** Object:  StoredProcedure [dbo].[InsertaOrdenDetalle]    Script Date: 28/01/2015 12:00:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*******************************************************************************************
* NOMBRE:			InsertaOrdenSalidaDetalleTemp
* FECHA CREACIÓN:	28-Enero-2014
* AUTOR:			Rubén Rodríguez
* DESCRIPCIÓN:		Inserta en la tabla de Orden salida detalle temporal el detalle
*					
* PARÁMETROS:		OrdenId					Identificador de la orden de compra
					ProductoId				Identificador del producto
					Cantidad				Cantidad de productos
*********************************************************************************************/
CREATE PROCEDURE [dbo].[InsertaOrdenSalidaDetalleTemp]
(
	@OrdenSalidaId VARCHAR(36),
	@ProductoId VARCHAR(36),
	@Cantidad INT
)

AS

	SET NOCOUNT ON

		INSERT INTO OrdenSalidaDetalleTemp([OrdenSalidaId],[ProductoId],[Cantidad])
			VALUES (@OrdenSalidaId, @ProductoId, @Cantidad)

	SET NOCOUNT OFF
