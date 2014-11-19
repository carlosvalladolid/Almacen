/*******************************************************************************************
* NOMBRE:			InsertaProductoOrdenEncabezadoTemp
* FECHA CREACI�N:	18-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCI�N:		Inserta un registro en la tabla temporal de OrdenEncabezadoTemp
*					
* PAR�METROS:		OrdenId					Identificador de la orden de compra temporal
*					PreOrdenId				Identificador de la preorden
*********************************************************************************************/
ALTER PROCEDURE [dbo].[InsertaProductoOrdenEncabezadoTemp]
(
	@OrdenId VARCHAR(36),
	@PreOrdenId VARCHAR(36)
)

AS

	SET NOCOUNT ON

		INSERT INTO OrdenEncabezadoTemp(OrdenId, PreOrdenId, EmpleadoId, JefeId, ProveedorId, EstatusId, Clave, FechaOrden, FechaInserto)
			VALUES(@OrdenId, @PreOrdenId, NULL, NULL, NULL, NULL, NULL, NULL, GETDATE())

	SET NOCOUNT OFF
