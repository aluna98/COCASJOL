<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantenimientoNotaDePeso.aspx.cs" Inherits="COCASJOL.Website.Source.Inventario.Ingresos.MantenimientoNotaDePeso" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Nota de Peso</title>
    <%--<meta http-equiv="refresh" content="3" />--%>
    <ext:XScript runat="server">
    
    <script  type="text/javascript">

        var Grid = null;
        var GridStore = null;
        var AddWindow = null;
        var AddForm = null;
        var EditWindow = null;
        var EditForm = null;

        var AlertSelMsgTitle = "Atención";
        var AlertSelMsg = "Debe seleccionar 1 elemento";



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

        var PageX = {

             setReferences: function () {
                Grid = #{gridNotasDePeso};
                GridStore = #{stNotasPeso};
                AddWindow = #{winNotaPeso};
                AddForm = #{notaPesoFormP};
                //EditWindow = EditarUsuarioWin;
                //EditForm = EditarUsuarioFormP;
            },
               
            add: function () {
                AddWindow.show();
            },

            keyUpEvent : function(sender, eArgs){
                if (eArgs.getKey() == 13)
                    GridStore.reload();
            }
        };
            
        </script>
    </ext:XScript>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server">
        <Listeners>
            <DocumentReady Handler="PageX.setReferences();" />
        </Listeners>
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
    <ext:Store ID="stNotasPeso" runat="server" AutoSave="true" OnRefreshData="stNotasPeso_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="NOTA_ID">
                <Fields>
                    <ext:RecordField Name="NOTA_ID" />
                    <ext:RecordField Name="NOTA_FECHA" />
                    <ext:RecordField Name="SOCIO_ID" />
                    <ext:RecordField Name="SOCIO_NOMBRE" />
                    <ext:RecordField Name="NOTA_TIPO_CAFE" />
                    <ext:RecordField Name="NOTA_PORCENTAJE_DEFECTO" />
                    <ext:RecordField Name="NOTA_PORENTAJE_HUMDEDA" />
                    <ext:RecordField Name="NOTA_CARRO_PROPIO" />
                    <ext:RecordField Name="CREADO_POR" />
                    <ext:RecordField Name="FECHA_CREACION" Type="Date" />
                    <ext:RecordField Name="MODIFICADO_POR" />
                    <ext:RecordField Name="FECHA_MODIFICACION" Type="Date" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
            <CommitDone Handler="Ext.Msg.alert('Guardar', 'Cambios guardados exitosamente.');" />
        </Listeners>
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
        <ext:Viewport runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Notas de Peso" Icon="Group" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="gridNotasDePeso" runat="server" AutoExpandColumn="SOCIO_NOMBRE" Height="300"
                            Title="Notas de Peso" Header="false" StoreID="stNotasPeso"  Border="false" StripeRows="true" TrackMouseOver="true">
                            
                            <ColumnModel>
                                <Columns>
                                    <ext:Column DataIndex="NOTA_ID" Header="ID" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="SOCIO_ID"   Header="Socio" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="SOCIO_NOMBRE" Header="Nombre" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="NOTA_TIPO_CAFE"   Header="Tipo de Café" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="NOTA_PORCENTAJE_DEFECTO"   Header="Porcentaje de Defecto" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="NOTA_CARRO_PROPIO"   Header="Carro Propio" Sortable="true">
                                        <Renderer  Handler="" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <ext:Button ID="AgregarUsuarioBtn" runat="server" Text="Agregar" Icon="UserAdd" >
                                            <Listeners>
                                                <Click Handler="PageX.add();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EditarUsuarioBtn" runat="server" Text="Editar" Icon="UserEdit">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="CambiarClaveBtn" runat="server" Text="Cambiar Contraseña" Icon="Key">
                                            <Listeners>
                                                <Click Handler="PageX.editPassword();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EliminarUsuarioBtn" runat="server" Text="Eliminar" Icon="UserDelete">
                                            <Listeners>
                                                <Click Handler="PageX.remove();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <HeaderRows>
                                        <ext:HeaderRow>
                                            <Columns>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_NOTA_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_SOCIO_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_SOCIO_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_NOTA_TIPO_CAFE" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_NOTA_PORCENTAJE_DEFECTO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_NOTA_CARRO_PROPIO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                            </Columns>
                                        </ext:HeaderRow>
                                    </HeaderRows>
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="stNotasPeso" />
                            </BottomBar>
                            <LoadMask ShowMask="true" />
                            <SaveMask ShowMask="true" />
                            <Listeners>
                                <RowDblClick Handler="PageX.edit();" />
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                 
            </Items>
        </ext:Viewport>


        <ext:Window runat="server" ID="winNotaPeso" CloseAction="Hide" Hidden="true" Height="600" Width="600" Maximizable="false" Resizable="false" Title="Nota de Peso">
        <Listeners>
            <Hide Handler="#{winAgregarPeso}.hide();" />
        </Listeners>
                <Items>
                <ext:FormPanel ID="notaPesoFormP" runat="server" LabelWidth="110" Anchor="100% 100%">
                    <Items>
                        <ext:Container ID="Container1" Layout="ColumnLayout" Height="52" AnchorHorizontal="100%" runat="server">
                            <Items>
                                <ext:Container ID="Container2" runat="server" Layout="FormLayout" AnchorVertical="100%" ColumnWidth="0.5">
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
                                <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth="0.5">
                                    <Items>
                                        <ext:DateField runat="server" ID="FECHA" FieldLabel="Fecha" AnchorHorizontal="95%">
                                            <Listeners>
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
                        <ext:Container ID="Container4" runat="server" AnchorVertical="100%">
                            <Items>
                                <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth="1">
                                    <Items>
                                        <ext:TextField runat="server" ID="SOCIOS_NOMBRE" FieldLabel="Nombre del Socio" AnchorHorizontal="97.5%"
                                            ReadOnly="true" />
                                        <ext:TextArea runat="server" ID="TOTALRECIBIDO" FieldLabel="Total recibido" AnchorHorizontal="97.5%"
                                            ReadOnly="true" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:FieldSet ID="FieldSet1" runat="server" Title="Descuentos" Layout="FormLayout" LabelWidth="80"
                            PaddingSummary="10">
                            <Items>
                                <ext:Container ID="Container6" Layout="ColumnLayout" Height="26" AnchorHorizontal="100%"
                                    runat="server">
                                    <Items>
                                        <ext:Container ID="Container7" runat="server" Layout="FormLayout" AnchorVertical="100%"
                                            ColumnWidth="0.333">
                                            <Items>
                                                <ext:NumberField MaskRe="%" runat="server" ID="txtPorcentajeDefecto" FieldLabel="% Defecto"
                                                    AnchorHorizontal="90%" Text="0" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container8" runat="server" Layout="FormLayout" AnchorVertical="100%"
                                            ColumnWidth="0.333">
                                            <Items>
                                                <ext:NumberField runat="server" ID="txtPorcentajeHumedad" FieldLabel="% Humedad"
                                                    AnchorHorizontal="90%" Text="0" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container9" runat="server" Layout="FormLayout" AnchorVertical="100%"
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
                        <ext:Panel runat="server" ID="GridContainer" Height="340">
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
                                <ext:GridPanel ID="gridPesos" runat="server" StoreID="stPesos" Height="220px">                                    
                                  <%--  <CustomConfig>
                                        <ext:ConfigItem Name="width" Value="75%" Mode="Value" />
                                    </CustomConfig>--%>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:NumberColumn DataIndex="CANT_SACOS" Header="Cantidad de Sacos">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField1" runat="server">
                                                    </ext:NumberField>
                                                </Editor>
                                            </ext:NumberColumn>
                                            <ext:NumberColumn DataIndex="PESO" Header="Peso">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField2" runat="server">
                                                    </ext:NumberField>
                                                </Editor>
                                            </ext:NumberColumn>
                                        </Columns>
                                    </ColumnModel>
                                    <Plugins>
                                        <ext:RowEditor ID="RowEditor1" runat="server">
                                        </ext:RowEditor>
                                    </Plugins>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar1" runat="server">
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
                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Height="100"
                                    MinHeight="100">
                                    <CustomConfig>
                                        <%--<ext:ConfigItem Name="width" Value="75%" Mode="Value" />--%>
                                    </CustomConfig>
                                    <Items>
                                        <ext:Container ID="Container11" runat="server" ColumnWidth=".5">
                                            <Items>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container12" runat="server" ColumnWidth=".375">
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
            </ext:Window>

        <ext:Window ID="winAgregarPeso" runat="server" CloseAction="Hide" Width="400" Height="170" Hidden="true">
        <Listeners>
            <Hide Handler="#{txtAddSacos}.setValue(0); #{txtAddPeso}.setValue(0); " />
            <Show Handler="#{txtAddSacos}.setValue(0); #{txtAddPeso}.setValue(0); #{txtAddPeso}.focus();" />
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
