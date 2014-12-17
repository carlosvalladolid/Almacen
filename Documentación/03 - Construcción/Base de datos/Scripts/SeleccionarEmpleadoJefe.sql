/*******************************************************************************************
* NOMBRE:			SeleccionarEmpleadoJefe
* FECHA CREACI�N:	15-Diciembre-2014
* AUTOR:			Carlos Valladolid
* DESCRIPCI�N:		Busca el jefe del empleado enviado como par�metro
*					
* PAR�METROS:		EmpleadoId				Identificador del empleado
*
*********************************************************************************************/
CREATE PROCEDURE [dbo].[SeleccionarEmpleadoJefe]
(
	@EmpleadoId VARCHAR(36)
)

AS

SET NOCOUNT ON

		SELECT (Jefe.Nombre + ' ' + Jefe.ApellidoPaterno) AS NombreJefe
			FROM Empleado
			INNER JOIN Empleado AS Jefe
			ON Empleado.EmpleadoIdJefe = Jefe.EmpleadoId
			WHERE (Empleado.EmpleadoId = @EmpleadoId)

SET NOCOUNT OFF
