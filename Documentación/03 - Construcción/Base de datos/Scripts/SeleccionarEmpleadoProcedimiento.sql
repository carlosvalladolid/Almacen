/*******************************************************************************************
* NOMBRE:			SeleccionarEmpleadoProcedimiento
* FECHA CREACIÓN:	26/11/2014
* AUTOR:			OLGUCA
* MODIFICACIÓN		Carlos Valladolid (29-Enero-2015)
* DESCRIPCIÓN:		Busca un empleado con los valores enviados como parámetros
*					
* PARÁMETROS:		
*					EmpleadoId			Identificador del empleado
*					
*********************************************************************************************/

ALTER PROCEDURE [dbo].[SeleccionarEmpleadoProcedimiento]
(
	@EmpleadoId int
)
AS

	SET NOCOUNT ON

		SELECT Empleado.EmpleadoId,  Empleado.DepartamentoId,Empleado.EmpleadoIdJefe,
			Empleado.PuestoId,
			(Empleado.Nombre + ' ' + Empleado.ApellidoPaterno + ' ' + ISNULL(Empleado.ApellidoMaterno, '')) AS Nombre,
			EmpleadoJefe.Nombre + ' ' + EmpleadoJefe.ApellidoPaterno AS EmpleadoJefe,
			Puesto.Nombre AS Puesto, Direccion.DireccionId, Direccion.Nombre AS Direccion,
			Dependencia.nombre as Dependencia
			FROM  [DefensoriaDB.Seguridad].dbo.Usuario Usuario
			INNER JOIN [DefensoriaDB.Catalogo].[dbo].Empleado
			ON Usuario.UsuarioId = Empleado.UsuarioId
			INNER JOIN [DefensoriaDB.Catalogo].dbo.Departamento Departamento
			ON Empleado.DepartamentoId = Departamento.DepartamentoId
			INNER JOIN [DefensoriaDB.Catalogo].dbo.Direccion Direccion
			ON Departamento.DireccionId = Direccion.DireccionId
			INNER JOIN [DefensoriaDB.Catalogo].dbo.Dependencia Dependencia
			ON Direccion.DependenciaId = Direccion.DependenciaId
			INNER JOIN [DefensoriaDB.Catalogo].[dbo].[Puesto] Puesto
			ON Empleado.PuestoId = Puesto.PuestoId
			INNER JOIN [DefensoriaDB.Catalogo].dbo.Jefe
			ON Empleado.EmpleadoIdJefe = Jefe.EmpleadoId
			INNER JOIN [DefensoriaDB.Catalogo].dbo.Empleado EmpleadoJefe
			ON Jefe.EmpleadoId = EmpleadoJefe.EmpleadoId
			WHERE Empleado.EmpleadoId = @EmpleadoId
			ORDER BY Empleado.Nombre

	SET NOCOUNT OFF
