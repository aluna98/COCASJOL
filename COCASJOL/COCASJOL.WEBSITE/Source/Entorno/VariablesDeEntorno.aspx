<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VariablesDeEntorno.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Entorno.VariablesDeEntorno" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

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
        var ConfirmUpdate = "Seguro desea modificar la variable de entorno?";
        var ConfirmDelete = "Seguro desea eliminar la variable de entorno?";

        var PageX = {
            _index: 0,

            setReferences: function () {
                Grid = VariablesEntornoGridP;
                GridStore = VariablesEntornoSt;
                AddWindow = AgregarVariablesEntornoWin;
                AddForm = AgregarVariablesEntornoFormP;
                EditWindow = EditarVariablesEntornoWin;
                EditForm = EditarVariablesEntornoFormP;
            },

            add: function () {
                AddWindow.show();
            },

            insert: function () {
                var fields = AddForm.getForm().getFieldValues(false, "dataIndex");

                Grid.insertRecord(0, fields, false);
                AddForm.getForm().reset();
            },

            getIndex: function () {
                return this._index;
            },

            setIndex: function (index) {
                if (index > -1 && index < Grid.getStore().getCount()) {
                    this._index = index;
                }
            },

            getRecord: function () {
                var rec = Grid.getStore().getAt(this.getIndex());  // Get the Record

                if (rec != null) {
                    return rec;
                }
            },

            edit: function () {
                if (Grid.getSelectionModel().hasSelection()) {
                    var record = Grid.getSelectionModel().getSelected();
                    var index = Grid.store.indexOf(record);
                    this.setIndex(index);
                    this.open();
                } else {
                    var msg = Ext.Msg;
                    Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
                }
            },

            edit2: function (index) {
                this.setIndex(index);
                this.open();
            },

            next: function () {
                this.edit2(this.getIndex() + 1);
            },

            previous: function () {
                this.edit2(this.getIndex() - 1);
            },

            open: function () {
                rec = this.getRecord();

                if (rec != null) {
                    EditWindow.show();
                    EditForm.getForm().loadRecord(rec);
                    EditForm.record = rec;
                }
            },

            update: function () {
                if (EditForm.record == null) {
                    return;
                }

                Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
                    if (btn == 'yes') {
                        EditForm.getForm().updateRecord(EditForm.record);
                    }
                });
            },

            remove: function () {
                if (Grid.getSelectionModel().hasSelection()) {
                    Ext.Msg.confirm(ConfirmMsgTitle, ConfirmDelete, function (btn, text) {
                        if (btn == 'yes') {
                            var record = Grid.getSelectionModel().getSelected();
                            Grid.deleteRecord(record);
                        }
                    });
                } else {
                    var msg = Ext.Msg;
                    Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
                }
            },

            keyUpEvent: function (sender, e) {
                if (e.getKey() == 13)
                    GridStore.reload();
            }
        };

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
        </ext:ResourceManager>
        
        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Icon="Database" Layout="Fit">
                    <Items>
                        <ext:PropertyGrid ID="VariablesEntornoGridP" runat="server" Title="Variables de Entorno"
                            Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <View>
                                <ext:GridView ID="GridView1" ForceFit="true" ScrollOffset="2" runat="server" />
                            </View>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="GuardarVariablesBtn" runat="server" Text="Guardar" Icon="DatabaseSave">
                                            <DirectEvents>
                                                <Click OnEvent="GuardarVariablesBtn_Click" Success="Ext.Msg.alert('Guardar Variables de Entorno', 'Variables de entorno guardadas exitosamente!');">
                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{VariablesEntornoGridP}" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Source>
                                <ext:PropertyGridParameter Name="tmp"/>
                            </Source>
                        </ext:PropertyGrid>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>