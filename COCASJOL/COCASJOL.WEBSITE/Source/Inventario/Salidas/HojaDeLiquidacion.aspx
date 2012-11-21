<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HojaDeLiquidacion.aspx.cs"
    Inherits="COCASJOL.WEBSITE.Source.Inventario.Salidas.HojaDeLiquidacion" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="refresh" content="3" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <div>
        <ext:Viewport runat="server" Layout="FormLayout">
            <Items>
                <ext:Container runat="server" Margins="20">
                    <Items>
                        <ext:TextField runat="server" ID="txtNombreSocio" FieldLabel="Nombre del Socio" AnchorHorizontal="80%">
                        </ext:TextField>
                        <ext:ComboBox runat="server" ID="cmbTipoCafe" FieldLabel="Tipo de Café" AnchorHorizontal="80%">
                        </ext:ComboBox>
                    </Items>
                </ext:Container>
                <ext:Container runat="server" Layout="ColumnLayout" Height="500">
                    <Items>
                        <ext:Container runat="server" ColumnWidth=".5">
                            <Items>
                                <ext:Panel ID="panelDetalle" runat="server" Title="Detalle del Café">
                                    <Items>
                                        <ext:TextField ID="txtCantidad" runat="server" FieldLabel="Cantidad" ></ext:TextField>
                                        <ext:TextField ID="txtPrecioLibra" runat="server" FieldLabel="Precio Por Libra" ></ext:TextField>
                                        <ext:TextField ID="txtTotal" runat="server" FieldLabel="Total" ></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container1" runat="server" ColumnWidth=".5">
                            <Items>
                                <ext:Panel ID="panel1" runat="server" Title="Detalle de Liqudación">
                                    <Items>
                                        <ext:TextField ID="txtAportacionAsociacion" runat="server" FieldLabel="Aportación por asociación"></ext:TextField>
                                        <ext:TextField ID="txtAportacionAnual" runat="server" FieldLabel="Aportación anual"></ext:TextField>
                                        <ext:TextField ID="txtAportacion" runat="server" FieldLabel="Aportacion"></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Height="500">
                    <Items>
                        <ext:Container ID="Container3" runat="server" ColumnWidth=".5">
                            <Items>
                                <ext:Panel ID="panel2" runat="server" Title="Detalle del Café">
                                    <Items>
                                        <ext:TextField ID="TextField1" runat="server" FieldLabel="Cantidad" ></ext:TextField>
                                        <ext:TextField ID="TextField2" runat="server" FieldLabel="Precio Por Libra" ></ext:TextField>
                                        <ext:TextField ID="TextField3" runat="server" FieldLabel="Total" ></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container4" runat="server" ColumnWidth=".5">
                            <Items>
                                <ext:Panel ID="panel3" runat="server" Title="Detalle de Liqudación">
                                    <Items>
                                        <ext:TextField ID="TextField4" runat="server" FieldLabel="Aportación por asociación"></ext:TextField>
                                        <ext:TextField ID="TextField5" runat="server" FieldLabel="Aportación anual"></ext:TextField>
                                        <ext:TextField ID="TextField6" runat="server" FieldLabel="Aportacion"></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Container>
                <%--<ext:Panel runat="server" Layout="ColumnLayout" AnchorVertical="-70" Height="500">
                    <Items>
                        <ext:Container runat="server" ColumnWidth=".5"  >
                            <Items>
                                <ext:GridPanel ID="gridDetalleCafe" runat="server" StoreID="stDetalleCafe" Title="Detalle de Café" Height="500">
                                    
                                    <ColumnModel>
                                        <Columns>
                                            <ext:NumberColumn DataIndex="DETALLE" Header="Detale" />
                                            <ext:NumberColumn DataIndex="VALOR" Header="Valor" />
                                        </Columns>
                                    </ColumnModel>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar1" runat="server">
                                            <Items>
                                                <ext:Button ID="btnAgregarDetalleCafe" runat="server" Icon="CogAdd" Text="Agregar">
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" ColumnWidth=".5">
                            <Items>
                                <ext:GridPanel ID="gridDetalleLiquidación" runat="server" StoreID="stDetalleLiquidacion"
                                    Title="Detalle de Liquidacion">
                                    
                                    <ColumnModel>
                                        <Columns>
                                            <ext:NumberColumn DataIndex="DETALLE" Header="Detale" />
                                            <ext:NumberColumn DataIndex="VALOR" Header="Valor" />
                                        </Columns>
                                    </ColumnModel>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                <ext:Button ID="btnAgregarDetalleLiquidacion" runat="server" Icon="CogAdd" Text="Agregar">
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>--%>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
