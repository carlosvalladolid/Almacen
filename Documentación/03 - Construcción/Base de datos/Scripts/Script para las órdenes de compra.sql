/*
Script para las órdenes de compra

DELETE OrdenDetalleTemp
DELETE OrdenEncabezadoTemp
DELETE OrdenDetalle
DELETE OrdenEncabezado
*/

SELECT * FROM OrdenEncabezadoTemp
	ORDER BY FechaInserto DESC

SELECT * FROM OrdenDetalleTemp

SELECT * FROM OrdenEncabezado

SELECT * FROM OrdenDetalle

SELECT * FROM PreOrdenEncabezado

SELECT * FROM PreOrdenDetalle

SELECT * FROM Producto

SELECT * FROM Familia

SELECT * FROM SubFamilia

SELECT * FROM Marca

SELECT * FROM OrdenClave

SELECT * FROM [DefensoriaDB.Catalogo].dbo.Proveedor

SELECT * FROM [DefensoriaDB.Seguridad].dbo.Estatus

SELECT * FROM [DefensoriaDB.Seguridad].dbo.Seccion

SELECT NEWID()

-- 7E4A6981-D5CA-49E9-B4A8-6D1EDE80EACA
SeleccionarPreOrdenDetalleSinOrden '25', '7E4A6981-D5CA-49E9-B4A8-6D1EDE80EACA'

