<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirResguardoVehiculo.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirResguardoVehiculo" %>
<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaImpresion.Master" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   
</asp:Content>
<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivContenido">
      <div class="DivInformacionContenido">
         
         <asp:Panel ID="EncabezadoPanel" Height="40px" runat="server">
            <div class="DivVehiculoTitulo">
               RESGUARDO DE VEHÍCULO
            </div>
            <div class="DivVehiculoFecha">
               <div class="DivVehiculoFechaConBorde">
                  <table class="TablaVehiculoFecha">
                     <tr>
                        <td class="CampoVehiculoFecha">FECHA:</td>
                        <td class="CampoVehiculoFecha"><asp:Label ID="FechaLabel" runat="server"></asp:Label></td>
                     </tr>
                     <tr>
                        <td class="CampoVehiculoFecha"><asp:Label ID="TipoDocumentoLabel" runat="server"></asp:Label>:</td>
                        <td class="CampoVehiculoFechaSubrrayado"><asp:Label ID="FolioLabel" runat="server"></asp:Label></td>
                     </tr>
                  </table>
               </div>
            </div>
         </asp:Panel>
         
         <div class="DivSaltoLinea">&nbsp;</div>
         
         <div class="DivVehiculoSeccion">
            <table class="TablaVehiculo">
               <tr>
                  <td class="Grande">DEPENDENCIA</td>
                  <td class="Subrrayado"><asp:Label ID="DependenciaLabel" runat="server"></asp:Label></td>
                  <td class="Espacio">&nbsp;</td>
                  <td class="Chica">&nbsp;</td>
                  <td class="CampoMediano">&nbsp;</td>
                  <td class="Espacio">&nbsp;</td>
                  <td class="Mediana">&nbsp;</td>
                  <td class="CampoMediano">&nbsp;</td>
               </tr>
               <tr>
                  <td class="Grande">DIRECCIÓN</td>
                  <td class="Subrrayado"><asp:Label ID="DireccionLabel" runat="server"></asp:Label></td>
                  <td class="Espacio">&nbsp;</td>
                  <td class="Chica">&nbsp;</td>
                  <td class="CampoMediano">&nbsp;</td>
                  <td class="Espacio">&nbsp;</td>
                  <td class="Mediana">Organismo</td>
                  <td class="CampoMedianoSubrrayado">X</td>
               </tr>
               <tr>
                  <td class="Grande">TITULAR DEL ÁREA</td>
                  <td class="Subrrayado"><asp:Label ID="TitularAreaLabel" runat="server"></asp:Label></td>
                  <td class="Espacio">&nbsp;</td>
                  <td class="Chica">R.F.C.</td>
                  <td class="CampoMedianoSubrrayado"><asp:Label ID="RFCTitularAreaLabel" runat="server"></asp:Label></td>
                  <td class="Espacio">&nbsp;</td>
                  <td class="Mediana">No. de Empleado</td>
                  <td class="CampoMedianoSubrrayado"><asp:Label ID="NumeroEmpleadoTitularAreaLabel" runat="server"></asp:Label></td>
               </tr>
               <tr>
                  <td class="Grande">RESPONSABLE DEL VEHÍCULO</td>
                  <td class="Subrrayado"><asp:Label ID="ResponsableVehiculoLabel" runat="server"></asp:Label></td>
                  <td class="Espacio">&nbsp;</td>
                  <td class="Chica">R.F.C.</td>
                  <td class="CampoMedianoSubrrayado"><asp:Label ID="RFCResponsableVehiculoLabel" runat="server"></asp:Label></td>
                  <td class="Espacio">&nbsp;</td>
                  <td class="Mediana">No. de Empleado</td>
                  <td class="CampoMedianoSubrrayado"><asp:Label ID="NumeroEmpleadoResponsableVehiculoLabel" runat="server"></asp:Label></td>
               </tr>
            </table>
         </div>
         
         <div class="DivSaltoLinea">&nbsp;</div>
         
         <div class="DivVehiculoSeccion">
            <table class="TablaCaracteristicas">
               <tr>
                  <td class="CampoCarac">MARCA:&nbsp;<asp:Label ID="MarcaLabel" runat="server"></asp:Label></td>
                  <td class="CampoCarac">TIPO:&nbsp;<asp:Label ID="TipoLabel" runat="server"></asp:Label></td>
                  <td class="CampoCarac">MODELO:&nbsp;<asp:Label ID="ModeloLabel" runat="server"></asp:Label></td>
                  <td class="CampoCarac">COLOR:&nbsp;<asp:Label ID="ColorLabel" runat="server"></asp:Label></td>
               </tr>
            </table>
         </div>
         
         <div class="DivSaltoLinea">&nbsp;</div>
         
         <div class="DivVehiculoSeccion">
            <table class="DivVehiculoAccesorios">
               <tr>
                  <td class="NombreVehiculoAccMediano">&nbsp;</td>
                  <td>&nbsp;</td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccChico">&nbsp;</td>
                  <td class="AccesorioCampo">&nbsp;</td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccGrandeCentrado">ACCESORIOS</td>
                  <td class="AccesorioCampo">&nbsp;</td>
               </tr>
               <tr>
                  <td class="NombreVehiculoAccMediano">NUM. ECO (C.B.):</td>
                  <td class="AccesorioSubrrayado"><asp:Label ID="CodigoBarrasLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccChico">CLIMA:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="ClimaLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccGrande">CRUCETA "L"</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="CrucetaLLabel" runat="server"></asp:Label></td>
               </tr>
               <tr>
                  <td class="NombreVehiculoAccMediano">PLACAS:</td>
                  <td class="AccesorioSubrrayado"><asp:Label ID="PlacasLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccChico">RADIO:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="RadioLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccGrande">HERRAMIENTA:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="HerramientaLabel" runat="server"></asp:Label></td>
               </tr>
               <tr>
                  <td class="NombreVehiculoAccMediano">MOTOR:</td>
                  <td class="AccesorioSubrrayado"><asp:Label ID="MotorLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccChico">ANTENA:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="AntenaLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccGrande">JGO. LUCES PREVENTIVAS:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="LucesPreventivasLabel" runat="server"></asp:Label></td>
               </tr>
               <tr>
                  <td class="NombreVehiculoAccMediano">SERIE:</td>
                  <td class="AccesorioSubrrayado"><asp:Label ID="SerieLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccChico">EXTINGUIDOR:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="ExtinguidorLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccGrande">JGO. CABLES PASACORRIENTE:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="CablesPasaCorrienteLabel" runat="server"></asp:Label></td>
               </tr>
               <tr>
                  <td class="NombreVehiculoAccMediano">KILOMETRAJE:</td>
                  <td class="AccesorioSubrrayado"><asp:Label ID="KilometrajeLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccChico">REFACCIÓN:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="RefaccionLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccGrande">TARJETA DE ESTACIONAMIENTO:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="TarjetaEstacionamientoLabel" runat="server"></asp:Label></td>
               </tr>
               <tr>
                  <td class="NombreVehiculoAccMediano">PLACA ANTERIOR:</td>
                  <td class="AccesorioSubrrayado"><asp:Label ID="PlacaAnteriorLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccChico">GATO:</td>
                  <td class="CampoAccSubrrayado"><asp:Label ID="GatoLabel" runat="server"></asp:Label></td>
                  <td class="VehiculoEspacio">&nbsp;</td>
                  <td class="NombreVehiculoAccGrande">&nbsp;</td>
                  <td class="AccesorioCampo">&nbsp;</td>
               </tr>
            </table>
         </div>
         
         <div class="DivSaltoLinea">&nbsp;</div>
         
         <div class="DivVehiculoSeccion">
            <table class="TablaVehiculoObservaciones">
               <tr>
                  <td class="NombreVehiculoObserv">Observaciones:</td>
                  <td class="CampoVehiculoObservSubrrayado">El vehículo es para uso de las diligencias de la Dirección y/o Departamento a mi cargo.</td>
               </tr>
               <tr>
                  <td colspan="2" class="CampoVehiculoObservSubrrayado">&nbsp;</td>
               </tr>
               <tr>
                  <td colspan="2" class="CampoVehiculoObservSubrrayado">&nbsp;</td>
               </tr>
            </table>
         </div> 
         
         <div class="DivSaltoLinea">&nbsp;</div>
         
         <div class="DivVehiculoSeccion">
            <table class="TablaVehiculoDescripcion">
               <tr>
                  <td class="NombreVehiculoDesc">Describir en forma específica el uso:</td>
                  <td class="CampoVehiculoDescSubrrayado">Actividades propias de la función.</td>
               </tr>
               <tr>
                  <td colspan="2" class="CampoVehiculoDescSubrrayado">&nbsp;</td>
               </tr>
               <tr>
                  <td colspan="2" class="CampoVehiculoDescSubrrayado">&nbsp;</td>
               </tr>
            </table>
         </div> 
         
         <div class="DivVehiculoCompromiso">
            Me comprometo a hacer buen uso y mantenerlo en buen estado
         </div>
         
         <div>
            <table class="TablaVehiculoFirmas">
               <tr>
                  <td class="CampoVehiculoFirmas">TITULAR DEL ÁREA</td>
                  <td class="EspacioVehiculoFirmas">&nbsp;</td>
                  <td class="CampoVehiculoFirmas">RESPONSABLE DEL VEHÍCULO</td>
                  <td class="EspacioVehiculoFirmas">&nbsp;</td>
                  <td class="CampoVehiculoFirmas">ENLACE DE VEHÍCULOS</td>
               </tr>
               <tr>
                  <td colspan="5">&nbsp;</td>
               </tr>
               <tr>
                  <td class="CampoVehiculoFirmasSubrrayado">&nbsp;</td>
                  <td class="EspacioVehiculoFirmas">&nbsp;</td>
                  <td class="CampoVehiculoFirmasSubrrayado">&nbsp;</td>
                  <td class="EspacioVehiculoFirmas">&nbsp;</td>
                  <td class="CampoVehiculoFirmasSubrrayado">&nbsp;</td>
               </tr>
               <tr>
                  <td class="CampoVehiculoFirmas"><asp:Label ID="FirmaTitularAreaLabel" runat="server"></asp:Label></td>
                  <td class="EspacioVehiculoFirmas">&nbsp;</td>
                  <td class="CampoVehiculoFirmas"><asp:Label ID="FirmaResponsableVehiculoLabel" runat="server"></asp:Label></td>
                  <td class="EspacioVehiculoFirmas">&nbsp;</td>
                  <td class="CampoVehiculoFirmas"><asp:Label ID="FirmaEnlaceVehiculosLabel" runat="server"></asp:Label></td>
               </tr>
               <tr>
                  <td class="CampoVehiculoFirmas">(nombre y firma)</td>
                  <td class="EspacioVehiculoFirmas">&nbsp;</td>
                  <td class="CampoVehiculoFirmas">(nombre y firma)</td>
                  <td class="EspacioVehiculoFirmas">&nbsp;</td>
                  <td class="CampoVehiculoFirmas">(nombre y firma)</td>
               </tr>
            </table>
         </div>
         
      </div> 
   </div>
   <script language="javascript" type="text/javascript">

     function ImprimirPantalla() {
         window.print();
     }

     ImprimirPantalla();
     
   </script>
</asp:Content>
