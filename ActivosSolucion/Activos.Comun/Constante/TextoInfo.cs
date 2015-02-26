using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Comun.Constante
{
  public class TextoInfo: Base
    {
      // Mensajes Genericos
      public const string MensajeGuardadoGenerico = "La información se ha guardado correctamente";
      public const string MensajeBorradoGenerico = "La información se ha eliminado correctamente";

      //Mensajes Formulario PreOrden
      public const string MensajeFechaGenerico = "Por favor, inserte una fecha válida.";
      public const string MensajeSolicitanteGenerico = "Por favor, especifique el solicitante.";
      public const string MensajeCantidadGenerico = "Por favor, especifique la cantidad del producto.";
      public const string MensajeProductoGenerico = "Por favor, especifique los productos de la PreOrden.";
      public const string MensajeClaveGenerico = "Por favor, especifique los productos de la PreOrden.";
      public const string MensajeNoPreOrden = "No. PreOrden: ";
      public const string MensajeConfirmPreOrden = "¿Desea guardar la PreOrden?";

      //Mensajes Formulario Requisicion
      public const string MensajeNoRequisicion= "No. Requisicion: ";
      public const string MensajeNoHayExistenciaDe = "No hay suficientes articulos para el producto: ";

      //Mensajes Formulario Orden
      public const string MensajeNoOrden= "No. Orden: ";
      public const string MensajeSeleccioneEmpleado = "Por favor, especifique el empleado.";
      public const string MensajeSeleccioneProveedor = "Por favor, especifique el proveedor.";
      public const string MensajeEmpleadosVacio = "No existen empleados en la lista.";
      public const string MensajeProveedoresVacio = "No existen proveedores en la lista.";
      public const string MensajeOrdenVacia = "Por favor, seleccione los articulos de la orden.";
      public const string MensajeConfirmacionOrden = "¿Desea guardar la Orden?";
      public const string MensajeOrdenEstatusSurtida = "Esta orden de compra ya fue surtida.";

      //Mensajes Formulario Recepcion
      public const string MensajeNoRecepcion = "No. Recepción: ";
      public const string MensajeTipoDocumentoVacio = "No existen tipos de documentos.";
      public const string MensajeSeleccioneTipoDocumento = "Por favor, especifique el tipo de documento.";
      public const string MensajeFolioVacio = "Por favor, capture un folio.";
      public const string MensajePrecioInvalido = "Por favor, especifique un precio válido.";
      public const string MensajeMontoInvalido = "Por favor, especifique un monto válido";
      public const string MensajeRecepcionVacia = "Por favor, especifique los productos de la recepción.";
      public const string MensajeMontosNoConcuerdan = "El monto de la factura no corresponde al la suma del monto de los articulos.";
      public const string MensajeFechaVencimiento = "Por favor, especifique una fecha de Vencimiento ";
      public const string MensajeFechaDocumento = "Por favor, especifique una fecha del Documento ";

      //Mensajes Formulario Orden Salida
      public const string MensajeNoOrdenSalida = "No. Orden Salida: ";
      public const string MensajeConfirmOrdenSalida = "¿Desea guardar la Orden de Salida?";
      public const string MensajeRangoFechasInvalido = "Por favor, indique un rango de fechas valido.";
      public const string MensajeLimpiarFormulario = "¿Desea limpiar el formulario?";
      public const string MensajeSeleccioneRequisicion = "Por favor, seleccione una requisicion.";

         public const string MensajeMaximoCompra = "Se excede la compra Maxima";

         public const string MensajeEliminacionExitosa = "Elemento eliminado de forma satisfactoria.";
      // Mensaje Informativos de Requisición

    }
}
