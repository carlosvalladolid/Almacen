-- =============================================
-- Author:		<OLGUCA>
-- Create date: <26/11/2014>
-- Description:	<Genera un registro en la tabla RequisicionEncabezado>
-- =============================================
ALTER PROCEDURE [dbo].[InsertarRequisicionEncabezadoProcedimiento]
(
	@RequisicionId varchar(36)
)

AS

BEGIN

	SET NOCOUNT ON

	insert into RequisicionEncabezado(RequisicionId, EmpleadoId, JefeId, EstatusId, Clave, FechaInserto)
		select RequisicionId, EmpleadoId, JefeId, EstatusId, Clave, getdate()
			from RequisicionEncabezadoTemp
			WHERE RequisicionId = @RequisicionId

	SET NOCOUNT OFF

END
