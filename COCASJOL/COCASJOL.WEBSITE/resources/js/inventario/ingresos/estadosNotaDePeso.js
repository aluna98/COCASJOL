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

var ConfirmMsgTitle = "Estado de Nota de Peso";
var ConfirmUpdate = "Seguro desea modificar el estado para las notas de peso?";
var ConfirmDelete = "Seguro desea eliminar el estado para las notas de peso?";

var PageX = {
    _index: 0,

    setReferences: function () {
        Grid = EstadosNotaGridP;
        GridStore = EstadosNotaSt;
        AddWindow = AgregarEstadosNotaWin;
        AddForm = AgregarEstadosNotaFormP;
        EditWindow = EditarEstadosNotaWin;
        EditForm = EditarEstadosNotaFormP;
        FormatKeysSt.reload();
    },

    add: function () {
        AddWindow.show();
        Ext.net.DirectMethods.AddEstadosNotaSiguienteSt_Refresh();
    },

    insert: function () {
        var fields = AddForm.getForm().getFieldValues(false, "dataIndex");

        Ext.net.DirectMethods.AddGuardarBtn_Click(
        {
            success: function () {
                GridStore.reload();
                Ext.Msg.alert('Estados de Nota de Peso', 'Estado agregado exitosamente.');
            },
            eventMask: {
                showMask: true, target: 'customtarget', customTarget: AddWindow
            },
            failure: function () {
                Ext.Msg.alert('Estados de Nota de Peso', 'Error al agregar estado.');
            }
        });
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
            Ext.net.DirectMethods.EditEstadosNotaSiguienteSt_Refresh();
            Ext.net.DirectMethods.LoadPlantilla();
        }
    },

    update: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                Ext.net.DirectMethods.EditGuardarBtn_Click(
                {
                    success: function () {
                        GridStore.reload();
                        Ext.Msg.alert('Estados de Nota de Peso', 'Estado actualizado exitosamente.');
                    },
                    eventMask: {
                        showMask: true, target: 'customtarget', customTarget: EditWindow
                    },
                    failure: function () {
                        Ext.Msg.alert('Estados de Nota de Peso', 'Error al actualizar estado.');
                    }
                })
            }
        });
    },

    remove: function () {
        if (Grid.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitle, ConfirmDelete, function (btn, text) {
                if (btn == 'yes') {
                    var record = Grid.getSelectionModel().getSelected();
                    Ext.net.DirectMethods.EliminarBtn_Click(record.data.ESTADOS_NOTA_ID,
                    {
                        success: function () {
                            GridStore.reload();
                            Ext.Msg.alert('Estados de Nota de Peso', 'Estado eliminado exitosamente.');
                        },
                        eventMask: {
                            showMask: true, target: 'customtarget', customTarget: Grid
                        },
                        failure: function () {
                            Ext.Msg.alert('Estados de Nota de Peso', 'Error al eliminar estado.');
                        }
                    });
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
        }
    },

    activate: function () {
        if (Grid.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitle, "Seguro desea activar el estado?", function (btn, text) {
                if (btn == 'yes') {
                    var record = Grid.getSelectionModel().getSelected();
                    Ext.net.DirectMethods.ActivarBtn_Click(record.data.ESTADOS_NOTA_ID, LoggedUserHdn.getValue(),
                    {
                        success: function () {
                            GridStore.reload();
                            Ext.Msg.alert('Estados de Nota de Peso', 'Estado activado exitosamente.');
                        },
                        eventMask: {
                            showMask: true, target: 'customtarget', customTarget: Grid
                        },
                        failure: function () {
                            Ext.Msg.alert('Estados de Nota de Peso', 'Error al activar estado.');
                        }
                    });
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
        }
    },

    deactivate: function () {
        if (Grid.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitle, "Seguro desea desactivar el estado?", function (btn, text) {
                if (btn == 'yes') {
                    var record = Grid.getSelectionModel().getSelected();
                    Ext.net.DirectMethods.DesactivarBtn_Click(record.data.ESTADOS_NOTA_ID, LoggedUserHdn.getValue(),
                    {
                        success: function () {
                            GridStore.reload();
                            Ext.Msg.alert('Estados de Nota de Peso', 'Estado desactivado exitosamente.');
                        },
                        eventMask: {
                            showMask: true, target: 'customtarget', customTarget: Grid
                        },
                        failure: function () {
                            Ext.Msg.alert('Estados de Nota de Peso', 'Error al desactivar estado.');
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

    reloadGridStore: function () {
        GridStore.reload();
    },

    clearFilter: function () {
        f_ESTADOS_NOTA_ID.reset();
        f_ESTADOS_NOTA_SIGUIENTE.reset();
        f_ESTADOS_NOTA_LLAVE.reset();
        f_ESTADOS_NOTA_NOMBRE.reset();

        EstadosNotaSt.reload();
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