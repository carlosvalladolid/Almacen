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

      //Mensajes Formulario Orden
      public const string MensajeNoOrden= "No. Orden: ";
      public const string MensajeSeleccioneEmpleado = "Por favor, especifique el empleado.";
      public const string MensajeSeleccioneProveedor = "Por favor, especifique el proveedor.";
      public const string MensajeEmpleadosVacio = "No existen empleados en la lista.";
      public const string MensajeProveedoresVacio = "No existen proveedores en la lista.";
      public const string MensajeOrdenVacia = "Por favor, seleccione los articulos de la orden.";
      public const string MensajeConfirmacionOrden = "¿Desea guardar la Orden?";


      public const string MensajeRangoFechasInvalido = "Por favor, indique un rango de fechas valido.";
      public const string MensajeLimpiarFormulario = "¿Desea limpiar el formulario?";
      // Mensaje Informativos de Requisición
    }
}
