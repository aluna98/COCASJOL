/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />
/// <reference path="~/resources/js/md5.js" />

var Grid = null;
var GridStore = null;
var AddWindow = null;
var AddForm = null;
var EditWindow = null;
var EditForm = null;

var AlertSelMsgTitle = "Atención";
var AlertSelMsg = "Debe seleccionar 1 elemento";

var ConfirmMsgTitle = "Usuario";
var ConfirmUpdate = "Seguro desea modificar el usuario?";
var ConfirmDelete = "Seguro desea eliminar el usuario?";

var PageX = {
    _index: 0,

    setReferences: function () {
        Grid = UsuariosGridP;
        GridStore = UsuariosSt;
        AddWindow = AgregarUsuarioWin;
        AddForm = AddUsuarioFormP
        EditWindow = EditarUsuarioWin;
        EditForm = EditarUsuarioFormP;
    },

    add: function () {
        AddWindow.show();
    },

    insert: function () {
        var pss = AddPasswordTxt.getValue();
        AddPasswordTxt.setValue(md5(AddPasswordTxt.getValue()));
        var fields = AddForm.getForm().getFieldValues(false, "dataIndex");

        Grid.insertRecord(0, fields, false);
        Ext.net.DirectMethods.EnviarCorreoUsuarioNuevo(pss);
        AddForm.getForm().reset();
    },

    insertRol: function () {
        if (RolesNoDeUsuarioGridP.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm('Agregar Roles', 'Seguro desea agregar estos roles?', function (btn, text) {
                if (btn == 'yes') {
                    Ext.net.DirectMethods.AddRolesAddRolBtn_Click({
                        success: function () {
                            RolesDeUsuarioSt.reload();
                            Ext.net.DirectMethods.EnviarCorreoRolesNuevos();
                            RolesNoDeUsuarioSt.reload();
                            RolesNoDeUsuarioGridP.getSelectionModel().clearSelections();
                            Ext.Msg.alert('Agregar Roles', 'Roles agregado exitosamente.');
                        }
                    }, { eventMask: { showMask: true, target: 'customtarget', customTarget: RolesNoDeUsuarioGridP} }, { failure: function () { Ext.Msg.alert('Agregar Roles', 'Error al agregar roles.'); } });
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

    editPassword: function () {
        if (Grid.getSelectionModel().hasSelection()) {
            var record = Grid.getSelectionModel().getSelected();
            var index = Grid.store.indexOf(record);
            this.setIndex(index);

            rec = this.getRecord();

            if (rec != null) {
                CambiarClaveWin.show();
                FormPanel2.getForm().loadRecord(rec);
                FormPanel2.record = rec;
            }
        } else {
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
        }
    },

    edit: function () {
        if (Grid.getSelectionModel().hasSelection()) {
            var record = Grid.getSelectionModel().getSelected();
            var index = Grid.store.indexOf(record);
            this.setIndex(index);
            this.open();
        } else {
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
            RolesDeUsuarioGridP.getStore().removeAll();
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

    updatePassword: function () {
        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                var pss = CambiarClaveConfirmarTxt.getValue();
                var encrypted = md5(CambiarClaveConfirmarTxt.getValue());
                CambiarClaveTxt.setValue(encrypted);
                CambiarClaveConfirmarTxt.setValue(encrypted);
                Ext.net.DirectMethods.CambiarClaveGuardarBtn_Click({
                    success: function () {
                        Ext.net.DirectMethods.EnviarCorreoUsuarioPasswordNuevo(pss);
                        Ext.Msg.alert('Cambiar Contraseña', 'Contraseña actualizada exitosamente.');
                        FormPanel2.getForm().reset();
                        CambiarClaveWin.hide();
                    }
                }, { eventMask: { showMask: true, target: 'customtarget', customTarget: FormPanel2} });
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

    removeRol: function () {
        if (RolesDeUsuarioGridP.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm('Eliminar Roles', 'Seguro desea eliminar estos roles?', function (btn, text) {
                if (btn == 'yes') {
                    Ext.net.DirectMethods.EditUsuarioDeleteRolBtn_Click({ success: function () { RolesDeUsuarioSt.reload(); Ext.Msg.alert('Eliminar Roles', 'Roles eliminados exitosamente.'); } }, { eventMask: { showMask: true, target: 'customtarget', customTarget: RolesDeUsuarioGridP} }, { failure: function () { Ext.Msg.alert('Eliminar Roles', 'Error al eliminar roles.'); } });
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
            RolesDeUsuarioSt.reload();
    },

    keyUpEvent3: function (sender, e) {
        if (e.getKey() == 13)
            RolesNoDeUsuarioSt.reload();
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

    clearFilter: function () {
        f_USR_USERNAME.reset();
        f_USR_NOMBRE.reset();
        f_USR_SEGUNDO_NOMBRE.reset();
        f_USR_APELLIDO.reset();
        f_USR_SEGUNDO_APELLIDO.reset();
        f_USR_CEDULA.reset();
        f_USR_CORREO.reset();
        f_USR_PUESTO.reset();

        UsuariosSt.reload();
    },

    clearFilterRols: function () {
        f_ROL_ID.reset();
        f_ROL_NOMBRE.reset();

        RolesDeUsuarioSt.reload();
    },

    clearFilterNotRols: function () {
        f2_ROL_ID.reset();
        f2_ROL_NOMBRE.reset();

        RolesNoDeUsuarioSt.reload();
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