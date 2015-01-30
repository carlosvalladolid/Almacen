/*
Script para carga inicial de datos
*/

-- Secci�n
INSERT INTO [DefensoriaDB.Seguridad].dbo.Seccion(SeccionId, Nombre, Descripcion)
	VALUES(14, 'PreOrden', 'Secci�n de preorden de compra en Almac�n')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Seccion(SeccionId, Nombre, Descripcion)
	VALUES(15, 'Orden', 'Secci�n de �rdenes de compra en Almac�n')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Seccion(SeccionId, Nombre, Descripcion)
	VALUES(16, 'Requerimiento', 'Secci�n de requerimientos en Almac�n')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Seccion(SeccionId, Nombre, Descripcion)
	VALUES(17, 'OrdenSalida', 'Seccion de �rdenes de salida')


-- Estatus
INSERT INTO [DefensoriaDB.Seguridad].dbo.Estatus(EstatusId, SeccionId, Nombre, Descripcion)
	VALUES(29, 14, 'OC Completa', 'PreOrden completa')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Estatus(EstatusId, SeccionId, Nombre, Descripcion)
	VALUES(30, 14, 'OC Incompleta', 'PreOrden incompleta')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Estatus(EstatusId, SeccionId, Nombre, Descripcion)
	VALUES(31, 15, 'Sin surtir', 'Orden de compra que no ha sido surtida')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Estatus(EstatusId, SeccionId, Nombre, Descripcion)
	VALUES(32, 16, 'Incompleta', 'Requisici�n incompleta')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Estatus(EstatusId, SeccionId, Nombre, Descripcion)
	VALUES(33, 14, 'Sin OC', 'Sin Orden de Compra')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Estatus(EstatusId, SeccionId, Nombre, Descripcion)
	VALUES(34, 17, 'Cancelada', 'Orden de salida cancelada')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Estatus(EstatusId, SeccionId, Nombre, Descripcion)
	VALUES(35, 17, 'Salida completa', 'Orden de salida completa')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Estatus(EstatusId, SeccionId, Nombre, Descripcion)
	VALUES(36, 17, 'Salida incompleta', 'Orden de salida enviada pero incompleta')

INSERT INTO [DefensoriaDB.Seguridad].dbo.Estatus(EstatusId, SeccionId, Nombre, Descripcion)
	VALUES(37, 15, 'Surtida', 'Orden de compra surtida')


-- Tipo de documento
/*INSERT INTO [DefensoriaDB.Almacen].dbo.TipoDocumento(TipoDocumentoId)
	VALUES(1, 'Factura')

INSERT INTO [DefensoriaDB.Almacen].dbo.TipoDocumento(TipoDocumentoId)
	VALUES(2, 'Remisi�n')

INSERT INTO [DefensoriaDB.Almacen].dbo.TipoDocumento(TipoDocumentoId)
	VALUES(3, 'Otro')*/



