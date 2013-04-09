/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var Grid = null;
var GridStore = null;

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
                Ext.net.DirectMethods.GuardarVariablesBtn_Click(this.variablesToJson(),
                {
                    success: function () {
                        GridStore.reload();
                        Ext.Msg.alert('Guardar', 'Variables de Entorno actualizadas exitosamente.');
                    },
                    eventMask: {
                        showMask: true, target: 'customtarget', customTarget: Grid
                    },
                    failure: function () {
                        Ext.Msg.alert('Guardar', 'Error al actualizar Variables de Entorno.');
                    }
                });
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
};