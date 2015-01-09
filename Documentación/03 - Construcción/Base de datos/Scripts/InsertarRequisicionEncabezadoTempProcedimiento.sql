-- =============================================
-- Author:		<OLGUCA>
-- Create date: <26/11/2014>
-- Description:	<Genera un registro en la tabla RequisicionEncabezado>
-- =============================================
ALTER PROCEDURE [dbo].[InsertarRequisicionEncabezadoTempProcedimiento]
(
	@RequisicionId varchar(36),
	@EmpleadoId int,
	@JefeId int,
	@EstatusId smallint = 1
)

AS

BEGIN

	SET NOCOUNT ON

		DECLARE @Clave VARCHAR(10)

		INSERT INTO RequisicionClave DEFAULT VALUES

		SET @Clave = SCOPE_IDENTITY()

		insert into RequisicionEncabezadoTemp(RequisicionId, EmpleadoId, JefeId, EstatusId, Clave, FechaInserto)
			values(@RequisicionId, @EmpleadoId, @JefeId, @EstatusId, @Clave, getdate())

	SET NOCOUNT OFF

END
