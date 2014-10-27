using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Aplicacion.Activo
{
    public partial class TransferenciaAccesorio : System.Web.UI.Page
    {
        #region "Eventos"

            protected void BotonBuscarActivoDestino_Click(object sender, EventArgs e)
            {
                TipoActivoBusquedaHidden.Value = "Destino";
                ControlBuscarActivo.InicioControl();
            }

            protected void BotonBuscarActivoOrigen_Click(object sender, EventArgs e)
            {
                TipoActivoBusquedaHidden.Value = "Origen";
                ControlBuscarActivo.InicioControl();
            }

            protected void BotonCancelar_Click(object sender, EventArgs e)
            {
                LimpiarFormulario();
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                GuardarTransferencia();
            }

            protected void LinkBuscarActivoDestino_Click(object sender, EventArgs e)
            {
                BuscarActivoDestinoPorNumeroSerie();
            }

            protected void LinkBuscarActivoOrigen_Click(object sender, EventArgs e)
            {
                BuscarActivoOrigenPorNumeroSerie();
            }

            protected void LinkBuscarActivoDestinoEconomico_Click(object sender, EventArgs e)
            {
                NumeroEconomicoDestinoTextoModificado();
            }

            protected void LinkBuscarActivoOrigenEconomico_Click(object sender, EventArgs e)
            {
                NumeroEconomicoOrigenTextoModificado();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void TablaActivo_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DropDownList CondicionId = (DropDownList)e.Row.FindControl("CondicionId");
                    //CheckBox chkUbicacion = (CheckBox)e.Row.FindControl("chkUbicacion");

                    //CondicionId.DataValueField = "CondicionId";
                    //CondicionId.DataTextField = "Nombre";

                    //CondicionId.DataSource = DTCondiciones;
                    //CondicionId.DataBind();

                    //CondicionId.SelectedValue = TablaActivo.DataKeys[e.Row.RowIndex]["CondicionId"].ToString();

                    //switch (Int16.Parse(TablaActivo.DataKeys[e.Row.RowIndex]["UbicacionActivoId"].ToString()))
                    //{
                    //    case (Int16)ConstantePrograma.UbicacionActivo.Bodega:
                    //        chkUbicacion.Checked = true;
                    //        break;

                    //    default:
                    //        chkUbicacion.Checked = false;
                    //        break;
                    //}

                }
            }

            protected void NumeroSerieDestino_TextChanged(object sender, EventArgs e)
            {
                NumeroSerieDestinoTextoModificado();
            }

            protected void NumeroSerieOrigen_TextChanged(object sender, EventArgs e)
            {
                NumeroSerieOrigenTextoModificado();
            }

            protected void NumeroEconomicoDestino_TextChanged(object sender, EventArgs e)
            {
                NumeroEconomicoDestinoTextoModificado();
            }

            protected void NumeroEconomicoOrigen_TextChanged(object sender, EventArgs e)
            {
                NumeroEconomicoOrigenTextoModificado();
            }

        #endregion

        #region "Métodos"

            public void BuscarActivoPorId(int ActivoId, string Descripcion, string NumeroSerie, string Modelo, string Color)
            {
                switch (TipoActivoBusquedaHidden.Value)
                {
                    case "Origen":
                        ActivoOrigenIdHidden.Value = ActivoId.ToString();
                        NumeroSerieOrigen.Text = NumeroSerie;
                        DescripcionActivoOrigen.Text = Descripcion;
                        ModeloActivoOrigen.Text = Modelo;
                        ColorActivoOrigen.Text = Color;
                        EtiquetaMensaje.Text = "";
                        BuscarNumeroEconomicoOrigen(ActivoId);
                        SeleccionarAccesorios(ActivoId);
                        break;

                    case "Destino":
                        ActivoDestinoIdHidden.Value = ActivoId.ToString();
                        NumeroSerieDestino.Text = NumeroSerie;
                        DescripcionActivoDestino.Text = Descripcion;
                        ModeloActivoDestino.Text = Modelo;
                        ColorActivoDestino.Text = Color;
                        EtiquetaMensaje.Text = "";
                        BuscarNumeroEconomicoDestino(ActivoId);
                        break;

                    default:
                        // Do nothing
                        break;
                }

                ActualizarTablaTransferencia.Update();
            }

            protected void BuscarActivoDestinoPorNumeroSerie()
            {
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

                ActivoEntidadObjeto.NumeroSerie = NumeroSerieDestino.Text.Trim();

                BuscarActivoDestino(ActivoEntidadObjeto, true);
            }

            protected void BuscarActivoDestinoPorNumeroEconomico()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
                AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();

                AccesorioEntidadObjeto.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.NumeroEconomico;
                AccesorioEntidadObjeto.Descripcion = NumeroEconomicoDestino.Text.Trim();

                Resultado = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        BuscarActivoDestinoPorID(int.Parse((Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString())));
                    }
                    else
                    {
                        LimpiarActivoDestino();
                        EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;
                        NumeroSerieDestino.Focus();
                    }
                }
                else
                {
                    LimpiarActivoDestino();
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

            }

            protected void BuscarActivoOrigenPorNumeroSerie()
            {
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

                ActivoEntidadObjeto.NumeroSerie = NumeroSerieOrigen.Text.Trim();

                BuscarActivoOrigen(ActivoEntidadObjeto, true);
            }

            protected void BuscarActivoOrigenPorNumeroEconomico()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
                AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();

                AccesorioEntidadObjeto.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.NumeroEconomico;
                AccesorioEntidadObjeto.Descripcion = NumeroEconomicoOrigen.Text.Trim();

                Resultado = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        BuscarActivoOrigenPorID(int.Parse((Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString())));
                    }
                    else
                    {
                        LimpiarActivoOrigen();
                        EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;
                        NumeroSerieOrigen.Focus();
                    }
                }
                else
                {
                    LimpiarActivoOrigen();
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

            }

            protected void BuscarNumeroEconomicoDestino(int ActivoID)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
                AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();

                AccesorioEntidadObjeto.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.NumeroEconomico;
                AccesorioEntidadObjeto.ActivoId = ActivoID;

                NumeroEconomicoDestino.Text = "";

                Resultado = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        NumeroEconomicoDestino.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                    }
                }
            }

            protected void BuscarNumeroEconomicoOrigen(int ActivoID)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
                AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();

                AccesorioEntidadObjeto.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.NumeroEconomico;
                AccesorioEntidadObjeto.ActivoId = ActivoID;

                NumeroEconomicoOrigen.Text = "";

                Resultado = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        NumeroEconomicoOrigen.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                    }
                }
            }

            protected void BuscarActivoDestinoPorID(int ActivoID)
            {
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

                ActivoEntidadObjeto.ActivoId = ActivoID;

                BuscarActivoDestino(ActivoEntidadObjeto, false);
            }

            protected void BuscarActivoOrigenPorID(int ActivoID)
            {
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

                ActivoEntidadObjeto.ActivoId = ActivoID;

                BuscarActivoOrigen(ActivoEntidadObjeto, false);
            }

            protected void BuscarActivoDestino(ActivoEntidad ActivoEntidadObjeto, bool BuscarNumeroEconomico)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                bool TransferenciaPermitida = true;

                Resultado = ActivoProcesoObjeto.SeleccionarActivo(ActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        //Ahora se valida que sea un activo del tipo Vehículo
                        if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString()) == (Int16)ConstantePrograma.TipoAtivo.Vehiculo)
                        {
                            //Ahora se valida que al activo e le pueda transferir accesorios (no baja, no salida, no sea un acesorio de otro activo)
                            TransferenciaPermitida = ActivoProcesoObjeto.ValidarTransferenciaAccesorios(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString()));

                            if (TransferenciaPermitida == true)
                            {
                                ActivoDestinoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                                NumeroSerieDestino.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroSerie"].ToString();
                                DescripcionActivoDestino.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                                ModeloActivoDestino.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
                                ColorActivoDestino.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Color"].ToString();

                                if (BuscarNumeroEconomico == true)
                                    BuscarNumeroEconomicoDestino(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString()));

                                EtiquetaMensaje.Text = "";
                            }
                            else
                            {
                                LimpiarActivoDestino();
                                EtiquetaMensaje.Text = TextoError.ActivoNoPuedeTransferir;
                                NumeroSerieDestino.Focus();
                            }

                        }
                        else
                        {
                            LimpiarActivoDestino();
                            EtiquetaMensaje.Text = TextoError.ActivoNoVehiculo;
                            NumeroSerieDestino.Focus();
                        }
                    }
                    else
                    {
                        LimpiarActivoDestino();
                        EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;
                        NumeroSerieDestino.Focus();
                    }
                }
                else
                {
                    LimpiarActivoDestino();
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

            }

            protected void BuscarActivoOrigen(ActivoEntidad ActivoEntidadObjeto, bool BuscarNumeroEconomico)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                bool TransferenciaPermitida = true;

                Resultado = ActivoProcesoObjeto.SeleccionarActivo(ActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        //Ahora se valida que sea un activo del tipo Vehículo
                        if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString()) == (Int16)ConstantePrograma.TipoAtivo.Vehiculo)
                        {
                            //Ahora se valida que el activo pueda transferir accesorios (no baja, no salida, no sea un acesorio de otro activo)
                            TransferenciaPermitida = ActivoProcesoObjeto.ValidarTransferenciaAccesorios(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString()));

                            if (TransferenciaPermitida == true)
                            {
                                ActivoOrigenIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                                NumeroSerieOrigen.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroSerie"].ToString();
                                DescripcionActivoOrigen.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                                ModeloActivoOrigen.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
                                ColorActivoOrigen.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Color"].ToString();

                                if (BuscarNumeroEconomico == true)
                                    BuscarNumeroEconomicoOrigen(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString()));

                                SeleccionarAccesorios(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString()));
                                EtiquetaMensaje.Text = "";
                            }
                            else
                            {
                                LimpiarActivoOrigen();
                                EtiquetaMensaje.Text = TextoError.ActivoNoPuedeTransferir;
                                NumeroSerieOrigen.Focus();
                            }

                        }
                        else
                        {
                            LimpiarActivoOrigen();
                            EtiquetaMensaje.Text = TextoError.ActivoNoVehiculo;
                            NumeroSerieOrigen.Focus();
                        }
                    }
                    else
                    {
                        LimpiarActivoOrigen();
                        EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;
                        NumeroSerieOrigen.Focus();
                    }
                }
                else
                {
                    LimpiarActivoOrigen();
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

            }

            protected void GuardarTransferencia()
            {
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
                CheckBox chkTransferir;
                int CantidadAccesorios = 0;
                System.Text.StringBuilder ActivoAccesorioIDs = new System.Text.StringBuilder();

                if (ActivoOrigenIdHidden.Value != "0")
                {
                    if (ActivoDestinoIdHidden.Value != "0")
                    {
                        if (ActivoOrigenIdHidden.Value != ActivoDestinoIdHidden.Value)
                        {
                            ActivoAccesorioIDs.Append(",");

                            UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                            foreach (GridViewRow FilaGrid in TablaActivo.Rows)
                            {
                                chkTransferir = (CheckBox)FilaGrid.FindControl("SeleccionarTransferir");

                                if (chkTransferir.Checked == true)
                                {
                                    ActivoAccesorioIDs.Append(TablaActivo.DataKeys[FilaGrid.RowIndex]["ActivoAccesorioId"].ToString() + ",");
                                    CantidadAccesorios += 1;
                                }
                                
                            }

                            AccesorioObjetoEntidad.GrupoActivoAccesorioId = ActivoAccesorioIDs.ToString();
                            AccesorioObjetoEntidad.ActivoId = int.Parse(ActivoDestinoIdHidden.Value);
                            AccesorioObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;

                            if (CantidadAccesorios > 0)
                                GuardarTransferencia(AccesorioObjetoEntidad, CantidadAccesorios);
                            else
                                EtiquetaMensajeError.Text = "Favor de seleccionar los accesorios a transferir.";

                        }
                        else
                        {
                            EtiquetaMensajeError.Text = "El vehículo origen y destino deben ser diferentes.";
                        }
                    }
                    else
                    {
                        EtiquetaMensajeError.Text = "Favor de seleccionar el vehículo destino.";
                    }
                }
                else
                {
                    EtiquetaMensajeError.Text = "Favor de seleccionar el vehículo de origen.";
                }

            }

            protected void GuardarTransferencia(AccesorioEntidad AccesorioObjetoEntidad, int CantidadAccesorios)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AccesorioProceso AccesorioProcesoNegocio = new AccesorioProceso();

                Resultado = AccesorioProcesoNegocio.GuardarTransferenciaAccesorio(AccesorioObjetoEntidad, CantidadAccesorios);

                if (Resultado.ErrorId == (int)ConstantePrograma.Accesorio.AccesorioGuardadoCorrectamente)
                {
                    LimpiarFormulario();
                    EtiquetaMensaje.Text = "Transferencia de accesorios guardada correctamente";
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.TransferenciaAccesorios);

                    TablaActivo.DataSource = null;
                    TablaActivo.DataBind();

                    SeleccionarTextoError();
                }
            }

            protected void NumeroSerieDestinoTextoModificado()
            {
                if (NumeroSerieDestino.Text.Trim() != "")
                {
                    BuscarActivoDestinoPorNumeroSerie();
                }
                else
                {
                    LimpiarActivoDestino();
                }
            }

            protected void NumeroEconomicoDestinoTextoModificado()
            {
                if (NumeroEconomicoDestino.Text.Trim() != "")
                {
                    BuscarActivoDestinoPorNumeroEconomico();
                }
                else
                {
                    LimpiarActivoOrigen();
                }
            }

            protected void NumeroSerieOrigenTextoModificado()
            {
                if (NumeroSerieOrigen.Text.Trim() != "")
                {
                    BuscarActivoOrigenPorNumeroSerie();
                }
                else
                {
                    LimpiarActivoOrigen();
                }
            }

            protected void NumeroEconomicoOrigenTextoModificado()
            {
                if (NumeroEconomicoOrigen.Text.Trim() != "")
                {
                    BuscarActivoOrigenPorNumeroEconomico();
                }
                else
                {
                    LimpiarActivoOrigen();
                }
            }

            protected void LimpiarActivoDestino()
            {
                ActivoDestinoIdHidden.Value = "0";
                DescripcionActivoDestino.Text = "";
                ModeloActivoDestino.Text = "";
                ColorActivoDestino.Text = "";
            }

            protected void LimpiarActivoOrigen()
            {
                ActivoOrigenIdHidden.Value = "0";
                DescripcionActivoOrigen.Text = "";
                ModeloActivoOrigen.Text = "";
                ColorActivoOrigen.Text = "";
                TablaActivo.DataSource = null;
                TablaActivo.DataBind();
            }

            protected void LimpiarFormulario()
            {
                LimpiarActivoDestino();
                LimpiarActivoOrigen();
                NumeroSerieOrigen.Text = "";
                NumeroEconomicoOrigen.Text = "";
                NumeroSerieDestino.Text = "";
                NumeroEconomicoDestino.Text = "";
                EtiquetaMensajeError.Text = "";
                EtiquetaMensaje.Text = "";
            }

            protected void SeleccionarAccesorios(int ActivoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
                AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();

                AccesorioObjetoEntidad.ActivoId = ActivoId;
                AccesorioObjetoEntidad.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.ActivoFijo;

                Resultado = AccesorioProcesoObjeto.SeleccionarAccesorioParaTransferir(AccesorioObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivo.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivo.CssClass = ConstantePrograma.ClaseTabla;

                    TablaActivo.DataSource = Resultado.ResultadoDatos;
                    TablaActivo.DataBind();
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarTextoError()
            {
                BuscarEconomicoOrigenRequerido.ErrorMessage = TextoError.NumeroEconomicoObligatorio + "<br />";
                BuscarSerieOrigenRequerido.ErrorMessage = TextoError.NumeroSerieObligatorio + "<br />";
                BuscarEconomicoDestinoRequerido.ErrorMessage = TextoError.NumeroEconomicoObligatorio + "<br />";
                BuscarSerieDestinoRequerido.ErrorMessage = TextoError.NumeroSerieObligatorio + "<br />";
                
            }

        #endregion
    }
}
