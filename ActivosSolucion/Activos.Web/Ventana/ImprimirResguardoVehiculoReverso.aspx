<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimirResguardoVehiculoReverso.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirResguardoVehiculoReverso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/Incluir/Estilo/Privado/Impresion.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="DivPrincipal">
      <div id="DivCuerpo">
         <div class="DivContenido">
            <div class="DivInformacionContenido">
               <asp:Panel ID="Panel1" runat="server" Visible="true">
                   <div class="DivVehiculoReverso">
                     <table class="TablaVehiculoReverso">
                        <tr class="FilaTitulo">
                           <td colspan="5">APRECIACIÓN VISUAL DE CONDICIONES DEL VEHICULO</td>
                        </tr>
                        <tr class="FilaCentrada">
                           <td>&nbsp;</td>
                           <td class="CampoChico">BUENO</td>
                           <td class="CampoChico">REGULAR</td>
                           <td class="CampoChico">MALO</td>
                           <td class="CampoGrande" rowspan="12">
                              <img alt="Catálogos" src="/Imagen/Foto/vehiculo.png" />
                           </td>
                        </tr>
                        <tr class="FilaChica">
                           <td>Carrocería</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                        </tr>
                        <tr class="FilaChica">
                           <td>Tapicería</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                        </tr>
                        <tr class="FilaChica">
                           <td>Llantas</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                        </tr>
                        <tr class="FilaChica">
                           <td>Defensas</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                        </tr>
                        <tr class="FilaChica">
                           <td>Pintura</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                        </tr>
                        <tr>
                           <td colspan="4"></td>
                        </tr>
                        <tr>
                           <td colspan="4">DOCUMENTACIÓN VIGENTE OBLIGATORIA</td>
                        </tr>
                        <tr class="FilaCentrada">
                           <td colspan="2">&nbsp;</td>
                           <td class="CampoChico">SI</td>
                           <td class="CampoChico">NO</td>
                        </tr>
                        <tr class="FilaChica">
                           <td colspan="2">Licencia</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                        </tr>
                        <tr class="FilaChica">
                           <td colspan="2">Tarjeta de Circulación</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                        </tr>
                        <tr class="FilaChica">
                           <td colspan="2">Póliza de Seguros</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                           <td class="CampoChicoBorde">&nbsp;</td>
                        </tr>
                     </table>
                   </div>
                </asp:Panel>
            </div> 
         </div> 
      </div> 
    </div>
    
     
    </form>
    <script language="javascript" type="text/javascript">

     function ImprimirPantalla() {
         window.print();
     }

     ImprimirPantalla();
     
   </script>
</body>
</html>
