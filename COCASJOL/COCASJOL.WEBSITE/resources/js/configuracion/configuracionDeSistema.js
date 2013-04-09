/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var EditForm = null;

var ConfirmMsgTitle = "Configuración de Sistema";
var ConfirmUpdate = "Seguro desea modificar la configuración de sistema?";
var ConfirmDelete = "Seguro desea eliminar la configuración de sistema?";

var PageX = {
    setReferences: function () {
        EditForm = FormPanel1;
    },

    update: function () {
        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                Ext.net.DirectMethods.GuardarConfiguracionBtn_Click(
                {
                    success: function () {
                        Ext.Msg.alert('Guardar', 'Configuración de Sistema actualizada exitosamente.');
                    },
                    eventMask: {
                        showMask: true, target: 'customtarget', customTarget: EditForm
                    },
                    failure: function () {
                        Ext.Msg.alert('Guardar', 'Error al actualizar Configuración de Sistema.');
                    }
                });
            }
        });
    }
};