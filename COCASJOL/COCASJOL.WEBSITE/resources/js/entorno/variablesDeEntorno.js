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

var ConfirmMsgTitle = "Variables de Entorno";
var ConfirmUpdate = "Seguro desea modificar las variables de entorno?";
var ConfirmDelete = "Seguro desea eliminar la variable de entorno?";

var PageX = {
    setReferences: function () {
        Grid = VariablesEntornoGridP;
        GridStore = VariablesEntornoSt;
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
};