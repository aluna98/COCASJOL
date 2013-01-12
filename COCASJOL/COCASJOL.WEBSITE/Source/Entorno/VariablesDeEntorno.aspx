<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VariablesDeEntorno.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Entorno.VariablesDeEntorno" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
    <script type="text/javascript">
        var Grid = null;
        var GridStore = null;
        var AddWindow = null;
        var AddForm = null;
        var EditWindow = null;
        var EditForm = null;

        var AlertSelMsgTitle = "Atención";
        var AlertSelMsg = "Debe seleccionar 1 elemento";

        var ConfirmMsgTitle = "Variables de Entorno";
        var ConfirmUpdate = "Seguro desea modificar las variables de entorno?";
        var ConfirmDelete = "Seguro desea eliminar la variable de entorno?";

        var PageX = {
            setReferences: function () {
                Grid = VariablesEntornoGridP;
                GridStore = VariablesEntornoSt;
            },

            update: function () {
                Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
                    if (btn == 'yes') {
                        Ext.net.DirectMethods.GuardarVariablesBtn_Click(this.variablesToJson());
                    }
                });
            }
        };

        var variablesToJson = function () {
            var items = Grid.getStore().data;
            var ret = [];
            for (var i = 0; i < items.length; i++) {
                ret.push({ VARIABLES_LLAVE: items.items[i].data.VARIABLES_LLAVE, VARIABLES_VALOR: items.items[i].data.VARIABLES_VALOR });
            }

            return Ext.encode(ret);
        }

        var HideButtons = function () {
            EditPreviousBtn.hide();
            EditNextBtn.hide();
            EditGuardarBtn.hide();
        }

        var ShowButtons = function () {
            EditPreviousBtn.show();
            EditNextBtn.show();
            EditGuardarBtn.show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server"  DisableViewState="true" >
            <Listeners>
                <DocumentReady Handler="PageX.setReferences();" />
            </Listeners>
        </ext:ResourceManager>

        <aud:Auditoria runat="server" ID="AudWin" />
        
        <asp:ObjectDataSource ID="VariablesEntornoDS" runat="server"
                TypeName="COCASJOL.LOGIC.Entorno.VariablesDeEntornoLogic"
                SelectMethod="GetVariablesDeEntorno" >
                <SelectParameters>
                    <asp:ControlParameter Name="VARIABLES_LLAVE"       Type="String"   ControlID="nullHdn"  PropertyName="Text" />
                    <asp:ControlParameter Name="VARIABLES_NOMBRE"      Type="String"   ControlID="nullHdn"  PropertyName="Text" />
                    <asp:ControlParameter Name="VARIABLES_DESCRIPCION" Type="String"   ControlID="nullHdn"  PropertyName="Text" />
                    <asp:ControlParameter Name="VARIABLES_VALOR"       Type="String"   ControlID="nullHdn"  PropertyName="Text" />
                    <asp:ControlParameter Name="CREADO_POR"            Type="String"   ControlID="nullHdn"  PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"        Type="DateTime" ControlID="nullHdn"  PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"        Type="String"   ControlID="nullHdn"  PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION"    Type="DateTime" ControlID="nullHdn"  PropertyName="Text" DefaultValue="" />
                </SelectParameters>
        </asp:ObjectDataSource>

        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Icon="Database" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="VariablesEntornoGridP" runat="server" Title="Variables de Entorno"
                            Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="VariablesEntornoSt" runat="server" DataSourceID="VariablesEntornoDS" AutoSave="true" SkipIdForNewRecords="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="VARIABLES_LLAVE">
                                            <Fields>
                                                <ext:RecordField Name="VARIABLES_LLAVE"       />
                                                <ext:RecordField Name="VARIABLES_NOMBRE"      />
                                                <ext:RecordField Name="VARIABLES_DESCRIPCION" />
                                                <ext:RecordField Name="VARIABLES_VALOR"       />
                                                <ext:RecordField Name="CREADO_POR"            />
                                                <ext:RecordField Name="FECHA_CREACION"        Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"        />
                                                <ext:RecordField Name="FECHA_MODIFICACION"    Type="Date" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <Plugins>
                                <ext:EditableGrid ID="EditableGrid1" runat="server" />
                            </Plugins>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column DataIndex="VARIABLES_NOMBRE" Header="Nombre" Sortable="true" Editable="false"></ext:Column>
                                    <ext:Column DataIndex="VARIABLES_VALOR" Header="Valor" Sortable="true">
                                        <Editor>
                                            <ext:TextField runat="server" AllowBlank="false"></ext:TextField>
                                        </Editor>
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="GuardarVariablesBtn" runat="server" Text="Guardar" Icon="DatabaseSave">
                                            <%--<DirectEvents>
                                                <Click OnEvent="GuardarVariablesBtn_Click" Success="Ext.Msg.alert('Guardar Variables de Entorno', 'Variables de entorno guardadas exitosamente!');">
                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{VariablesEntornoGridP}" />
                                                </Click>
                                            </DirectEvents>--%>
                                            <Listeners>
                                                <Click Handler="PageX.update();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarFill runat="server" />
                                        <ext:Button ID="AuditoriaBtn" runat="server" Text="Auditoria" Icon="Cog">
                                            <Listeners>
                                                <Click Handler="PageX.showAudit();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="GridView1" ForceFit="true" ScrollOffset="2" runat="server" />
                            </View>
                            <%--<Listeners>
                                <RowDblClick Handler="Ext.Msg.alert('lol', Ext.encode(#{VariablesEntornoGridP}.getRowsValues({selectedOnly : true})));" />
                            </Listeners>--%>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>