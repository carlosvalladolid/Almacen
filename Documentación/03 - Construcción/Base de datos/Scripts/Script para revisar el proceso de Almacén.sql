/*
Script para revisar el proceso de Almacén
*/

-- Catálogos
SELECT * FROM SubFamilia

SELECT * FROM Familia

SELECT * FROM Proveedor

SELECT * FROM Marca

SELECT * FROM Producto

SELECT * FROM [DefensoriaDB.Seguridad].[dbo].[Estatus]
SELECT * FROM [DefensoriaDB.Seguridad].[dbo].[Seccion]

SELECT * FROM [DefensoriaDB.Catalogo].[dbo].[Empleado]


-- Operación
SELECT * FROM RequisicionEncabezado

SELECT * FROM RequisicionDetalle

SELECT * FROM PreOrdenEncabezado

SELECT * FROM PreOrdenDetalle

SELECT * FROM OrdenEncabezado

SELECT * FROM OrdenDetalle

SELECT * FROM RecepcionEncabezado

SELECT * FROM RecepcionDetalle

--SELECT * FROM OrdenSalidaEncabezado

SELECT * FROM OrdenSalidaDetalle

SELECT * FROM OrdenClave

SELECT * FROM Existencia


-- Tablas temporales
SELECT * FROM RequisicionEncabezadoTemp

SELECT * FROM RequisicionDetalleTemp

SELECT * FROM PreOrdenEncabezadoTemp

SELECT * FROM PreOrdenDetalleTemp

SELECT * FROM OrdenEncabezadoTemp

SELECT * FROM OrdenDetalleTemp

SeleccionarRequisicionSalida

--SELECT * FROM RecepcionEncabezadoTemp

--SELECT * FROM RecepcionDetalleTemp

--SELECT * FROM OrdenSalidaEncabezadoTemp

--SELECT * FROM OrdenSalidaDetalleTemp


-- Limpiar tablas
/*
DELETE RequisicionEncabezado
DELETE RequisicionDetalle
DELETE PreOrdenEncabezado
DELETE PreOrdenDetalle
DELETE OrdenEncabezado
DELETE OrdenDetalle
DELETE RecepcionEncabezado
DELETE RecepcionDetalle
--DELETE OrdenSalidaEncabezado
DELETE OrdenSalidaDetalle

DELETE RequisicionEncabezadoTemp
DELETE RequisicionDetalleTemp
DELETE PreOrdenEncabezadoTemp
DELETE PreOrdenDetalleTemp
DELETE OrdenEncabezadoTemp
DELETE OrdenDetalleTemp
*/
