-- =============================================
-- Author:		<OLGUCA>
-- Create date: <13/11/2014>
-- Description:	<Genera un registro en la tabla de RecepcionEncabezado>
-- =============================================
ALTER PROCEDURE [dbo].[InsertarRecepcionEncabezadoProcedimiento]
	@RecepcionId varchar(36),
	@OrdenId  varchar(36),
	@EmpleadoId int,
	@JefeId int,
	@ProveedorId smallint,
	@TipoDocumentoId smallint,
	@EstatusId smallint = 1,
	@Clave varchar(10),
	@FechaDocumento smalldatetime,
	@Monto decimal(9,2)
AS
BEGIN

	SET NOCOUNT ON

		insert Into RecepcionEncabezado(RecepcionId, OrdenId, EmpleadoId, JefeId, ProveedorId, TipoDocumentoId, EstatusId,
			Clave, FechaDocumento, Monto)
			values(@RecepcionId, @OrdenId, @EmpleadoId, @JefeId, @ProveedorId, @TipoDocumentoId, @EstatusId, @Clave,
				@FechaDocumento, @Monto)

	SET NOCOUNT OFF

END

