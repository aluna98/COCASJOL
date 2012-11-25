<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotaDePeso.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Inventario.Ingresos.NotaDePeso" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nota de Peso</title>
    <%--<meta http-equiv="refresh" content="3" />--%>
    <ext:XScript runat="server">
    
    <script  type="text/javascript">

        function validarFormulario() {
            return true;
        }

        function gridRowAdded(item) {
            #{txtPeso}.setValue(#{txtPeso}.getValue() + item.data.items[0].data.PESO);
        }
         
        function pesoToJson() {
            var items =  #{stPesos}.data;
            var ret = [];
            for (var i = 0; i < items.length; i++) {
                ret.push({DETALLE_PESO : items.items[i].data.PESO, DETALLE_CANTIDAD_SACOS : items.items[i].data.CANT_SACOS });
            }
            
            return Ext.encode(ret);
        } 
            
        </script>
    </ext:XScript>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server">
        </ext:ResourceManager>
        <ext:Store runat="server" ID="stPesos">
        <Listeners>
            <Add Fn="gridRowAdded" />
        </Listeners>
        <Reader>
            <ext:JsonReader>
                <Fields>
                    <ext:RecordField Name="CANT_SACOS" Type="Int">
                    </ext:RecordField>
                    <ext:RecordField Name="PESO" Type="Float">
                    </ext:RecordField>
                </Fields>
            </ext:JsonReader>
        </Reader>
        </ext:Store>
        <ext:Store runat="server" ID="stSocios">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="SOCIOS_ID" Type="String" />
                        <ext:RecordField Name="SOCIOS_NOMBRE" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <ext:Viewport runat="server">
            <Items>
                <ext:FormPanel runat="server" LabelWidth="110" Anchor="100% 100%">
                    <Items>
                        <ext:Container Layout="ColumnLayout" Height="52" AnchorHorizontal="100%" runat="server">
                            <Items>
                                <ext:Container runat="server" Layout="FormLayout" AnchorVertical="100%" ColumnWidth="0.5">
                                    <Items>
                                        <ext:TextField runat="server" ID="ID_NOTA" FieldLabel="No. Nota de Peso" AnchorHorizontal="95%" />
                                        <ext:ComboBox runat="server" ID="SOCIOS_ID" FieldLabel="Cod. Socio" AnchorHorizontal="95%"
                                            ValueField="SOCIOS_ID" DisplayField="SOCIOS_ID" StoreID="stSocios">
                                            <Listeners>
                                                <Select Fn="function(combo, record, index){#{SOCIOS_NOMBRE}.setValue(record.data.SOCIOS_NOMBRE ); }" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth="0.5">
                                    <Items>
                                        <ext:DateField runat="server" ID="FECHA" FieldLabel="Fecha" AnchorHorizontal="95%" >
                                            <Listeners>
                                                <Render Handler="#{FECHA}.setValue(new Date());" />
                                            </Listeners>
                                        </ext:DateField>
                                        <ext:ComboBox runat="server" ID="cmbTipoCafe" FieldLabel="Tipo Cafe" AnchorHorizontal="95%">
                                            <Items>
                                                <ext:ListItem Value="CONV" Text="Convencional" />
                                                <ext:ListItem Value="ESP" Text="Especial" />
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:Container runat="server" AnchorVertical="100%">
                            <Items>
                                <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth="1">
                                    <Items>
                                        <ext:TextField runat="server" ID="SOCIOS_NOMBRE" FieldLabel="Nombre del Socio" AnchorHorizontal="97.5%"
                                            ReadOnly="true" />
                                        <ext:TextArea runat="server" ID="TOTALRECIBIDO" FieldLabel="Total recibido" AnchorHorizontal="97.5%"
                                            ReadOnly="true" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:FieldSet runat="server" Title="Descuentos" Layout="FormLayout" LabelWidth="80"
                            PaddingSummary="10">
                            <Items>
                                <ext:Container ID="Container4" Layout="ColumnLayout" Height="26" AnchorHorizontal="100%"
                                    runat="server">
                                    <Items>
                                        <ext:Container ID="Container3" runat="server" Layout="FormLayout" AnchorVertical="100%"
                                            ColumnWidth="0.333">
                                            <Items>
                                                <ext:NumberField MaskRe="%" runat="server" ID="txtPorcentajeDefecto" FieldLabel="% Defecto"
                                                    AnchorHorizontal="90%" Text="0" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container5" runat="server" Layout="FormLayout" AnchorVertical="100%"
                                            ColumnWidth="0.333">
                                            <Items>
                                                <ext:NumberField runat="server" ID="txtPorcentajeHumedad" FieldLabel="% Humedad"
                                                    AnchorHorizontal="90%" Text="0" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container6" runat="server" Layout="FormLayout" AnchorVertical="100%"
                                            ColumnWidth="0.333">
                                            <Items>
                                                <ext:Checkbox runat="server" ID="chkCarroPropio" FieldLabel="Carro Propio" AnchorHorizontal="90%"
                                                    Checked="True" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                        <ext:Panel runat="server" Layout="Center" ID="GridContainer">
                            <Buttons>
                                <ext:Button runat="server" ID="btnGuardar" Text="Guardar">
                                    <DirectEvents>
                                        <Click Before="return validarFormulario();" OnEvent="btnGuardar_OnClick">
                                            <ExtraParams >
                                                <ext:Parameter Name="DETALLE" Mode="Raw" Value="
                                                pesoToJson()
                                                " />
                                            </ExtraParams>
                                            <Confirmation Message="¿Desea registrar la nota de peso?" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" ID="btnCancelar" Text="Cancelar">
                                    <DirectEvents>
                                        <Click>
                                            <Confirmation Message="¿Desea descartar la nota de peso?" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                            <Items>
                                <ext:GridPanel ID="gridPesos" runat="server" StoreID="stPesos">
                                    
                                    <CustomConfig>
                                        <ext:ConfigItem Name="width" Value="75%" Mode="Value" />
                                    </CustomConfig>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:NumberColumn DataIndex="CANT_SACOS" Header="Cantidad de Sacos">
                                                <Editor>
                                                    <ext:NumberField runat="server">
                                                    </ext:NumberField>
                                                </Editor>
                                            </ext:NumberColumn>
                                            <ext:NumberColumn DataIndex="PESO" Header="Peso">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField1" runat="server">
                                                    </ext:NumberField>
                                                </Editor>
                                            </ext:NumberColumn>
                                        </Columns>
                                    </ColumnModel>
                                    <Plugins>
                                    <ext:RowEditor runat="server" ></ext:RowEditor>
                                    </Plugins>
                                    <TopBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:Button ID="btnAgregar" runat="server" Icon="CogAdd" Text="Agregar">
                                                <Listeners>
                                                    <Click Handler="#{winAgregarPeso}.show()" />
                                                </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel>
                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Height="100"
                                    MinHeight="100">
                                    <CustomConfig>
                                        <%--<ext:ConfigItem Name="width" Value="75%" Mode="Value" />--%>
                                    </CustomConfig>
                                    <Items>
                                        <ext:Container ID="Container8" runat="server" ColumnWidth=".5">
                                            <Items>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container9" runat="server" ColumnWidth=".375">
                                            <Items>
                                                <ext:NumberField runat="server" ID="txtDescuentos" FieldLabel="Descuentos" AnchorHorizontal="90%"
                                                    ReadOnly="true">
                                                    <Listeners>
                                                        <Render Handler="#{txtDescuentos}.setValue(0);" />
                                                    </Listeners>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="txtPeso" FieldLabel="Peso" AnchorHorizontal="90%"
                                                    ReadOnly="true">
                                                    <Listeners>
                                                        <Render Handler="#{txtPeso}.setValue(0);" />
                                                    </Listeners>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="txtTotal" FieldLabel="Total" AnchorHorizontal="90%"
                                                    ReadOnly="true">
                                                    <Listeners>
                                                        <Render Handler="#{txtTotal}.setValue(0);" />
                                                    </Listeners>
                                                </ext:NumberField>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>

        <ext:Window ID="winAgregarPeso" runat="server" CloseAction="Hide" Width="400" Height="170" Hidden="true">
        <Listeners>
            <Hide Handler="#{txtAddSacos}.setValue(0); #{txtAddPeso}.setValue(0); " />
            <Show Handler="#{txtAddPeso}.focus();" />
        </Listeners>
        <Items>
            <ext:FormPanel runat="server">
            <Items>
                <ext:TabPanel runat="server">
                    <Items>
                        <ext:Panel runat="server" Title="Datos de Peso" AutoHeight="true" >
                        <Items>
                        <ext:Panel runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                            <Items>
                                <ext:NumberField runat="server" ID="txtAddPeso" DataIndex="PESO" AnchorHorizontal="90%"
                                    FieldLabel="Peso" AllowBlank="false" IndicatorIcon="BulletRed" EnableKeyEvents="true">
                                    <Listeners>
                                        <KeyDown Handler="if (e.keyCode == e.ENTER) #{txtAddSacos}.focus()" />
                                    </Listeners>
                                </ext:NumberField>
                                <ext:NumberField runat="server" ID="txtAddSacos" DataIndex="CANT_SACOS" AnchorHorizontal="90%"
                                    FieldLabel="Cant. Sacos" AllowBlank="false" IndicatorIcon="BulletRed">
                                    <Listeners>
                                        <KeyDown Handler="if (e.keyCode == e.ENTER) #{btnAgregarPesoAgregar}.focus()" />
                                    </Listeners>
                                </ext:NumberField>
                            </Items>
                        </ext:Panel>
                        </Items>
                        </ext:Panel>
                    </Items>
                </ext:TabPanel>
            </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnAgregarPesoAgregar" Text="Agregar" Icon="Add">
                    <Listeners>
                        <Click Handler="#{gridPesos}.insertRecord(#{stPesos}.data.length,{CANT_SACOS : #{txtAddSacos}.getValue(), PESO : #{txtAddPeso}.getValue() } ); #{txtAddSacos}.setValue(0); #{txtAddPeso}.setValue(0); #{txtAddPeso}.focus();" />
                    </Listeners>
                    </ext:Button>
                    <ext:Button runat="server" ID="btnAgregarPesoCancelar" Text="Cancelar" Icon="Cancel">
                    <Listeners>
                        <Click Handler="#{winAgregarPeso}.hide();" />
                    </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Items>
        

        </ext:Window>




    </div>
    </form>
</body>
</html>
