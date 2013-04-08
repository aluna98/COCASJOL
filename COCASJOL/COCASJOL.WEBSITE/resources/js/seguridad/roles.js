/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var Grid = null;
var GridStore = null;
var AddWindow = null;
var AddForm = null;
var EditWindow = null;
var EditForm = null;

var AlertSelMsgTitle = "Atención";
var AlertSelMsg = "Debe seleccionar 1 elemento";

var ConfirmMsgTitle = "Rol";
var ConfirmUpdate = "Seguro desea modificar el rol?";
var ConfirmDelete = "Seguro desea eliminar el rol?";

var PageX = {
    _index: 0,

    setReferences: function () {
        Grid = RolesGridP;
        GridStore = RolesSt;
        AddWindow = AgregarRolWin;
        AddForm = AddRolFormP
        EditWindow = EditarRolWin;
        EditForm = EditarRolFormP;
    },

    add: function () {
        AddWindow.show();
    },

    insert: function () {
        var fields = AddForm.getForm().getFieldValues(false, "dataIndex");

        Grid.insertRecord(0, fields, false);
        AddForm.getForm().reset();
    },

    insertPrivilege: function () {
        if (PrivilegiosNoDeRolGridP.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm('Agregar Privilegios', 'Seguro desea agregar estos privilegios?', function (btn, text) {
                if (btn == 'yes') {
                    Ext.net.DirectMethods.AddPrivilegiosAddPrivilegioBtn_Click(
                        {
                            success: function () {
                                PrivilegiosDeRolSt.reload();
                                Ext.net.DirectMethods.EnviarCorreoPrivilegiosNuevos();
                                PrivilegiosNoDeRolesSt.reload();
                                PrivilegiosNoDeRolGridP.getSelectionModel().clearSelections();
                                Ext.Msg.alert('Agregar Privilegios', 'Privilegios agregados exitosamente.');
                            },
                            eventMask: {
                                showMask: true, target: 'customtarget', customTarget: PrivilegiosNoDeRolGridP
                            },
                            failure: function () {
                                Ext.Msg.alert('Agregar Privilegios', 'Error al agregar privilegios.');
                            }
                        });
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
        }
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
            PrivilegiosDeRolGridP.getStore().removeAll();
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

    removePrivilege: function () {
        if (PrivilegiosDeRolGridP.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm('Eliminar Privilegios', 'Seguro desea eliminar estos privilegios?', function (btn, text) {
                if (btn == 'yes') {
                    Ext.net.DirectMethods.EditRolDeletePrivilegioBtn_Click(
                        {
                            success: function () {
                                PrivilegiosDeRolSt.reload();
                                Ext.Msg.alert('Eliminar Privilegios', 'Privilegios eliminados exitosamente.');
                            },
                            eventMask: {
                                showMask: true, target: 'customtarget', customTarget: PrivilegiosDeRolGridP
                            },
                            failure: function () {
                                Ext.Msg.alert('Eliminar Privilegios', 'Error al eliminar privilegios.');
                            }
                        });
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
    },

    keyUpEvent2: function (sender, e) {
        if (e.getKey() == 13)
            PrivilegiosDeRolSt.reload();
    },

    keyUpEvent3: function (sender, e) {
        if (e.getKey() == 13)
            PrivilegiosNoDeRolesSt.reload();
    },

    clearFilter: function () {
        f_ROL_ID.reset();
        f_ROL_NOMBRE.reset();
        f_ROL_DESCRIPCION.reset();

        RolesSt.reload();
    },

    clearFilterPrivs: function () {
        f_PRIV_ID.reset();
        f_PRIV_LLAVE.reset();
        f_PRIV_NOMBRE.reset();

        PrivilegiosDeRolSt.reload();
    },

    clearFilterNotPrivs: function () {
        f2_PRIV_ID.reset();
        f2_PRIV_LLAVE.reset();
        f2_PRIV_NOMBRE.reset();

        PrivilegiosNoDeRolesSt.reload();
    },

    HideButtons: function () {
        EditPreviousBtn.hide();
        EditNextBtn.hide();
        EditGuardarBtn.hide();
    },

    ShowButtons: function () {
        EditPreviousBtn.show();
        EditNextBtn.show();
        EditGuardarBtn.show();
    },

    gridKeyUpEvent: function (sender, e) {
        var k = e.getKey();

        switch (k) {
            case 45: //INSERT
                this.add();
                break;
            case 13: //ENTER
                this.edit();
                break;
            case 46: //DELETE
                this.remove();
                break;
            default:
                break;
        }

    },

    navHome: function () {
        if (Grid.getStore().getTotalCount() == 0)
            Grid.getStore().reload();
        PagingToolbar1.moveFirst();
    },

    navPrev: function () {
        if (Grid.getStore().getTotalCount() > 0)
            PagingToolbar1.movePrevious();
    },

    navNext: function () {
        if (Grid.getStore().getTotalCount() > 0)
            if (Grid.getStore().getTotalCount() > (PagingToolbar1.cursor + PagingToolbar1.pageSize))
                PagingToolbar1.moveNext();
    },

    navEnd: function () {
        if (Grid.getStore().getTotalCount() == 0)
            Grid.getStore().reload();
        PagingToolbar1.moveLast();
    }
};