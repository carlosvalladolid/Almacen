/*******************************************************************************************
* NOMBRE:			InsertaOrdenEncabezado
* FECHA CREACIÓN:	20-Noviembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCIÓN:		Copia la información de una orden de compra de la tabla temporal a la tabla definitiva
*					
* PARÁMETROS:		OrdenId					Identificador de la orden de compra
*					EmpleadoId				Identificador del empleado
*					JefeId					Identificador del jefe
*					ProveedorId				Identificador del proveedor
*					EstatusId				Identificador del estatus
*					FechaOrden				Fecha de la orden de compra
*					
*********************************************************************************************/
ALTER PROCEDURE [dbo].[InsertaOrdenEncabezado]
(
	@OrdenId VARCHAR(36),
	@EmpleadoId VARCHAR(36),
	@JefeId VARCHAR(36),
	@ProveedorId VARCHAR(36),
	@EstatusId VARCHAR(36),
	@FechaOrden VARCHAR(10)
)

AS

	SET NOCOUNT ON

		DECLARE @Clave VARCHAR(10)

		INSERT INTO OrdenClave DEFAULT VALUES

		SET @Clave = SCOPE_IDENTITY()

		INSERT INTO OrdenEncabezado(OrdenId, PreOrdenId, EmpleadoId, JefeId, ProveedorId, EstatusId, Clave, FechaOrden, FechaInserto)
			SELECT OrdenId, PreOrdenId, @EmpleadoId, @JefeId, @ProveedorId, @EstatusId, @Clave, @FechaOrden, FechaInserto
				FROM OrdenEncabezadoTemp
				WHERE (OrdenId = @OrdenId)

	SET NOCOUNT OFF
